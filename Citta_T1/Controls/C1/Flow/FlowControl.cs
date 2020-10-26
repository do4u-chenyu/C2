using C2.Core;
using System;
using System.Windows.Forms;

namespace C2.Controls.Flow
{
    public partial class FlowControl : UserControl
    {
        private bool selectRemark;
        private bool selectFrame;
        private bool selectDrag;
        public bool SelectRemark { get => selectRemark; set => selectRemark = value; }
        public bool SelectDrag { get => selectDrag; set => selectDrag = value; }
        public bool SelectFrame { get => selectFrame; set => selectFrame = value; }
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlowControl));
        public FlowControl()
        {
            InitializeComponent();
            SelectDrag = false;
            SelectFrame = false;
            SelectRemark = false;

        }
        // 恢复到编辑模式
        public void ResetStatus()
        {
            SelectDrag = false;
            SelectFrame = false;
            DragChange(SelectDrag);
            FrameChange(SelectFrame);
            ChangeCursor();
        }
        private void ChangeCursor()
        {
            // 拖拽
            if (SelectDrag)
            {
                Global.GetCanvasPanel().Cursor = Cursors.SizeAll;
            }
            // 框选
            else if (SelectFrame)
            {
                Global.GetCanvasPanel().Cursor = Cursors.Default;
            }
            // 编辑
            else
            {
                Global.GetCanvasPanel().Cursor = Cursors.Hand;
            }
            // FlowControl本身的图标不变
            this.Cursor = Cursors.Default;
        }
        #region 拖动

        private void MovePictureBox_MouseEnter(object sender, EventArgs e)
        {
            DragChange(true);

        }
        private void MovePictureBox_Click(object sender, EventArgs e)
        {
            // 1. 点击之后图标变色
            // 2. 鼠标变成手的图标
            // 3. 画布中触发MouseDown MouseMove MouseUp动作
            Global.GetCanvasPanel().ClearAllLineStatus();
            SelectDrag = !SelectDrag;
            SelectFrame = false;
            ChangeCursor();
            FrameChange(SelectFrame);
        }
        private void MovePictureBox_MouseLeave(object sender, EventArgs e)
        {
            DragChange(SelectDrag);
        }
        #endregion

        #region 放大缩小
        private void ZoomUpPictureBox_MouseEnter(object sender, EventArgs e)
        {
            ZoomUpChange(true);
        }

        private void ZoomUpPictureBox_MouseLeave(object sender, EventArgs e)
        {
            ZoomUpChange(false);
        }

        private void ZoomDownPictureBox_MouseEnter(object sender, EventArgs e)
        {
            ZoomDownChange(true);
        }

        private void ZoomDownPictureBox_MouseLeave(object sender, EventArgs e)
        {
            ZoomDownChange(false);
        }

        private void ZoomUpPictureBox_Click(object sender, EventArgs e)
        {
            SelectFrame = false;
            ChangeCursor();
            FrameChange(SelectFrame);
            Global.GetCanvasPanel().FrameWrapper.InitFrame();
            Global.GetCanvasPanel().ChangSize(true);
        }

        private void ZoomDownPictureBox_Click(object sender, EventArgs e)
        {
            SelectFrame = false;
            ChangeCursor();
            FrameChange(SelectFrame);
            Global.GetCanvasPanel().FrameWrapper.InitFrame();
            Global.GetCanvasPanel().ChangSize(false);
        }
        #endregion

        #region 备注
        private void RemarkPictureBox_MouseEnter(object sender, EventArgs e)
        {
            RemarkChange(true);
        }

        private void RemarkPictureBox_MouseLeave(object sender, EventArgs e)
        {
            RemarkChange(SelectRemark);
        }

        private void HideRemarkControl()//单击备注按钮，备注出现和隐藏功能
        {
            Global.GetRemarkControl().Visible = false;
        }

        private void ShowRemarkControl()//单击备注按钮，备注出现和隐藏功能
        {
            Global.GetRemarkControl().Visible = true;
        }

        private void RemarkPictureBox_Click(object sender, EventArgs e)//单击备注按钮，备注出现和隐藏功能
        {
            Global.GetCurrentDocument().RemarkVisible = !Global.GetCurrentDocument().RemarkVisible;
            SelectRemark = Global.GetCurrentDocument().RemarkVisible;
            if (SelectRemark)
                ShowRemarkControl();
            else
                HideRemarkControl();
        }
        #endregion

        #region 框选
        private void FramePictureBox_MouseEnter(object sender, EventArgs e)
        {
            FrameChange(true);
        }

        private void FramePictureBox_MouseLeave(object sender, EventArgs e)
        {
            FrameChange(SelectFrame);
        }

        private void FramePictureBox_Click(object sender, EventArgs e)
        {
            Global.GetCanvasPanel().ClearAllLineStatus();
            SelectFrame = !SelectFrame;
            SelectDrag = false;
            ChangeCursor();
            DragChange(SelectDrag);
        }
        #endregion

        private void DragChange(bool flag)
        {
            if (flag)
            {
                this.movePictureBox.Image = global::C2.Properties.Resources.selectDrag;
                this.movePictureBox.Location = new System.Drawing.Point(124, 2);
                this.movePictureBox.Size = new System.Drawing.Size(35, 29);
                return;
            }
            this.movePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("movePictureBox.Image")));
            this.movePictureBox.Location = new System.Drawing.Point(129, 4);
            this.movePictureBox.Size = new System.Drawing.Size(22, 22);
        }
        private void ZoomUpChange(bool flag)
        {
            if (flag)
            {
                this.zoomUpPictureBox.Image = global::C2.Properties.Resources.selectZoomUp;
                this.zoomUpPictureBox.Location = new System.Drawing.Point(45, 3);
                this.zoomUpPictureBox.Size = new System.Drawing.Size(35, 29);
                return;
            }
            this.zoomUpPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("zoomUpPictureBox.Image")));
            this.zoomUpPictureBox.Location = new System.Drawing.Point(54, 5);
            this.zoomUpPictureBox.Size = new System.Drawing.Size(22, 22);
        }
        private void ZoomDownChange(bool flag)
        {
            if (flag)
            {
                this.zoomDownPictureBox.Image = global::C2.Properties.Resources.selectZoomDown;
                this.zoomDownPictureBox.Location = new System.Drawing.Point(87, 3);
                this.zoomDownPictureBox.Size = new System.Drawing.Size(29, 29);
                return;
            }
            this.zoomDownPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("zoomDownPictureBox.Image")));
            this.zoomDownPictureBox.Location = new System.Drawing.Point(92, 5);
            this.zoomDownPictureBox.Size = new System.Drawing.Size(22, 22);
        }
        public void RemarkChange(bool flag)
        {
            if (flag)
            {
                this.remarkPictureBox.Image = global::C2.Properties.Resources.selectRemark;
                this.remarkPictureBox.Location = new System.Drawing.Point(13, 3);
                this.remarkPictureBox.Size = new System.Drawing.Size(29, 29);
                return;
            }
            this.remarkPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("remarkPictureBox.Image")));
            this.remarkPictureBox.Location = new System.Drawing.Point(18, 5);
            this.remarkPictureBox.Size = new System.Drawing.Size(23, 23);
        }
        private void FrameChange(bool flag)
        {
            Global.GetCurrentDocument().Show();
            Global.GetCanvasPanel().FrameWrapper.InitFrame();
            if (flag)
            {
                this.framePictureBox.Image = global::C2.Properties.Resources.selectFrame;
                this.framePictureBox.Location = new System.Drawing.Point(167, 2);
                this.framePictureBox.Size = new System.Drawing.Size(29, 29);
                return;
            }
            this.framePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("framePictureBox.Image")));
            this.framePictureBox.Location = new System.Drawing.Point(171, 5);
            this.framePictureBox.Size = new System.Drawing.Size(24, 24);
        }
        public void InterruptSelectFrame()
        {
            SelectFrame = false;
            FrameChange(SelectFrame);
            Global.GetCanvasPanel().FrameWrapper.InitFrame();
            Global.GetCanvasPanel().Invalidate();
        }

        private void FlowControl_Load(object sender, EventArgs e)
        {

        }

        private void customOPButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
