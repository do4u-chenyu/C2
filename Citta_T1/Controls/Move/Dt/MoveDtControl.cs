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
using System.Windows.Forms;


namespace Citta_T1.Controls.Move.Dt
{
    public partial class MoveDtControl : MoveBaseControl, IMoveControl
    {
        private static LogUtil log = LogUtil.GetInstance("MoveDtContorl");
        //绘制引脚
        private string lineStaus = "noLine";
        private Point rightPin = new Point(126, 9);

        private Pen pen = new Pen(Color.DarkGray, 1f);
        private SolidBrush whiteSmokeBrush = new SolidBrush(Color.WhiteSmoke);
        private String pinStatus = "noEnter";

        private ECommandType cmd = ECommandType.Null;
        MoveWrapper moveWrapper;


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

            changeStatus = new Size(0, 28);
            normalStatus = new Size(53, 28);
            InitializeOpPinPicture();
            ChangeSize(sizeL);
            this.moveWrapper = new MoveWrapper();
            oldControlPosition = this.Location;
        }

        #region 重写方法
        private void RightPictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.helpToolTip.SetToolTip(this.rightPictureBox, FullFilePath);
        }

        public void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.GetFlowControl().SelectDrag || (Global.GetFlowControl().SelectFrame && !Global.GetCanvasPanel().DelEnable))
                return;
            this.DeleteMyself();
        }

        private void DeleteMyself()
        {
            CanvasPanel cp = Global.GetCanvasPanel();
            ModelDocument doc = Global.GetCurrentDocument();

            // 状态改变
            doc.StatusChangeWhenDeleteControl(this.ID);
            // 删关系 重置针脚状态
            List<ModelRelation> modelRelations = new List<ModelRelation>(doc.ModelRelations);
            List<Tuple<int, int, int>> relations = new List<Tuple<int, int, int>>();

            foreach (ModelRelation mr in modelRelations)
            {
                if (mr.StartID == this.ID)
                {
                    cp.DeleteRelation(mr);
                    relations.Add(new Tuple<int, int, int>(mr.StartID, mr.EndID, mr.EndPin));
                }
            }
            cp.Invalidate();
            ModelElement me = doc.SearchElementByID(ID);
            BaseCommand cmd = new ElementDeleteCommand(Global.GetCurrentDocument().WorldMap, me, relations);
            UndoRedoManager.GetInstance().PushCommand(doc, cmd);
            // 删控件
            cp.DeleteEle(me);
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
        private void PreViewMenuItem_Click(object sender, EventArgs e)
        {
            Global.GetMainForm().PreViewDataByFullFilePath(this, this.FullFilePath, this.Separator, this.ExtType, this.Encoding);
        }
        #endregion

        protected override void ChangeSize(bool zoomUp, float factor = Global.Factor)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲DoubleBuffer
            ExtensionMethods.SetDouble(this);
            double f = Math.Pow(factor, sizeLevel);
            DrawRoundRect(0, 0, this.Width - (int)(6 * f), this.Height - (int)(1 * f), (int)(3 * f));
            factor = zoomUp ? factor : 1 / factor;

            SetControlsBySize(factor, this);
            this.rectOut = SetRectBySize(factor, this.rectOut);
            this.Invalidate();
        }

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
                int startX = this.Location.X + e.X;
                int startY = this.Location.Y + e.Y;
                MouseEventArgs e1 = new MouseEventArgs(e.Button, e.Clicks, startX, startY, 0);
                Global.GetCanvasPanel().CanvasPanel_MouseMove(this, e1);

                return;
            }
            else
            {
                #region 控件移动部分
                int left = this.Left + e.X - mouseOffset.X;
                int top = this.Top + e.Y - mouseOffset.Y;
                Global.GetCurrentDocument().WorldMap
                      .WorldBoundControl(new Point(left, top), this);
                #endregion
                /*
                 * 1. 遍历所有关系
                 * 2. 如果关系中的startC 是当前控件，则更新关系的坐标
                 * 3. 重绘线
                 */
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
                    this.moveWrapper.DragMove(e);
            }
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
                    int startX = this.Location.X + e.X;
                    int startY = this.Location.Y + e.Y;
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
            this.moveWrapper.DragDown( e);
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
                    int startX = this.Location.X + e.X;
                    int startY = this.Location.Y + e.Y;
                    Global.GetCanvasPanel().CanvasPanel_MouseUp(this, new MouseEventArgs(e.Button, e.Clicks, startX, startY, 0));
                }
                else if (cmd == ECommandType.Hold)
                    this.moveWrapper.DragUp(e);
                Global.GetNaviViewControl().UpdateNaviView();
            }
            cmd = ECommandType.Null;
            if (oldControlPosition != this.Location)
            {
                // 构造移动命令类,压入undo栈
                ModelElement element = Global.GetCurrentDocument().SearchElementByID(ID);
                if (element != ModelElement.Empty)
                {
                    Point oldControlPostionInWorld = Global.GetCurrentDocument().WorldMap.ScreenToWorld(oldControlPosition, true);
                    BaseCommand moveCommand = new ElementMoveCommand(element, oldControlPostionInWorld);
                    UndoRedoManager.GetInstance().PushCommand(Global.GetCurrentDocument(), moveCommand);
                }
                Global.GetMainForm().SetDocumentDirty();
            }
        }
        #endregion

        #region 控件名称长短改变时改变控件大小
        protected override void ResizeControl(int txtWidth, Size controlSize)
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
            DrawRoundRect((int)(4 * f), 0, this.Width - (int)(11 * f), this.Height - (int)(2 * f), (int)(3 * f));
        }

        #endregion

        #region 右键菜单
        private void OptionMenuItem_Click(object sender, EventArgs e)
        {
            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                return;
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
            PinOpLeaveAndEnter(new Point(0, 0));
        }

        public void InPinInit(int endLineIndex)
        {
        }
        #endregion

        #region 接口实现
        public override PointF RevisePointLoc(PointF p)
        {
            // 不存在连DtControl 的 LeftPin的情况
            return p;
        }

        #endregion
        private void MoveDtControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;//去掉锯齿
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;//合成图像的质量
            e.Graphics.FillEllipse(whiteSmokeBrush, rectOut);
            e.Graphics.DrawEllipse(pen, rectOut);
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
            DrawRoundRect((int)(4 * f), 0, this.Width - (int)(11 * f), this.Height - (int)(2 * f), (int)(3 * f));
            UpdateRound((int)(4 * f), 0, this.Width - (int)(11 * f), this.Height - (int)(2 * f), (int)(3 * f));
        }
        public void ControlNoSelect()
        {
            pen = new Pen(Color.DarkGray, 1f);
            double f = Math.Pow(factor, sizeLevel);
            DrawRoundRect((int)(4 * f), 0, this.Width - (int)(11 * f), this.Height - (int)(2 * f), (int)(3 * f));
        }
    }

}
