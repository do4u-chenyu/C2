using C2.Business.GlueWater;
using C2.Controls;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
        public string bakDirectory = Path.Combine(Global.UserWorkspacePath, "胶水系统", "backup");
        string txtModelDirectory = Path.Combine(Application.StartupPath, "Resources/Templates/JS模板");

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
            // 默认加载涉赌数据
            string backSdFile = Path.Combine(bakDirectory, "涉赌模板0112.zip");
            if (!File.Exists(backSdFile))
            {
                string trueDbFile = Path.Combine(txtModelDirectory, "涉赌模板0112.zip");
                File.Copy(trueDbFile, backSdFile, true);
                initLoadModelData(backSdFile);
            }
        }

        private void initLoadModelData(string excelPath)
        {
            using (GuarderUtil.WaitCursor)
            {
                string returnMsg = glueSetting.UpdateContent(excelPath);
                if (returnMsg == "数据添加成功")
                    RefreshHtmlTable();
            }
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
         
            if (selectedItem == "涉枪专项")
            {
                string backSqFile = Path.Combine(bakDirectory, "涉枪模板0112.zip");
                if (!File.Exists(backSqFile))
                {
                    string trueSqFile = Path.Combine(txtModelDirectory, "涉枪模板0112.zip");
                    File.Copy(trueSqFile, backSqFile, true);
                    initLoadModelData(backSqFile);
                }
            }
            else if (selectedItem == "涉黄专项")
            {
                string backShFile = Path.Combine(bakDirectory, "涉黄模板0112.zip");
                if (!File.Exists(backShFile))
                {
                    string trueShFile = Path.Combine(txtModelDirectory, "涉黄模板0112.zip");
                    File.Copy(trueShFile, backShFile, true);
                    initLoadModelData(backShFile);
                }
            }
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

        public void excelTextBoxSetting(int start, int end, Color color) 
        {
            this.excelTextBox.SelectionStart = start;
            this.excelTextBox.SelectionLength = end;
            this.excelTextBox.SelectionColor = color;
        }
    

        private void StyleChange(string selectedItem)
        {
            this.excelTextBox.Text = "未选择任何文件";
            excelTextBoxSetting(0, this.excelTextBox.Text.Length, SystemColors.WindowText);

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
                    this.excelTextBox.Text = string.Empty;
                    this.excelTextBox.Text = "[" + Path.GetFileNameWithoutExtension(excelPath) + "]" + "数据添加成功";
                    excelTextBoxSetting(0, this.excelTextBox.Text.Length - "数据添加成功".Length, Color.DarkBlue);
                    RefreshHtmlTable();
                }
                else
                {
                    this.excelTextBox.Text = string.Empty;
                    this.excelTextBox.Text = "[" + Path.GetFileNameWithoutExtension(excelPath) + "]" + "数据添加失败";
                    excelTextBoxSetting(0, this.excelTextBox.Text.Length - "数据添加失败".Length, Color.DarkBlue);
                    HelpUtil.ShowMessageBox(returnMsg);
                }
            }
        }

        public void SelectTabByName(string name)
        {
            foreach (TabItem ti in tabBar1.Items)
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
        public void refreshData(string item)
        {
            this.webBrowser.Document.InvokeScript("clearTable");
            this.webBrowser.Document.InvokeScript("clearTableTitle");
            DataTable resTable = glueSetting.DeleteInfo(item);

            this.webBrowser.Document.InvokeScript("WfToHtml", new object[] { glueSetting.RefreshHtmlTable(resTable,false,true,true,false) });
        }
        
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
            DataTable dt = new DataTable();//只是为了传参，无实际意义
            this.webBrowser.Document.InvokeScript("clearTable");
            if (freshTitle)
            {
                this.webBrowser.Document.InvokeScript("clearTableTitle");
                this.webBrowser.Document.InvokeScript("WfToHtml", new object[] { glueSetting.RefreshHtmlTable(dt, true, true, true,false) });
            }
            else 
            {
                this.webBrowser.Document.InvokeScript("WfToHtml", new object[] { glueSetting.RefreshHtmlTable(dt, true, false, false,false) });
            }      
        }
        #endregion

        private void SampleButton_Click(object sender, EventArgs e)
        {
            string selectedItem = tabBar1.SelectedItem.Tag.ToString();
            if (doneGlueList.Contains(selectedItem))
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "数据包|*.zip";

                string type = string.Empty;
                type = selectedItem == "涉赌专项" ? "db-" : type;
                type = selectedItem == "涉枪专项" ? "gun-" : type;
                type = selectedItem == "涉黄专项" ? "yellow-" : type;
                dialog.FileName = "XX省XX市-" + type + DateTime.Now.ToString("yyyyMMdd") + "-XX.zip";

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                using (GuarderUtil.WaitCursor)
                {
                    string localExcelPath = Path.Combine(Application.StartupPath, "Resources/Templates/JS模板", selectedItem.Replace("专项", "") + "模板0112.zip");
                    FileUtil.FileCopy(localExcelPath, dialog.FileName);
                }
                HelpUtil.ShowMessageBox("模板保存完毕。");
            }
            else
            {
                HelpUtil.ShowMessageBox("该专项尚未完成，敬请期待!");
                return;
            }
        }

        private void deleteAllData_Click(object sender, EventArgs e)
        {
            this.webBrowser.Document.InvokeScript("clearTable");
            this.webBrowser.Document.InvokeScript("clearTableTitle");
            DataTable resTable = new DataTable();//读取空表，等价于清空操作
            this.webBrowser.Document.InvokeScript("WfToHtml", new object[] { glueSetting.RefreshHtmlTable(resTable, false, true, true,true) });
        }
    }
}
