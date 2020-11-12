using C2.Business.Model;
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
using C2.Model;
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
        private OptionDao optionDao;
        private UndoRedoManager undoRedoManager;
        private ModelDocument document;
        private readonly string userInfoPath = Path.Combine(Global.WorkspaceDirectory, "UserInformation.xml");
        private string userName;
        public CanvasPanel CanvasPanel{ get { return this.canvasPanel; }}
        public RemarkControl RemarkControl { get { return this.remarkControl; } }
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
            InitializeMainFormEventHandler();
            InitializeControlsLocation();
            InitializeUndoRedoManager();
        }

        private void InitializeDao()
        {
            this.optionDao = new OptionDao();
            this.userName = Global.GetMainForm().UserName;
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

        public CanvasForm(ModelDocument document)
            :this()
        {
            Document = document;
        }

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
        private void InitializeMainFormEventHandler()
        {
            // 新增文档事件
            this.canvasPanel.NewElementEvent += NewDocumentOperator;
        }
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
        private void CanvasForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Document.Save();
        }

        private void CanvasForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!HardClose && Document != null && Document.Modified && !ReadOnly)
            {
                DialogResult result = MessageBox.Show("有尚未保存的模型，是否保存后关闭？",
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
                    Document.Save();
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
            // TODO DK 文档另存为
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
        public void InitializeControlsLocation()
        {
            int x = this.canvasPanel.Width - 10 - this.naviViewControl.Width;
            int y = this.canvasPanel.Height - 5 - this.naviViewControl.Height;

            // 缩略图定位
            this.naviViewControl.Location = new Point(x, y+30);
            this.naviViewControl.Invalidate();

            // 底层工具按钮定位
            x = x - (this.canvasPanel.Width) / 2 + 100;
            this.resetButton.Location = new Point(x + 100, y + 83);
            this.stopButton.Location = new Point(x + 50, y + 83);
            this.runButton.Location = new Point(x, y + 83);

            //运行状态动图、进度条定位
            this.currentModelRunBackLab.Location = new Point(x, this.canvasPanel.Height / 2 - 50);
            this.currentModelFinLab.Location = new Point(x, this.canvasPanel.Height / 2 - 50);
            this.progressBar.Location = new Point(x, this.canvasPanel.Height / 2 + 54);
            this.progressBarLabel.Location = new Point(x + 125, this.canvasPanel.Height / 2 + 50);

            // 顶层浮动工具栏和右侧工具及隐藏按钮定位
            this.operatorControl.Location = new Point(this.canvasPanel.Width -280 , 38);
            this.remarkControl.Location = new Point(this.canvasPanel.Width - 505 , 38);
            this.rightHideButton.Location = new Point(this.canvasPanel.Width - 60, 38);
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
                MessageBox.Show("当前模型的运算结果已重置，点击‘运行’可以重新运算了", "已重置", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (Global.GetCurrentModelDocument().Modified)
                {
                    MessageBox.Show("当前模型没有保存，请保存后再运行模型", "保存", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                currentManager.GetCurrentModelTripleList(Global.GetCurrentModelDocument(), "all");
                //int notReadyNum = currentManager.CountOpStatus(ElementStatus.Null);
                int notReadyNum = currentManager.CountOpNullAndNoRelation();
                if (notReadyNum > 0)
                {
                    MessageBox.Show("有" + notReadyNum + "个未配置的算子，请配置后再运行模型", "未配置", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (currentManager.IsAllOperatorDone())
                {
                    MessageBox.Show("当前模型的算子均已运算完毕，重新运算需要先点击‘重置’按钮。", "运算完毕", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                currentManager.Start();
                int taskNum = currentManager.CountOpStatus(ElementStatus.Ready);
                this.progressBar.Step = taskNum > 0 ? 100 / taskNum : 100;

                this.progressBar.Value = 0;
                this.progressBarLabel.Text = "0%";
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
            this.Invoke(new AsynUpdateOpErrorMessage(delegate ()
            {
                MoveOpControl op = Document.SearchElementByID(id).InnerControl as MoveOpControl;
                op.SetStatusBoxErrorContent(error);
            }));
        }

        //更新进度条
        private void UpdateProgressBar(TaskManager manager)
        {
            if (manager.ModelStatus == ModelStatus.Running)
                this.Invoke(new AsynUpdateProgressBar(delegate ()
                {
                    this.progressBar.Value = manager.CurrentModelTripleStatusNum(ElementStatus.Done) * 100 / manager.TripleListGen.CurrentModelTripleList.Count;
                    this.progressBarLabel.Text = this.progressBar.Value.ToString() + "%";
                }));
        }


        //更新log
        private void UpdateLogStatus(string logContent)
        {
            this.Invoke(new AsynUpdateLog(delegate (string tlog)
            {
                log.Info(tlog);
            }), logContent);
        }


        private void UpdateRunningGif(TaskManager manager)
        {
            if (manager.ModelStatus == ModelStatus.GifDone)
                this.Invoke(new AsynUpdateGif(delegate ()
                {
                    this.currentModelRunBackLab.Hide();
                    this.currentModelRunLab.Hide();
                    this.currentModelFinLab.Show();
                }));
            else if (manager.ModelStatus == ModelStatus.Done)
                this.Invoke(new AsynUpdateGif(delegate ()
                {
                    this.progressBar.Hide();
                    this.progressBarLabel.Hide();
                    this.currentModelFinLab.Hide();
                }));

        }

        //完成任务时需要调用
        private void Accomplish(TaskManager manager)
        {
            Document.Save();
            this.Invoke(new TaskCallBack(delegate ()
            {
                UpdateRunbuttonImageInfo();
            }));
        }

        //更新状态的节点：1、当前模型开始、终止、运行完成；2、切换文档
        public void UpdateRunbuttonImageInfo()
        {
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
            this.Invoke(new AsynUpdateMask(delegate ()
            {
                Document.Enable();
                EnableCommonControl(true);
            }));
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
        #endregion

        #region 文档加载
        public void LoadDocument(string modelTitle)
        {
            // TODO 就不该有多个doc
            CanvasRemoveAllEle();
            ModelDocument modelDoc = new ModelDocument(modelTitle, userName);
            this.Document = modelDoc;
            this.Document.Load();
            this.CanvasAddElement(modelDoc);
        }
        private void CanvasRemoveAllEle()
        {
            this.canvasPanel.Controls.Clear();
            this.naviViewControl.UpdateNaviView();
        }
        private void CanvasAddElement(ModelDocument doc)
        {
            doc.ModelElements.ForEach(me => this.canvasPanel.Controls.Add(me.InnerControl));
            this.naviViewControl.UpdateNaviView();
            doc.UpdateAllLines();
        }
        #endregion

        public ModelElement AddDocumentOperator(MoveBaseControl ct)
        {
            ct.ID = Document.ElementCount++;
            ModelElement e = ModelElement.CreateModelElement(ct);
            Document.AddModelElement(e);
            return e;
        }

        public void GenMindMapDataSources(List<DataItem> dataItems)
        {
            this.canvasPanel.AddMindMapDataSource(dataItems);
        }
    }
}
