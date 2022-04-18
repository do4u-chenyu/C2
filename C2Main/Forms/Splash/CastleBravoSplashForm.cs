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
            AddItem("盗洞模拟器", "盗洞模型的配套工具集", Global.GetCastleBravoControl().WebShellButton);
            AddItem("VPN专项", "VPN模型的配套工具集", Global.GetCastleBravoControl().VPNButton);
            AddItem("二进制逆向", "二进制分析时的各种配套攻击", Global.GetCastleBravoControl().BinaryButton);
            AddItem("大马破门锤", "大马模型的配套工具集", Global.GetCastleBravoControl().IntruderButton);
            base.ShowDialog();
        }
    }
}
