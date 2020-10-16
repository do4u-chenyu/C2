using Citta_T1.Business.Model;
using Citta_T1.Business.Schedule.Cmd;
using Citta_T1.Controls.Move.Op;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

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

    /// <summary>
    /// 后台调度类
    /// </summary> 
    class TaskManager
    {

        public delegate void UpdateLog(string log);//声明一个更新主线程日志的委托
        public UpdateLog UpdateLogDelegate;

        public delegate void AccomplishTask(TaskManager manager);//声明一个在完成任务时通知主线程的委托
        public AccomplishTask TaskCallBack;

        public delegate void UpdateGif(TaskManager manager);//声明一个更新运作动图的委托
        public UpdateGif UpdateGifDelegate;

        public delegate void UpdateBar(TaskManager manager);//声明一个更新进度条的委托
        public UpdateBar UpdateBarDelegate;

        public delegate void UpdateMask(TaskManager manager);//声明一个运行遮罩的委托
        public UpdateMask UpdateMaskDelegate;

        public delegate void UpdateOpError(TaskManager manager, int id, string errorMessage);//声明一个op算子异常时修改提示内容的委托
        public UpdateOpError UpdateOpErrorDelegate;

        private TripleListGen tripleListGen;
        private Thread scheduleThread = null;
        private ModelStatus modelStatus;
        private ModelDocument currentModel;

        private List<Triple> currentModelTripleList = new List<Triple>();

        CancellationTokenSource tokenSource = new CancellationTokenSource();
        ManualResetEvent resetEvent = new ManualResetEvent(true);

        //并行任务集合
        private List<Task> parallelTasks;

        //cmd进程集合
        private List<Process> cmdProcessList;

        public TripleListGen TripleListGen { get => tripleListGen; set => tripleListGen = value; }
        public ModelStatus ModelStatus { get => modelStatus; set => modelStatus = value; }

        public TaskManager()
        {
            //new document的同时初始化后台调度类
            this.modelStatus = ModelStatus.Null;
        }

        public void GetCurrentModelTripleList(ModelDocument currentModel, string state, ModelElement stopElement = null)
        {
            //生成当前模型所有三元组列表
            this.currentModel = currentModel;
            this.tripleListGen = new TripleListGen(currentModel, state, stopElement);
            this.tripleListGen.GenerateList();
            this.currentModelTripleList = this.tripleListGen.CurrentModelTripleList;
        }

        #region 当前模型算子类型统计
        private void ChangeStatus(ElementStatus oldStatus, ElementStatus newStatus)
        {
            foreach (Triple triple in this.currentModelTripleList.FindAll(c => c.OperateElement.Status == oldStatus))
            {
                triple.OperateElement.Status = newStatus;
                UpdateLogDelegate(triple.TripleName + "的状态由" + oldStatus.ToString() + "变更为" + newStatus.ToString());
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

        public int CountOpStatus(ElementStatus es)
        {
            return this.currentModel.ModelElements.FindAll(me => me.Type == ElementType.Operator).Count(op => op.Status == es);
        }

        public int CountOpNullAndNoRelation()
        {
            int nullNoRelationCount = 0;
            foreach (ModelElement me in this.currentModel.ModelElements.FindAll(me => me.Type == ElementType.Operator).FindAll(op => op.Status == ElementStatus.Null))
            {
                if (this.currentModel.ModelRelations.FindAll(mr => mr.EndID == me.ID).Count > 0 || this.currentModel.ModelRelations.FindAll(mr => mr.StartID == me.ID).Count > 0)
                    nullNoRelationCount += 1;
            }
            return nullNoRelationCount;
        }
        #endregion

        #region 暂停、继续、终止、重置运算调度逻辑
        public void Reset()
        {
            //重置，找到所有op状态是 done\stop\warn 的三元组，重置他们的op和rs算子状态，同时将warn算子的错误浮动提示重置成“配置算子”
            foreach (Triple triple in this.currentModelTripleList.FindAll(c => c.OperateElement.Status == ElementStatus.Stop || c.OperateElement.Status == ElementStatus.Done || c.OperateElement.Status == ElementStatus.Warn))
            {
                if (triple.OperateElement.Status == ElementStatus.Warn) (triple.OperateElement.InnerControl as MoveOpControl).SetStatusBoxErrorContent("配置算子");
                triple.OperateElement.Status = ElementStatus.Ready;
                triple.ResultElement.Status = ElementStatus.Null;
            }
        }

        public void Pause()
        {
            //暂停，当前模型状态置pause，op状态由running到suspend，所有task挂起
            this.modelStatus = ModelStatus.Pause;
            ChangeStatus(ElementStatus.Runnnig, ElementStatus.Suspend);
            resetEvent.Reset();
        }

        public void Continue()
        {
            //继续，当前模型状态置running，op状态由suspend到running，所有task继续
            this.modelStatus = ModelStatus.Running;
            ChangeStatus(ElementStatus.Suspend, ElementStatus.Runnnig);
            resetEvent.Set();
        }

        //锁
        private readonly object _objLock = new object();

        /*
         * 现在终止的问题是 只能kill控制台进程，在上面执行的脚本进程不会kill，导致资源无法正常释放
         * 目前解决办法：获取控制台进程，键入ctrl+c，最多等待2秒后kill控制台进程
         */
        [DllImport("kernel32.dll")]
        private static extern bool AttachConsole(uint dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate HandlerRoutine, bool Add);

        enum CtrlTypes : uint
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }

        private delegate bool ConsoleCtrlDelegate(CtrlTypes CtrlType);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GenerateConsoleCtrlEvent(CtrlTypes dwCtrlEvent, uint dwProcessGroupId);

        public void Stop()
        {
            try
            {
                //终止，当前模型状态置stop，op状态由suspend和running到stop，所有task资源释放并关闭，process关闭，后台运算thread关闭
                this.modelStatus = ModelStatus.Stop;
                ChangeStatus(ElementStatus.Suspend, ElementStatus.Stop);
                ChangeStatus(ElementStatus.Runnnig, ElementStatus.Stop);
                resetEvent.Dispose();

                lock (_objLock)
                {
                    // 我们自己的进程需要忽略掉 Ctrl+C 信号，否则自己也会退出。
                    SetConsoleCtrlHandler(null, true);

                    foreach (Process p in cmdProcessList)
                    {
                        if (p != null)
                        {
                            UpdateLogDelegate("关闭进程" + p.Id + "  " + p.ProcessName);

                            if (AttachConsole((uint)p.Id))
                            {
                                // 将 Ctrl+C 信号发送到前面已关联（附加）的控制台进程中。
                                GenerateConsoleCtrlEvent(CtrlTypes.CTRL_C_EVENT, 0);

                                // 拾前面已经附加的控制台。
                                FreeConsole();

                                // 如果没有超时处理，则一直等待，直到最终进程停止。
                                p.WaitForExit(2000);

                            }
                            else
                            {
                                p.Kill();
                            }
                            p.Dispose();
                            p.Close();
                        }
                    }
                    // 重新恢复我们自己的进程对 Ctrl+C 信号的响应。
                    SetConsoleCtrlHandler(null, false);
                    //cmdProcessList.ForEach(p => { p.Kill(); p.Dispose(); p.Close(); });
                }
            }
            catch (Exception ex)
            {
                UpdateLogDelegate("终止异常:" + ex.Message);
            }
            finally
            {
                foreach (Task currentTask in parallelTasks)
                {
                    if (currentTask != null && currentTask.Status == TaskStatus.Running)//终止task线程
                        tokenSource.Cancel();
                }
                CloseThread();
            }
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
        #endregion

        #region 开始运算调度逻辑
        public void Start()
        {
            //开始运算，初始化后台运算线程scheduleThread，线程里开启方法StartTask
            this.modelStatus = ModelStatus.Running;
            this.tokenSource = new CancellationTokenSource();
            this.resetEvent = new ManualResetEvent(true);

            this.cmdProcessList = new List<Process>();

            scheduleThread = new Thread(new ThreadStart(() => StartTask()))
            {
                IsBackground = true
            };
            scheduleThread.Start();
        }

        public void StartTask()
        {
            parallelTasks = new List<Task>();
            foreach (Triple tmpTri in currentModelTripleList)
            {
                //运行前，把所有warn状态的结果算子重置为null状态，把所有warn状态的op算子重置为ready状态
                if (tmpTri.ResultElement.Status == ElementStatus.Warn) tmpTri.ResultElement.Status = ElementStatus.Null;
                if (tmpTri.OperateElement.Status == ElementStatus.Warn)
                {
                    tmpTri.OperateElement.Status = ElementStatus.Ready;
                    UpdateOpErrorDelegate(this, tmpTri.OperateElement.ID, "配置算子");
                }

                bool isDataElementError = false;
                if (tmpTri.OperateElement.Status == ElementStatus.Done)
                    continue;

                //判断数据节点是否算完，如果数据节点（上一个的结果算子）为warn，跳过这个循环，并将其结果算子也置为warn
                foreach (ModelElement tmpDE in tmpTri.DataElements)
                {
                    string filename = tmpDE.FullFilePath;
                    if (!File.Exists(filename))
                    {
                        UpdateOpErrorDelegate(this, tmpTri.OperateElement.ID, "文件\"" + filename + "\"不存在，请确认后重新运行。");
                        tmpTri.OperateElement.Status = ElementStatus.Warn;
                        isDataElementError = true;
                        break;
                    }

                    while (tmpDE.Type == ElementType.Result && tmpDE.Status != ElementStatus.Done)
                    {
                        if (tmpDE.Status == ElementStatus.Warn)
                        {
                            isDataElementError = true;
                            break;
                        }
                        Thread.Sleep(1000);
                    }

                    if (isDataElementError) break;
                }
                if (isDataElementError)
                {
                    //tmpTri.OperateElement.Status = ElementStatus.Warn;
                    tmpTri.ResultElement.Status = ElementStatus.Warn;
                    continue;
                }
                //该三元组未算过，且数据节点都已经算完，开一个子任务去算
                Task t = new Task(() => TaskMethod(tmpTri), tokenSource.Token);
                t.Start();
                parallelTasks.Add(t);

            }

            Task.WaitAll(new Task[] { Task.WhenAll(parallelTasks.ToArray()) });

            this.modelStatus = ModelStatus.GifDone;
            TaskCallBack(this);
            UpdateGifDelegate(this);
            this.modelStatus = ModelStatus.Done;
            UpdateMaskDelegate(this);
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

            int retryCount = 3;//最多重试次数
            string errorMessage = "";
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
                        case ElementSubType.DataFormatOperator: cmds = (new DataFormatOperatorCmd(triple)).GenCmd(); break;
                        case ElementSubType.KeywordOperator: cmds = (new KeywordOperatorCmd(triple)).GenCmd(); break;
                    }
                    break;
                }
                catch (System.IO.IOException ex)
                {
                    errorMessage = ex.Message;
                    UpdateLogDelegate("TaskMethod异常: " + ex.Message);
                    Thread.Sleep(5000);
                    retryCount--;
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    UpdateLogDelegate("TaskMethod其他异常: " + ex.Message);
                    isTaskMethodError = true;
                    break;
                }
            }

            if (retryCount == 0 || isTaskMethodError)
            {
                UpdateOpErrorDelegate(this, triple.OperateElement.ID, errorMessage + "请确认问题后重新运行。");
                triple.OperateElement.Status = ElementStatus.Warn;
                triple.ResultElement.Status = ElementStatus.Warn;
                return false;
            }

            errorMessage = RunLinuxCommand(cmds);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                UpdateOpErrorDelegate(this, triple.OperateElement.ID, errorMessage);
                triple.OperateElement.Status = ElementStatus.Warn;
                triple.ResultElement.Status = ElementStatus.Warn;
                return false;
            }

            if (resetEvent.SafeWaitHandle.IsClosed)
            {
                UpdateLogDelegate(triple.TripleName + "该resetEvent已被释放");
                return true;
            }
            //阻止当前线程
            resetEvent.WaitOne();
            tokenSource.Token.ThrowIfCancellationRequested();
            //在改变状态之前设置暂停，虽然暂停了但是后台还在继续跑
            triple.OperateElement.Status = ElementStatus.Done;
            triple.ResultElement.Status = ElementStatus.Done;
            UpdateBarDelegate(this);
            UpdateLogDelegate(triple.TripleName + "结束运行");

            return true;
        }

        public string RunLinuxCommand(List<string> cmds)
        {
            // 补充条件检查, cmds 不能为空
            if (cmds == null || !cmds.Any())
                return "";
            string errorMessage = String.Empty;

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
                        UpdateLogDelegate("执行命令: " + cmd);
                        p.StandardInput.WriteLine(cmd);
                    }

                    //多线程下异步读取
                    //p.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);

                    p.BeginErrorReadLine();
                    p.BeginOutputReadLine();

                    p.StandardInput.WriteLine("exit");
                    p.WaitForExit(); //等待进程结束，等待时间为指定的毫秒

                    UpdateLogDelegate("退出码" + p.ExitCode.ToString());
                    if (p.ExitCode != 0)
                    {
                        errorMessage = "执行程序非正常退出，请检查程序后再运行。";
                        UpdateLogDelegate("执行程序非正常退出，请检查程序后再运行。");
                    }

                }
            }
            catch (System.InvalidOperationException)
            {
                //没有关联进程的异常，是由于用户点击终止按钮，导致进程被关闭
                //UpdateLogDelegate("InvalidOperationException: " + ex.Message);
            }
            catch (Exception ex)
            {
                //异常停止的处理方法
                errorMessage = ex.Message;
                UpdateLogDelegate("RunLinuxCommand进程异常: " + ex.Message);
            }
            finally
            {
                if (p != null)
                    p.Dispose();//释放资源
                p.Close();
                lock (_objLock)
                {
                    this.cmdProcessList.Remove(p);
                }
            }
            return errorMessage;
        }

        void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                UpdateLogDelegate(e.Data);
            }
        }
        #endregion
    }
}