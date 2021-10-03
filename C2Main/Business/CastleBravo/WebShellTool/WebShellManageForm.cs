﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.WebShellTool
{
    public partial class WebShellManageForm : Form
    {
        List<WebShellTaskConfig> tasks = new List<WebShellTaskConfig>();
        readonly string configFFP = Path.Combine(Application.StartupPath, "Resources", "WebShellConfig", "config.db");
        public WebShellManageForm()
        {
            InitializeComponent();
        }

        private void AddShellMenu_Click(object sender, EventArgs e)
        {
            WebShellTaskConfig config = new AddWebShellForm().ShowDialog();
            if (config == WebShellTaskConfig.Empty)
                return;

            ListViewItem lvi = new ListViewItem(config.CreateTime);
            lvi.SubItems.Add(config.Name);
            lvi.SubItems.Add(config.Url);
            lvi.SubItems.Add(config.Password);
            lvi.SubItems.Add(config.TrojanType);
            lvi.SubItems.Add(config.ClientVersion);
            lvi.SubItems.Add(config.DatabaseConfig);
            this.LV.Items.Add(lvi);

            tasks.Add(config);
            SaveDB();
        }

        private void RefreshTasks()
        {
            tasks.Clear();
            foreach (ListViewItem lvi in LV.Items)
                tasks.Add(new WebShellTaskConfig(lvi.SubItems[0].Text,  // 创建时间
                                                 lvi.SubItems[1].Text,  // 名称
                                                 lvi.SubItems[2].Text,  // url
                                                 lvi.SubItems[3].Text,  // 密码
                                                 lvi.SubItems[4].Text,  // 木马类型
                                                 lvi.SubItems[5].Text,  // 客户端版本
                                                 lvi.SubItems[6].Text));// 数据库配置
        }

        private void SaveDB()
        {
            try
            {
                using (Stream stream = File.Open(configFFP, FileMode.Create))
                   new BinaryFormatter().Serialize(stream, tasks);
            }
            catch { }
        }

        private void WebShellManageForm_Load(object sender, EventArgs e)
        {
            LoadDB();
            RefreshLV();
        }

        private void LoadDB()
        {
            try
            {
                using (Stream stream = File.Open(configFFP, FileMode.Open))
                    tasks = new BinaryFormatter().Deserialize(stream) as List<WebShellTaskConfig>;
            }
            catch { }

        }

        public void RefreshLV()
        {
            LV.Items.Clear();  // 不能删表头的clear方法

            foreach (WebShellTaskConfig config in tasks)
            {
                ListViewItem lvi = new ListViewItem(config.CreateTime);
                lvi.SubItems.Add(config.Name);
                lvi.SubItems.Add(config.Url);
                lvi.SubItems.Add(config.Password);
                lvi.SubItems.Add(config.TrojanType);
                lvi.SubItems.Add(config.ClientVersion);
                lvi.SubItems.Add(config.DatabaseConfig);

                LV.Items.Add(lvi);
            }
        }

        private void EnterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.LV.SelectedItems.Count == 0)
                return;

            //new WebShellDetailsForm((WebShellTaskConfig)this.listView1.SelectedItems[0].Tag).ShowDialog();
        }

        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in LV.SelectedItems)
                lvi.Remove();
            
            RefreshTasks();
            SaveDB();
        }
    }
}
