using C2.IAOLab.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QQSpiderPlugin
{
    public partial class Form1 : Form, IPlugin
    {
        QrLogin qrLogin;
        private List<string> idDataSource;
        private List<string> grpDataSource;
        private List<string> idQueryResult;
        private List<string> grpQueryResult;
        public Form1()
        {
            InitializeComponent();
            idDataSource = new List<string>();
            grpDataSource = new List<string>();
            idQueryResult = new List<string>();
            grpQueryResult = new List<string>();
            qrLogin = new QrLogin();
        }

        public string GetPluginDescription()
        {
            return "QQ爬虫:可以爬取QQ头像,昵称,备注,QQ群描述等信息,支持导出成xls文件,支持批量查询。";
        }

        public Image GetPluginImage()
        {
            return this.Icon.ToBitmap();
        }

        public string GetPluginName()
        {
            return "QQ爬虫";
        }

        public string GetPluginVersion()
        {
            return "0.0.2";
        }

        public DialogResult ShowFormDialog()
        {
            return this.ShowDialog();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateListView(this.tabControl1.SelectedIndex);
        }

        private void InputIDDataButton_Click(object sender, EventArgs e)
        {
            ImportData(0);
            UpdateListView(0);
        }
        private void InputGroupButton_Click(object sender, EventArgs e)
        {
            ImportData(1);
            UpdateListView(1);
        }

        private void UpdateListView(int tabIndex)
        {
            ListView lv = tabIndex == 0 ? this.idListView : this.groupListView;
            List<string> dataSource = tabIndex == 0 ? this.idDataSource : this.grpDataSource;
            lv.BeginUpdate();

            lv.Columns.Clear();
            lv.Items.Clear();
            lv.Columns.Add("ID", -1);

            for (int i = 0; i < dataSource.Count; i++)
                lv.Items.Add(dataSource[i]);

            lv.EndUpdate();
        }
        private void UpdateRichTextView(int tabIndex)
        {
            StringBuilder sb = new StringBuilder();
            if (tabIndex == 0)
            {
                foreach (string line in this.idQueryResult)
                    sb.AppendLine(line);
            }
            else
            {
                foreach (string line in this.grpQueryResult)
                    sb.AppendLine(line);
            }

            this.resultLRichTextBox1.Text = sb.ToString();
        }

        private void ImportData(int tabIndex)
        {
            //获取导入模型路径
            OpenFileDialog fd = new OpenFileDialog
            {
                Title = "导入待爬取数据",
                AddExtension = true
            };
            if (fd.ShowDialog() == DialogResult.OK)
                if (tabIndex == 0)
                    this.idDataSource = LoadFile(fd.FileName);
                else
                    this.grpDataSource = LoadFile(fd.FileName);
        }

        private List<string> LoadFile(string fileName)
        {
            List<string> lines = new List<string>();
            string line;
            using (StreamReader file = new System.IO.StreamReader(fileName))
            {
                while ((line = file.ReadLine()) != null)
                    lines.Add(line);
            }
            return lines;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ActStartButton_Click(object sender, EventArgs e)
        {
            Start(0);
        }


        private void GroupStartButton_Click(object sender, EventArgs e)
        {
            Start(1);
        }
        private void Start(int tabIndex)
        {
            QrLogin login = new QrLogin();
            string ldw = login.GetLDW();

            List<string> dataSource = tabIndex == 0 ? this.idDataSource : this.grpDataSource;

            if (String.IsNullOrEmpty(ldw))
            {
                ShowMessageBox("登录失败，请重新扫描登录");
                return;
            }
            QQCrawler crawler = new QQCrawler(login.Session, ldw);

            if (dataSource.Count == 0)
            {
                ShowMessageBox("请先导入信息");
                return;
            }
            if (tabIndex == 0)
                idQueryResult = crawler.QueryAct(dataSource);
            else if (tabIndex == 1)
                idQueryResult = crawler.QueryGroup(dataSource);
            UpdateRichTextView(tabIndex);
        }
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public static DialogResult ShowMessageBox(string message, string caption = "提示信息", MessageBoxIcon type = MessageBoxIcon.Information)
        {
            return MessageBox.Show(message, caption, MessageBoxButtons.OK, type);
        }

    }
}
