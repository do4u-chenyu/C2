using C2.Controls;

namespace C2.Business.CastleBravo.WebShellTool
{
    partial class TrojanGeneratorForm : StandardDialog
    {
        public TrojanGeneratorForm(string trojanType = "")
        {
            InitializeComponent();
            this.comboBox1.Text = trojanType;
        }
    }
}
