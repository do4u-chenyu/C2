using C2.Business.GlueWater;
using C2.Controls;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace C2.Forms
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public partial class JSForm : BaseForm
    {
        BaseForm _SelectedForm;
        private string excelPath;
        private static readonly int maxRow = 100;
        
        private string DbWebPath;
        private string DbMemberPath;
        private string[] DbWebColList;
        private string[] DbMemberColList;
        private DataTable DbWebTable;
        private DataTable DbMemberTable;
        private string webUrl = Path.Combine(Application.StartupPath, "Business\\IAOLab\\WebEngine\\Html", "JSTable.html");

        public JSForm()
        {
            InitializeComponent();
            InitLocalPathAndSetting();

            webBrowser1.Navigate(webUrl);
            webBrowser1.ObjectForScripting = this;

            init();
        }

        #region tab页代码
        public void init() 
        {
            ShowForm(panel3, "DB专项", true, false, true);
            ShowForm(panel3, "SQ专项", true, false, false);
            ShowForm(panel3, "SH专项", true, false, false);
            ShowForm(panel3, "DD专项", true, false, false);
            ShowForm(panel3, "后门专项", true, false, false);
            ShowForm(panel3, "购置境资产专项", true, false, false);
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

        private void BrowserButton_Click(object sender, System.EventArgs e)
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
        }

        private void UpdateButton_Click(object sender, System.EventArgs e)
        {
            ReadRst rrst1 = FileUtil.ReadExcel(excelPath, maxRow, "涉赌网站");
            if (rrst1.ReturnCode != 0 || rrst1.Result.Count == 0)
            {
                HelpUtil.ShowMessageBox(rrst1.Message);
                return;
            }
            ReadRst rrst2 = FileUtil.ReadExcel(excelPath, maxRow, "涉赌网站人员");
            if (rrst2.ReturnCode != 0 || rrst2.Result.Count == 0)
            {
                HelpUtil.ShowMessageBox(rrst2.Message);
                return;
            }

            List<string> DbWebExcelColList = new List<string> { "网站名称", "网站网址", "网站Ip", "REFER标题", "REFER", "金额", "用户数", "赌博类型(多值##分隔)", "开始运营时间", "归属地", "发现时间" };
            List<string> DbMemberExcelColList = new List<string> { "网站网址", "认证账号", "最后登录IP", "登陆账号(多值##分隔)", "登陆密码(多值##分隔)" };
            List<int> headIndex = IndexFilter(DbWebExcelColList, rrst1.Result);
            List<int> headIndex2 = IndexFilter(DbMemberExcelColList, rrst2.Result);

            for (int i = 1; i < rrst1.Result.Count; i++)
            {
                if (headIndex.Max() > rrst1.Result[i].Count)
                    return;
                List<string> resultList = ContentFilter(headIndex, rrst1.Result[i]);

                //这里要做判断了 对于web，url存在，替换掉
                DataRow[] rows = DbWebTable.Select("域名='" + resultList[1] + "'");
                if(rows.Length > 0)
                    DbWebTable.Rows.Remove(rows[0]);

                DbWebTable.Rows.Add(resultList.ToArray());
            }

            for (int i = 1; i < rrst2.Result.Count; i++)
            {
                if (headIndex2.Max() > rrst2.Result[i].Count)
                    return;
                List<string> resultList = ContentFilter(headIndex2, rrst2.Result[i]);

                //这里要做判断了 对于member，url存在，比较是否完全一致
                DataRow[] rows = DbMemberTable.Select("域名='" + resultList[1] + "'");
                if(rows.Length == 0)
                    DbMemberTable.Rows.Add(resultList.ToArray());
                else
                {
                    foreach (DataRow row in rows)
                    {
                        List<string> rowContent = new List<string>();
                        for (int j = 0; j < DbMemberTable.Columns.Count; j++)
                            rowContent.Add(row[j].ToString());

                        if (rowContent.Count != 0 && string.Join("\t", rowContent) != string.Join("\t", resultList))
                            DbMemberTable.Rows.Add(resultList.ToArray());
                    }
                }

            }

            HelpUtil.ShowMessageBox("赌博数据上传成功。");
            RefreshHtmlTable();

            ReWriteResult(DbWebPath, DbWebTable);
            ReWriteResult(DbMemberPath, DbMemberTable);
        }

        private List<int> IndexFilter(List<string> colList, List<List<string>> rowContentList)
        {
            List<int> headIndex = new List<int> { };
            foreach (string content in colList)
            {
                for (int i = 0; i < rowContentList[0].Count; i++)
                {
                    if (rowContentList[0][i] == content)
                    {
                        headIndex.Add(i);
                        break;
                    }
                }
            }
            if (headIndex.Count != colList.Count)
            {
                HelpUtil.ShowMessageBox("上传的赌博数据非标准格式。");
                return new List<int>();
            }
            return headIndex;
        }

        private List<string> ContentFilter(List<int> indexList, List<string> contentList)
        {
            List<string> resultList = new List<string> { };
            {
                foreach (int index in indexList)
                    resultList.Add(contentList[index]);
            }

            return resultList;
        }

        private void ReWriteResult(string txtPath, DataTable dataTable)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(txtPath, false, System.Text.Encoding.UTF8))
                {
                    foreach(DataRow row in dataTable.Rows)
                    {
                        List<string> rowContent = new List<string>();
                        for (int i = 0; i < dataTable.Columns.Count; i++)
                            rowContent.Add(row[i].ToString());

                        sw.WriteLine(string.Join("\t", rowContent));
                    }
                }
            }
            catch (Exception ex)
            {
                HelpUtil.ShowMessageBox(ex.Message);
            }
        }
        

        #region 界面html版
        private void InitLocalPathAndSetting()
        {
            string txtDirectory = Path.Combine(Global.UserWorkspacePath, "胶水系统");
            Directory.CreateDirectory(txtDirectory);
            DbWebPath = Path.Combine(txtDirectory, "DB_web.txt");
            DbMemberPath = Path.Combine(txtDirectory, "DB_member.txt");

            DbWebColList = new string[] { "网站名称", "域名", "IP", "Refer对应Title", "Refer", "涉案金额", "涉赌人数", "赌博类型", "运营时间", "发现地市", "发现时间" };
            DbMemberColList = new string[] { "域名", "认证账号", "登陆IP", "登陆账号", "登陆密码", "安全码", "登陆地址" };

        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        [System.Runtime.InteropServices.ComVisibleAttribute(true)]
        public void Hello(string a)
        {
            DataRow[] rows = DbMemberTable.Select("域名='" + a + "'");
            if(rows.Length > 0)
                MessageBox.Show(rows[0][0].ToString() + rows[0][1].ToString() + rows[0][2].ToString());
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        [System.Runtime.InteropServices.ComVisibleAttribute(true)]
        public void InitDataTable()
        {
            //空文件的话，这些table都只有colume表头信息
            DbWebTable = GenDataTable(DbWebPath, DbWebColList);
            DbMemberTable = GenDataTable(DbMemberPath, DbMemberColList);

            RefreshHtmlTable();
        }

        private void RefreshHtmlTable()
        {
            //有几个操作都会动态刷新html，初始化、添加、排序
            this.webBrowser1.Document.InvokeScript("clearTable");

            //先试试初始化
            foreach (DataRow dr in DbWebTable.Rows)
            {
                this.webBrowser1.Document.InvokeScript("WfToHtml", new object[] { string.Format(
                "<tr name=\"row\">" +
                "   <td id=\"th0\">{0}<br><a onclick=\"Hello(this)\">{1}</a><br>{2}</td>" +
                "   <td>{3}<br>{4}</td>" +
                "   <td>{5}</td>" +
                "   <td>{6}</td>" +
                "   <td>{7}<br>{8}</td>" +
                "   <td>{9}<br>{10}</td>" +
                "</tr>",
                dr["网站名称"].ToString(), dr["域名"].ToString(), dr["IP"].ToString(),
                dr["Refer对应Title"].ToString(), dr["Refer"].ToString(),
                dr["涉案金额"].ToString(),
                dr["涉赌人数"].ToString(),
                dr["赌博类型"].ToString(), dr["运营时间"].ToString(),
                dr["发现地市"].ToString(), dr["发现时间"].ToString()) });
            }
        }

        private DataTable GenDataTable(string path, string[] colList)
        {
            DataTable dataTable = new DataTable(Path.GetFileNameWithoutExtension(path));

            foreach (string col in colList)
                dataTable.Columns.Add(col);

            if (!File.Exists(path))
                return dataTable;

            FileStream fs_dir = null;
            StreamReader reader = null;
            try
            {
                fs_dir = new FileStream(path, FileMode.Open, FileAccess.Read);
                reader = new StreamReader(fs_dir);

                string lineStr;
                while ((lineStr = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(lineStr))
                        continue;

                    string[] rowList = lineStr.TrimEnd(new char[] { '\r', '\n' }).Split('\t');
                    List<string> tmpRowList = new List<string>();
                    for (int j = 0; j < colList.Length; j++)
                    {
                        string cellValue = j < rowList.Length ? rowList[j] : "";
                        tmpRowList.Add(cellValue);
                    }
                    dataTable.Rows.Add(tmpRowList.ToArray());
                }
            }
            catch { }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (fs_dir != null)
                    fs_dir.Close();
            }

            return dataTable;
        }
        #endregion

        private void SampleButton_Click(object sender, EventArgs e)
        {
            RefreshHtmlTable();
        }
    }
}
