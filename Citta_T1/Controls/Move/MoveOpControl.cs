using Citta_T1.Controls.Interface;
using Citta_T1.OperatorViews;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Citta_T1.Business.Option;
using Citta_T1.Business.Model;
using System.Linq;
using static Citta_T1.Controls.CanvasPanel;

namespace Citta_T1.Controls.Move
{ 
    public delegate void DeleteOperatorEventHandler(Control control); 
    public delegate void ModelDocumentDirtyEventHandler();
  

    public partial class MoveOpControl : UserControl, IScalable, IDragable, IMoveControl
    {
        private LogUtil log = LogUtil.GetInstance("MoveOpControl");
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
        private OperatorOption option=new OperatorOption();
        private int id;

        // 一些倍率
        public string ReName { get => textBox.Text; }
        public string SubTypeName { get => typeName; }
        internal OperatorOption Option { get => this.option; set => this.option = value; }
        private ElementStatus status;
        public ElementStatus Status { 
            get => this.status;
            set
            {                
                this.status = value;
                OptionDirty();
            }  
        }
        public int ID { get => this.id; set => this.id = value; }
        public bool EnableOpenOption { get => this.OptionToolStripMenuItem.Enabled; set => this.OptionToolStripMenuItem.Enabled = value; }

        
        private bool relationStatus = true;
        

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
        private List<Rectangle> leftPinArray = new List<Rectangle> {};
        public int revisedPinIndex;
        // 以该控件为起点的所有点
        private List<int> startLineIndexs = new List<int>() { };
        // 以该控件为终点的所有点
        private List<int> endLineIndexs = new List<int>() { };
        List<Line> affectedStartLines = new List<Line>() { };
        List<Line> affectedEndLines = new List<Line>() { };
        public eCommandType cmd = eCommandType.select;

        // 绘制引脚
        private Point leftPin = new Point(3, 11);
        private Point rightPin = new Point(140, 11);
        private int pinWidth = 4;
        private int pinHeight = 4;
        private Pen pen = new Pen(Color.DarkGray, 0.0001f);
        private SolidBrush trnsRedBrush = new SolidBrush(Color.White);
        private Rectangle rectIn_down;
        private Rectangle rectIn_up;
        public Rectangle rectOut;
        private String pinStatus = "noEnter";
        private String rectArea = "rectIn_down rectIn_up rectOut";
        public MoveOpControl()
        {
            InitializeComponent();
        }
        public MoveOpControl(int sizeL, string text, Point loc)
        {
            this.status = ElementStatus.Null;
            InitializeComponent();
            textBox.Text = text;
            typeName = text;
            Location = loc;
            doublelPinFlag = doublePin.Contains(this.textBox.Text);
            InitializeOpPinPicture();
            ChangeSize(sizeL);
            log.Info("Create a MoveOpControl, sizeLevel = " + sizeLevel);

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲DoubleBuffer

            this.EnableOpenOption = this.relationStatus;//设置选项是否可以打开
        }
        public void ChangeSize(int sizeL)
        {
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
            int dy = 0;
            if (doublelPinFlag)
            {
                dy = 4;
            }
            rectIn_down = new Rectangle(this.leftPin.X, this.leftPin.Y - dy, this.pinWidth, this.pinHeight);
            this.leftPinArray.Add(rectIn_down);
            this.endLineIndexs.Add(-1);
            rectIn_up = new Rectangle(this.leftPin.X, this.leftPin.Y + dy, this.pinWidth, this.pinHeight);
            this.leftPinArray.Add(rectIn_up);
            this.endLineIndexs.Add(-1);
            rectOut = new Rectangle(this.rightPin.X, this.rightPin.Y, this.pinWidth, this.pinHeight);
        }

