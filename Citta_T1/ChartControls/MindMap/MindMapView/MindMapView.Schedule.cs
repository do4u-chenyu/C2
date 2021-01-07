using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C2.Business.Schedule.Cmd;
using C2.Core;
using C2.Database;
using C2.Dialogs.Base;
using C2.Dialogs.C2OperatorViews;
using C2.Model;
using C2.Model.MindMaps;
using C2.Model.Widgets;
using C2.Utils;

namespace C2.Controls.MapViews
{
    public partial class MindMapView
    {
        private static LogUtil log = LogUtil.GetInstance("MindMapView");
        private void GenRunCmds()
        {
            //除了未配置状态，其余情况下全部重新运行
            if (opw.Status != OpStatus.Null)
            {
                //Global.GetDocumentForm().ShowRunLab();
                List<string> cmds = GenerateCmd();
                if (cmds == null)
                    return;
                this.Cursor = Cursors.WaitCursor;
                string runMessage = RunLinuxCommand(cmds);
                opw.Status = runMessage == "算子成功执行完毕" ? OpStatus.Done : OpStatus.Warn;
                HelpUtil.ShowMessageBox(runMessage, "运行"); // 这个对话框还是挺丑的.后面要优化
                this.Cursor = Cursors.Default;

                //没能成功运行，直接返回
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
        }
        private void RunSQL()
        {
            //除了未配置状态，其余情况下全部重新运行
            if (opw.Status != OpStatus.Null)
            {
                string connString;
                string sqlText;
                if (!opw.Option.OptionDict.TryGetValue("sqlText", out sqlText) || !opw.Option.OptionDict.TryGetValue("connection", out connString))
                    return;
                OraConnection conn = new OraConnection(new DatabaseItem(connString));
                DbUtil.ExecuteOracleSQL(conn, sqlText, opw.ResultItem.FilePath);
                opw.Status = OpStatus.Done;
                HelpUtil.ShowMessageBox("算子运算完毕", "运行"); // 这个对话框还是挺丑的.后面要优化
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


        public string RunLinuxCommand(List<string> cmds)
        {
            // 补充条件检查, cmds 不能为空
            if (cmds == null || !cmds.Any())
                return "执行命令为空";
            string message = String.Empty;

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
                    log.Info("=====运算开始=====");
                    foreach (string cmd in cmds)
                    {
                        log.Info(cmd);
                        p.StandardInput.WriteLine(cmd);
                    }
                    log.Info("=====运算结束=====");
                    p.BeginErrorReadLine();
                    p.BeginOutputReadLine();

                    p.StandardInput.WriteLine("exit");
                    p.WaitForExit(); //等待进程结束，等待时间为指定的毫秒

                    message = "算子成功执行完毕";

                    if (p.ExitCode != 0)
                    {
                        message = "执行程序非正常退出，请检查程序后再运行。";
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
