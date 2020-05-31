using Citta_T1.Business.Model;
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
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Citta_T1.Controls.Move.Dt
{
    public delegate void DtDocumentDirtyEventHandler();
    public partial class MoveDtControl: MoveBaseControl, IScalable, IMoveControl
    {
        private static LogUtil log = LogUtil.GetInstance("MoveDtContorl");
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveDtControl));
        private Point oldControlPosition;

        //绘制引脚
        private string lineStaus = "noLine";
        private Point rightPin = new Point(126, 9);
        private int pinWidth = 6;
        private int pinHeight = 6;
        private Pen pen = new Pen(Color.DarkGray, 1f);
        private SolidBrush trnsRedBrush = new SolidBrush(Color.WhiteSmoke);
        public Rectangle rectOut;
        private String pinStatus = "noEnter";
        private Bitmap staticImage;

        private Size changeStatus = new Size(0, 28);
        private Size normalStatus = new Size(53, 28);

        #region 继承属性
        private Point mouseOffset;
        // 一些倍率
        // 画布上的缩放倍率
        float factor = Global.Factor;
        // 缩放等级
        private int sizeLevel = 0;
        // 绘制贝塞尔曲线的起点
        private int startX;
        private int startY;
        #endregion

        private ECommandType cmd = ECommandType.Null;

        ControlMoveWrapper controlMoveWrapper;


        public MoveDtControl(string bcpPath, int sizeL, string name, Point loc,
            char separator = '\t',
            OpUtil.Encoding encoding = OpUtil.Encoding.UTF8 
            )
        {
            InitializeComponent();
            InitializeContextMenuStrip();

            this.Type = ElementType.DataSource;
            this.Description = name;
            this.Location = loc;
            this.FullFilePath = bcpPath;
            this.Encoding = encoding;
            this.Separator = separator;
            this.Status = ElementStatus.Done;

            InitializeOpPinPicture();
            ChangeSize(sizeL);
            this.controlMoveWrapper = new ControlMoveWrapper(this);

        }


        #region 重写方法
        public void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                return;
            // 按下回车键
            if (e.KeyChar == 13)
            {
                FinishTextChange();
            }
        }
        public void TextBox_Leave(object sender, EventArgs e)
        {
            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                return;
            FinishTextChange();
        }

        private void FinishTextChange()
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
            if (element != ModelElement.Empty)
            {
                ICommand renameCommand = new ElementRenameCommand(element, oldTextString);
                UndoRedoManager.GetInstance().PushCommand(Global.GetCurrentDocument(), renameCommand);
            }
            

            this.oldTextString = this.textBox.Text;
            Global.GetMainForm().SetDocumentDirty(); 
            Global.GetCurrentDocument().UpdateAllLines();
            Global.GetCanvasPanel().Invalidate(false);
        }

        public string UndoRedoChangeTextName(string des)
        {
            this.oldTextString = Description;
            Description = des;
            SetOpControlName(Description);
            Global.GetCurrentDocument().UpdateAllLines();
            Global.GetCanvasPanel().Invalidate(false);
            return oldTextString;
        }

        public void RightPictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.helpToolTip.SetToolTip(this.rightPictureBox, FullFilePath);
        }

        public void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.GetFlowControl().SelectDrag || (Global.GetFlowControl().SelectFrame && ! Global.GetCanvasPanel().DelEnable))
                return;
            Global.GetCurrentDocument().StatusChangeWhenDeleteControl(this.ID);
            List<ModelRelation> modelRelations = new List<ModelRelation>(Global.GetCurrentDocument().ModelRelations);
            foreach (ModelRelation mr in modelRelations)
            {
                if (mr.StartID == this.ID)
                {
                    ModelDocument doc = Global.GetCurrentDocument();
                    doc.RemoveModelRelation(mr);
                    Control lineEndC = doc.SearchElementByID(mr.EndID).InnerControl;
                    (lineEndC as IMoveControl).InPinInit(mr.EndPin);
                    Global.GetCanvasPanel().Invalidate();
                }
            }
            ICommand cmd = new ElementDeleteCommand(Global.GetCurrentDocument().SearchElementByID(ID));
            UndoRedoManager.GetInstance().PushCommand(Global.GetCurrentDocument(), cmd);
            DeleteMyself();
        }

        public void UndoRedoDeleteElement()
        {
            //TODO undo,redo元素时关系的处理
            DeleteMyself();
        }

        private void DeleteMyself()
        {
            Global.GetCanvasPanel().DeleteElement(this);
            Global.GetCurrentDocument().DeleteModelElement(this);
            Global.GetMainForm().SetDocumentDirty();
            Global.GetNaviViewControl().UpdateNaviView();
        }

        public void UndoRedoAddElement(ModelElement me)
        {
            //TODO undo,redo元素时关系的处理
            Global.GetCanvasPanel().AddElement(this);
            Global.GetCurrentDocument().AddModelElement(me);
            Global.GetMainForm().SetDocumentDirty();
            Global.GetNaviViewControl().UpdateNaviView();
        }
        #endregion

        #region 新方法

        private void InitializeOpPinPicture()
        {
            rectOut = new Rectangle(this.rightPin.X, this.rightPin.Y, this.pinWidth, this.pinHeight);
            SetOpControlName(this.textBox.Text);
        }

        private void InitializeContextMenuStrip()
        {
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PreviewMenuItem,
            this.OptionMenuItem,
            this.RenameMenuItem,
            this.RunMenuItem,
            this.LogMenuItem,
            this.DeleteMenuItem,
            this.toolStripSeparator1,
            this.ExplorerToolStripMenuItem,
            this.CopyFilePathToClipboardToolStripMenuItem});
        }
        public void PreViewMenuItem_Click(object sender, EventArgs e)
        {
            Global.GetMainForm().PreViewDataByFullFilePath(this.FullFilePath, this.Separator, this.ExtType, this.Encoding);
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

        private void ChangeSize(bool zoomUp, float factor = Global.Factor)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲DoubleBuffer

            ExtensionMethods.SetDouble(this);
            DrawRoundedRect(0, 0, this.Width - (int)(6 * Math.Pow(factor, sizeLevel)), this.Height - (int)(1 * Math.Pow(factor, sizeLevel)), (int)(3 * Math.Pow(factor, sizeLevel)));
            if (zoomUp)
            {
                SetControlsBySize(factor, this);
                this.rectOut = SetRectBySize(factor, this.rectOut);
                this.Invalidate();
            }
            else
            {
                SetControlsBySize(1 / factor, this);
                this.rectOut = SetRectBySize(1 / factor, this.rectOut);
            }

        }
        //int i = 0;
        #region MOC的事件
        private void MoveDtControl_MouseMove(object sender, MouseEventArgs e)
        {
            bool isNeedMoveLine = false;
            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                return;
            PinOpLeaveAndEnter(this.PointToClient(MousePosition));
            if (cmd == ECommandType.Null)
                return;

            // 开始划线
            else if (cmd == ECommandType.PinDraw)
            {
                startX = this.Location.X + e.X;
                startY = this.Location.Y + e.Y;
                MouseEventArgs e1 = new MouseEventArgs(e.Button, e.Clicks, startX, startY, 0);
                Global.GetCanvasPanel().CanvasPanel_MouseMove(this, e1);
                
                return;
            }
            else
            {
                #region 控件移动部分
                int left = this.Left + e.X - mouseOffset.X;
                int top = this.Top + e.Y - mouseOffset.Y;
                this.Location = WorldBoundControl(new Point(left, top));

                #endregion
                /*
                 * 1. 遍历所有关系
                 * 2. 如果关系中的startC 是当前控件，则更新关系的坐标
                 * 3. 重绘线
                 */
                CanvasPanel canvas = Global.GetCanvasPanel();
                foreach (ModelRelation mr in Global.GetCurrentDocument().ModelRelations)
                {
                    if (mr.StartID == this.ID)
                    {
                        mr.StartP = this.GetStartPinLoc(0);
                        mr.UpdatePoints();
                        isNeedMoveLine = true;
                    }
                }
                if (isNeedMoveLine)
                    this.controlMoveWrapper.DragMove(this.Size, Global.GetCanvasPanel().ScreenFactor, e);
            }
        }

        public Point WorldBoundControl(Point Pm)
        {

            Point Pw = Global.GetCurrentDocument().WorldMap.ScreenToWorld(Pm,false);


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
            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                return;
            if (e.Button == MouseButtons.Left)
            {
                if (rectOut.Contains(e.Location))
                {
                    lineStaus = "lineExit";
                    startX = this.Location.X + e.X;
                    startY = this.Location.Y + e.Y;
                    oldControlPosition = this.Location;
                    cmd = ECommandType.PinDraw;
                    Global.GetCanvasPanel().CanvasPanel_MouseDown(this, new MouseEventArgs(e.Button, e.Clicks, startX, startY, 0));
                    return;
                }
                mouseOffset.X = e.X;
                mouseOffset.Y = e.Y;
                cmd = ECommandType.Hold;
            }
            oldControlPosition = this.Location;
            this.controlMoveWrapper.DragDown(this.Size, Global.GetCanvasPanel().ScreenFactor, e);
        }


        private void TxtButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                return;
            // 单击鼠标, 移动控件
            if (e.Clicks == 1)
                MoveDtControl_MouseDown(sender, e);
            // 双击鼠标, 改名字
            if (e.Clicks == 2)
                RenameMenuItem_Click(this, e);
            oldControlPosition = this.Location;
        }

        private void MoveDtControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                return;
            if (e.Button == MouseButtons.Left)
            {
                if (cmd == ECommandType.PinDraw)
                {
                    startX = this.Location.X + e.X;
                    startY = this.Location.Y + e.Y;
                    Global.GetCanvasPanel().CanvasPanel_MouseUp(this, new MouseEventArgs(e.Button, e.Clicks, startX, startY, 0));
                    cmd = ECommandType.Null;
                }
                if (cmd == ECommandType.Hold)
                {
                    this.controlMoveWrapper.DragUp(this.Size, Global.GetCanvasPanel().ScreenFactor, e);
                    cmd = ECommandType.Null;
                }
                cmd = ECommandType.Null;

                Global.GetNaviViewControl().UpdateNaviView();
                if (oldControlPosition != this.Location)
                {
                    // 构造移动命令类,压入undo栈
                    ModelElement element = Global.GetCurrentDocument().SearchElementByID(ID);
                    if (element != ModelElement.Empty)
                    {
                        Point oldControlPostionInWorld = Global.GetCurrentDocument().WorldMap.ScreenToWorld(oldControlPosition,false);
                        ICommand moveCommand = new ElementMoveCommand(element, oldControlPostionInWorld);
                        UndoRedoManager.GetInstance().PushCommand(Global.GetCurrentDocument(), moveCommand);
                    }

                    Global.GetMainForm().SetDocumentDirty();
                }
            }

        }

        public Point UndoRedoMoveLocation(Point location)
        {
            this.oldControlPosition = this.Location;
            this.Location = Global.GetCurrentDocument().WorldMap.WorldToScreen(location);
            Global.GetNaviViewControl().UpdateNaviView();
            Global.GetMainForm().SetDocumentDirty();
            return Global.GetCurrentDocument().WorldMap.ScreenToWorld(oldControlPosition,false);
        }

        #endregion

        #region 控件名称长短改变时改变控件大小
        private string SubstringByte(string text, int startIndex, int length)
        {
            byte[] bytes = ConvertUtil.GB2312.GetBytes(text);
            if (bytes.Length < length)
                length = bytes.Length;
            return ConvertUtil.GB2312.GetString(bytes, startIndex, length);
        }
        private int CountTextWidth(int chineseRatio, int otherRatio)
        {
            int padding = 3;
            int addValue = 10;
            if ((chineseRatio + otherRatio == 1) && (chineseRatio != 0))
                addValue -= 10;
            return padding * 2 + chineseRatio * 12 + otherRatio * 7 + addValue;
        }
        private void SetOpControlName(string name)
        {
            this.Description = name;
            int maxLength = 24;
            name = SubstringByte(name, 0, maxLength);
            int sumCount = Regex.Matches(name, "[\u4E00-\u9FA5]").Count;
            int sumCountDigit = Regex.Matches(name, "[a-zA-Z0-9]").Count;
            int txtWidth = CountTextWidth(sumCount, sumCountDigit);
            this.txtButton.Text = name;
            if (ConvertUtil.GB2312.GetBytes(this.Description).Length > maxLength)
            {
                txtWidth += 10;
                this.txtButton.Text = name + "...";
            }
            changeStatus.Width = normalStatus.Width + txtWidth;
            ResizeControl(txtWidth, changeStatus);
            this.helpToolTip.SetToolTip(this.txtButton, this.Description);
        }

        private void ResizeControl(int txtWidth, Size controlSize)
        {
            double f = Math.Pow(factor, sizeLevel);
            int pading = 4;
            if (f != 1)
                pading += 1;
            this.Size = new Size((int)(controlSize.Width * f), (int)(controlSize.Height * f));
            this.rightPictureBox.Location = new Point(this.Width - (int)(25 * f), (int)(5 * f));
            this.rectOut.Location = new Point(this.Width - (int)(10 * f), (int)(10 * f));
            this.txtButton.Size = new Size((int)(txtWidth * f), this.Height - (int)(pading * f));
            this.textBox.Size = new Size((int)(txtWidth * f), this.Height - (int)(4 * f));
            DrawRoundedRect((int)(4 * f), 0, this.Width - (int)(11 * f), this.Height - (int)(2 * f), (int)(3 * f));
        }

        #endregion

        #region 右键菜单
        private void OptionMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                return;
        }

        private void RenameMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                return;
            this.textBox.ReadOnly = false;
            this.oldTextString = this.textBox.Text;
            this.txtButton.Visible = false;
            this.textBox.Visible = true;
            this.textBox.Focus();//获取焦点
            this.textBox.Select(this.textBox.TextLength, 0);
        }
        #endregion

        #region 针脚事件
        private void PinOpLeaveAndEnter(Point mousePosition)
        {
            if (rectOut.Contains(mousePosition) || lineStaus == "lineExit")
            {
                if (pinStatus == "rectOut") return;
                rectOut = RectEnter(rectOut);
                pinStatus = "rectOut";
                this.Invalidate();
               
            }
            else if (pinStatus != "noEnter")
            {
                rectOut = RectLeave(rectOut);
                pinStatus = "noEnter";
                this.Invalidate();
            }
        }
        public Rectangle RectEnter(Rectangle rect)
        {
            Point oriLtCorner = rect.Location;
            Size oriSize = rect.Size;
            Point oriCenter = new Point(oriLtCorner.X + oriSize.Width / 2, oriLtCorner.Y + oriSize.Height / 2);
            Point dstLtCorner = new Point(oriCenter.X - oriSize.Width / 2 - 1, oriCenter.Y - oriSize.Height / 2 - 1);
            Size dstSize = new Size(oriSize.Width + 2, oriSize.Height + 2);
            return new Rectangle(dstLtCorner, dstSize);
        }
        public Rectangle RectLeave(Rectangle rect)
        {
            Point oriLtCorner = rect.Location;
            Size oriSize = rect.Size;
            Point oriCenter = new Point(oriLtCorner.X + oriSize.Width / 2, oriLtCorner.Y + oriSize.Height / 2);
            Point dstLtCorner = new Point(oriCenter.X - oriSize.Width / 2 + 1, oriCenter.Y - oriSize.Height / 2 + 1);
            Size dstSize = new Size(oriSize.Width - 2, oriSize.Height - 2);
            return new Rectangle(dstLtCorner, dstSize);
        }

        public void OutPinInit(String status)
        {
            this.lineStaus = status;
            PinOpLeaveAndEnter(new Point(0,0));
        }

        public void InPinInit(int endLineIndex)
        {
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

        #region 接口实现
        /*
         * 当空间移动的时候，更新该控件连接线的坐标
         */
        public void UpdateLineWhenMoving()
        {

        }
        public void SaveStartLines(int line_index)
        {
            //this.startLineIndexs.Add(line_index);
        }

        public void SaveEndLines(int line_index)
        {
            
        }

        public PointF RevisePointLoc(PointF p)
        {
            // 不存在连DtControl 的 LeftPin的情况
            return p;
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
        #endregion
        private void MoveDtControl_Paint(object sender, PaintEventArgs e)
        {
            
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;//去掉锯齿
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;//合成图像的质量
            e.Graphics.FillEllipse(trnsRedBrush, rectOut);
            e.Graphics.DrawEllipse(pen, rectOut);
        }

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
            p1.DashStyle = DashStyle.Dash;
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
            this.helpToolTip.SetToolTip(this.leftPictureBox, String.Format("元素ID: {0}", this.ID.ToString()));
        }

        public void RectInAdd(int pinIndex)
        {

        }

        private void CopyFilePathToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileUtil.TryClipboardSetText(FullFilePath);
        }

        private void ExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileUtil.ExploreDirectory(FullFilePath);
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
    }

}
