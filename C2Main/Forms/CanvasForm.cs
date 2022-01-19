﻿using C2.Business.Model;
using C2.Business.Option;
using C2.Business.Schedule;
using C2.Controls;
using C2.Controls.Flow;
using C2.Controls.Move;
using C2.Controls.Move.Dt;
using C2.Controls.Move.Op;
using C2.Controls.Right;
using C2.Controls.Top;
using C2.Core;
using C2.Core.UndoRedo;
using C2.Core.UndoRedo.Command;
using C2.Dialogs;
using C2.Model;
using C2.Model.MindMaps;
using C2.Model.Widgets;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace C2.Forms
{
    public partial class CanvasForm : BaseDocumentForm
    {
        bool HardClose;
        private ShortcutKeysTable ShortcutKeys;
        private OptionDao optionDao;
        private UndoRedoManager undoRedoManager;
        private ModelDocument document;
        private readonly string userInfoPath = Path.Combine(Global.WorkspaceDirectory, "UserInformation.xml");
        private string userName;
        private string mindMapName;
        private DocumentForm mindMapDoc;
        public CanvasPanel CanvasPanel{ get { return this.canvasPanel; }}
        public RemarkControl RemarkControl {  get { return this.remarkControl; }  }
        public OperatorControl OperatorControl { get { return this.operatorControl; } }
        public OptionDao OptionDao { get { return this.optionDao; } }
        public UndoRedoManager UndoRedoManager { get { return this.undoRedoManager; } }
        public NaviViewControl NaviViewControl { get { return this.naviViewControl; } }
        #region 运行委托
        delegate void AsynUpdateLog(string logContent);
        delegate void AsynUpdateGif();
        delegate void TaskCallBack();
        delegate void AsynUpdateProgressBar();
        delegate void AsynUpdateMask();
        delegate void AsynUpdateOpErrorMessage();
        #endregion
        private static readonly LogUtil log = LogUtil.GetInstance("CanvasForm-i"); // 获取日志模块
        public CanvasForm()
        {
            InitializeComponent();
            InitializeDao();
            InitializeEventHandler();
            InitializeControlsLocation();
            InitializeUndoRedoManager();
            InitializeKeyBoard();
        }

        private void InitializeDao()
        {
            this.optionDao = new OptionDao();
            this.userName = Global.GetMainForm().UserName;
        }
        private void InitializeEventHandler()
        {
            // 新增文档事件
            this.canvasPanel.NewElementEvent += NewDocumentOperator;
            //this.canvasPanel.KeyDown += new KeyEventHandler(CanvasPanel_KeyDown);
            this.remarkControl.RemarkChangeEvent += RemarkChange;
        }
        private void InitializeUndoRedoManager()
        {
            this.topToolBarControl.Disable_UndoButton();
            this.topToolBarControl.Disable_RedoButton();
            this.undoRedoManager = new UndoRedoManager();
            this.undoRedoManager.RedoStackEmpty += TopToolBarControl_RedoStackEmpty;
            this.undoRedoManager.RedoStackNotEmpty += TopToolBarControl_RedoStackNotEmpty;
            this.undoRedoManager.UndoStackEmpty += TopToolBarControl_UndoStackEmpty;
            this.undoRedoManager.UndoStackNotEmpty += TopToolBarControl_UndoStackNotEmpty;
        }
        public void InitializeControlsLocation()
        {
            int x = this.canvasPanel.Width - 10 - this.naviViewControl.Width;
            int y = this.canvasPanel.Height - 5 - this.naviViewControl.Height;

            // 缩略图定位
            this.naviViewControl.Location = new Point(x, y + 30);
            this.naviViewControl.Invalidate();

            // 底层工具按钮定位
            x = x - (this.canvasPanel.Width) / 2 + 100;
            this.resetButton.Location = new Point(x + 100, y + 83);
            this.stopButton.Location = new Point(x + 50, y + 83);
            this.runButton.Location = new Point(x, y + 83);

            //运行状态动图、进度条定位
            this.currentModelRunLab.Location = new Point(
                (this.currentModelRunBackLab.Width - this.currentModelRunLab.Width) / 2,
                (this.currentModelRunBackLab.Height - this.currentModelRunLab.Height) / 2 - 10);
            this.currentModelRunBackLab.Location = new Point(x, this.canvasPanel.Height / 2 - 50);
            this.currentModelFinLab.Location = new Point(x, this.canvasPanel.Height / 2 - 50);
            this.progressBar.Location = new Point(x, this.canvasPanel.Height / 2 + 54);
            this.progressBarLabel.Location = new Point(x + 125, this.canvasPanel.Height / 2 + 50);

            // 顶层浮动工具栏和右侧工具及隐藏按钮定位
            this.operatorControl.Location = new Point(this.canvasPanel.Width - 280, 38);
            this.remarkControl.Location = new Point(this.canvasPanel.Width - 505, 38);
            this.rightHideButton.Location = new Point(this.canvasPanel.Width - 60, 38);
        }
        public CanvasForm(ModelDocument document)
            :this()
        {
            Document = document;
        }
        private void RemarkChange(RemarkControl rc)
        {
            Global.GetMainForm().SetDocumentDirty();
            this.document.RemarkDescription = rc.RemarkDescription;
        }
        public CanvasForm(ModelDocument document,Topic topic,DocumentForm mindMapForm) : this()
        {
            this.mindMapName = mindMapForm.Document.Name;
            this.mindMapDoc = mindMapForm;
            Document = document;
            RelateTopic = topic;
            FormNameToolTip = string.Format("{0}-{1}-{2}", mindMapName, topic.Text,document.Name);
        }
        public Topic RelateTopic { set; get; }

        public TopToolBarControl TopToolBarControl { get { return this.topToolBarControl; } }
        public new ModelDocument Document
        {
            get { return document; }
            set
            {
                if (document != value)
                {
                    ModelDocument old = document;
                    document = value;
                    OnDocumentChanged(old);
                }
            }
        }
        #region C1文档切换、修改、关闭
        void OnDocumentChanged(ModelDocument old)
        {
            if (old != null)
            {
                old.NameChanged -= new EventHandler(Document_NameChanged);
                old.ModifiedChanged -= new EventHandler(Document_ModifiedChanged);
            }
            if (Document != null)
            {
                this.canvasPanel.Document = document;
                Document.NameChanged += new EventHandler(Document_NameChanged);
                Document.ModifiedChanged += new EventHandler(Document_ModifiedChanged);
            }
            ResetFormTitle();
        }
        void Document_NameChanged(object sender, EventArgs e)
        {
            ResetFormTitle();
        }
        void Document_ModifiedChanged(object sender, EventArgs e)
        {
            ResetFormTitle();
        }
        private void NewDocumentOperator(MoveBaseControl ct)
        {
            ModelElement me = this.AddDocumentOperator(ct);
            Global.GetMainForm().SetDocumentDirty();
            if (ct is MoveDtControl || ct is MoveOpControl)
            {
                BaseCommand cmd = new ElementAddCommand(me);
                UndoRedoManager.GetInstance().PushCommand(this.Document, cmd);
            }
        }
        public void ResetFormTitle()
        {
            if (Document != null)
            {
                if (Document.Modified)
                    Text = string.Format("{0} *", Document.Name);
                else
                    Text = Document.Name;
            }
            else
            {
                Text = string.Empty;
            }
        }

        public void SaveDocAndTopic()
        {
            bool oldStatus = false; 
            if (Global.GetCurrentModelDocument()!=null)
            {
                oldStatus = Global.GetCurrentModelDocument().Modified;
            }        
            Save();
            // 父文档dirty
            if (Global.GetCurrentModelDocument() != null && oldStatus && !Global.GetCurrentModelDocument().Modified)
                DocumentFormDirty(this.mindMapName);
            UpdateTopicResults(RelateTopic);
        }


        private void CanvasForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!HardClose && Document != null && Document.Modified && !ReadOnly)
            {
                DialogResult result = MessageBox.Show(Document.Name+" 尚未保存，是否保存后关闭？",
                            "保存", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                // 取消操作
                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }

                // 保存文件
                if (result == DialogResult.Yes)
                {
                    SaveDocAndTopic();
                    return;
                }
                // 不保存关闭文件
                if (result == DialogResult.No)
                    return;
            }
        }
        public override void AskSave(ref bool cancel)
        {
            if (!HardClose && Document != null && Document.Modified && !ReadOnly)
            {
                DialogResult dr = this.ShowMessage("Confirm Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    Save();
                }
                else if (dr == DialogResult.Cancel)
                {
                    cancel = true;
                    return;
                }
                else
                {
                    HardClose = true;
                    Close();
                }
            }

            base.AskSave(ref cancel);
        }
        public override bool Save()
        {
            if (string.IsNullOrEmpty(Document.SavePath))
            {
                SaveAs();
            }
            else
            {
                //OnBeforeSave(Document);
                try
                {
                    Document.Save();
                    //MindMapIO.Save(Map, Map.Filename);
                    //mapView1.Modified = false;
                }
                catch (System.Exception ex)
                {
                    Helper.WriteLog(ex);
                    this.ShowMessage(ex.Message, MessageBoxIcon.Error);
                    return false;
                }

                //RecentFilesManage.Default.Push(Document.FileName, Document.CreateThumbImage());
            }
            //Global.GetMindMapModelControl().AddMindMapModel(Document.FileName);
            return true;
        }
        public override bool SaveAs()
        {
            return true;
        }
        public override string GetFileName()
        {
            if (Document != null)
                return Document.SavePath;
            else
                return null;
        }
        #endregion


        private void CanvasForm_SizeChanged(object sender, EventArgs e)
        {
            InitializeControlsLocation();
        }
        public void BlankButtonFocus()
        {
            this.blankButton.Focus();
        }
        #region 运行相关部分
        private void ResetButton_Click(object sender, EventArgs e)
        {
            //重置前打断框选、选中线
            Global.GetTopToolBarControl().InterruptSelectFrame();
            Global.GetCanvasPanel().ClearAllLineStatus();

            TaskManager currentManager = Global.GetCurrentModelDocument().TaskManager;

            //在模型运行完成，及终止的情况下，可以重置
            //Console.WriteLine(currentManager.ModelStatus.ToString());
            if (currentManager.ModelStatus != ModelStatus.GifDone && currentManager.ModelStatus != ModelStatus.Pause && currentManager.ModelStatus != ModelStatus.Running)
            {
                currentManager.GetCurrentModelTripleList(Global.GetCurrentModelDocument(), "all");
                currentManager.Reset();
                //SetDocumentDirty();//需不需要dirty
                MessageBox.Show("当前运算结果已重置，点击‘运行’可以重新运算了", "已重置", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void StopButton_Click(object sender, EventArgs e)
        {
            //终止前打断框选、选中线
            Global.GetTopToolBarControl().InterruptSelectFrame();
            Global.GetCanvasPanel().ClearAllLineStatus();

            if (this.runButton.Name == "pauseButton" || this.runButton.Name == "continueButton")
            {
                Global.GetCurrentModelDocument().TaskManager.Stop();
                UpdateRunbuttonImageInfo();
            }
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            //运算前打断框选、选中线
            Global.GetTopToolBarControl().InterruptSelectFrame();
            Global.GetCanvasPanel().ClearAllLineStatus();

            TaskManager currentManager = Global.GetCurrentModelDocument().TaskManager;
            BindUiManagerFunc();

            if (this.runButton.Name == "runButton")
            {
                //运行前自动保存
                //Global.GetCurrentModelDocument().Modified = false;
                Save();

                currentManager.GetCurrentModelTripleList(Global.GetCurrentModelDocument(), "all");
                //int notReadyNum = currentManager.CountOpStatus(ElementStatus.Null);
                int notReadyNum = currentManager.CountOpNullAndNoRelation();
                if (notReadyNum > 0)
                {
                    MessageBox.Show("有" + notReadyNum + "个未配置的算子，请配置后再运行", "未配置", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (currentManager.IsAllOperatorDone())
                {
                    MessageBox.Show("当前算子均已运算完毕，重新运算需要先点击‘重置’按钮。", "运算完毕", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string runStatus = new ModelRunDialog().ShowDialog();
                if (runStatus == "cancle") 
                    return;
                if(runStatus == "restart")
                {
                    currentManager.GetCurrentModelTripleList(Global.GetCurrentModelDocument(), "all");
                    currentManager.Reset();
                }
                currentManager.Start();
                int taskNum = currentManager.CountOpStatus(ElementStatus.Ready);
                this.progressBar.Step = taskNum > 0 ? 100 / taskNum : 100;

                this.progressBar.Value = 0;
                this.progressBarLabel.Text = "0%";
                // 业务视图Dirty
                if (!string.IsNullOrEmpty(this.mindMapName))
                    DocumentFormDirty(this.mindMapName);
            }
            else if (this.runButton.Name == "pauseButton")
            {
                currentManager.Pause();
            }
            else if (this.runButton.Name == "continueButton")
            {
                currentManager.Continue();
            }

            UpdateRunbuttonImageInfo();
        }
        private void DocumentFormDirty(string formName)
        {
            List<BaseDocumentForm> parentDocumentForm = Global.SearchDocumentForm(formName);
            foreach (BaseDocumentForm form in parentDocumentForm)
            {
                
                if (form is DocumentForm && !form.Document.Modified)
                    form.Document.Modified = true;
            }
        }
        public void BindUiManagerFunc()
        {
            TaskManager currentManager = Global.GetCurrentModelDocument().TaskManager;
            //初次运行时，绑定线程与ui交互的委托
            if (currentManager.ModelStatus == ModelStatus.Null)
            {
                currentManager.UpdateLogDelegate = UpdateLogStatus;
                currentManager.TaskCallBack = Accomplish;
                currentManager.UpdateGifDelegate = UpdateRunningGif;
                currentManager.UpdateBarDelegate = UpdateProgressBar;
                currentManager.UpdateOpErrorDelegate = UpdateOpErrorMessage;
                currentManager.UpdateMaskDelegate = EnableRunningControl;
            }
        }

        //更新op算子错误信息
        private void UpdateOpErrorMessage(TaskManager manager, int id, string error)
        {
            string message = ControlUtil.Invoke(this,new AsynUpdateOpErrorMessage(delegate ()
            {
                MoveOpControl op = Document.SearchElementByID(id).InnerControl as MoveOpControl;
                op.SetStatusBoxErrorContent(error);
            }));
            if (!string.IsNullOrEmpty(message))
                HelpUtil.ShowMessageBox(message);
        }

        //更新进度条
        private void UpdateProgressBar(TaskManager manager)
        {
            string error = string.Empty;
            if (manager.ModelStatus == ModelStatus.Running)
                error = ControlUtil.Invoke(this,new AsynUpdateProgressBar(delegate ()
                {
                    this.progressBar.Value = manager.CurrentModelTripleStatusNum(ElementStatus.Done) * 100 / manager.TripleListGen.CurrentModelTripleList.Count;
                    this.progressBarLabel.Text = this.progressBar.Value.ToString() + "%";
                }));
            if (!string.IsNullOrEmpty(error))
                HelpUtil.ShowMessageBox(error);
        }
        public bool CanClose()
        {
            if (Document.TaskManager.ModelStatus == ModelStatus.Running || Document.TaskManager.ModelStatus == ModelStatus.Pause)
            {
                DialogResult isDocClose = MessageBox.Show(string.Format("正在运行中，是否强制关闭？"), "关闭", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // 取消关闭，直接return
                // 强制关闭
                if (isDocClose == DialogResult.Yes)
                {
                    Document.TaskManager.Stop();
                    UpdateRunbuttonImageInfo();
                    return true;
                }
                return false;
            }
            return true;
        }

        //更新log
        private void UpdateLogStatus(string logContent)
        {
            if(Global.GetCanvasForm() == null)
                return;

            string error = ControlUtil.Invoke(this,new AsynUpdateLog(delegate (string tlog)
            {
                log.Info(tlog);
            }), logContent);
            if (!string.IsNullOrEmpty(error))
                HelpUtil.ShowMessageBox(error);
        }


        private void UpdateRunningGif(TaskManager manager)
        {
            string error = string.Empty;
            if (manager.ModelStatus == ModelStatus.GifDone)
                error = ControlUtil.Invoke(this,new AsynUpdateGif(delegate ()
                {
                    this.currentModelRunBackLab.Hide();
                    this.currentModelRunLab.Hide();
                    this.currentModelFinLab.Show();
                }));
            else if (manager.ModelStatus == ModelStatus.Done)
                error = ControlUtil.Invoke(this,new AsynUpdateGif(delegate ()
                {
                    this.progressBar.Hide();
                    this.progressBarLabel.Hide();
                    this.currentModelFinLab.Hide();
                }));
            if (!string.IsNullOrEmpty(error))
                HelpUtil.ShowMessageBox(error);
        }

        //完成任务时需要调用
        private void Accomplish(TaskManager manager)
        {
            Save();
            string error = ControlUtil.Invoke(this,new TaskCallBack(delegate ()
            {
                UpdateRunbuttonImageInfo();
                UpdateTopicResults(RelateTopic);
            }));
            if (!string.IsNullOrEmpty(error))
                HelpUtil.ShowMessageBox(error);
        }

        public void UpdateTopicResults(Topic topic)
        {
            if (topic == null)
                return;
            //OperatorWidget opw = RelateTopic.FindWidget<OperatorWidget>();
            //if(opw != null)
            //    RelateTopic.Remove(opw);
            //RelateTopic.Add(new OperatorWidget { HasModelOperator = true, OpName = document.Name });

            List<int> starNodes = new List<int>();
            List<int> endNodes = new List<int>();
            document.ModelRelations.ForEach(mr => { starNodes.Add(mr.StartID); endNodes.Add(mr.EndID); });
            List<ModelElement> rsElements = document.ModelElements.FindAll(me => me.Type == ElementType.Result&&me.Status == ElementStatus.Done).FindAll(me => endNodes.Contains(me.ID)&&!starNodes.Contains(me.ID));
            List<DataItem> rsDataItems = new List<DataItem>();
            foreach(ModelElement rsElement in rsElements)
            {
                DataItem tmpDataItem = new DataItem(rsElement.FullFilePath, rsElement.Description, rsElement.Separator, rsElement.Encoding, rsElement.ExtType)
                {
                    ResultDataType = DataItem.ResultType.ModelOp
                };
                rsDataItems.Add(tmpDataItem);
            }


            ResultWidget rsw = topic.FindWidget<ResultWidget>();

            /*
             * 1、没有挂件且没有结果更新时，直接返回
             * 2、没有挂件且有结果更新时，新建挂件，add
             * 3、有挂件,没有单算子结果，没有模型更新的结果，删除
             */
            if (rsw == null && rsDataItems.Count == 0)
                return;
            else if (rsw == null)
            {
                rsw = new ResultWidget {  DataItems = rsDataItems  };
                topic.Add(rsw);
            }
            else if (rsDataItems.Count == 0 && rsw.DataItems.FindAll(di => di.ResultDataType == DataItem.ResultType.SingleOp).Count == 0)
            {
                (rsw.Container as Topic).Widgets.Remove(rsw);
            }
            else
            {
                rsw.DataItems.RemoveAll(di => di.ResultDataType == DataItem.ResultType.ModelOp);
                rsw.DataItems.AddRange(rsDataItems);
            }
            this.mindMapDoc.AddSubWidget(topic);
        }

        //更新状态的节点：1、当前模型开始、终止、运行完成；2、切换文档
        public void UpdateRunbuttonImageInfo()
        {
            //当前窗口可能为首页或业务视图，当前模型视图为null，无需更新runbutton
            if (Global.GetCurrentModelDocument() == null)
                return;

            TaskManager manager = Global.GetCurrentModelDocument().TaskManager;
            switch (manager.ModelStatus)
            {
                //点击暂停按钮，均隐藏
                case ModelStatus.Pause:
                    this.runButton.Name = "continueButton";
                    this.runButton.Image = global::C2.Properties.Resources.continual;
                    this.currentModelRunBackLab.Hide();
                    this.currentModelRunLab.Hide();
                    this.progressBar.Hide();
                    this.progressBarLabel.Hide();
                    EnableRunningRsControl();
                    break;
                //点击运行按钮
                case ModelStatus.Running:
                    this.runButton.Name = "pauseButton";
                    //this.runButton.Image = global::C2.Properties.Resources.pause;
                    this.runButton.Image = global::C2.Properties.Resources.run;
                    this.runButton.Enabled = false;//暂时隐去暂停功能
                    this.currentModelRunBackLab.Show();
                    this.currentModelRunLab.Show();
                    this.progressBar.Show();
                    this.progressBarLabel.Show();
                    this.progressBar.Value = manager.CurrentModelTripleStatusNum(ElementStatus.Done) * 100 / manager.TripleListGen.CurrentModelTripleList.Count;
                    this.progressBarLabel.Text = this.progressBar.Value.ToString() + "%";
                    UnEnableRunningControl();
                    break;
                case ModelStatus.GifDone:
                    this.runButton.Name = "runButton";
                    this.runButton.Image = global::C2.Properties.Resources.run;
                    this.runButton.Enabled = true;//暂时隐去暂停功能
                    break;
                default:
                    this.runButton.Name = "runButton";
                    this.runButton.Image = global::C2.Properties.Resources.run;
                    this.runButton.Enabled = true;//暂时隐去暂停功能
                    this.currentModelRunBackLab.Hide();
                    this.currentModelRunLab.Hide();
                    this.currentModelFinLab.Hide();
                    this.progressBar.Hide();
                    this.progressBarLabel.Hide();
                    EnableRunningControl();
                    break;
            }
        }

        private void UnEnableRunningControl()
        {
            //禁止的控件
            /*
             * 1、当前模型的element
             * 2、右上方菜单栏
             * 3、左侧菜单栏
             */
            Global.GetCurrentModelDocument().UnEnable();
            EnableCommonControl(false);
        }
        private void EnableRunningControl()
        {
            Global.GetCurrentModelDocument().Enable();
            EnableCommonControl(true);
        }
        private void EnableRunningRsControl()
        {
            Global.GetCurrentModelDocument().EnableRs();
            EnableCommonControl(false);
        }
        private void EnableRunningControl(TaskManager manager)
        {
            string error = ControlUtil.Invoke(this,new AsynUpdateMask(delegate ()
            {
                Document.Enable();
                EnableCommonControl(true);
            }));
            if (!string.IsNullOrEmpty(error))
                HelpUtil.ShowMessageBox(error);
        }

        private void EnableCommonControl(bool status)
        {
            this.topToolBarControl.Enabled = status;

            this.operatorControl.Enabled = status;
            Global.GetLeftToolBoxPanel().Enabled = status;
        }
        #endregion


        #region UndoRedo
        private void TopToolBarControl_UndoStackNotEmpty()
        {
            this.topToolBarControl.Enable_UndoButton();
        }

        private void TopToolBarControl_UndoStackEmpty()
        {
            this.topToolBarControl.Disable_UndoButton();
        }

        private void TopToolBarControl_RedoStackNotEmpty()
        {
            this.topToolBarControl.Enable_RedoButton();
        }

        private void TopToolBarControl_RedoStackEmpty()
        {
            this.topToolBarControl.Disable_RedoButton();
        }
        public void Undo()
        {
            this.topToolBarControl.UndoButton_Click(this, EventArgs.Empty);
        }
        public void Redo()
        {
            this.topToolBarControl.RedoButton_Click(this, EventArgs.Empty);
        }
        #endregion

        #region 文档加载
        public void CanvasAddElement(ModelDocument doc)
        {
            doc.ModelElements.ForEach(me => this.canvasPanel.Controls.Add(me.InnerControl));
            this.naviViewControl.UpdateNaviView();
            doc.UpdateAllLines();
        }
        #endregion
        #region 快捷键
        private bool IsCurrentModelNotRun()
        {
            return Document.TaskManager.ModelStatus != ModelStatus.Running;
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (Visible && IsCurrentModelNotRun())
            {
                if (ShortcutKeys.Handle(e.KeyData))
                {
                    e.SuppressKeyPress = true;
                }
            }

            base.OnKeyDown(e);
        }
        private void InitializeKeyBoard()
        {
            ShortcutKeys = new ShortcutKeysTable();
            ShortcutKeys.Register(KeyMap.Undo, delegate () { Undo(); });
            ShortcutKeys.Register(KeyMap.Redo, delegate () { Redo(); });
            ShortcutKeys.Register(KeyMap.Save, delegate () { this.Document.Save(); });
            ShortcutKeys.Register(KeyMap.Copy, delegate () { this.CanvasPanel.ControlSelect_Copy(); });
            ShortcutKeys.Register(KeyMap.Paste, delegate () { this.CanvasPanel.ControlSelect_paste(); });
            ShortcutKeys.Register(KeyMap.Delete, delegate () { this.CanvasPanel.DeleteSelectedLinesByIndex(); });
        }
        #endregion

        public ModelElement AddDocumentOperator(MoveBaseControl ct)
        {
            ct.ID = Document.ElementCount++;
            ModelElement e = ModelElement.CreateModelElement(ct);
            Document.AddModelElement(e);
            return e;
        }

        public void GenMindMapDataSources(Topic topic)
        {
            List<DataItem> dataItems = new List<DataItem>();
            DataSourceWidget dtw = topic.FindWidget<DataSourceWidget>();
            if (dtw != null)
                dataItems = dtw.DataItems;
            this.canvasPanel.AddMindMapDataSource(dataItems);
        }
    }
}
