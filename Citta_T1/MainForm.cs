using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Citta_T1.Controls;
using Citta_T1.Utils;
using Citta_T1.Controls.Title;
using Citta_T1.Controls.Flow;
using Citta_T1.Business;
using System.IO;

namespace  Citta_T1
{
    public delegate void DocumentLoadEventHandler(string userName);
    public delegate void DocumentSaveEventHandler();

    public partial class MainForm : Form
    {
        bool MouseIsDown = false;
        Point basepoint;
        Bitmap i;
        Graphics g;
        Pen p;
        public Dictionary<string, Citta_T1.Data> contents = new Dictionary<string, Citta_T1.Data>();
        private bool isBottomViewPanelMinimum;
        private bool isLeftViewPanelMinimum;
        private string userName;
        private Citta_T1.Dialogs.FormInputData formInputData;
        private Citta_T1.Dialogs.CreateNewModel createNewModel;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));

        private Citta_T1.Business.ModelDocumentDao modelDocumentDao;
        public event DocumentLoadEventHandler DocumentLoadEvent;
  
        public event DocumentSaveEventHandler DocumenSaveEvent;
        public string GetUserName { get => this.userName; set => this.userName = value; }



        public MainForm()
        {
            this.formInputData = new Citta_T1.Dialogs.FormInputData();
            this.formInputData.InputDataEvent += frm_InputDataEvent;
            this.createNewModel = new Citta_T1.Dialogs.CreateNewModel();
            InitializeComponent();
            this.isBottomViewPanelMinimum = false;
            this.isLeftViewPanelMinimum = false;
            InitializeControlsLocation();
            InitializeMainFormEventHandler();
            
            
            modelDocumentDao = new Business.ModelDocumentDao();
        }

        private void InitializeMainFormEventHandler()
        {
            // 新增文档事件
            this.modelTitlePanel.NewModelDocument += ModelTitlePanel_NewModelDocument;
            this.modelTitlePanel.ModelDocumentSwitch += ModelTitlePanel_DocumentSwitch;
            this.canvasPanel.NewOperatorEvent += NewDocumentOperator;
            this.canvasPanel.DocumentDirtyEvent += DocumentDirty;
            this.DocumentLoadEvent += DocumentsLoad;
            this.DocumenSaveEvent += SaveDocument;



        }

        private void ModelTitlePanel_NewModelDocument(string modelTitle)
        {
            this.modelDocumentDao.AddDocument(modelTitle,this.userName);
            
        }
        internal void DocumentDirty()
        {
            string currentModelTitle = this.modelDocumentDao.CurrentDocument.ModelDocumentTitle;
            ModelTitleControl mtc = Utils.ControlUtil.FindMTCByName(currentModelTitle, this.modelTitlePanel);
            mtc.SetDirtyPictureBox();
            this.saveModelButton.Image= global::Citta_T1.Properties.Resources.clicksavebutton;
        }
        private void NewDocumentOperator(Control ct)
        {
            DocumentDirty();
            this.modelDocumentDao.AddDocumentOperator(ct);

        }
        
        public void SaveDocument()
        {
            this.saveModelButton.Image = ((System.Drawing.Image)resources.GetObject("saveModelButton.Image"));
            DirectoryInfo[] modelTitleList =new DirectoryInfo[1];
            try
            {
                DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\cittaModelDocument\\" + this.userName + "\\");
                modelTitleList = di.GetDirectories();              
            }
            catch
            { }
            modelDocumentDao.SaveDocument();
            try
            {
                foreach (DirectoryInfo modelTitle in modelTitleList)
                {
                    if (modelTitle.ToString() == modelDocumentDao.CurrentDocument.ModelDocumentTitle)
                        return;
                }
            }
            catch
            { }
            this.myModelControl.AddModel(modelDocumentDao.CurrentDocument.ModelDocumentTitle);
        }
        private void  ReSaveDocument()
        {
            this.modelTitlePanel.AddModel(this.createNewModel.ModelTitle);
            modelDocumentDao.SaveDocument();

        }
        private void ModelTitlePanel_DocumentSwitch(string modelTitle)
        {
            this.modelDocumentDao.SwitchDocument(modelTitle);

        }
        private void DocumentsLoad(string userName)
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\cittaModelDocument\\" + userName + "\\"))
            { 
                this.modelDocumentDao.AddDocument("新建模型", userName);
                return;
            }
            try
            {                
                DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\cittaModelDocument\\" + userName + "\\");
                DirectoryInfo[] modelTitleList = di.GetDirectories();
                foreach (DirectoryInfo modelTitle in modelTitleList)//---------------------------------------
                {                
                   List<Control> controls = this.modelDocumentDao.LoadDocuments(modelTitle.ToString(), userName);
                    foreach ( Control ct in controls)
                    {
                        if (ct.Name == "MoveOpControl")
                            (ct as Citta_T1.Controls.Move.MoveOpControl).ModelDocumentDirtyEvent += DocumentDirty; 
                        else
                            (ct as Citta_T1.Controls.Move.MoveDtControl).DtDocumentDirtyEvent += DocumentDirty;                        
                        this.canvasPanel.Controls.Add(ct);
                        this.naviViewControl.AddControl(ct);
                        this.naviViewControl.UpdateNaviView();
                        if (modelTitle.ToString() == modelTitleList[modelTitleList.Length - 1].ToString())//当前文件
                            ct.Show();
                        else
                            ct.Hide();
                    }
                    this.myModelControl.AddModel(modelTitle.ToString());
                    this.modelTitlePanel.AddModel(modelTitle.ToString());
                    this.modelDocumentDao.ModelDocuments.RemoveAt(this.modelDocumentDao.ModelDocuments.Count-1);
                }
                foreach (Citta_T1.Controls.Title.ModelTitleControl ct in this.modelTitlePanel.Controls)
                {
                    if (ct.Location.X == 1)
                    {
                        this.modelTitlePanel.RemoveModel(ct);
                        this.modelTitlePanel.SelectedModel(modelTitleList[modelTitleList.Length - 1].ToString());
                        return;
                    }                   
                }
                
            }
            catch
            {               
                Console.WriteLine("不存在模型文档");
            }
        }
        private void InitializeControlsLocation()
        {
            Point org = new Point(this.canvasPanel.Width, 0);
            Point org2 = new Point(0, this.canvasPanel.Height);
            int x = org.X - 10 - this.naviViewControl.Width;
            int y = org2.Y - 10 - this.naviViewControl.Height;

            // 缩略图定位
            this.naviViewControl.Location = new Point(x, y);

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
            this.dataGridView1.Visible = false;
            this.dataGridView2.Visible = false;
            this.dataGridView3.Visible = true;
        }
        private void ResultLabel_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Visible = true;
            this.dataGridView2.Visible = false;
            this.dataGridView3.Visible = false;
        }

        private void ErrorLabel_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Visible = false;
            this.dataGridView2.Visible = true;
            this.dataGridView3.Visible = false;
        }

        private void LogLabel_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Visible = false;
            this.dataGridView2.Visible = true;
            this.dataGridView3.Visible = false;
        }

        private void MinMaxPictureBox_Click(object sender, EventArgs e)
        {
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

        void frm_InputDataEvent(Citta_T1.Data data)
        {
            // `FormInputData`中的数据添加处理方式，同一个数据不可多次导入
            string index = OpUtil.GenerateMD5(data.content);
            this.dataSourceControl.GenDataButton(index, data.dataName, data.filePath);
            this.dataSourceControl.Visible = true;
            this.operatorControl.Visible = false;
            this.flowChartControl.Visible = false;
        }

        public void OverViewDataByIndex(string index)
        {
            this.dataGridView3.OverViewDataByIndex(index);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            int count = System.Text.RegularExpressions.Regex.Matches(userName, "[a-z0-9]").Count;
            int rightMargin = (userName.Length - (count / 3) - 3) * 14;
            this.usernamelabel.Text = userName;
            Point userNameLocation = new Point(185,10);
            this.usernamelabel.Location = new Point(userNameLocation.X+65- rightMargin, userNameLocation.Y+2);
            this.helpPictureBox.Location = new Point(userNameLocation.X-rightMargin, userNameLocation.Y);
            this.portraitpictureBox.Location = new Point(userNameLocation.X+30- rightMargin, userNameLocation.Y+1);
            //文档加载事件
            DocumentLoadEvent?.Invoke(userName);
        }

        private void StopButton_Click(object sender, EventArgs e)
        {

            if (this.runButton.Name == "pauseButton")
            {
                this.runButton.Image = ((System.Drawing.Image)resources.GetObject("runButton.Image"));
                this.runButton.Name = "runButton";
            }

        }

        private void RunButton_Click(object sender, EventArgs e)
        {

            if (this.runButton.Name == "runButton")
            {
                this.runButton.Image = ((System.Drawing.Image)resources.GetObject("pauseButton.Image"));
                this.runButton.Name = "pauseButton";
            }
            else if (this.runButton.Name == "pauseButton")
            {
                this.runButton.Image = ((System.Drawing.Image)resources.GetObject("runButton.Image"));
                this.runButton.Name = "runButton";
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
            string helpfile = Application.StartupPath.Substring(0, Application.StartupPath.Substring(0, Application.StartupPath.LastIndexOf("\\")).LastIndexOf("\\")); 
            helpfile += @"\Doc\citta帮助文档.chm";
            Help.ShowHelp(this, helpfile);
        }

        private void SaveModelButton_Click(object sender, EventArgs e)
        {

            DocumenSaveEvent?.Invoke();
            string currentModelTitle = this.modelDocumentDao.CurrentDocument.ModelDocumentTitle;
            ModelTitleControl mtc = Utils.ControlUtil.FindMTCByName(currentModelTitle, this.modelTitlePanel);
            mtc.ClearDirtyPictureBox();
        }
    }
}
