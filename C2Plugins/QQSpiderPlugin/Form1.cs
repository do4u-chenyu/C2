﻿using C2.IAOLab.Plugins;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace QQSpiderPlugin
{
    public partial class Form1 : Form, IPlugin
    {
        private static readonly string wxid_prefix = @"weixin://contacts/profile/";
        delegate void CloseQrForm();
        string tmpPath = @"session.txt";
        Session session;
        private List<string> idDataSource;
        private List<string> grpDataSource;
        //{ "头像", "群号", "群名称", "群人数", "群上限", "群主", "地域", "分类", "标签", "群简介"};
        //{ "头像", "账号", "昵称", "国家", "省市", "城市", "性别", "年龄" };
        public string TmpPath
        {
            get { return Path.Combine(Util.TryGetSysTempDir(), tmpPath); }
        }

        public Form1()
        {
            InitializeComponent();
            InitializeForm();
        }
        private void InitializeForm()
        {
            session = new Session();
            this.richTextBox2.Text = String.Empty;
            this.richTextBox1.Text = String.Empty;
            DgvUtil.CleanDgv(this.dataGridView1);
            DgvUtil.CleanDgv(this.dataGridView2);
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
            RichTextBox lv = tabIndex == 0 ? this.richTextBox1 : this.richTextBox2;
            List<string> dataSource = tabIndex == 0 ? this.idDataSource : this.grpDataSource;
            lv.SuspendLayout();
            lv.Text = String.Empty;
            lv.Text = String.Join(Environment.NewLine, dataSource);

            lv.ResumeLayout();
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
            try
            {
                string line;
                using (StreamReader file = new System.IO.StreamReader(fileName))
                {
                    while ((line = file.ReadLine()) != null)
                        lines.Add(line);
                }
            }
            catch (IOException)
            {
                ShowMessageBox("该文件已被占用，请关闭文件后再次导入文件");
            }
            catch (Exception)
            {

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
            DgvManager dgvMgr = new DgvManager(this.dataGridView1);
            List<string> dataSource = this.idDataSource;
            ResetProgressBar(0, dataSource.Count);
            if (dataSource.Count == 0)
            {
                ShowMessageBox("请先导入信息");
                return;
            }

            //if (session.IsEmpty() && File.Exists(this.TmpPath))
            //{
            //    Console.WriteLine(String.Format("尝试从{0}中读取缓存Cookie", this.TmpPath));
            //    session.Deserialize(this.TmpPath);
            //}


            if (session.IsEmpty() || !QQCrawler.IsValidQQSession(session))
            {
                Console.WriteLine("无已缓存的可用session，需要重新登录");
                Login();
                //session.Serialize(this.TmpPath);
            }

            if (String.IsNullOrEmpty(session.Ldw))
            {
                ShowMessageBox("登录失败，请重新扫描登录");
                session = new Session();
                return;
            }

            QQCrawler crawler = new QQCrawler(session);

            this.Cursor = Cursors.WaitCursor;
            foreach (string id in dataSource)
            {
                dgvMgr.AppendLine(crawler.QueryAct(id));
                this.progressBar1.Value += 1;
            }
            this.Cursor = Cursors.Arrow;
        }


        private void GroupStartButton_Click(object sender, EventArgs e)
        {
            DgvManager dgvMgr = new DgvManager(this.dataGridView2);
            List<string> dataSource = this.grpDataSource;
            ResetProgressBar(1, dataSource.Count);
            if (dataSource.Count == 0)
            {
                ShowMessageBox("请先导入信息");
                return;
            }

            //if (session.IsEmpty() && File.Exists(this.TmpPath))
            //    session.Deserialize(this.TmpPath);

            if (session.IsEmpty() || !QQCrawler.IsValidQQSession(session))
            {
                Console.WriteLine("无已缓存的可用session，需要重新登录");
                Login();
                //session.Serialize(this.TmpPath);
            }

            if (String.IsNullOrEmpty(session.Ldw))
            {
                ShowMessageBox("登录失败，请重新扫描登录");
                session = new Session();
                return;
            }


            QQCrawler crawler = new QQCrawler(session);

            this.Cursor = Cursors.WaitCursor;
            foreach (string id in dataSource)
            {
                dgvMgr.AppendLine(crawler.QueryGroup(id));
                this.progressBar2.Value += 1;
            }
            this.Cursor = Cursors.Arrow;
        }

        private void ResetProgressBar(int tabIndex, int count)
        {
            ProgressBar bar = tabIndex == 0 ? this.progressBar1 : this.progressBar2;
            bar.Maximum = count;
            bar.Minimum = 0;
            bar.Value = 0;
        }
        //private void ShowResult(string result, int tabIndex, StringBuilder tmpResult)
        //{
        //    if ((tabIndex == 0 || tabIndex == 1) && tmpResult != null)
        //    {
        //        tmpResult.Append(result);
        //        switch (tabIndex)
        //        {
        //            case 0:
        //                UpdateStatus(this.IDResultRichTextBox, result);
        //                this.progressBar1.Value += 1;
        //                break;
        //            case 1:
        //                UpdateStatus(this.GroupRichTextBox, result);
        //                this.progressBar2.Value += 1;
        //                break;
        //        }
        //    }
        //}
        void UpdateStatus(RichTextBox richTextBox, string textMessage)
        {
            if (richTextBox.InvokeRequired)
            {
                richTextBox.Invoke(new MethodInvoker(() => UpdateStatus(richTextBox, textMessage)));
                return;
            }
            richTextBox.AppendText(textMessage);
            richTextBox.Refresh();
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

        private void OutputButton1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel Documents (*.xls)|*.xls",
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Util.SaveToExcel(sfd.FileName, this.dataGridView1);
                ShowMessageBox("导出成功");
            }
        }

        private void OutputButton2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel Documents (*.xls)|*.xls",
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Util.SaveToExcel(sfd.FileName, this.dataGridView2);
                ShowMessageBox("导出成功");
            }
        }
        public void Login()
        {
            QrLogin login = new QrLogin();
            byte[] imgBytes = login.GetQRCode().Content;
            if (imgBytes == null || imgBytes != null && imgBytes.Length == 0)
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
            int maxTimes = 30;
            object status = -1;
            while (count < maxTimes)
            {
                Dictionary<string, object> result = login.Login();
                if (qrCodeForm.IsDisposed)
                {
                    _thread.Abort();
                    break;
                }
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
                this.Invoke(new CloseQrForm(new CloseQrForm(delegate ()
                {
                    qrCodeForm.Close();
                    _thread.Abort();
                })));
                return;
            }
            this.session = login.Session;
            string skey = this.session.Cookies.GetCookieValue("skey");
            if (String.IsNullOrEmpty(skey))
                return;
            this.session.Ldw = Util.GenBkn(skey);
            return;
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            List<string> input = new List<string>();
            try
            {
                input = new List<string>(this.richTextBox2.Text
                    .Trim(Environment.NewLine.ToCharArray())
                    .Split(Environment.NewLine.ToCharArray())
                    );
            }
            catch
            {
                ShowMessageBox("输出有误"); 
            }
            this.grpDataSource = input;
        }

        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {
            List<string> input = new List<string>();
            try
            {
                input = new List<string>(this.richTextBox1.Text
                    .Trim(Environment.NewLine.ToCharArray())
                    .Split(Environment.NewLine.ToCharArray())
                    );
            }
            catch
            {
                ShowMessageBox("输出有误");
            }
            this.idDataSource = input;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string wxid = wxid_prefix + textBox1.Text.Trim();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
