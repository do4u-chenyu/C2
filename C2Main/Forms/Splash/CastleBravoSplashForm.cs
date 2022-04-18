using C2.Controls.C1.Left;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Forms.Splash
{
    public partial class CastleBravoSplashForm : BaseSplashForm
    {
        public new void ShowDialog()
        {
            AddItem(HelpUtil.MD5CrackerInfo,     HelpUtil.MD5CrackerDesc, (Form)null);
            AddItem(HelpUtil.MD5SaltCrackerInfo, HelpUtil.MD5SaltCrackerDesc, (Form)null);
            //List<PluginButton> buttons = Global.GetCastleBravoControl().CBPluginButtons;
            //foreach (PluginButton button in buttons)
            //    AddItem(button., button.Desc, button);
            base.ShowDialog();
        }
    }
}
