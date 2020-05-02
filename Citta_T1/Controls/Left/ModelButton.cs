using System;
using System.Windows.Forms;
using Citta_T1.Utils;
using System.IO;

namespace Citta_T1.Controls.Left
{
    public partial class ModelButton : UserControl
    {
        private string fullFilePath;

        public ModelButton(string modelName)
        {
            InitializeComponent();
            this.textButton.Text = modelName;
            fullFilePath = Path.Combine(Global.GetCurrentDocument().UserPath, this.textButton.Text, this.textButton.Text + ".xml");
        }


        public string ModelName => this.textButton.Text;

        public void EnableOpenDocumentMenu() { this.OpenToolStripMenuItem.Enabled = true; }
        public void EnableRenameDocumentMenu() { this.RenameToolStripMenuItem.Enabled = true; }
        public void EnableDeleteDocumentMenu() { this.DeleteToolStripMenuItem.Enabled = true; }
        public string FullFilePath { get => fullFilePath; set => fullFilePath = value; }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 文件打开后,不能重复打开,不能删除,不能重命名
            Global.GetMainForm().LoadDocument(this.textButton.Text);
            this.OpenToolStripMenuItem.Enabled = false;
            this.RenameToolStripMenuItem.Enabled = false;
            this.DeleteToolStripMenuItem.Enabled = false;
        }

        private void ModelButton_Load(object sender, EventArgs e)
        {
            // 模型全路径浮动提示信息
            String helpInfo = FullFilePath;
            this.toolTip1.SetToolTip(this.rightPictureBox, helpInfo);

            // 模型名称浮动提示信息
            helpInfo = ModelName;
            this.toolTip1.SetToolTip(this.textButton, helpInfo);
        }

        private void ExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileUtil.ExploreDirectory(FullFilePath);
        }

        private void CopyFilePathToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileUtil.TryClipboardSetText(FullFilePath);
        }
    }


}
