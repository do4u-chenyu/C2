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

        // 以该控件为起点的所有点
        private List<int> startLineIndexs = new List<int>() { };
        // 以该控件为终点的所有点
        #endregion
        // 受影响的线
        List<Line> affectedLines = new List<Line>() { };
        public eCommandType cmd = eCommandType.select;
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
            log.Info("Create a MoveDtControl, sizeLevel = " + sizeLevel);
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
                if (this.oldTextString != this.textBox1.Text)
                {
                    this.oldTextString = this.textBox1.Text;
                    Global.GetMainForm().SetDocumentDirty();
                }
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
            Global.GetCanvasPanel().DeleteElement(this);
            Global.GetNaviViewControl().RemoveControl(this);
            Global.GetNaviViewControl().UpdateNaviView();
            Global.GetMainForm().DeleteDocumentElement(this);
            Global.GetMainForm().SetDocumentDirty();

          
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
        public void InitializeOpPinPicture()
        {
            SetOpControlName(this.textBox1.Text);
           
            rectOut = new Rectangle(this.rightPin.X, this.rightPin.Y, this.pinWidth, this.pinHeight);
        }
        public void PreViewMenuItem_Click(object sender, EventArgs e)
        {
            MainForm prt = (MainForm)Parent.Parent;
            prt.PreViewDataByBcpPath(this.Name, this.encoding);
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

        public void ChangeSize(int sizeL)
        {
            this.sizeL = sizeL.ToString();
            log.Info("MoveOpControl: " + this.Width + ";" + this.Height + ";" + this.Left + ";" + this.Top + ";" + this.Font.Size);
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

            SetTag(this);
            if (isLarger)
            {
                SetControlsBySize(factor, factor, this);
            }
            else if (!isLarger)
            {
                SetControlsBySize(1 / factor, 1 / factor, this);
            }

        }

        #region MOC的事件
        private void MoveOpControl_MouseMove(object sender, MouseEventArgs e)
        {
            // 按住拖拽
            PinOpLeaveAndEnter(this.PointToClient(MousePosition));
            if (isMouseDown)
            {
                if (cmd == eCommandType.draw)
                {
                    startX = this.Location.X + e.X;
                    startY = this.Location.Y + e.Y;
                    MouseEventArgs e1 = new MouseEventArgs(e.Button, e.Clicks, startX, startY, 0);
                    Global.GetCanvasPanel().CanvasPanel_MouseMove(this, e1);
                    return;
                }
                (this.Parent as CanvasPanel).StartMove = true;
                //log.Info("[MoveDtControl]开始移动");

                #region 控件移动部分
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
                (sender as MoveDtControl).Location = WorldBoundControl(new Point(left, top));
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

                Line line;
                CanvasPanel canvas = Global.GetCanvasPanel();
                List<Line> lines = canvas.lines;
                PointF startP;
                PointF endP;
                // 受影响的点
                List<float> affectedPointsX = new List<float> { };
                List<float> affectedPointsY = new List<float> { };

                if (this.startLineIndexs.Count == 0)
                {
                    return;
                }
                foreach (int index in startLineIndexs)
                {
                    line = lines[index];
                    affectedLines.Add(line);
                }

                // 受影响区域
                foreach (Line l in affectedLines)
                {
                    if (!affectedPointsX.Contains(l.StartP.X))
                        affectedPointsX.Add(l.StartP.X);
                    if (!affectedPointsY.Contains(l.StartP.Y))
                        affectedPointsY.Add(l.StartP.Y);
                    if (!affectedPointsX.Contains(l.EndP.X))
                        affectedPointsX.Add(l.EndP.X);
                    if (!affectedPointsY.Contains(l.EndP.Y))
                        affectedPointsY.Add(l.EndP.Y);
                }
                int minX = (int)affectedPointsX.Min();
                int maxX = (int)affectedPointsX.Max();
                int minY = (int)affectedPointsY.Min();
                int maxY = (int)affectedPointsY.Max();
                Rectangle affectedArea = new Rectangle(
                    new Point(minX, minY),
                    new Size(maxX - minX, maxY - minY)
                );
                // 重绘静态图
                // TODO [DK] 不用每次都重新计算
                canvas.staticImage = new Bitmap(canvas.ClientRectangle.Width, canvas.ClientRectangle.Height);
                Rectangle clipRectangle = canvas.ClientRectangle;
                CanvasWrapper dcStatic = new CanvasWrapper(canvas, Graphics.FromImage(canvas.staticImage), canvas.ClientRectangle);
                canvas.RepaintStatic(dcStatic, clipRectangle, affectedLines);
                canvas.staticImage.Save("Dt_static_image_save.png");
                canvas.CoverPanelByRect(affectedArea);
                foreach (int index in startLineIndexs)
                {
                    line = lines[index];
                    // 边界坐标修正
                    line.StartP = new PointF(
                        Math.Min(Math.Max(line.StartP.X + e.X - mouseOffset.X, this.rightPictureBox.Location.X), canvas.Width),
                        Math.Min(Math.Max(line.StartP.Y + e.Y - mouseOffset.Y, this.rightPictureBox.Location.Y), canvas.Height)
                    );
                    // 坐标更新
                    line.UpdatePoints();
                    canvas.RepaintObject(line);
                }
            }
        }

         public Point WorldBoundControl(Point Pm)
        {
            float screenFactor = (this.Parent as CanvasPanel).ScreenFactor;
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

        private void MoveOpControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (rectOut.Contains(e.Location))
                {
                    startX = this.Location.X + e.X;
                    startY = this.Location.Y + e.Y;
                    MouseEventArgs e1 = new MouseEventArgs(e.Button, e.Clicks, startX, startY, 0);
                    isMouseDown = true;
                    cmd = eCommandType.draw;
                    CanvasPanel canvas = (this.Parent as CanvasPanel);
                    canvas.CanvasPanel_MouseDown(this, e1);
                    return;
                }
                mouseOffset.X = e.X;
                mouseOffset.Y = e.Y;
                isMouseDown = true;
            }
            oldcontrolPosition = this.Location;
        }


        private void TxtButton_MouseDown(object sender, MouseEventArgs e)
        {
            MainForm prt = (MainForm)Parent.Parent;
            try
            {
                prt.PreViewDataByBcpPath(this.GetBcpPath(), this.encoding);
            }
            catch (Exception ex)
            {
                MessageBox.Show("该数据源路径发生改变，请检查数据源路径，当前数据源路径为: " + this.GetBcpPath());
                return;
            }
            // 单击鼠标, 移动控件
            if (e.Clicks == 1)
                MoveOpControl_MouseDown(sender, e);
            // 双击鼠标, 改名字
            if (e.Clicks == 2)
                RenameMenuItem_Click(this, e);
            oldcontrolPosition = this.Location;
        }

        private void MoveOpControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (cmd == eCommandType.draw)
                {
                    isMouseDown = false;
                    cmd = eCommandType.select;
                    startX = this.Location.X + e.X;
                    startY = this.Location.Y + e.Y;
                    MouseEventArgs e1 = new MouseEventArgs(e.Button, e.Clicks, startX, startY, 0);
                    CanvasPanel canvas = Global.GetCanvasPanel();
                    canvas.CanvasPanel_MouseUp(this, e1);
                }
                (this.Parent as CanvasPanel).StartMove = true;
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
                if (oldcontrolPosition != this.Location)
                    Global.GetMainForm().SetDocumentDirty();

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
        }
        public void ResizeToSmall()
        {
            this.Size = new System.Drawing.Size((int)(142 * Math.Pow(factor, sizeLevel)), (int)(25 * Math.Pow(factor, sizeLevel)));
            this.rightPictureBox.Location = new System.Drawing.Point((int)(109 * Math.Pow(factor, sizeLevel)), (int)(5 * Math.Pow(factor, sizeLevel)));
            this.rightPinPictureBox.Location = new System.Drawing.Point((int)(131 * Math.Pow(factor, sizeLevel)), (int)(11 * Math.Pow(factor, sizeLevel)));
            this.txtButton.Size = new System.Drawing.Size((int)(72 * Math.Pow(factor, sizeLevel)), (int)(23 * Math.Pow(factor, sizeLevel)));
            this.textBox1.Size = new System.Drawing.Size((int)(72 * Math.Pow(factor, sizeLevel)), (int)(23 * Math.Pow(factor, sizeLevel)));
        }
        public void ResizeToNormal()
        {
            this.Size = new System.Drawing.Size((int)(184 * Math.Pow(factor, sizeLevel)), (int)(25 * Math.Pow(factor, sizeLevel)));
            this.rightPictureBox.Location = new System.Drawing.Point((int)(151 * Math.Pow(factor, sizeLevel)), (int)(5 * Math.Pow(factor, sizeLevel)));
            this.rightPinPictureBox.Location = new System.Drawing.Point((int)(170 * Math.Pow(factor, sizeLevel)), (int)(11 * Math.Pow(factor, sizeLevel)));
            this.txtButton.Size = new System.Drawing.Size((int)(114 * Math.Pow(factor, sizeLevel)), (int)(23 * Math.Pow(factor, sizeLevel)));
            this.textBox1.Size = new System.Drawing.Size((int)(110 * Math.Pow(factor, sizeLevel)), (int)(23 * Math.Pow(factor, sizeLevel)));
        }
        #endregion

        #region 右键菜单
        public void 设置ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //this.randomOperatorView = new Citta_T1.OperatorViews.FilterOperatorView();
            this.randomOperatorView.StartPosition = FormStartPosition.CenterScreen;
            DialogResult dialogResult = this.randomOperatorView.ShowDialog();
        }

        public void RenameMenuItem_Click(object sender, EventArgs e)
        {
            this.textBox1.ReadOnly = false;
            this.oldTextString = this.textBox1.Text;
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
        public void PinOpLeaveAndEnter(Point mousePosition)
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

        #region 右针脚事件
        // 划线部分
        private void rightPinPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            // 绘制贝塞尔曲线，起点只能是rightPin
            //startX = this.Location.X + this.rightPinPictureBox.Location.X + e.X;
            //startY = this.Location.Y + this.rightPinPictureBox.Location.Y + e.Y;
            //MouseEventArgs e1 = new MouseEventArgs(e.Button, e.Clicks, startX, startY, 0);
            //isMouseDown = true;
            //CanvasPanel canvas = (this.Parent as CanvasPanel);
            //canvas.CanvasPanel_MouseDown(this, e1);
        }

        private void rightPinPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            //startX = this.Location.X + this.rightPinPictureBox.Location.X + e.X;
            //startY = this.Location.Y + this.rightPinPictureBox.Location.Y + e.Y;
            //MouseEventArgs e1 = new MouseEventArgs(e.Button, e.Clicks, startX, startY, 0);
            //CanvasPanel canvas = Global.GetCanvasPanel();
            //canvas.CanvasPanel_MouseMove(this, e1);
        }

        private void rightPinPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            //isMouseDown = false;
            //startX = this.Location.X + this.rightPinPictureBox.Location.X + e.X;
            //startY = this.Location.Y + this.rightPinPictureBox.Location.Y + e.Y;
            //MouseEventArgs e1 = new MouseEventArgs(e.Button, e.Clicks, startX, startY, 0);
            //CanvasPanel canvas = Global.GetCanvasPanel();
            //canvas.CanvasPanel_MouseUp(this, e1);
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
                log.Info(cons.GetType().ToString());
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

        #region 拖动实现
        public void ChangeLoc(float dx, float dy)
        {
            int left = this.Left + Convert.ToInt32(dx);
            int top = this.Top + Convert.ToInt32(dy);
            this.Location = new Point(left, top);

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
        private void MoveOpControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(trnsRedBrush, rectOut);
            e.Graphics.DrawRectangle(pen, rectOut);
        }

        #region 划线动作
        #endregion
    }

}
