using C2.Business.GlueWater;
using C2.Business.GlueWater.Settings;
using C2.Controls;
using C2.Core;
using System;
using System.IO;
using System.Windows.Forms;


namespace C2.Forms
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public partial class JSForm : BaseForm
    {
        private string excelPath;
        private readonly string webUrl = Path.Combine(Application.StartupPath, "Business/IAOLab/WebEngine/Html", "JSTable.html");
        IGlueSetting glueSetting;

        public JSForm()
        {
            InitializeComponent();
            InitTabItems();

            webBrowser.Navigate(webUrl);
            webBrowser.ObjectForScripting = this;

            glueSetting = GlueSettingFactory.GetSetting("涉赌专项");
        }

        #region tab页代码
        public void InitTabItems() 
        {
            AddTabItem("涉赌专项", true); //第一个展示
            AddTabItem("涉枪专项");
            AddTabItem("涉黄专项");
            AddTabItem("盗洞专项");
            AddTabItem("后门黑吃黑专项");
            AddTabItem("境外网产专项");
            OnTaskBarChanged();
        }
       
        private void OnTaskBarChanged()
        {
            if (tabBar1 != null)
                tabBar1.SelectedItemChanged += new EventHandler(TaskBar_SelectedItemChanged);
        }
        void TaskBar_SelectedItemChanged(object sender, EventArgs e)
        {
            glueSetting = GlueSettingFactory.GetSetting(tabBar1.SelectedItem.Tag.ToString());
            RefreshHtmlTable();
        }

        protected virtual void AddTabItem(string name, bool visiable = false)
        {
            Global.GetWorkSpacePanel().SuspendLayout();
            if (tabBar1 != null)
            {
                var ti = new TabItem
                {
                    Text = name,
                    CanClose = false,
                    Tag = name,
                };
                tabBar1.Items.Add(ti);
                if (visiable)
                    tabBar1.SelectedItem = ti;
            }
            Global.GetWorkSpacePanel().ResumeLayout();
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

            if (glueSetting.UpdateContent(excelPath))
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
            MessageBox.Show(glueSetting.SearchInfo(a));
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        [System.Runtime.InteropServices.ComVisibleAttribute(true)]
        public void InitDataTable()
        {
            glueSetting.InitDataTable();
            RefreshHtmlTable();
        }

        private void RefreshHtmlTable()
        {
            //有几个操作都会动态刷新html，初始化、添加、排序
            this.webBrowser.Document.InvokeScript("clearTable");

            //先试试初始化
            this.webBrowser.Document.InvokeScript("WfToHtml", new object[] { glueSetting.RefreshHtmlTable() });
        }
        #endregion

    }
}
