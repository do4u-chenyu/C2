using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Citta_T1.Business.Model;
using Citta_T1.Utils;

namespace Citta_T1.Controls.Top
{
    public partial class CommonUseControl : UserControl
    {
        public CommonUseControl()
        {
            InitializeComponent();
            InitializeToolTip();
        }

        private void InitializeToolTip()
        {
            this.toolTip1.SetToolTip(this.relateButton, HelpUtil.RelateOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.collideButton, HelpUtil.CollideOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.unionButton, HelpUtil.UnionOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.differButton, HelpUtil.DifferOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.filterButton, HelpUtil.FilterOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.randomButton, HelpUtil.RandomOperatorHelpInfo);
            this.toolTip1.SetToolTip(this.formatButton, HelpUtil.FormatOperatorHelpInfo);
        }

        private void CommonUse_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataObject dragDropData = new DataObject();
                dragDropData.SetData("Type", ElementType.Operator);
                dragDropData.SetData("Path", "");
                dragDropData.SetData("Text", NameTranslate((sender as Button).Name));
                this.relateButton.DoDragDrop(dragDropData, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }
        private string NameTranslate(string name)
        {
            String text = "";
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
                    text = "过滤算子";
                    break;
                case "randomButton":
                    text = "随机采样";
                    break;
            }
            return text;
        }

        private void formatButton_MouseClick(object sender, MouseEventArgs e)
        {
            Global.GetMainForm().SetDocumentDirty();
            ModelDocument currentModel = Global.GetCurrentDocument();
            QuickformatWrapper quickformatWrapper = new QuickformatWrapper(currentModel);
            quickformatWrapper.TreeGroup();
            Global.GetMainForm().SetDocumentDirty();
        }
    }
}
