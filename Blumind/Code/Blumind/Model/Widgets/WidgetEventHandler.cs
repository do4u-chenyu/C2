using System;
using System.Collections.Generic;
using System.Text;

namespace Blumind.Model.Widgets
{
    public delegate void WidgetEventHandler(object sender, WidgetEventArgs e);

    public class WidgetEventArgs : EventArgs
    {
        private Widget _Widget;

        public WidgetEventArgs(Widget widget)
        {
            Widget = widget;
        }

        public Widget Widget
        {
            get { return _Widget; }
            private set { _Widget = value; }
        }
    }
}
