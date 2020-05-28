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
using NPOI.HSSF.Record;
using System.Windows.Forms;

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

        public delegate void UpdateBar(Manager manager);//声明一个更新进度条的委托
        public UpdateGif UpdateBarDelegate;

        private TripleListGen tripleList;
        private Thread scheduleThread = null;
        private ModelStatus modelStatus;

        private List<Triple> currentModelTripleList = new List<Triple>();

        CancellationTokenSource tokenSource = new CancellationTokenSource();
        ManualResetEvent resetEvent = new ManualResetEvent(true);

        //并行任务集合
        private List<Task> parallelTasks;

        //cmd进程集合
        private List<Process> cmdProcessList;

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

        public void GetCurrentModelRunhereTripleList(ModelDocument currentModel, ModelElement stopElement)
        {
            this.tripleList = new TripleListGen(currentModel, stopElement);
            this.tripleList.GenerateList();
            this.currentModelTripleList = this.tripleList.CurrentModelTripleList;
        }

        public void ChangeStatus(ElementStatus oldStatus, ElementStatus newStatus)
        {
            foreach (Triple triple in this.currentModelTripleList.FindAll(c => c.OperateElement.Status == oldStatus))
            {
                triple.OperateElement.Status = newStatus;
                UpdateLogDelegate(triple.TripleName + "的状态由" + oldStatus.ToString() + "变更为" + newStatus.ToString());
            }
        }

        public void Reset()
        {
            foreach (Triple triple in this.currentModelTripleList.FindAll(c => c.OperateElement.Status == ElementStatus.Stop || c.OperateElement.Status == ElementStatus.Done || c.OperateElement.Status == ElementStatus.Warn))
            {
                triple.OperateElement.Status = ElementStatus.Ready;
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

        //锁
        private readonly object _objLock = new object();

        public void Stop()
        {
            this.modelStatus = ModelStatus.Stop;
            ChangeStatus(ElementStatus.Suspend, ElementStatus.Stop);
            ChangeStatus(ElementStatus.Runnnig, ElementStatus.Stop);
            resetEvent.Dispose();

            lock (_objLock)
            {
                foreach (Process p in cmdProcessList)
                {
                    UpdateLogDelegate("当前process名字：" + p.ProcessName);
                    if (p.ProcessName == "cmd")
                    {
                        p.Kill();
                    }
                }
            }




            foreach (Task currentTask in parallelTasks)
            {
                if (currentTask != null)
                {
                    if (currentTask.Status == TaskStatus.Running)
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

        public int CurrentModelTripleStatusNum(ElementStatus status)
        {
            int count = 0;
            foreach (Triple tmpTri in currentModelTripleList)
            {
                if (tmpTri.OperateElement.Status == status)
                {
                    count++;
                }
            }
            return count;
        }

        public bool IsAllOperatorDone()
        {
            if (CurrentModelTripleStatusNum(ElementStatus.Done) == this.currentModelTripleList.Count())
            {
                UpdateLogDelegate("当前模型的算子均已运算完毕");
                return true;
            }
            return false;
        }


        public void Start()
        {
            this.modelStatus = ModelStatus.Running;
            this.tokenSource = new CancellationTokenSource();
            this.resetEvent = new ManualResetEvent(true);

            this.cmdProcessList = new List<Process>();

            scheduleThread = new Thread(new ThreadStart(() => StartTask()));
            scheduleThread.IsBackground = true;
            scheduleThread.Start();

        }

        public void StartTask()
        {
            parallelTasks = new List<Task>();
            foreach (Triple tmpTri in currentModelTripleList)
            {
                bool isDataElementError = false;
                if (tmpTri.OperateElement.Status == ElementStatus.Done)
                {
                    continue;
                }
                else
                {
                    //判断数据节点是否算完，如果数据节点（上一个的结果算子）为error，跳过这个循环，并将其结果算子也置为error
                    foreach (ModelElement tmpDE in tmpTri.DataElements)
                    {
                        if (tmpDE.Type == ElementType.Result)
                        {
                            while (tmpDE.Status != ElementStatus.Done)
                            {
                                UpdateLogDelegate(tmpTri.TripleName +"的data状态："+ tmpDE.Status);
                                if (tmpDE.Status == ElementStatus.Warn)
                                {
                                    isDataElementError = true;
                                    break;
                                }
                                Thread.Sleep(1000);
                            }
                        }
                        if (isDataElementError) break;
                    }
                    if (isDataElementError)
                    {
                        tmpTri.ResultElement.Status = ElementStatus.Warn;
                        continue;
                    }
                    //该三元组未算过，且数据节点都已经算完，开一个子任务去算
                    Task t = new Task(() => TaskMethod(tmpTri), tokenSource.Token);
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
            if (resetEvent.SafeWaitHandle.IsClosed)
            {
                UpdateLogDelegate(triple.TripleName + "该resetEvent已被释放");
                return true;
            }
            //阻止当前线程
            resetEvent.WaitOne();

            tokenSource.Token.ThrowIfCancellationRequested();

            triple.OperateElement.Status = ElementStatus.Runnnig;
            UpdateLogDelegate(triple.TripleName + "开始运行");
            List<string> cmds = new List<string>();

            int retryCount = 3;
            bool isTaskMethodError = false;
            while (retryCount > 0)
            {
                try
                {
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
                        case ElementSubType.RelateOperator: cmds = (new RelateOperatorCmd(triple)).GenCmd(); break;
                        case ElementSubType.GroupOperator: cmds = (new GroupOperatorCmd(triple)).GenCmd(); break;
                        case ElementSubType.CustomOperator1: cmds = (new CustomOperatorCmd(triple)).GenCmd(); break;
                        case ElementSubType.CustomOperator2: cmds = (new CustomOperatorCmd(triple)).GenCmd(); break;
                        case ElementSubType.PythonOperator: cmds = (new PythonOperatorCmd(triple)).GenCmd(); break;
                    }
                    break;
                }
                catch (System.IO.IOException ex)
                {
                    UpdateLogDelegate("TaskMethod异常: " + ex.Message);
                    Thread.Sleep(1000);
                    retryCount--;
                }
                catch (Exception ex)
                {
                    UpdateLogDelegate("TaskMethod其他异常: " + ex.Message);
                    isTaskMethodError = true;
                    break;
                }
            }

            UpdateLogDelegate("retryCount个数"+retryCount);
            if (retryCount == 0 || isTaskMethodError)
            {
                triple.OperateElement.Status = ElementStatus.Warn;
                triple.ResultElement.Status = ElementStatus.Warn;
                return false;
            }

            RunLinuxCommand(cmds);
            if (resetEvent.SafeWaitHandle.IsClosed)
            {
                UpdateLogDelegate(triple.TripleName + "该resetEvent已被释放");
                return true;
            }
            //阻止当前线程
            resetEvent.WaitOne();

            //在改变状态之前设置暂停，虽然暂停了但是后台还在继续跑
            triple.OperateElement.Status = ElementStatus.Done;
            triple.ResultElement.Status = ElementStatus.Done;
            triple.IsOperated = true;
            UpdateBarDelegate(this);
            UpdateLogDelegate(triple.TripleName + "结束运行");

            return true;
        }

        public void RunLinuxCommand(List<string> cmds)
        {
            // 补充条件检查, cmds 不能为空
            if (cmds == null || !cmds.Any())
                return;

            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            //p.StartInfo.Arguments = "/c " + string.Join(";",cmds);
            p.StartInfo.UseShellExecute = false; // 不显示用户界面
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardInput = true;//可以重定向输入  
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;


            try
            {
                if (p.Start())//开始进程  
                {
                    lock (_objLock)
                    {
                        this.cmdProcessList.Add(p);
                    }

                    

                    foreach (string cmd in cmds)
                    {
                        //UpdateLogDelegate("执行命令: " + cmd);
                        p.StandardInput.WriteLine(cmd);
                    }
                    //此处要exit两次?
                    //退出visual studio 到 cmd.exe
                    //p.StandardInput.WriteLine("exit");
                    //退出cmd.exe
                    p.StandardInput.WriteLine("exit");
                    p.WaitForExit(); //等待进程结束，等待时间为指定的毫秒   
                }
            }
            catch (Exception ex)
            {
                //异常停止的处理方法
                lock (_objLock)
                {
                    this.cmdProcessList.Remove(p);
                }
                
                UpdateLogDelegate("异常: " + ex.Message);

            }
            finally
            {
                if (p != null)
                    p.Close();
                lock (_objLock)
                {
                    this.cmdProcessList.Remove(p);
                }
            }
        }

    }
}