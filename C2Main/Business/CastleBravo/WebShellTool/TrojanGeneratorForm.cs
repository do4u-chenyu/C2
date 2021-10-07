using C2.Controls;

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
            this.OKButton.Click += OKButton_Click;
            if (encry)
            {
                this.keyTextBox.Enabled = true;
                this.keyTextBox.Text = string.Empty;
                this.encryComboBox.SelectedIndex = 0;
                this.encryComboBox.Enabled = true;
            }
        }

        private void OKButton_Click(object sender, System.EventArgs e)
        {
            
        }
    }
}
