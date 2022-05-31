using C2.Controls;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.WebShellTool.SettingsDialog
{
    partial class FindSet : StandardDialog
    {
        private string SText { get; set; }
        private string TText { get => this.textBox1.Text.Trim(); }
        private readonly ListView lv;
        private int lastIndex;
        public FindSet(ListView lv)
        {

            InitializeComponent();
            this.lv = lv;
            this.lastIndex = 0;
        }

        public void FindHit()
        {
            if (ShowDialog() != DialogResult.OK)
                return;

            if (string.IsNullOrEmpty(TText))
                return;

            if (SText != TText)
            {
                this.lastIndex = 0;
                SText = TText;
            }
                

            ListViewItem lvi = FindItemWithText(SText, this.lastIndex);
            
            //  wrap模式, 往后没找到,再从头来一遍
            if (lastIndex > 0 && lvi == null)
                lvi = FindItemWithText(SText, 0);

            this.lastIndex = lvi == null ? 0 : lvi.Index + 1;

            if (lvi == null) 
                return;
            
            lv.SelectedItems.Clear();
            lv.FocusedItem = lvi;
            lvi.Selected = true;
            lvi.EnsureVisible();
        }

        private ListViewItem FindItemWithText(string text, int startIndex)
        {
            foreach(ListViewItem item in lv.Items)
            {
                if (item.Index < startIndex)
                    continue;

                foreach(ListViewItem.ListViewSubItem sub in item.SubItems)
                    if (sub.Text.Contains(text))
                        return item;
            }

            return null;
        }
    }
}
