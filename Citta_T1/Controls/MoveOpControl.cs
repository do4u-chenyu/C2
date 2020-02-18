using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace Citta_T1.Controls
{
    
    public partial class MoveOpControl : UserControl
    {
        private static Encoding _encoding = System.Text.Encoding.GetEncoding("GB2312");
        private string opControlName;
        private bool isMouseDown = false;
        private Point mouseOffset;
        public string doublePin = "连接算子 取差集 取交集 取并集 ";
        public bool doublelPinFlag = false;

        DateTime clickTime;
        bool isClicked = false;



        public MoveOpControl()
        {
            
            InitializeComponent();
        }

        public void InitializeOpPinPicture()
        {
            SetOpControlName(this.textBox1.Text);
            System.Console.WriteLine(doublelPinFlag);
            if (doublelPinFlag)
            {
                int x = this.leftPinPictureBox.Location.X;
                int y = this.leftPinPictureBox.Location.Y;
                this.leftPinPictureBox.Location = new System.Drawing.Point(x, y-4);
                PictureBox leftPinPictureBox1 = new PictureBox();
                leftPinPictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                leftPinPictureBox1.Location = new System.Drawing.Point(x, y+4);
                leftPinPictureBox1.Name = "leftPinPictureBox1";
                leftPinPictureBox1.Size = this.leftPinPictureBox.Size;
                leftPinPictureBox1.TabIndex = 3;
                leftPinPictureBox1.TabStop = false;
                leftPinPictureBox1.MouseEnter += new System.EventHandler(this.PinOpPictureBox_MouseEnter);
                leftPinPictureBox1.MouseLeave += new System.EventHandler(this.PinOpPictureBox_MouseLeave);
                this.leftPinPictureBox.Parent.Controls.Add(leftPinPictureBox1);
            }
            /*
            System.Windows.Forms.PictureBox leftPicture1 = this.leftPinPictureBox;
            leftPicture1.Location = new System.Drawing.Point(16, 24);
            this.Controls.Add(leftPicture1);
            */
        }

        private void MoveOpControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                if (sender is Button)
                {
                    sender = (sender as Button).Parent;
                }
                if (sender is PictureBox)
                {
                    sender = (sender as PictureBox).Parent;
                }
                if (sender is TextBox)
                {
                    sender = (sender as TextBox).Parent;
                }
                int left = (sender as MoveOpControl).Left + e.X - mouseOffset.X;
                int top = (sender as MoveOpControl).Top + e.Y - mouseOffset.Y;
                (sender as MoveOpControl).Location = new Point(left, top);
            }
        }

        private void MoveOpControl_MouseDown(object sender, MouseEventArgs e)
        {
            System.Console.WriteLine("移动开始");
            if (e.Button == MouseButtons.Left)
            {
                mouseOffset.X = e.X;
                mouseOffset.Y = e.Y;
                isMouseDown = true;
            }
        }

        private void MoveOpControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)


            {
                isMouseDown = false;
                if (sender is Button)
                {
                    sender = (sender as Button).Parent;
                }
                if (sender is PictureBox)
                {
                    sender = (sender as PictureBox).Parent;
                }
                Control parent = (sender as MoveOpControl).Parent;
                foreach (Control ct in parent.Controls)
                {
                    if (ct.Name == "naviViewControl")
                    {
                        (ct as NaviViewControl).UpdateNaviView();
                        break;
                    }
                }

            }

        }

        private void PinOpPictureBox_MouseEnter(object sender, EventArgs e)
        {
            (sender as PictureBox).Size = new System.Drawing.Size(10, 10);
        }

        private void PinOpPictureBox_MouseLeave(object sender, EventArgs e)
        {
            (sender as PictureBox).Size = new System.Drawing.Size(5, 5);
        }

        private void MoveOpControl_Load(object sender, EventArgs e)
        {

        }

        private void 菜单2ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        

        private string SubstringByte(string text, int startIndex, int length)
        {
            byte[] bytes = _encoding.GetBytes(text);
            System.Console.WriteLine("bytes:" + bytes);
            return _encoding.GetString(bytes, startIndex, length);
        }
        public void SetOpControlName(string opControlName)
        {
            this.opControlName = opControlName;
            int maxLength = 12;
         
            int sumcount = 0;
            int sumcountDigit = 0;
            
            sumcount = Regex.Matches(opControlName, "[\u4E00-\u9FA5]").Count * 2;
            sumcountDigit = Regex.Matches(opControlName, "[a-zA-Z0-9]").Count;

            System.Console.WriteLine("算子长度:" + opControlName.Length);
            System.Console.WriteLine("sumcount:" + sumcount);
            System.Console.WriteLine("sumcountDigit:" + sumcountDigit);
            if (sumcount + sumcountDigit > maxLength)
            {
                resize();
                this.textButton.Text = SubstringByte(opControlName, 0, maxLength) + "...";
            }
            else
            {
                this.textButton.Text = opControlName;
            }
            this.toolTip1.SetToolTip(this.textButton, opControlName);        
        }

        public void resize()
        {
            this.Size = new System.Drawing.Size(200, 26);
            this.rightPictureBox.Location = new System.Drawing.Point(165, 3);
            this.rightPinPictureBox.Location = new System.Drawing.Point(185, 11);
            this.textButton.Size = new System.Drawing.Size(133, 24);
        }
        private void 重命名ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.textBox1.ReadOnly = false;

            this.textButton.Visible = false;
            this.textBox1.Visible = true;
            this.textBox1.Focus();//获取焦点
            this.textBox1.Select(this.textBox1.TextLength, 0);
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Panel parentPanel = (Panel)this.Parent;
            parentPanel.Controls.Remove(this);
            foreach (Control ct in parentPanel.Controls)
            {
                if (ct.Name == "naviViewControl")
                {
                    (ct as NaviViewControl).RemoveControl(this);
                    (ct as NaviViewControl).UpdateNaviView();
                    break;
                }
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 按下回车键
            if (e.KeyChar == 13)
            {
                if (this.textBox1.Text.Length == 0)
                    return;
                this.textBox1.ReadOnly = true;
                SetOpControlName(this.textBox1.Text);
                this.textBox1.Visible = false;
                this.textButton.Visible = true; 
            }
        }

        private void textButton_Click(object sender, EventArgs e)
        {
            if (isClicked)
            {
                TimeSpan span = DateTime.Now - clickTime;
                clickTime = DateTime.Now;
                if (span.TotalMilliseconds < SystemInformation.DoubleClickTime)

                    //  把milliseconds改成totalMilliseconds 因为前者不是真正的时间间隔，totalMilliseconds才是真正的时间间隔
                {
                    重命名ToolStripMenuItem_Click_1(this, e);
                    isClicked = false;
                }
            }
            else
            {
                isClicked = true;
                clickTime = DateTime.Now;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Length == 0)
                return;
            this.textBox1.ReadOnly = true;
            SetOpControlName(this.textBox1.Text);
            this.textBox1.Visible = false;
            this.textButton.Visible = true;
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Length == 0)
                return;
            this.textBox1.ReadOnly = true;
            SetOpControlName(this.textBox1.Text);
            this.textBox1.Visible = false;
            this.textButton.Visible = true;
        }

    }
}

