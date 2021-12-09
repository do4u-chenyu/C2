using C2.Controls;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.WebShellTool.SettingsDialog
{
    partial class FindSet : StandardDialog
    {
        private string SText { get; set; }
        private readonly ListView lv;
        private int lastIndex;
        public FindSet(ListView lv)
        {

            InitializeComponent();
            this.lv = lv;
            this.lastIndex = 0;
        }

        public void SearchHit()
        {
            base.ShowDialog();
            if (string.IsNullOrEmpty(SText))
                return;

            if (!SText.Equals(this.textBox1.Text.Trim()))
                this.lastIndex = 0;

            ListViewItem lvi = lv.FindItemWithText(SText, true, this.lastIndex, true);
            this.lastIndex = lvi == null ? 0 : lvi.Index;

            if (lvi == null) 
                return;
 
            lvi.Selected = true;
            lvi.EnsureVisible();
        }
    }
}
