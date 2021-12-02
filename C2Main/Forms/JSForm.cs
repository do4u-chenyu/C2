using C2.Controls;
using System.Collections.Generic;
using System.Windows.Forms;
using C2.Utils;

namespace C2.Forms
{
    public partial class JSForm : BaseForm
    {
        private string excelPath;
        private static readonly int maxRow = 100;
        public JSForm()
        {
            InitializeComponent();
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
            ReadRst rrst = FileUtil.ReadExcel(excelPath, maxRow);
            if (rrst.ReturnCode != 0 || rrst.Result.Count == 0)
            {
                HelpUtil.ShowMessageBox(rrst.Message);
                return;
            }
            List<List<string>> rowContentList = rrst.Result;
            List<string> headList = new List<string> { "网站网址", "网站名称", "发现时间", "用户数", "金额" };
            List<int> headIndex = new List<int> { };
            foreach (string content in headList)
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
            if (headIndex.Count != headList.Count)
            {
                HelpUtil.ShowMessageBox("上传的赌博数据非标准格式。");
                return;
            }

            for (int i = 1; i < rowContentList.Count; i++)
            {
                List<string> resultList = ContentFilter(headIndex, rowContentList[i]);
                FillLV(resultList);
            }
            HelpUtil.ShowMessageBox("赌博数据上传成功。");
        }
        private List<string> ContentFilter(List<int> indexList ,List<string> contentList)
        {

            List<string> resultList = new List<string> { };
            foreach (int index in indexList)
            {
                resultList.Add(contentList[index]);
            }
            return resultList;
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
    }
}
