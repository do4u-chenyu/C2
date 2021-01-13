using C2.Business.Model;
using C2.Core;
using C2.Utils;
using System;
using System.IO;
using System.Windows.Forms;

namespace C2.Controls.Left
{
    public partial class MindMapModelButton : UserControl
    {
        private string oldTextString;

        public MindMapModelButton(string modelTitle)
        {
            InitializeComponent();
            this.textButton.Text = modelTitle;
            this.oldTextString = modelTitle;
            FullFilePath = Path.Combine(Global.BusinessViewPath, this.textButton.Text, this.textButton.Text + ".bmd");
        }

        public string ModelTitle => this.textButton.Text;

        public string FullFilePath { get; set; }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.GetMainForm().OpenDocument(FullFilePath);
        }

        private void MindMapModelButton_Load(object sender, EventArgs e)
        {
            // 模型全路径浮动提示信息
            String helpInfo = FullFilePath;
            this.toolTip1.SetToolTip(this.rightPictureBox, helpInfo);

            // 模型名称浮动提示信息
            helpInfo = ModelTitle;
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

        private void RenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  容错处理, 标题栏中文档未关闭时,不能重命名
            if (Global.GetTaskBar().ContainModel(this.ModelTitle))
                return;

            this.textBox.Text = ModelTitle;
            this.textBox.ReadOnly = false;
            this.oldTextString = ModelTitle;
            this.textButton.Visible = false;
            this.textBox.Visible = true;
            this.textBox.Focus();//获取焦点
            this.textBox.Select(this.textBox.TextLength, 0);
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 容错处理, 标题栏中文档未关闭时,不能删除
            if (Global.GetTaskBar().ContainModel(this.ModelTitle))
                return;
            // 删除前用对话框确认
            DialogResult rs = MessageBox.Show(String.Format("删除业务视图: {0}, 继续删除请点击 \"确定\"", ModelTitle),
                    "删除 " + this.ModelTitle,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information);

            if (rs != DialogResult.OK)
                return;

            string modelDic = Path.Combine(Global.UserWorkspacePath, "业务视图", ModelTitle);
            FileUtil.DeleteDirectory(modelDic);
            Global.GetMindMapModelControl().RemoveModelButton(this);
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 按下回车键
            if (e.KeyChar == 13)
            {
                FinishTextChange();
            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            FinishTextChange();
        }

        private void TextButton_MouseDown(object sender, MouseEventArgs e)
        {
            // 鼠标左键双击触发
            if (e.Button != MouseButtons.Left || e.Clicks != 2)
                return;
            // 双击打开对应模型
            using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
            {
                Global.GetMainForm().OpenDocument(FullFilePath);
            }
           
        }


        private void FinishTextChange()
        {
            if (this.textBox.Text.Trim().Length == 0)
                this.textBox.Text = this.oldTextString;
            this.textBox.ReadOnly = true;
            this.textBox.Visible = false;
            this.textButton.Visible = true;
            if (this.oldTextString == this.textBox.Text)
                return;
            this.textButton.Text = this.textBox.Text.Trim();

            // 新旧名称相同, 不需要做目录操作
            if (ModelTitle == oldTextString)
                return;

            string newModelDirectory = Path.Combine(Global.GetCurrentModelDocument().UserPath, ModelTitle);
            string oldModelDirectory = Path.Combine(Global.GetCurrentModelDocument().UserPath, oldTextString);
            string tmpFFP = Path.Combine(newModelDirectory, oldTextString + ".xml");
            string newFFP = Path.Combine(newModelDirectory, ModelTitle + ".xml");

            // 开始移动文件
            bool ret = FileUtil.DirecotryMove(oldModelDirectory, newModelDirectory);
            if (!ret) // 失败回滚
            {
                this.textButton.Text = oldTextString;
                return;
            }
            // 目前的机制，到这两步，一旦失败就无法回滚了
            ret = ModelDocument.ModifyRsPath(tmpFFP, oldModelDirectory, newModelDirectory);
            ret = FileUtil.FileMove(tmpFFP, newFFP);

            // 重命名
            this.oldTextString = ModelTitle;
            FullFilePath = newFFP;
            this.toolTip1.SetToolTip(this.textButton, ModelTitle);
            this.toolTip1.SetToolTip(this.rightPictureBox, FullFilePath);
        }

        private void ExportModelButton_Click(object sender, EventArgs e)
        {
            // 模型文档不存在返回
            if (!File.Exists(this.FullFilePath))
            {
                HelpUtil.ShowMessageBox("模型文档不存在，可能已被删除");
                return;
            }

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.Filter = "业务视图文件(*.c2)|*.c2";
            saveFileDialog1.Title = "导出业务视图";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string exportFullPath = saveFileDialog1.FileName;
                HelpUtil.ShowMessageBox("模型导出成功,存储路径：" + exportFullPath);
                C2.Business.Model.ExportModel.GetInstance().ExportC2Model(this.FullFilePath, exportFullPath);
            }
        }

        private void ContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Global.GetMainForm().OpendDocuments().Contains(ModelTitle))
            {
                this.OpenToolStripMenuItem.Enabled = false;
                this.RenameToolStripMenuItem.Enabled = false;
                this.DeleteToolStripMenuItem.Enabled = false;
            }
            else
            {
                this.OpenToolStripMenuItem.Enabled = true;
                this.DeleteToolStripMenuItem.Enabled = true;
            }

        }
    }
}
