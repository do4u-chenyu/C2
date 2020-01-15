using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1
{
    public partial class Form1 : Form
    {
        private bool panel3Minimum;
        private bool isMouseDown = false;
        private Point mouseOffset; //记录鼠标指针的坐标
        public Form1()
        {
            InitializeComponent();
            this.panel3Minimum = false;
            InitializePanel10Location();
            InitializeFlowControlLoaction();


        }
        private void InitializePanel10Location()
        {
            Panel parentPanel = (Panel)this.panel10.Parent;
            int x = parentPanel.Location.X + parentPanel.Width;
            int y = parentPanel.Location.Y + parentPanel.Height;
            int buttonRunx;
            if (x - 300 - this.panel10.Width> 0)
                x = x - 300 - this.panel10.Width;
                buttonRunx = x - (this.panel7.Width)/2+100;       
            if (y - 100 - this.panel10.Height> 0)
                y = y - 100 - this.panel10.Height;
            this.panel10.Location = new Point(x, y);
            this.ButtonDownload.Location = new Point(buttonRunx+50, y+50);
            this.ButtonRun.Location      = new Point(buttonRunx, y+50);
            
        }

        private void InitializeFlowControlLoaction()
        {
            Point org = new Point(this.panel7.Width, 0);
            Point loc = new Point(org.X - 20 - this.flowControl1.Width, org.Y + 50);
            this.flowControl1.Location = loc;
        }

        private void MyModelButton_Click(object sender, EventArgs e)
        {
            this.tabControl.Visible = false;
            this.panel12.Visible = false;
        }

        private void OprateButton_Click(object sender, EventArgs e)
        {
            this.tabControl.Visible = false;
            this.panel12.Visible = true;
        }

        private void DataButton_Click(object sender, EventArgs e)
        {
            this.tabControl.Visible = true;
            this.panel12.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Visible = false;
            this.dataGridView2.Visible = false;
            this.dataGridView3.Visible = true;
        }
        private void label4_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Visible = true;
            this.dataGridView2.Visible = false;
            this.dataGridView3.Visible = false;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Visible = false;
            this.dataGridView2.Visible = true;
            this.dataGridView3.Visible = false;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Visible = false;
            this.dataGridView2.Visible = true;
            this.dataGridView3.Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (this.panel3Minimum == true)
            {
                this.panel3Minimum = false;
                this.panel3.Height = 180;
                this.pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\res\\displaypanel\\minfold.png");
            }
            else {
                this.panel3Minimum = true;
                this.panel3.Height = 40;
                this.pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\res\\displaypanel\\maxunfold.png");
            }
            InitializePanel10Location();
           
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            InitializePanel10Location();
            InitializeFlowControlLoaction();
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
            /*
            Graphics g = e.Graphics;
            Point p  = this.panel11.Location;
            Point p1 = new Point(p.X, p.Y + this.panel11.Height);
            Point p2 = new Point(p.X + this.panel11.Width, p1.Y);

            System.Console.WriteLine(p1.ToString());
            System.Console.WriteLine(p2.ToString());
            Pen pen = new Pen(Color.Black, 2); ;
            g.DrawLine(pen, p1, p2);
            */
        }

        private void ConnectOpButton_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click_1(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Point p = this.panel11.PointToScreen(this.panel11.Location);
            Point p1 = new Point(p.X, p.Y + this.panel11.Height);
            Point p2 = new Point(p.X + this.panel11.Width, p1.Y);

            System.Console.WriteLine(p1.ToString());
            System.Console.WriteLine(p2.ToString());
            Pen pen = new Pen(Color.Black, 100); ;
            g.DrawLine(pen,300,200,800,200);
        }

        private void flowControl1_Load(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ucDataGridView1_Load(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_Load(object sender, EventArgs e)
        {

        }
        private void dataGridView2_Load(object sender, EventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_DragDrop(object sender, DragEventArgs e)
        {
            Button btn = new Button();
            btn.Size = button4.Size;
            btn.Location = this.PointToClient(new Point(e.X-300, e.Y-100));
            this.panel7.Controls.Add(btn);
            btn.Text = e.Data.GetData("Text").ToString();  
            btn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_MouseDown);
            btn.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btn_MouseMove);
            btn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_MouseUp);
        }

        private void panel7_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void btn_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOffset.X = e.X;
                mouseOffset.Y = e.Y;
                isMouseDown = true;
            }
        }
        private void btn_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                int left = (sender as Button).Left + e.X - mouseOffset.X;
                int top = (sender as Button).Top + e.Y - mouseOffset.Y;
                (sender as Button).Location = new Point(left, top);
            }
        }

        private void btn_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }
    }
}
