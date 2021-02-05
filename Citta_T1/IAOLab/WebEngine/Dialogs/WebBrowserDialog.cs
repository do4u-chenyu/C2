using C2.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    }
}
