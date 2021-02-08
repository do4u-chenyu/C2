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
    partial class WebBrowserDialog : StandardDialog
    {
        public string Title { set => this.Text = value; }
        public string WebUrl;

        public WebBrowserDialog()
        {
            InitializeComponent();
        }

        private void WebBrowserDialog_Load(object sender, EventArgs e)
        {
            WebBrowserConfig.SetWebBrowserFeatures(11);//TODO 暂定11，后面需要检测
            webBrowser1.Navigate(WebUrl);
        }

        public void InitializeMapToolStrip()
        {
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadMapData,
            this.SaveHtml,
            this.SavePic,
            this.toolStripSeparator1,
            this.Clear,
            this.EditCode});
        }
        public void InitializeBossToolStrip()
        {
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadBossData,
            this.SaveHtml,
            this.SavePic
            });
        }

        void LoadMapData_Click(object sender, EventArgs e)
        {
            var dialog = new SelectMapDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
                webBrowser1.Navigate(dialog.WebUrl);
        }
        void LoadBossData_Click(object sender, EventArgs e)
        {
            //var dialog = new SelectMapDialog();
            //if (dialog.ShowDialog() == DialogResult.OK)
            //    webBrowser1.Navigate(dialog.WebUrl);
            //GenBossHtml.GetInstance().TransDataToHtml()
            webBrowser1.Navigate(Path.Combine(Application.StartupPath, "IAOLab\\WebEngine\\Html", "BossIndex01.html"));
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            string[] strArr = new string[4];
            strArr[0] = "[{ \"lng\": \"114.363979\", \"lat\": \"36.03773\", \"count\": \"52\" }, { \"lng\": \"115.363979\", \"lat\": \"37.03773\", \"count\": \"53\" }]";

            string JSON_OBJ_Format = "{ \"lng\": \"{0}\", \"lat\": \"{1}\", \"count\": \"{2}\" }";
           // String.Format(JSON_OBJ_Format, 114.376, 36.01, 53);


            webBrowser1.Document.InvokeScript("getPoints", strArr);
        }
    }
}
