﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Citta_T1.Controls;

namespace  Citta_T1
{
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
        private Citta_T1.Dialogs.FormInputData formInputData;
        private Citta_T1.Dialogs.CreateNewModel createNewModel;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));

        public MainForm()
        {
            this.formInputData = new Citta_T1.Dialogs.FormInputData();
            this.formInputData.InputDataEvent += frm_InputDataEvent;
            this.createNewModel = new Citta_T1.Dialogs.CreateNewModel();
            InitializeComponent();
            this.isBottomViewPanelMinimum = false;
            this.isLeftViewPanelMinimum = false;
            InitializeControlsLocation();
   
        }
      
       
        private void InitializeControlsLocation()
        {
            // 根据父控件对缩略图控件和底层工具按钮定位
            Panel canvasPanel = (Panel)this.naviViewControl.Parent;
            int x = canvasPanel.Location.X + canvasPanel.Width;
            int y = canvasPanel.Location.Y + canvasPanel.Height;

            // 缩略图定位
            if (x - 330 - this.naviViewControl.Width> 0)
                x = x - 330 - this.naviViewControl.Width;      
            if (y - 100 - this.naviViewControl.Height> 0)
                y = y - 100 - this.naviViewControl.Height;
            this.naviViewControl.Location = new Point(x, y);

            // 底层工具按钮定位
            x = x - (this.CanvasPanel.Width) / 2 + 100;
            this.downloadButton.Location = new Point(x + 100, y + 50);
            this.stopButton.Location = new Point(x + 50, y + 50);
            this.runButton.Location      = new Point(x, y + 50);

            // 顶层浮动工具栏和右侧工具及隐藏按钮定位
            Point org = new Point(this.CanvasPanel.Width, 0);
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
            this.dataSourceControl.Visible = false;
            this.operatorControl.Visible = false;
            this.flowChartControl.Visible = false;
        }

        private void OprateButton_Click(object sender, EventArgs e)
        {
            this.operatorControl.Visible = true;

            this.dataSourceControl.Visible = false;
            this.flowChartControl.Visible = false;
        }

        private void DataButton_Click(object sender, EventArgs e)
        {
            this.dataSourceControl.Visible = true;

            this.operatorControl.Visible = false;
            this.flowChartControl.Visible = false;
        }

        private void FlowChartButton_Click(object sender, EventArgs e)
        {
            this.flowChartControl.Visible = true;

            this.dataSourceControl.Visible = false;
            this.operatorControl.Visible = false;
            
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


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ucDataGridView1_Load(object sender, EventArgs e)
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

        private void formInputData_Load(object sender, EventArgs e)
        {

        }

        private void newModelButton_Click(object sender, EventArgs e)
        {
            this.createNewModel.StartPosition = FormStartPosition.CenterScreen;
            DialogResult dialogResult = this.createNewModel.ShowDialog();
            // 模型标题栏添加新标题
            if (dialogResult == DialogResult.OK)
                this.modelTitlePanel.AddModel(this.createNewModel.ModelTitle);
        }

        void frm_InputDataEvent(Citta_T1.Data data)
        {
            // `FormInputData`中的数据添加处理方式，同一个数据不可多次导入
            string index = GenerateMD5(data.content);
            this.dataSourceControl.GenDataButton(index, data.dataName, data.filePath);
        }

        public void OverViewDataByIndex(string index)
        {
            this.dataGridView3.OverViewDataByIndex(index);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            int lettercount=0;
            int newstringcount;
            lettercount = System.Text.RegularExpressions.Regex.Matches(this.Tag.ToString(), "[a-zA-Z0-9]").Count;
            newstringcount = this.Tag.ToString().Length - lettercount/2;
            newstringcount =( newstringcount - 3)*15;
            this.usernamelabel.Text = this.Tag.ToString();
            Point newusernameLocation = new Point(185,10);
            this.usernamelabel.Location =new Point(newusernameLocation.X+65- newstringcount, newusernameLocation.Y+2);
            this.helpPictureBox.Location = new Point(newusernameLocation.X-newstringcount, newusernameLocation.Y);
            this.portraitpictureBox.Location = new Point(newusernameLocation.X+30- newstringcount, newusernameLocation.Y+1);

        }

        /// <summary>
        /// MD5字符串加密
        /// </summary>
        /// <param name="txt"></param>
        /// <returns>加密后字符串</returns>
        public static string GenerateMD5(string txt)
        {
            using (MD5 mi = MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(txt);
                //开始加密
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
        private void stopButton_Click(object sender, EventArgs e)
        {

            if (this.runButton.Name == "pauseButton")
            {
                this.runButton.Image = ((System.Drawing.Image)resources.GetObject("runButton.Image"));
                this.runButton.Name = "runButton";
            }

        }

        private void runButton_Click(object sender, EventArgs e)
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

        private void leftFoldButton_Click(object sender, EventArgs e)
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
    }
}
