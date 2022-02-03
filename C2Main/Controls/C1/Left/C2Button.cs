using C2.Core;
using C2.Dialogs;
using C2.Utils;
using System;
using System.IO;
using System.Windows.Forms;

namespace C2.Controls.Left
{
    public partial class C2Button : UserControl
    {
        protected string desc;

        public C2Button(string c2Title)
        {
            InitializeComponent();
            InitializeOther(c2Title);
        }

        private void InitializeOther(string modelTitle)
        {
            this.textButton.Text = modelTitle;
            this.toolTip1.SetToolTip(this.textButton, ModelTitle);
            FullFilePath = Path.Combine(Global.BusinessViewPath, ModelTitle, ModelTitle + ".bmd");
            this.desc = "分析笔记";
        }

        public string ModelTitle => this.textButton.Text;

        public string FullFilePath { get; set; }

        protected virtual void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.GetMainForm().OpenMindMapDocument(FullFilePath);
        }

        private void ExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileUtil.ExploreDirectory(FullFilePath);
        }

        private void CopyFilePathToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileUtil.TryClipboardSetText(FullFilePath);
        }

 
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 容错处理, 标题栏中文档未关闭时,不能删除
            if (Global.GetMainForm().OpenedDocumentsFFP().Contains(FullFilePath))
                return;
            // 删除前用对话框确认
            DialogResult rs = MessageBox.Show(String.Format("删除{1}: {0}, 继续删除请点击 \"确定\"", ModelTitle, desc),
                    "删除 " + ModelTitle,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information);

            if (rs != DialogResult.OK)
                return;

            FileUtil.DeleteDirectory(Path.GetDirectoryName(FullFilePath));
            Global.GetMindMapControl().RemoveButton(this);
        }


        protected virtual void TextButton_MouseDown(object sender, MouseEventArgs e)
        {
            // 鼠标左键双击触发
            if (e.Button != MouseButtons.Left || e.Clicks != 2)
                return;
            // 双击打开对应模型
            Global.GetMainForm().OpenMindMapDocument(FullFilePath);
        }


        private void ExportModelButton_Click(object sender, EventArgs e)
        {
            // 模型文档不存在返回
            if (!File.Exists(this.FullFilePath))
            {
                HelpUtil.ShowMessageBox("文档不存在，可能已被删除");
                return;
            }

            ZipDialog zipDialog = new ExportZipDialog(ModelTitle, desc);
            if (zipDialog.ShowDialog() == DialogResult.OK)
            {
                string exportFullPath = zipDialog.ModelPath;
                string password = zipDialog.Password;
                if (Business.Model.ExportModel.GetInstance().ExportC2Model(this.FullFilePath, exportFullPath, password))
                    HelpUtil.ShowMessageBox("导出成功,存储路径：" + exportFullPath);
                FileUtil.DeleteDirectory(Path.Combine(Global.TempDirectory));
            }
        }

        private void ContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 文档打开时不能删除
            if (Global.GetMainForm().OpenedDocumentsFFP().Contains(FullFilePath))
            {
                this.OpenToolStripMenuItem.Enabled = false;
                this.DeleteToolStripMenuItem.Enabled = false;
                this.DeleteToolStripMenuItem.Text = "删除(先关闭)";
            }
            else
            {
                this.OpenToolStripMenuItem.Enabled = true;
                this.DeleteToolStripMenuItem.Enabled = true;
                this.DeleteToolStripMenuItem.Text = "删除";
            }

        }

        private void MindMapModelButton_Load(object sender, EventArgs e)
        {
            // 模型全路径浮动提示信息
            String helpInfo = FullFilePath;
            this.toolTip1.SetToolTip(this.rightPictureBox, helpInfo);
        }
    }
}
