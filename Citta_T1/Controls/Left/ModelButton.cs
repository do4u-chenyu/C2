using System;
using System.Windows.Forms;
using Citta_T1.Utils;
using System.IO;
using Citta_T1.Core;
using Citta_T1.Business.Model;

namespace Citta_T1.Controls.Left
{
    public partial class ModelButton : UserControl
    {
        private string oldTextString;
        private string fullFilePath;

        public ModelButton(string modelTitle)
        {
            InitializeComponent();
            this.textButton.Text = modelTitle;
            this.oldTextString = modelTitle;
            fullFilePath = Path.Combine(Global.GetCurrentDocument().UserPath, this.textButton.Text, this.textButton.Text + ".xml");
        }


        public string ModelTitle => this.textButton.Text;

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
            if (Global.GetModelTitlePanel().ContainModel(this.ModelTitle))
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
            if (Global.GetModelTitlePanel().ContainModel(this.ModelTitle))
                return;
            // 删除前用对话框确认
            DialogResult rs = MessageBox.Show(String.Format("删除模型 {0}, 继续删除请点击 \"确定\"", ModelTitle),
                    "删除 " + this.ModelTitle,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information);

            if (rs != DialogResult.OK)
                return;

            string modelDic = System.IO.Path.Combine(Global.GetCurrentDocument().UserPath, ModelTitle);
            FileUtil.DeleteDirectory(modelDic);
            Global.GetMyModelControl().RemoveModelButton(this);
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
            RenameToolStripMenuItem_Click(sender, e);
        }


        private void FinishTextChange()
        {
            if (this.textBox.Text.Trim().Length == 0)
                return;

            this.textBox.ReadOnly = true;
            this.textBox.Visible = false;
            this.textButton.Text = this.textBox.Text.Trim();
            this.textButton.Visible = true;

            // 新旧名称相同, 不需要做目录操作
            if (ModelTitle == oldTextString)
                return;


            string newModelDirectory = System.IO.Path.Combine(Global.GetCurrentDocument().UserPath, ModelTitle);
            string oldModelDirectory = System.IO.Path.Combine(Global.GetCurrentDocument().UserPath, oldTextString);
            string newFFP   = Path.Combine(newModelDirectory, ModelTitle + ".xml");
            string oldFFP   = Path.Combine(oldModelDirectory, ModelTitle + ".xml");
            ModelDocument.ModifyRSPath(oldFFP, oldModelDirectory, newModelDirectory);
            // 开始移动文件
            bool ret = FileUtil.CreateDirectory(newModelDirectory);
            if (!ret) // 失败回滚
            {
                this.textButton.Text = oldTextString;
                return;
            }

            ret = FileUtil.FileMove(FullFilePath, newFFP);
            if (!ret) // 失败回滚
            {
                FileUtil.DeleteDirectory(newModelDirectory);
                this.textButton.Text = oldTextString;
                return;
            }

            // 重命名
            this.oldTextString = ModelTitle;
            this.fullFilePath = newFFP;
            this.toolTip1.SetToolTip(this.textButton, ModelTitle);
            this.toolTip1.SetToolTip(this.rightPictureBox, FullFilePath);
            FileUtil.DeleteDirectory(oldModelDirectory);
        }


    }


}
