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
        private readonly string webUrl = Path.Combine(Global.WebEnginePath, "Html", "JSTable.html");
        private IGlueSetting glueSetting;
        readonly GlueDetailInfoDialog detailDialog;
        readonly SqDetailInfoDialog sqDeatilDialog;
        readonly SqDetailInfoDialogReply dbDeatilDialogReply;
        private readonly static Dictionary<GlueType, string> GlueListDesc = new Dictionary<GlueType, string>
        {
            [GlueType.Gamble] = "涉赌专项",
            [GlueType.Gun] = "涉枪专项",
            [GlueType.Yellow] = "涉黄专项",
            [GlueType.BBanshee] = "黑吃黑专项",
            [GlueType.Webshell] = "盗洞专项",
            [GlueType.VB] = "境外网产专项"
        };
        // 这个是临时, 等黑吃黑，盗洞，等实现完毕后就删除
        private readonly static Dictionary<GlueType, string> GlueListTmpDesc = new Dictionary<GlueType, string>
        {
            [GlueType.Gamble] = "涉赌专项",
            [GlueType.Gun] = "涉枪专项",
            [GlueType.Yellow] = "涉黄专项",
        };

        private readonly string txtDirectory = Global.JSViewPath;
        private readonly string bakDirectory = Path.Combine(Global.JSViewPath, "backup");
        private readonly string txtModelDirectory = Path.Combine(Global.TemplatesPath, "JS模板");

        public JSForm()
        {
            InitializeComponent();
            InitTabItems();

            webBrowser.Navigate(webUrl);
            webBrowser.ObjectForScripting = this;

            detailDialog = new GlueDetailInfoDialog();
            sqDeatilDialog = new SqDetailInfoDialog();
            dbDeatilDialogReply = new SqDetailInfoDialogReply();
            glueSetting = GlueSettingFactory.GetSetting(GlueType.Gamble);
            this.label1.Visible = false;
            // 默认展示 加载涉赌数据
            string backSdFile = Path.Combine(bakDirectory, "涉赌模板0112.zip");
            if (
                !File.Exists(backSdFile) ||
                (!File.Exists(Path.Combine(txtDirectory, "DB_web.txt")) &&
                !File.Exists(Path.Combine(txtDirectory, "DB_member.txt"))
                ))
            {
                string trueDbFile = Path.Combine(txtModelDirectory, "涉赌模板0112.zip");
                File.Copy(trueDbFile, backSdFile, true);
                InitLoadModelData(backSdFile, true);
            }
        }

        private void InitLoadModelData(string excelPath, bool isWrite)
        {
            using (GuarderUtil.WaitCursor)
            {
                string returnMsg = glueSetting.UpdateContent(excelPath, isWrite);
                if (returnMsg == "数据添加成功")
                    RefreshHtmlTable();
            }
        }

        #region tab页代码
        public void InitTabItems() 
        {
            AddTabItem(GlueType.Gamble, true); //第一个展示
            AddTabItem(GlueType.Gun);
            AddTabItem(GlueType.Yellow);
            AddTabItem(GlueType.Webshell);
            AddTabItem(GlueType.BBanshee);
            AddTabItem(GlueType.VB);
            OnTaskBarChanged();
        }
       
        private void OnTaskBarChanged()
        {
            if (tabBar1 != null)
                tabBar1.SelectedItemChanged += new EventHandler(TaskBar_SelectedItemChanged);
        }
        private void TaskBar_SelectedItemChanged(object sender, EventArgs e)
        {

            this.webBrowser.Visible = false;
            this.label1.Visible = true;

            GlueType type = (GlueType)tabBar1.SelectedItem.Tag;
            glueSetting = GlueSettingFactory.GetSetting(type);
            if (GlueListTmpDesc.ContainsKey(type))
            {
                RefreshHtmlTable();
                this.webBrowser.Visible = true;
                this.label1.Visible = false;
            }
            StyleChange(type);

            //默认加载 涉枪数模板据
            if (type == GlueType.Gun)
            {
                string backSqFile = Path.Combine(bakDirectory, "涉枪模板0112.zip");
                if (
                    !File.Exists(backSqFile) ||
                    (!File.Exists(Path.Combine(txtDirectory, "SQ_web.txt")) &&
                    !File.Exists(Path.Combine(txtDirectory, "SQ_member.txt")) &&
                    !File.Exists(Path.Combine(txtDirectory, "SQ_member2.txt")))
                    )
                {
                    string trueSqFile = Path.Combine(txtModelDirectory, "涉枪模板0112.zip");
                    File.Copy(trueSqFile, backSqFile, true);
                    InitLoadModelData(backSqFile,false);
                }
            }
            
            //默认加载 涉黄数模板据
            else if (type == GlueType.Yellow)
            {
                string backShFile = Path.Combine(bakDirectory, "涉黄模板0112.zip");
                if (
                    !File.Exists(backShFile) ||
                    (!File.Exists(Path.Combine(txtDirectory, "Yellow_member.txt")) &&
                    !File.Exists(Path.Combine(txtDirectory, "Yellow_web.txt")))
                    )
                {
                    string trueShFile = Path.Combine(txtModelDirectory, "涉黄模板0112.zip");
                    File.Copy(trueShFile, backShFile, true);
                    InitLoadModelData(backShFile,false);
                }
            }
        }

        protected virtual void AddTabItem(GlueType type, bool visiable = false)
        {
            Global.GetWorkSpacePanel().SuspendLayout();
            if (tabBar1 != null)
            {
                var ti = new TabItem
                {
                    Text = GlueListDesc[type],
                    CanClose = false,
                    Tag = type,
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

        private void ExcelTextBoxSetting(int start, int end, Color color) 
        {
            this.excelTextBox.SelectionStart = start;
            this.excelTextBox.SelectionLength = end;
            this.excelTextBox.SelectionColor = color;
        }
    

        private void StyleChange(GlueType type)
        {
            this.excelTextBox.Text = "未选择任何文件";
            ExcelTextBoxSetting(0, this.excelTextBox.Text.Length, SystemColors.WindowText);
            this.itemLabel.Text = GlueListDesc[type].Replace("专项", string.Empty);
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
                string returnMsg = glueSetting.UpdateContent(excelPath,true);
                if (returnMsg == "数据添加成功")
                {
                    this.excelTextBox.Text = "[" + Path.GetFileNameWithoutExtension(excelPath) + "]" + OpUtil.Blank + "成功";
                    ExcelTextBoxSetting(0, this.excelTextBox.Text.Length - "成功".Length, Color.DarkBlue);
                    RefreshHtmlTable();
                }
                else
                {
                    this.excelTextBox.Text = "[" + Path.GetFileNameWithoutExtension(excelPath) + "]" + OpUtil.Blank + "失败";
                    ExcelTextBoxSetting(0, this.excelTextBox.Text.Length - "失败".Length, Color.Red);
                    HelpUtil.ShowMessageBox(returnMsg);
                }
            }
        }

        public void SelectTabByName(string name)
        {
            foreach (TabItem ti in tabBar1.Items)
            {
                if (ti.Text == name)
                {
                    tabBar1.SelectedItem = ti;
                    break;
                }
            }
        }

        #region 界面html版

        
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        [System.Runtime.InteropServices.ComVisibleAttribute(true)]
        public void RefreshData(string item)
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
            GlueType glueType = (GlueType)tabBar1.SelectedItem.Tag;
            if (GlueListDesc.ContainsKey(glueType))
            {
                SaveFileDialog dialog = new SaveFileDialog
                {
                    Title = "数据包样例",
                    Filter = "数据包|*.zip",
                };

                string type = string.Empty;
                type = glueType == GlueType.Gamble ? "db-" : type;
                type = glueType == GlueType.Gun ? "gun-" : type;
                type = glueType == GlueType.Yellow ? "yellow-" : type;
                dialog.FileName = "XX省XX市-" + type + DateTime.Now.ToString("yyyyMMdd") + "-XX.zip";

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                using (GuarderUtil.WaitCursor)
                {
                    string prefix = string.Empty;
                    switch (glueType)
                    {
                        case GlueType.Gamble:
                            prefix = "涉赌";
                            break;
                        case GlueType.Gun:
                            prefix = "涉枪";
                            break;
                        case GlueType.Yellow:
                            prefix = "涉黄";
                            break;
                        default:
                            break;
                    }
                    string localExcelPath = Path.Combine(Global.TemplatesPath, "JS模板", prefix + "模板0112.zip");
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
