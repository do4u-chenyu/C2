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
using C2.Utils;
using C2.Dialogs.Base;

namespace C2.Controls.MapViews
{
    public partial class MindMapView 
    {
        public ContextMenuStrip WidgetMenuStrip = new ContextMenuStrip();
        
        private DataSourceWidget dtw;
        private OperatorWidget opw;
        private AttachmentWidget atw;
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
                    if (opw.OpType == OpType.Null)
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
                case AttachmentWidget.TypeID:
                    atw = HoverObject.Widget as AttachmentWidget;
                    CreateAttachmentWidgetMenu(atw);
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
        {
            WidgetMenuStrip.SuspendLayout();
            foreach (DataItem dataItem in cw.DataItems)
            {

                ToolStripMenuItem MenuGetChart = new ToolStripMenuItem();
                ToolStripMenuItem MenuDelete = new ToolStripMenuItem();
                ToolStripMenuItem MenuViewChart = new ToolStripMenuItem();
                MenuViewChart.Image = Properties.Resources.chart_w_icon;

                MenuViewChart.Text = String.Format("{0}{1}{2}{3}", dataItem.FileName, " [", dataItem.ChartType, "]");
                MenuViewChart.DropDownItems.AddRange(new ToolStripItem[] {
                MenuGetChart,
                MenuDelete});

                MenuGetChart.Image = Properties.Resources.getchart;
                MenuGetChart.Text = Lang._("ViewChart");
                MenuGetChart.Tag = dataItem;
                MenuGetChart.Click += MenuViewChart_Click;

                MenuDelete.Image = Properties.Resources.deletewidget;
                MenuDelete.Text = Lang._("Delete");
                MenuDelete.Tag = dataItem;
                MenuDelete.Click += ChartMenuDelete_Click;

                WidgetMenuStrip.Items.Add(MenuViewChart);
            }
            WidgetMenuStrip.ResumeLayout();
        }

        #endregion

        #region 算子挂件
        private void CreateInitOperatorMenu(OperatorWidget opw)
        {
            ToolStripMenuItem MenuOpenOperator = new ToolStripMenuItem();
            MenuOpenOperator.Text = Lang._("OpenDesigner");
            MenuOpenOperator.Image = Properties.Resources.operator_w_icon;
            MenuOpenOperator.Click += MenuOpenOperatorDesigner_Click;

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
            //MenuOpDesign.Enabled = opw.Status != OpStatus.Done;
            MenuOpDesign.Click += MenuDesignOp_Click;

            MenuOpRunning.Image = Properties.Resources.opRunning;
            MenuOpRunning.Text = Lang._("Running") ;
            MenuOpRunning.Enabled = opw.Status != OpStatus.Null ;
            MenuOpRunning.Click += MenuRunningOp_Click;

            MenuOpPublic.Image = Properties.Resources.opModelPublic;
            MenuOpPublic.Text = Lang._("Public");
            MenuOpPublic.Enabled = opw.OpType == OpType.ModelOperator;

            MenuOpDelete.Image = Properties.Resources.deletewidget;
            MenuOpDelete.Text = Lang._("Delete");
            MenuOpDelete.Click += MenuDeleteOp_Click;

            WidgetMenuStrip.Items.Add(MenuOpenOperator);
        }

        private Image GetOpOpenOperatorImage(OpStatus opStatus)
        {
            switch (opStatus)
            {
                case OpStatus.Null:
                    return Properties.Resources.opSet;
                case OpStatus.Ready:
                    return Properties.Resources.opSetSuccess;
                case OpStatus.Done:
                    return Properties.Resources.opDone;
                case OpStatus.Warn:
                    return Properties.Resources.opWarn;
                default:
                    return Properties.Resources.operator_w_icon;
            }
        }

        void MenuDesignOp_Click(object sender, EventArgs e)
        {
            C2BaseOperatorView dialog = GenerateOperatorView();
            if (dialog != null && dialog.ShowDialog(this) == DialogResult.OK)
                opw.Status = OpStatus.Ready;
        }
        private C2BaseOperatorView GenerateOperatorView()
        {
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
                default: return null;
            }
        }

        void MenuOpenOperatorDesigner_Click(object sender, EventArgs e)
        {
            ShowDesigner(opw.Container,true);
        }

