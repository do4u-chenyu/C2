using C2.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.VPN
{
    partial class StaticForm : StandardDialog
    {
        public StaticForm()
        {
            InitializeComponent();
            InitializeOther();
        }

        private void InitializeOther()
        {
            OKButton.Size = new Size(75, 24);
            CancelBtn.Size = new Size(75, 24);
        }
    }
}
