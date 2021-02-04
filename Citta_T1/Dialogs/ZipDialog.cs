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
            }
            else
            {
                this.passwordTextBox.Enabled = false;
                this.passwordTextBox.Text = string.Empty;
            }
        }
    }

    class ExportZipDialog : ZipDialog
    {
        public ExportZipDialog() : base()
        {
            fd = new SaveFileDialog
            {
                FileName = "业务视图.c2",             // 保存时给一个默认的名字
                Filter = "业务视图文件(*.c2)|*.c2",
                Title = "导出业务视图",
                AddExtension = true
        };    
            this.Text = "导出业务视图";
        }
    }

    class ImportZipDialog : ZipDialog
    {
        public ImportZipDialog()
        {
            fd = new OpenFileDialog
            {
                Filter = "业务视图文件(*.c2)|*.c2|zip压缩包(*.zip)|*.zip",
                Title = "导入业务视图",
                AddExtension = true
            };
            this.Text = "导入业务视图";
        }
    }

}
