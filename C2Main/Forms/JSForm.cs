using C2.Business.GlueWater;
using C2.Controls;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace C2.Forms
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public partial class JSForm : BaseForm
    {
        BaseForm _SelectedForm;
        private string excelPath;

        private DbSetting dbSetting;
        private readonly string webUrl = Path.Combine(Application.StartupPath, "Business/IAOLab/WebEngine/Html", "JSTable.html");

        public JSForm()
        {
            InitializeComponent();

            webBrowser.Navigate(webUrl);
            webBrowser.ObjectForScripting = this;

            dbSetting = new DbSetting();

            Init();
        }

        #region tab页代码
        public void Init() 
        {
            ShowForm(panel3, "涉赌专项", true, false, true);
            ShowForm(panel3, "涉枪专项", true, false, false);
            ShowForm(panel3, "涉黄专项", true, false, false);
            ShowForm(panel3, "盗洞专项", true, false, false);
            ShowForm(panel3, "后门黑吃黑专项", true, false, false);
            ShowForm(panel3, "境外网产专项", true, false, false);
            OnTaskBarChanged();
            OnMdiClientChanged();
        }
        public BaseForm SelectedForm
        {
            get { return _SelectedForm; }
            private set
            {
                if (_SelectedForm != value)
                {
                    var old = _SelectedForm;
                    _SelectedForm = value;
                    OnSelectedFormChanged(old);
                }
            }
        }
        protected virtual void OnSelectedFormChanged(BaseForm old)
        {
            /*
            if (mdiWorkSpace1 != null && SelectedForm != null)
            {
                mdiWorkSpace1.ActiveMdiForm(SelectedForm);
                Global.GetBottomViewPanel().Visible = SelectedForm.IsNeedShowBottomViewPanel();
            }
            */
        }
        private void OnMdiClientChanged()
        {
            /*
            if (mdiWorkSpace1 != null)
            {
                IsMdiContainer = false;

                mdiWorkSpace1.MdiFormActived += new EventHandler(MdiClient_MdiFormActived);
                mdiWorkSpace1.MdiFormClosed += new EventHandler(MdiClient_MdiFormClosed);
            }
            else
            {
                IsMdiContainer = true;
            }
            */
        }
        
        void MdiClient_MdiFormClosed(object sender, EventArgs e)
        {
            Form_FormClosed(sender, new FormClosedEventArgs(CloseReason.MdiFormClosing));
        }
        
        void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is BaseForm)
            {
                var form = (BaseForm)sender;
                if (tabBar1 != null)
                {
                    TabItem ti = tabBar1.GetItemByTag(form);
                    if (ti != null)
                    {
                        tabBar1.Items.Remove(ti);
                    }
                }

                if (form == SelectedForm)
                {
                    SelectedForm = null;
                }

                if (Forms1.Contains(form))
                {
                    Forms1.Remove(form);
                }
            }
        }
        void MdiClient_MdiFormActived(object sender, EventArgs e)
        {
            //SelectedForm = mdiWorkSpace1.ActivedMdiForm as BaseForm;

            Form_Activated(sender, e);
        }
        void Form_Activated(object sender, EventArgs e)
        {
            if (sender is Form && tabBar1 != null)
            {
                Form form = (Form)sender;
                tabBar1.SelectByTag(form);
                if (sender is C2.Forms.CanvasForm)
                    (sender as C2.Forms.CanvasForm).BlankButtonFocus();
            }
        }

        private void OnTaskBarChanged()
        {
            if (tabBar1 != null)
            {
                tabBar1.SelectedItemChanged += new EventHandler(TaskBar_SelectedItemChanged);
                tabBar1.ItemClose += new TabItemEventHandler(TaskBar_ItemClose);
            }
        }
        void TaskBar_SelectedItemChanged(object sender, EventArgs e)
        {
            if (tabBar1.SelectedItem != null)
            {
                Form form = tabBar1.SelectedItem.Tag as Form;
                if (form != null)
                {
                    /*
                    if (mdiWorkSpace1 != null)
                        mdiWorkSpace1.ActiveMdiForm(form);
                    else
                        form.Activate();
                    */
                    if (form is CanvasForm)
                    {
                        (form as CanvasForm).UpdateRunbuttonImageInfo();
                    }
                    else
                    {
                        Global.GetLeftToolBoxPanel().Enabled = true;
                    }
                }
            }
        }
        void TaskBar_ItemClose(object sender, TabItemEventArgs e)
        {
            if (e.Item != null && e.Item.Tag is Form)
            {
                /*
                if (mdiWorkSpace1 == null)
                    ((Form)e.Item.Tag).Close();
                else if (e.Item.Tag is DocumentForm)
                {
                    //不仅仅关闭当前form，关联form也要关掉
                    TabItem[] relateItem = tabBar1.Items.Where(ti => ti.Tag is CanvasForm).Where(ti => ti.ToolTipText != null && ti.ToolTipText.StartsWith(e.Item.ToolTipText + "-")).ToArray();
                    foreach (TabItem ti in relateItem)
                    {
                        if ((ti.Tag as CanvasForm).CanClose())
                            mdiWorkSpace1.CloseMdiForm((Form)ti.Tag);
                    }
                    if (tabBar1.Items.Where(ti => ti.Tag is CanvasForm).Where(ti => ti.ToolTipText != null && ti.ToolTipText.StartsWith(e.Item.ToolTipText + "-")).ToArray().Count() == 0)
                        mdiWorkSpace1.CloseMdiForm((Form)e.Item.Tag);
                }
                else if (e.Item.Tag is CanvasForm)
                {
                    //模型视图关闭前，需要判断是否还在运行
                    if ((e.Item.Tag as CanvasForm).CanClose())
                        mdiWorkSpace1.CloseMdiForm((Form)e.Item.Tag);
                }
                else
                    mdiWorkSpace1.CloseMdiForm((Form)e.Item.Tag);
                */

            }
        }
       
       /*
        public MdiWorkSpace MdiClient
        {
            get { return _MdiClient; }
            set
            {
                if (_MdiClient != value)
                {
                    _MdiClient = value;
                    OnMdiClientChanged();
                }
            }
        }
       */
        protected List<BaseForm> Forms1 { get; private set; }

        public IEnumerable<T> GetForms<T>()
            where T : BaseForm
        {
            return from f in Forms1
                   where f is T
                   select (T)f;
        }
        
        protected virtual void ShowForm(Panel panel, string name,bool showTab, bool canClose, bool visiable = true)
        {
            if (panel == null)
                throw new ArgumentNullException();
            Global.GetWorkSpacePanel().SuspendLayout();
            if (showTab && tabBar1 != null)
            {
                var ti = new TabItem
                {
                    Text = name,
                    CanClose = canClose,
                    Tag = panel,
                    //ToolTipText = panel.FormNameToolTip
                };

                /*
                 * 去掉图标
                if (form is BaseForm)
                    ti.Icon = form.IconImage;
                else
                    ti.Icon = PaintHelper.IconToImage(form.Icon);
                */

                /*
                if (form is CanvasForm && !string.IsNullOrEmpty(form.FormNameToolTip))
                    tabBar1.Items.Insert(tabBar1.Items.IndexOf(tabBar1.SelectedItem) + 1, ti);
                else if (form is CanvasForm)
                    //模型市场的模型默认在最前面打开
                    tabBar1.Items.Insert(2, ti);
                else
                    //tabBar1.Items.Add(ti);
                    tabBar1.Items.Add(ti);
                */
                tabBar1.Items.Add(ti);
                if (visiable)
                    tabBar1.SelectedItem = ti;
            }

            //form.TextChanged += new EventHandler(Form_TextChanged);
            //form.Activated += new EventHandler(Form_Activated);
            //form.FormClosed += new FormClosedEventHandler(Form_FormClosed);



            /*
            if (mdiWorkSpace1 != null)
            {
                if (visiable)
                    mdiWorkSpace1.ShowMdiForm(form);
                else
                    mdiWorkSpace1.AddMidForm(form); // 初始化时不需要显示
            }
            else if (IsMdiContainer)
            {
                form.MdiParent = this;
                form.WindowState = FormWindowState.Maximized;
                //form.FormBorderStyle = FormBorderStyle.None;
                form.ControlBox = false;
                form.Show();
            }
            */

            Global.GetWorkSpacePanel().ResumeLayout();
            /*
            if (!Forms1.Contains(form))
            {
                Forms1.Add(form);
            }
            */
        }
        void Form_TextChanged(object sender, EventArgs e)
        {
            if (sender is Form && tabBar1 != null)
            {
                Form form = (Form)sender;
                TabItem item = tabBar1.GetItemByTag(form);
                if (item != null)
                {
                    item.Text = form.Text;
                }
            }
        }
        #endregion

        public override bool IsNeedShowBottomViewPanel()
        {
            return false;
        }

        private void BrowserButton_Click(object sender, EventArgs e)
        {
            this.excelPathTextBox.Clear();
            OpenFileDialog OpenFileDialog = new OpenFileDialog
            {
                Filter = "文档 | *.xls;*.xlsx"
            };
            if (OpenFileDialog.ShowDialog() != DialogResult.OK)
                return;
            this.excelPathTextBox.Text = OpenFileDialog.FileName;
            excelPath = OpenFileDialog.FileName;

            if (dbSetting.UpdateContent(excelPath))
                MessageBox.Show("数据上传成功。");
            else
                MessageBox.Show("数据上传失败。");

            RefreshHtmlTable();
        }

        #region 界面html版

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        [System.Runtime.InteropServices.ComVisibleAttribute(true)]
        public void Hello(string a)
        {
            MessageBox.Show(dbSetting.SearchInfo(a));
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        [System.Runtime.InteropServices.ComVisibleAttribute(true)]
        public void InitDataTable()
        {
            dbSetting.InitDataTable();
            RefreshHtmlTable();
        }

        private void RefreshHtmlTable()
        {
            //有几个操作都会动态刷新html，初始化、添加、排序
            this.webBrowser.Document.InvokeScript("clearTable");

            //先试试初始化
            this.webBrowser.Document.InvokeScript("WfToHtml", new object[] { dbSetting.RefreshHtmlTable() });
        }
        #endregion

    }
}
