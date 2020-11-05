using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Linq;
using C2.Configuration;
using C2.Core;
using C2.Model;
using C2.Model.MindMaps;
using C2.Model.Styles;
using C2.Model.Widgets;
using System.Collections.Generic;
using C2.Globalization;
using C2.Business.Schedule.Cmd;
using System.Diagnostics;
using C2.Dialogs.C2OperatorViews;
using System.IO;
using C2.Dialogs;
using C2.Business.Option;

namespace C2.Controls.MapViews
{
    public partial class MindMapView 
    {
        public ContextMenuStrip WidgetMenuStrip = new ContextMenuStrip();
        
        private DataSourceWidget dtw;
        private OperatorWidget opw;
        private ResultWidget rsw;
        private ChartWidget cw;
        private Topic currentTopic;

        public void CreateWidgetMenu()
        {
            WidgetMenuStrip.SuspendLayout();
            WidgetMenuStrip.Items.Clear();

            switch (HoverObject.Widget.GetTypeID())
            {
                case OperatorWidget.TypeID:
                    opw = HoverObject.Widget as OperatorWidget;
                    if (opw.Status == OpStatus.Null)
                        CreateInitOperatorMenu(opw);
                    else
                        CreateOperatorMenu(opw);                        
                    break;
                case DataSourceWidget.TypeID:
                    dtw = HoverObject.Widget as DataSourceWidget;
                    currentTopic = HoverObject.Topic;
                    CreateDataSourceMenu(dtw);
                    break;
                case ResultWidget.TypeID:
                    rsw = HoverObject.Widget as ResultWidget;
                    CreateResultMenu(rsw);
                    break;
                case ChartWidget.TypeID:
                    cw = HoverObject.Widget as ChartWidget;
                    CreateChartWidgetMenu(cw);
                    break;
                default:
                    break;
            }
            WidgetMenuStrip.ResumeLayout();
            if (UITheme.Default != null)
            {
                WidgetMenuStrip.Renderer = UITheme.Default.ToolStripRenderer;
            }
        }

        #region 图表挂件
        private void CreateChartWidgetMenu(ChartWidget cw)
        { }
        #endregion

        #region 算子挂件
        private void CreateInitOperatorMenu(OperatorWidget opw)
        {
            ToolStripMenuItem MenuOpenOperator = new ToolStripMenuItem();
            MenuOpenOperator.Text = Lang._("OpenDesigner");
            MenuOpenOperator.Image = Properties.Resources.operator_w_icon;

            WidgetMenuStrip.Items.Add(MenuOpenOperator);
        }
        private void CreateOperatorMenu(OperatorWidget opw)
        {
            ToolStripMenuItem MenuOpenOperator = new ToolStripMenuItem();
            ToolStripMenuItem MenuOpDesign = new ToolStripMenuItem();
            ToolStripMenuItem MenuOpRunning = new ToolStripMenuItem();
            ToolStripMenuItem MenuOpPublic = new ToolStripMenuItem();
            ToolStripMenuItem MenuOpDelete = new ToolStripMenuItem();

            MenuOpenOperator.Image = GetOpOpenOperatorImage(opw.Status);
            MenuOpenOperator.Text = opw.OpName;
            MenuOpenOperator.DropDownItems.AddRange(new ToolStripItem[] {
                MenuOpDesign,
                MenuOpRunning,
                MenuOpPublic,
                MenuOpDelete});

            MenuOpDesign.Image = Properties.Resources.opDesign;
            MenuOpDesign.Text = Lang._("Design");
            MenuOpDesign.Enabled = opw.Status != OpStatus.Done;
            MenuOpDesign.Click += new System.EventHandler(MenuDesignOp_Click);

            MenuOpRunning.Image = Properties.Resources.opRunning;
            MenuOpRunning.Text = Lang._("Running") ;
            MenuOpRunning.Enabled = opw.Status != OpStatus.Null ;
            MenuOpRunning.Click += new System.EventHandler(MenuRunningOp_Click);

            MenuOpPublic.Image = Properties.Resources.opModelPublic;
            MenuOpPublic.Text = Lang._("Public");
            MenuOpPublic.Enabled = opw.OpType == Lang._("Model");

            MenuOpDelete.Image = Properties.Resources.deletewidget;
            MenuOpDelete.Text = Lang._("Delete");
            MenuOpDelete.Click += new System.EventHandler(MenuDeleteOp_Click);

            WidgetMenuStrip.Items.Add(MenuOpenOperator);
        }