        void MenuRunningOp_Click(object sender, EventArgs e)
        {
            if (Global.GetCurrentDocument().Modified)
            {
                HelpUtil.ShowMessageBox("业务视图未保存，请保存后再运行。", "未保存");
                return;
            }

            if(opw.Status == OpStatus.Ready || opw.Status == OpStatus.Done)
            {
                List<string> cmds = GenerateCmd();
                if (cmds == null)
                    return;
                MessageBox.Show(RunLinuxCommand(cmds));
                DataItem resultItem = opw.ResultItem;
                ResultWidget rsw = (opw.Container as Topic).FindWidget<ResultWidget>();
                if (rsw == null)
                {
                    AddResult(new Topic[] { opw.Container as Topic }, resultItem);
                    TopicUpdate(opw.Container as Topic,null); 
                }
                   
                else
                {
                    //TODO
                    //是否对undo redo有影响
                    rsw.DataItems.Clear();
                    rsw.DataItems.Add(resultItem);
                }
                opw.Status = OpStatus.Done;
            }
        }

        private List<string> GenerateCmd()
        {
            switch (opw.OpType)
            {
                case OpType.MaxOperator: return (new MaxOperatorCmd(opw)).GenCmd();
                case OpType.CustomOperator: return  (new CustomOperatorCmd(opw)).GenCmd();
                case OpType.MinOperator: return  (new MinOperatorCmd(opw)).GenCmd();
                case OpType.RandomOperator: return  (new RandomOperatorCmd(opw)).GenCmd();
                case OpType.FilterOperator: return  (new FilterOperatorCmd(opw)).GenCmd();
                case OpType.AvgOperator: return  (new AvgOperatorCmd(opw)).GenCmd();
                case OpType.SortOperator: return  (new SortOperatorCmd(opw)).GenCmd();
                case OpType.FreqOperator: return  (new FreqOperatorCmd(opw)).GenCmd();
                case OpType.GroupOperator: return  (new GroupOperatorCmd(opw)).GenCmd();
                case OpType.DataFormatOperator: return  (new DataFormatOperatorCmd(opw)).GenCmd();
                case OpType.PythonOperator: return  (new PythonOperatorCmd(opw)).GenCmd();
                default: return null;
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
             Delete(new ChartObject[] { opw });
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
            TopicUpdate(dtw.Container,hitItem);
            ShowDesigner(dtw.Container,false);
            if (dtw.DataItems.IsEmpty())
                Delete(new ChartObject[] { dtw });
        }
        void ChartMenuDelete_Click(object sender, EventArgs e)
        {
            DataItem hitItem = (sender as ToolStripMenuItem).Tag as DataItem;
            // 剩余最后一个菜单项，删除数据源挂件
            cw.DataItems.Remove(hitItem);
            ShowDesigner(cw.Container,false);
            if (cw.DataItems.IsEmpty())
                Delete(new ChartObject[] { cw });
        }

        void DSWidgetMenuDelete_Click(object sender, EventArgs e)
        {
            Delete(new ChartObject[] { opw });
        }

        void MenuGetChart_Click(object sender, EventArgs e)
        {
            DataItem hitItem = (sender as ToolStripMenuItem).Tag as DataItem;
            DataItem dataCopy = new DataItem(
               hitItem.FilePath,
               hitItem.FileName,
               hitItem.FileSep,
               hitItem.FileEncoding,
               hitItem.FileType);

            VisualDisplayDialog displayDialog = new VisualDisplayDialog(dataCopy);
            if (DialogResult.OK != displayDialog.ShowDialog())
                return;
            ChartWidget cw = currentTopic.FindWidget<ChartWidget>();
            // 生成图表挂件
            if (cw == null)
            {
                Topic[] hitTopic = new Topic[] { currentTopic };
                AddChartWidget(hitTopic);
            }
            UpdateChartWidgetMenu(currentTopic.FindWidget<ChartWidget>(), dataCopy);
        }
        void MenuViewChart_Click(object sender, EventArgs e)
        {
            DataItem hitItem = (sender as ToolStripMenuItem).Tag as DataItem;
            string path = hitItem.FilePath;
            Utils.OpUtil.Encoding encoding = hitItem.FileEncoding;
            // 获取选中输入、输出各列数据
            string fileContent;
            if (hitItem.FileType == OpUtil.ExtType.Excel)
                fileContent = BCPBuffer.GetInstance().GetCachePreViewExcelContent(path);
            else
                fileContent = BCPBuffer.GetInstance().GetCachePreViewBcpContent(path, encoding);
            List<string> rows = new List<string>(fileContent.Split('\n'));
            // 最多绘制前100行数据
            int upperLimit = Math.Min(rows.Count, 100);
            List<List<string>> columnValues = Utils.FileUtil.GetColumns(hitItem.SelectedIndexs, hitItem, rows, upperLimit);
            if (columnValues.Count == 0)
                return;
            Utils.ControlUtil.PaintChart(columnValues, hitItem.SelectedItems, hitItem.ChartType);
        }
        void UpdateChartWidgetMenu(ChartWidget widget, DataItem hitItem)
        {
            DataItem item = widget.DataItems.Find((DataItem d) => d.FileName == hitItem.FileName && d.ChartType == hitItem.ChartType);
            if (item != null)
            {
                widget.DataItems.Remove(item);
            }
            widget.DataItems.Add(hitItem);
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

        #region 附件挂件
        private void CreateAttachmentWidgetMenu(AttachmentWidget atw)
        {
            foreach (string path in atw.FullFilePaths)
            {
                ToolStripMenuItem MenuOpenData = new ToolStripMenuItem();
                ToolStripMenuItem MenuExploreDirectory = new ToolStripMenuItem();
                ToolStripMenuItem MenuDelete = new ToolStripMenuItem();
                ToolStripMenuItem MenuOpenDataSource = new ToolStripMenuItem();

                MenuOpenDataSource.Text = String.Format("{0}{1}{2}{3}", Path.GetFileNameWithoutExtension(path), " [", Path.GetExtension(path).Trim('.'), "]");
                switch (Path.GetExtension(path).Trim('.'))
                {
                    case "txt":
                        MenuOpenDataSource.Image = Properties.Resources.txtData;
                        break;
                    case "bcp":
                        MenuOpenDataSource.Image = Properties.Resources.bcpData;
                        break;
                    case "doc":
                    case "docx":
                        MenuOpenDataSource.Image = Properties.Resources.docData;
                        break;
                    case "xls":
                    case "xlsx":
                        MenuOpenDataSource.Image = Properties.Resources.xlsData;
                        break;
                    case "pdf":
                        MenuOpenDataSource.Image = Properties.Resources.pdfData;
                        break;
                    default:
                        break;
                }
                MenuOpenDataSource.DropDownItems.AddRange(new ToolStripItem[] {
                MenuOpenData,
                MenuExploreDirectory,
                MenuDelete});

                MenuOpenData.Image = Properties.Resources.opendata;
                MenuOpenData.Tag = path;
                MenuOpenData.Text = Lang._("OpenData");
                MenuOpenData.Click += MenuOpenData_Click;

                MenuExploreDirectory.Image = Properties.Resources.datadirectory;
                MenuExploreDirectory.Text = Lang._("ExploreDirectory");
                MenuExploreDirectory.Tag = path;
                MenuExploreDirectory.Click += MenuExploreDirectory_Click;

                MenuDelete.Image = Properties.Resources.deletewidget;
                MenuDelete.Text = Lang._("Delete");
                MenuDelete.Tag = path;
                MenuDelete.Click += AttachmentMenuDelete_Click;

                WidgetMenuStrip.Items.Add(MenuOpenDataSource);
            }
        }

        void MenuOpenData_Click(object sender, EventArgs e)
        {
            string directory = (sender as ToolStripMenuItem).Tag as string;
            try
            {
                Process.Start(directory);
            }
            catch {
                MessageBox.Show("该类型文件无法打开.",
                    "打开错误",                // 标题
                    MessageBoxButtons.OK,          // 按钮样式
                    MessageBoxIcon.Information);   // 图标样式
            }
        }

        void MenuExploreDirectory_Click(object sender, EventArgs e)
        {
            string directory = (sender as ToolStripMenuItem).Tag as string;
            if (directory != null)
                FileUtil.ExploreDirectory(directory);
        }


        void AttachmentMenuDelete_Click(object sender, EventArgs e)
        {
            string directory = (sender as ToolStripMenuItem).Tag as string;
            atw.FullFilePaths.Remove(directory);           
            if (atw.FullFilePaths.IsEmpty())
                Delete(new ChartObject[] { atw });
        }
        #endregion
    }
}
