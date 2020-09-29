using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Citta_T1.Core
{
    public interface IColorToolTip
    {
        string ToolTip { get; }

        bool ToolTipShowAlway { get; }

        string ToolTipHyperlinks { get; }
    }
}
