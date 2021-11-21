using C2.Controls;
using C2.Core;
using C2.Utils;

namespace C2.Business.CastleBravo.WebShellTool
{
    partial class MSFSet : StandardDialog
    {
        private string MSF { get => msfTextBox.Text.Trim(); set => msfTextBox.Text = value; }
        public MSFSet()
        {
            InitializeComponent();
            MSF = Global.MSFHost;
        }
        protected override bool OnOKButtonClick()
        {
            if (MSF.IsNullOrEmpty())
            {
                HelpUtil.ShowMessageBox("【MSF地址】不能为空。");
                return false;
            }
            Global.MSFHost = MSF;
            return base.OnOKButtonClick();
        }
    }
}
