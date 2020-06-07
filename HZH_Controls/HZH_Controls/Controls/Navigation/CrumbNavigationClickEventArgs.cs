using System;

namespace HZH_Controls.Controls
{
    public class CrumbNavigationClickEventArgs : EventArgs
    {
        public int Index { get; set; }
        public CrumbNavigationItem Item { get; set; }
    }
}
