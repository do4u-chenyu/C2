using C2.Business.Model;
using C2.Core;
using C2.Forms;
using C2.Utils;
using System;
using System.IO;
using System.Windows.Forms;

namespace C2.Controls.Left
{
    public partial class ModelButton : UserControl
    {
        private DataObject dragDropData;

        public ModelButton(string modelTitle)
        {
            InitializeComponent();
            this.textButton.Text = modelTitle;
            this.toolTip1.SetToolTip(this.textButton, ModelTitle);
            FullFilePath = Path.Combine(Global.MarketViewPath, ModelTitle, ModelTitle + ".xml"); 
        }


        public string ModelTitle => this.textButton.Text;

        public void EnableOpenDocumentMenu() { this.OpenToolStripMenuItem.Enabled = true; }

        public string FullFilePath { get ; set ; }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
             OpenModelDocument();
        }

        private void OpenModelDocument()
        {
            // 文件打开后,不能重复打开,不能删除,不能重命名
            //现在需要手动new一个canvasform
            using (new GuarderUtil.CursorGuarder())
                Global.GetMainForm().LoadCanvasFormByXml(Global.MarketViewPath, ModelTitle);
            new Log.Log().LogManualButton("聚沙成塔" + "-" + ModelTitle, "打开");
            this.OpenToolStripMenuItem.Enabled = false;
        }

        private void SelectCanvasForm()
        {
            CanvasForm form = SearchCanvasForm();
            Global.GetMainForm().SelectForm(form);
        }

        private void ExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileUtil.ExploreDirectory(FullFilePath);
        }

        private void CopyFilePathToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileUtil.TryClipboardSetText(FullFilePath);
        }


        private void TextButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            if (e.Clicks == 1) // 单击拖拽
            {
                dragDropData = new DataObject();
                dragDropData.SetData("Path", FullFilePath);      // 模型全路径
                dragDropData.SetData("Type", ElementType.Empty); // 模型为了统一逻辑，暂定为empty
                dragDropData.SetData("Text", ModelTitle); 
            }
            else if (e.Clicks == 2) // 双击打开
            {   
                if (IsCanvasFormOpened())
                    SelectCanvasForm();  // 切换Tab
                else
                    OpenModelDocument(); // 打开文档
            }
        }

        private void ExportModelButton_Click(object sender, EventArgs e)
        {
            Business.Model.ExportModel.GetInstance().Export(FullFilePath, Path.GetFileNameWithoutExtension(FullFilePath), Global.MarketViewPath);
        }

        private void ContextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.OpenToolStripMenuItem.Enabled = !IsCanvasFormOpened();
        }

        private bool IsCanvasFormOpened()
        {
            return SearchCanvasForm() != null;
        }

        private CanvasForm SearchCanvasForm()
        {
            return Global.GetMainForm().SearchCanvasForm(Path.Combine(Global.MarketViewPath, ModelTitle));
        }

        private void ModelButton_Load(object sender, EventArgs e)
        {
            // 模型全路径浮动提示信息
            this.toolTip1.SetToolTip(this.rightPictureBox, FullFilePath);
        }

        private void TextButton_MouseMove(object sender, MouseEventArgs e)
        {   //  因为和鼠标双击冲突,所以在移动时判断是否进入拖拽状态,
            if (dragDropData != null)
                this.textButton.DoDragDrop(dragDropData, DragDropEffects.Copy | DragDropEffects.Move);
        }

        private void TextButton_MouseLeave(object sender, EventArgs e)
        {
            dragDropData = null;
        }

        private void TextButton_MouseUp(object sender, MouseEventArgs e)
        {
            dragDropData = null;
        }
    }


}
