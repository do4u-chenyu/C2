using C2.Controls.C1.Left;
using C2.Core;
using C2.Utils;
using System.Windows.Forms;

namespace C2.Forms.Splash
{
    public partial class CastleBravoSplashForm : BaseSplashForm
    {

        public CastleBravoSplashForm()
        {
            this.StartPosition = FormStartPosition.Manual;
            // 网上抄来的
            this.SetBounds((Screen.GetBounds(this).Width  / 2) - Width  / 2,
                           (Screen.GetBounds(this).Height / 2) - Height / 4,
                           Width, Height);
        }

        private readonly PluginButton WebShellButton = Global.GetCastleBravoControl().WebShellButton;
        private readonly PluginButton VPNButton      = Global.GetCastleBravoControl().VPNButton;
        private readonly PluginButton BinaryButton   = Global.GetCastleBravoControl().BinaryButton;
        private readonly PluginButton IntruderButton = Global.GetCastleBravoControl().IntruderButton;

        public new void ShowDialog()
        {
            AddItem(HelpUtil.MD5CrackerInfo,     HelpUtil.MD5CrackerDesc, (Form)null);
            AddItem(HelpUtil.MD5SaltCrackerInfo, HelpUtil.MD5SaltCrackerDesc, (Form)null);
            AddItem(WebShellButton.Type, WebShellButton.Desc, WebShellButton);
            AddItem(VPNButton.Type, VPNButton.Desc, VPNButton);
            AddItem(BinaryButton.Type, BinaryButton.Desc, BinaryButton);
            AddItem(IntruderButton.Type, IntruderButton.Desc, IntruderButton);
            base.ShowDialog();
        }
    }
}
