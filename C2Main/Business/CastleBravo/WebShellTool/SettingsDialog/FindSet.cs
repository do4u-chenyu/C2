using C2.Controls;

namespace C2.Business.CastleBravo.WebShellTool.SettingsDialog
{
    partial class FindSet : StandardDialog
    {
        public FindSet()
        {
            InitializeComponent();
        }

        public new string ShowDialog()
        {
            base.ShowDialog();
            return this.textBox1.Text.Trim();
        }
    }
}
