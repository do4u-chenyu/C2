﻿using C2.Business.Model;
using C2.Business.Option;
using C2.Core;
using C2.Database;
using C2.Dialogs;
using C2.Dialogs.Base;
using C2.Forms;
using C2.Globalization;
using C2.IAOLab.WebEngine;
using C2.Model;
using C2.Model.MindMaps;
using C2.Model.Widgets;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

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
        private MapWidget mw;
        private Topic currentTopic;

        public void CreateWidgetMenu()
        {
            WidgetMenuStrip.SuspendLayout();
            WidgetMenuStrip.Items.Clear();

            currentTopic = HoverObject.Topic;

            switch (HoverObject.Widget.GetTypeID())
            {
                case OperatorWidget.TypeID:
                    opw = HoverObject.Widget as OperatorWidget;
                    //没有单算子 且 没有模型
                    if (opw.OpType == OpType.Null && !opw.HasModelOperator)
                        CreateInitOperatorMenu();
                    else
                        CreateOperatorMenu();                        
                    break;
                case DataSourceWidget.TypeID:
                    dtw = HoverObject.Widget as DataSourceWidget;
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
                case MapWidget.TypeID:
                    mw = HoverObject.Widget as MapWidget;
                    CreateMapWidgetMenu(mw);
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
        public bool IsSQLOp()
        {
            return opw.OpType == OpType.SqlOperator;
        }
        #region 图表挂件
        private void CreateChartWidgetMenu(ChartWidget cw)
        {
            WidgetMenuStrip.SuspendLayout();
            foreach (DataItem dataItem in cw.DataItems)
            {

                ToolStripMenuItem MenuViewDataChart = new ToolStripMenuItem();
                ToolStripMenuItem MenuDeleteDataChart = new ToolStripMenuItem();
                ToolStripMenuItem MenuDataChartParent = new ToolStripMenuItem();
                MenuDataChartParent.Image = Properties.Resources.图表;

                MenuDataChartParent.Text = String.Format("{0}[{1}]", dataItem.FileName, dataItem.ChartType);
                MenuDataChartParent.DropDownItems.AddRange(new ToolStripItem[] {
                MenuViewDataChart,
                MenuDeleteDataChart});

                MenuViewDataChart.Image = Properties.Resources.getChart;
                MenuViewDataChart.Text = Lang._("ViewChart");
                MenuViewDataChart.Tag = dataItem;
                MenuViewDataChart.Click += MenuViewDataChart_Click;

                MenuDeleteDataChart.Image = Properties.Resources.deleteWidget;
                MenuDeleteDataChart.Text = Lang._("Delete");
                MenuDeleteDataChart.Tag = dataItem;
                MenuDeleteDataChart.Click += MenuDeleteDataChart_Click;

                WidgetMenuStrip.Items.Add(MenuDataChartParent);
            }
            WidgetMenuStrip.ResumeLayout();
        }

        #endregion

        #region 算子挂件
        private void CreateInitOperatorMenu()
        {
            ToolStripMenuItem MenuOpenOperator = new ToolStripMenuItem();
            ToolStripMenuItem MenuDeleteOperator = new ToolStripMenuItem();

            MenuOpenOperator.Text = Lang._("OpenDesigner");
            MenuOpenOperator.Image = Properties.Resources.算子;
            MenuOpenOperator.Click += MenuOpenOperatorDesigner_Click;

            MenuDeleteOperator.Text = Lang._("Delete");
            MenuDeleteOperator.Image = Properties.Resources.deleteWidget;
            MenuDeleteOperator.Click += MenuDeleteSingleOp_Click;

            WidgetMenuStrip.Items.Add(MenuOpenOperator);
            WidgetMenuStrip.Items.Add(MenuDeleteOperator);
        }
        private void CreateOperatorMenu()
        {
            if(opw.OpType != OpType.Null)
                WidgetMenuStrip.Items.Add(GenOpSingleMenu("single"));
            if(opw.HasModelOperator)
                WidgetMenuStrip.Items.Add(GenOpSingleMenu("model"));
        }

        private ToolStripMenuItem GenOpSingleMenu(string type)
        {
            ToolStripMenuItem MenuOpenOperator = new ToolStripMenuItem();
            ToolStripMenuItem MenuOpDesign = new ToolStripMenuItem();
            ToolStripMenuItem MenuOpRunning = new ToolStripMenuItem();
            ToolStripMenuItem MenuOpPublic = new ToolStripMenuItem();
            ToolStripMenuItem MenuOpDelete = new ToolStripMenuItem();

            MenuOpenOperator.Image = type == "single" ? opw.GetOpOpenOperatorImage() : Properties.Resources.算子; 
            MenuOpenOperator.Text = type == "single" ? opw.OpName: opw.ModelDataItem.FileName;
            MenuOpenOperator.DropDownItems.AddRange(new ToolStripItem[] {
                MenuOpDesign,
                MenuOpRunning,
                MenuOpPublic,
                MenuOpDelete});

            MenuOpDesign.Image = Properties.Resources.opDesign;
            MenuOpDesign.Text = Lang._("Design");
            if (type == "single")
                MenuOpDesign.Click += MenuDesignOp_Click;
            else
                MenuOpDesign.Click += MenuDesignModel_Click;

            if(type == "single")
            {
                MenuOpRunning.Image = Properties.Resources.opRunning;
                MenuOpRunning.Text = Lang._("Running");
                MenuOpRunning.Enabled = type == "single" ? opw.Status != OpStatus.Null : !opw.HasModelOperator;
                if (opw.DataSourceItem.DataType == DatabaseType.Null && !IsSQLOp())
                    MenuOpRunning.Click += MenuRunningOp_Click;
                else
                    MenuOpRunning.Click += MenuRunningSQLOp_Click;
            }
            else
            {
                MenuOpRunning.Image = Properties.Resources.modelUpdate;
                MenuOpRunning.Text = Lang._("ModelUpdate");
                MenuOpRunning.Enabled = Global.GetMainForm().TaskBar.Items.Contains(opw.ModelRelateTab);
                MenuOpRunning.Click += MenuModelUpdate_Click;
            }


            MenuOpPublic.Image = Properties.Resources.opModelPublic;
            MenuOpPublic.Text = Lang._("Public");
            MenuOpPublic.Enabled = type == "single" ? false : true;
            MenuOpPublic.Click += MenuOpPublic_Click;

            MenuOpDelete.Image = Properties.Resources.deleteWidget;
            MenuOpDelete.Text = Lang._("Delete");
            if(type == "single")
                MenuOpDelete.Click += MenuDeleteSingleOp_Click;
            else    
                MenuOpDelete.Click += MenuDeleteModelOp_Click;

            return MenuOpenOperator;
        }

        void MenuDesignModel_Click(object sender, EventArgs e)
        {
            TabItem tab = opw.ModelRelateTab;
            TabBar tabBar = Global.GetMainForm().TaskBar;
            if (tabBar.Items.Contains(tab))
                tabBar.SelectedItem = tab;
            else
            {
                Topic topic = opw.Container as Topic;
                string modelDocumentName = opw.ModelDataItem.FileName;
                Global.GetMainForm().LoadCanvasFormByMindMap(modelDocumentName, Global.GetCurrentDocument().Name, topic);
            }
        }
        void MenuDesignOp_Click(object sender, EventArgs e)
        {
            if(opw.DataSourceItem != null && opw.DataSourceItem.DataType == DatabaseType.Null && !string.IsNullOrEmpty(opw.DataSourceItem.FilePath))
            {
                string message = FileUtil.FileExistOrUse(opw.DataSourceItem.FilePath);
                if (!string.IsNullOrEmpty(message))
                {
                    MessageBox.Show(message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            Cursor tempCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            C2BaseOperatorView dialog = GenerateOperatorView();
            if (dialog == null)
                return;
            DialogResult dr = dialog.ShowDialog(this);
            if (dr == DialogResult.OK)
                opw.Status = OpStatus.Ready;
 
            this.Cursor = tempCursor;
        }
        void MenuOpenOperatorDesigner_Click(object sender, EventArgs e)
        {
            ShowDesigner(opw.Container,true);
        }
        void MenuRunningOp_Click(object sender, EventArgs e)
        {
            Global.GetDocumentForm().Save();
            GenRunCmds();
        }
        void MenuRunningSQLOp_Click(object sender, EventArgs e)
        {
            using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
            {
                Global.GetDocumentForm().Save();
                RunSQL();
            }
               
        }

        void MenuOpPublic_Click(object sender, EventArgs e)
        {
            if (!opw.HasModelOperator || opw.ModelDataItem == null)
                return;

            CreateNewModelForm createNewModelForm = new CreateNewModelForm
            {
                StartPosition = FormStartPosition.CenterScreen,
                RelateMindMapView = this,
                ModelType = "添加模型市场",
                ModelTitle = opw.ModelDataItem.FileName
            };
            DialogResult dialogResult = createNewModelForm.ShowDialog();
            if (dialogResult != DialogResult.OK)
                return;

            //发布前先保存一次已打开的模型视图，否则会出现一直未保存模型导致发布路径逻辑出错
            if(opw.ModelRelateTab != null)
                (opw.ModelRelateTab.Tag as CanvasForm).Save();

            string modelNewName = createNewModelForm.ModelTitle;
            string modelPath = opw.ModelDataItem.FilePath;

            if (!ExportModel.GetInstance().Export(modelPath, modelNewName, Path.Combine(Global.UserWorkspacePath, "模型市场")))
                return;

            //复制之后修改XML文件中数据源路径
            string modelDir = Path.Combine(Global.WorkspaceDirectory, Global.GetMainForm().UserName, "模型市场", modelNewName);
            string modelFilePath = Path.Combine(modelDir, modelNewName + ".xml");
            string dirs = Path.Combine(modelDir, "_datas");
            ImportModel.GetInstance().RenameFile(dirs, modelFilePath);

            if (!Global.GetMyModelControl().ContainModel(modelNewName))
                Global.GetMyModelControl().AddModel(modelNewName);
            HelpUtil.ShowMessageBox("模型成功发布到模型市场");
        }
        void MenuModelUpdate_Click(object sender, EventArgs e)
        {
            TabItem tab = opw.ModelRelateTab;
            if (Global.GetMainForm().TaskBar.Items.Contains(tab))
                (opw.ModelRelateTab.Tag as CanvasForm).UpdateTopicResults(opw.Container as Topic);
            Global.OnModifiedChange();
        }
        void MenuDeleteSingleOp_Click(object sender, EventArgs e)
        {
            ClearSingleOpContent();
            if (!opw.HasModelOperator)
                (opw.Container as Topic).Widgets.Remove(opw);
                //Delete(new ChartObject[] { opw });
            Global.OnModifiedChange();
        }
        void MenuDeleteModelOp_Click(object sender, EventArgs e)
        {
            //模型删除本地文件
            //FileUtil.DeleteDirectory(Path.GetDirectoryName(opw.ModelDataItem.FilePath));
            ClearModelOpContent();
            if (opw.OpType == OpType.Null)
                (opw.Container as Topic).Widgets.Remove(opw);
                //Delete(new ChartObject[] { opw });
            Global.OnModifiedChange();
        }
        private void ClearSingleOpContent()
        {
            opw.Status = OpStatus.Null;
            opw.DataSourceItem = null;
            opw.ResultItem = null;
            opw.OpType = OpType.Null;
            opw.Option = new OperatorOption();
        }
        private void ClearModelOpContent()
        {
            CloseRelateOpTab(opw);
            opw.ModelDataItem = null;
            opw.HasModelOperator = false;
        }
        #endregion

        #region 数据挂件
        private void CreateDataSourceMenu(DataSourceWidget dtw)
        {
            foreach (DataItem dataItem in dtw.DataItems)
            {
                ToolStripMenuItem MenuViewData = new ToolStripMenuItem();
                ToolStripMenuItem MenuCreateChart = new ToolStripMenuItem();
                ToolStripMenuItem MenuDelete = new ToolStripMenuItem();
                ToolStripMenuItem MenuExploreDirectory = new ToolStripMenuItem();
                ToolStripMenuItem MenuCopyFilePathToClipboard = new ToolStripMenuItem();
                ToolStripMenuItem MenuOpenDataSource = new ToolStripMenuItem();
                MenuOpenDataSource.Image = Properties.Resources.数据;

                MenuOpenDataSource.Text = dataItem.DataType == DatabaseType.Null ? String.Format("{0}[{1}]", dataItem.FileName, Path.GetExtension(dataItem.FilePath).Trim('.')) : String.Format("{0}[{1}]", dataItem.FileName, dataItem.DataType);
                MenuOpenDataSource.DropDownItems.AddRange(new ToolStripItem[] {
                MenuViewData,
                MenuCreateChart,
                MenuDelete,
                new ToolStripSeparator(),
                MenuExploreDirectory,
                MenuCopyFilePathToClipboard});

                MenuViewData.Image = Properties.Resources.viewData;
                MenuViewData.Tag = dataItem;
                MenuViewData.Text = Lang._("ViewData");        // 预览数据
                MenuViewData.ToolTipText = "预览数据源前一百条数据";
                MenuViewData.Click += MenuPreViewData_Click;

                MenuCreateChart.Image = Properties.Resources.getChart;              
                MenuCreateChart.Text = Lang._("CreateChart");  // 生成图表 
                MenuCreateChart.Tag = dataItem;
                MenuCreateChart.ToolTipText = "仅支持数据源前一百行数据生成图表";
                MenuCreateChart.Click += MenuCreateDataChart_Click;

                MenuDelete.Image = Properties.Resources.deleteWidget;
                MenuDelete.Text = Lang._("Delete");           //  删除
                MenuDelete.Tag = dataItem;
                MenuDelete.Click += MenuDelete_Click;

                MenuExploreDirectory.Image = Properties.Resources.dataDirectory;
                MenuExploreDirectory.Text = Lang._("ExploreDirectory");
                MenuExploreDirectory.Tag = dataItem.FilePath;
                MenuExploreDirectory.Click += MenuExploreDirectory_Click;
                if (dataItem.IsDatabase())  // 外部数据源不存在浏览文件夹的逻辑
                    MenuExploreDirectory.Enabled = false;

                MenuCopyFilePathToClipboard.Image = Properties.Resources.copyFilePath;
                MenuCopyFilePathToClipboard.Text = Lang._("CopyFilePathToClipboard");
                MenuCopyFilePathToClipboard.Tag = dataItem.FilePath;
                MenuCopyFilePathToClipboard.Click += MenuCopyFilePathToClipboard_Click;

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
                (dtw.Container as Topic).Widgets.Remove(dtw);
                //Delete(new ChartObject[] { dtw });
            Global.OnModifiedChange();
        }

        void MenuDeleteDataChart_Click(object sender, EventArgs e)
        {
            DataItem hitItem = (sender as ToolStripMenuItem).Tag as DataItem;
            // 剩余最后一个菜单项，删除数据源挂件
            cw.DataItems.Remove(hitItem);
            ShowDesigner(cw.Container,false);
            if (cw.DataItems.IsEmpty())
                (cw.Container as Topic).Widgets.Remove(cw);
                //Delete(new ChartObject[] { cw });
            Global.OnModifiedChange();
        }

        void MenuCreateDataChart_Click(object sender, EventArgs e)
        {
            DataItem hitItem = (sender as ToolStripMenuItem).Tag as DataItem;
            if (hitItem.IsDatabase())
            {
                BCPBuffer.GetInstance().GetCachePreviewTable(hitItem.DBItem); // TODO DK 预加载也要写好方法
            }

            // 内部数据源且文件不存在
            if (!hitItem.IsDatabase() && !File.Exists(hitItem.FilePath))
            {
                HelpUtil.ShowMessageBox(hitItem.FilePath + "文件不存在", "文件不存在");
                return;
            }

            DataItem dataCopy = hitItem.Clone();
     
            VisualDisplayDialog displayDialog = new VisualDisplayDialog(dataCopy);
            if (DialogResult.OK != displayDialog.ShowDialog())
                return;
            ChartWidget cw = currentTopic.FindWidget<ChartWidget>();
            // 生成图表挂件
            if (cw == null)
            {
                currentTopic.Widgets.Add(new ChartWidget { DataItems = new List<DataItem> { dataCopy } });
            }
            UpdateChartWidgetMenu(currentTopic.FindWidget<ChartWidget>(), dataCopy);
            Global.OnModifiedChange();
        }
        void MenuViewDataChart_Click(object sender, EventArgs e)
        {
            DataItem hitItem = (sender as ToolStripMenuItem).Tag as DataItem;
            ChartWidget.DoViewDataChart(hitItem);
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
            
            foreach (DataItem dataItem in rsw.DataItems)
            {
                ToolStripMenuItem MenuPreViewData = new ToolStripMenuItem();
                ToolStripMenuItem MenuDelete = new ToolStripMenuItem();
                ToolStripMenuItem MenuProcessData = new ToolStripMenuItem();
                ToolStripMenuItem MenuJoinPool = new ToolStripMenuItem();
                ToolStripMenuItem MenuCreateChart = new ToolStripMenuItem();
                ToolStripMenuItem MenuFileSavaAs = new ToolStripMenuItem();
                ToolStripMenuItem MenuExploreDirectory = new ToolStripMenuItem();
                ToolStripMenuItem MenuCopyFilePathToClipboard = new ToolStripMenuItem();
                ToolStripMenuItem MenuOpenResult = new ToolStripMenuItem();
                MenuOpenResult.Image = Properties.Resources.结果;

                MenuOpenResult.Text = String.Format("{0}[{1}]", dataItem.FileName, Path.GetExtension(dataItem.FilePath).Trim('.'));
                MenuOpenResult.DropDownItems.AddRange(new ToolStripItem[] {
                MenuPreViewData,
                MenuProcessData,
                MenuDelete,
                MenuJoinPool,
                MenuCreateChart,
                new ToolStripSeparator(),
                MenuFileSavaAs,
                MenuExploreDirectory,
                MenuCopyFilePathToClipboard});

                MenuPreViewData.Image = Properties.Resources.viewData;
                MenuPreViewData.Tag = dataItem;
                MenuPreViewData.Text = Lang._("ViewData");  //数据预览
                MenuPreViewData.ToolTipText = "预览数据源前一百条数据";
                MenuPreViewData.Click += MenuPreViewData_Click;

                MenuProcessData.Image = Properties.Resources.dealData;
                MenuProcessData.Text = Lang._("ProcessData");
                MenuProcessData.Click += MenuProcessData_Click;

                MenuJoinPool.Image = Properties.Resources.joinPool;
                MenuJoinPool.Text = Lang._("JoinPool");
                MenuJoinPool.Tag = dataItem;
                MenuJoinPool.Click += MenuJoinPool_Click;

                MenuCreateChart.Image = Properties.Resources.getChart;
                MenuCreateChart.Text = Lang._("CreateChart");  // 生成图表 
                MenuCreateChart.Tag = dataItem;
                MenuCreateChart.ToolTipText = "仅支持数据源前一百行数据生成图表";
                MenuCreateChart.Click += MenuCreateDataChart_Click;

                MenuDelete.Image = Properties.Resources.deleteWidget;
                MenuDelete.Text = Lang._("Delete");
                MenuDelete.Tag = dataItem;
                MenuDelete.Click += MenuDeleteRes_Click;

                MenuFileSavaAs.Image = Properties.Resources.resultDone;
                MenuFileSavaAs.Text = Lang._("Save As");
                MenuFileSavaAs.Tag = dataItem.FilePath;
                MenuFileSavaAs.Click += MenuSaveAs_Click;

                MenuExploreDirectory.Image = Properties.Resources.dataDirectory;
                MenuExploreDirectory.Text = Lang._("ExploreDirectory");
                MenuExploreDirectory.Tag = dataItem.FilePath;
                MenuExploreDirectory.Click += MenuExploreDirectory_Click;

                MenuCopyFilePathToClipboard.Image = Properties.Resources.copyFilePath;
                MenuCopyFilePathToClipboard.Text = Lang._("CopyFilePathToClipboard");
                MenuCopyFilePathToClipboard.Tag = dataItem.FilePath;
                MenuCopyFilePathToClipboard.Click += MenuCopyFilePathToClipboard_Click;

                WidgetMenuStrip.Items.Add(MenuOpenResult);
            }
        }

        void MenuPreViewData_Click(object sender, EventArgs e)
        {
            C2BaseWidget.DoPreViewDataSource((sender as ToolStripMenuItem).Tag as DataItem);
        }

        void MenuProcessData_Click(object sender, EventArgs e)
        {
            AddSubTopic(rsw.Container as Topic, null, false);
        }
        void MenuDeleteRes_Click(object sender, EventArgs e)
        {
            DataItem hitItem = (sender as ToolStripMenuItem).Tag as DataItem;
            // 剩余最后一个菜单项，删除数据源挂件
            rsw.DataItems.Remove(hitItem);
            TopicUpdate(rsw.Container, hitItem);
            if (rsw.DataItems.IsEmpty())
                (rsw.Container as Topic).Widgets.Remove(rsw);
                //Delete(new ChartObject[] { rsw });
            Global.OnModifiedChange();
        }
        void MenuJoinPool_Click(object sender, EventArgs e)
        {
            DataItem hitItem = (sender as ToolStripMenuItem).Tag as DataItem;
            string destDirectory = Path.Combine(Global.UserWorkspacePath, "数据池");
            string destFilePath = Path.Combine(destDirectory, Path.GetFileName(hitItem.FilePath));
            Directory.CreateDirectory(destDirectory);
            File.Copy(hitItem.FilePath, destFilePath, true);
            Global.GetDataSourceControl().GenDataButton(hitItem.FileName,
                                                        destFilePath,
                                                        hitItem.FileSep,
                                                        hitItem.FileType,
                                                        hitItem.FileEncoding);
            Global.GetDataSourceControl().Visible = true;
        }
        #endregion

        #region 附件挂件
        private void CreateAttachmentWidgetMenu(AttachmentWidget atw)
        {
            foreach (string path in atw.AttachmentPaths)
            {
                ToolStripMenuItem MenuOpenAttachment = new ToolStripMenuItem();
                ToolStripMenuItem MenuExploreDirectory = new ToolStripMenuItem();
                ToolStripMenuItem MenuCopyFilePathToClipboard = new ToolStripMenuItem();
                ToolStripMenuItem MenuDeleteAttachment = new ToolStripMenuItem();
                ToolStripMenuItem MenuAttachment = new ToolStripMenuItem();

                MenuAttachment.Text = String.Format("{0}[{1}]", Path.GetFileNameWithoutExtension(path), Path.GetExtension(path).TrimStart('.'));
                switch (Path.GetExtension(path))
                {
                    case ".txt":
                        MenuAttachment.Image = Properties.Resources.txtData;
                        break;
                    case ".bcp":
                        MenuAttachment.Image = Properties.Resources.bcpData;
                        break;
                    case ".doc":
                    case ".docx":
                        MenuAttachment.Image = Properties.Resources.docData;
                        break;
                    case ".xls":
                    case ".xlsx":
                        MenuAttachment.Image = Properties.Resources.xlsData;
                        break;
                    case ".pdf":
                        MenuAttachment.Image = Properties.Resources.pdfData;
                        break;
                    case ".xmind":
                        MenuAttachment.Image = Properties.Resources.xmindData;
                        break;
                    default:
                        break;
                }
                MenuAttachment.DropDownItems.AddRange(new ToolStripItem[] {
                MenuOpenAttachment,
                MenuExploreDirectory,
                MenuCopyFilePathToClipboard,
                MenuDeleteAttachment});

                MenuOpenAttachment.Image = Properties.Resources.opendata;
                MenuOpenAttachment.Tag = path;
                MenuOpenAttachment.Text = Lang._("OpenAttachment");
                MenuOpenAttachment.Click += MenuOpenAttachment_Click;

                MenuExploreDirectory.Image = Properties.Resources.dataDirectory;
                MenuExploreDirectory.Text = Lang._("ExploreDirectory");
                MenuExploreDirectory.Tag = path;
                MenuExploreDirectory.Click += MenuExploreDirectory_Click;

                MenuCopyFilePathToClipboard.Image = Properties.Resources.copyFilePath;
                MenuCopyFilePathToClipboard.Text = Lang._("CopyFilePathToClipboard");
                MenuCopyFilePathToClipboard.Tag = path;
                MenuCopyFilePathToClipboard.Click += MenuCopyFilePathToClipboard_Click;

                MenuDeleteAttachment.Image = Properties.Resources.deleteAttachment;
                MenuDeleteAttachment.Text = Lang._("DeleteAttachment");
                MenuDeleteAttachment.Tag = path;
                MenuDeleteAttachment.Click += MenuDeleteAttachment_Click;

                WidgetMenuStrip.Items.Add(MenuAttachment);
            }
        }

        void MenuOpenAttachment_Click(object sender, EventArgs e)
        {
            AttachmentWidget.DoOpenAttachment((sender as ToolStripMenuItem).Tag as string);
        }

        void MenuExploreDirectory_Click(object sender, EventArgs e)
        {
            string ffp = (sender as ToolStripMenuItem).Tag as string;
            FileUtil.ExploreDirectory(ffp);
        }

        void MenuSaveAs_Click(object sender, EventArgs e)
        {
            string ffp = (sender as ToolStripMenuItem).Tag as string;
            SaveFileDialog sfd = new SaveFileDialog
            {
                FileName = Path.GetFileName(ffp)
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileUtil.FileCopy(ffp, sfd.FileName);
            }
        }

        void MenuCopyFilePathToClipboard_Click(object sender, EventArgs e) 
        {
            string ffp = (sender as ToolStripMenuItem).Tag as string;
            FileUtil.TryClipboardSetText(ffp);
        }


        void MenuDeleteAttachment_Click(object sender, EventArgs e)
        {
            string ffp = (sender as ToolStripMenuItem).Tag as string;
            atw.AttachmentPaths.Remove(ffp);           
            if (atw.AttachmentPaths.IsEmpty())
                (atw.Container as Topic).Widgets.Remove(atw);
                //Delete(new ChartObject[] { atw });
        }
        #endregion

        #region 地图挂件
        private void CreateMapWidgetMenu(MapWidget mw)
        {
            ToolStripMenuItem MenuMap = new ToolStripMenuItem();
            ToolStripMenuItem MenuOpenMap = new ToolStripMenuItem();
            ToolStripMenuItem MenuDeleteMap = new ToolStripMenuItem();

            MenuMap.Text = String.Format("地图");//TODO phx 名字
            MenuMap.DropDownItems.AddRange(new ToolStripItem[] {
                MenuOpenMap,
                MenuDeleteMap});

            MenuOpenMap.Image = Properties.Resources.opendata;
            MenuOpenMap.Text = Lang._("Open");
            MenuOpenMap.Click += MenuOpenMap_Click;

            MenuDeleteMap.Image = Properties.Resources.deleteAttachment;
            MenuDeleteMap.Text = Lang._("Delete");
            MenuDeleteMap.Click += MenuDeleteMap_Click;

            WidgetMenuStrip.Items.Add(MenuMap);
        }
        void MenuOpenMap_Click(object sender, EventArgs e)
        {
            //TODO phx 打开web窗口
            new WebManager()
            {
                Type = WebManager.WebType.Map
                //Type = WebManager.WebType.Map,
                //WebUrl = mw.WebUrl
            }.OpenWebBrowser();
        }
        void MenuDeleteMap_Click(object sender, EventArgs e)
        {
            (mw.Container as Topic).Widgets.Remove(mw);
        }
        #endregion
    }
}
