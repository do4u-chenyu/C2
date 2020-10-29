using C2.Business.DataSource;
using C2.Business.Model;
using C2.Business.Option;
using C2.Business.Schedule;
using C2.Controls.Flow;
using C2.Controls.Left;
using C2.Controls.Move;
using C2.Controls.Move.Dt;
using C2.Controls.Move.Op;
using C2.Core;
using C2.Core.UndoRedo;
using C2.Core.UndoRedo.Command;
using C2.Utils;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using C2.Controls;
using C2.Model.Documents;
using C2;
using C2.Model.MindMaps;
using C2.Model.Styles;
using C2.Globalization;
#region Blumind
using System.ComponentModel;
using System.IO;
using System.Text;
using C2.Configuration;
using C2.Controls.OS;
using C2.Core.Exports;
using C2.Dialogs;
using C2.Core.Win32Apis;
using C2.Forms;
#endregion

namespace C2
{
    public partial class MainForm : DocumentManageForm
    {
        #region
        StartMenuButton BtnStart;
        SpecialTabItem TabNew;
        TabBarButton BtnOpen;
        TabBarButton BtnHelp;
        TabBarButton BtnNew;
        AboutDialogBox AboutDialog;
        FindDialog MyFindDialog;
        ShortcutKeysMapDialog ShortcutsMapDialog;
        CheckUpdate CheckUpdateForm;
        ToolStripMenuItem MenuClearRecentFiles;
        ShortcutKeysTable ShortcutKeys;
        bool ImportMenusHasBuilded;
        StartPage startPage;
        #endregion

