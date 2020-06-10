using Citta_T1.Business.DataSource;
using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Business.Schedule;
using Citta_T1.Controls;
using Citta_T1.Controls.Flow;
using Citta_T1.Controls.Left;
using Citta_T1.Controls.Move;
using Citta_T1.Controls.Move.Dt;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.Core.UndoRedo;
using Citta_T1.Core.UndoRedo.Command;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Citta_T1
{
    public partial class MainForm : Form
    {
        private bool isBottomViewPanelMinimum;
        private bool isLeftViewPanelMinimum;

        private string userName;
        private Citta_T1.Dialogs.InputDataForm inputDataForm;
        private Citta_T1.Dialogs.CreateNewModelForm createNewModelForm;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));

        private ModelDocumentDao modelDocumentDao;
        private OptionDao optionDao;
        public string UserName { get => this.userName; set => this.userName = value; }

        delegate void AsynUpdateLog(string logContent);
        delegate void AsynUpdateGif();
        delegate void AsynUpdateProgressBar();
        delegate void AsynUpdateMask();
        delegate void AsynUpdateOpErrorMessage();

        private static LogUtil log = LogUtil.GetInstance("MainForm"); // 获取日志模块
        public MainForm(string userName)
        {
            this.UserName = userName;

            InitializeComponent();
            this.inputDataForm = new Dialogs.InputDataForm();
            this.inputDataForm.InputDataEvent += InputDataFormEvent;
            this.createNewModelForm = new Dialogs.CreateNewModelForm();
            this.isBottomViewPanelMinimum = false;
            this.isLeftViewPanelMinimum = false;

            this.modelDocumentDao = new ModelDocumentDao();
            this.optionDao = new OptionDao();

            InitializeGlobalVariable();
            InitializeControlsLocation();
        }

        private void InitializeMainFormEventHandler()
        {
            // 新增文档事件
            this.modelTitlePanel.NewModelDocument += ModelTitlePanel_NewModelDocument;
            this.modelTitlePanel.ModelDocumentSwitch += ModelTitlePanel_DocumentSwitch;
            this.canvasPanel.NewElementEvent += NewDocumentOperator;
            this.remarkControl.RemarkChangeEvent += RemarkChange;
        }
        private void InitializeGlobalVariable()
        {
            Global.SetMainForm(this);
            Global.SetModelTitlePanel(this.modelTitlePanel);
            Global.SetModelDocumentDao(this.modelDocumentDao);
            Global.SetCanvasPanel(this.canvasPanel);
            Global.SetFlowControl(this.flowControl);
            Global.SetMyModelControl(this.myModelControl);
            Global.SetNaviViewControl(this.naviViewControl);
            Global.SetRemarkControl(this.remarkControl);
            Global.SetLogView(this.bottomLogControl);
            Global.SetOptionDao(this.optionDao);
            Global.SetDataSourceControl(this.dataSourceControl);
            Global.SetBottomPythonConsoleControl(this.bottomPyConsole);
            Global.SetTopToolBarControl(this.topToolBarControl);
        }

        private void RemarkChange(RemarkControl rc)
        {
            SetDocumentDirty();
            this.modelDocumentDao.UpdateRemark(rc);
        }

        private void ModelTitlePanel_NewModelDocument(string modelTitle)
        {
            this.modelDocumentDao.AddBlankDocument(modelTitle, this.userName);
        }
        public void SetDocumentDirty()
        {
            // 已经为dirty了，就不需要再操作了，以提高性能
            if (this.modelDocumentDao.CurrentDocument.Dirty)
                return;
            this.modelDocumentDao.CurrentDocument.Dirty = true;
            string currentModelTitle = this.modelDocumentDao.CurrentDocument.ModelTitle;
            this.modelTitlePanel.ResetDirtyPictureBox(currentModelTitle, true);
        }
        public void DeleteCurrentDocument()
        {
            UndoRedoManager.GetInstance().Remove(modelDocumentDao.CurrentDocument);
            List<ModelElement> modelElements = modelDocumentDao.DeleteCurrentDocument();
            modelElements.ForEach(me => canvasPanel.Controls.Remove(me.InnerControl));
            this.naviViewControl.UpdateNaviView();
        }

        private void NewDocumentOperator(MoveBaseControl ct)
        {
            ModelElement me = this.modelDocumentDao.AddDocumentOperator(ct);
            SetDocumentDirty();
            ICommand cmd = new ElementAddCommand(me);
            if (ct is MoveDtControl || ct is MoveOpControl)
                UndoRedoManager.GetInstance().PushCommand(this.modelDocumentDao.CurrentDocument, cmd);
        }

        public void SaveCurrentDocument()
        {
            string modelTitle = this.modelDocumentDao.SaveCurrentDocument();
            if (!this.myModelControl.ContainModel(modelTitle))
                this.myModelControl.AddModel(modelTitle);
        }

        private void SaveAllDocuments()
        {
            string[] modelTitles = this.modelDocumentDao.SaveAllDocuments();
            foreach (string modelTitle in modelTitles)
            {   // 加入左侧我的模型面板
                if (!this.myModelControl.ContainModel(modelTitle))
                    this.myModelControl.AddModel(modelTitle);
                // 清空Dirty标志
                this.modelTitlePanel.ResetDirtyPictureBox(modelTitle, false);
            }
        }

        private void ModelTitlePanel_DocumentSwitch(string modelTitle)
        {
            this.modelDocumentDao.SwitchDocument(modelTitle);
            this.naviViewControl.UpdateNaviView();
            // 切换文档时，需要暂时关闭remark的TextChange事件
            this.remarkControl.RemarkChangeEvent -= RemarkChange;
            this.remarkControl.RemarkDescription = this.modelDocumentDao.RemarkDescription;
            this.remarkControl.RemarkChangeEvent += RemarkChange;
            // 切换文档时, 显示或隐藏备注控件
            if (Global.GetCurrentDocument().RemarkVisible)
                this.remarkControl.Show();
            else
                this.remarkControl.Hide();
            // 切换文档时, 浮动框备注框选中状态切换
            Global.GetFlowControl().SelectRemark = Global.GetCurrentDocument().RemarkVisible;
            Global.GetFlowControl().RemarkChange(Global.GetFlowControl().SelectRemark);
            // 重绘所有Relation线
            this.canvasPanel.Invalidate(false);
            //切换文档时，更新运行按钮图标及进度条
            UpdateRunbuttonImageInfo();
            //切换文档时,更新撤回/重做按钮状态
            UpdateUndoRedoButton();
        }

        private void UpdateUndoRedoButton()
        {
            topToolBarControl.SetUndoButtonEnable(!UndoRedoManager.GetInstance().IsUndoStackEmpty(modelDocumentDao.CurrentDocument));
            topToolBarControl.SetRedoButtonEnable(!UndoRedoManager.GetInstance().IsRedoStackEmpty(modelDocumentDao.CurrentDocument));
        }

        public void LoadDocument(string modelTitle)
        {
            this.modelTitlePanel.AddModel(modelTitle);
            this.modelDocumentDao.CurrentDocument.Load();
            this.modelDocumentDao.CurrentDocument.ReCountDocumentMaxElementID();
            this.modelDocumentDao.CurrentDocument.Show();
            this.modelDocumentDao.CurrentDocument.Dirty = false;
            CanvasAddElement(this.modelDocumentDao.CurrentDocument);
            // 加载文档时，需要暂时关闭remark的TextChange事件
            this.remarkControl.RemarkChangeEvent -= RemarkChange;
            this.remarkControl.RemarkDescription = this.modelDocumentDao.RemarkDescription;
            this.remarkControl.RemarkChangeEvent += RemarkChange;
        }
        private void LoadDocuments()
        {
            if (this.modelDocumentDao.WithoutDocumentLogin(this.userName))
            {
                this.modelTitlePanel.AddModel("我的新模型");
                this.modelDocumentDao.AddBlankDocument("我的新模型", this.userName);
                return;
            }
            // 穷举当前用户空间的所有模型
            string[] modelTitles = this.modelDocumentDao.LoadSaveModelTitle(this.userName);
            // 多文档面板加载控件
            this.modelTitlePanel.LoadModelDocument(modelTitles);
            //加载用户空间的所有模型,并加入到canvas面板中
            foreach (string mt in modelTitles)
            {
                ModelDocument doc = this.modelDocumentDao.LoadDocument(mt, this.userName);
                CanvasAddElement(doc);
            }
            // 将用户本地保存的模型文档加载到左侧myModelControl
            string[] allModelTitle = this.modelDocumentDao.LoadAllModelTitle(this.userName);
            foreach (string modelTitle in allModelTitle)
            {
                this.myModelControl.AddModel(modelTitle);
                if (!modelTitles.Contains(modelTitle))
                    this.myModelControl.EnableClosedDocumentMenu(modelTitle);
            }
            // 显示当前模型
            this.modelDocumentDao.CurrentDocument.Show();
            // 更新当前模型备注信息
            this.remarkControl.RemarkDescription = this.modelDocumentDao.RemarkDescription;
        }
        private void CanvasAddElement(ModelDocument doc)
        {
            doc.ModelElements.ForEach(me => this.canvasPanel.Controls.Add(me.InnerControl));
            this.naviViewControl.UpdateNaviView();
            doc.UpdateAllLines();
        }

        private void InitializeControlsLocation()
        {
            log.Info("画布大小：" + this.canvasPanel.Width.ToString() + "," + this.canvasPanel.Height.ToString());

            Point org = new Point(this.canvasPanel.Width, 0);
            Point org2 = new Point(0, this.canvasPanel.Height);
            int x = org.X - 10 - this.naviViewControl.Width;
            int y = org2.Y - 5 - this.naviViewControl.Height;
            log.Info("缩略图定位：" + x.ToString() + "," + y.ToString());
            log.Info("x：" + x.ToString() + ",y:" + y.ToString());

            // 缩略图定位
            this.naviViewControl.Location = new Point(x, y);
            this.naviViewControl.Invalidate();

            // 底层工具按钮定位
            x = x - (this.canvasPanel.Width) / 2 + 100;
            this.resetButton.Location = new Point(x + 100, y + 50);
            this.stopButton.Location = new Point(x + 50, y + 50);
            this.runButton.Location = new Point(x, y + 50);

            //运行状态动图、进度条定位
            this.currentModelRunBackLab.Location = new Point(x, this.canvasPanel.Height / 2 - 50);
            this.currentModelFinLab.Location = new Point(x, this.canvasPanel.Height / 2 - 50);
            this.progressBar1.Location = new Point(x, this.canvasPanel.Height / 2 + 54);
            this.progressBarLabel.Location = new Point(x + 125, this.canvasPanel.Height / 2 + 50);


            // 顶层浮动工具栏和右侧工具及隐藏按钮定位
            Point loc = new Point(org.X - 70 - this.flowControl.Width, org.Y + 50);
            Point loc_flowcontrol2 = new Point(org.X - this.rightShowButton.Width, loc.Y);
            Point loc_flowcontrol3 = new Point(loc_flowcontrol2.X, loc.Y + this.rightHideButton.Width + 10);
            Point loc_panel3 = new Point(loc.X, loc.Y + this.flowControl.Height + 10);

            this.flowControl.Location = loc;

            this.rightShowButton.Location = loc_flowcontrol2;
            this.rightHideButton.Location = loc_flowcontrol3;

            this.remarkControl.Location = loc_panel3;

            // 右上用户名，头像
            int count = System.Text.RegularExpressions.Regex.Matches(userName, "[a-z0-9]").Count;
            int rightMargin = (this.userName.Length - (count / 3) - 3) * 14;
            this.usernamelabel.Text = this.userName;
            Point userNameLocation = new Point(185, 10);
            this.usernamelabel.Location = new Point(userNameLocation.X + 65 - rightMargin, userNameLocation.Y + 2);
            this.helpPictureBox.Location = new Point(userNameLocation.X - rightMargin, userNameLocation.Y + 1);
            this.portraitpictureBox.Location = new Point(userNameLocation.X + 30 - rightMargin, userNameLocation.Y + 1);

            log.Info("画布大小：" + this.canvasPanel.Width.ToString() + "," + this.canvasPanel.Height.ToString());
        }

        private void MyModelButton_Click(object sender, EventArgs e)
        {
            this.ShowLeftFold();
            this.myModelControl.Visible = true;

            this.dataSourceControl.Visible = false;
            this.operatorControl.Visible = false;
            this.flowChartControl.Visible = false;

        }

        private void OprateButton_Click(object sender, EventArgs e)
        {
            this.ShowLeftFold();

            this.operatorControl.Visible = true;

            this.dataSourceControl.Visible = false;
            this.flowChartControl.Visible = false;
            this.myModelControl.Visible = false;
        }

        private void DataButton_Click(object sender, EventArgs e)
        {
            this.ShowLeftFold();
            this.dataSourceControl.Visible = true;

            this.operatorControl.Visible = false;
            this.flowChartControl.Visible = false;
            this.myModelControl.Visible = false;
        }

        private void FlowChartButton_Click(object sender, EventArgs e)
        {
            this.ShowLeftFold();
            this.flowChartControl.Visible = true;

            this.dataSourceControl.Visible = false;
            this.operatorControl.Visible = false;
            this.myModelControl.Visible = false;

        }


        private void PreviewLabel_Click(object sender, EventArgs e)
        {
            this.ShowBottomPanel();
            this.ShowBottomPreview();
        }

        private void ShowBottomPreview()
        {
            this.bottomLogControl.Visible = false;
            this.bottomPyConsole.Visible = false;
            this.bottomPreview.Visible = true;
        }

        private void PyControlLabel_Click(object sender, EventArgs e)
        {
            this.ShowBottomPanel();
            this.ShowPyConsole();
        }

        private void ShowPyConsole()
        {
            this.bottomPyConsole.Visible = true;
            this.bottomLogControl.Visible = false;
            this.bottomPreview.Visible = false;
        }

        private void LogLabel_Click(object sender, EventArgs e)
        {
            this.ShowBottomPanel();
            this.ShowLogView();
        }

        private void ShowLogView()
        {
            this.bottomLogControl.Visible = true;
            this.bottomPyConsole.Visible = false;
            this.bottomPreview.Visible = false;
        }

        private void ShowBottomPanel()
        {
            if (this.isBottomViewPanelMinimum == true)
            {
                this.isBottomViewPanelMinimum = false;
                this.bottomViewPanel.Height = 280;
                this.minMaxPictureBox.Image = Image.FromFile(Application.StartupPath + "\\res\\displaypanel\\minfold.png");
            }
            InitializeControlsLocation();
            if (bottomViewPanel.Height == 280)
            {
                this.toolTip1.SetToolTip(this.minMaxPictureBox, "隐藏底层面板");
            }
            if (bottomViewPanel.Height == 40)
            {
                this.toolTip1.SetToolTip(this.minMaxPictureBox, "展开底层面板");
            }
        }

        private void MinMaxPictureBox_Click(object sender, EventArgs e)
        {
            log.Info("MinMaxPictureBox_Click");
            if (this.isBottomViewPanelMinimum == true)
            {
                this.isBottomViewPanelMinimum = false;
                this.bottomViewPanel.Height = 280;
                this.minMaxPictureBox.Image = Image.FromFile(Application.StartupPath + "\\res\\displaypanel\\minfold.png");
            }
            else
            {
                this.isBottomViewPanelMinimum = true;
                this.bottomViewPanel.Height = 40;
                this.minMaxPictureBox.Image = Image.FromFile(Application.StartupPath + "\\res\\displaypanel\\maxunfold.png");
            }
            // TODO [DK] BUG 这里是因为CanvasPanel设置了Dock属性，在this.bottomViewPanel.Height变化的时候，CanvasPanel的Height也变了，因此控件位置发生了改变，但是线并没有变
            InitializeControlsLocation();
            if (bottomViewPanel.Height == 280)
            {
                this.toolTip1.SetToolTip(this.minMaxPictureBox, "隐藏底层面板");
            }
            if (bottomViewPanel.Height == 40)
            {
                this.toolTip1.SetToolTip(this.minMaxPictureBox, "展开底层面板");
            }
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            InitializeControlsLocation();
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            this.inputDataForm.StartPosition = FormStartPosition.CenterScreen;
            this.inputDataForm.ShowDialog();
            this.inputDataForm.ReSetParams();
        }



        private void NewModelButton_Click(object sender, EventArgs e)
        {
            this.createNewModelForm.StartPosition = FormStartPosition.CenterScreen;
            this.createNewModelForm.Owner = this;
            DialogResult dialogResult = this.createNewModelForm.ShowDialog();

            // 模型标题栏添加新标题
            if (dialogResult == DialogResult.OK)
                this.modelTitlePanel.AddModel(this.createNewModelForm.ModelTitle);
        }

        private void InputDataFormEvent(string name, string fullFilePath, char separator, OpUtil.ExtType extType, OpUtil.Encoding encoding)
        {
            this.dataSourceControl.GenDataButton(name, fullFilePath, separator, extType, encoding);
            this.dataSourceControl.Visible = true;
            this.operatorControl.Visible = false;
            this.flowChartControl.Visible = false;
        }

        public void PreViewDataByFullFilePath(string fullFilePath, char separator, OpUtil.ExtType extType, OpUtil.Encoding encoding, bool isForceRead = false)
        {
            if (!System.IO.File.Exists(fullFilePath))
                return;
            this.ShowBottomPanel();
            this.bottomPreview.PreViewDataByFullFilePath(fullFilePath, separator, extType, encoding, isForceRead);
            this.ShowBottomPreview();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //加载文件及数据源
            LoadDocuments();
            LoadDataSource();
            InitializeMainFormEventHandler();

        }
        private void LoadDataSource()
        {
            DataSourceInfo dataSource = new DataSourceInfo(this.userName);
            List<DataButton> dataButtons = dataSource.LoadDataSourceInfo();
            foreach (DataButton dataButton in dataButtons)
                this.dataSourceControl.GenDataButton(dataButton);
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            TaskManager currentManager = Global.GetCurrentDocument().TaskManager;

            //在模型运行完成，及终止的情况下，可以重置
            Console.WriteLine(currentManager.ModelStatus.ToString());
            if (currentManager.ModelStatus != ModelStatus.GifDone && currentManager.ModelStatus != ModelStatus.Pause && currentManager.ModelStatus != ModelStatus.Running)
            {
                currentManager.GetCurrentModelTripleList(Global.GetCurrentDocument());
                currentManager.Reset();
                //SetDocumentDirty();//需不需要dirty
                MessageBox.Show("当前模型的运算结果已重置，点击‘运行’可以重新运算了", "已重置", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void StopButton_Click(object sender, EventArgs e)
        {

            if (this.runButton.Name == "pauseButton" || this.runButton.Name == "continueButton")
            {
                Global.GetCurrentDocument().TaskManager.Stop();
                UpdateRunbuttonImageInfo();
            }
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            TaskManager currentManager = Global.GetCurrentDocument().TaskManager;
            BindUiManagerFunc();

            if (this.runButton.Name == "runButton")
            {
                if (Global.GetCurrentDocument().Dirty)
                {
                    MessageBox.Show("当前模型没有保存，请保存后再运行模型", "保存", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                currentManager.GetCurrentModelTripleList(Global.GetCurrentDocument());
                int notReadyNum = currentManager.TripleList.CountOpStatus(ElementStatus.Null);
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
                int taskNum = currentManager.TripleList.CountOpStatus(ElementStatus.Ready);
                this.progressBar1.Step = taskNum > 0 ? 100 / taskNum : 100;

                this.progressBar1.Value = 0;
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

        public void SetCanvasEnable(bool status)
        {
            foreach (Control c in Global.GetCanvasPanel().Controls)
            {
                //log.Info("暂停该控件：" + c.Name);
                if (c.Name == "MoveRsControl" || c.Name == "MoveOpControl")
                {
                    c.Enabled = status;
                }
            }
        }

        public void BindUiManagerFunc()
        {
            TaskManager currentManager = Global.GetCurrentDocument().TaskManager;
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
                ModelDocument model = Global.GetModelDocumentDao().GetManagerRelateModel(manager);
                MoveOpControl op = model.SearchElementByID(id).InnerControl as MoveOpControl;
                op.SetStatusBoxErrorContent(error);
            }));
        }

        //更新进度条
        private void UpdateProgressBar(TaskManager manager)
        {
            ModelDocument doneModel = Global.GetModelDocumentDao().GetManagerRelateModel(manager);
            if (doneModel != Global.GetCurrentDocument())
                return;


            if (manager.ModelStatus == ModelStatus.Running)
                this.Invoke(new AsynUpdateProgressBar(delegate ()
                {
                    this.progressBar1.Value = manager.CurrentModelTripleStatusNum(ElementStatus.Done) * 100 / manager.TripleList.CurrentModelTripleList.Count;
                    this.progressBarLabel.Text = this.progressBar1.Value.ToString() + "%";
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
            ModelDocument doneModel = Global.GetModelDocumentDao().GetManagerRelateModel(manager);
            if (doneModel != Global.GetCurrentDocument())
                return;

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
                    this.progressBar1.Hide();
                    this.progressBarLabel.Hide();
                    this.currentModelFinLab.Hide();
                }));

        }

        //完成任务时需要调用
        private void Accomplish(TaskManager manager)
        {
            ModelDocument doneModel = Global.GetModelDocumentDao().GetManagerRelateModel(manager);
            if (doneModel.TaskManager.ModelStatus == ModelStatus.Done)
            {
                doneModel.Save();
            }

            if (doneModel == Global.GetCurrentDocument())
            {
                UpdateRunbuttonImageInfo();
            }
        }

        public void UpdateRunbuttonImageInfo()
        {
            TaskManager manager = Global.GetCurrentDocument().TaskManager;
            switch (manager.ModelStatus)
            {
                //点击暂停按钮，均隐藏
                case ModelStatus.Pause:
                    this.runButton.Name = "continueButton";
                    this.runButton.Image = global::Citta_T1.Properties.Resources.continual;
                    this.currentModelRunBackLab.Hide();
                    this.currentModelRunLab.Hide();
                    this.progressBar1.Hide();
                    this.progressBarLabel.Hide();
                    EnableRunningRsControl();
                    break;
                //点击运行按钮
                case ModelStatus.Running:
                    this.runButton.Name = "pauseButton";
                    this.runButton.Image = global::Citta_T1.Properties.Resources.pause;
                    this.currentModelRunBackLab.Show();
                    this.currentModelRunLab.Show();
                    this.progressBar1.Show();
                    this.progressBarLabel.Show();
                    this.progressBar1.Value = manager.CurrentModelTripleStatusNum(ElementStatus.Done) * 100 / manager.TripleList.CurrentModelTripleList.Count;
                    UnEnableRunningControl();
                    break;
                case ModelStatus.GifDone:
                    this.runButton.Name = "runButton";
                    this.runButton.Image = global::Citta_T1.Properties.Resources.run;
                    break;
                default:
                    this.runButton.Name = "runButton";
                    this.runButton.Image = global::Citta_T1.Properties.Resources.run;
                    this.currentModelRunBackLab.Hide();
                    this.currentModelRunLab.Hide();
                    this.currentModelFinLab.Hide();
                    this.progressBar1.Hide();
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
            Global.GetCurrentDocument().UnEnable();
            EnableCommonControl(false);
        }
        private void EnableRunningControl()
        {
            Global.GetCurrentDocument().Enable();
            EnableCommonControl(true);
        }
        private void EnableRunningRsControl()
        {
            Global.GetCurrentDocument().EnableRs();
            EnableCommonControl(false);
        }
        private void EnableRunningControl(TaskManager manager)
        {
            ModelDocument doneModel = Global.GetModelDocumentDao().GetManagerRelateModel(manager);
            this.Invoke(new AsynUpdateMask(delegate ()
            {
                doneModel.Enable();
                EnableCommonControl(true);
            }));
        }

        private void EnableCommonControl(bool status)
        {
            this.topToolBarControl.Enabled = status;
            this.panel5.Enabled = status;
            this.leftToolBoxPanel.Enabled = status;
            this.flowControl.Enabled = status;
        }

        private void ShowLeftFold()
        {
            if (this.isLeftViewPanelMinimum)
            {
                this.isLeftViewPanelMinimum = false;
                this.leftToolBoxPanel.Width = 187;
                this.toolTip1.SetToolTip(this.leftFoldButton, "隐藏左侧面板");
            }
            InitializeControlsLocation();
        }
        private void LeftFoldButton_Click(object sender, EventArgs e)
        {
            if (this.isLeftViewPanelMinimum)
            {
                this.isLeftViewPanelMinimum = false;
                this.leftToolBoxPanel.Width = 187;
                this.toolTip1.SetToolTip(this.leftFoldButton, "隐藏左侧面板");
            }
            else
            {
                this.isLeftViewPanelMinimum = true;
                this.leftToolBoxPanel.Width = 10;
                this.toolTip1.SetToolTip(this.leftFoldButton, "展开左侧面板");
            }

            InitializeControlsLocation();
        }

        private void HelpPictureBox_Click(object sender, EventArgs e)
        {
            string helpfile = Application.StartupPath;
            helpfile += @"\Doc\citta帮助文档.chm";
            Help.ShowHelp(this, helpfile);
        }

        private void SaveModelButton_Click(object sender, EventArgs e)
        {
            // 如果文档不dirty的情况下, 对于大文档, 不做重复保存,以提高性能
            if (!this.modelDocumentDao.CurrentDocument.Dirty)
                if (this.modelDocumentDao.CurrentDocument.ModelElements.Count > 10)
                    return;

            string currentModelTitle = this.modelDocumentDao.CurrentDocument.ModelTitle;
            this.modelDocumentDao.UpdateRemark(this.remarkControl);
            this.modelTitlePanel.ResetDirtyPictureBox(currentModelTitle, false);
            SaveCurrentDocument();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.modelDocumentDao.SaveEndDocuments(this.userName);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (ModelDocument md in this.modelDocumentDao.ModelDocuments)
            {
                if (md.Dirty)
                {
                    MessageBox.Show("有未保存的文件!", "保存", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void UsernameLabel_MouseEnter(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(this.usernamelabel, this.userName + "已登录");
        }

        public void BlankButtonFocus()
        {
            this.blankButton.Focus();
        }
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData) //激活回车键
        {
            int WM_KEYDOWN = 256;
            int WM_SYSKEYDOWN = 260;

            if (msg.Msg == WM_KEYDOWN | msg.Msg == WM_SYSKEYDOWN)
            {
                if (keyData == Keys.Delete)
                    this.canvasPanel.DeleteSelectedLinesByIndex();
                if (keyData == (Keys.C | Keys.Control))
                    this.canvasPanel.ControlSelect_Copy();
                if (keyData == (Keys.V | Keys.Control))
                    this.canvasPanel.ControlSelect_paste();
            }
            return false;
        }

        private void SaveAllButton_Click(object sender, EventArgs e)
        {
            SaveAllDocuments();
        }
    }
}
