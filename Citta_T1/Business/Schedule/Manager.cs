using Citta_T1.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Citta_T1.Controls;
using Citta_T1.Controls.Move;
using Citta_T1.Business.Option;
using System.Diagnostics;
using Citta_T1.Business.Schedule.Cmd;

namespace Citta_T1.Business.Schedule
{
    public enum ModelStatus
    {
        Running,   //模型的调度器正在运行
        Pause,     //模型的调度器暂停
        Stop,      //模型的调度器中止
        Done,      //模型的调度器运行完成
        GifDone,   //模型的调度器运行完成，运行动图完成
        Null       //模型的调度器未初始化
    }


    class Manager
    {

        public delegate void UpdateLog(string log);//声明一个更新主线程日志的委托
        public UpdateLog UpdateLogDelegate;

        public delegate void AccomplishTask(Manager manager);//声明一个在完成任务时通知主线程的委托
        public AccomplishTask TaskCallBack;

        public delegate void UpdateGif(Manager manager);//声明一个更新运作动图的委托
        public UpdateGif UpdateGifDelegate;

        private TripleListGen tripleList;
        private Thread scheduleThread = null;
        private ModelStatus modelStatus;

        private List<Triple> currentModelTripleList = new List<Triple>();

        CancellationTokenSource tokenSource = new CancellationTokenSource();
        ManualResetEvent resetEvent = new ManualResetEvent(true);

        //并行任务集合
        private List<Task> parallelTasks;

        public TripleListGen TripleList { get => tripleList; set => tripleList = value; }
        public ModelStatus ModelStatus { get => modelStatus; set => modelStatus = value; }

        public Manager()
        {
            this.modelStatus = ModelStatus.Null;
        }

        public void GetCurrentModelTripleList(ModelDocument currentModel)
        {
            this.tripleList = new TripleListGen(currentModel);
            this.tripleList.GenerateList();
            this.currentModelTripleList = this.tripleList.CurrentModelTripleList;
        }

        public void ChangeStatus(ElementStatus oldStatus, ElementStatus newStatus)
        {
            foreach (Triple pauseTri in this.currentModelTripleList.FindAll(c => c.OperateElement.Status == oldStatus))
            {
                pauseTri.OperateElement.Status = newStatus;
                UpdateLogDelegate(pauseTri.TripleName + "的状态由" + oldStatus.ToString() + "变更为" + newStatus.ToString());
            }
        }

        public void Pause()
        {
            this.modelStatus = ModelStatus.Pause;
            ChangeStatus(ElementStatus.Runnnig, ElementStatus.Suspend);
            resetEvent.Reset();
        }

        public void Continue()
        {
            this.modelStatus = ModelStatus.Running;
            ChangeStatus(ElementStatus.Suspend, ElementStatus.Runnnig);
            resetEvent.Set();
        }

        public void Stop()
        {
            this.modelStatus = ModelStatus.Stop;
            ChangeStatus(ElementStatus.Suspend, ElementStatus.Stop);
            ChangeStatus(ElementStatus.Runnnig, ElementStatus.Stop);
            resetEvent.Dispose();
            foreach (Task currentTask in parallelTasks)
            {
                if (currentTask != null)
                {
                    if (currentTask.Status == TaskStatus.Running) { }
                    {
                        //终止task线程
                        tokenSource.Cancel();
                    }
                }
            }
            CloseThread();

        }

        public void CloseThread()
        {
            if (scheduleThread != null)
            {
                if (scheduleThread.IsAlive)
                {
                    scheduleThread.Abort();
                }
            }
        }

        public int CurrentModelTripleReadyNum()
        {
            int count = 0;
            foreach (Triple tmpTri in currentModelTripleList)
            {
                if (tmpTri.OperateElement.Status == ElementStatus.Ready)
                {
                    count++;
                }
            }
            return count;
        }




        public void Start()
        {
            if (CurrentModelTripleReadyNum() == 0)
            {
                UpdateLogDelegate("当前模型的算子均已运算完毕");
                return;
            }
            this.modelStatus = ModelStatus.Running;
            this.tokenSource = new CancellationTokenSource();
            this.resetEvent = new ManualResetEvent(true);

            scheduleThread = new Thread(new ThreadStart(() => StartTask()));
            scheduleThread.IsBackground = true;
            scheduleThread.Start();

        }