        private string userName;
        private C2.Dialogs.InputDataForm inputDataForm;
        private C2.Dialogs.CreateNewModelForm createNewModelForm;
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));

        private ModelDocumentDao modelDocumentDao;
        private OptionDao optionDao;
        public string UserName { get => this.userName; set => this.userName = value; }

        delegate void AsynUpdateLog(string logContent);
        delegate void AsynUpdateGif();
        delegate void TaskCallBack();
        delegate void AsynUpdateProgressBar();
        delegate void AsynUpdateMask();
        delegate void AsynUpdateOpErrorMessage();

        private static LogUtil log = LogUtil.GetInstance("MainForm"); // 获取日志模块
        private OpenFileDialog openFileDialog1;
        public MainForm(string userName)
        {
            this.UserName = userName;

            InitializeComponent();
            this.inputDataForm = new Dialogs.InputDataForm();
            this.inputDataForm.InputDataEvent += InputDataFormEvent;
            this.createNewModelForm = new Dialogs.CreateNewModelForm();

            this.leftToolBoxPanel.Width = 10;
            this.toolTip1.SetToolTip(this.leftFoldButton, "展开左侧面板");

            this.modelDocumentDao = new ModelDocumentDao();
            this.optionDao = new OptionDao();

            InitializeTaskBar();
            InitializeShortcutKeys();
            InitializeGlobalVariable();
            InitializeControlsLocation();
            InitializeLeftFold();

            MdiClient = this.mdiWorkSpace1;
            openFileDialog1 = new OpenFileDialog();
            this.NewDocument(false);
        }
        #region 初始化
        void InitializeTaskBar()
        {
            TaskBar = taskBar;
            TaskBar.Font = SystemFonts.MenuFont;
            TaskBar.Height = Math.Max(32, TaskBar.Height);
            TaskBar.MaxItemSize = 300;
            //TaskBar.Padding = new Padding(2, 0, 2, 0);

            BtnNew = new TabBarButton();
            BtnNew.Icon = Properties.Resources._new;
            BtnNew.ToolTipText = "Create New Document";
            BtnNew.Click += new EventHandler(MenuNew_Click);

            TaskBar.LeftButtons.Add(BtnNew);
            TaskBar.Items.ItemAdded += TaskBar_Items_ItemAdded;
            TaskBar.Items.ItemRemoved += TaskBar_Items_ItemRemoved;

            TabNew = new SpecialTabItem(Properties.Resources._new);
            TabNew.Click += new EventHandler(MenuNew_Click);
            TaskBar.RightSpecialTabs.Add(TabNew);
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
        #region Blumnd Hotkey
        void InitializeShortcutKeys()
        {
            KeyMap.Default.KeyManChanged += new EventHandler(Default_KeyManChanged);
            Default_KeyManChanged(null, EventArgs.Empty);

            ShortcutKeys = new ShortcutKeysTable();
            ShortcutKeys.Register(KeyMap.New, delegate () { NewDocument(); });
            ShortcutKeys.Register(KeyMap.Open, delegate () { OpenDocument(); });
            ShortcutKeys.Register(KeyMap.NextTab, delegate () { taskBar.SelectNextTab(false); });
            ShortcutKeys.Register(KeyMap.PreviousTab, delegate () { taskBar.SelectNextTab(true); });
        }
        void Default_KeyManChanged(object sender, EventArgs e)
        {
            //MenuNew.ShortcutKeyDisplayString = KeyMap.New.ToString();
            //MenuOpen.ShortcutKeyDisplayString = KeyMap.Open.ToString();
            //MenuSave.ShortcutKeyDisplayString = KeyMap.Save.ToString();
            //MenuQuickHelp.ShortcutKeys = KeyMap.Help.Keys;
        }
        #endregion
        private void InitializeGlobalVariable()
        {
            Global.SetMainForm(this);
        }
        private void InitializeControlsLocation()
        {
            //int x = this.canvasPanel.Width - 10 - this.naviViewControl.Width;
            //int y = this.canvasPanel.Height - 5 - this.naviViewControl.Height;

            //// 缩略图定位
            //this.naviViewControl.Location = new Point(x, y);
            //this.naviViewControl.Invalidate();

            //// 底层工具按钮定位
            //x = x - (this.canvasPanel.Width) / 2 + 100;
            //this.resetButton.Location = new Point(x + 100, y + 50);
            //this.stopButton.Location = new Point(x + 50, y + 50);
            //this.runButton.Location = new Point(x, y + 50);

            ////运行状态动图、进度条定位
            //this.currentModelRunBackLab.Location = new Point(x, this.canvasPanel.Height / 2 - 50);
            //this.currentModelFinLab.Location = new Point(x, this.canvasPanel.Height / 2 - 50);
            //this.progressBar1.Location = new Point(x, this.canvasPanel.Height / 2 + 54);
            //this.progressBarLabel.Location = new Point(x + 125, this.canvasPanel.Height / 2 + 50);

            //// 顶层浮动工具栏和右侧工具及隐藏按钮定位
            //this.flowControl.Location     = new Point(this.canvasPanel.Width - 70 - this.flowControl.Width, 50);
            //this.remarkControl.Location   = new Point(this.canvasPanel.Width - 70 - this.flowControl.Width, 50 + this.flowControl.Height + 10);
            //this.rightShowButton.Location = new Point(this.canvasPanel.Width - this.rightShowButton.Width , 50);
            //this.rightHideButton.Location = new Point(this.canvasPanel.Width - this.rightShowButton.Width , 50 + this.rightHideButton.Width + 10);

            //// 右上用户名，头像
            //int count = System.Text.RegularExpressions.Regex.Matches(userName, "[a-z0-9]").Count;
            //int rightMargin = (this.userName.Length - (count / 3) - 3) * 14;
            //this.usernamelabel.Text = this.userName;
            //Point userNameLocation = new Point(185, 10);
            //this.usernamelabel.Location = new Point(userNameLocation.X + 65 - rightMargin, userNameLocation.Y + 2);
            //this.helpPictureBox.Location = new Point(userNameLocation.X - rightMargin, userNameLocation.Y + 1);
            //this.portraitpictureBox.Location = new Point(userNameLocation.X + 30 - rightMargin, userNameLocation.Y + 1);
        }
        private void InitializeLeftFold()
        {
            this.leftFoldPanel.Location = new Point(this.leftFoldPanel.Location.X + this.mindMapModelControl.Width, this.leftFoldPanel.Location.Y);
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
            //// 已经为dirty了，就不需要再操作了，以提高性能
            //if (this.modelDocumentDao.CurrentDocument.Dirty)
            //    return;
            //this.modelDocumentDao.CurrentDocument.Dirty = true;
            //string currentModelTitle = this.modelDocumentDao.CurrentDocument.ModelTitle;
            //this.modelTitlePanel.ResetDirtyPictureBox(currentModelTitle, true);
        }
        public void DeleteCurrentDocument()
        {
            //UndoRedoManager.GetInstance().Remove(modelDocumentDao.CurrentDocument);
            //List<ModelElement> modelElements = modelDocumentDao.DeleteCurrentDocument();
            //modelElements.ForEach(me => canvasPanel.Controls.Remove(me.InnerControl));
            //this.naviViewControl.UpdateNaviView();
        }



        public void SaveCurrentDocument()
        {
            //string modelTitle = this.modelDocumentDao.SaveCurrentDocument();
            //if (!this.myModelControl.ContainModel(modelTitle))
            //    this.myModelControl.AddModel(modelTitle);
        }

        private void SaveAllDocuments()
        {
            //string[] modelTitles = this.modelDocumentDao.SaveAllDocuments();
            //foreach (string modelTitle in modelTitles)
            //{   // 加入左侧我的模型面板
            //    if (!this.myModelControl.ContainModel(modelTitle))
            //        this.myModelControl.AddModel(modelTitle);
            //    // 清空Dirty标志
            //    this.modelTitlePanel.ResetDirtyPictureBox(modelTitle, false);
            //}
        }
        private void LoadDocuments()
        {
            //if (this.modelDocumentDao.WithoutDocumentLogin(this.userName))
            //{
            //    this.modelTitlePanel.AddModel("我的新模型");
            //    this.modelDocumentDao.AddBlankDocument("我的新模型", this.userName);
            //    return;
            //}
            //// 穷举当前用户空间的所有模型
            //string[] modelTitles = this.modelDocumentDao.LoadSaveModelTitle(this.userName);
            //// 多文档面板加载控件
            ////this.modelTitlePanel.LoadModelDocument(modelTitles);
            ////加载用户空间的所有模型,并加入到canvas面板中
            //foreach (string mt in modelTitles)
            //{
            //    ModelDocument doc = this.modelDocumentDao.LoadDocument(mt, this.userName);
            //    CanvasAddElement(doc);
            //}
            //// 将用户本地保存的模型文档加载到左侧myModelControl
            //string[] allModelTitle = this.modelDocumentDao.LoadAllModelTitle(this.userName);
            //foreach (string modelTitle in allModelTitle)
            //{
            //    this.myModelControl.AddModel(modelTitle);
            //    if (!modelTitles._Contains(modelTitle))
            //        this.myModelControl.EnableClosedDocumentMenu(modelTitle);
            //}
            //// 显示当前模型
            //this.modelDocumentDao.CurrentDocument.Show();
            //// 更新当前模型备注信息
            //this.remarkControl.RemarkDescription = this.modelDocumentDao.RemarkDescription;
        }
        private void CanvasAddElement(ModelDocument doc)
        {
            //doc.ModelElements.ForEach(me => this.canvasPanel.Controls.Add(me.InnerControl));
            //this.naviViewControl.UpdateNaviView();
            //doc.UpdateAllLines();
        }

        private void MyModelButton_Click(object sender, EventArgs e)
        {
            this.ShowLeftFold();
            this.myModelControl.Visible = true;
            this.dataSourceControl.Visible = false;
            this.mindMapModelControl.Visible = false;
            this.flowChartControl.Visible = false;
        }

        private void OprateButton_Click(object sender, EventArgs e)
        {
            this.ShowLeftFold();
            this.mindMapModelControl.Visible = true;
            this.dataSourceControl.Visible = false;
            this.flowChartControl.Visible = false;
            this.myModelControl.Visible = false;
        }

        private void DataButton_Click(object sender, EventArgs e)
        {
            this.ShowLeftFold();
            this.dataSourceControl.Visible = true;
            this.mindMapModelControl.Visible = false;
            this.flowChartControl.Visible = false;
            this.myModelControl.Visible = false;
        }

        private void FlowChartButton_Click(object sender, EventArgs e)
        {
            this.ShowLeftFold();
            this.flowChartControl.Visible = true;
            this.dataSourceControl.Visible = false;
            this.mindMapModelControl.Visible = false;
            this.myModelControl.Visible = false;
        }


        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            InitializeControlsLocation();
        }


        private void NewModelButton_Click(object sender, EventArgs e)
        {
            //this.createNewModelForm.StartPosition = FormStartPosition.CenterScreen;
            //this.createNewModelForm.Owner = this;
            //DialogResult dialogResult = this.createNewModelForm.ShowDialog();

            //// 模型标题栏添加新标题
            //if (dialogResult == DialogResult.OK)
            //    this.modelTitlePanel.AddModel(this.createNewModelForm.ModelTitle);
            NewDocument(false);
        }

        private void InputDataFormEvent(string name, string fullFilePath, char separator, OpUtil.ExtType extType, OpUtil.Encoding encoding)
        {
            this.dataSourceControl.GenDataButton(name, fullFilePath, separator, extType, encoding);
            this.dataSourceControl.Visible = true;
            this.mindMapModelControl.Visible = false;
            this.flowChartControl.Visible = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //加载文件及数据源
            LoadDocuments();
            LoadDataSource();
        }
        private void LoadDataSource()
        {
            DataSourceInfo dataSource = new DataSourceInfo(this.userName);
            List<DataButton> dataButtons = dataSource.LoadDataSourceInfo();
            foreach (DataButton dataButton in dataButtons)
                this.dataSourceControl.GenDataButton(dataButton);
        }

        private void ShowLeftFold()
        {
            if (Global.GetCanvsaForm().isLeftViewPanelMinimum)
            {
                Global.GetCanvsaForm().isLeftViewPanelMinimum = false;
                this.leftToolBoxPanel.Width = 187;
                this.toolTip1.SetToolTip(this.leftFoldButton, "隐藏左侧面板");
            }
            Global.GetCanvsaForm().InitializeControlsLocation();
        }
        private void LeftFoldButton_Click(object sender, EventArgs e)
        {
            if (Global.GetCanvsaForm().isLeftViewPanelMinimum)
            {
                Global.GetCanvsaForm().isLeftViewPanelMinimum = false;
                this.leftToolBoxPanel.Width = 187;
                this.toolTip1.SetToolTip(this.leftFoldButton, "隐藏左侧面板");
            }
            else
            {
                Global.GetCanvsaForm().isLeftViewPanelMinimum = true;
                this.leftToolBoxPanel.Width = 10;
                this.toolTip1.SetToolTip(this.leftFoldButton, "展开左侧面板");
            }

            InitializeControlsLocation();
        }

        private void HelpPictureBox_Click(object sender, EventArgs e)
        {
            if (Global.VersionType.Equals(Global.GreenLevel))
                return;
            string helpfile = Application.StartupPath;
            helpfile += @"\Doc\IAO解决方案帮助文档v1.chm";
            Help.ShowHelp(this, helpfile);
        }

        private void SaveModelButton_Click(object sender, EventArgs e)
        {
            //// 如果文档不dirty的情况下, 对于大文档, 不做重复保存,以提高性能
            //if (!this.modelDocumentDao.CurrentDocument.Dirty)
            //    if (this.modelDocumentDao.CurrentDocument.ModelElements.Count > 10)
            //        return;

            //string currentModelTitle = this.modelDocumentDao.CurrentDocument.ModelTitle;
            //this.modelDocumentDao.UpdateRemark(this.remarkControl);
            //this.modelTitlePanel.ResetDirtyPictureBox(currentModelTitle, false);
            //SaveCurrentDocument();
        }


        private void UsernameLabel_MouseEnter(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(this.usernamelabel, this.userName + "已登录");
        }

        public void BlankButtonFocus()
        {
            this.blankButton.Focus();
        }
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData) //激活回车键
        {
            //int WM_KEYDOWN = 256;
            //int WM_SYSKEYDOWN = 260;

            //if (this.IsCurrentModelNotRun() && this.IsClickOnUneditableCtr() && (msg.Msg == WM_KEYDOWN | msg.Msg == WM_SYSKEYDOWN))
            //{
            //    if (keyData == Keys.Delete)
            //        this.canvasPanel.DeleteSelectedLinesByIndex();
            //    if (keyData == (Keys.C | Keys.Control))
            //        this.canvasPanel.ControlSelect_Copy();
            //    if (keyData == (Keys.V | Keys.Control))
            //        this.canvasPanel.ControlSelect_paste();
            //    if (keyData == (Keys.S | Keys.Control))
            //        this.SaveModelButton_Click(this, null);
            //    if (keyData == (Keys.Z | Keys.Control))
            //        this.topToolBarControl.UndoButton_Click(this, null);
            //    if (keyData == (Keys.Y | Keys.Control))
            //        this.topToolBarControl.RedoButton_Click(this, null);
            //}
            return false;
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        internal static extern IntPtr GetFocus();
        ///获取 当前拥有焦点的控件
        private Control GetFocusedControl()
        {
            Control focusedControl = null;
            // To get hold of the focused control:
            IntPtr focusedHandle = GetFocus();
            if (focusedHandle != IntPtr.Zero)
                //focusedControl = Control.FromHandle(focusedHandle)
                focusedControl = Control.FromChildHandle(focusedHandle);
            return focusedControl;

        }
        private bool IsCurrentModelNotRun()
        {
            //return Global.GetCurrentDocument().TaskManager.ModelStatus == ModelStatus.Running ? false : true;
            return false;
        }

        private bool IsClickOnUneditableCtr()
        {
            // TODO 目前解决方案属于穷举法，最好能找到当前控件是否有可编辑的属性
            Control focusedCtr = GetFocusedControl();
            if (focusedCtr is TextBox)
                return (focusedCtr as TextBox).ReadOnly;
            else if (focusedCtr is RichTextBox)
                return (focusedCtr as RichTextBox).ReadOnly;
            else
                return true;
        }

        private void SaveAllButton_Click(object sender, EventArgs e)
        {
            SaveAllDocuments();
        }
    
        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            if(Global.GetCanvasPanel() != null && Global.GetCanvasPanel().DragWrapper.StartDrag)
            {
                Global.GetCanvasPanel().DragWrapper.StartDrag = false;
                Global.GetCanvasPanel().DragWrapper.ControlChange();
            }
            if (Global.GetCanvasPanel() != null && Global.GetCanvasPanel().LeftButtonDown)
                Global.GetCanvasPanel().LeftButtonDown = false;
        }
        #region C2
        private void NewDocument(bool isCanvas = true)
        {
            if (isCanvas)
            {
                Document doc = CreateNewMap();

                DocumentForm form = new DocumentForm(doc);
                ShowForm(form);
            }
            else
            {
                ModelDocument doc = new ModelDocument(String.Empty, String.Empty);
                CanvasForm form = new CanvasForm(doc);
                ShowForm(form);
            }
        }
        public void NewDocumentForm()
        {
            this.NewDocument(false);
        }
        public 

        Document CreateNewMap()
        {
            MindMap map = new MindMap();
            map.Name = string.Format("{0} 1", Lang._("New Chart"));
            map.Root.Text = Lang._("Center Topic");
            map.Author = System.Environment.UserName;

            if (ChartThemeManage.Default.DefaultTheme != null)
            {
                map.ApplyTheme(ChartThemeManage.Default.DefaultTheme);
            }

            Document doc = new Document();
            doc.Name = Lang._("New Document");
            doc.Author = System.Environment.UserName;
            doc.Charts.Add(map);
            //doc.Modified = true;
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
        public void OpenDocument(string filename)
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
                OpenDocument(filename, fif.IsReadOnly);
            }
        }

        public void OpenDocument(string filename, bool readOnly)
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
                        form = OpenDocument(doc, readOnly);
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

        public BaseDocumentForm OpenDocument(Document doc, bool readOnly)
        {
            if (doc != null)
            {
                BaseDocumentForm form = new DocumentForm(doc);
                form.ReadOnly = readOnly;
                ShowForm(form);
                return form;
            }

            return null;
        }

        void OpenDocuments(string[] filenames)
        {
            if (filenames != null)
            {
                for (int i = 0; i < filenames.Length; i++)
                {
                    if (string.IsNullOrEmpty(filenames[i]))
                        continue;
                    OpenDocument(filenames[i]);
                }
            }
        }
        public void OpenDocument()
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                OpenDocument(openFileDialog1.FileName, openFileDialog1.ReadOnlyChecked);
            }
        }
        public void ShowOptionsDialog()
        {
            var dialog = new C2.Configuration.Dialog.SettingDialog();
            dialog.ShowDialog(this);
        }
        BaseDocumentForm FindDocumentForm(string filename)
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
        void BtnStart_Click(object sender, EventArgs e)
        {
            //StartMenu.Show(taskBar1, new Point(BtnStart.Bounds.Left, BtnStart.Bounds.Bottom + 1), ToolStripDropDownDirection.BelowRight);
            //BtnStart.ShowMenu(StartMenu);
        }
        void MenuNew_Click(object sender, System.EventArgs e)
        {
            NewDocument();
        }
        void MenuOpen_Click(object sender, EventArgs e)
        {
            OpenDocument();
        }
        void BtnHelp_Click(object sender, EventArgs e)
        {
            //MenuHelps.DropDown.Show(TaskBar, BtnHelp.Bounds.X, BtnHelp.Bounds.Bottom);
            //BtnHelp.ShowMenu(MenuHelps.DropDown);
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

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ImportDataSource_Click(object sender, EventArgs e)
        {
            this.inputDataForm.StartPosition = FormStartPosition.CenterScreen;
            this.inputDataForm.ShowDialog();
            this.inputDataForm.ReSetParams();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            this.NewDocument();
        }
    }
}
