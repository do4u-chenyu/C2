using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C2.Model;

namespace C2.Core
{
    class ChartPropertyEventArgs : EventArgs
    {
        public ChartPropertyEventArgs(string propertyName, ChangeTypes changeTypes)
        {
            PropertyName = propertyName;
            Changes = changeTypes;
        }

        public string PropertyName { get; private set; }

        public ChangeTypes Changes { get; private set; }
    }

    delegate void ChartPropertyEventHandler(object sender, ChartPropertyEventArgs e);
}
