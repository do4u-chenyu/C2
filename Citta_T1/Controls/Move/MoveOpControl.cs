using Citta_T1.Controls.Interface;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static Citta_T1.Controls.CanvasPanel;

namespace Citta_T1.Controls.Move
{ 
    public delegate void DeleteOperatorEventHandler(Control control); 
    public delegate void ModelDocumentDirtyEventHandler();


    public partial class MoveOpControl : UserControl, IScalable, IDragable, IMoveControl
    {
        public event ModelDocumentDirtyEventHandler ModelDocumentDirtyEvent;

        private static System.Text.Encoding EncodingOfGB2312 = System.Text.Encoding.GetEncoding("GB2312");
        private static string doublePin = "连接算子 取差集 取交集 取并集";

        private string opControlName;
        private bool isMouseDown = false;
        private Point mouseOffset;

        private bool doublelPinFlag = false;

        private PictureBox leftPinPictureBox1 = new PictureBox();

        private string typeName;
        private string oldTextString;

        // 一些倍率
        public string ReName { get => textBox.Text; }
        public string SubTypeName { get => typeName; }
        // 一些倍率
        // 鼠标放在Pin上，Size的缩放倍率
        int multiFactor = 2;
        // 画布上的缩放倍率
        float factor = 1.3F;
        // 缩放等级
        private int sizeLevel = 0;

        // 绘制贝塞尔曲线的起点
        private int startX;
        private int startY;
        private Point oldcontrolPosition;
        Line line;
        private List<int> startLineIndexs = new List<int>{};
        private List<int> endLineIndexs = new List<int>{};
        private List<PictureBox> leftPinArray = new List<PictureBox>{};
        private int revisedPinIndex;




        private Citta_T1.OperatorViews.FilterOperatorView randomOperatorView;
        public MoveOpControl()
        {
            InitializeComponent();
        }
        public MoveOpControl(int sizeL, string text, Point loc)
        {
            
            InitializeComponent();
            textBox.Text = text;
            typeName = text;
            Location = loc;
            doublelPinFlag = doublePin.Contains(this.textBox.Text);
            InitializeOpPinPicture();
            ChangeSize(sizeL);
            Console.WriteLine("Create a MoveOpControl, sizeLevel = " + sizeLevel);

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲DoubleBuffer

            


        }
        public void ChangeSize(int sizeL)
        {
            Console.WriteLine("MoveOpControl: " + this.Width + ";" + this.Height + ";" + this.Left + ";" + this.Top + ";" + this.Font.Size);
            bool originVisible = this.Visible;
            if (originVisible)
                this.Hide();  // 解决控件放大缩小闪烁的问题，非当前文档的元素，不需要hide,show
            if (sizeL > sizeLevel)
            {
                while (sizeL > sizeLevel)
                {
                    ChangeSize(true);
                    sizeLevel += 1;
                }
            }
            else
            {
                while (sizeL < sizeLevel)
                {
                    ChangeSize(false);
                    sizeLevel -= 1;
                }
            }
            if (originVisible)
                this.Show();
        }

        private void InitializeOpPinPicture()
        {
            SetOpControlName(this.textBox.Text);
            System.Console.WriteLine(doublelPinFlag);
            this.leftPinArray.Add(this.leftPinPictureBox);
            this.endLineIndexs.Add(0);
            if (doublelPinFlag)
            {
                int x = this.leftPinPictureBox.Location.X;
                int y = this.leftPinPictureBox.Location.Y;
                this.leftPinPictureBox.Location = new System.Drawing.Point(x, y - 4);
                
                leftPinPictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                leftPinPictureBox1.Location = new System.Drawing.Point(x, y + 4);
                leftPinPictureBox1.Name = "leftPinPictureBox1";
                leftPinPictureBox1.Size = this.leftPinPictureBox.Size;
                leftPinPictureBox1.TabIndex = 3;
                leftPinPictureBox1.TabStop = false;
                leftPinPictureBox1.MouseEnter += new System.EventHandler(this.PinOpPictureBox_MouseEnter);
                leftPinPictureBox1.MouseLeave += new System.EventHandler(this.PinOpPictureBox_MouseLeave);
                this.Controls.Add(leftPinPictureBox1);
                this.leftPinArray.Add(leftPinPictureBox1);
                this.endLineIndexs.Add(0);
            }
            /*
            System.Windows.Forms.PictureBox leftPicture1 = this.leftPinPictureBox;
            leftPicture1.Location = new System.Drawing.Point(16, 24);
            this.Controls.Add(leftPicture1);
            */
        }

