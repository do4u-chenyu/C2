using System;
using System.Collections;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.WebShellTool
{
    class LVComparer : IComparer
    {
        public int col = 0;
        public bool asce = false;
        public int Compare(object x, object y)
        {
            string l = ((ListViewItem)x).SubItems[col].Text;
            string r = ((ListViewItem)y).SubItems[col].Text;
            int returnVal = String.Compare(l, r);
            return asce ? returnVal : 0 - returnVal;
        }
    }
}
