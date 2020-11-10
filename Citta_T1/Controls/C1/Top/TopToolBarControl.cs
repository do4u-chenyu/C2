using C2.Business.Model;
using C2.Business.Model.World;
using C2.Core;
using C2.Core.UndoRedo;
using C2.Core.UndoRedo.Command;
using C2.Dialogs;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace C2.Controls.Top
{
    public partial class TopToolBarControl : UserControl
    {
        [Browsable(false)]
        public bool SelectRemark { get; set; } = false;
        [Browsable(false)]
        public bool SelectDrag { get; set; } = false;
        [Browsable(false)]
        public bool SelectFrame { get; set; } = false;

        public TopToolBarControl()
        {
            InitializeComponent();
            InitializeToolTip();
            InitializeUndoRedoManger();
        } // 恢复到编辑模式
        public void ResetStatus()
        {
            SelectDrag = false;
            SelectFrame = false;
            FrameChange();
            ChangeCursor();
        }
        private void ChangeCursor()
        {
            // 拖拽
            if (SelectDrag)
            {
                Global.GetCanvasPanel().Cursor = Helper.LoadCursor(Properties.Resources.hand_cur);
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
        }
        #region 拖动

        private void MovePictureBox_Click(object sender, EventArgs e)
        {
            // 1. 点击之后图标变色
            // 2. 鼠标变成手的图标
            // 3. 画布中触发MouseDown MouseMove MouseUp动作
            Global.GetCanvasPanel().ClearAllLineStatus();
            SelectDrag = !SelectDrag;
            SelectFrame = false;
            ChangeCursor();
            FrameChange();
            this.movePictureBox.BackColor = Color.FromArgb(200, 200, 200);
            this.framePictureBox.BackColor = Color.FromArgb(230, 237, 246);
        }

        #endregion

        #region 放大缩小
       
        private void ZoomUpPictureBox_Click(object sender, EventArgs e)
        {
            SelectFrame = false;
            ChangeCursor();
            FrameChange();
            Global.GetCanvasPanel().FrameWrapper.InitFrame();
            Global.GetCanvasPanel().ChangSize(true);
            this.framePictureBox.BackColor = Color.FromArgb(230, 237, 246);
        }

        private void ZoomDownPictureBox_Click(object sender, EventArgs e)
        {
            SelectFrame = false;
            ChangeCursor();
            FrameChange();
            Global.GetCanvasPanel().FrameWrapper.InitFrame();
            Global.GetCanvasPanel().ChangSize(false);
            this.framePictureBox.BackColor = Color.FromArgb(230, 237, 246);
        }
        #endregion

        #region 备注

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
            Global.GetCurrentModelDocument().RemarkVisible = !Global.GetCurrentModelDocument().RemarkVisible;
            SelectRemark = Global.GetCurrentModelDocument().RemarkVisible;
            if (SelectRemark)
                ShowRemarkControl();
            else
                HideRemarkControl();
        }
        #endregion

        #region 框选

        private void FramePictureBox_Click(object sender, EventArgs e)
        {
            Global.GetCanvasPanel().ClearAllLineStatus();
            SelectFrame = !SelectFrame;
            SelectDrag = false;
            ChangeCursor();

            this.movePictureBox.BackColor = Color.FromArgb(230, 237, 246);
            this.framePictureBox.BackColor = Color.FromArgb(200, 200, 200);
        }
        #endregion

        private void FrameChange()
        {
            Global.GetCurrentModelDocument().Show();
            Global.GetCanvasPanel().FrameWrapper.InitFrame();
        }
        public void InterruptSelectFrame()
        {
            SelectFrame = false;
            FrameChange();
            Global.GetCanvasPanel().FrameWrapper.InitFrame();
            Global.GetCanvasPanel().Invalidate();
        }

        private void InitializeUndoRedoManger()
        {
            UndoRedoManager.GetInstance().RedoStackEmpty += TopToolBarControl_RedoStackEmpty;
            UndoRedoManager.GetInstance().RedoStackNotEmpty += TopToolBarControl_RedoStackNotEmpty;
            UndoRedoManager.GetInstance().UndoStackEmpty += TopToolBarControl_UndoStackEmpty;
            UndoRedoManager.GetInstance().UndoStackNotEmpty += TopToolBarControl_UndoStackNotEmpty;
        }

        private void TopToolBarControl_UndoStackNotEmpty()
        {
            this.undoButton.Enabled = true;
        }

        private void TopToolBarControl_UndoStackEmpty()
        {
            this.undoButton.Enabled = false;
        }

        private void TopToolBarControl_RedoStackNotEmpty()
        {
            this.redoButton.Enabled = true;
        }

        private void TopToolBarControl_RedoStackEmpty()
        {
            this.redoButton.Enabled = false;
        }

        private void InitializeToolTip()
        {
            this.saveModelButton.ToolTipText = HelpUtil.SaveModelButtonHelpInfo;
            this.saveAllButton.ToolTipText = HelpUtil.SaveAllButtonHelpInfo;
            this.undoButton.ToolTipText = HelpUtil.UndoButtonHelpInfo;
            this.redoButton.ToolTipText = HelpUtil.RedoButtonHelpInfo;
            this.ImportModel.ToolTipText = HelpUtil.ImportModelHelpInfo;
            this.formatButton.ToolTipText = HelpUtil.FormatOperatorHelpInfo;
            this.remarkPictureBox.ToolTipText = HelpUtil.RemarkPictureBoxHelpInfo;
            this.zoomUpPictureBox.ToolTipText = HelpUtil.ZoomUpPictureBoxHelpInfo;
            this.zoomDownPictureBox.ToolTipText = HelpUtil.zoomDownPictureBoxHelpInfo;
            this.movePictureBox.ToolTipText = HelpUtil.MovePictureBoxHelpInfo;
            this.framePictureBox.ToolTipText = HelpUtil.FramePictureBoxHelpInfo;
            this.moreButton.ToolTipText = HelpUtil.moreButtonHelpInfo;
        }

        public void UndoButton_Click(object sender, EventArgs e)
        {
            UndoRedoManager.GetInstance().Undo(Global.GetCurrentModelDocument());
            Global.GetMainForm().SetDocumentDirty();
            this.framePictureBox.BackColor = Color.FromArgb(230, 237, 246);
            this.movePictureBox.BackColor = Color.FromArgb(230, 237, 246);
        }

        public void RedoButton_Click(object sender, EventArgs e)
        {
            UndoRedoManager.GetInstance().Redo(Global.GetCurrentModelDocument());
            Global.GetMainForm().SetDocumentDirty();
            this.framePictureBox.BackColor = Color.FromArgb(230, 237, 246);
            this.movePictureBox.BackColor = Color.FromArgb(230, 237, 246);
        }

        private void ImportModel_Click(object sender, EventArgs e)
        {
            C2.Business.Model.ImportModel.GetInstance().ImportIaoFile(Global.GetMainForm().UserName);
        }

        private void SaveModelButton_Click(object sender, EventArgs e)
        {
            Global.GetCurrentModelDocument().Save();
        }

        private void FormatButton_Click(object sender, EventArgs e)
        {
            ModelDocument currentModel = Global.GetCurrentModelDocument();
            WorldMap curWorldMap = Global.GetCurrentModelDocument().WorldMap;
            // 文档为空时,返回,不需要触发dirty动作
            if (currentModel.ModelElements.Count == 0)
                return;
            Dictionary<int, Point> idPtsDict = new Dictionary<int, Point>();
            idPtsDict = ControlUtil.SaveElesWorldCord(currentModel.ModelElements);
            BaseCommand cmd = new BatchMoveCommand(idPtsDict);
            UndoRedoManager.GetInstance().PushCommand(Global.GetCurrentModelDocument(), cmd);

            QuickformatWrapper quickformatWrapper = new QuickformatWrapper(currentModel);
            quickformatWrapper.TreeGroup();
            Global.GetMainForm().SetDocumentDirty();
            this.movePictureBox.BackColor = Color.FromArgb(230, 237, 246);
            this.framePictureBox.BackColor = Color.FromArgb(230, 237, 246);
        }

        private void MoreButton_Click(object sender, EventArgs e)
        {
            ConfigForm config = new ConfigForm();
            config.ShowDialog();
        }
    }
}
