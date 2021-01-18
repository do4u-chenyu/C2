using C2.Controls;
using C2.Utils;
using System.Windows.Forms;

namespace C2.Dialogs
{
    partial class ZipDialog : StandardDialog
    {
        private string _ModelPath;
        private bool isExport;
        public string Password { set; get; }
        public string ModelPath 
        {
            set
            {
                _ModelPath = value;
                this.modelPathTextBox.Text = value;
            }
            get
            {
                return _ModelPath;
            }
        }
        
        public ZipDialog(bool isExport)
        {
            InitializeComponent();
            this.isExport = isExport;
            this.Text = isExport ? "导出业务视图" : "导入业务视图";
        }

        private void BrowseButton_Click(object sender, System.EventArgs e)
        {
            FileDialog fd;
            if (isExport)
            {
                fd = new SaveFileDialog();
                fd.FileName = "业务视图.c2"; // 保存时给一个默认的名字
            }
                

            else
                fd = new OpenFileDialog();

            fd.AddExtension = true;
            fd.Filter = isExport ? "业务视图文件(*.c2)|*.c2" : "业务视图文件(*.c2)|*.c2|zip压缩包(*.zip)|*.zip";
            fd.Title = isExport ? "导出业务视图" : "导入业务视图";
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
            }
            else
            {
                this.passwordTextBox.Enabled = false;
                this.passwordTextBox.Text = string.Empty;
            }
        }
    }
}
