
using Citta_T1.Business.Model;
using Citta_T1.Business.Schedule;
using Citta_T1.Controls.Interface;
using Citta_T1.Core;
using Citta_T1.Core.UndoRedo;
using Citta_T1.Core.UndoRedo.Command;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Citta_T1.Controls.Move
{
    public partial class MoveRsControl : UserControl, IScalable, IDragable, IMoveControl
    {
        public event ModelDocumentDirtyEventHandler ModelDocumentDirtyEvent;

        private static System.Text.Encoding EncodingOfGB2312 = System.Text.Encoding.GetEncoding("GB2312");

        private string opControlName;
        private bool isMouseDown = false;
        private Point mouseOffset;

        private static LogUtil log = LogUtil.GetInstance("MoveRsControl");

        private string typeName;
        private string oldTextString;
        private string fullFilePath;
        private DSUtil.Encoding encoding;
        private char separator;

        // 一些倍率
        public string DescriptionName { get => this.textBox.Text; set => this.textBox.Text = value; }
        public string SubTypeName { get => typeName; }
        public char Separator { get => this.separator; set => this.separator = value; }
        // 一些倍率
        // 鼠标放在Pin上，Size的缩放倍率
        int multiFactor = 2;
        // 画布上的缩放倍率
        float factor = Global.Factor;
        // 缩放等级
        private int sizeLevel = 0;

        // 绘制贝塞尔曲线的起点
        private int startX;
        private int startY;
        private Point oldcontrolPosition;
        public ECommandType cmd = ECommandType.Null;



        private ElementStatus status;
        private int id;
        public int ID { get => this.id; set => this.id = value; }
        List<int> startLineIndexs = new List<int>() { };
        List<int> endLineIndexs = new List<int>() { };

        //绘制引脚
        private Point leftPin = new Point(2, 11);
        private Point rightPin = new Point(130, 8);
        private int pinWidth = 6;
        private int pinHeight = 6;
        private Pen pen = new Pen(Color.DarkGray, 1f);
        private SolidBrush trnsRedBrush = new SolidBrush(Color.WhiteSmoke);
        private Rectangle rectIn;
        private Rectangle rectOut;
        private String pinStatus = "noEnter";
        private String rectArea = "rectIn rectOut";
        private List<int> linePinArray = new List<int> { };
        private String lineStatus = "noLine";
        private ControlMoveWrapper controlMoveWrapper;
        private Bitmap staticImage;
        public DSUtil.Encoding Encoding { get => this.encoding; set => this.encoding = value; }

        private Size changeStatus = new Size(0, 28);
        private Size normalStatus = new Size(58, 28);
        public ElementStatus Status
        {
            get => this.status;
            set
            {
                this.status = value;
                StatusDirty();
            }
        }

        public string FullFilePath { get => this.fullFilePath; set => this.fullFilePath = value; }
        public Rectangle RectIn { get => rectIn; set => rectIn = value; }
        public Rectangle RectOut { get => rectOut; set => rectOut = value; }

        public MoveRsControl()
        {
            InitializeComponent();
            InitializeOpPinPicture();
        }
        public MoveRsControl(int sizeL, string desciption, Point loc)
        {

            InitializeComponent();
            DescriptionName = desciption;
            this.typeName = "运算结果";
            this.Location = loc;
            SetOpControlName(DescriptionName);
            ChangeSize(sizeL);
            InitializeOpPinPicture();
            this.controlMoveWrapper = new ControlMoveWrapper(this);
            this.status = ElementStatus.Null;
            endLineIndexs.Add(-1);
        }

        private void InitializeOpPinPicture()
        {
            rectIn = new Rectangle(this.leftPin.X, this.leftPin.Y, this.pinWidth, this.pinHeight);
            rectOut = new Rectangle(this.rightPin.X, this.rightPin.Y, this.pinWidth, this.pinHeight);
            SetOpControlName(DescriptionName);


        }
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

        /*
        System.Windows.Forms.PictureBox leftPicture1 = this.leftPinPictureBox;
        leftPicture1.Location = new System.Drawing.Point(16, 24);
        this.Controls.Add(leftPicture1);
        */


        #region MOC的事件
        private void MoveRsControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                return;
            PinOpLeaveAndEnter(this.PointToClient(MousePosition));
            if (isMouseDown)
            {
                if (cmd == ECommandType.PinDraw)
                {
                    lineStatus = "lineExit";
                    startX = this.Location.X + e.X;
                    startY = this.Location.Y + e.Y;
                    MouseEventArgs e1 = new MouseEventArgs(e.Button, e.Clicks, startX, startY, 0);
                    Global.GetCanvasPanel().CanvasPanel_MouseMove(this, e1);
                    return;
                }
                int left = this.Left + e.X - mouseOffset.X;
                int top = this.Top + e.Y - mouseOffset.Y;
                this.Location = new Point(left, top);

                CanvasPanel canvas = Global.GetCanvasPanel();
                foreach (ModelRelation mr in Global.GetCurrentDocument().ModelRelations)
                {
                    if (mr.StartID == this.id)
                    {
                        mr.StartP = this.GetStartPinLoc(0);
                        mr.UpdatePoints();
                    }
                    if (mr.EndID == this.id)
                    {
                        mr.EndP = this.GetEndPinLoc(mr.EndPin);
                        mr.UpdatePoints();
                    }
                    Bezier newLine = new Bezier(mr.StartP, mr.EndP);
                }
                this.controlMoveWrapper.DragMove(this.Size, Global.GetCanvasPanel().ScreenFactor, e);
            }
        }
        private void MoveRsControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                return;
            if (e.Button == MouseButtons.Left)
            {
                if (rectOut.Contains(e.Location))
                {
                    startX = this.Location.X + e.X;
                    startY = this.Location.Y + e.Y;
                    oldcontrolPosition = this.Location;
                    MouseEventArgs e1 = new MouseEventArgs(e.Button, e.Clicks, startX, startY, 0);
                    isMouseDown = true;
                    cmd = ECommandType.PinDraw;
                    CanvasPanel canvas = (this.Parent as CanvasPanel);
                    canvas.CanvasPanel_MouseDown(this, e1);
                    return;
                }
                mouseOffset.X = e.X;
                mouseOffset.Y = e.Y;
                isMouseDown = true;
            }
            oldcontrolPosition = this.Location;
            this.controlMoveWrapper.DragDown(this.Size, Global.GetCanvasPanel().ScreenFactor, e);
        }

        private void TxtButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                return;
            // 单击鼠标, 移动控件
            if (e.Clicks == 1)
                MoveRsControl_MouseDown(sender, e);
            // 双击鼠标, 改名字
            if (e.Clicks == 2)
                RenameMenuItem_Click(this, e);

        }

        private void MoveRsControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                return;
            if (e.Button == MouseButtons.Left)
            {
                if (cmd == ECommandType.PinDraw)
                {
                    isMouseDown = false;
                    cmd = ECommandType.Null;
                    startX = this.Location.X + e.X;
                    startY = this.Location.Y + e.Y;
                    MouseEventArgs e1 = new MouseEventArgs(e.Button, e.Clicks, startX, startY, 0);
                    CanvasPanel canvas = Global.GetCanvasPanel();
                    canvas.CanvasPanel_MouseUp(this, e1);
                }
                this.isMouseDown = false;
                this.controlMoveWrapper.DragUp(this.Size, Global.GetCanvasPanel().ScreenFactor, e);
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
            if (bytes.Length < length)
                length = bytes.Length;
            return EncodingOfGB2312.GetString(bytes, startIndex, length);
        }
        private int ConutTxtWidth(int chineseRatio, int otherRatio)
        {
            int padding = 3;
            int addValue = 10;
            if ((chineseRatio + otherRatio == 1) && (chineseRatio != 0))
                addValue -= 10;
            return padding * 2 + chineseRatio * 12 + otherRatio * 7 + addValue;
        }
        public void SetOpControlName(string name)
        {
            this.opControlName = name;
            int maxLength = 24;
            name = SubstringByte(name, 0, maxLength);
            int sumCount = Regex.Matches(name, "[\u4E00-\u9FA5]").Count;
            int sumCountDigit = Regex.Matches(name, "[a-zA-Z0-9]").Count;
            int txtWidth = ConutTxtWidth(sumCount, sumCountDigit);
            this.txtButton.Text = name;
            if (EncodingOfGB2312.GetBytes(this.opControlName).Length > maxLength)
            {
                txtWidth += 10;
                this.txtButton.Text = name + "...";
            }
            changeStatus.Width = normalStatus.Width + txtWidth;
            ResizeControl(txtWidth, changeStatus);
            this.nameToolTip.SetToolTip(this.txtButton, this.opControlName);
        }

        private void ResizeControl(int txtWidth, Size controlSize)
        {
            double f = Math.Pow(factor, sizeLevel);
            int pading = 4;
            if (f != 1)
                pading += 1;
            this.Size = new Size((int)(controlSize.Width * f), (int)(controlSize.Height * f));
            this.rightPictureBox.Location = new Point(this.Width - (int)(30 * f), (int)(4 * f));
            this.rectOut.Location = new Point(this.Width - (int)(10 * f), (int)(11 * f));
            this.txtButton.Size = new Size((int)(txtWidth * f), this.Height - (int)(pading * f));
            this.textBox.Size = new Size((int)(txtWidth * f), this.Height - (int)(4 * f));
            DrawRoundedRect((int)(4 * f), 0, this.Width - (int)(11 * f), this.Height - (int)(2 * f), (int)(3 * f));
        }


        #endregion

        #region 右键菜单
        public void PreviewMenuItem_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(this.FullFilePath))
            {
                Global.GetMainForm().PreViewDataByFullFilePath(this.FullFilePath, this.separator, DSUtil.ExtType.Text, this.encoding);
            }
        }

        public void RenameMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                this.textBox.Text = this.oldTextString;
            this.textBox.ReadOnly = false;
            this.oldTextString = this.textBox.Text;
            this.txtButton.Visible = false;
            this.textBox.Visible = true;
            this.textBox.Focus();//获取焦点
            this.textBox.Select(this.textBox.TextLength, 0);
            ModelDocumentDirtyEvent?.Invoke();
        }


        public void RunMenuItem_Click(object sender, EventArgs e)
        {
            //运行到此
            ModelElement currentRs = Global.GetCurrentDocument().SearchElementByID(this.ID);

            //找到对应的op算子
            ModelElement currentOp = null;
            foreach (ModelRelation mr in Global.GetCurrentDocument().ModelRelations)
            {
                if (mr.EndID == this.ID)
                {
                    currentOp = Global.GetCurrentDocument().SearchElementByID(mr.StartID);
                }
            }

            //未找到op算子？？
            if (currentOp == null)
            {
                MessageBox.Show("该算子没有对应的操作算子，请检查模型后再运行", "未找到", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (currentOp.Status == ElementStatus.Null)
            {
                MessageBox.Show("该算子对应的操作算子未配置，请配置后再运行", "未配置", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //判断模型是否保存
            if (Global.GetCurrentDocument().Dirty)
            {
                MessageBox.Show("当前模型没有保存，请保存后再运行模型", "保存", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //需要判断模型当前运行状态，正在运行时，无法执行运行到此
            Manager currentManager = Global.GetCurrentDocument().Manager;
            currentManager.GetCurrentModelRunhereTripleList(Global.GetCurrentDocument(), currentOp);
            Global.GetMainForm().BindUiManagerFunc();

            currentManager.Start();
            Global.GetMainForm().UpdateRunbuttonImageInfo(currentManager.ModelStatus);

        }



        #endregion

        #region textBox
        public void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 按下回车键
            if (e.KeyChar == 13)
            {
                FinishTextChange();
            }
        }

        public void textBox1_Leave(object sender, EventArgs e)
        {
            FinishTextChange();
        }

        public void FinishTextChange()
        {
            if (this.textBox.Text.Trim().Length == 0)
                this.textBox.Text = this.oldTextString;
            this.textBox.ReadOnly = true;
            this.textBox.Visible = false;
            this.txtButton.Visible = true;
            if (this.oldTextString == this.textBox.Text)
                return;

            SetOpControlName(this.textBox.Text);
            // 构造重命名命令类,压入undo栈
            ModelElement element = Global.GetCurrentDocument().SearchElementByID(ID);
            if (element != null)
            {
                ICommand renameCommand = new ElementRenameCommand(element, oldTextString);
                UndoRedoManager.GetInstance().PushCommand(Global.GetCurrentDocument(), renameCommand);
            }

            this.oldTextString = this.textBox.Text;
            Global.GetMainForm().SetDocumentDirty();
            Global.GetCurrentDocument().UpdateAllLines();
            Global.GetCanvasPanel().Invalidate(false);
        }


        public string ChangeTextName(string des)
        {
            string ret = this.opControlName;
            this.oldTextString = this.textBox.Text;
            this.textBox.Text = des;
            SetOpControlName(des);
            Global.GetCurrentDocument().UpdateAllLines();
            Global.GetCanvasPanel().Invalidate(false);
            return ret;
        }
        #endregion

        public void rightPictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.nameToolTip.SetToolTip(this.rightPictureBox, this.FullFilePath);
        }

        #region 针脚事件
        public void PinOpLeaveAndEnter(Point mousePosition)
        {
            if (rectIn.Contains(mousePosition))
            {
                if (pinStatus == "rectIn" || linePinArray.Contains(1)) return;
                rectIn = rectEnter(rectIn);
                this.Invalidate();
                pinStatus = "rectIn";
            }
            else if (rectOut.Contains(mousePosition) || lineStatus == "lineExit")
            {
                if (pinStatus == "rectOut") return;
                rectOut = rectEnter(rectOut);
                this.Invalidate();
                pinStatus = "rectOut";
            }
            else if (pinStatus != "noEnter")
            {
                switch (pinStatus)
                {
                    case "rectIn":
                        rectIn = rectLeave(rectIn);
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
            double f = Math.Pow(factor, sizeLevel);
            Point oriLtCorner = rect.Location;
            Size oriSize = rect.Size;
            Point oriCenter = new Point(oriLtCorner.X + oriSize.Width / 2, oriLtCorner.Y + oriSize.Height / 2);
            Point dstLtCorner = new Point(oriCenter.X - oriSize.Width / 2 - 1, oriCenter.Y - oriSize.Height / 2 - 1);
            Size dstSize = new Size(oriSize.Width + 2, oriSize.Height + 2);
            return new Rectangle(dstLtCorner, dstSize);
        }
        public Rectangle rectLeave(Rectangle rect)
        {
            double f = Math.Pow(factor, sizeLevel);
            Point oriLtCorner = rect.Location;
            Size oriSize = rect.Size;
            Point oriCenter = new Point(oriLtCorner.X + oriSize.Width / 2, oriLtCorner.Y + oriSize.Height / 2);
            Point dstLtCorner = new Point(oriCenter.X - oriSize.Width / 2 + 1, oriCenter.Y - oriSize.Height / 2 + 1);
            Size dstSize = new Size(oriSize.Width - 2, oriSize.Height - 2);
            return new Rectangle(dstLtCorner, dstSize);
        }
        public void OutPinInit(String status)
        {

            lineStatus = status;

            PinOpLeaveAndEnter(new Point(0, 0));
        }
        #endregion

        #region 托块的放大与缩小
        private void ChangeSize(bool zoomUp, float factor = Global.Factor)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲DoubleBuffer
            DrawRoundedRect((int)(4 * Math.Pow(factor, sizeLevel)), 0, this.Width - (int)(11 * Math.Pow(factor, sizeLevel)), this.Height - (int)(2 * Math.Pow(factor, sizeLevel)), (int)(3 * Math.Pow(factor, sizeLevel)));
            SetDouble(this);
            if (zoomUp)
            {
                SetControlsBySize(factor, this);
                this.rectOut = SetRectBySize(factor, this.rectOut);
                this.rectIn = SetRectBySize(factor, this.rectIn);
            }

            else
            {
                SetControlsBySize(1 / factor, this);
                this.rectOut = SetRectBySize(1 / factor, this.rectOut);
                this.rectIn = SetRectBySize(1 / factor, this.rectIn);
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

        #region 状态改变
        private void StatusDirty()
        {
            if (this.status == ElementStatus.Null)
                this.leftPicture.Image = Properties.Resources.resultNull;
            else if (this.status == ElementStatus.Done)
                this.leftPicture.Image = Properties.Resources.resultDone;
            // 状态改变, 需要设置BCP缓冲dirty，以便预览时重新加载
            if (!System.IO.File.Exists(this.FullFilePath))
                return;
            // 手工
            BCPBuffer.GetInstance().SetDirty(this.FullFilePath);
        }

    
        #endregion
        private void MoveOpControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;//去掉锯齿
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;//合成图像的质量
            e.Graphics.FillEllipse(trnsRedBrush, rectIn);
            e.Graphics.DrawEllipse(pen, rectIn);
            e.Graphics.FillEllipse(trnsRedBrush, rectOut);
            e.Graphics.DrawEllipse(pen, rectOut);
        }

        #region IMoveControl接口
        public void UpdateLineWhenMoving()
        {

        }
        public void SaveStartLines(int line_index)
        {
            //this.startLineIndexs.Add(line_index);
        }
        public void SaveEndLines(int line_index)
        {
            try
            {
                //this.endLineIndexs[0] = line_index;
            }
            catch (IndexOutOfRangeException)
            {
                log.Error("索引越界");
            }
            catch (Exception ex)
            {
                log.Error("MoveRsControl SaveEndLines 出错: " + ex.ToString());
            }
        }
        // 修正坐标
        public PointF RevisePointLoc(PointF p)
        {
            return p;
        }

        public int GetID()
        {
            return this.ID;
        }

        public PointF GetStartPinLoc(int pinIndex)
        {
            return new PointF(
                this.Location.X + this.rectOut.Location.X + this.rectOut.Width / 2,
                this.Location.Y + this.rectOut.Location.Y + this.rectOut.Height / 2);
        }
        public PointF GetEndPinLoc(int pinIndex)
        {
            return new PointF(
                this.Location.X + this.rectIn.Location.X + this.rectIn.Width / 2,
                this.Location.Y + this.rectIn.Location.Y + this.rectIn.Height / 2);
        }

        #endregion
        private void DrawRoundedRect(int x, int y, int width, int height, int radius)
        {
            if (this.staticImage != null)
            {   // bitmap是重型资源,需要强制释放
                this.staticImage.Dispose();
                this.staticImage = null;
            }
            this.staticImage = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(staticImage);
            g.Clear(Color.White);
            //去掉圆角的锯齿
            System.Drawing.Pen p = new System.Drawing.Pen(Color.DarkGray, 1);

            g.SmoothingMode = SmoothingMode.HighQuality;//去掉锯齿
            g.CompositingQuality = CompositingQuality.HighQuality;//合成图像的质量
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;//去掉文字的锯齿

            //上
            g.DrawLine(pen, new PointF(x + radius, y), new PointF(x + width - radius, y));
            //下
            g.DrawLine(pen, new PointF(x + radius, y + height), new PointF(x + width - radius, y + height));
            //左
            g.DrawLine(pen, new PointF(x, y + radius), new PointF(x, y + height - radius));
            //右
            g.DrawLine(pen, new PointF(x + width, y + radius), new PointF(x + width, y + height - radius));

            //左上角
            g.DrawArc(pen, new Rectangle(x, y, radius * 2, radius * 2), 180, 90);
            //右上角
            g.DrawArc(pen, new Rectangle(x + width - radius * 2, y, radius * 2, radius * 2), 270, 90);
            //左下角
            g.DrawArc(pen, new Rectangle(x, y + height - radius * 2, radius * 2, radius * 2), 90, 90);
            //右下角
            g.DrawArc(pen, new Rectangle(x + width - radius * 2, y + height - radius * 2, radius * 2, radius * 2), 0, 90);
            g.Dispose();

            this.BackgroundImage = this.staticImage;
        }

        private void UpdateRounde(int x, int y, int width, int height, int radius)
        {
            Pen p1 = new Pen(Color.Green, 2f);
            p1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            Graphics g = Graphics.FromImage(staticImage);


            g.SmoothingMode = SmoothingMode.HighQuality;//去掉锯齿
            g.CompositingQuality = CompositingQuality.HighQuality;//合成图像的质量
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;//去掉文字的锯齿
            g.DrawLine(p1, new PointF(x + radius, y), new PointF(x + width - radius, y));
            g.DrawLine(p1, new PointF(x + radius, y + height), new PointF(x + width - radius, y + height));
            g.DrawLine(p1, new PointF(x, y + radius), new PointF(x, y + height - radius));
            g.DrawLine(p1, new PointF(x + width, y + radius), new PointF(x + width, y + height - radius));
            g.DrawArc(p1, new Rectangle(x, y, radius * 2, radius * 2), 180, 90);
            g.DrawArc(p1, new Rectangle(x + width - radius * 2, y, radius * 2, radius * 2), 270, 90);
            g.DrawArc(p1, new Rectangle(x, y + height - radius * 2, radius * 2, radius * 2), 90, 90);
            g.DrawArc(p1, new Rectangle(x + width - radius * 2, y + height - radius * 2, radius * 2, radius * 2), 0, 90);

            g.Dispose();
            this.BackgroundImage = this.staticImage;
        }
        private void LeftPicture_MouseEnter(object sender, EventArgs e)
        {
            this.nameToolTip.SetToolTip(this.leftPicture, String.Format("元素ID: {0}", this.ID.ToString()));
        }
        public void rectInAdd(int pinIndex)
        {

            if (pinStatus != "rectIn" && !linePinArray.Contains(1))
            {
                rectIn = rectEnter(rectIn);
                linePinArray.Add(1);
                this.Invalidate();
            }

            PinOpLeaveAndEnter(new Point(0, 0));

        }
        public void InPinInit(int endLineIndex)
        {
        }
        private void ExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileUtil.ExploreDirectory(FullFilePath);
        }

        private void CopyFilePathToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileUtil.TryClipboardSetText(FullFilePath);
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.saveFileDialog.FileName = DescriptionName + ".bcp";
            DialogResult dr = this.saveFileDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string srcFilePath = this.FullFilePath;
                string dstFilePath = this.saveFileDialog.FileName;
                try
                {
                    FileInfo file = new FileInfo(srcFilePath);
                    file.CopyTo(dstFilePath, true);
                }
                catch (Exception ex)
                {
                    log.Error("导出文件出错:" + ex.Message);
                }

            }
        }
        public void ControlSelect()
        {
            pen = new Pen(Color.DarkGray, 1.5f);
            double f = Math.Pow(factor, sizeLevel);
            DrawRoundedRect((int)(4 * f), 0, this.Width - (int)(11 * f), this.Height - (int)(2 * f), (int)(3 * f));
            UpdateRounde((int)(4 * f), 0, this.Width - (int)(11 * f), this.Height - (int)(2 * f), (int)(3 * f));
        }


        public void ControlNoSelect()
        {
            pen = new Pen(Color.DarkGray, 1f);
            double f = Math.Pow(factor, sizeLevel);
            DrawRoundedRect((int)(4 * f), 0, this.Width - (int)(11 * f), this.Height - (int)(2 * f), (int)(3 * f));
        }
        public void DeleteMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

