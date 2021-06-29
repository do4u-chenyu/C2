using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace C2.Controls
{
    public delegate void TabItemEventHandler(object sender, TabItemEventArgs e);

    public delegate void TabItemCancelEventHandler(object sender, TabItemCancelEventArgs e);

    public class TabItemEventArgs : EventArgs
    {
        public TabItem Item { get; private set; }

        public TabBar Bar { get; private set; }

        public TabItemEventArgs(TabBar bar, TabItem item)
        {
            Bar = bar;
            Item = item;
        }
    }

    public class TabItemCancelEventArgs : CancelEventArgs
    {
        public TabItem Item { get; private set; }

        public TabBar Bar { get; private set; }

        public TabItemCancelEventArgs(TabBar bar, TabItem item)
        {
            Bar = bar;
            Item = item;
        }
    }
}