        #region MOC的事件
        private void MoveOpControl_MouseMove(object sender, MouseEventArgs e)
        {
            PinOpLeaveAndEnter(this.PointToClient(MousePosition));

            if (isMouseDown)
            {
                // TODO [DK] 无用代码 过段时间删除
                if (cmd == eCommandType.draw)
                {
                    startX = this.Location.X + e.X;
                    startY = this.Location.Y + e.Y;
                    MouseEventArgs e1 = new MouseEventArgs(e.Button, e.Clicks, startX, startY, 0);
                    Global.GetCanvasPanel().CanvasPanel_MouseMove(this, e1);
                    return;
                }
                #region 控件移动
                (this.Parent as CanvasPanel).StartMove = true;
                int left = this.Left + e.X - mouseOffset.X;
                int top = this.Top + e.Y - mouseOffset.Y;
                this.Location = WorldBoundControl(new Point(left, top));
                #endregion

                #region 线移动部分
                /*
                 * 1. 计算受影响的线, 计算受影响区域 -> 对于OpControl而言，目前左侧至多有{针脚数量}条线，右侧至多有一条线
                 * 2. 重绘静态图                     -> 对于OpControl而言，两侧都存在无效区域
                 * 3. 用静态图盖住变化区域           -> canvas提供封装好的方法完成
                 * 4. 更新坐标                       -> 左右两边的线都要更新坐标
                 * 5. 绘线                           -> 左右两边的线都要更新
                 * 6. 更新canvas.lines               -> 左右两边的线都要更新
                 */

                Line line;
                CanvasPanel canvas = Global.GetCanvasPanel();
                canvas.staticImage = new Bitmap(canvas.ClientRectangle.Width, canvas.ClientRectangle.Height);
                Rectangle clipRectangle = canvas.ClientRectangle;

                CanvasWrapper canvasWrp = new CanvasWrapper(canvas, Graphics.FromImage(canvas.staticImage), canvas.ClientRectangle);

                List<Line> lines = canvas.lines;
                PointF startP;
                PointF endP;
                // 受影响的点
                List<float> affectedPointsX = new List<float> { };
                List<float> affectedPointsY = new List<float> { };
                // 受影响区域数组
                List<Rectangle> affectedAreaArr = new List<Rectangle> { };
                List<Line> affectedLines = new List<Line> { };
                Rectangle affectedArea;


                if (this.endLineIndexs.Count == 0)
                {
                    log.Info("[MoveDtControl] 不满足线移动条件");
                    return;
                }
                log.Info("[MoveDtControl] 满足线移动条件");
                foreach (int index in startLineIndexs)
                {
                    line = lines[index];
                    affectedStartLines.Add(line);
                    affectedLines.Add(line);
                }
                foreach (int index in endLineIndexs)
                {
                    if (index == -1) return;
                    line = lines[index];
                    affectedEndLines.Add(line);
                    affectedLines.Add(line);
                }
                // 受影响左侧区域

                foreach (Line l in affectedEndLines)
                {
                    affectedArea = OpUtil.GetAreaByLine(l);
                    affectedAreaArr.Add(affectedArea);
                }
                foreach (Line l in affectedStartLines)
                {
                    affectedArea = OpUtil.GetAreaByLine(l);
                    affectedAreaArr.Add(affectedArea);
                }
                // 重绘静态图
                canvasWrp.RepaintStatic(clipRectangle, affectedLines);
                canvas.staticImage.Save("Dt_static_image_save.png");
                foreach (Rectangle rect in affectedAreaArr)
                {
                    canvasWrp.CoverPanelByRect(rect);
                }
                // 坐标修正
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
                    canvasWrp.RepaintObject(line);
                }
                //#endregion
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
            if (Pw.Y > 980  - this.Height)
            {
                Pm.Y = this.Parent.Height - this.Height;
            }
            return Pm;
        }

