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

namespace  Citta_T1
{
    public partial class MainForm : Form
    {
        private bool isBottomViewPanelMinimum;
        private Citta_T1.Dialogs.FormInputData formInputData;
        private Citta_T1.Dialogs.CreateNewModel createNewModel;
        public MainForm()
        {
            this.formInputData = new Citta_T1.Dialogs.FormInputData();
            this.createNewModel = new Citta_T1.Dialogs.CreateNewModel();
            InitializeComponent();
            this.isBottomViewPanelMinimum = false;
            InitializeControlsLocation();
        }
      
       
        private void InitializeControlsLocation()
        {
            // 根据父控件对缩略图控件和底层工具按钮定位
            Panel canvasPanel = (Panel)this.naviViewControl.Parent;
            int x = canvasPanel.Location.X + canvasPanel.Width;
            int y = canvasPanel.Location.Y + canvasPanel.Height;

            // 缩略图定位
            if (x - 300 - this.naviViewControl.Width> 0)
                x = x - 300 - this.naviViewControl.Width;      
            if (y - 100 - this.naviViewControl.Height> 0)
                y = y - 100 - this.naviViewControl.Height;
            this.naviViewControl.Location = new Point(x, y);

            // 底层工具按钮定位
            x = x - (this.canvasPanel.Width) / 2 + 100;
            this.downloadButton.Location = new Point(x + 50, y + 50);
            this.runButton.Location      = new Point(x, y + 50);

            // 顶层浮动工具栏定位
            Point org = new Point(this.canvasPanel.Width, 0);
            Point loc = new Point(org.X - 20 - this.flowControl.Width, org.Y + 50);
            this.flowControl.Location = loc;

        }

        private void MyModelButton_Click(object sender, EventArgs e)
        {
            this.dataSourceControl.Visible = false;
            this.operatorControl.Visible = false;
        }

        private void OprateButton_Click(object sender, EventArgs e)
        {
            this.dataSourceControl.Visible = false;
            this.operatorControl.Visible = true;
        }

        private void DataButton_Click(object sender, EventArgs e)
        {
            this.dataSourceControl.Visible = true;
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

        private void CanvasPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        private void ImportButton_Click(object sender, EventArgs e)
        {
            this.formInputData.StartPosition = FormStartPosition.CenterScreen;
            this.formInputData.ShowDialog();
        }

        private void CanvasPanel_DragDrop(object sender, DragEventArgs e)
        {
            MoveOpControl btn = new MoveOpControl();
            btn.Location = this.PointToClient(new Point(e.X - 300, e.Y - 100));
            this.canvasPanel.Controls.Add(btn);
            btn.textButton.Text = e.Data.GetData("Text").ToString();
            btn.doublelPinFlag  = btn.doublePin.Contains(btn.textButton.Text.ToString());
            btn.InitializeOpPinPicture();
            this.naviViewControl.AddControl(btn);
            this.naviViewControl.UpdateNaviView();
        }

        private void formInputData_Load(object sender, EventArgs e)
        {

        }

        private void newModelButton_Click(object sender, EventArgs e)
        {
            this.createNewModel.StartPosition = FormStartPosition.CenterScreen;
            this.createNewModel.ShowDialog();
        }

    }
}
