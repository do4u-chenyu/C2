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
        bool MouseIsDown = false;
        Point basepoint;
        Bitmap i;
        Graphics g;
        Pen p;
        private bool isBottomViewPanelMinimum;
        private Citta_T1.Dialogs.FormInputData formInputData;
        private Citta_T1.Dialogs.CreateNewModel createNewModel;
        
        public MainForm()
        {
            this.formInputData = new Citta_T1.Dialogs.FormInputData();
            this.formInputData.InputDataEvent += frm_InputDataEvent;
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
            if (x - 330 - this.naviViewControl.Width> 0)
                x = x - 330 - this.naviViewControl.Width;      
            if (y - 100 - this.naviViewControl.Height> 0)
                y = y - 100 - this.naviViewControl.Height;
            this.naviViewControl.Location = new Point(x, y);

            // 底层工具按钮定位
            x = x - (this.CanvasPanel.Width) / 2 + 100;
            this.downloadButton.Location = new Point(x + 50, y + 50);
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
            this.CanvasPanel.Controls.Add(btn);
            btn.textBox1.Text = e.Data.GetData("Text").ToString();
            btn.doublelPinFlag  = btn.doublePin.Contains(btn.textBox1.Text.ToString());
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
            DialogResult dialogResult = this.createNewModel.ShowDialog();
            // 模型标题栏添加新标题
            if (dialogResult == DialogResult.OK)
                this.modelTitlePanel.AddModel(this.createNewModel.ModelTitle);
        }

        void frm_InputDataEvent(Citta_T1.Data data)
        {
            this.dataSourceControl.AddData(data);
        }

        private void canvasPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.usernamelabel.Text = this.Tag.ToString();
        }

        private void CanvasPanel_MouseDown(object sender, MouseEventArgs e)
        {
            MouseIsDown = true;
            basepoint = e.Location;
        }

        private void CanvasPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseIsDown)
            {
                //实例化一个和窗口一样大的位图
                i = new Bitmap(this.Width, this.Height);
                //创建位图的gdi对象
                g = Graphics.FromImage(i);
                //创建画笔
                p = new Pen(Color.Gray, 0.0001f);
                //指定线条的样式为划线段
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                //根据当前位置画图，使用math的abs()方法求绝对值
                if (e.X < basepoint.X && e.Y < basepoint.Y)
                    g.DrawRectangle(p, e.X, e.Y, System.Math.Abs(e.X - basepoint.X), System.Math.Abs(e.Y - basepoint.Y));
                else if (e.X > basepoint.X && e.Y < basepoint.Y)
                    g.DrawRectangle(p, basepoint.X, e.Y, System.Math.Abs(e.X - basepoint.X), System.Math.Abs(e.Y - basepoint.Y));
                else if (e.X < basepoint.X && e.Y > basepoint.Y)
                    g.DrawRectangle(p, e.X, basepoint.Y, System.Math.Abs(e.X - basepoint.X), System.Math.Abs(e.Y - basepoint.Y));
                else
                    g.DrawRectangle(p, basepoint.X, basepoint.Y, System.Math.Abs(e.X - basepoint.X), System.Math.Abs(e.Y - basepoint.Y));

                //将位图贴到窗口上
                this.CanvasPanel.BackgroundImage = i;
                //释放gid和pen资源
                g.Dispose();
                p.Dispose();
            }
        }

        private void CanvasPanel_MouseUp(object sender, MouseEventArgs e)
        {
            i = new Bitmap(this.Width, this.Height);
            g = Graphics.FromImage(i);
            g.Clear(Color.Transparent);
            this.CanvasPanel.BackgroundImage = i;
            g.Dispose();

            //标志位置低
            MouseIsDown = false;
        }
    }
}
