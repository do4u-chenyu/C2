using C2.IAOLab.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QQSpiderPlugin
{
    public partial class Form1 : Form, IPlugin
    {
        delegate void CloseQrForm();
        string tmpPath = @"session.txt";
        Session session;
        private List<string> idDataSource;
        private List<string> grpDataSource;
        private List<string> groupHeaders = new List<string>() { "群号", "群名称", "群人数", "群上限", "群主", "地域", "分类", "标签", "群简介" };
        private List<string> actHeaders = new List<string>() { "账号", "昵称", "国家", "省市", "城市", "性别", "年龄", "头像地址" };


        public Form1()
        {
            InitializeComponent();
            session = new Session();
            idDataSource = new List<string>();
            grpDataSource = new List<string>();
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
            List<string> dataSource = this.idDataSource;
            ResetProgressBar(0, dataSource.Count);
            if (dataSource.Count == 0)
            {
                ShowMessageBox("请先导入信息");
                return;
            }

            if (session.IsEmpty() && File.Exists(this.tmpPath))
                session.Deserialize(this.tmpPath);

            if (session.IsEmpty() || !QQCrawler.IsValidQQSession(session))
            {
                Console.WriteLine("无已缓存的可用session，需要重新登录");
                Login();
                session.Serialize(this.tmpPath);
            }

            if (String.IsNullOrEmpty(session.Ldw))
            {
                ShowMessageBox("登录失败，请重新扫描登录");
                session = new Session();
                return;
            }

            QQCrawler crawler = new QQCrawler(session);


            StringBuilder tmpResult = new StringBuilder();
            tmpResult.AppendLine(String.Join("\t", actHeaders));
            this.Cursor = Cursors.WaitCursor;
            foreach (string id in dataSource)
                ShowResult(crawler.QueryAct(id), 0, tmpResult);
            this.IDResultRichTextBox.Text = tmpResult.ToString();
            this.Cursor = Cursors.Arrow;
        }


        private void GroupStartButton_Click(object sender, EventArgs e)
        {
            List<string> dataSource = this.grpDataSource;
            ResetProgressBar(1, dataSource.Count);
            if (dataSource.Count == 0)
            {
                ShowMessageBox("请先导入信息");
                return;
            }

            if (session.IsEmpty() && File.Exists(this.tmpPath))
                session.Deserialize(this.tmpPath);

            if (session.IsEmpty() || !QQCrawler.IsValidQQSession(session))
            {
                Console.WriteLine("无已缓存的可用session，需要重新登录");
                Login();
                session.Serialize(this.tmpPath);
            }

            if (String.IsNullOrEmpty(session.Ldw))
            {
                ShowMessageBox("登录失败，请重新扫描登录");
                session = new Session();
                return;
            }


            QQCrawler crawler = new QQCrawler(session);


            StringBuilder tmpResult = new StringBuilder();
            tmpResult.AppendLine(String.Join("\t", groupHeaders));
            this.Cursor = Cursors.WaitCursor;
            foreach (string id in dataSource)
                ShowResult(crawler.QueryGroup(id), 1, tmpResult);
            this.GroupRichTextBox.Text = tmpResult.ToString();
        }

        private void ResetProgressBar(int tabIndex, int count)
        {
            ProgressBar bar = tabIndex == 0 ? this.progressBar1 : this.progressBar2;
            bar.Maximum = count;
            bar.Minimum = 0;
            bar.Value = 0;
        }
        private void ShowResult(string result, int tabIndex, StringBuilder tmpResult)
        {
            if (!String.IsNullOrEmpty(result) && (tabIndex == 0 || tabIndex == 1) && tmpResult != null)
            {
                if (progressBar1.Value % 50 == 0)
                {
                    Thread.Sleep(500);

                }
                tmpResult.Append(result);
                switch (tabIndex)
                {
                    case 0:
                        this.IDResultRichTextBox.Text = tmpResult.ToString();
                        this.progressBar1.Value += 1;
                        break;
                    case 1:
                        this.GroupRichTextBox.Text = tmpResult.ToString();
                        this.progressBar2.Value += 1;
                        break;
                }
            }

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

        private void ResetCookieButton1_Click(object sender, EventArgs e)
        {
            ResetCookie();
        }

        private void ResetCookieButton2_Click(object sender, EventArgs e)
        {
            ResetCookie();
        }
        private void ResetCookie()
        {
            this.session = new Session();
            if (File.Exists(this.tmpPath))
                File.Delete(this.tmpPath);
        }
        public void Login()
        {
            QrLogin login = new QrLogin();
            byte[] imgBytes = login.GetQRCode().Content;
            if (imgBytes.Length == 0)
                return;
            Image img = Image.FromStream(new MemoryStream(imgBytes));
            QrCodeForm qrCodeForm = new QrCodeForm(img);
            Thread _thread = new Thread(() =>
            {
                Application.Run(qrCodeForm);
            });
            _thread.SetApartmentState(ApartmentState.STA);
            _thread.Start();

            int count = 0;
            int maxTimes = 15;
            while (count < maxTimes)
            {
                Dictionary<string, object> result = login.Login();
                object status = -1;
                if (result.TryGetValue("status", out status) && (int)status == 2)
                {
                    this.Invoke(new CloseQrForm(new CloseQrForm(delegate ()
                    {
                        qrCodeForm.Close();
                        _thread.Abort();
                    })));
                    break;
                }
                Console.WriteLine(result.ToString(), "请使用QQ手机客户端扫码登录！");
                System.Threading.Thread.Sleep(1000);
                count += 1;
            }

            if (count == maxTimes)
            {
                Console.WriteLine("扫码超时！");
                return;
            }
            this.session = login.Session;
            string skey = this.session.Cookies.GetCookieValue("skey");
            if (String.IsNullOrEmpty(skey))
                return;
            this.session.Ldw = Util.GenBkn(skey);
            return;
        }
    }
}
