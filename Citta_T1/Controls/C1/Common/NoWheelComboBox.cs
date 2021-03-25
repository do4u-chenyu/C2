using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Controls.C1.Common
{
    public partial class NoWheelComboBox : ComboBox, IMessageFilter
    {
        public NoWheelComboBox()
        {
            InitializeComponent();
            Application.AddMessageFilter(this);
        }

        bool IMessageFilter.PreFilterMessage(ref Message m)
        {
            if (m.Msg == 0x020A && m.HWnd == this.Handle)  // 不处理滚轮事件，只处理属于自己的message
                return true;
            return false;
        }
    }
}