        public void StartTask()
        {
            parallelTasks = new List<Task>();
            foreach (Triple tmpTri in currentModelTripleList)
            {
                if (tmpTri.OperateElement.Status == ElementStatus.Done)
                {
                    continue;
                }
                else
                {
                    //判断数据节点是否算完
                    foreach (ModelElement tmpDE in tmpTri.DataElements)
                    {
                        if (tmpDE.Type == ElementType.Result)
                        {
                            while (tmpDE.Status != ElementStatus.Done)
                            {
                                Thread.Sleep(1000);
                            }
                        }
                    }
                    //该三元组未算过，且数据节点都已经算完，开一个子任务去算
                    Task<bool> t = new Task<bool>(() => TaskMethod(tmpTri), tokenSource.Token);
                    t.Start();
                    parallelTasks.Add(t);

                }
            }

            Task.WaitAll(new Task[] { Task.WhenAll(parallelTasks.ToArray()) });

            this.modelStatus = ModelStatus.GifDone;
            TaskCallBack(this);
            UpdateGifDelegate(this);
            this.modelStatus = ModelStatus.Done;
            Thread.Sleep(1000);
            UpdateGifDelegate(this);
        }





        bool TaskMethod(Triple triple)
        {
            try
            {
                tokenSource.Token.ThrowIfCancellationRequested();

                triple.OperateElement.Status = ElementStatus.Runnnig;
                UpdateLogDelegate(triple.TripleName + "开始运行");

                List<string> cmds = new List<string>();
                string cmd = "";
                switch (triple.OperateElement.SubType)
                {
                    case ElementSubType.MaxOperator: cmds = (new MaxOperatorCmd(triple)).GenCmd(); break;
                    case ElementSubType.FilterOperator: cmds = (new FilterOperatorCmd(triple)).GenCmd(); break;
                    case ElementSubType.CollideOperator: cmds = (new CollideOperatorCmd(triple)).GenCmd(); break;
                    case ElementSubType.UnionOperator: cmds = (new UnionOperatorCmd(triple)).GenCmd(); break;
                    case ElementSubType.DifferOperator: cmds = (new DifferOperatorCmd(triple)).GenCmd(); break;
                    case ElementSubType.RandomOperator: cmds = (new RandomOperatorCmd(triple)).GenCmd(); break;
                    case ElementSubType.MinOperator: cmds = (new MinOperatorCmd(triple)).GenCmd(); break;
                    case ElementSubType.AvgOperator: cmds = (new AvgOperatorCmd(triple)).GenCmd(); break;
                    case ElementSubType.SortOperator: cmds = (new SortOperatorCmd(triple)).GenCmd(); break;
                    case ElementSubType.FreqOperator: cmds = (new FreqOperatorCmd(triple)).GenCmd(); break;
                    case ElementSubType.JoinOperator: cmds = (new JoinOperatorCmd(triple)).GenCmd(); break;
                }

                foreach (string cmd1 in cmds)
                {
                    UpdateLogDelegate("执行命令: " + cmd1);
                    RunLinuxCommand(cmd1);
                }

                resetEvent.WaitOne();
                //在改变状态之前设置暂停，虽然暂停了但是后台还在继续跑
                triple.OperateElement.Status = ElementStatus.Done;
                triple.ResultElement.Status = ElementStatus.Done;
                triple.IsOperated = true;
                UpdateLogDelegate(triple.TripleName + "结束运行");
            }
            catch (Exception ex)
            {
            }
            return triple.IsOperated;

        }

        public void RunLinuxCommand(string cmd)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/c " + cmd;
            p.StartInfo.UseShellExecute = false; // 不显示用户界面
            p.StartInfo.RedirectStandardOutput = true; // 是否重
            p.StartInfo.CreateNoWindow = true;

            try
            {
                if (p.Start())//开始进程  
                {
                    p.WaitForExit(); //等待进程结束，等待时间为指定的毫秒   
                }
            }
            catch (Exception ex)
            {
                //异常停止的处理方法
                //TODO
            }
            finally
            {
                if (p != null)
                    p.Close();
            }
        }

    }
}
