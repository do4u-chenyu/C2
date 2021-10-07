using C2.Controls;
using C2.Utils;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.WebShellTool
{
    partial class TrojanGeneratorForm : StandardDialog
    {

        public TrojanGeneratorForm(string trojanType, bool encry = false)
        {
            InitializeComponent();
            InitializeComponent2(trojanType, encry); 
        }

        private void InitializeComponent2(string trojanType, bool encry)
        {
            this.trojanComboBox.Text = trojanType;
            this.encryComboBox.Text = "无需配置";
            this.OKButton.Text = "生成";
            if (encry)
            {
                this.keyTextBox.Enabled = true;
                this.keyTextBox.Text = "key";
                this.encryComboBox.SelectedIndex = 0;
                this.encryComboBox.Enabled = true;
            }
        }

        protected override bool OnOKButtonClick()
        {
            this.saveFileDialog1.FileName = this.trojanComboBox.Text;
            this.saveFileDialog1.DefaultExt = ".php";
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                HelpUtil.ShowMessageBox("成功, 保存: " + this.saveFileDialog1.FileName);
                return base.OnOKButtonClick();
            }
            return false;
        }
    }
}
