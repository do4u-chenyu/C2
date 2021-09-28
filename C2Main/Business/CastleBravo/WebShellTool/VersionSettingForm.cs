using C2.Controls;
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

namespace C2.Business.CastleBravo.WebShellTool
{
    partial class VersionSettingForm : StandardDialog
    {
        public WebShellVersionSetting SelectedVersion;

        public VersionSettingForm()
        {
            InitializeComponent();
            this.OKButton.Text = "更换";
            SelectedVersion = new WebShellVersionSetting();
            this.versionComboBox.SelectedIndex = 0;
        }

        protected override bool OnOKButtonClick()
        {
            SelectedVersion.LoadSetting(this.versionComboBox.Text);

            return base.OnOKButtonClick();
        }
    }
}
