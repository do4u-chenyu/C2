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

namespace Citta_T1.Business.Schedule
{
    class Manager
    {
        public delegate void UpdateLog(string log);//声明一个更新主线程日志的委托
        public UpdateLog UpdateLogDelegate;

        public delegate void UpdateUI(int id);//声明一个更新主线程的委托
        public UpdateUI UpdateUIDelegate;

        public delegate void AccomplishTask();//声明一个在完成任务时通知主线程的委托
        public AccomplishTask TaskCallBack;



        private List<Triple> currentModelTripleList = new List<Triple>();
        private int maxAllowCount = 3;

        CancellationTokenSource tokenSource = new CancellationTokenSource();
        ManualResetEvent resetEvent = new ManualResetEvent(true);

        //并行任务集合
        private List<Task> parallelTasks;

        public delegate string FuncDelegate(Triple triple);
        Dictionary<ElementSubType, FuncDelegate> operatorFuncDict = new Dictionary<ElementSubType, FuncDelegate>();

        public List<Triple> CurrentModelTripleList { get => currentModelTripleList; set => currentModelTripleList = value; }
        public int MaxAllowCount { get => maxAllowCount; set => maxAllowCount = value; }

        public List<Task> ParallelTasks { get => parallelTasks; set => parallelTasks = value; }

        public Manager(int maxAllowCount, List<Triple> currentModelTripleList, List<ModelElement> currentModelElements)
        {
            this.maxAllowCount = maxAllowCount;
            this.currentModelTripleList = currentModelTripleList;

            tokenSource = new CancellationTokenSource();
            resetEvent = new ManualResetEvent(true);
            Adddic();
        }

        private void Adddic()
        {
            operatorFuncDict.Add(ElementSubType.MaxOperator, MaxUnit);
            operatorFuncDict.Add(ElementSubType.FilterOperator, FilterUnit);
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
            ChangeStatus(ElementStatus.Runnnig, ElementStatus.Suspend);
            
            resetEvent.Reset();
        }

        public void Continue()
        {
            ChangeStatus(ElementStatus.Suspend, ElementStatus.Runnnig);
            resetEvent.Set();
        }

        public void Stop()
        {
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
            //thread.Abort();
        }

        public void Start()
        {
            parallelTasks = new List<Task>();

            foreach (Triple tmpTri in currentModelTripleList)
            {
                /*
                 * 1、判断tmpTri中的isOperated，是否算过
                 *    1.1 算过continue；
                 *    1.2 未算 → 2；
                 * 2、判断tmpTri中的DataElements中的ModelElement的ElementType是否存在Result
                 *    2.1 不存在，开运算线程，continue；
                 *    2.2 存在，循环
                 *        2.2.1 判断数据节点的类型，都为数据源直接算，如果有“result”需要判断状态
                 */
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

            TaskCallBack();
        }


        


        bool TaskMethod(Triple triple)
        {
            try
            {
                tokenSource.Token.ThrowIfCancellationRequested();

                //开始task先更新几个状态
                //op控件 running
                triple.OperateElement.Status = ElementStatus.Runnnig;
                UpdateLogDelegate(triple.TripleName + "开始运行");

                if (operatorFuncDict.ContainsKey(triple.OperateElement.SubType))
                {
                    RunLinuxCommand(FuncCmd(triple,operatorFuncDict[triple.OperateElement.SubType]));
                }
                resetEvent.WaitOne();
                //在改变状态之前设置暂停，虽然暂停了但是后台还在继续跑
                triple.OperateElement.Status = ElementStatus.Done;
                triple.ResultElement.Status = ElementStatus.Done;
                triple.IsOperated = true;
                UpdateLogDelegate(triple.TripleName + "结束运行");
                UpdateUIDelegate(triple.OperateElement.ID);
            }
            catch (Exception ex)
            {
            }
            return triple.IsOperated;

        }


        public string FuncCmd(Triple triple, FuncDelegate OperatorUnit)
        {
            return OperatorUnit(triple);
        }

        public string MaxUnit(Triple triple)
        {
            string filePath = triple.DataElements.First().GetPath();
            OperatorOption option = (triple.OperateElement.GetControl as MoveOpControl).Option;
            string maxfield = option.GetOption("maxfield");
            string[] outfield = option.GetOption("outfield").Split(',');
            string outfieldCmd = "$"+(int.Parse(outfield[0]) + 1).ToString();
            for(int i = 1; i < outfield.Length; i++)
            {
                outfieldCmd = outfieldCmd + "\"\\t\"$" + (int.Parse(outfield[i]) + 1).ToString();
            }
            string cmd = "sbin\\sort.exe -k " + (int.Parse(maxfield) + 1).ToString() + " " + filePath + " | sbin\\head.exe -n1 | sbin\\awk.exe -F'\\t' '{print " + outfieldCmd + "}'> 1.txt";
            return cmd;
        }

        public string FilterUnit(Triple triple)
        {
            
            return "";
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
