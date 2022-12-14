using C2.Business.DataSource;
using C2.Business.Model;
using C2.Business.WebsiteFeatureDetection;
using C2.ChartPageView;
using C2.Configuration;
using C2.Controls;
using C2.Controls.Left;
using C2.Controls.Move.Dt;
using C2.Controls.OS;
using C2.Core;
using C2.Core.Win32Apis;
using C2.Database;
using C2.Dialogs;
using C2.Forms;
using C2.Globalization;
using C2.IAOLab.Plugins;
using C2.Model;
using C2.Model.Documents;
using C2.Model.MindMaps;
using C2.Model.Widgets;
using C2.Update;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace C2
{
    public enum FormType
    {
        Null,
        DocumentForm,
        CanvasForm,
        StartForm,   // 首页
        JSForm       // 胶水界面
    }
    public partial class MainForm : DocumentManageForm
    {
        public string UserName { get => Global.GetUsername(); }

        SpecialTabItem TabNew;
        FindDialog MyFindDialog;
        ShortcutKeysTable ShortcutKeys;

        private bool isBottomViewPanelMinimum;
        public bool isLeftViewPanelMinimum;
        private InputDataForm inputDataForm;
        private Control[] leftPanelControls;
        private Control[] leftMainButtons;

        private static readonly Color LeftFocusColor = Color.FromArgb(228, 60, 89); // 红
        private static readonly Color LeftLeaveColor = Color.FromArgb(41, 60, 85);  // 蓝
        private string fullFilePath;

        private static DirectoryInfo info = new DirectoryInfo(Global.TempDirectory);
        private static string xmlDirectory = Path.Combine(info.Parent.FullName, "tmpRedisASK");
        private static string xmlPath = Path.Combine(xmlDirectory, "tmpRedisASK.xml");
        private static string installPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "install");
        private string startTime = DateTime.Now.ToString("yyyyMMddHHmmss");

        private void InitializeOpenFile(string path)
        {
            fullFilePath = path;
        }

        public MainForm(string ffp)
        {
            InitializeComponent();
#if C2_Outer
            searchToolkitButton.Visible = false;
            HIBUButton.Location = new Point(0, 422);
            castleBravoButton.Location = new Point(0, 362);
            iaoLabButton.Location = new Point(0, 302);
#endif
            InitializeInputDataForm();
            InitializeBottomPrviewPanel();
            InitializeLeftToolPanel();
            InitializeTaskBar();
            InitializeShortcutKeys();
            InitializeGlobalVariable();
            InitializeMdiClient();
            InitializeStartForm();
            OpenSavedTabs();
            InitializeOpenFile(ffp);
        }

        #region 初始化
        void InitializeInputDataForm()
        {
            inputDataForm = new InputDataForm();
            inputDataForm.InputDataEvent += InputDataFormEvent;
            Text = Global.GetMainWindowTitle();  // 标题用于关联快捷方式
        }
        void InitializeBottomPrviewPanel()
        {
            isBottomViewPanelMinimum = true;
            bottomViewPanel.Height = 40;
        }
        void InitializeLeftToolPanel()
        {
            isLeftViewPanelMinimum = true;
            this.leftToolBoxPanel.Width = 10;

            // 注册左侧一级按钮
            this.leftMainButtons = new Control[] {
                this.manualButton,
                this.mindMapButton,
                this.modelMarketButton,
                this.dataSourceButton,
                this.iaoLabButton,
                this.detectionButton,
                this.searchToolkitButton,
                this.castleBravoButton,
                this.HIBUButton
            };

            // 注册左侧二级面板
            this.leftPanelControls = new Control[] {
                this.manualControl,
                this.mindMapControl,
                this.modelMarketControl,
                this.dataSourceControl,
                this.iaoLabControl,
                this.websiteFeatureDetectionControl,
                this.searchToolkitControl,
                this.castleBravoControl,
                this.HIBUControl,
            };
            // 默认战术手册为初始选中状态
            this.manualButton.BackColor = LeftFocusColor;
        }
        void InitializeTaskBar()
        {
            TaskBar = taskBar;
            TaskBar.Font = SystemFonts.MenuFont;
            TaskBar.Height = Math.Max(32, TaskBar.Height);
            TaskBar.MaxItemSize = 300;

            TaskBar.Items.ItemAdded += TaskBar_Items_ItemAdded;
            TaskBar.Items.ItemRemoved += TaskBar_Items_ItemRemoved;

            TabNew = new SpecialTabItem(Properties.Resources._new);
            TabNew.Click += new EventHandler(NewDocumentForm_Click);
            TaskBar.RightSpecialTabs.Add(TabNew);

            var navBtnFirst = new TabBarNavButton(Lang._("First"), Properties.Resources.nav_small_first_white);
            navBtnFirst.Click += NavBtnFirst_Click;
            var navBtnPrev = new TabBarNavButton(Lang._("Previous"), Properties.Resources.nav_small_prev_white);
            navBtnPrev.Click += NavBtnPrev_Click;
            var navBtnNext = new TabBarNavButton(Lang._("Next"), Properties.Resources.nav_small_next_white);
            navBtnNext.Click += NavBtnNext_Click;
            var navBtnLast = new TabBarNavButton(Lang._("Last"), Properties.Resources.nav_small_last_white);
            navBtnLast.Click += NavBtnLast_Click;
            TaskBar.LeftButtons.Add(navBtnFirst);
            TaskBar.LeftButtons.Add(navBtnPrev);
            TaskBar.RightButtons.Insert(0, navBtnNext);
            TaskBar.RightButtons.Insert(1, navBtnLast);

            TaskBar.AllowScrollPage = true;
        }

        void InitializeWindowStates()
        {
            if (Options.Current.GetValue(OptionNames.Customizations.MainWindowMaximized, true))
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;

            if (Options.Current.Contains(OptionNames.Customizations.MainWindowSize))
            {
                Size = Options.Current.GetValue(OptionNames.Customizations.MainWindowSize, Size);
                SetAGoodLocation();
            }
        }
        void InitializeShortcutKeys()
        {
            ShortcutKeys = new ShortcutKeysTable();
            ShortcutKeys.Register(KeyMap.NextTab, delegate () { taskBar.SelectNextTab(false); });
            ShortcutKeys.Register(KeyMap.PreviousTab, delegate () { taskBar.SelectNextTab(true); });
        }

        void InitializeGlobalVariable()
        {
            Global.SetMainForm(this);
            Global.SetTaskBar(this.TaskBar);
            Global.SetLeftToolBoxPanel(this.leftToolBoxPanel);
            Global.SetDataSourceControl(this.dataSourceControl);
            Global.SetMyModelControl(this.modelMarketControl);
            Global.SetWebsiteFeatureDetectionControl(this.websiteFeatureDetectionControl);
            Global.SetCastleBravoControl(this.castleBravoControl);
            Global.SetSearchToolkitControl(this.searchToolkitControl);
            Global.SetLogView(this.bottomLogControl);
            Global.SetBottomViewPanel(this.bottomViewPanel);
            Global.SetWorkSpacePanel(this.workSpacePanel);
            Global.SetManualControl(this.manualControl);
            Global.SetMindMapControl(this.mindMapControl);
            Global.SetIAOLabControl(this.iaoLabControl);
            Global.SetHIBUControl(this.HIBUControl);
        }
        void InitializeMdiClient()
        {
            MdiClient = this.mdiWorkSpace;
        }
        void InitializeStartForm()
        {
            ShowForm(new StartForm(), true, false, true);
#if !C2_Outer
            ShowForm(new JSForm(), true, false, false);      // 想默认展示JSForm,ZZ策略,但JSForm鬼影太严重,支楞不起来
#endif
        }
        #endregion

        void SetAGoodLocation()
        {
            if (WindowState == FormWindowState.Normal)
            {
                Rectangle rect = Screen.GetWorkingArea(this);
                Location = new Point(Math.Max(rect.X, Math.Min(rect.Right - Width, Location.X)),
                    Math.Max(rect.Y, Math.Min(rect.Bottom - Height, Location.Y)));
            }
        }

        protected override void AfterInitialize()
        {
            base.AfterInitialize();
            InitializeWindowStates();
        }

        public void SetDocumentDirty()
        {
            Global.GetCurrentModelDocument().Modified = true;
        }

        public void BlankButtonFocus()
        {
            blankButton.Focus();
        }

        public void ShowLeftPanel(Control leftButton, Control leftPanel)
        {
            using (new GuarderUtil.LayoutGuarder(leftPanel))
            {
                SelectLeftPanel(leftButton, leftPanel);
                if (isLeftViewPanelMinimum)
                    this.ShowLeftFold();
            }
        }

        public void SelectLeftPanel(Control leftButton, Control leftPanel)
        {
            foreach (Control ct in leftPanelControls)
                ct.Visible = false;
            foreach (Control ct in leftMainButtons)
                ct.BackColor = LeftLeaveColor;

            leftPanel.Visible = true;
            leftButton.BackColor = LeftFocusColor;
        }
        //聚沙成塔
        private void ModelMarketButton_Click(object sender, EventArgs e)
        {
            if (!modelMarketControl.Visible || isLeftViewPanelMinimum)
                ShowLeftPanel(modelMarketButton, modelMarketControl);
        }

        private void MindMapButton_Click(object sender, EventArgs e)
        {
            if (!mindMapControl.Visible || isLeftViewPanelMinimum)
                ShowLeftPanel(mindMapButton, mindMapControl);
        }

        private void ManualButton_Click(object sender, EventArgs e)
        {
            if (!manualControl.Visible || isLeftViewPanelMinimum)
                ShowLeftPanel(manualButton, manualControl);
        }

        private void DataSourceButton_Click(object sender, EventArgs e)
        {
            ShowDataSourcePanel();
        }

        public void ShowDataSourcePanel()
        {
            if (!dataSourceControl.Visible || isLeftViewPanelMinimum)
                ShowLeftPanel(dataSourceButton, dataSourceControl);
        }

        private void IAOLabButton_Click(object sender, EventArgs e)
        {
            if (!iaoLabControl.Visible || isLeftViewPanelMinimum)  // 避免反复点击时的闪烁
                ShowLeftPanel(iaoLabButton, iaoLabControl);
        }

        private void HIBUButton_Click(object sender, EventArgs e)
        {
            if (!HIBUControl.Visible || isLeftViewPanelMinimum)  // 避免反复点击时的闪烁
                ShowLeftPanel(HIBUButton, HIBUControl);
        }

        private void DetectionButton_Click(object sender, EventArgs e)
        {
            if (!websiteFeatureDetectionControl.Visible || isLeftViewPanelMinimum)
                ShowLeftPanel(detectionButton, websiteFeatureDetectionControl);
        }

        private void SearchToolkitButton_Click(object sender, EventArgs e)
        {
            if (!searchToolkitControl.Visible || isLeftViewPanelMinimum)
                ShowLeftPanel(searchToolkitButton, searchToolkitControl);
        }

        private void CastleBravoButton_Click(object sender, EventArgs e)
        {
            if (!castleBravoControl.Visible || isLeftViewPanelMinimum)
                ShowLeftPanel(castleBravoButton, castleBravoControl);
        }
        private void InputDataFormEvent(string name, string fullFilePath, char separator, OpUtil.ExtType extType, OpUtil.Encoding encoding)
        {
            dataSourceControl.GenDataButton(name, fullFilePath, separator, extType, encoding);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //加载文件及数据源
            using (GuarderUtil.WaitCursor)
            {
                LoadHotModel();
                LoadDocuments();
                LoadFile();
                LoadDataSource();
                LoadIAOLaboratory();
                LoadHIBU();
                LoadHeadLine();
#if !C2_Inner
                CheckUpdate();
                Identification();
#endif
            }
        }
        public void CheckUpdate()
        {
            SoftUpdate app = new SoftUpdate(Application.ExecutablePath, "C2");
            app.UpdateFinish += new UpdateState(App_UpdateFinish);
            app.SaveFinish += new UpdateState(App_SaveFinish);
            try
            {
                if (app.IsUpdate == 1 && MessageBox.Show("检查到C2新版本，是否更新？", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    new Log.Log().LogManualButton("外网版", "下载");
                    if (!File.Exists(installPath))
                        Directory.CreateDirectory(installPath);
                    Thread update = new Thread(new ThreadStart(app.Update));
                    update.Start();
                    this.Dispose();
                }
                else if (app.IsUpdate == 2 && MessageBox.Show("检查到战术手册新版本，是否更新？", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    new Log.Log().LogManualButton("战术手册", "下载");
                    if (!File.Exists(installPath))
                        Directory.CreateDirectory(installPath);
                    Thread update = new Thread(new ThreadStart(app.Update));
                    update.Start();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void App_UpdateFinish()
        {
            MessageBox.Show("下载完成，请点击进行安装！", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information); 
        }
        void App_SaveFinish()
        {
            MessageBox.Show("新版本下载完成", "Download", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Identification()
        {
            if (!File.Exists(xmlPath))
                AddIden();

            if (File.Exists(xmlPath))
            {
                string endTime = EndTimeString();
                if (string.Compare(endTime, startTime, true) == 1)
                    return;
                else
                {
                    File.Delete(xmlPath);
                    AddIden();
                }
            }
        }

        private void AddIden()
        {
            string result = new WFDWebAPI().ReAuthBeforeQueryC2();

            if (result.Contains("false"))
                Close();
            else if (!result.Contains("首字母大写"))
                Save();
            else
            { }
        }

        private void Save()
        {
            Directory.CreateDirectory(xmlDirectory);
            XmlDocument xDoc = new XmlDocument();
            XmlElement rootElement = xDoc.CreateElement("IdenInformation");
            xDoc.AppendChild(rootElement);
            SaveUserInfo(rootElement);
            xDoc.Save(xmlPath);
        }

        private void SaveUserInfo(XmlNode node)
        {
            string endTime = EndTimeString();
            new ModelXmlWriter("userInfo", node)
                .Write("StartTime", startTime)
                .Write("userName", WFDWebAPI.GetInstance().UserName);
        }

        private string EndTimeString()
        {
            return ConvertUtil.TryParseDateTime(Program.LinceseDeadLine, Program.DateTimeFormat).ToString("yyyyMMddHHmmss");
        }

        private void LoadHeadLine()
        {
            string v = ConfigUtil.TryGetAppSettingsByKey("version", string.Empty);
            string d = ConfigUtil.TryGetAppSettingsByKey("BuildDay", string.Empty);
            label2.Text = string.Format("{0}|编译日期:{1}|{2}", v, d, Program.LinceseDeadLineDesc());
            if (v == "内网版")
            {
                Task.Run(() => {
                    this.Invoke(new Action(() =>
                    {
                        bool b = NetUtil.Ping("8.8.8.8") != -1 && NetUtil.Ping("114.114.114.114") != -1;
                        if (b)
                            label2.Text = "能接入互联网|" + this.label2.Text;
                    }));
                });
            }
        }
        private void LoadFile()
        {
            using (GuarderUtil.WaitCursor)
                if (ImportModel.GetInstance().UnZipC2File(fullFilePath, UserName))
                    HelpUtil.ShowMessageBox(String.Format("[{0}] 导入 分析笔记 成功", Path.GetFileNameWithoutExtension(fullFilePath)));
        }

        private void LoadHotModel()
        {

            string HotModelPath = Global.TemplatesPath;
            string[] ModelFiles = Directory.GetFiles(HotModelPath, "*.iao");

            foreach (string file in ModelFiles)
            {
                ImportModel.GetInstance().UnZipIaoFile(file, UserName);
            }

        }

        private void LoadDocuments()
        {
            // 将用户本地保存的模型文档加载到左侧myModelControl	
            string[] bsTitles = ModelInfo.LoadAllModelTitle(Global.MindMapViewPath);
            string[] mtTitles = ModelInfo.LoadAllModelTitle(Global.MarketViewPath);
            string[] mvTitles = ModelInfo.LoadAllModelTitle(Global.ManualViewPath);

            foreach (string title in bsTitles)
                this.mindMapControl.AddButton(title);
            foreach (string title in mvTitles)
                this.manualControl.AddButton(title);
            foreach (string title in mtTitles)
                this.modelMarketControl.AddModel(title);
        }



        private void LoadDataSource()
        {

            DataSourceInfo dataSource0 = new DataSourceInfo(this.UserName);
            List<DataButton> dataButtons = dataSource0.LoadDataSourceInfo();

            foreach (DataButton dataButton in dataButtons)
                this.dataSourceControl.GenDataButton(dataButton);
            // 外部数据源加载
            DataSourceInfo dataSource1 = new DataSourceInfo(this.UserName, "ExternalDataInformation.xml");
            List<LinkButton> linkButtons = dataSource1.LoadExternalData();
            foreach (LinkButton linkButton in linkButtons)
                this.dataSourceControl.GenLinkButton(linkButton);
        }

        private void LoadIAOLaboratory()
        {
            // 加载固定的6个小工具
            LoadInnerPlugins();
            // 加载DLL动态插件
            LoadDllPlugins();
        }

        private void LoadInnerPlugins()
        {
            foreach (string name in ConfigUtil.DefaultIAOLab.Split(','))
                this.iaoLabControl.GenIAOButton(name.Trim());
        }

        private void LoadDllPlugins()
        {
            PluginsManager.Instance.Refresh();

            foreach (IPlugin plugin in PluginsManager.Instance.Plugins)
            {
                string name = plugin.GetPluginName();
                string desc = plugin.GetPluginDescription();
                Image icon = plugin.GetPluginImage();

                this.iaoLabControl.GenIAOButton(name, desc, icon).ShowDialogDelegate += delegate () { plugin.ShowFormDialog(); };
            }
        }

        private void LoadHIBU()
        {
            // 加载固定工具
            string[] HIArr = { "图片文本识别", "涉赌文本识别", "涉政文本识别", " 涉黄图像识别",
                               "涉恐图像识别", "涉藏图像检测", "涉枪图像检测", "颜值打分",
                               "人脸年龄性别识别", "银行卡图像识别", "红包转账图像识别", "卡证识别",
                               "语种识别", "语音转文本", "二维码识别", "二维码图像识别",
                               "红头文件识别", "命名实体识别", "信息抽取", "轨迹联通类图像识别",
                               "表情识别", "人脸检测", "人脸识别"};
            foreach (string name in HIArr)
                this.HIBUControl.GenIAOButton(name.Trim());
        }

        private void ShowLeftFold()
        {
            this.isLeftViewPanelMinimum = false;
            this.toolTip1.SetToolTip(this.leftFoldButton, "隐藏左侧面板");
            this.leftToolBoxPanel.Width = 187;
        }

        private void HideLeftFold()
        {
            this.isLeftViewPanelMinimum = true;
            this.leftToolBoxPanel.Width = 10;
            this.toolTip1.SetToolTip(this.leftFoldButton, "展开左侧面板");
        }
        private void LeftFoldButton_Click(object sender, EventArgs e)
        {
            if (this.isLeftViewPanelMinimum)
            {
                int i = Array.FindIndex(leftMainButtons, v => v.BackColor == LeftFocusColor);
                ShowLeftPanel(leftMainButtons[i], leftPanelControls[i]);
            }
            else
                HideLeftFold();
        }

        private void UsernameLabel_MouseEnter(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(this.usernamelabel, this.usernamelabel.Text + "已登录");
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            if (Global.GetCanvasPanel() != null && Global.GetCanvasPanel().DragWrapper.StartDrag)
            {
                Global.GetCanvasPanel().DragWrapper.StartDrag = false;
                Global.GetCanvasPanel().DragWrapper.ControlChange();
            }
            if (Global.GetCanvasPanel() != null && Global.GetCanvasPanel().LeftButtonDown)
                Global.GetCanvasPanel().LeftButtonDown = false;
        }
#region C2
        private void NewDocumentForm(string name = "", string titile = "")
        {
            Document doc = CreateNewMap(name);
            if (!string.IsNullOrEmpty(titile))
                doc.Name = titile;
            DocumentForm form = new DocumentForm(doc);
            ShowForm(form);
        }
        public void ShowFormWord(DocumentForm form)
        {
            ShowForm(form);
        }

        public void NewCanvasFormByMindMap(string modelDocumentName, Topic topic)
        {
            ModelDocument doc = new ModelDocument(modelDocumentName, this.UserName)
            {
                SavePath = Path.Combine(Global.GetDefaultDocumentDirectory(), modelDocumentName)
            };
            CanvasForm form = new CanvasForm(doc, topic, Global.GetDocumentForm());
            ShowForm(form);
            form.GenMindMapDataSources(topic);
            OperatorWidget opw = topic.FindWidget<OperatorWidget>();
            if (opw != null)
                opw.ModelRelateTab = TaskBar.SelectedItem;
            form.Save();
        }

        public void LoadCanvasFormByXml(string savePath, string modelTitle)
        {
            ModelDocument doc = new ModelDocument(modelTitle, UserName)
            {
                SavePath = Path.Combine(savePath, modelTitle)
            };
            CanvasForm form = new CanvasForm(doc);
            doc.Load();
            form.CanvasAddElement(doc);
            ShowForm(form);
            form.RemarkControl.RemarkDescription = doc.RemarkDescription;
            Global.GetCanvasForm().Save();
        }

        public void LoadCanvasFormByMindMap(string modelDocumentName, Topic topic)
        {
            ModelDocument doc = new ModelDocument(modelDocumentName, UserName)
            {
                SavePath = Path.Combine(Global.GetDefaultDocumentDirectory(), modelDocumentName)
            };
            CanvasForm form = new CanvasForm(doc, topic, Global.GetDocumentForm());
            ShowForm(form);

            doc.Load();
            // 加载时暂停事件,不然导致非期望的文档dirty
            form.RemarkControl.RemarkChangeEventStop = true;
            form.RemarkControl.RemarkDescription = doc.RemarkDescription;
            form.RemarkControl.RemarkChangeEventStop = false;

            form.CanvasAddElement(doc);
            form.GenMindMapDataSources(topic);
            OperatorWidget opw = topic.FindWidget<OperatorWidget>();
            if (opw != null)
                opw.ModelRelateTab = TaskBar.SelectedItem;
        }

        public Document CreateNewMapForWord(string templateName)
        {
            return CreateNewMap(templateName);
        }
        Document CreateNewMap(string templateName)
        {
            Document doc = LoadDocumentTemplate(templateName);
            doc.Modified = true;
            return doc;
        }
        private Document LoadDocumentTemplate(string templateName)
        {
            Document doc;
            switch (templateName)
            {
                case "逻辑图":
                    doc = Document.LoadXml("逻辑图", Properties.Resources.逻辑图); ;
                    break;
                case "树状图":
                    doc = Document.LoadXml("树状图", Properties.Resources.树状图); ;
                    break;
                case "组织架构图":
                    doc = Document.LoadXml("组织架构图", Properties.Resources.组织架构图); ;
                    break;
                case "思维导图":
                    doc = Document.LoadXml("思维导图", Properties.Resources.思维导图); ;
                    break;
                default:
                    doc = Document.LoadXml("空模板", Properties.Resources.空模板);
                    break;
            }
            return doc;
        }

        public void ShowFindDialog(ChartControl chartControl, FindDialog.FindDialogMode mode)
        {
            if (MyFindDialog == null || MyFindDialog.IsDisposed)
            {
                FindDialog fd = MyFindDialog;
                MyFindDialog = new FindDialog(this);
                if (fd != null)
                {
                    MyFindDialog.StartPosition = FormStartPosition.Manual;
                    MyFindDialog.Location = fd.Location;
                    MyFindDialog.OpenOptions = fd.OpenOptions;
                }
            }

            MyFindDialog.Mode = mode;
            if (MyFindDialog.Visible)
                MyFindDialog.Activate();
            else
                MyFindDialog.Show(this);
            MyFindDialog.ResetFocus();
        }
        public void OpenMindMapDocument(string filename)
        {
            bool isManual = filename.StartsWith(Global.ManualViewPath);
            OpenDocument(filename, isManual);
        }

        private void OpenDocument(string filename, bool isManual)
        {
            if (string.IsNullOrEmpty(filename))
                return;

            if (!File.Exists(filename))
            {
                this.ShowMessage(string.Format(Lang._("File \"{0}\" Not Exists"), filename), MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrEmpty(filename) && File.Exists(filename))
            {
                FileInfo fif = new FileInfo(filename);
                DoOpenDocument(filename, fif.IsReadOnly, isManual);
            }
        }

        public void OpenManualDocument(string filename)
        {
            OpenDocument(filename, true);
        }

        public void OpenJSTab(string tabName)
        {
            TabItem item = taskBar.GetJSForm();
            taskBar.SelectedItem = item;
            (item.Tag as JSForm).SelectTabByName(tabName);
        }

        private void DoOpenDocument(string filename, bool readOnly, bool isManual = false)
        {
            BaseDocumentForm form = FindDocumentForm(filename);
            if (form != null)
            {
                SelectForm(form);
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    Document doc = null;
                    string ext = Path.GetExtension(filename);
                    if (ext.ToLower() == ".mm")
                        doc = FreeMindFile.LoadFile(filename);
                    else
                        doc = Document.Load(filename);

                    if (doc != null)
                    {
                        form = OpenDocument(doc, readOnly, isManual);
                        if (form != null)
                            form.Filename = filename;
                    }

                    RecentFilesManage.Default.Push(filename);
                    Cursor.Current = Cursors.Default;
                }
                catch (System.Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    Helper.WriteLog(ex);
                    this.ShowMessage("File name is invalid or the format is not supported", MessageBoxIcon.Error);
                }
            }
        }

        public BaseDocumentForm OpenDocument(Document doc, bool readOnly, bool isManual = false)
        {
            if (doc != null)
            {
                BaseDocumentForm form = new DocumentForm(doc)
                {
                    ReadOnly = readOnly,
                    Description = isManual ? Global.ManualDesc : Global.MindMapDesc,
                    IconImage = isManual ? Properties.Resources.战术手册 : Properties.Resources.分析笔记1
                };
                ShowForm(form);
                return form;
            }

            return null;
        }

        public BaseDocumentForm FindDocumentForm(string filename)
        {
            foreach (Form form in Forms)
            {
                if (form is BaseDocumentForm && StringComparer.OrdinalIgnoreCase.Equals(((BaseDocumentForm)form).Filename, filename))
                {
                    return (BaseDocumentForm)form;
                }
            }

            return null;
        }

        void NavBtnLast_Click(object sender, EventArgs e)
        {
            TaskBar.ScrollToLast();
        }

        void NavBtnPrev_Click(object sender, EventArgs e)
        {
            TaskBar.ScrollToPrev();
        }

        void NavBtnNext_Click(object sender, EventArgs e)
        {
            TaskBar.ScrollToNext();
        }

        void NavBtnFirst_Click(object sender, EventArgs e)
        {
            TaskBar.ScrollToFirst();
        }
        void NewDocumentForm_Click(object sender, System.EventArgs e)
        {
            // 文档重命名
            NewDocumentForm_Click(string.Empty);
        }
        public void NewDocumentForm_Click(string templateName)
        {

            CreateNewModelForm createNewModelForm = new CreateNewModelForm
            {
                StartPosition = FormStartPosition.CenterScreen,
                Owner = this,
                OpenDocuments = OpenedDocuments(),
                NewFormType = FormType.DocumentForm,
                ModelType = "新建分析笔记"
            };

            DialogResult dialogResult = createNewModelForm.ShowDialog();
            // 新建业务视图
            if (dialogResult == DialogResult.OK)
            {
                NewDocumentForm(templateName, createNewModelForm.ModelTitle);
                new Log.Log().LogManualButton("分析笔记", "新建");
            }
        }

        void TaskBar_Items_ItemRemoved(object sender, XListEventArgs<TabItem> e)
        {
            RefreshFunctionTaskBarItems();
        }

        void TaskBar_Items_ItemAdded(object sender, XListEventArgs<TabItem> e)
        {
            RefreshFunctionTaskBarItems();
        }
        void RefreshFunctionTaskBarItems()
        {
            var hasForms = TaskBar.Items.Exists(item => item.Tag is Form);
            TabNew.Visible = hasForms;
        }
#endregion
#region 底部控件事件
        public void PreViewDataByFullFilePath(object sender, string fullFilePath, char separator, OpUtil.ExtType extType, OpUtil.Encoding encoding, bool isForceRead = false)
        {
            if (!File.Exists(fullFilePath))
            {
                if (sender is MoveDtControl || sender is DataButton || sender is ToolStripMenuItem)
                    HelpUtil.ShowMessageBox("该数据文件不存在");
                return;
            }
            this.ShowBottomPanel();
            this.bottomPreview.PreViewDataByFullFilePath(fullFilePath, separator, extType, encoding, isForceRead);
            this.ShowBottomPreview();
        }

        public void PreViewDataSource(DataItem item, bool isForceRead = false)
        {
            if (item.IsDatabase())
            {
                IDAO dao = DAOFactory.CreateDAO(item.DBItem);
                if (dao.TestConn())
                {
                    this.ShowBottomPanel();
                    this.bottomPreview.PreViewDataByDatabase(item);
                    this.ShowBottomPreview();
                }
                else
                    HelpUtil.ShowMessageBox(HelpUtil.DbCannotBeConnectedInfo);
                return;
            }

            if (!File.Exists(item.FilePath))
            {
                HelpUtil.ShowMessageBox("该数据文件不存在");
                return;
            }
            this.ShowBottomPanel();
            this.bottomPreview.PreViewDataByFullFilePath(item.FilePath, item.FileSep, item.FileType, item.FileEncoding, isForceRead);
            this.ShowBottomPreview();
        }
        private void ShowLogView()
        {
            this.bottomLogControl.Visible = true;
            this.bottomPyConsole.Visible = false;
            this.bottomPreview.Visible = false;
            this.bottomLogControl.FocusLocation();
        }
        private void ShowPyConsole()
        {
            this.bottomPyConsole.Visible = true;
            this.bottomLogControl.Visible = false;
            this.bottomPreview.Visible = false;
        }
        private void ShowBottomPreview()
        {
            this.bottomLogControl.Visible = false;
            this.bottomPyConsole.Visible = false;
            this.bottomPreview.Visible = true;
        }
        public void ShowBottomPanel()
        {
            if (this.isBottomViewPanelMinimum == true)
            {
                this.isBottomViewPanelMinimum = false;
                this.bottomViewPanel.Height = 200;
                this.minMaxPictureBox.Image = global::C2.Properties.Resources.minfold;
            }
            if (bottomViewPanel.Height == 200)
            {
                this.toolTip1.SetToolTip(this.minMaxPictureBox, "隐藏底层面板");
            }
            if (bottomViewPanel.Height == 40)
            {
                this.toolTip1.SetToolTip(this.minMaxPictureBox, "展开底层面板");
            }
        }
        private void PreviewLabel_Click(object sender, EventArgs e)
        {
            this.ShowBottomPanel();
            this.ShowBottomPreview();

        }

        private void PyControlLabel_Click(object sender, EventArgs e)
        {
            this.ShowBottomPanel();
            this.ShowPyConsole();
        }

        private void LogLabel_Click(object sender, EventArgs e)
        {
            ShowLogViewG();
        }

        // 全局, 供其他人调用
        public void ShowLogViewG()
        {
            this.ShowBottomPanel();
            this.ShowLogView();
        }
#endregion
        private void MinMaxPictureBox_Click(object sender, EventArgs e)
        {
            if (this.isBottomViewPanelMinimum == true)
            {
                this.isBottomViewPanelMinimum = false;
                this.bottomViewPanel.Height = 200;
                this.minMaxPictureBox.Image = global::C2.Properties.Resources.minfold;
            }
            else
            {
                this.isBottomViewPanelMinimum = true;
                this.bottomViewPanel.Height = 40;
                this.minMaxPictureBox.Image = global::C2.Properties.Resources.maxunfold;
            }
            if (bottomViewPanel.Height == 200)
            {
                this.toolTip1.SetToolTip(this.minMaxPictureBox, "隐藏底层面板");
            }
            if (bottomViewPanel.Height == 40)
            {
                this.toolTip1.SetToolTip(this.minMaxPictureBox, "展开底层面板");
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (!TrySaveTabs())
            {
                e.Cancel = true;
                return;
            }

            if (!mdiWorkSpace.CloseAll())
            {
                e.Cancel = true;
            }
        }
        private bool TrySaveTabs()
        {
            var saveTabs = Options.Current.GetValue(OptionNames.Miscellaneous.SaveTabs, SaveTabsType.Ask);
            if (saveTabs == SaveTabsType.No)
                return true;

            // ensure document saved
            bool cancel = false;
            ConfirmSaveDocuments(ref cancel);
            if (cancel)
                return false;

            // ask and save
            string[] tabs = GetOpendDocuments();

            if (!tabs.IsEmpty() && saveTabs == SaveTabsType.Ask)
            {
                var dialog = new SaveTabsDialog();
                var dr = dialog.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                    return false;

                if (dialog.DoNotAskAgain)
                    Options.Current.SetValue(OptionNames.Miscellaneous.SaveTabs, (dr == DialogResult.Yes) ? SaveTabsType.Yes : SaveTabsType.No);
                else
                    Options.Current.SetValue(OptionNames.Miscellaneous.SaveTabs, SaveTabsType.Ask);

                if (dr == DialogResult.No)
                {
                    Options.Current.SetValue(OptionNames.Miscellaneous.LastOpenTabs, null);
                    return true;
                }
            }

            Options.Current.SetValue(OptionNames.Miscellaneous.LastOpenTabs, tabs);
            return true;
        }
        private void OpenSavedTabs()
        {
            if (Options.Current.GetValue<SaveTabsType>(OptionNames.Miscellaneous.SaveTabs) == SaveTabsType.No)
                return;

            var tabs = Options.Current.GetValue<string[]>(OptionNames.Miscellaneous.LastOpenTabs);
            if (tabs.IsNullOrEmpty())
                return;

            foreach (var filename in tabs)
                if (!string.IsNullOrEmpty(filename) && File.Exists(filename))
                    OpenMindMapDocument(filename);
        }

        /*
        protected override void DefWndProc(ref Message m)
        {
            switch(m.Msg)
            {
                case WinMessages.WM_COPYDATA:
                    DoCopyData(m);
                    break;
                default:
                    base.DefWndProc(ref m);
                    break;
            }
            
        }
        */

        private void DoCopyData(Message m)
        {
            COPYDATASTRUCT cd = (COPYDATASTRUCT)m.GetLParam(new COPYDATASTRUCT().GetType());
            if (cd.dwData == new IntPtr(Program.OPEN_FILES_MESSAGE))
            {
                byte[] bytes = OSHelper.GetBuffer(cd.lpData, cd.cbData);
                fullFilePath = Encoding.UTF8.GetString(bytes);
                SetForegroundWindow();
                DoOpenFile();
            }
        }

        private void DoOpenFile()
        {
            // 这里没有支持加密的.c2, 后续要改进
            using (GuarderUtil.WaitCursor)
                LoadFile();
        }
        private void SetForegroundWindow()
        {
            User32.ShowWindowAsync(this.Handle, ShowWindowFlags.SW_MAXIMIZE);
            User32.SetForegroundWindow(this.Handle);
        }
    }
}
