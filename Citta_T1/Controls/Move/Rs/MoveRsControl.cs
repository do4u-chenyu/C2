using Citta_T1.Business.Model;
using Citta_T1.Business.Schedule;
using Citta_T1.Controls.Interface;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.Core.UndoRedo;
using Citta_T1.Core.UndoRedo.Command;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace Citta_T1.Controls.Move.Rs
{
    public partial class MoveRsControl : MoveBaseControl, IMoveControl
    {
        private static LogUtil log = LogUtil.GetInstance("MoveRsControl");

        private ECommandType cmd = ECommandType.Null;

        List<int> endLineIndexs = new List<int>() { };

        //绘制引脚
        private Point leftPin = new Point(2, 11);
        private Point rightPin = new Point(130, 8);

        private Pen pen = new Pen(Color.DarkGray, 1f);
        private SolidBrush whiteSmokeBrush = new SolidBrush(Color.WhiteSmoke);
        private String pinStatus = "noEnter";
        private List<int> linePinArray = new List<int> { };
        private String lineStatus = "noLine";
        private MoveWrapper moveWrapper;

        public override ElementStatus Status
        {
            get => base.Status;
            set
            {
                base.Status = value;
                StatusDirty();
            }
        }
        public Rectangle RectIn { get; set; }

        public MoveRsControl(int size, string desciption, Point loc)
        {

            InitializeComponent();
            InitializeContextMenuStrip();

            Type = ElementType.Result;
            Description = desciption;
            Location = loc;
            FullFilePath = String.Empty;
            Encoding = OpUtil.Encoding.GBK;
            Separator = OpUtil.DefaultSeparator;
            Status = ElementStatus.Null;

            ChangeSize(size);

            changeStatus = new Size(0, 28);
            normalStatus = new Size(58, 28);

            InitializeOpPinPicture();
            this.moveWrapper = new MoveWrapper();

            endLineIndexs.Add(-1);
            oldControlPosition = this.Location;

        }

        private void InitializeOpPinPicture()
        {
            RectIn = new Rectangle(this.leftPin.X, this.leftPin.Y, this.pinWidth, this.pinHeight);
            rectOut = new Rectangle(this.rightPin.X, this.rightPin.Y, this.pinWidth, this.pinHeight);
            SetOpControlName(Description);
        }

        private void InitializeContextMenuStrip()
        {
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PreviewMenuItem,
            this.RenameMenuItem,
            this.SaveAsToolStripMenuItem,
            this.RunMenuItem,
            this.ErrorLogMenuItem,
            this.toolStripSeparator1,
            this.ExplorerToolStripMenuItem,
            this.CopyFilePathToClipboardToolStripMenuItem});
        }


        #region MOC的事件
        private void MoveRsControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (Global.GetFlowControl().SelectDrag || Global.GetFlowControl().SelectFrame)
                return;
            PinOpLeaveAndEnter(this.PointToClient(MousePosition));
            if (cmd == ECommandType.Null)
                return;
            else if (cmd == ECommandType.PinDraw)
            {
                this.lineStatus = "lineExit";
                int startX = this.Location.X + e.X;
                int startY = this.Location.Y + e.Y;
                MouseEventArgs e1 = new MouseEventArgs(e.Button, e.Clicks, startX, startY, 0);
                Global.GetCanvasPanel().CanvasPanel_MouseMove(this, e1);
                return;
            }
            else
            {
                int left = this.Left + e.X - mouseOffset.X;
                int top = this.Top + e.Y - mouseOffset.Y;
                Global.GetCurrentDocument
                    ().WorldMap.WorldBoundControl(new Point(left, top), this);
                foreach (ModelRelation mr in Global.GetCurrentDocument().ModelRelations)
                {
                    if (mr.StartID == this.ID)
                    {
                        mr.StartP = this.GetStartPinLoc(0);
                        mr.UpdatePoints();
                    }
                    if (mr.EndID == this.ID)
                    {
                        mr.EndP = this.GetEndPinLoc(mr.EndPin);
                        mr.UpdatePoints();
                    }
                }
                this.moveWrapper.DragMove(e);
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
                    int startX = this.Location.X + e.X;
                    int startY = this.Location.Y + e.Y;
                    oldControlPosition = this.Location;
                    MouseEventArgs e1 = new MouseEventArgs(e.Button, e.Clicks, startX, startY, 0);
                    cmd = ECommandType.PinDraw;
                    Global.GetCanvasPanel().CanvasPanel_MouseDown(this, e1);
                    return;
                }
                mouseOffset.X = e.X;
                mouseOffset.Y = e.Y;
                cmd = ECommandType.Hold;
            }
            oldControlPosition = this.Location;
            this.moveWrapper.DragDown(e);
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
                    int startX = this.Location.X + e.X;
                    int startY = this.Location.Y + e.Y;
                    Global.GetCanvasPanel().CanvasPanel_MouseUp(this, new MouseEventArgs(e.Button, e.Clicks, startX, startY, 0));
                    cmd = ECommandType.Null;
                }
                else if (cmd == ECommandType.Hold)
                {
                    this.moveWrapper.DragUp(e);
                    cmd = ECommandType.Null;
                }

                Global.GetNaviViewControl().UpdateNaviView();
                if (oldControlPosition != this.Location)
                {
                    // 构造移动命令类,压入undo栈
                    ModelElement element = Global.GetCurrentDocument().SearchElementByID(ID);
                    if (element != ModelElement.Empty)
                    {   // Command类中存储世界坐标系,避免不同放大系数情况下出现问题
                        Point oldControlPostionInWorld = Global.GetCurrentDocument().WorldMap.ScreenToWorld(oldControlPosition, true);
                        ICommand moveCommand = new ElementMoveCommand(element, oldControlPostionInWorld);
                        UndoRedoManager.GetInstance().PushCommand(Global.GetCurrentDocument(), moveCommand);
                    }
                    Global.GetMainForm().SetDocumentDirty();
                }
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
            this.rightPictureBox.Location = new Point(this.Width - (int)(30 * f), (int)(4 * f));
            this.rectOut.Location = new Point(this.Width - (int)(10 * f), (int)(11 * f));
            this.txtButton.Size = new Size((int)(txtWidth * f), this.Height - (int)(pading * f));
            this.textBox.Size = new Size((int)(txtWidth * f), this.Height - (int)(4 * f));
            DrawRoundRect((int)(4 * f), 0, this.Width - (int)(11 * f), this.Height - (int)(2 * f), (int)(3 * f));
        }


        #endregion

        #region 右键菜单
        private void PreviewMenuItem_Click(object sender, EventArgs e)
        {
            Global.GetMainForm().PreViewDataByFullFilePath(this.FullFilePath, this.Separator, this.ExtType, this.Encoding);
        }

        public void RunMenuItem_Click(object sender, EventArgs e)
        {
            //运行到此
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
            if (currentOp == ModelElement.Empty)
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
            TaskManager currentManager = Global.GetCurrentDocument().TaskManager;
            currentManager.GetCurrentModelTripleList(Global.GetCurrentDocument(), "mid", currentOp);
            Global.GetMainForm().BindUiManagerFunc();

            int notReadyNum = currentManager.CurrentModelTripleStatusNum(ElementStatus.Null);
            if (notReadyNum > 0)
            {
                MessageBox.Show("运行到此路径上有" + notReadyNum + "个未配置的算子，请配置后再运行", "未配置", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (currentManager.IsAllOperatorDone())
            {
                MessageBox.Show("运行到此路径上的算子均已运算完毕，重新运算需要先点击‘重置’按钮。", "运算完毕", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            currentManager.Start();
            Global.GetMainForm().UpdateRunbuttonImageInfo();

        }
        #endregion

        private void RightPictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.helpToolTip.SetToolTip(this.rightPictureBox, this.FullFilePath);
        }

        #region 针脚事件
        private void PinOpLeaveAndEnter(Point mousePosition)
        {
            if (RectIn.Contains(mousePosition))
            {
                if (pinStatus == "rectIn" || linePinArray.Contains(1)) return;
                RectIn = RectEnter(RectIn);
                this.Invalidate();
                pinStatus = "rectIn";
            }
            else if (rectOut.Contains(mousePosition) || this.lineStatus == "lineExit")
            {
                if (pinStatus == "rectOut") return;
                rectOut = RectEnter(rectOut);
                this.Invalidate();
                pinStatus = "rectOut";
            }
            else if (pinStatus != "noEnter")
            {
                switch (pinStatus)
                {
                    case "rectIn":
                        RectIn = RectLeave(RectIn);
                        break;
                    case "rectOut":
                        rectOut = RectLeave(rectOut);
                        break;
                }
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
            this.lineStatus = status;
            PinOpLeaveAndEnter(new Point(0, 0));
        }
        #endregion

        #region 托块的放大与缩小
        protected override void ChangeSize(bool zoomUp, float factor = Global.Factor)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲DoubleBuffer
            ExtensionMethods.SetDouble(this);
            double f = Math.Pow(factor, sizeLevel);
            DrawRoundRect((int)(4 * f), 0, this.Width - (int)(11 * f), this.Height - (int)(2 * f), (int)(3 * f));

            factor = zoomUp ? factor : 1 / factor;

            SetControlsBySize(factor, this);
            this.rectOut = SetRectBySize(factor, this.rectOut);
            this.RectIn = SetRectBySize(factor, this.RectIn);
            this.Invalidate();
        }
        #endregion

        #region 状态改变
        private void StatusDirty()
        {
            if (this.Status == ElementStatus.Null)
                this.leftPictureBox.Image = Properties.Resources.resultNull;
            else if (this.Status == ElementStatus.Done)
                this.leftPictureBox.Image = Properties.Resources.resultDone;
            // 状态改变, 需要设置BCP缓冲dirty，以便预览时重新加载
            if (System.IO.File.Exists(this.FullFilePath))
                BCPBuffer.GetInstance().SetDirty(this.FullFilePath);
        }


        #endregion
        private void MoveOpControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;//去掉锯齿
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;//合成图像的质量
            e.Graphics.FillEllipse(whiteSmokeBrush, RectIn);
            e.Graphics.DrawEllipse(pen, RectIn);
            e.Graphics.FillEllipse(whiteSmokeBrush, rectOut);
            e.Graphics.DrawEllipse(pen, rectOut);
        }

        #region IMoveControl接口
        // 修正坐标
        public override PointF RevisePointLoc(PointF p)
        {
            return p;
        }

        public override PointF GetEndPinLoc(int pinIndex)
        {
            return new PointF(
                this.Location.X + this.RectIn.Location.X + this.RectIn.Width / 2,
                this.Location.Y + this.RectIn.Location.Y + this.RectIn.Height / 2);
        }

        #endregion

        public void RectInAdd(int pinIndex)
        {

            if (pinStatus != "rectIn" && !linePinArray.Contains(1))
            {
                RectIn = RectEnter(RectIn);
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

            this.saveFileDialog.FileName = Description + ".bcp";
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
            DrawRoundRect((int)(4 * f), 0, this.Width - (int)(11 * f), this.Height - (int)(2 * f), (int)(3 * f));
            UpdateRound((int)(4 * f), 0, this.Width - (int)(11 * f), this.Height - (int)(2 * f), (int)(3 * f));
        }


        public void ControlNoSelect()
        {
            pen = new Pen(Color.DarkGray, 1f);
            double f = Math.Pow(factor, sizeLevel);
            DrawRoundRect((int)(4 * f), 0, this.Width - (int)(11 * f), this.Height - (int)(2 * f), (int)(3 * f));
        }
        public void DeleteMenuItem_Click(object sender, EventArgs e)
        {

        }
        public override void UndoRedoDeleteElement(ModelElement me, List<Tuple<int, int, int>> relations = null, ModelElement opEle = null)
        {
            /*
             * 1. 删自身
             * 2. 删与之相连的关系
             * 3. 删与之相连的结果控件
             * 4. 改变其他控件的Pin状态
             */
            CanvasPanel cp = Global.GetCanvasPanel();
            cp.DeleteEle(me);
            if (relations != null)
            {
                foreach (Tuple<int, int, int> rel in relations)
                    cp.DeleteRelationByCtrID(rel.Item1, rel.Item2, rel.Item3);
            }
            opEle.Status = ElementStatus.Null;
            (opEle.InnerControl as MoveOpControl).Option.Clear();
        }
        public void UndoRedoAddElement(ModelElement me, List<Tuple<int, int, int>> relations, ModelElement opEle, ElementStatus status, Dictionary<string, string> opOptDict)
        {
            /*
             * 1. 恢复自身
             * 2. 恢复与之相连的关系
             * 3. 恢复与之相连的结果控件
             * 4. 改变其他控件的Pin状态
             */
            CanvasPanel cp = Global.GetCanvasPanel();
            cp.AddEle(me);
            foreach (Tuple<int, int, int> rel in relations)
                cp.AddNewRelationByCtrID(rel.Item1, rel.Item2, rel.Item3);
            opEle.Status = status;
            (opEle.InnerControl as MoveOpControl).Option.OptionDict = new Dictionary<string, string>(opOptDict);
        }
    }
}

