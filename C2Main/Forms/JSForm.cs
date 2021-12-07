using C2.Controls;
using System.Collections.Generic;
using System.Windows.Forms;
using C2.Utils;
using C2.Core;
using System.IO;
using System.Xml;
using C2.Business.WebsiteFeatureDetection;
using C2.Business.Model;
using System;
using System.Linq;
using System.Data;

namespace C2.Forms
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public partial class JSForm : BaseForm
    {
        private string excelPath;
        private static readonly int maxRow = 100;
        private readonly string txtDirectory;
        private readonly string SDPath;

        private DataTable DbTable;
        private string webUrl = Path.Combine(Application.StartupPath, "Business\\IAOLab\\WebEngine\\Html", "JSTable.html");

        public JSForm()
        {
            InitializeComponent();
            txtDirectory = Path.Combine(Global.UserWorkspacePath, "胶水系统");
            SDPath = Path.Combine(txtDirectory, "涉D.txt");
            Directory.CreateDirectory(txtDirectory);
            LoadHistory(SDPath);
            DbTable = GenDataTable(SDPath);

            webBrowser1.Navigate(webUrl);
            webBrowser1.ObjectForScripting = this;
        }

        public override bool IsNeedShowBottomViewPanel()
        {
            return false;
        }

        private void BrowserButton_Click(object sender, System.EventArgs e)
        {
            this.excelPathTextBox.Clear();
            //this.dbListView.Items.Clear();
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

            List<string> colList1 = new List<string> { "网站网址", "网站名称", "网站网址", "网站Ip", "REFER标题", "REFER", "金额", "用户数", "赌博类型(多值##分隔)", "开始运营时间", "归属地", "发现时间" };
            List<string> colList2 = new List<string> { "网站网址", "认证账号", "最后登录IP", "登陆账号(多值##分隔)", "登陆密码(多值##分隔)" };

            List<int> headIndex1 = IndexFilter(colList1, rrst1.Result);
            List<int> headIndex2 = IndexFilter(colList2, rrst2.Result);
            if (headIndex1.Count == 0 || headIndex2.Count == 0)
                return;


            for (int i = 1; i < rrst1.Result.Count; i++)
            {
                List<string> resultList = ContentFilter(headIndex1, rrst1.Result[i]);
                for (int j = 1; j < rrst2.Result.Count; j++)
                {
                    List<string> resultList1 = ContentFilter(headIndex2, rrst2.Result[j]);
                    if (resultList[0] == resultList1[0])
                    {
                        resultList1.Remove(resultList1[0]);
                        resultList.InsertRange(6, resultList1);

                        WriteResult(SDPath, resultList.Skip(1).ToList());
                        break;
                    }
                }
            }
            HelpUtil.ShowMessageBox("赌博数据上传成功。");
        }

        private List<string> ContentFilter(List<int> indexList, List<string> contentList)
        {
            List<string> resultList = new List<string> { };

            foreach (int colindex in indexList)
                resultList.Add(contentList[colindex].TrimEnd(new char[] { '\r', '\n'}));

            return resultList;
        }

        private List<int> IndexFilter(List<string> colList, List<List<string>> rowContentList)
        {
            List<int> headIndex = new List<int>();
            foreach (string col in colList)
            {
                for (int i = 0; i < rowContentList[0].Count; i++)
                {
                    if (rowContentList[0][i] == col)
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

        public void LoadHistory(string path)
        {
            if (!File.Exists(path))
                return;
            FileStream fs_dir = null;
            StreamReader reader = null;
            try
            {
                fs_dir = new FileStream(path, FileMode.Open, FileAccess.Read);
                reader = new StreamReader(fs_dir);
                string lineStr;
                while ((lineStr = reader.ReadLine()) != null)
                {
                    if (!lineStr.Equals(""))
                    {
                        List<string> line = new List<string>(lineStr.Split("\t"));
                        FillLV(line);
                    }
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
        }

        private void FillLV(List<string> contentList)
        {
            ListViewItem items = new ListViewItem(contentList[0]);
            for (int i=1; i < contentList.Count; i++)
            {
                items.SubItems.Add(contentList[i]);
            }
            this.dbListView.Items.Add(items);
        }

        private void WriteResult(string txtPath, List<string> contentList)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(txtPath, true, System.Text.Encoding.UTF8))
                {
                    string content = string.Join("\t", contentList);
                    sw.WriteLine(content);
                }
            }
            catch (Exception ex)
            {
                HelpUtil.ShowMessageBox(ex.Message);
            }
        }

        #region 界面html版
        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab.Text == "涉Gun专项")
            {
                RefreshHtmlTable();
            }
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        [System.Runtime.InteropServices.ComVisibleAttribute(true)]
        public void Hello(string a)
        {
            DataRow[] rows = DbTable.Select("域名='" + a + "'");
            MessageBox.Show(rows[0][0].ToString() + rows[0][1].ToString() + rows[0][2].ToString());
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            RefreshHtmlTable();
        }

        private void RefreshHtmlTable()
        {
            //有几个操作都会动态刷新html，初始化、添加、排序
            this.webBrowser1.Document.InvokeScript("clearTable");

            //先试试初始化
            foreach (DataRow dr in DbTable.Rows)
            {
                this.webBrowser1.Document.InvokeScript("WfToHtml", new object[] { string.Format(
                "<tr name=\"row\">" +
                "   <td id=\"th0\">{0}<br><a onclick=\"Hello(this)\">{1}</a><br>{2}</td>" +
                "   <td>{3}<br>{4}</td>" +
                "   <td>{5}<br>{6}</td>" +
                "   <td>{7}<br>{8}</td>" +
                "   <td>{9}<br>{10}</td>" +
                "   <td>{11}<br>{12}</td>" +
                "   <td>{13}<br>{14}</td>" +
                "</tr>",
                dr["网站名称"].ToString(), dr["域名"].ToString(), dr["IP"].ToString(),
                dr["Refer对应Title"].ToString(), dr["Refer"].ToString(),
                dr["认证账号"].ToString(), dr["登陆IP"].ToString(),
                dr["登陆账号"].ToString(), dr["登陆密码"].ToString(),
                dr["涉案金额"].ToString(), dr["涉赌人数"].ToString(),
                dr["赌博类型"].ToString(), dr["运营时间"].ToString(),
                dr["发现地市"].ToString(), dr["发现时间"].ToString()) });
            }
        }

        private DataTable GenDataTable(string path)
        {
            DataTable dataTable = new DataTable(Path.GetFileNameWithoutExtension(path));
            if (!File.Exists(path))
                return dataTable;

            FileStream fs_dir = null;
            StreamReader reader = null;
            try
            {
                fs_dir = new FileStream(path, FileMode.Open, FileAccess.Read);
                reader = new StreamReader(fs_dir);

                string[] colList = new string[] { "网站名称", "域名", "IP", "Refer对应Title", "Refer", "认证账号", "登陆IP", "登陆账号", "登陆密码", "涉案金额", "涉赌人数", "赌博类型", "运营时间", "发现地市", "发现时间" };
                foreach(string col in colList)
                    dataTable.Columns.Add(col);

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
    }
}
