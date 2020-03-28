using Citta_T1.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Citta_T1.Business.Schedule
{
    class Manager
    {
        public delegate void UpdateUI(int step);//声明一个更新主线程的委托
        public UpdateUI UpdateUIDelegate;

        public delegate void AccomplishTask();//声明一个在完成任务时通知主线程的委托
        public AccomplishTask TaskCallBack;



        private List<Triple> currentModelTripleList = new List<Triple>();
        private int maxAllowCount = 3;

        CancellationTokenSource tokenSource = new CancellationTokenSource();
        ManualResetEvent resetEvent = new ManualResetEvent(true);

        //并行任务集合
        private List<Task> parallelTasks;

        public List<Triple> CurrentModelTripleList { get => currentModelTripleList; set => currentModelTripleList = value; }
        public int MaxAllowCount { get => maxAllowCount; set => maxAllowCount = value; }

        public List<Task> ParallelTasks { get => parallelTasks; set => parallelTasks = value; }

        public Manager(int maxAllowCount, List<Triple> currentModelTripleList, List<ModelElement> currentModelElements)
        {
            this.maxAllowCount = maxAllowCount;
            this.currentModelTripleList = currentModelTripleList;

            tokenSource = new CancellationTokenSource();
            resetEvent = new ManualResetEvent(true);
        }

        public void ChangeStatus(ElementStatus oldStatus, ElementStatus newStatus)
        {
            foreach (Triple pauseTri in this.currentModelTripleList.FindAll(c => c.OperateElement.Status == oldStatus))
            {
                pauseTri.OperateElement.Status = newStatus;
                Console.WriteLine("{0}的状态改为{1}", pauseTri.OperateElement.RemarkName, pauseTri.OperateElement.Status);
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
                if (tmpTri.ResultElement.Status == ElementStatus.Done)
                {
                    Console.WriteLine("该三元组已算过，下一个");
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
            Console.WriteLine("所有任务都已完成");

            TaskCallBack();
        }





        bool TaskMethod(Triple triple)
        {
            try
            {
                tokenSource.Token.ThrowIfCancellationRequested();

                //开始task先更新几个状态
                //op控件 running
                //TODO
                triple.OperateElement.Status = ElementStatus.Runnnig;

                string dataName = "";
                foreach (ModelElement d in triple.DataElements)
                {
                    dataName = dataName + d.RemarkName;
                }

                Console.WriteLine("{0}-{1}-{2}开始运行，状态为{3}", dataName, triple.OperateElement.RemarkName, triple.ResultElement.RemarkName, triple.ResultElement.Status);
                Thread.Sleep(5000);
                //此处写处理数据方法


                resetEvent.WaitOne();
                //在改变状态之前设置暂停，虽然暂停了但是后台还在继续跑
                triple.OperateElement.Status = ElementStatus.Done;
                triple.ResultElement.Status = ElementStatus.Done;
                triple.IsOperated = true;
                Console.WriteLine("{0}-{1}-{2}结束运行，状态为{3}", dataName, triple.OperateElement.RemarkName, triple.ResultElement.RemarkName, triple.ResultElement.Status);
                UpdateUIDelegate(triple.OperateElement.ID);
            }
            catch (Exception ex)
            {
                Console.WriteLine("线程被取消");
            }

            return triple.IsOperated;

        }

    }
}