        private Image GetOpOpenOperatorImage(OpStatus opStatus)
        {
            switch (opStatus)
            {
                case OpStatus.Null:
                    return Properties.Resources.set;
                case OpStatus.Ready:
                    return Properties.Resources.setSuccess;
                case OpStatus.Done:
                    return Properties.Resources.done;
                case OpStatus.Warn:
                    return Properties.Resources.warn;
                default:
                    return Properties.Resources.operator_w_icon;
            }
        }

        void MenuDesignOp_Click(object sender, EventArgs e)
        {
            switch (opw.OpType)
            {
                case "最大值":
                    new C2MaxOperatorView(opw).ShowDialog();
                    break;
                case "AI实践":
                    new C2CustomOperatorView(opw).ShowDialog();
                    break;
                default:
                    break;
            }
        }
        void MenuRunningOp_Click(object sender, EventArgs e)
        {
            if(opw.Status == OpStatus.Ready)
            {
                List<string> cmds = (new MaxOperatorCmd(opw)).GenCmd();
                MessageBox.Show(RunLinuxCommand(cmds));
                DataItem resultItem = opw.ResultItem;
                ResultWidget rsw = (opw.Container as Topic).FindWidget<ResultWidget>();
                if (rsw == null)
                    AddResult(new Topic[] { opw.Container as Topic },resultItem);
                else
                {
                    rsw.DataItems.Clear();
                    rsw.DataItems.Add(resultItem);
                }
                opw.Status = OpStatus.Done;
            }
        }

