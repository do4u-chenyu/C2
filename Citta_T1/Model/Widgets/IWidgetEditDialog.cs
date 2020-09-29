using System;
using System.Windows.Forms;

namespace Citta_T1.Model.Widgets
{
    public interface IWidgetEditDialog
    {
        DialogResult ShowDialog(IWin32Window owner);

        Widget Widget { get; set; }
    }
}
