using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C2.Controls;
using C2.Core;
using C2.IAOLab.WebEngine;
using C2.Utils;

namespace C2.Business.IAOLab.Visualization.Dialogs
{
    partial class ShowChartDialog : StandardDialog
    {
        SettingChartDialog settingChartDialog;
        public Dictionary<string, string[]> ChartOptions;
        public string WebUrl;
        private readonly string picPath;

        public ShowChartDialog()
        {
            InitializeComponent();
            WebUrl = string.Empty;
            ChartOptions = new Dictionary<string, string[]>();
            picPath = Path.Combine(Global.TempDirectory, "visual.png");
        }

        private void SettingButton_Click(object sender, EventArgs e)
        {
            OpenShowChartDialog();
        }

        private void PicSaveButton_Click(object sender, EventArgs e)
        {
            SavePic();
        }

        private void SavePic()
        {
            Bitmap bitmap = new Bitmap(webBrowser1.DrawToImage(), new Size(webBrowser1.Width, webBrowser1.Height));
            bitmap.Save(picPath, ImageFormat.Png);

            SaveFileDialog fd = new SaveFileDialog
            {
                Filter = "图片文件(*.png)|*.png",
                AddExtension = true,
                FileName = ChartOptions.Count == 0 || !ChartOptions.ContainsKey("ChartType") ? String.Empty : String.Format("{0}_{1}", ChartOptions["ChartType"][0], DateTime.Now.ToString("yyyyMMdd"))
            };

            if (fd.ShowDialog() != DialogResult.OK)
                return;
            File.Copy(picPath, fd.FileName, true);
            HelpUtil.ShowMessageBox("图片保存成功。");
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

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            SavePic();
        }

        private void ShowChartDialog_Shown(object sender, EventArgs e)
        {
            WebBrowserConfig.SetWebBrowserFeatures(11);//TODO 暂定11，后面需要检测
            webBrowser1.Navigate(WebUrl);
            OpenShowChartDialog();
        }
    }
}
