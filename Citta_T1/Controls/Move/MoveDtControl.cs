using System;
using System.Drawing;
using System.Windows.Forms;
using Citta_T1.Utils;

using Citta_T1.Controls.Flow;
using System.Text.RegularExpressions;
using static Citta_T1.Controls.CanvasPanel;
using Citta_T1.Controls.Interface;
using System.Collections.Generic;
using System.Linq;
using Citta_T1.Business.Model;

namespace Citta_T1.Controls.Move
{
    public delegate void DtDocumentDirtyEventHandler();
    public partial class MoveDtControl: UserControl, IScalable, IDragable, IMoveControl
    {
        private LogUtil log = LogUtil.GetInstance("MoveDtContorl");
        private System.Windows.Forms.ToolStripMenuItem overViewMenuItem;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveDtControl));
        public string MDCName { get => this.textBox1.Text; }
        private string oldTextString;
        private Point oldcontrolPosition;
        private DSUtil.Encoding encoding;
        private int id;
        public DSUtil.Encoding Encoding { get => this.encoding; set => this.encoding = value; }
        public int ID { get => this.id; set => this.id = value; }

        //绘制引脚
        private Point rightPin = new Point(130, 11);
        private int pinWidth = 4;
        private int pinHeight = 4;
        private Pen pen = new Pen(Color.DarkGray, 0.0001f);
        private SolidBrush trnsRedBrush = new SolidBrush(Color.White);
        public Rectangle rectOut;
        private String pinStatus = "noEnter";

        #region 继承属性
        public event DtDocumentDirtyEventHandler DtDocumentDirtyEvent;
        private static System.Text.Encoding _encoding = System.Text.Encoding.GetEncoding("GB2312");
        public event ModelDocumentDirtyEventHandler ModelDocumentDirtyEvent;
        //public event DeleteOperatorEventHandler DeleteOperatorEvent;

        public bool isClicked = false;
        Bezier line;
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

        // 以该控件为起点的所有点
        private List<int> startLineIndexs = new List<int>() { };
        // 以该控件为终点的所有点
        #endregion
        // 受影响的线
        List<Bezier> affectedLines = new List<Bezier>() { };
        public ECommandType cmd = ECommandType.Null;
        public string GetBcpPath()
        {
            return this.Name;
        }

        public MoveDtControl(string bcpPath, int sizeL, string name, Point loc)
        {
            InitializeComponent();
            this.textBox1.Text = name;
            this.Location = loc;
            this.Name = bcpPath;
            InitializeOpPinPicture();
            ChangeSize(sizeL);
            log.Info("Create a MoveDtControl, sizeLevel = " + sizeLevel);
        }


        #region 重写方法
        public void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((this.Parent as CanvasPanel).Parent as MainForm).flowControl.SelectDrag)
                return;
            // 按下回车键
            if (e.KeyChar == 13)
                FinishTextChange();
           
        }
        public void textBox1_Leave(object sender, EventArgs e)
        {
            if (((this.Parent as CanvasPanel).Parent as MainForm).flowControl.SelectDrag)
                return;
            FinishTextChange();
        }

        private void FinishTextChange()
        {
            if (this.textBox1.Text.Length == 0)
                return;
            this.textBox1.ReadOnly = true;
            SetOpControlName(this.textBox1.Text);
            this.textBox1.Visible = false;
            this.txtButton.Visible = true;
            if (this.oldTextString != this.textBox1.Text)
            {
                this.oldTextString = this.textBox1.Text;
                Global.GetMainForm().SetDocumentDirty();
            }
        }
        public void rightPictureBox_MouseEnter(object sender, EventArgs e)
        {

            this.nameToolTip.SetToolTip(this.rightPictureBox, this.Name);
        }

        public void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (((this.Parent as CanvasPanel).Parent as MainForm).flowControl.SelectDrag)
                return;
            Global.GetCanvasPanel().DeleteElement(this);
            Global.GetNaviViewControl().RemoveControl(this);
            Global.GetNaviViewControl().UpdateNaviView();
            Global.GetMainForm().DeleteDocumentElement(this);
            Global.GetMainForm().SetDocumentDirty();

          
        }
        #endregion

        #region 新方法

        public void InitializeOpPinPicture()
        {
            rectOut = new Rectangle(this.rightPin.X, this.rightPin.Y, this.pinWidth, this.pinHeight);
            SetOpControlName(this.textBox1.Text);
        }
        public void PreViewMenuItem_Click(object sender, EventArgs e)
        {
            MainForm prt = Global.GetMainForm();
            prt.PreViewDataByBcpPath(this.Name, this.encoding);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DtDocumentDirtyEvent?.Invoke();
        }
        private void MoveDtControl_LocationChanged(object sender, EventArgs e)
        {
            // ModelDocumentDirtyEvent?.Invoke();
        }
        #endregion

        public void ChangeSize(int sizeL)
        {
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

        }

        public void ChangeSize(bool isLarger, float factor = 1.3F)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲DoubleBuffer

            if (isLarger)
            {
                SetControlsBySize(factor, this);
                this.rectOut = SetRectBySize(factor, this.rectOut);
            }
            else if (!isLarger)
            {
                SetControlsBySize(1 / factor, this);
                this.rectOut = SetRectBySize(1 / factor, this.rectOut);
            }

        }
        //int i = 0;
        #region MOC的事件
        private void MoveDtControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (((this.Parent as CanvasPanel).Parent as MainForm).flowControl.SelectDrag)
                return;
            PinOpLeaveAndEnter(this.PointToClient(MousePosition));
            //if (!isMouseDown)
            if (cmd == ECommandType.Null)
                return;

            // 开始划线
            if (cmd == ECommandType.PinDraw)
            {
                startX = this.Location.X + e.X;
                startY = this.Location.Y + e.Y;
                MouseEventArgs e1 = new MouseEventArgs(e.Button, e.Clicks, startX, startY, 0);
                Global.GetCanvasPanel().CanvasPanel_MouseMove(this, e1);
                return;
            }

            //(this.Parent as CanvasPanel).StartMove = true;
            //log.Info("[MoveDtControl]开始移动");

            #region 控件移动部分
            int left = this.Left + e.X - mouseOffset.X;
            int top = this.Top + e.Y - mouseOffset.Y;
            this.Location = WorldBoundControl(new Point(left, top));

            #endregion

            // TODO [DK] 拖影严重
            /*
                * 1. 计算受影响的线, 计算受影响区域，将受影响的线直接remove
                * 2. 重绘静态图
                * 3. 用静态图盖住变化区域
                * 4. 更新坐标
                * 5. 绘线
                * 6. 更新canvas.lines
                */

            if (this.startLineIndexs.Count == 0)
            {
                return;
            }

            Bezier line;
            CanvasPanel canvas = Global.GetCanvasPanel();
            List<Bezier> lines = canvas.lines;
            List<ModelRelation> mrs = Global.GetCurrentDocument().ModelRelations;
            PointF startP;
            PointF endP;
            // 受影响的点
            List<float> affectedPointsX = new List<float> { };
            List<float> affectedPointsY = new List<float> { };


            //foreach (int index in startLineIndexs)
            //{
            //    line = lines[index];
            //    affectedLines.Add(line);
            //}

            //// 受影响区域
            //foreach (Bezier l in affectedLines)
            //{
            //    if (!affectedPointsX.Contains(l.StartP.X))
            //        affectedPointsX.Add(l.StartP.X);
            //    if (!affectedPointsY.Contains(l.StartP.Y))
            //        affectedPointsY.Add(l.StartP.Y);
            //    if (!affectedPointsX.Contains(l.EndP.X))
            //        affectedPointsX.Add(l.EndP.X);
            //    if (!affectedPointsY.Contains(l.EndP.Y))
            //        affectedPointsY.Add(l.EndP.Y);
            //}
            //int minX = (int)affectedPointsX.Min();
            //int maxX = (int)affectedPointsX.Max();
            //int minY = (int)affectedPointsY.Min();
            //int maxY = (int)affectedPointsY.Max();
            //Rectangle affectedArea = new Rectangle(
            //    new Point(minX, minY),
            //    new Size(maxX - minX, maxY - minY)
            //);
            // 重绘静态图
            // TODO [DK] 不用每次都重新计算
            if (canvas.staticImage2 == null)
            {
                canvas.staticImage2 = new Bitmap(canvas.Width, canvas.Height);
                Graphics g2 = Graphics.FromImage(canvas.staticImage2);
                g2.Clear(Color.White);
                g2.Dispose();
            }
            //log.Info(canvas.Size.ToString());
            Bitmap tmp = new Bitmap(canvas.staticImage2);
            Graphics g = Graphics.FromImage(tmp);

            // TODO [DK BUG] 将控件挪出`CanvasPanel`时，会出错，DrawToBitmap 坐标不能为负
            this.DrawToBitmap(tmp, new Rectangle(this.Location.X, this.Location.Y, this.Width,this.Height));
            //Bitmap tmp = new Bitmap(2000, 2000);
               


            //g2.Clear(Color.White);
            //g2.Dispose();

            // Rectangle clipRectangle = canvas.ClientRectangle;
            // CanvasWrapper dcStatic = new CanvasWrapper(canvas, g1, canvas.ClientRectangle);
            // canvas.RepaintStatic(dcStatic, clipRectangle, affectedLines);
            //canvas.staticImage.Save("Dt_static_image_save.png");
            // canvas.CoverPanelByRect(affectedArea);
            //Graphics g = this.CreateGraphics();
            foreach (int index in startLineIndexs)
            {
                ModelRelation mr = mrs[index];
                mr.StartP = new PointF(
                    Math.Min(Math.Max(mr.StartP.X + e.X - mouseOffset.X, this.rightPictureBox.Location.X), canvas.Width),
                    Math.Min(Math.Max(mr.StartP.Y + e.Y - mouseOffset.Y, this.rightPictureBox.Location.Y), canvas.Height));
                mr.UpdatePoints();
                Bezier newLine = new Bezier(mr.StartP, mr.EndP);
                canvas.RepaintObject(newLine, g);
            }
            Graphics g1 = canvas.CreateGraphics();
            g1.DrawImageUnscaled(tmp, 0,0);
            g.Dispose();
            g1.Dispose();
            //i++;
            //tmp.Save(i.ToString() + ".bmp");

            //tmp.Dispose();
            //tmp = null;


            //this.Hide();

        }

         public Point WorldBoundControl(Point Pm)
        {
            float screenFactor = Global.GetCanvasPanel().ScreenFactor;
            Point mapOrigin = Global.GetCurrentDocument().MapOrigin;

            int orgX = Convert.ToInt32(Pm.X / screenFactor);
            int orgY = Convert.ToInt32(Pm.Y / screenFactor);
            Point Pw = Global.GetCurrentDocument().ScreenToWorld(new Point(orgX, orgY), mapOrigin);


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

        private void MoveDtControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (((this.Parent as CanvasPanel).Parent as MainForm).flowControl.SelectDrag)
                return;
            if (e.Button == MouseButtons.Left)
            {
                if (rectOut.Contains(e.Location))
                {
                    startX = this.Location.X + e.X;
                    startY = this.Location.Y + e.Y;
                    cmd = ECommandType.PinDraw;
                    Global.GetCanvasPanel().CanvasPanel_MouseDown(this, new MouseEventArgs(e.Button, e.Clicks, startX, startY, 0));
                    return;
                }
                mouseOffset.X = e.X;
                mouseOffset.Y = e.Y;
                cmd = ECommandType.Hold;
            }
            oldcontrolPosition = this.Location;
        }


        private void TxtButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (((this.Parent as CanvasPanel).Parent as MainForm).flowControl.SelectDrag)
                return;
            // 单击鼠标, 移动控件
            if (e.Clicks == 1)
                MoveDtControl_MouseDown(sender, e);
            // 双击鼠标, 改名字
            if (e.Clicks == 2)
                RenameMenuItem_Click(this, e);
            oldcontrolPosition = this.Location;
        }

        private void MoveOpControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (((this.Parent as CanvasPanel).Parent as MainForm).flowControl.SelectDrag)
                return;
            if (e.Button == MouseButtons.Left)
            {
                if (cmd == ECommandType.PinDraw)
                {
                    cmd = ECommandType.Null;
                    startX = this.Location.X + e.X;
                    startY = this.Location.Y + e.Y;
                    Global.GetCanvasPanel().CanvasPanel_MouseUp(this, new MouseEventArgs(e.Button, e.Clicks, startX, startY, 0));
                }
                Global.GetCanvasPanel().StartMove = true;
                cmd = ECommandType.Null;

                Global.GetNaviViewControl().UpdateNaviView();
                if (oldcontrolPosition != this.Location)
                    Global.GetMainForm().SetDocumentDirty();

                affectedLines.Clear();
            }

        }
        #endregion

        #region 控件名称长短改变时改变控件大小
        private string SubstringByte(string text, int startIndex, int length)
        {
            byte[] bytes = _encoding.GetBytes(text);
            log.Info("bytes:" + bytes);
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

            log.Info("算子长度:" + opControlName.Length);
            log.Info("sumcount:" + sumcount);
            log.Info("sumcountDigit:" + sumcountDigit);
            if (sumcount + sumcountDigit > maxLength)
            {
                ResizeToBig();
                this.txtButton.Text = SubstringByte(opControlName, 0, maxLength) + "...";
                log.Info("sumcountDigit:" + this.txtButton.Text);
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
            this.Size = new System.Drawing.Size((int)(194 * Math.Pow(factor, sizeLevel)), (int)(25 * Math.Pow(factor, sizeLevel)));
            this.rightPictureBox.Location = new System.Drawing.Point((int)(159 * Math.Pow(factor, sizeLevel)), (int)(5 * Math.Pow(factor, sizeLevel)));
            this.rightPinPictureBox.Location = new System.Drawing.Point((int)(179 * Math.Pow(factor, sizeLevel)), (int)(11 * Math.Pow(factor, sizeLevel)));
            this.txtButton.Size = new System.Drawing.Size((int)(122 * Math.Pow(factor, sizeLevel)), (int)(23 * Math.Pow(factor, sizeLevel)));
            this.textBox1.Size = new System.Drawing.Size((int)(122 * Math.Pow(factor, sizeLevel)), (int)(23 * Math.Pow(factor, sizeLevel)));
            this.rectOut.Location = new System.Drawing.Point((int)(179 * Math.Pow(factor, sizeLevel)), (int)(11 * Math.Pow(factor, sizeLevel)));
        }
        public void ResizeToSmall()
        {
            this.Size = new System.Drawing.Size((int)(142 * Math.Pow(factor, sizeLevel)), (int)(25 * Math.Pow(factor, sizeLevel)));
            this.rightPictureBox.Location = new System.Drawing.Point((int)(109 * Math.Pow(factor, sizeLevel)), (int)(5 * Math.Pow(factor, sizeLevel)));
            this.rightPinPictureBox.Location = new System.Drawing.Point((int)(131 * Math.Pow(factor, sizeLevel)), (int)(11 * Math.Pow(factor, sizeLevel)));
            this.txtButton.Size = new System.Drawing.Size((int)(72 * Math.Pow(factor, sizeLevel)), (int)(23 * Math.Pow(factor, sizeLevel)));
            this.textBox1.Size = new System.Drawing.Size((int)(72 * Math.Pow(factor, sizeLevel)), (int)(23 * Math.Pow(factor, sizeLevel)));
            this.rectOut.Location = new System.Drawing.Point((int)(131 * Math.Pow(factor, sizeLevel)), (int)(11 * Math.Pow(factor, sizeLevel)));
        }
        public void ResizeToNormal()
        {
            this.Size = new System.Drawing.Size((int)(184 * Math.Pow(factor, sizeLevel)), (int)(25 * Math.Pow(factor, sizeLevel)));
            this.rightPictureBox.Location = new System.Drawing.Point((int)(151 * Math.Pow(factor, sizeLevel)), (int)(5 * Math.Pow(factor, sizeLevel)));
            this.rightPinPictureBox.Location = new System.Drawing.Point((int)(170 * Math.Pow(factor, sizeLevel)), (int)(11 * Math.Pow(factor, sizeLevel)));
            this.txtButton.Size = new System.Drawing.Size((int)(114 * Math.Pow(factor, sizeLevel)), (int)(23 * Math.Pow(factor, sizeLevel)));
            this.textBox1.Size = new System.Drawing.Size((int)(110 * Math.Pow(factor, sizeLevel)), (int)(23 * Math.Pow(factor, sizeLevel)));
            this.rectOut.Location = new System.Drawing.Point((int)(170 * Math.Pow(factor, sizeLevel)), (int)(11 * Math.Pow(factor, sizeLevel)));
        }
        #endregion

        #region 右键菜单
        private void OptionMenuItem_Click(object sender, EventArgs e)
        {
            if (((this.Parent as CanvasPanel).Parent as MainForm).flowControl.SelectDrag)
                return;
            //this.randomOperatorView = new Citta_T1.OperatorViews.FilterOperatorView();
            //this.randomOperatorView.StartPosition = FormStartPosition.CenterScreen;
            //DialogResult dialogResult = this.randomOperatorView.ShowDialog();
        }

        private void RenameMenuItem_Click(object sender, EventArgs e)
        {
            if (((this.Parent as CanvasPanel).Parent as MainForm).flowControl.SelectDrag)
                return;
            this.textBox1.ReadOnly = false;
            this.oldTextString = this.textBox1.Text;
            this.txtButton.Visible = false;
            this.textBox1.Visible = true;
            this.textBox1.Focus();//获取焦点
            this.textBox1.Select(this.textBox1.TextLength, 0);
            ModelDocumentDirtyEvent?.Invoke();
        }
        #endregion

        #region 针脚事件
        private void PinOpLeaveAndEnter(Point mousePosition)
        {
            if (rectOut.Contains(mousePosition))
            {
                if (pinStatus == "rectOut") return;
                rectOut = rectEnter(rectOut);
                this.Invalidate();
                pinStatus = "rectOut";
            }
            else if (pinStatus != "noEnter")
            {
                rectOut = rectLeave(rectOut);
                pinStatus = "noEnter";
                this.Invalidate();
            }
        }
        public Rectangle rectEnter(Rectangle rect)
        {
            Point oriLtCorner = rect.Location;
            Size oriSize = rect.Size;
            Point oriCenter = new Point(oriLtCorner.X + oriSize.Width / 2, oriLtCorner.Y + oriSize.Height / 2);
            Point dstLtCorner = new Point(oriCenter.X - oriSize.Width * multiFactor / 2, oriCenter.Y - oriSize.Height * multiFactor / 2);
            Size dstSize = new Size(oriSize.Width * multiFactor, oriSize.Height * multiFactor);
            return new Rectangle(dstLtCorner, dstSize);
        }
        public Rectangle rectLeave(Rectangle rect)
        {
            Point oriLtCorner = rect.Location;
            Size oriSize = rect.Size;
            Point oriCenter = new Point(oriLtCorner.X + oriSize.Width / 2, oriLtCorner.Y + oriSize.Height / 2);
            Point dstLtCorner = new Point(oriCenter.X - oriSize.Width / multiFactor / 2, oriCenter.Y - oriSize.Height / multiFactor / 2);
            Size dstSize = new Size(oriSize.Width / multiFactor, oriSize.Height / multiFactor);
            return new Rectangle(dstLtCorner, dstSize);
        }
        #endregion

        #region 托块的放大与缩小
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
        public Rectangle SetRectBySize(float f, Rectangle rect)
        {
            rect.Width = Convert.ToInt32(rect.Width * f);
            rect.Height = Convert.ToInt32(rect.Height * f);
            rect.X = Convert.ToInt32(rect.Left * f);
            rect.Y = Convert.ToInt32(rect.Top * f);
            return rect;
        }
        #endregion

        #region 拖动实现
        public void ChangeLoc(float dx, float dy)
        {
            int left = this.Left + Convert.ToInt32(dx);
            int top = this.Top + Convert.ToInt32(dy);
            this.Location = new Point(left, top);

        }
        #endregion


        #region 文档修改事件

        #endregion
        #region 接口实现
        /*
         * TODO [DK] 更新线坐标
         * 当空间移动的时候，更新该控件连接线的坐标
         */
        public void UpdateLineWhenMoving()
        {

        }
        public void SaveStartLines(int line_index)
        {
            this.startLineIndexs.Add(line_index);
        }

        public void SaveEndLines(int line_index)
        {
            
        }

        // TODO
        public PointF RevisePointLoc(PointF p)
        {
            // 不存在连DtControl 的 LeftPin的情况
            return p;
        }

        public int GetID()
        {
            return this.ID;
        }
        public PointF GetEndPinLoc(int pinIndex)
        {
            // 不应该被调用
            return new PointF(0, 0);
        }
        public PointF GetStartPinLoc(int pinIndex)
        {
            return new PointF(
                this.Location.X + this.rectOut.Location.X + this.rectOut.Width / 2, 
                this.Location.Y + this.rectOut.Location.Y + this.rectOut.Height / 2);
        }
        public void BindStartLine(int pinIndex, int relationIndex)
        {
            this.startLineIndexs.Add(relationIndex);
        }
        public void BindEndLine(int pinIndex, int relationIndex) { }
        #endregion
        private void MoveDtControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(trnsRedBrush, rectOut);
            e.Graphics.DrawRectangle(pen, rectOut);
        }

        #region 划线动作
        #endregion

        private void txtButton_Click(object sender, EventArgs e)
        {
            MainForm prt = Global.GetMainForm();
            try
            {
                prt.PreViewDataByBcpPath(this.GetBcpPath(), this.encoding);
            }
            catch (Exception ex)
            {
                MessageBox.Show("该数据源路径发生改变，请检查数据源路径，当前数据源路径为: " + this.GetBcpPath());
                return;
            }
        }

    }

}
