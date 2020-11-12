using System;
using System.Collections.Generic;
using System.Text;

namespace C2.ChartPageView
{
    public class NeedShowPropertyEventArgs : EventArgs
    {
        public bool Force { get; private set; }

        public NeedShowPropertyEventArgs(bool force)
        {
            Force = force;
        }
    }

    public delegate void NeedShowPropertyEventHandler(object sender, NeedShowPropertyEventArgs e);
}
