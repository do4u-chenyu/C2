using System;
using System.Windows.Forms;
using Citta_T1.Utils;
using Citta_T1.Controls.Move;
using Citta_T1.IAOLab.PythonOP;


namespace Citta_T1.OperatorViews
{
    public partial class PythonOperatorView : Form
    {
        public PythonOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
        }

        private void ConfirmButton_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void CancelButton_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void PyBrowseButton_Click(object sender, System.EventArgs e)
        {
            DialogResult rs = this.openFileDialog1.ShowDialog();
            if (rs != DialogResult.OK)
                return;
            this.pyFullFilePathTextBox.Text = this.openFileDialog1.FileName;
            this.toolTip1.SetToolTip(this.pyFullFilePathTextBox, this.pyFullFilePathTextBox.Text);
        }

        private void RsChosenButton_Click(object sender, System.EventArgs e)
        {   // 由用户自己指定Py脚本生成的文件路径名,因此在配置的时候,py脚本还没运行
            // 此时结果文件还不存在,故使用saveFileDialog对话框
            DialogResult rs = this.saveFileDialog1.ShowDialog();
            if (rs != DialogResult.OK)
                return;
            this.browseChosenTextBox.Text = this.saveFileDialog1.FileName;
            this.toolTip1.SetToolTip(this.browseChosenTextBox, this.browseChosenTextBox.Text);
        }

        private void StdoutRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            // 此时不需要 rsChosenButton
            this.rsChosenButton.Enabled = false;
        }

        private void ParamRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            // 此时不需要 rsChosenButton
            this.rsChosenButton.Enabled = false;
        }

        private void BrowseChosenRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            // 此时需要 rsChosenButton
            this.rsChosenButton.Enabled = true;
        }

        private void PythonOperatorView_Load(object sender, System.EventArgs e)
        {
            PythonInterpreterInfoLoad();
        }

        private void PythonInterpreterInfoLoad()
        {
            string pythonConfigString = ConfigUtil.TryGetAppSettingsByKey("python");
            if (String.IsNullOrEmpty(pythonConfigString))
            {
                this.pythonChosenComboBox.Text = "未配置Python虚拟机";
                return;
            }
            PythonOPConfig config = new PythonOPConfig(pythonConfigString);
            //if (config.)
                
        }
    }
}
