using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2.Controls
{
    class ButtonEventArgs : EventArgs
    {
        public ButtonEventArgs(ButtonInfo button)
        {
            Button = button;
        }

        public ButtonInfo Button { get; private set; }
    }

    delegate void ButtonEventHandler(object sender, ButtonEventArgs e);
}
