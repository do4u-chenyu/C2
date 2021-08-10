using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C2.Controls;
using C2.IAOLab.WebEngine;
using C2.Utils;

namespace C2.Business.IAOLab.Visualization.Dialogs
{
    partial class ShowChartDialog : StandardDialog
    {
        SettingChartDialog settingChartDialog;
        public Dictionary<string, string> ChartOptions;
        public string WebUrl;

        public ShowChartDialog()
        {
            InitializeComponent();
            WebUrl = string.Empty;
            ChartOptions = new Dictionary<string, string>();

        }

        private void SettingButton_Click(object sender, EventArgs e)
        {
            OpenShowChartDialog();
        }

        private void PicSaveButton_Click(object sender, EventArgs e)
        {

        }

        public void OpenShowChartDialog()
        {
            if (settingChartDialog == null)
                settingChartDialog = new SettingChartDialog(ChartOptions, webBrowser1);
            if (settingChartDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    WebBrowserConfig.GetBrowserVersion();
                }
                catch (Exception ex)
                {
                    HelpUtil.ShowMessageBox(ex.Message);
                }
                webBrowser1.Navigate(settingChartDialog.WebUrl);
                ChartOptions = settingChartDialog.Options;
            }
        }
    }
}
