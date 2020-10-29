using C2.Business.Model;
using C2.Business.Model.World;
using C2.Controls.Move;
using C2.Core;
using C2.Core.UndoRedo;
using C2.Core.UndoRedo.Command;
using C2.Dialogs;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace C2.Controls.Top
{
    public partial class TopToolBarControl : UserControl
    {
        private bool selectRemark;
        private bool selectFrame;
        private bool selectDrag;
        public bool SelectRemark { get => selectRemark; set => selectRemark = value; }
        public bool SelectDrag { get => selectDrag; set => selectDrag = value; }
        public bool SelectFrame { get => selectFrame; set => selectFrame = value; }
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TopToolBarControl));
        public TopToolBarControl()
        {
            InitializeComponent();
            InitializeToolTip();
            InitializeUndoRedoManger();
            SelectDrag = false;
            SelectFrame = false;
            SelectRemark = false;
        } // 恢复到编辑模式
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

        #endregion

        #region 放大缩小
       
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
            
        }

        public void RemarkChange(bool flag)
        {
          
        }
        private void FrameChange(bool flag)
        {
            Global.GetCurrentDocument().Show();
            Global.GetCanvasPanel().FrameWrapper.InitFrame();
            
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
         
            this.toolTip1.SetToolTip(this.formatButton, HelpUtil.FormatOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.undoButton, HelpUtil.UndoButtonHelpInfo);
            this.toolTip1.SetToolTip(this.redoButton, HelpUtil.RedoButtonHelpInfo);
        }

        private void CommonUse_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && sender is Button)
            {
                DataObject dragDropData = new DataObject();
                dragDropData.SetData("Type", ElementType.Operator);
                dragDropData.SetData("Path", "");
                dragDropData.SetData("Text", NameTranslate((sender as Button).Name));
              //  this.relateButton.DoDragDrop(dragDropData, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }
        private string NameTranslate(string name)
        {
            String text = String.Empty;
            switch (name)
            {
                case "relateButton":
                    text = "关联算子";
                    break;
                case "collideButton":
                    text = "碰撞算子";
                    break;
                case "unionButton":
                    text = "取并集";
                    break;
                case "differButton":
                    text = "取差集";
                    break;
                case "filterButton":
                    text = "关键词过滤";
                    break;
                case "randomButton":
                    text = "随机采样";
                    break;
            }
            return text;
        }

        private void FormatButton_MouseClick(object sender, MouseEventArgs e)
        {
            ModelDocument currentModel = Global.GetCurrentDocument();
            WorldMap curWorldMap = Global.GetCurrentDocument().WorldMap;
            // 文档为空时,返回,不需要触发dirty动作
            if (currentModel.ModelElements.Count == 0)
                return;
            Dictionary<int, Point> idPtsDict = new Dictionary<int, Point>();
            idPtsDict = ControlUtil.SaveElesWorldCord(currentModel.ModelElements);
            BaseCommand cmd = new BatchMoveCommand(idPtsDict);
            UndoRedoManager.GetInstance().PushCommand(Global.GetCurrentDocument(), cmd);

            QuickformatWrapper quickformatWrapper = new QuickformatWrapper(currentModel);
            quickformatWrapper.TreeGroup();
            Global.GetMainForm().SetDocumentDirty();
        }


        private void MoreButton_MouseClick(object sender, MouseEventArgs e)
        {
            ConfigForm config = new ConfigForm();
            config.ShowDialog();
        }

        public void UndoButton_Click(object sender, EventArgs e)
        {
            UndoRedoManager.GetInstance().Undo(Global.GetCurrentDocument());
            Global.GetMainForm().SetDocumentDirty();
        }

        public void RedoButton_Click(object sender, EventArgs e)
        {
            UndoRedoManager.GetInstance().Redo(Global.GetCurrentDocument());
            Global.GetMainForm().SetDocumentDirty();
        }

        private void TopToolBarControl_Load(object sender, EventArgs e)
        {
            // 测试用
            //UndoRedoManager.GetInstance().DoCommand(new TestCommand());
            //UndoRedoManager.GetInstance().DoCommand(new TestCommand());
            //UndoRedoManager.GetInstance().DoCommand(new TestCommand());
        }

        public void SetUndoButtonEnable(bool value)
        {
            this.undoButton.Enabled = value;
        }

        public void SetRedoButtonEnable(bool value)
        {
            this.redoButton.Enabled = value;
        }

        private void ImportModel_Click(object sender, EventArgs e)
        {
            C2.Business.Model.ImportModel.GetInstance().ImportIaoFile(Global.GetMainForm().UserName);
        }
    }
}
