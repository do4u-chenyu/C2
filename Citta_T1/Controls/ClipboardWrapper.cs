using System.Collections.Generic;
using System.Windows.Forms;

namespace Citta_T1.Controls
{
    class ClipboardWrapper
    {
        private List<Control> clipboardControls;
        public List<Control> ClipboardControls { get => clipboardControls; set => clipboardControls = value; }
        public void ClipboardPaste()
        {
            foreach(Control ct in clipboardControls)
            {
                //创建新的控件
            }
        }
    }
}
