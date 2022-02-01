using C2.Controls;
using C2.Utils;
using System.Windows.Forms;

namespace C2.Dialogs
{
    partial class ZipDialog : StandardDialog
    {
        protected FileDialog fd;
        public string Password { set; get; }
        public string ModelPath 
        {
            set
            {
                this.modelPathTextBox.Text = value;
            }
            get
            {
                return this.modelPathTextBox.Text;
            }
        }
       
        public ZipDialog() 
        {
            InitializeComponent();
            InitializeButton();
        }
        private void InitializeButton()
        {
            this.OKButton.Size = new System.Drawing.Size(75, 27);
            this.CancelBtn.Size = new System.Drawing.Size(75, 27);
        }

        private void BrowseButton_Click(object sender, System.EventArgs e)
        {
            if (fd.ShowDialog() == DialogResult.OK)
            {
                ModelPath = fd.FileName;
            }
        }

        protected override bool OnOKButtonClick()
        {
            if (string.IsNullOrEmpty(ModelPath))
            {
                HelpUtil.ShowMessageBox("未选择路径");
                return false;
            }

            Password = this.passwordTextBox.Text;

            return base.OnOKButtonClick();
        }

        private void PasswordCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.passwordCheckBox.Checked)
            {
                this.passwordTextBox.Enabled = true;
                this.showPasswordCheckBox.Visible = true;
            }
            else
            {
                this.passwordTextBox.Enabled = false;
                this.passwordTextBox.Text = string.Empty;

                this.showPasswordCheckBox.Checked = false;
                this.showPasswordCheckBox.Visible = false;
            }
        }

        private void ShowPasswordCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.showPasswordCheckBox.Checked)
                this.passwordTextBox.UseSystemPasswordChar = false;
            else
                this.passwordTextBox.UseSystemPasswordChar = true;
        }
    }

    class ExportZipDialog : ZipDialog
    {
        public ExportZipDialog() : this("分析笔记")
        { }

        public ExportZipDialog(string fileName) : base()
        {
            fd = new SaveFileDialog
            {
                FileName = string.Format("{0}.c2", fileName),             // 保存时给一个默认的名字
                Filter = "分析笔记文件(*.c2)|*.c2",
                Title = "导出分析笔记",
                AddExtension = true
            };
            this.Text = "导出分析笔记";
        }
    }

    class ImportZipDialog : ZipDialog
    {
        public ImportZipDialog()
        {
            fd = new OpenFileDialog
            {
                Filter = "文件类型|*.c2;*.docx;*.doc;*.xmind|zip压缩包(*.zip)|*.zip",
                Title = "导入分析笔记",
                AddExtension = true
            };
            this.Text = "导入分析笔记";
        }
    }

}
