using C2.Business.Schedule.Cmd;
using C2.Core;
using C2.Database;
using C2.Dialogs;
using C2.Dialogs.Base;
using C2.Dialogs.C2OperatorViews;
using C2.Model;
using C2.Model.MindMaps;
using C2.Model.Widgets;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace C2.Controls.MapViews
{
    public partial class MindMapView
    {
        private static LogUtil log = LogUtil.GetInstance("MindMapView");
        public delegate void DelReadStdOutput(string result);
        public delegate void DelReadErrOutput(string result);
        // 定义委托事件
        public event DelReadStdOutput ReadStdOutput;
        public event DelReadErrOutput ReadErrOutput;
        bool flag = false;
        string message = string.Empty;
        private void GenRunCmds()
        {
            //除了未配置状态，其余情况下全部重新运行
            if (opw.Status == OpStatus.Null)
                return;

            //运行前，先判断要运算的是否有python算子，且虚拟机配置了，若未配置，结束，同时弹窗
            if (opw.OpType == OpType.PythonOperator && OpUtil.GetPythonConfigPaths().Count == 0)
            {
                HelpUtil.ShowMessageBox(HelpUtil.InvalidPythonENV2);
                new ConfigForm().ShowDialog();
                return;
            }

            //Global.GetDocumentForm().ShowRunLab();
            List<string> cmds = GenerateCmd();
            if (cmds == null)
                return;

            string runMessage = string.Empty;
            using (GuarderUtil.WaitCursor)
                runMessage = RunLinuxCommand(cmds);
            opw.Status = runMessage == "算子成功执行完毕" ? OpStatus.Done : OpStatus.Warn;
            //HelpUtil.ShowMessageBox(runMessage, "运行结束"); // 这个对话框还是挺丑的.后面要优化

            // 没能成功运行, 自动蹦出日志面板
            if (opw.Status == OpStatus.Warn || opw.Status == OpStatus.Done)
                Global.GetMainForm()?.ShowLogViewG();
           
            // 没能成功运行，直接返回
            if (opw.Status != OpStatus.Done)
                return;

            //Global.GetDocumentForm().HideRunLab();
            DataItem resultItem = opw.ResultItem;
            Topic topic = opw.Container as Topic;
            ResultWidget rsw = topic.FindWidget<ResultWidget>();
            if (rsw == null)
            {
                topic.Widgets.Add(new ResultWidget { DataItems = new List<DataItem> { resultItem } });
                //Global.GetCurrentDocument().Modified = false; //新建了一个挂件，此时文档dirty，需要置false
            }
            else
            {
                rsw.DataItems.RemoveAll(di => di.ResultDataType == DataItem.ResultType.SingleOp);
                rsw.DataItems.Add(resultItem);                   
            }
            TopicUpdate(topic, null);
        }
        private void RunSQL()
        {
            //除了未配置状态，其余情况下全部重新运行
            if (opw.Status != OpStatus.Null)
            {
                string connString, sqlText, maxNumString;
                int inputMaxNum;
                int maxNum = int.MaxValue;
                if (!opw.Option.OptionDict.TryGetValue("sqlText", out sqlText) || !opw.Option.OptionDict.TryGetValue("connection", out connString))
                    return;
                if (opw.Option.OptionDict.TryGetValue("maxNum", out maxNumString) && int.TryParse(maxNumString, out inputMaxNum) && inputMaxNum > 0)
                    maxNum = inputMaxNum;
                IDAO dao = DAOFactory.CreateDAO(new DatabaseItem(connString));
                if (!dao.TestConn())
                {
                    HelpUtil.ShowMessageBox(HelpUtil.DbCannotBeConnectedInfo);
                    return;
                }
                bool isSuccess = dao.ExecuteSQL(sqlText, opw.ResultItem.FilePath, maxNum);
                string runMessage = isSuccess ? HelpUtil.SQLOpExecuteSucceeded : HelpUtil.SQLOpExecuteFailed;
                opw.Status = isSuccess ? OpStatus.Done : OpStatus.Warn;
                HelpUtil.ShowMessageBox(runMessage, "运行"); // 这个对话框还是挺丑的.后面要优化
                this.Cursor = Cursors.Default;

                //没能成功运行，直接返回
                if (opw.Status != OpStatus.Done)
                    return;

                DataItem resultItem = opw.ResultItem;
                Topic topic = opw.Container as Topic;
                ResultWidget rsw = topic.FindWidget<ResultWidget>();
                if (rsw == null)
                {
                    topic.Widgets.Add(new ResultWidget { DataItems = new List<DataItem> { resultItem } });
                }
                else
                {
                    rsw.DataItems.RemoveAll(di => di.ResultDataType == DataItem.ResultType.SingleOp);
                    rsw.DataItems.Add(resultItem);
                }
                TopicUpdate(topic, null);
            }
        }


        private C2BaseOperatorView GenerateOperatorView()
        {
            //当数据源为数据库表时，不管选择什么算子，都打开sql自定义算子
            //TODO 后期加入hive看是否复用同一个配置窗口
            if (opw.DataSourceItem.DataType == DatabaseType.Oracle)
                return new C2SqlOperatorView(opw);

            switch (opw.OpType)
            {
                case OpType.MaxOperator: return new C2MaxOperatorView(opw);
                case OpType.CustomOperator: return new C2CustomOperatorView(opw);
                case OpType.MinOperator: return new C2MinOperatorView(opw);
                case OpType.AvgOperator: return new C2AvgOperatorView(opw);
                case OpType.DataFormatOperator: return new C2DataFormatOperatorView(opw);
                case OpType.RandomOperator: return new C2RandomOperatorView(opw);
                case OpType.FreqOperator: return new C2FreqOperatorView(opw);
                case OpType.SortOperator: return new C2SortOperatorView(opw);
                case OpType.FilterOperator: return new C2FilterOperatorView(opw);
                case OpType.GroupOperator: return new C2GroupOperatorView(opw);
                case OpType.PythonOperator: return new C2PythonOperatorView(opw);
                case OpType.SqlOperator: return new C2SqlOperatorView(opw);
                default: return null;
            }
        }

        private List<string> GenerateCmd()
        {
            switch (opw.OpType)
            {
                case OpType.MaxOperator: return (new MaxOperatorCmd(opw)).GenCmd();
                case OpType.CustomOperator: return (new CustomOperatorCmd(opw)).GenCmd();
                case OpType.MinOperator: return (new MinOperatorCmd(opw)).GenCmd();
                case OpType.RandomOperator: return (new RandomOperatorCmd(opw)).GenCmd();
                case OpType.FilterOperator: return (new FilterOperatorCmd(opw)).GenCmd();
                case OpType.AvgOperator: return (new AvgOperatorCmd(opw)).GenCmd();
                case OpType.SortOperator: return (new SortOperatorCmd(opw)).GenCmd();
                case OpType.FreqOperator: return (new FreqOperatorCmd(opw)).GenCmd();
                case OpType.GroupOperator: return (new GroupOperatorCmd(opw)).GenCmd();
                case OpType.DataFormatOperator: return (new DataFormatOperatorCmd(opw)).GenCmd();
                case OpType.PythonOperator: return (new PythonOperatorCmd(opw)).GenCmd();
                default: return null;
            }
        }

        private void ReadStdOutputAction(string result)
        {
            log.Info(result);
            
            if (flag == false)
            {
                message = string.Format("运算进行中, 【运行日志】面板查看具体信息");
                HelpUtil.ShowMessageBox(message, "运行中");
                flag = true;
            }
        }

        private void ReadErrOutputAction(string result)
        {
            log.Warn(result);
            if (flag == false)
            {
                message = string.Format("运算出现问题, 【运行日志】面板查看出错信息,反馈SH群");
                HelpUtil.ShowMessageBox(message, "运行结束");
                flag = true;
            }
        }

        private void p_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                // 异步调用，需要invoke
                this.Invoke(ReadStdOutput, new object[] { e.Data });
                Console.WriteLine(e.Data);
            }
        }

        private void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                this.Invoke(ReadErrOutput, new object[] { e.Data });
                Console.WriteLine(e.Data);
            }
        }

        private void Process_Exited(object sender, EventArgs e)
        { Console.WriteLine("命令执行完毕"); }

        

        public string RunLinuxCommand(List<string> cmds)
        {
            ReadStdOutput += new DelReadStdOutput(ReadStdOutputAction);
            ReadErrOutput += new DelReadErrOutput(ReadErrOutputAction);

            // 补充条件检查, cmds 不能为空
            if (cmds == null || !cmds.Any())
                return "执行命令为空";
            string message = String.Empty;

            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false; // 不显示用户界面
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardInput = true;//可以重定向输入  
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;

            p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            p.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);

            p.EnableRaisingEvents = true;
            p.Exited += new EventHandler(Process_Exited);

            try
            {
                if (p.Start())//开始进程  
                {
                    log.Info("===========运算开始==========");
                    p.BeginOutputReadLine();
                    p.BeginErrorReadLine();  // 让后续可以读取到错误流
                    p.StandardInput.AutoFlush = true;
                    string cmd = string.Empty;
                    int count = cmds.Count;

                    for (int i = 0; i < cmds.Count; i++)
                    {
                        cmd = cmds[i] + string.Empty;
                    }
                    log.Info(cmd);
                    p.StandardInput.WriteLine(cmd);
                    //等待进程结束，等待时间为指定的毫秒
                    p.StandardInput.WriteLine("exit");
                }
            }
            catch (InvalidOperationException)
            {
                //没有关联进程的异常，是由于用户点击终止按钮，导致进程被关闭
                //UpdateLogDelegate("InvalidOperationException: " + ex.Message);
            }
            
            catch (Exception ex)
            {
                //异常停止的处理方法
                message = ex.Message;
            }
            finally
            {
                if (p != null)
                    p.Dispose();//释放资源
                p.Close();
            }
            return message;
        }
    }
}
