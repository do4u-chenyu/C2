using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Citta_T1.Utils;
using Citta_T1.Controls.Title;
using Citta_T1.Controls.Flow;
using Citta_T1.Business.Model;
using Citta_T1.Controls.Move;
using Citta_T1.Controls.Left;
using Citta_T1.Business.DataSource;
using Citta_T1.Business.Schedule;
using System.Threading;
using Citta_T1.Business.Option;

namespace  Citta_T1
{ 
    public partial class MainForm : Form
    {
        public Dictionary<string, Citta_T1.Data> contents = new Dictionary<string, Citta_T1.Data>();
        private bool isBottomViewPanelMinimum;
        private bool isLeftViewPanelMinimum;
        
        private string userName;
        public Citta_T1.Dialogs.FormInputData formInputData;
        private Citta_T1.Dialogs.CreateNewModel createNewModel;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));

        private ModelDocumentDao modelDocumentDao;
        private OptionDao optionDao;
        public string UserName { get => this.userName; set => this.userName = value; }
        public bool IsBottomViewPanelMinimum { get => isBottomViewPanelMinimum; set => isBottomViewPanelMinimum = value; }
        
        delegate void AsynUpdateLog(string log);
        delegate void AsynUpdateGif();

        LogUtil log = LogUtil.GetInstance("MainForm"); // 获取日志模块
        public MainForm()
        {
            this.formInputData = new Citta_T1.Dialogs.FormInputData();
            this.formInputData.InputDataEvent += frm_InputDataEvent;
            this.createNewModel = new Citta_T1.Dialogs.CreateNewModel();
            InitializeComponent();
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
            Global.SetLogView(this.logView);
            Global.SetOptionDao(this.optionDao); 

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
            ModelTitleControl mtc = Utils.ControlUtil.FindMTCByName(currentModelTitle, this.modelTitlePanel);
            mtc.SetDirtyPictureBox();
           
           
        }
        internal void DeleteCurrentDocument()
        {
            
            List<ModelElement> modelElements = modelDocumentDao.DeleteCurrentDocument();
            foreach (ModelElement me in modelElements)
            {
                this.canvasPanel.Controls.Remove(me.GetControl);
            }
            this.naviViewControl.UpdateNaviView();
  
        }

        private void NewDocumentOperator(Control ct)
        {
            SetDocumentDirty();
            this.modelDocumentDao.AddDocumentOperator(ct);

        }
        public void DeleteDocumentElement(Control ct)
        {
            SetDocumentDirty();
            this.modelDocumentDao.CurrentDocument.DeleteModelElement(ct);
        }


        public void SaveDocument()
        {
            string modelTitle = this.modelDocumentDao.SaveDocument();
            if (!this.myModelControl.ContainModel(modelTitle))
                this.myModelControl.AddModel(modelTitle);
        }
        internal List<ModelDocument>  DocumentsList()
        {            
            return modelDocumentDao.ModelDocuments;

        } 
        private void ModelTitlePanel_DocumentSwitch(string modelTitle)
        {
            this.modelDocumentDao.SwitchDocument(modelTitle);
            this.naviViewControl.UpdateNaviView();
            // 切换文档时，需要暂时关闭remark的TextChange事件
            this.remarkControl.RemarkChangeEvent -= RemarkChange;
            this.remarkControl.RemarkText = this.modelDocumentDao.GetRemark();
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
            //切换文档时，更新运行按钮图标
            UpdateRunbuttonImageInfo(this.modelDocumentDao.CurrentDocument.Manager.ModelStatus);
        }

        public void LoadDocument(string modelTitle)
        {
            this.modelTitlePanel.AddModel(modelTitle);
            this.modelDocumentDao.CurrentDocument.Load();
            this.modelDocumentDao.CurrentDocument.DocumentElementCount();
            this.modelDocumentDao.CurrentDocument.Show();
            this.modelDocumentDao.CurrentDocument.Dirty = false;
            CanvasAddElement(this.modelDocumentDao.CurrentDocument);
            // 加载文档时，需要暂时关闭remark的TextChange事件
            this.remarkControl.RemarkChangeEvent -= RemarkChange;
            this.remarkControl.RemarkText = this.modelDocumentDao.GetRemark();
            this.remarkControl.RemarkChangeEvent += RemarkChange;

        }
        private void LoadDocuments(string userName)
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
                    this.myModelControl.EnableOpenDocument(modelTitle);
            }   
            // 显示当前模型
            this.modelDocumentDao.CurrentDocument.Show();
            // 更新当前模型备注信息
            this.remarkControl.RemarkText = this.modelDocumentDao.GetRemark();
        }
        private void CanvasAddElement(ModelDocument doc)
        {
            foreach (ModelElement me in doc.ModelElements)
            {
                Control ct = me.GetControl;
                if (ct is RemarkControl)
                    continue;
                this.canvasPanel.Controls.Add(ct);
                this.naviViewControl.UpdateNaviView();
            }
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

            // 缩略图定位
            this.naviViewControl.Location = new Point(x, y);
            this.naviViewControl.Invalidate();

            // 底层工具按钮定位
            x = x - (this.canvasPanel.Width) / 2 + 100;
            this.downloadButton.Location = new Point(x + 100, y + 50);
            this.stopButton.Location = new Point(x + 50, y + 50);
            this.runButton.Location      = new Point(x, y + 50);

            // 顶层浮动工具栏和右侧工具及隐藏按钮定位
            Point loc = new Point(org.X - 70 - this.flowControl.Width, org.Y + 50);
            Point loc_flowcontrol2 = new Point(org.X - this.rightShowButton.Width, loc.Y);
            Point loc_flowcontrol3 = new Point(loc_flowcontrol2.X, loc.Y + this.rightHideButton.Width + 10);
            Point loc_panel3 = new Point(loc.X, loc.Y + this.flowControl.Height + 10);

            this.flowControl.Location = loc;

            this.rightShowButton.Location = loc_flowcontrol2;
            this.rightHideButton.Location = loc_flowcontrol3;

            this.remarkControl.Location = loc_panel3;

            log.Info("画布大小：" + this.canvasPanel.Width.ToString() + "," + this.canvasPanel.Height.ToString());
        }

        private void MyModelButton_Click(object sender, EventArgs e)
        {
            this.myModelControl.Visible = true;
            this.dataSourceControl.Visible = false;
            this.operatorControl.Visible = false;
            this.flowChartControl.Visible = false;

        }

        private void OprateButton_Click(object sender, EventArgs e)
        {
            this.operatorControl.Visible = true;

            this.dataSourceControl.Visible = false;
            this.flowChartControl.Visible = false;
            this.myModelControl.Visible = false;
        }

        private void DataButton_Click(object sender, EventArgs e)
        {
            this.dataSourceControl.Visible = true;

            this.operatorControl.Visible = false;
            this.flowChartControl.Visible = false;
            this.myModelControl.Visible = false;
        }

        private void FlowChartButton_Click(object sender, EventArgs e)
        {
            this.flowChartControl.Visible = true;

            this.dataSourceControl.Visible = false;
            this.operatorControl.Visible = false;
            this.myModelControl.Visible = false;

        }

        //private void NewModelButton_Click(object sender, EventArgs e)
        //{
        //    this.anewModel.StartPosition = FormStartPosition.CenterParent;
        //    this.anewModel.ShowDialog();
        //}

        private void PreviewLabel_Click(object sender, EventArgs e)
        {
            this.logView.Visible = false;
            this.dataGridView2.Visible = false;
            this.dataGridView3.Visible = true;
        }

        private void ErrorLabel_Click(object sender, EventArgs e)
        {
            this.logView.Visible = false;
            this.dataGridView2.Visible = true;
            this.dataGridView3.Visible = false;
        }

        private void LogLabel_Click(object sender, EventArgs e)
        {
            this.logView.Visible = true;
            this.dataGridView2.Visible = false;
            this.dataGridView3.Visible = false;
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
            else {
                this.isBottomViewPanelMinimum = true;
                this.bottomViewPanel.Height = 40;
                this.minMaxPictureBox.Image = Image.FromFile(Application.StartupPath + "\\res\\displaypanel\\maxunfold.png");
            }
            InitializeControlsLocation();
            if (bottomViewPanel.Height == 280)
            {
                this.toolTip1.SetToolTip(this.minMaxPictureBox, "隐藏数据框");
            }
            if (bottomViewPanel.Height == 40)
            {
                this.toolTip1.SetToolTip(this.minMaxPictureBox, "显示数据框");
            }
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            InitializeControlsLocation();
        }


        private void ConnectOpButton_Click(object sender, EventArgs e)
        {

        }

        private void InterOpButton_Click(object sender, EventArgs e)
        {

        }

        private void UnionButton_Click(object sender, EventArgs e)
        {

        }

        private void DiffButton_Click(object sender, EventArgs e)
        {

        }

        private void FilterButton_Click(object sender, EventArgs e)
        {

        }

        private void GroupButton_Click(object sender, EventArgs e)
        {

        }

        private void HistogramButton_Click(object sender, EventArgs e)
        {

        }

        private void FormatButton_Click(object sender, EventArgs e)
        {

        }

        private void MoreButton_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_Load(object sender, EventArgs e)
        {

        }
        private void dataGridView2_Load(object sender, EventArgs e)
        {

        }


        private void ImportButton_Click(object sender, EventArgs e)
        {
            this.formInputData.StartPosition = FormStartPosition.CenterScreen;
            this.formInputData.ShowDialog();
        }


       // NewOperatorEvent?.Invoke(btn);
          

        private void NewModelButton_Click(object sender, EventArgs e)
        {
            this.createNewModel.StartPosition = FormStartPosition.CenterScreen;
            this.createNewModel.Owner = this;
            DialogResult dialogResult = this.createNewModel.ShowDialog();
            
            // 模型标题栏添加新标题
            if (dialogResult == DialogResult.OK)
                this.modelTitlePanel.AddModel(this.createNewModel.ModelTitle);
        }

        void frm_InputDataEvent(string name, string filePath, DSUtil.ExtType extType, DSUtil.Encoding encoding)
        {
            // `FormInputData`中的数据添加处理方式，同一个数据不可多次导入
            // TODO [DK] 读取Excel
            this.dataSourceControl.GenDataButton(name, filePath, extType, encoding);
            this.dataSourceControl.Visible = true;
            this.operatorControl.Visible = false;
            this.flowChartControl.Visible = false;
        }

        public void PreViewDataByBcpPath(string bcpPath, DSUtil.ExtType extType, DSUtil.Encoding encoding)
        {
            this.dataGridView3.PreViewDataByBcpPath(bcpPath, extType = extType, encoding = encoding);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            int count = System.Text.RegularExpressions.Regex.Matches(userName, "[a-z0-9]").Count;
            int rightMargin = (this.userName.Length - (count / 3) - 3) * 14;
            this.usernamelabel.Text = this.userName;
            Point userNameLocation = new Point(185,10);
            this.usernamelabel.Location = new Point(userNameLocation.X + 65 - rightMargin, userNameLocation.Y + 2);
            this.helpPictureBox.Location = new Point(userNameLocation.X - rightMargin, userNameLocation.Y);
            this.portraitpictureBox.Location = new Point(userNameLocation.X + 30 - rightMargin, userNameLocation.Y + 1);
            //加载文件及数据源
            LoadDocuments(this.userName);
            LoadDataSource(this.userName);
            InitializeMainFormEventHandler();

        }
        private void LoadDataSource(string userName)
        {
            DataSourceInfo dataSource = new DataSourceInfo(userName);
            List<DataButton> dataButtons = dataSource.LoadDataSourceInfo();
            foreach (DataButton dataButton in dataButtons)
                this.dataSourceControl.GenDataButton(dataButton);
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            
            if (this.runButton.Name == "pauseButton" || this.runButton.Name == "continueButton")
            {
                Global.GetCurrentDocument().Manager.Stop();
                UpdateRunbuttonImageInfo(Global.GetCurrentDocument().Manager.ModelStatus);
            }
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            Manager currentManager = Global.GetCurrentDocument().Manager;

            //初次运行时，绑定线程与ui交互的委托
            if (currentManager.ModelStatus == ModelStatus.Null)
            {
                currentManager.UpdateLogDelegate = UpdataLogStatus;
                currentManager.TaskCallBack = Accomplish;
                currentManager.UpdateGifDelegate = UpdataRunningGif;
            }

            if (this.runButton.Name == "runButton")
            {
                if (Global.GetCurrentDocument().Dirty)
                {
                    MessageBox.Show("当前模型没有保存，请保存后再运行模型", "保存", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                currentManager.GetCurrentModelTripleList(Global.GetCurrentDocument());
                int notReadyNum = currentManager.TripleList.AllOperatorNotReadyNum();
                if (notReadyNum > 0)
                {
                    MessageBox.Show("有" + notReadyNum + "个未配置的算子，请配置后再运行模型", "未配置", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                currentManager.Start();
            }
            else if (this.runButton.Name == "pauseButton")
            {
                currentManager.Pause();
            }
            else if (this.runButton.Name == "continueButton")
            {
                currentManager.Continue();
            }

            UpdateRunbuttonImageInfo(currentManager.ModelStatus);
        }


        //更新log
        private void UpdataLogStatus(string log)
        {
            if (InvokeRequired)
            {
                this.Invoke(new AsynUpdateLog(delegate (string tlog)
                {
                    this.log.Info(tlog);
                }), log);
            }
            else
            {
                this.log.Info(log);
            }
        }


        private void UpdataRunningGif(Manager manager)
        {
            ModelDocument doneModel = Global.GetModelDocumentDao().GetManagerRelateModel(manager);
            if (doneModel == Global.GetCurrentDocument())
            {
                if (manager.ModelStatus == ModelStatus.GifDone)
                {
                    if (InvokeRequired)
                    {
                        this.Invoke(new AsynUpdateGif(delegate ()
                        {
                            this.currentModelRunBackLab.Hide();
                            this.currentModelRunLab.Hide();
                            this.currentModelFinLab.Show();

                        }));
                    }
                    else
                    {
                        this.currentModelRunBackLab.Hide();
                        this.currentModelRunLab.Hide();
                        this.currentModelFinLab.Show();
                    }
                }
                else if (manager.ModelStatus == ModelStatus.Done)
                {
                    if (InvokeRequired)
                    {
                        this.Invoke(new AsynUpdateGif(delegate ()
                        {
                            this.currentModelFinLab.Hide();
                        }));
                    }
                    else
                    {
                        this.currentModelFinLab.Hide();
                    }
                }

            }
        }



        //完成任务时需要调用
        private void Accomplish(Manager manager)
        {
            ModelDocument doneModel = Global.GetModelDocumentDao().GetManagerRelateModel(manager);
            doneModel.Save();

            if (doneModel == Global.GetCurrentDocument())
            {
                UpdateRunbuttonImageInfo(doneModel.Manager.ModelStatus);

            }
        }



        private void UpdateRunbuttonImageInfo(ModelStatus modelStatus)
        {
            switch (modelStatus)
            {
                case ModelStatus.Pause:
                    this.runButton.Name = "continueButton";
                    this.runButton.Image = ((System.Drawing.Image)resources.GetObject("runButton.Image"));
                    this.currentModelRunBackLab.Hide();
                    this.currentModelRunLab.Hide();
                    break;
                case ModelStatus.Running:
                    this.runButton.Name = "pauseButton";
                    this.runButton.Image = global::Citta_T1.Properties.Resources.pause;
                    this.currentModelRunBackLab.Show();
                    this.currentModelRunLab.Show();
                    break;
                case ModelStatus.GifDone:
                    this.runButton.Name = "runButton";
                    this.runButton.Image = ((System.Drawing.Image)resources.GetObject("runButton.Image"));
                    break;
                default:
                    this.runButton.Name = "runButton";
                    this.runButton.Image = ((System.Drawing.Image)resources.GetObject("runButton.Image"));
                    this.currentModelRunBackLab.Hide();
                    this.currentModelRunLab.Hide();
                    this.currentModelFinLab.Hide();
                    break;
            }
        }

        private void LeftFoldButton_Click(object sender, EventArgs e)
        {
            if (this.isLeftViewPanelMinimum == true)
            {
                this.isLeftViewPanelMinimum = false;
                this.leftToolBoxPanel.Width = 187;

            }
            else
            {
                this.isLeftViewPanelMinimum = true;
                this.leftToolBoxPanel.Width = 10;
            }
            InitializeControlsLocation();
        }

        public void RenameDataButton(string index, string dstName)
        {
            this.dataSourceControl.RenameDataButton(index, dstName);
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
            ModelTitleControl mtc = Utils.ControlUtil.FindMTCByName(currentModelTitle, this.modelTitlePanel);
            this.modelDocumentDao.UpdateRemark(this.remarkControl);
            SaveDocument();
            mtc.ClearDirtyPictureBox();            

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

        private void headPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void usernamelabel_Click(object sender, EventArgs e)
        {

        }
    }
}
