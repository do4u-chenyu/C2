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
        public bool MoveColor { get; set; } = false;
        [Browsable(false)]
        public bool FrameColor { get; set; } = false;

        public TopToolBarControl()
        {
            InitializeComponent();
            InitializeToolTip();
            InitializeOther();
        } // 恢复到编辑模式

        private void InitializeOther()
        {
            this.toolStrip1.Renderer = DefaultToolStripRenderer.Default;
        }

        public void ResetStatus()
        {
            SelectDrag = false;
            SelectFrame = false;
            FrameChange();
            ChangeCursor();
            ChangeCursorColor();


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
        private void ChangeCursorColor()
        {
            // 拖拽
            if (SelectDrag)
            {
                this.MoveButton.Checked = true;
                this.FrameButton.Checked = false;

            }
            // 框选
            else if (SelectFrame)
            {
                this.FrameButton.Checked = true;
                this.MoveButton.Checked = false;
            }
            // 编辑
            else
            {
                this.FrameButton.Checked = false;
                this.MoveButton.Checked = false;
            }
        }
        #region 拖动

        private void MoveButton_Click(object sender, EventArgs e)
        {
            // 1. 点击之后图标变色
            // 2. 鼠标变成手的图标
            // 3. 画布中触发MouseDown MouseMove MouseUp动作
            Global.GetCanvasPanel().ClearAllLineStatus();
            SelectDrag = !SelectDrag;
            SelectFrame = false;
            ChangeCursor();
            FrameChange();
            ChangeCursorColor();

        }

        #endregion

        #region 放大缩小
       
        private void ZoomUpButton_Click(object sender, EventArgs e)
        {
            SelectFrame = false;
            ChangeCursor();
            FrameChange();
            Global.GetCanvasPanel().FrameWrapper.InitFrame();
            Global.GetCanvasPanel().ChangSize(true);
        }

        private void ZoomDownButton_Click(object sender, EventArgs e)
        {
            SelectFrame = false;
            ChangeCursor();
            FrameChange();
            Global.GetCanvasPanel().FrameWrapper.InitFrame();
            Global.GetCanvasPanel().ChangSize(false);
        }
        #endregion

        #region 备注
        private void RemarkButton_Click(object sender, EventArgs e)//单击备注按钮，备注出现和隐藏功能
        {
            Global.GetCurrentModelDocument().RemarkVisible = !Global.GetCurrentModelDocument().RemarkVisible;
            SelectRemark = Global.GetCurrentModelDocument().RemarkVisible;
            Global.GetRemarkControl().Visible = SelectRemark;
        }
        #endregion

        #region 框选
        private void FrameButton_Click(object sender, EventArgs e)
        {
            Global.GetCanvasPanel().ClearAllLineStatus();
            SelectFrame = !SelectFrame;
            SelectDrag = false;
            ChangeCursor();
            FrameChange();
            ChangeCursorColor();
 
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


        private void InitializeToolTip()
        {
            this.SaveModelButton.ToolTipText = HelpUtil.SaveModelButtonHelpInfo;
            this.UndoButton.ToolTipText = HelpUtil.UndoButtonHelpInfo;
            this.RedoButton.ToolTipText = HelpUtil.RedoButtonHelpInfo;
            this.ImportModelButton.ToolTipText = HelpUtil.ImportModelHelpInfo;
            this.FormatButton.ToolTipText = HelpUtil.FormatOperatorHelpInfo;
            this.RemarkButton.ToolTipText = HelpUtil.RemarkPictureBoxHelpInfo;
            this.ZoomUpButton.ToolTipText = HelpUtil.ZoomUpPictureBoxHelpInfo;
            this.ZoomDownButton.ToolTipText = HelpUtil.zoomDownPictureBoxHelpInfo;
            this.MoveButton.ToolTipText = HelpUtil.MovePictureBoxHelpInfo;
            this.FrameButton.ToolTipText = HelpUtil.FramePictureBoxHelpInfo;
            this.MoreButton.ToolTipText = HelpUtil.moreButtonHelpInfo;
        }

        public void UndoButton_Click(object sender, EventArgs e)
        {
            UndoRedoManager.GetInstance().Undo(Global.GetCurrentModelDocument());
            Global.GetMainForm().SetDocumentDirty();
        }

        public void RedoButton_Click(object sender, EventArgs e)
        {
            UndoRedoManager.GetInstance().Redo(Global.GetCurrentModelDocument());
            Global.GetMainForm().SetDocumentDirty();
        }

        private void ImportModel_Click(object sender, EventArgs e)
        {
            ImportModel.GetInstance().ImportIaoFile(Global.GetMainForm().UserName);
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
        }

        private void MoreButton_Click(object sender, EventArgs e)
        {
            new ConfigForm().ShowDialog();
        }
        public void Enable_UndoButton()
        {
            this.UndoButton.Enabled = true;
        }
        public void Disable_UndoButton()
        {
            this.UndoButton.Enabled = false;
        }
        public void Enable_RedoButton()
        {
            this.RedoButton.Enabled = true;
        }
        public void Disable_RedoButton()
        {
            this.RedoButton.Enabled = false;
        }
    }
}
