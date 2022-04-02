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
    partial class AddVPNServerForm : StandardDialog
    {
        public AddVPNServerForm()
        {
            InitializeComponent();
        }

        public VPNTaskConfig ShowDialog(string createTime)
        {
            base.ShowDialog();
            return VPNTaskConfig.Empty;
        }
    }
}
