using Citta_T1.Business.Model;
using Citta_T1.Business.Model.World;
using Citta_T1.Controls.Move;
using Citta_T1.Core;
using Citta_T1.Core.UndoRedo;
using Citta_T1.Core.UndoRedo.Command;
using Citta_T1.Dialogs;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Citta_T1.Controls.Top
{
    public partial class TopToolBarControl : UserControl
    {
        public TopToolBarControl()
        {
            InitializeComponent();
            InitializeToolTip();
            InitializeUndoRedoManger();
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
            Citta_T1.Business.Model.ImportModel.GetInstance().ImportIaoFile(Global.GetMainForm().UserName);
        }
    }
}
