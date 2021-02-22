﻿using C2.Controls;
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
        private ToolStripButton LoadMapData;
        private ToolStripButton LoadBossData;
        private ToolStripButton SaveHtml;
        private ToolStripButton SavePic;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton Clear;
        private ToolStripButton EditCode;

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
            LoadMapData = new ToolStripButton();
            SaveHtml = new ToolStripButton();
            SavePic = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            Clear = new ToolStripButton();
            EditCode = new ToolStripButton();

            // LoadMapData
            LoadMapData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            LoadMapData.Image = global::C2.Properties.Resources.importDataSource;
            LoadMapData.Text = "导入数据";
            LoadMapData.Click += new System.EventHandler(this.LoadMapData_Click);

            // SaveHtml
            SaveHtml.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            SaveHtml.Image = global::C2.Properties.Resources.save;
            SaveHtml.Text = "保存成html";

            // SavePic
            SavePic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            SavePic.Image = global::C2.Properties.Resources.image;
            SavePic.Text = "保存成图片";

            // Clear
            Clear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            Clear.Image = global::C2.Properties.Resources.delete;
            Clear.Text = "清空";
            Clear.Click += new System.EventHandler(this.Clear_Click);

            // EditCode
            EditCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            EditCode.Image = global::C2.Properties.Resources.edit_code;
            EditCode.Text = "自定义源码";

            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                LoadMapData,
                SaveHtml,
                SavePic,
                toolStripSeparator1,
                Clear,
                EditCode});
        }

        public void InitializeBossToolStrip()
        {
            LoadBossData = new ToolStripButton();
            SaveHtml = new ToolStripButton();
            SavePic = new ToolStripButton();

            // LoadBossData
            LoadBossData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            LoadBossData.Image = global::C2.Properties.Resources.importDataSource;
            LoadBossData.Text = "导入数据";
            LoadBossData.Click += new System.EventHandler(this.LoadBossData_Click);

            // SaveHtml
            SaveHtml.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            SaveHtml.Image = global::C2.Properties.Resources.save;
            SaveHtml.Text = "保存成html";

            // SavePic
            SavePic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            SavePic.Image = global::C2.Properties.Resources.image;
            SavePic.Text = "保存成图片";

            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                LoadBossData,
                SaveHtml,
                SavePic
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
            var dialog = new SelectBossDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
                webBrowser1.Navigate(dialog.WebUrl);
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            //string[] strArr = new string[4];
            //strArr[0] = "[{ \"lng\": \"114.363979\", \"lat\": \"36.03773\", \"count\": \"52\" }, { \"lng\": \"115.363979\", \"lat\": \"37.03773\", \"count\": \"53\" }]";

            string JSON_OBJ_Format = "\"lng\": \" {0} \", \"lat\": \" {1} \"";
            String.Format("\"lng\": \" {0} \", \"lat\": \" {1} \"", "114.376", "36.01");
            List<string> tmpList = new List<string>();
            var pointData = File.ReadAllLines(@"C:\Users\Administrator\Desktop\points.txt");

            var res = pointData.Select(x => x.Split('\t', ',')).ToArray();

            for (int i = 0; i < res.Length; i++)
            {

                tmpList.Add('{' + String.Format(JSON_OBJ_Format, res[1][0], res[i][1]) + '}');

            }
            //tmpList.Sort();

            string[] w = new string[1];
            w[0] = '[' + String.Join(",", tmpList.ToArray()) + ']';
            webBrowser1.Document.InvokeScript("getPoints", w);
        }
    }
}
