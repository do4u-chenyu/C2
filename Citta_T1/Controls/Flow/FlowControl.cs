using System;
using System.Drawing;
using System.Windows.Forms;
using Citta_T1.Properties;
using Citta_T1.Utils;

namespace Citta_T1.Controls.Flow
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
            this.Cursor = Cursors.Help;
        }
        #region 拖动

        private void MovePictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.movePictureBox.BackColor = Color.FromArgb(135, 135, 135);
        }
        private void MovePictureBox_Click(object sender, EventArgs e)
        {
            // 1. 点击之后图标变色
            // 2. 鼠标变成手的图标
            // 3. 画布中触发MouseDown MouseMove MouseUp动作
            SelectDrag = !SelectDrag;
            SelectFrame = false;
            ChangeCursor();
        }
        private void MovePictureBox_MouseLeave(object sender, EventArgs e)
        {
            this.movePictureBox.BackColor = Color.FromArgb(235, 235, 235);
        }
        #endregion

        #region 放大缩小
        private void ZoomUpPictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.zoomUpPictureBox.BackColor = Color.FromArgb(135, 135, 135);
        }

        private void ZoomUpPictureBox_MouseLeave(object sender, EventArgs e)
        {
            this.zoomUpPictureBox.BackColor = Color.FromArgb(235, 235, 235);
        }

        private void ZoomDownPictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.zoomDownPictureBox.BackColor = Color.FromArgb(135, 135, 135);
        }

        private void ZoomDownPictureBox_MouseLeave(object sender, EventArgs e)
        {
            this.zoomDownPictureBox.BackColor = Color.FromArgb(235, 235, 235);
        }

        private void ZoomUpPictureBox_Click(object sender, EventArgs e)
        {
            Global.GetCanvasPanel().ChangSize(true);
        }

        private void ZoomDownPictureBox_Click(object sender, EventArgs e)
        {
            Global.GetCanvasPanel().ChangSize(false);
        }
        #endregion

        #region 备注
        private void RemarkPictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.remarkPictureBox.BackColor = Color.FromArgb(135, 135, 135);
        }

        private void RemarkPictureBox_MouseLeave(object sender, EventArgs e)
        {
            this.remarkPictureBox.BackColor = Color.FromArgb(235, 235, 235);
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
            if (SelectRemark)
                ShowRemarkControl();
            else
                HideRemarkControl();

            SelectRemark = !SelectRemark;

        }
        #endregion

        #region 框选
        private void FramePictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.framePictureBox.BackColor = Color.FromArgb(0, 0, 0);
        }

        private void FramePictureBox_MouseLeave(object sender, EventArgs e)
        {
            this.framePictureBox.BackColor = Color.FromArgb(235, 235, 235);
        }

        private void FramePictureBox_Click(object sender, EventArgs e)
        {
            SelectFrame = !SelectFrame;
            
            SelectDrag = false;
            ChangeCursor();
        }
        #endregion
    }
}
