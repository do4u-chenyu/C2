using C2.Business.CastleBravo.VPN.Client;
using C2.Controls;
using C2.Core;
using System.Drawing;

namespace C2.Business.CastleBravo.VPN
{
    partial class CAForm : StandardDialog
    {
        public CAForm()
        {
            InitializeComponent();
            InitializeGB();
            InitializeOther();
        }
        private void InitializeOther()
        {
            this.OKButton.Size  = new Size(75, 27);
            this.CancelBtn.Size = new Size(75, 27);
        }

        private void InitializeGB()
        {
            I204TB.Text = ClientSetting.I204List.JoinString(System.Environment.NewLine);
            O204TB.Text = ClientSetting.O204List.JoinString(System.Environment.NewLine);
        }
    }
}
