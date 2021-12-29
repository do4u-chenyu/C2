using C2.Business.GlueWater;
using C2.Controls;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
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
        GlueDetailInfoDialog detailDialog;
        SqDetailInfoDialog sqDeatilDialog;
        SqDetailInfoDialogReply dbDeatilDialogReply;
        private List<string> doneGlueList;

        public JSForm()
        {
            InitializeComponent();
            InitTabItems();

            webBrowser.Navigate(webUrl);
            webBrowser.ObjectForScripting = this;

            detailDialog = new GlueDetailInfoDialog();
            sqDeatilDialog = new SqDetailInfoDialog();
            dbDeatilDialogReply = new SqDetailInfoDialogReply();
            glueSetting = GlueSettingFactory.GetSetting("涉赌专项");
            doneGlueList = new List<string>() { "涉赌专项", "涉枪专项", "涉黄专项" };

            this.label1.Visible = false;
        }

        #region tab页代码
        public void InitTabItems() 
        {
            AddTabItem("涉赌专项", true); //第一个展示
            AddTabItem("涉枪专项");
            AddTabItem("涉黄专项");
            AddTabItem("盗洞专项");
            AddTabItem("黑吃黑专项");
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
            string selectedItem = tabBar1.SelectedItem.Tag.ToString();
            glueSetting = GlueSettingFactory.GetSetting(selectedItem);
            if (doneGlueList.Contains(selectedItem))
            {
                RefreshHtmlTable();
                this.webBrowser.Visible = true;
                this.label1.Visible = false;
            }
            else
            {
                this.webBrowser.Visible = false;
                this.label1.Visible = true;
            }

            StyleChange(selectedItem);
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

        private void StyleChange(string selectedItem)
        {
            this.excelTextBox.Text = "未选择任何文件";
            selectedItem = selectedItem == "境外网产专项" ? "网产专项" : selectedItem;
            this.itemLabel.Text = selectedItem.Replace("专项", string.Empty);
            this.itemLabel.Location = selectedItem == "黑吃黑专项" ? new System.Drawing.Point(56, 14) : new System.Drawing.Point(72, 14);
            this.label3.Location = selectedItem == "黑吃黑专项" ? new System.Drawing.Point(24, 14) : new System.Drawing.Point(41, 14);
        }

        private void BrowserButton_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog OpenFileDialog = new OpenFileDialog
            {
                Filter = "数据包 | *.zip|其他| *.xls;*.xlsx"
            };
            if (OpenFileDialog.ShowDialog() != DialogResult.OK)
                return;
            excelPath = OpenFileDialog.FileName;

            using (GuarderUtil.WaitCursor)
            {
                string returnMsg = glueSetting.UpdateContent(excelPath);
                if (returnMsg == "数据添加成功")
                {
                    this.excelTextBox.Text = Path.GetFileNameWithoutExtension(excelPath) + "数据添加成功";
                    RefreshHtmlTable();
                }
                else
                {
                    this.excelTextBox.Text = Path.GetFileNameWithoutExtension(excelPath) + "数据添加失败";
                    HelpUtil.ShowMessageBox(returnMsg);
                }
            }
        }

        public void SelectTabByName(string name)
        {
            foreach(TabItem ti in tabBar1.Items)
            {
                if (ti.Tag.ToString() == name)
                {
                    tabBar1.SelectedItem = ti;
                    break;
                }
            }
        }

        #region 界面html版

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        [System.Runtime.InteropServices.ComVisibleAttribute(true)]
        public void ShowDetails(string item)
        {
            detailDialog.DetailTable = glueSetting.SearchInfo(item);
            detailDialog.RefreshDGV();
            detailDialog.ShowDialog();
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        [System.Runtime.InteropServices.ComVisibleAttribute(true)]
        public void ShowDetailsTopic(string item)
        {
            sqDeatilDialog.DetailTable = glueSetting.SearchInfo(item);
            sqDeatilDialog.RefreshDGV();
            sqDeatilDialog.ShowDialog();
        }
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        [System.Runtime.InteropServices.ComVisibleAttribute(true)]
        public void ShowDetailsReply(string item)
        {
            dbDeatilDialogReply.DetailTable = glueSetting.SearchInfoReply(item);
            dbDeatilDialogReply.RefreshDGV();
            dbDeatilDialogReply.ShowDialog();
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        [System.Runtime.InteropServices.ComVisibleAttribute(true)]
        public void InitDataTable()
        {
            glueSetting.InitDataTable();
            RefreshHtmlTable();
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        [System.Runtime.InteropServices.ComVisibleAttribute(true)]
        public void SortCol(string col, string sortType)
        {
            glueSetting.SortDataTableByCol(col, sortType);
            RefreshHtmlTable(false);
        }

        private void RefreshHtmlTable(bool freshTitle = true)
        {
            this.webBrowser.Document.InvokeScript("clearTable");
            if(freshTitle)
                this.webBrowser.Document.InvokeScript("clearTableTitle"); 

            this.webBrowser.Document.InvokeScript("WfToHtml", new object[] { glueSetting.RefreshHtmlTable(freshTitle) });
        }
        #endregion
    }
}