        #region MOC的事件
        private void MoveOpControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                int left = this.Left + e.X - mouseOffset.X;
                int top = this.Top + e.Y - mouseOffset.Y;
                this.Location = WorldBoundControl(new Point(left, top));
            }
        }
        public Point WorldBoundControl(Point Pm)
        {
            Point mapOrigin = Global.GetCurrentDocument().MapOrigin;
            Point Pw = Global.GetCurrentDocument().ScreenToWorld(Pm, mapOrigin);

            if (Pw.X < 20)
            {
                Pm.X = 20;
            }
            if (Pw.Y < 70)
            {
                Pm.Y = 70;
            }
            if (Pw.X > 2000 - this.Width)
            {
                Pm.X = this.Parent.Width - this.Width;
            }
            if (Pw.Y > 980 - this.Height)
            {
                Pm.Y = this.Parent.Height - this.Height;
            }
            return Pm;
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
            oldcontrolPosition=this.Location;
    }

    private void TxtButton_MouseDown(object sender, MouseEventArgs e)
        {
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
                this.isMouseDown = false;
                Global.GetNaviViewControl().UpdateNaviView();

            }
            if (oldcontrolPosition != this.Location)
                Global.GetMainForm().SetDocumentDirty();


        }

 

        #endregion

        #region 控件名称长短改变时改变控件大小
        private string SubstringByte(string text, int startIndex, int length)
        {
            byte[] bytes = EncodingOfGB2312.GetBytes(text);
            return EncodingOfGB2312.GetString(bytes, startIndex, length);
        }
        public void SetOpControlName(string name)
        {
            this.opControlName = name;
            int maxLength = 14;

            int sumCount = Regex.Matches(name, "[\u4E00-\u9FA5]").Count * 2;
            int sumCountDigit = Regex.Matches(name, "[a-zA-Z0-9]").Count;

            if (sumCount + sumCountDigit > maxLength)
            {
                ResizeToBig();
                this.txtButton.Text = SubstringByte(name, 0, maxLength) + "...";
            }
            else
            {
                this.txtButton.Text = name;
                
                if (sumCount + sumCountDigit <= 8) 
                    ResizeToSmall();
                else
                    ResizeToNormal();
            }
            this.nameToolTip.SetToolTip(this.txtButton, name);
        }

        private void ResizeToBig()
        {
            Console.WriteLine("[" + Name + "]" + "ResizeToBig: " + sizeLevel);
            double f = Math.Pow(factor, sizeLevel);
            this.Size = new Size((int)(194 * f), (int)(25 * f));
            this.rightPictureBox.Location = new Point((int)(159 * f), (int)(2 * f));
            this.rightPinPictureBox.Location = new Point((int)(179 * f), (int)(11 * f));
            this.txtButton.Size = new Size((int)(124 * f),(int)(23 * f));
            this.textBox.Size = new Size((int)(124 * f), (int)(23 * f));
        }
        private void ResizeToSmall()
        {
            Console.WriteLine("[" + Name + "]" + "ResizeToSmall: " + sizeLevel);
            double f = Math.Pow(factor, sizeLevel);
            this.Size = new Size((int)(142 * f), (int)(25 * f));
            this.rightPictureBox.Location = new Point((int)(107 * f), (int)(2 * f));
            this.rightPinPictureBox.Location = new Point((int)(131 * f), (int)(11 * f));
            this.txtButton.Size = new Size((int)(72 * f), (int)(23 * f));
            this.textBox.Size = new Size((int)(72 * f), (int)(23 * f));
        }
        private void ResizeToNormal()
        {
            Console.WriteLine("[" + Name + "]" + "ResizeToNormal: " + sizeLevel);
            double f = Math.Pow(factor, sizeLevel);
            this.Size = new Size((int)(184 * f), (int)(25 * f));
            this.rightPictureBox.Location = new Point((int)(151 * f), (int)(2 * f));
            this.rightPinPictureBox.Location = new Point((int)(170 * f), (int)(11 * f));
            this.txtButton.Size = new Size((int)(114 * f), (int)(23 * f));
            this.textBox.Size = new Size((int)(110 * f), (int)(23 * f));
        }
        #endregion

        #region 右键菜单
        public void OptionMenuItem_Click(object sender, EventArgs e)
        {
            this.randomOperatorView = new Citta_T1.OperatorViews.FilterOperatorView();
            this.randomOperatorView.StartPosition = FormStartPosition.CenterScreen;
            DialogResult dialogResult = this.randomOperatorView.ShowDialog();
        }

        public void RenameMenuItem_Click(object sender, EventArgs e)
        {
            this.textBox.ReadOnly = false;
            this.oldTextString = this.textBox.Text;
            this.txtButton.Visible = false;
            this.textBox.Visible = true;
            this.textBox.Focus();//获取焦点
            this.textBox.Select(this.textBox.TextLength, 0);
             ModelDocumentDirtyEvent?.Invoke();
        }

        public void DeleteMenuItem_Click(object sender, EventArgs e)
        {

            Global.GetCanvasPanel().DeleteElement(this);
            Global.GetNaviViewControl().RemoveControl(this);
            Global.GetNaviViewControl().UpdateNaviView();
            Global.GetMainForm().DeleteDocumentElement(this);
            Global.GetMainForm().SetDocumentDirty();
        }
        #endregion

        #region textBox
        public void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 按下回车键
            if (e.KeyChar == 13)
                FinishTextChange();
        }

        public void textBox1_Leave(object sender, EventArgs e)
        {
            FinishTextChange();
        }

        private void FinishTextChange()
        {
            if (this.textBox.Text.Length == 0)
                return;
            this.textBox.ReadOnly = true;
            SetOpControlName(this.textBox.Text);
            this.textBox.Visible = false;
            this.txtButton.Visible = true;
            if (this.oldTextString != this.textBox.Text)
            {
                this.oldTextString = this.textBox.Text;
                Global.GetMainForm().SetDocumentDirty();
            }
        }
        #endregion

        public void rightPictureBox_MouseEnter(object sender, EventArgs e)
        {
            String helpInfo = "温馨提示";
            this.nameToolTip.SetToolTip(this.rightPictureBox, helpInfo);
         
        }

        #region 针脚事件
        private void PinOpPictureBox_MouseEnter(object sender, EventArgs e)
        {
            Point oriLtCorner = (sender as PictureBox).Location;
            Size oriSize = (sender as PictureBox).Size;
            Point oriCenter = new Point(oriLtCorner.X + oriSize.Width / 2, oriLtCorner.Y + oriSize.Height / 2);
            Point dstLtCorner = new Point(oriCenter.X - oriSize.Width * multiFactor / 2, oriCenter.Y - oriSize.Height * multiFactor / 2);
            Size dstSize = new Size(oriSize.Width * multiFactor, oriSize.Height * multiFactor);
            (sender as PictureBox).Location = dstLtCorner;
            (sender as PictureBox).Size = dstSize;
        }

        private void PinOpPictureBox_MouseLeave(object sender, EventArgs e)
        {
            Point oriLtCorner = (sender as PictureBox).Location;
            Size oriSize = (sender as PictureBox).Size;
            Point oriCenter = new Point(oriLtCorner.X + oriSize.Width / 2, oriLtCorner.Y + oriSize.Height / 2);
            Point dstLtCorner = new Point(oriCenter.X - oriSize.Width / multiFactor / 2, oriCenter.Y - oriSize.Height / multiFactor / 2);
            Size dstSize = new Size(oriSize.Width / multiFactor, oriSize.Height / multiFactor);
            (sender as PictureBox).Location = dstLtCorner;
            (sender as PictureBox).Size = dstSize;
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
            CanvasPanel canvas = (this.Parent as CanvasPanel);
            canvas.cmd = eCommandType.draw;
            canvas.SetStartC = this;
            canvas.SetStartP(new PointF(startX, startY));
        }

        private void rightPinPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            // 绘制3阶贝塞尔曲线，共四个点，起点终点以及两个需要计算的点
            //Graphics g = this.Parent.CreateGraphics();
            //if (g != null)
            //{
            //    g.Clear(Color.White);
            //}
            //if (isMouseDown)
            //{
            //    //this.Refresh();
            //    int nowX = this.Location.X + this.rightPinPictureBox.Location.X + e.X;
            //    int nowY = this.Location.Y + this.rightPinPictureBox.Location.Y + e.Y;
            //    line = new Line(new PointF(startX, startY), new PointF(nowX, nowY));
            //    line.DrawLine(g);
            //}
            //g.Dispose();
        }

        private void rightPinPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            (this.Parent as CanvasPanel).lines.Add(line);
        }
        #endregion

        #region 托块的放大与缩小
        private void ChangeSize(bool zoomUp, float factor = 1.3F)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲DoubleBuffer
            
            SetDouble(this);
            if (zoomUp)
                SetControlsBySize(factor, this);
            else 
                SetControlsBySize(1 / factor, this);

        }

        public static void SetDouble(Control cc)
        {

            cc.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance |
                         System.Reflection.BindingFlags.NonPublic).SetValue(cc, true, null);

        }
        public void SetControlsBySize(float f, Control control)
        {      
            control.Width = Convert.ToInt32(control.Width * f);
            control.Height = Convert.ToInt32(control.Height * f);
            control.Left = Convert.ToInt32(control.Left * f);
            control.Top = Convert.ToInt32(control.Top * f);
            control.Font = new Font(control.Font.Name, control.Font.Size * f, control.Font.Style, control.Font.Unit);

            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in control.Controls)
                SetControlsBySize(f, con);
        }
        #endregion

        #region 拖动实现

        public void ChangeLoc(float dx, float dy)
        {
            Bitmap staticImage = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(staticImage, new Rectangle(0, 0, this.Width, this.Height));

            this.Visible = false;
            this.Left = this.Left + (int)dx;
            this.Top = this.Top + (int)dy;

            Graphics n = this.CreateGraphics();
            n.DrawImageUnscaled(staticImage, this.Left, this.Top);
            n.Dispose();
            this.Visible = true;
            //this.Location = new Point(left, top);
            // Console.WriteLine("拖拽中 世界坐标: X=" + left.ToString() + ", Y = " + top.ToString());
        }
        #endregion

        #region IMoveControl 接口实现方法
        // TODO [DK] 实现接口
        public void UpdateLineWhenMoving()
        {

        }
        public void SaveStartLines(int line_index)
        {
            this.startLineIndexs.Add(line_index);
        }

        public void SaveEndLines(int line_index)
        {
            // TODO [DK] 实现接口
            /*
             * 绘制动作结束后，将线索引存起来，存哪个针脚看线坐标修正结果
             */
            this.endLineIndexs[revisedPinIndex] = line_index;
        }
        public PointF RevisePointLoc(PointF p)
        {
            /*
             * 1. 遍历当前Document上所有LeftPin，检查该点是否在LeftPin的附近
             * 2. 如果在，对该点就行修正
             */
             // 鼠标判定矩形大小
            int mouseR = 15;
            int pinR = 6;
            float maxIntersectPerct = 0.0F;
            PointF revisedP = new PointF(p.X, p.Y);
            Console.WriteLine("当前点坐标: " + p.ToString());
            Rectangle rect = new Rectangle(
                   new Point((int)p.X - mouseR / 2, (int)p.Y - mouseR / 2),
                   new Size(mouseR, mouseR));
            CanvasPanel canvas = Global.GetCanvasPanel();
            
            foreach (PictureBox leftP in leftPinArray)
            {
                int pinLeftX = leftP.Location.X + this.Location.X;
                int pinTopY = leftP.Location.Y + this.Location.Y;
                Rectangle leftPinRect = new Rectangle(
                        new Point(pinLeftX, pinTopY),
                        new Size(leftP.Width, leftP.Height)
                );
                if (leftPinRect.IntersectsWith(rect))
                {
                    // 计算相交面积比
                    float iou = OpUtil.IOU(rect, leftPinRect);
                    Console.WriteLine(leftP.Name + "'iou: " + iou);
                    if (iou > maxIntersectPerct)
                    {
                        maxIntersectPerct = iou;
                        Console.WriteLine("修正鼠标坐标，修正前：" + p.ToString());
                        revisedP = new PointF(
                            pinLeftX + leftP.Width / 2,
                            pinTopY + leftP.Height / 2);
                        canvas.SetEndC = this;
                        revisedPinIndex = leftPinArray.IndexOf(leftP);
                        Console.WriteLine("修正鼠标坐标，修正后：" + revisedP.ToString());
                    }
                }
            }
            return revisedP;
        }
        #endregion

    }
}

