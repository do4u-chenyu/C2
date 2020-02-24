using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Citta_T1.Controls
{
    public delegate void delegateOverViewData(string index);
    public delegate void delegateRenameData(string index);
    public partial class MoveOpControl : UserControl
    {
        private static System.Text.Encoding _encoding = System.Text.Encoding.GetEncoding("GB2312");
        private string opControlName;
        private bool isMouseDown = false;
        private Point mouseOffset;
        public string doublePin = "连接算子 取差集 取交集 取并集";
        public bool doublelPinFlag = false;

        public DateTime clickTime;
        public bool isClicked = false;
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
                this.leftPinPictureBox.Location = new System.Drawing.Point(x, y - 4);
                PictureBox leftPinPictureBox1 = new PictureBox();
                leftPinPictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                leftPinPictureBox1.Location = new System.Drawing.Point(x, y + 4);
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
            int maxLength = 14;

            int sumcount = 0;
            int sumcountDigit = 0;

            sumcount = Regex.Matches(opControlName, "[\u4E00-\u9FA5]").Count * 2;
            sumcountDigit = Regex.Matches(opControlName, "[a-zA-Z0-9]").Count;

            System.Console.WriteLine("算子长度:" + opControlName.Length);
            System.Console.WriteLine("sumcount:" + sumcount);
            System.Console.WriteLine("sumcountDigit:" + sumcountDigit);
            if (sumcount + sumcountDigit > maxLength)
            {
                resizetoBig();
                this.txtButton.Text = SubstringByte(opControlName, 0, maxLength) + "...";
                System.Console.WriteLine("sumcountDigit:" + this.txtButton.Text);
            }
            else
            {
                resizetoNormal();
                if (sumcount + sumcountDigit <= 8) 
                { 
                    resizetoSmall(); 
                }              
                this.txtButton.Text = opControlName;

            }
            this.nameToolTip.SetToolTip(this.txtButton, opControlName);
        }

        public void resizetoBig()
        {
            this.Size = new System.Drawing.Size(194, 25);
            this.rightPictureBox.Location = new System.Drawing.Point(159, 2);
            this.rightPinPictureBox.Location = new System.Drawing.Point(179, 11);
            this.txtButton.Size = new System.Drawing.Size(124, 23);
            this.textBox1.Size = new System.Drawing.Size(124, 23);
        }
        public void resizetoSmall()
        {
            this.Size = new System.Drawing.Size(142, 25);
            this.rightPictureBox.Location = new System.Drawing.Point(107, 2);
            this.rightPinPictureBox.Location = new System.Drawing.Point(131, 11);
            this.txtButton.Size = new System.Drawing.Size(72, 23);
            this.textBox1.Size = new System.Drawing.Size(72, 23);
        }
        public void resizetoNormal()
        {
            this.Size = new System.Drawing.Size(184, 25);
            this.rightPictureBox.Location = new System.Drawing.Point(151, 2);
            this.rightPinPictureBox.Location = new System.Drawing.Point(170, 11);
            this.txtButton.Size = new System.Drawing.Size(114, 23);
            this.textBox1.Size = new System.Drawing.Size(110, 23);
        }

        public void 重命名ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.textBox1.ReadOnly = false;

            this.txtButton.Visible = false;
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
        public virtual void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 按下回车键
            if (e.KeyChar == 13)
            {
                if (this.textBox1.Text.Length == 0)
                    return;
                this.textBox1.ReadOnly = true;
                SetOpControlName(this.textBox1.Text);
                this.textBox1.Visible = false;
                this.txtButton.Visible = true;
            }
        }

        public virtual void textBox1_Leave(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Length == 0)
                return;
            this.textBox1.ReadOnly = true;
            SetOpControlName(this.textBox1.Text);
            this.textBox1.Visible = false;
            this.txtButton.Visible = true;
        }

        private void rightPictureBox_MouseEnter(object sender, EventArgs e)
        {
            String helpInfo = "温馨提示";
            this.nameToolTip.SetToolTip(this.rightPictureBox, helpInfo);
        }

        public virtual void txtButton_Click(object sender, EventArgs e)
        {
            System.Console.WriteLine("isClicked:" + isClicked);
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
    }
}