        public string RunLinuxCommand(List<string> cmds)
        {
            // 补充条件检查, cmds 不能为空
            if (cmds == null || !cmds.Any())
                return "";
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
                    foreach (string cmd in cmds)
                    {
                        p.StandardInput.WriteLine(cmd);
                    }

                    p.BeginErrorReadLine();
                    p.BeginOutputReadLine();

                    p.StandardInput.WriteLine("exit");
                    p.WaitForExit(); //等待进程结束，等待时间为指定的毫秒

                    message = "成功执行完毕";

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

        void MenuDeleteOp_Click(object sender, EventArgs e)
        {
            ResultWidget rs = (opw.Container as Topic).FindWidget<ResultWidget>();
            if(rs == null)
                Delete(new ChartObject[] { opw });
            else
                Delete(new ChartObject[] { opw,rs });
        }
        #endregion

        #region 数据挂件
        private void CreateDataSourceMenu(DataSourceWidget dtw)
        {
            foreach (DataItem dataItem in dtw.DataItems)
            {
                ToolStripMenuItem MenuViewData = new ToolStripMenuItem();
                ToolStripMenuItem MenuGetChart = new ToolStripMenuItem();
                ToolStripMenuItem MenuDelete = new ToolStripMenuItem();
                ToolStripMenuItem MenuOpenDataSource = new ToolStripMenuItem();
                MenuOpenDataSource.Image = Properties.Resources.data_w_icon;

                MenuOpenDataSource.Text = String.Format("{0}{1}{2}{3}", dataItem.FileName, " [", Path.GetExtension(dataItem.FilePath).Trim('.'), "]"); 
                MenuOpenDataSource.DropDownItems.AddRange(new ToolStripItem[] {
                MenuViewData,
                MenuGetChart,
                MenuDelete});
            
                MenuViewData.Image = Properties.Resources.viewdata;
                MenuViewData.Tag = dataItem;
                MenuViewData.Text = Lang._("ViewData");
                MenuViewData.Click += MenuViewData_Click;

                MenuGetChart.Image = Properties.Resources.getchart;              
                MenuGetChart.Text = Lang._("GetChart");
                MenuGetChart.Tag = dataItem;
                MenuGetChart.Click += MenuGetChart_Click;

                MenuDelete.Image = Properties.Resources.deletewidget;
                MenuDelete.Text = Lang._("Delete");
                MenuDelete.Tag = dataItem;
                MenuDelete.Click += MenuDelete_Click;

                WidgetMenuStrip.Items.Add(MenuOpenDataSource);           
            }
        }
        void MenuDelete_Click(object sender, EventArgs e)
        {
            DataItem hitItem = (sender as ToolStripMenuItem).Tag as DataItem;
            // 剩余最后一个菜单项，删除数据源挂件
            dtw.DataItems.Remove(hitItem);
            ShowDesigner(dtw.Container);
            if (dtw.DataItems.IsEmpty())
                Delete(new ChartObject[] { dtw });
        }

        void DSWidgetMenuDelete_Click(object sender, EventArgs e)
        {
            Delete(new ChartObject[] { opw });
        }

        void MenuGetChart_Click(object sender, EventArgs e)
        {
            DataItem hitItem = (sender as ToolStripMenuItem).Tag as DataItem;
            VisualDisplayDialog displayDialog = new VisualDisplayDialog(hitItem);
            if (DialogResult.OK != displayDialog.ShowDialog())
                return;
            ChartWidget cw = currentTopic.FindWidget<ChartWidget>();
            // 生成图表挂件
            if (cw == null)
            {
                Topic[] hitTopic = new Topic[] { currentTopic };
                AddChartWidget(hitTopic);
            }
            currentTopic.FindWidget<ChartWidget>().DataItems.Add(hitItem);
        }
        #endregion

        #region 结果挂件
        private void CreateResultMenu(ResultWidget rsw)
        {
            

            //ToolStripMenuItem MenuOpenResult = new ToolStripMenuItem();
            //MenuOpenResult.Text = "Result";

            //WidgetMenuStrip.Items.Add(MenuOpenResult);

            //改
            foreach (DataItem dataItem in rsw.DataItems)
            {
                ToolStripMenuItem MenuViewData = new ToolStripMenuItem();
                ToolStripMenuItem MenuGetChart = new ToolStripMenuItem();
                ToolStripMenuItem MenuDelete = new ToolStripMenuItem();
                ToolStripMenuItem MenuDealData = new ToolStripMenuItem();
                ToolStripMenuItem MenuJoinPool = new ToolStripMenuItem();

                ToolStripMenuItem MenuOpenResult = new ToolStripMenuItem();
                MenuOpenResult.Image = Properties.Resources.result_w_icon;

                MenuOpenResult.Text = String.Format("{0}{1}{2}{3}", dataItem.FileName, " [", Path.GetExtension(dataItem.FilePath).Trim('.'), "]");
                MenuOpenResult.DropDownItems.AddRange(new ToolStripItem[] {
                MenuViewData,
                MenuDealData,
                MenuJoinPool});

                MenuViewData.Image = Properties.Resources.viewdata;
                MenuViewData.Tag = dataItem;
                MenuViewData.Text = Lang._("ViewData");
                MenuViewData.Click += MenuViewData_Click;

                MenuDealData.Image = Properties.Resources.dealData;
                MenuDealData.Text = Lang._("DealData");
                MenuDealData.Click += MenuDealData_Click;

                MenuJoinPool.Image = Properties.Resources.joinPool;
                MenuJoinPool.Text = Lang._("JoinPool");
                MenuJoinPool.Tag = dataItem;
                MenuJoinPool.Click += MenuJoinPool_Click;

                WidgetMenuStrip.Items.Add(MenuOpenResult);
            }
        }

        void MenuViewData_Click(object sender, EventArgs e)
        {

            DataItem hitItem = (sender as ToolStripMenuItem).Tag as DataItem;
            if (hitItem != null)
                Global.GetMainForm().PreViewDataByFullFilePath(hitItem);
        }
        void MenuDealData_Click(object sender, EventArgs e)
        {
            AddSubTopic(rsw.Container as Topic, null, false);
        }
        void MenuJoinPool_Click(object sender, EventArgs e)
        {
        }
        #endregion
    }
}
