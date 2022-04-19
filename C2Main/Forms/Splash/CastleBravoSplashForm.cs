﻿using C2.Business.CastleBravo.Binary;
using C2.Controls.C1.Left;
using C2.Core;
using C2.Dialogs.CastleBravo;
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
        private readonly PluginButton IntruderButton = Global.GetCastleBravoControl().IntruderButton;

        public new void ShowDialog()
        {
            AddItem(HelpUtil.MD5CrackerInfo,     HelpUtil.MD5CrackerDesc, new AddCBTask(0));
            AddItem(HelpUtil.MD5SaltCrackerInfo, HelpUtil.MD5SaltCrackerDesc, new AddCBTask(1));
            AddItem(HelpUtil.MLFormInfo, HelpUtil.MLFormDesc, new MLForm(AddCBTask.MLD));
            AddItem(WebShellButton.Type, WebShellButton.Desc, WebShellButton);
            AddItem(VPNButton.Type, VPNButton.Desc, VPNButton);
            AddItem(HelpUtil.BinaryStringsInfo, HelpUtil.BinaryStringsDesc, new BinaryMainForm());
            AddItem(HelpUtil.XiseDecryptInfo, HelpUtil.XiseDecryptDesc, new BinaryMainForm());
            AddItem(HelpUtil.BehinderDecryptInfo, HelpUtil.BehinderDecryptDesc, new BinaryMainForm());
            AddItem(HelpUtil.BaiduLBSDecryptInfo, HelpUtil.BaiduLBSDecryptDesc, new BinaryMainForm());
            AddItem(HelpUtil.GaodeLBSDecryptInfo, HelpUtil.GaodeLBSDecryptDesc, new BinaryMainForm());
            AddItem(IntruderButton.Type, IntruderButton.Desc, IntruderButton);
            base.ShowDialog();
        }
    }
}
