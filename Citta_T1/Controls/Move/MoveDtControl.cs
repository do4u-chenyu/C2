using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Citta_T1.Utils;
using Citta_T1.Controls.Flow;
using System.Text.RegularExpressions;

namespace Citta_T1.Controls.Move
{
    public delegate void DtDocumentDirtyEventHandler();
    public partial class MoveDtControl: UserControl, IScalable
    {
        private System.Windows.Forms.ToolStripMenuItem overViewMenuItem;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveDtControl));
        public string MDCName { get => this.textBox1.Text; }

        #region 继承属性
        public event DtDocumentDirtyEventHandler DtDocumentDirtyEvent;
        private static System.Text.Encoding _encoding = System.Text.Encoding.GetEncoding("GB2312");
        public event ModelDocumentDirtyEventHandler ModelDocumentDirtyEvent;
        public event DeleteOperatorEventHandler DeleteOperatorEvent;

        private bool isMouseDown = false;
        public bool isClicked = false;
        Line line;
        private string opControlName;
        private Point mouseOffset;
        // 一些倍率
        // 鼠标放在Pin上，Size的缩放倍率
        int multiFactor = 2;
        // 画布上的缩放倍率
        float factor = 1.3F;
        // 缩放等级
        public int sizeLevel = 0;
        private string sizeL;

        private Citta_T1.OperatorViews.FilterOperatorView randomOperatorView;
        // 绘制贝塞尔曲线的起点
        private int startX;
        private int startY;

        #endregion

        public string GetBcpPath()
        {
            return this.Name;
        }

        public MoveDtControl(string bcpPath, int sizeL, string name, Point loc)
        {
            InitializeComponent();
            AddOverViewToMenu();
            this.textBox1.Text = name;
            this.Location = loc;
            this.Name = bcpPath;
            InitializeOpPinPicture();
            ChangeSize(sizeL);
            Console.WriteLine("Create a MoveDtControl, sizeLevel = " + sizeLevel);
        }


        #region 重写方法
        public void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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
        public void textBox1_Leave(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Length == 0)
                return;
            this.textBox1.ReadOnly = true;
            SetOpControlName(this.textBox1.Text);
            this.textBox1.Visible = false;
            this.txtButton.Visible = true;
        }
        public void rightPictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.nameToolTip.SetToolTip(this.rightPictureBox, this.Name);
        }

        public void DeleteMenuItem_Click(object sender, EventArgs e)
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
            ModelDocumentDirtyEvent?.Invoke();
            DeleteOperatorEvent?.Invoke(this);
            MainForm mainForm = (MainForm)parentPanel.Parent;
            mainForm.DeleteDocumentOperator(this);
            DtDocumentDirtyEvent?.Invoke();
        }
        #endregion

        #region 新方法
        private void AddOverViewToMenu()
        {
            this.overViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.overViewMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.overViewMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("菜单1ToolStripMenuItem.BackgroundImage")));
            this.overViewMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.overViewMenuItem.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.overViewMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(155)))), ((int)(((byte)(213)))));
            this.overViewMenuItem.Name = "菜单1ToolStripMenuItem";
            this.overViewMenuItem.Size = new System.Drawing.Size(133, 24);
            this.overViewMenuItem.Text = "预览";
            this.overViewMenuItem.Click += new System.EventHandler(this.PreViewMenuItem_Click);
            this.contextMenuStrip.Items.Insert(0, this.overViewMenuItem);
        }
        public new void InitializeOpPinPicture()
        {
            SetOpControlName(this.textBox1.Text);
            this.Controls.Remove(this.leftPinPictureBox);
        }
        public void PreViewMenuItem_Click(object sender, EventArgs e)
        {
            MainForm prt = (MainForm)Parent.Parent;
            prt.PreViewDataByBcpPath(this.Name);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DtDocumentDirtyEvent?.Invoke();
        }
        private void MoveDtControl_LocationChanged(object sender, EventArgs e)
        {
            // DtDocumentDirtyEvent?.Invoke();
        }
        #endregion

        #region 继承的方法
        public void ChangeSize(int sizeL)
        {
            this.sizeL = sizeL.ToString();
            Console.WriteLine("MoveOpControl: " + this.Width + ";" + this.Height + ";" + this.Left + ";" + this.Top + ";" + this.Font.Size);
            while (sizeL > 0)
            {
                Console.WriteLine("MoveOpControl: " + this.Width + ";" + this.Height + ";" + this.Left + ";" + this.Top + ";" + this.Font.Size);
                ChangeSize();
                Console.WriteLine("MoveOpButton 放大一次");
                Console.WriteLine("MoveOpControl: " + this.Width + ";" + this.Height + ";" + this.Left + ";" + this.Top + ";" + this.Font.Size);
                sizeL -= 1;
                sizeLevel += 1;
            }
        }

        //public void InitializeOpPinPicture()
        //{
        //    SetOpControlName(this.textBox1.Text);
        //    System.Console.WriteLine(doublelPinFlag);

        //    if (doublelPinFlag)
        //    {
        //        int x = this.leftPinPictureBox.Location.X;
        //        int y = this.leftPinPictureBox.Location.Y;
        //        this.leftPinPictureBox.Location = new System.Drawing.Point(x, y - 4);
        //        PictureBox leftPinPictureBox1 = new PictureBox();
        //        leftPinPictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        //        leftPinPictureBox1.Location = new System.Drawing.Point(x, y + 4);
        //        leftPinPictureBox1.Name = "leftPinPictureBox1";
        //        leftPinPictureBox1.Size = this.leftPinPictureBox.Size;
        //        leftPinPictureBox1.TabIndex = 3;
        //        leftPinPictureBox1.TabStop = false;
        //        leftPinPictureBox1.MouseEnter += new System.EventHandler(this.PinOpPictureBox_MouseEnter);
        //        leftPinPictureBox1.MouseLeave += new System.EventHandler(this.PinOpPictureBox_MouseLeave);
        //        this.leftPinPictureBox.Parent.Controls.Add(leftPinPictureBox1);
        //    }
        //    /*
        //    System.Windows.Forms.PictureBox leftPicture1 = this.leftPinPictureBox;
        //    leftPicture1.Location = new System.Drawing.Point(16, 24);
        //    this.Controls.Add(leftPicture1);
        //    */
        //}

        #region MOC的事件
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
                int left = (sender as MoveDtControl).Left + e.X - mouseOffset.X;
                int top = (sender as MoveDtControl).Top + e.Y - mouseOffset.Y;
                (sender as MoveDtControl).Location = new Point(left, top);
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

        private void TxtButton_MouseDown(object sender, MouseEventArgs e)
        {
            MainForm prt = (MainForm)Parent.Parent;
            prt.PreViewDataByBcpPath(this.GetBcpPath());
            // 单击鼠标, 移动控件
            if (e.Clicks == 1)
                MoveOpControl_MouseDown(sender, e);
            // 双击鼠标, 改名字
            if (e.Clicks == 2)
                RenameMenuItem_Click(this, e);

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
                Control parent = (sender as MoveDtControl).Parent;
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
        private void MoveOpControl_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region 控件名称长短改变时改变控件大小
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
                ResizeToBig();
                this.txtButton.Text = SubstringByte(opControlName, 0, maxLength) + "...";
                System.Console.WriteLine("sumcountDigit:" + this.txtButton.Text);
            }
            else
            {
                ResizeToNormal();
                if (sumcount + sumcountDigit <= 8)
                {
                    ResizeToSmall();
                }
                this.txtButton.Text = opControlName;

            }
            this.nameToolTip.SetToolTip(this.txtButton, opControlName);
        }

        public void ResizeToBig()
        {
            Console.WriteLine("[" + Name + "]" + "ResizeToBig: " + sizeLevel);
            this.Size = new System.Drawing.Size((int)(194 * Math.Pow(factor, sizeLevel)), (int)(25 * Math.Pow(factor, sizeLevel)));
            this.rightPictureBox.Location = new System.Drawing.Point((int)(159 * Math.Pow(factor, sizeLevel)), (int)(2 * Math.Pow(factor, sizeLevel)));
            this.rightPinPictureBox.Location = new System.Drawing.Point((int)(179 * Math.Pow(factor, sizeLevel)), (int)(11 * Math.Pow(factor, sizeLevel)));
            this.txtButton.Size = new System.Drawing.Size((int)(124 * Math.Pow(factor, sizeLevel)), (int)(23 * Math.Pow(factor, sizeLevel)));
            this.textBox1.Size = new System.Drawing.Size((int)(124 * Math.Pow(factor, sizeLevel)), (int)(23 * Math.Pow(factor, sizeLevel)));
        }
        public void ResizeToSmall()
        {
            Console.WriteLine("[" + Name + "]" + "ResizeToSmall: " + sizeLevel);
            this.Size = new System.Drawing.Size((int)(142 * Math.Pow(factor, sizeLevel)), (int)(25 * Math.Pow(factor, sizeLevel)));
            this.rightPictureBox.Location = new System.Drawing.Point((int)(107 * Math.Pow(factor, sizeLevel)), (int)(2 * Math.Pow(factor, sizeLevel)));
            this.rightPinPictureBox.Location = new System.Drawing.Point((int)(131 * Math.Pow(factor, sizeLevel)), (int)(11 * Math.Pow(factor, sizeLevel)));
            this.txtButton.Size = new System.Drawing.Size((int)(72 * Math.Pow(factor, sizeLevel)), (int)(23 * Math.Pow(factor, sizeLevel)));
            this.textBox1.Size = new System.Drawing.Size((int)(72 * Math.Pow(factor, sizeLevel)), (int)(23 * Math.Pow(factor, sizeLevel)));
        }
        public void ResizeToNormal()
        {
            Console.WriteLine("[" + Name + "]" + "ResizeToNormal: " + sizeLevel);
            this.Size = new System.Drawing.Size((int)(184 * Math.Pow(factor, sizeLevel)), (int)(25 * Math.Pow(factor, sizeLevel)));
            this.rightPictureBox.Location = new System.Drawing.Point((int)(151 * Math.Pow(factor, sizeLevel)), (int)(2 * Math.Pow(factor, sizeLevel)));
            this.rightPinPictureBox.Location = new System.Drawing.Point((int)(170 * Math.Pow(factor, sizeLevel)), (int)(11 * Math.Pow(factor, sizeLevel)));
            this.txtButton.Size = new System.Drawing.Size((int)(114 * Math.Pow(factor, sizeLevel)), (int)(23 * Math.Pow(factor, sizeLevel)));
            this.textBox1.Size = new System.Drawing.Size((int)(110 * Math.Pow(factor, sizeLevel)), (int)(23 * Math.Pow(factor, sizeLevel)));
        }
        #endregion

        #region 右键菜单
        public void 设置ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.randomOperatorView = new Citta_T1.OperatorViews.FilterOperatorView();
            this.randomOperatorView.StartPosition = FormStartPosition.CenterScreen;
            DialogResult dialogResult = this.randomOperatorView.ShowDialog();
        }

        public void RenameMenuItem_Click(object sender, EventArgs e)
        {
            this.textBox1.ReadOnly = false;

            this.txtButton.Visible = false;
            this.textBox1.Visible = true;
            this.textBox1.Focus();//获取焦点
            this.textBox1.Select(this.textBox1.TextLength, 0);
            ModelDocumentDirtyEvent?.Invoke();
        }

        private void 菜单2ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region textBox
        //public virtual void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    // 按下回车键
        //    if (e.KeyChar == 13)
        //    {
        //        if (this.textBox1.Text.Length == 0)
        //            return;
        //        this.textBox1.ReadOnly = true;
        //        SetOpControlName(this.textBox1.Text);
        //        this.textBox1.Visible = false;
        //        this.txtButton.Visible = true;
        //    }
        //}

        //public virtual void textBox1_Leave(object sender, EventArgs e)
        //{
        //    if (this.textBox1.Text.Length == 0)
        //        return;
        //    this.textBox1.ReadOnly = true;
        //    SetOpControlName(this.textBox1.Text);
        //    this.textBox1.Visible = false;
        //    this.txtButton.Visible = true;
        //}
        #endregion

        //public virtual void rightPictureBox_MouseEnter(object sender, EventArgs e)
        //{
        //    String helpInfo = "温馨提示";
        //    this.nameToolTip.SetToolTip(this.rightPictureBox, helpInfo);
        //}

        #region 针脚事件
        private void PinOpPictureBox_MouseEnter(object sender, EventArgs e)
        {
            System.Drawing.Point oriLtCorner = (sender as PictureBox).Location;
            System.Drawing.Size oriSize = (sender as PictureBox).Size;
            System.Drawing.Point oriCenter = new System.Drawing.Point(oriLtCorner.X + oriSize.Width / 2, oriLtCorner.Y + oriSize.Height / 2);
            System.Drawing.Point dstLtCorner = new System.Drawing.Point(oriCenter.X - oriSize.Width * multiFactor / 2, oriCenter.Y - oriSize.Height * multiFactor / 2);
            System.Drawing.Size dstSize = new System.Drawing.Size(oriSize.Width * multiFactor, oriSize.Height * multiFactor);
            (sender as PictureBox).Location = dstLtCorner;
            (sender as PictureBox).Size = dstSize;
            //(sender as PictureBox).Size = new System.Drawing.Size(10, 10);
        }

        private void PinOpPictureBox_MouseLeave(object sender, EventArgs e)
        {
            System.Drawing.Point oriLtCorner = (sender as PictureBox).Location;
            System.Drawing.Size oriSize = (sender as PictureBox).Size;
            System.Drawing.Point oriCenter = new System.Drawing.Point(oriLtCorner.X + oriSize.Width / 2, oriLtCorner.Y + oriSize.Height / 2);
            System.Drawing.Point dstLtCorner = new System.Drawing.Point(oriCenter.X - oriSize.Width / multiFactor / 2, oriCenter.Y - oriSize.Height / multiFactor / 2);
            System.Drawing.Size dstSize = new System.Drawing.Size(oriSize.Width / multiFactor, oriSize.Height / multiFactor);
            (sender as PictureBox).Location = dstLtCorner;
            (sender as PictureBox).Size = dstSize;
            //(sender as PictureBox).Size = new System.Drawing.Size(5, 5);
        }
        #endregion
        #region 右针脚事件
        // 划线部分
        private void rightPinPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            // 绘制贝塞尔曲线，起点只能是rightPin
            startX = this.Location.X + this.rightPinPictureBox.Location.X + e.X;
            startY = this.Location.Y + this.rightPinPictureBox.Location.Y + e.Y;
            Console.WriteLine(this.Location.ToString());
            isMouseDown = true;
        }

        private void rightPinPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            // 绘制3阶贝塞尔曲线，共四个点，起点终点以及两个需要计算的点
            Graphics g = this.Parent.CreateGraphics();
            if (g != null)
            {
                g.Clear(Color.White);
            }
            if (isMouseDown)
            {
                //this.Refresh();
                int nowX = this.Location.X + this.rightPinPictureBox.Location.X + e.X;
                int nowY = this.Location.Y + this.rightPinPictureBox.Location.Y + e.Y;
                line = new Line(new PointF(startX, startY), new PointF(nowX, nowY));
                line.DrawLine(g);
            }
            g.Dispose();
        }

        private void rightPinPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            (this.Parent as CanvasPanel).lines.Add(line);
        }
        #endregion

        #region 托块的放大与缩小
        private int deep = 0;
        public void ChangeSize(float factor = 1.3F)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲DoubleBuffer

            SetTag(this);
            SetControlsBySize(factor, factor, this);
        }

        public void SetTag(Control cons)
        {
            deep += 1;
            if (deep == 1)
            {
                cons.Tag = cons.Width + ";" + cons.Height + ";" + cons.Left + ";" + cons.Top + ";" + cons.Font.Size;
            }
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ";" + con.Height + ";" + con.Left + ";" + con.Top + ";" + con.Font.Size;
                if (con.Controls.Count > 0)
                {
                    Console.WriteLine("setTag:" + con.GetType().ToString());
                    SetTag(con);
                }
            }
            deep -= 1;
        }
        public static void SetDouble(Control cc)
        {

            cc.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance |
                         System.Reflection.BindingFlags.NonPublic).SetValue(cc, true, null);

        }
        public void SetControlsBySize(float fx, float fy, Control cons)
        {
            deep += 1;
            if (deep == 1)
            {
                Console.WriteLine(cons.GetType().ToString());
                SetDouble(this);
                SetDouble(cons);
                string[] mytag = cons.Tag.ToString().Split(new char[] { ';' });
                cons.Width = Convert.ToInt32(System.Convert.ToSingle(mytag[0]) * fx);//宽度
                cons.Height = Convert.ToInt32(System.Convert.ToSingle(mytag[1]) * fy);//高度
                cons.Left = Convert.ToInt32(System.Convert.ToSingle(mytag[2]) * fx);//左边距
                cons.Top = Convert.ToInt32(System.Convert.ToSingle(mytag[3]) * fy);//顶边距
                Single currentSize = System.Convert.ToSingle(mytag[4]) * fy;//字体大小
                // Note 字体变化会导致MoveOpControl的Width和Height也变化
                cons.Font = new Font(cons.Font.Name, currentSize, cons.Font.Style, cons.Font.Unit);
            }
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {
                // 获取控件的Tag属性值，并分割后存储字符串数组
                SetDouble(this);
                SetDouble(con);
                if (con.Tag != null)
                {
                    string[] mytag = con.Tag.ToString().Split(new char[] { ';' });
                    // 根据窗体缩放的比例确定控件的值
                    con.Width = Convert.ToInt32(System.Convert.ToSingle(mytag[0]) * fx);//宽度
                    con.Height = Convert.ToInt32(System.Convert.ToSingle(mytag[1]) * fy);//高度
                    con.Left = Convert.ToInt32(System.Convert.ToSingle(mytag[2]) * fx);//左边距
                    con.Top = Convert.ToInt32(System.Convert.ToSingle(mytag[3]) * fy);//顶边距
                    Single currentSize = System.Convert.ToSingle(mytag[4]) * fy;//字体大小
                    // Note 字体变化会导致MoveOpControl的Width和Height也变化
                    con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    if (con.Controls.Count > 0)
                    {
                        SetControlsBySize(fx, fy, con);
                    }
                }
            }
            deep -= 1;
        }
        #endregion


        #region 重绘
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //(this.Parent as CanvasPanel).Invalidate();
        }
        #endregion

        #region 文档修改事件

        //private void textBox1_TextChanged(object sender, EventArgs e)
        //{
        //    ModelDocumentDirtyEvent?.Invoke();
        //}

        private void MoveOpControl_LocationChanged(object sender, EventArgs e)
        {
            // ModelDocumentDirtyEvent?.Invoke();
        }
        #endregion
    }
    #endregion



}