        private void MoveOpControl_MouseDown(object sender, MouseEventArgs e)
        {
            
            (this.Parent as CanvasPanel).StartMove = true;
            if (e.Button == MouseButtons.Left)
            {
                // TODO [DK] 无用代码 过段时间删除
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
            (this.Parent as CanvasPanel).StartMove = true;
            if (e.Button == MouseButtons.Left)
            {
                // TODO [DK] 无用代码 过段时间删除
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
            int maxLength = 10;

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
            double f = Math.Pow(factor, sizeLevel);
            this.Size = new Size((int)(173 * f), (int)(25 * f));//194，25
            this.rightPictureBox.Location = new Point((int)(144 * f), (int)(5 * f));//159,2
            this.statusBox.Location = new Point((int)(126 * f), (int)(5 * f));//新增
            this.rectOut.Location = new Point((int)(159 * f), (int)(11 * f));
            
            this.txtButton.Size = new Size((int)(89 * f),(int)(23 * f));
            this.textBox.Size = new Size((int)(89 * f), (int)(23 * f));
        }
        private void ResizeToSmall()
        {
            double f = Math.Pow(factor, sizeLevel);
            this.Size = new Size((int)(152 * f), (int)(25 * f));//142，25
            this.rightPictureBox.Location = new Point((int)(124 * f), (int)(5 * f));//107,2
            this.statusBox.Location = new Point((int)(104 * f), (int)(5 * f));//新增
            this.txtButton.Size = new Size((int)(67 * f), (int)(23 * f));
            this.textBox.Size = new Size((int)(67 * f), (int)(23 * f));
            this.rectOut.Location = new Point((int)(140 * f), (int)(11 * f));
        }
        private void ResizeToNormal()
        {  
            double f = Math.Pow(factor, sizeLevel);
            this.Size = new Size((int)(167 * f), (int)(25 * f));//184，25
            this.rightPictureBox.Location = new Point((int)(137 * f), (int)(5 * f));//151,2
            this.statusBox.Location = new Point((int)(120 * f), (int)(5 * f));//新增
            this.txtButton.Size = new Size((int)(83 * f), (int)(23 * f));
            this.textBox.Size = new Size((int)(83 * f), (int)(23 * f));
            this.rectOut.Location = new Point((int)(154 * f), (int)(11 * f));
        }
        #endregion

        #region 右键菜单
        public void OptionMenuItem_Click(object sender, EventArgs e)
        {
            switch (this.typeName)
            {

                case "连接算子":
                    new CollideOperatorView(this.Option).ShowDialog();
                    break;
                case "取交集":
                    new CollideOperatorView(this.Option).ShowDialog();
                    break;
                case "取并集":
                    new UnionOperatorView(this.Option).ShowDialog();
                    break;
                case "取差集":
                    new DifferOperatorView(this).ShowDialog();
                    break;
                case "随机采样":
                    new RandomOperatorView(this).ShowDialog();
                    break;
                case "过滤算子":
                    new FilterOperatorView(this).ShowDialog();
                    break;
                case "取最大值":
                    new MaxOperatorView(this).ShowDialog();
                    break;
                case "取最小值":
                    new MinOperatorView(this).ShowDialog();
                    break;
                case "取平均值":
                    new AvgOperatorView(this).ShowDialog();
                    break;
                default:
                    break;
            }
                    
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
            //删除连接的结果控件
            foreach (ModelRelation mr in Global.GetCurrentDocument().ModelRelations)
            {
                if (mr.Start == this.id)
                {
                    DeleteResultControl(mr.End);
                    break;
                }
                    
            }
            //删除自身
            Global.GetCanvasPanel().DeleteElement(this);
            Global.GetNaviViewControl().RemoveControl(this);
            Global.GetNaviViewControl().UpdateNaviView();
            Global.GetMainForm().DeleteDocumentElement(this);
            Global.GetMainForm().SetDocumentDirty();
           
        }
        private void DeleteResultControl(int endID)
        {
            foreach (ModelElement mrc in Global.GetCurrentDocument().ModelElements)
            {
                if (mrc.ID == endID)
                {
                    Global.GetCanvasPanel().DeleteElement(mrc.GetControl);
                    Global.GetNaviViewControl().RemoveControl(mrc.GetControl);
                    Global.GetNaviViewControl().UpdateNaviView();
                    Global.GetCurrentDocument().DeleteModelElement(mrc.GetControl);
                    return;
                }
            }
        }
        private void OptionDirty()
        {
            if (this.status == ElementStatus.Null)
                this.statusBox.Image = Properties.Resources.set;
            else if (this.status == ElementStatus.Done)
                this.statusBox.Image = Properties.Resources.ready;
            else if (this.status == ElementStatus.Ready)
                this.statusBox.Image = null;
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
        public void PinOpLeaveAndEnter(Point mousePosition)
        {
            if (rectIn_down.Contains(mousePosition))
            {
                if (rectArea.Contains(pinStatus))  return; 
                rectIn_down = rectEnter(rectIn_down);
                this.Invalidate();
                pinStatus = "rectIn_down";
            }
            else if(rectIn_up.Contains(mousePosition))
            {
                if (rectArea.Contains(pinStatus)) return;
                rectIn_up = rectEnter(rectIn_up);
                this.Invalidate();
                pinStatus = "rectIn_up";
            }
            else if(rectOut.Contains(mousePosition))
            {
                if (rectArea.Contains(pinStatus)) return;
                rectOut = rectEnter(rectOut);
                this.Invalidate();
                pinStatus = "rectOut";
            }
            else if (pinStatus != "noEnter")
            {             
                switch (pinStatus)
                {
                    case "rectIn_down":
                        rectIn_down = rectLeave(rectIn_down);
                        break;
                    case "rectIn_up":
                        rectIn_up = rectLeave(rectIn_up);
                        break;
                    case "rectOut":
                        rectOut = rectLeave(rectOut);
                        break;
                }
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
            log.Info("rightPinPictureBox_MouseDown beigin =========================");
            // 绘制贝塞尔曲线，起点只能是rightPin
            startX = this.Location.X + this.rightPinPictureBox.Location.X + e.X;
            startY = this.Location.Y + this.rightPinPictureBox.Location.Y + e.Y;
            MouseEventArgs e1 = new MouseEventArgs(e.Button, e.Clicks, startX, startY, 0);
            isMouseDown = true;
            CanvasPanel canvas = (this.Parent as CanvasPanel);
            canvas.CanvasPanel_MouseDown(this, e1);
            log.Info("rightPinPictureBox_MouseDown end   =========================");
            //log.Info(this.Location.ToString());

            //canvas.cmd = eCommandType.draw;
            //canvas.SetStartC = this;
            //canvas.SetStartP(new PointF(startX, startY));
        }

        private void rightPinPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            // 转发给CanvasPanel
            startX = this.Location.X + this.rightPinPictureBox.Location.X + e.X;
            startY = this.Location.Y + this.rightPinPictureBox.Location.Y + e.Y;
            MouseEventArgs e1 = new MouseEventArgs(e.Button, e.Clicks, startX, startY, 0);
            CanvasPanel canvas = Global.GetCanvasPanel();
            canvas.CanvasPanel_MouseMove(this, e1);
        }

        private void rightPinPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            startX = this.Location.X + this.rightPinPictureBox.Location.X + e.X;
            startY = this.Location.Y + this.rightPinPictureBox.Location.Y + e.Y;
            MouseEventArgs e1 = new MouseEventArgs(e.Button, e.Clicks, startX, startY, 0);
            CanvasPanel canvas = Global.GetCanvasPanel();
            canvas.CanvasPanel_MouseUp(this, e1);
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
            {
                SetControlsBySize(factor, this);
                this.rectOut = SetRectBySize(factor, this.rectOut);
                this.rectIn_down = SetRectBySize(factor, this.rectIn_down);
                this.rectIn_up = SetRectBySize(factor, this.rectIn_up);
                this.Invalidate();
            }
            else
            {
                SetControlsBySize(1 / factor, this);
                this.rectOut = SetRectBySize(1 / factor, this.rectOut);
                this.rectIn_down = SetRectBySize(1 / factor, this.rectIn_down);
                this.rectIn_up = SetRectBySize(1 / factor, this.rectIn_up);
            }


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
            Rectangle rect = new Rectangle(
                   new Point((int)p.X - mouseR / 2, (int)p.Y - mouseR / 2),
                   new Size(mouseR, mouseR));
            CanvasPanel canvas = Global.GetCanvasPanel();
            
            foreach (Rectangle _leftPinRect in leftPinArray)
            {
                //int pinLeftX = leftP.Location.X + this.Location.X;
                //int pinTopY = leftP.Location.Y + this.Location.Y;
                //Rectangle leftPinRect = new Rectangle(
                //        new Point(pinLeftX, pinTopY),
                //        new Size(leftP.Width, leftP.Height)
                //);
                Rectangle leftPinRect = new Rectangle(
                    new Point(_leftPinRect.Location.X + this.Location.X, _leftPinRect.Location.Y + this.Location.Y),
                    new Size(_leftPinRect.Width, _leftPinRect.Height));
                int pinLeftX = leftPinRect.X;
                int pinTopY = leftPinRect.Y;
                Console.WriteLine(leftPinRect);
                Console.WriteLine(rect);
                if (leftPinRect.IntersectsWith(rect))
                {
                    // 计算相交面积比
                    float iou = OpUtil.IOU(rect, leftPinRect);
                    if (iou > maxIntersectPerct)
                    {
                        maxIntersectPerct = iou;
                        log.Info("修正鼠标坐标，修正前：" + p.ToString());
                        revisedP = new PointF(
                            pinLeftX + leftPinRect.Width / 2,
                            pinTopY + leftPinRect.Height / 2);
                        canvas.SetEndC = this;
                        revisedPinIndex = leftPinArray.IndexOf(_leftPinRect);
                        log.Info("revisedPinIndex: " + revisedPinIndex);
                        log.Info("修正鼠标坐标，修正后：" + revisedP.ToString());
                    }
                }
            }
            return revisedP;
        }

        public int GetID()
        {
            return this.ID;
        }
        #endregion

        private void MoveOpControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(trnsRedBrush, rectIn_up);
            e.Graphics.DrawRectangle(pen, rectIn_up);
            e.Graphics.FillRectangle(trnsRedBrush, rectIn_down);
            e.Graphics.DrawRectangle(pen, rectIn_down);
            e.Graphics.FillRectangle(trnsRedBrush, rectOut);
            e.Graphics.DrawRectangle(pen, rectOut);
        }
    }
}

        #endregion