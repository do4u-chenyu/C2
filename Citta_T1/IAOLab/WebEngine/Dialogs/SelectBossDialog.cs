using C2.Controls;
using C2.IAOLab.WebEngine.Boss;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.IAOLab.WebEngine.Dialogs
{
    partial class SelectBossDialog : StandardDialog
    {
        public string WebUrl;
        public SelectBossDialog()
        {
            WebUrl = Path.Combine(Application.StartupPath, "IAOLab\\WebEngine\\Html", "BossIndex01.html");
            InitializeComponent();
        }


        protected override bool OnOKButtonClick()
        {
            GenBossHtml.GetInstance().TransDataToHtml();
            return base.OnOKButtonClick();
        }
    }
}
