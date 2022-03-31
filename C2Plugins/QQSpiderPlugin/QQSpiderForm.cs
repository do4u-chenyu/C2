using C2.IAOLab.Plugins;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace QQSpiderPlugin
{
    public partial class QQSpiderForm : Form, IPlugin
    {
        private static readonly string wxid_prefix = @"weixin://contacts/profile/";
        delegate void CloseQrForm();
        string tmpPath = @"session.txt";
        Session session;
        private List<string> idDataSource;
        private List<string> grpDataSource;
        private List<string> keyWordDataSource;
        private Dictionary<string, List<string>> resultDictionary;
        //{ "头像", "群号", "群名称", "群人数", "群上限", "群主", "地域", "分类", "标签", "群简介"};
        //{ "头像", "账号", "昵称", "国家", "省市", "城市", "性别", "年龄" };
        public string TmpPath
        {
            get { return Path.Combine(Util.TryGetSysTempDir(), tmpPath); }
        }

        public QQSpiderForm()
        {
            InitializeComponent();
            InitializeForm();
        }
        private void InitializeForm()
        {
            session = new Session();
            this.richTextBox2.Text = String.Empty;
            this.richTextBox1.Text = String.Empty;
            this.keyWordRichTextBox.Text = String.Empty;
            DgvUtil.CleanDgv(this.dataGridView1);
            DgvUtil.CleanDgv(this.dataGridView2);
            DgvUtil.CleanDgv(this.dataGridView3);
            idDataSource = new List<string>();
            grpDataSource = new List<string>();
            keyWordDataSource = new List<string>();
            resultDictionary = new Dictionary<string, List<string>>();
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
            return "(网)QQ爬虫";
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

        private void InputKeyWordButton_Click(object sender, EventArgs e)
        {
            ImportData(2);
            UpdateListView(2);
        }

        private void UpdateListView(int tabIndex)
        {
            RichTextBox lv = new RichTextBox();
            List<string> dataSource = new List<string>();
            if (tabIndex == 0)
            {
                lv = this.richTextBox1;
                dataSource = this.idDataSource;
            }
            else if (tabIndex == 1)
            {
                lv = this.richTextBox2;
                dataSource = this.grpDataSource;
            }
            else
            {
                lv = this.keyWordRichTextBox;
                dataSource = this.keyWordDataSource;
            }
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
            if (fd.ShowDialog() != DialogResult.OK)
                return;
            if (tabIndex == 0)
                this.idDataSource = LoadFile(fd.FileName);
            else if (tabIndex == 1)
                this.grpDataSource = LoadFile(fd.FileName);
            else
                this.keyWordDataSource = LoadFile(fd.FileName);
        }

        private List<string> LoadFile(string fileName)
        {
            List<string> lines = new List<string>();
            try
            {
                string line;
                using (StreamReader file = new System.IO.StreamReader(fileName, Encoding.Default))
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

        private void KeyWordStartButton_Click(object sender, EventArgs e)
        {
            DgvManager dgvMgr = new DgvManager(this.dataGridView3);
            List<string> dataSource = this.keyWordDataSource;
            
            ResetProgressBar(2, dataSource.Count);
            if (dataSource.Count == 0)
            {
                ShowMessageBox("请先导入信息");
                return;
            }
            if (dataSource.Count > 5)
            {
                ShowMessageBox("查询关键词个数超过5个，请重新输入");
                return;
            }
            if(!KeyWordLogin())
                return;

            QQCrawler crawler = new QQCrawler(session);

            this.Cursor = Cursors.WaitCursor;
            foreach (string id in dataSource)
            {
                List<string> resultList = crawler.QueryKeyWord(id);
                dgvMgr.AppendLineList(resultList);
                this.progressBar3.Value += 1;
                this.resultDictionary.Add(id, resultList);
            }
            this.Cursor = Cursors.Arrow;
        }

        private void ResetProgressBar(int tabIndex, int count)
        {
            ProgressBar bar = new ProgressBar();
            if (tabIndex == 0 || tabIndex == 1 || tabIndex == 2)
            {
                switch (tabIndex)
                {
                    case 0:
                        bar = this.progressBar1;
                        break;
                    case 1:
                        bar = this.progressBar2;
                        break;
                    case 2:
                        bar = this.progressBar3;
                        break;
                }
            }
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

        private void Cancle_Click(object sender, EventArgs e)
        {
            this.Close();
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

            if (this.dataGridView1.Rows.Count == 0)
            {
                ShowMessageBox("空文件无法导出");
                return;
            }

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

            if (this.dataGridView2.Rows.Count == 0)
            {
                ShowMessageBox("空文件无法导出");
                return;
            }

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
        private void OutputButton3_Click(object sender, EventArgs e)
        {
            if (this.dataGridView3.Rows.Count == 0)
            {
                ShowMessageBox("空文件无法导出");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel Documents (*.xls)|*.xls",
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Util.KeyWordSaveToExcel(sfd.FileName, this.resultDictionary);
                ShowMessageBox("导出成功");
            }
        }

        public void Login()
        {
            QrLogin login = new QrLogin();
            byte[] imgBytes = login.GetQRCode().Content;
            if (imgBytes == null || imgBytes != null && imgBytes.Length == 0)
                return;
            Image img = null;
            try
            {
                img = Image.FromStream(new MemoryStream(imgBytes));
            }
            catch (ArgumentException)
            {
                imgBytes = new byte[0];
                return;
            }
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
            Uri a = new Uri("https://ui.ptlogin2.qq.com");
            //this.session.Cookies.SetCookies(a, "uin=o2420184549,skey=ZrThhhlK7g;RK=mbQAn+n77X,ptcz=21c6e274ea3e69c79df51e2df9a1e75901027b004996e87a1fe0d2e7f3f2e985,p_skey=1dHPok65JUsRVfqVu*M7BGiUtS5DVlH1vpOpsxn6RKs_,p_uin=o2420184549");
            this.session.Cookies.SetCookies(a, "p_skey=1dHPok65JUsRVfqVu*M7BGiUtS5DVlH1vpOpsxn6RKs_,p_uin=o2420184549");
            string skey = this.session.Cookies.GetCookieValue("skey");
            string RK = this.session.Cookies.GetCookieValue("RK");
            string uin = this.session.Cookies.GetCookieValue("uin");
            string ptcz = this.session.Cookies.GetCookieValue("ptcz");
            if (String.IsNullOrEmpty(skey))
                return;
            this.session.Ldw = Util.GenBkn(skey);
            return;
        }
        
        public bool KeyWordLogin()
        {
            QrLogin login = new QrLogin();
            byte[] imgBytes = login.GetKeyWordQRCode();
            if (imgBytes == null || imgBytes != null && imgBytes.Length == 0)
            {
                ShowMessageBox("爬虫服务器或本地网络错误");
                return false ;
            }
                
            Image img = null;
            try
            {
                img = Image.FromStream(new MemoryStream(imgBytes));
            }
            catch (ArgumentException)
            {
                imgBytes = new byte[0];
                return false;
            }
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
                Dictionary<string, object> result = login.GetScanStatus();
                if (qrCodeForm.IsDisposed)
                {
                    _thread.Abort();
                    break;
                }
                if (result.TryGetValue("status", out status) && (int)status == 1)
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
                Console.WriteLine("扫码超时!");
                ShowMessageBox("扫码超时!");
                this.Invoke(new CloseQrForm(new CloseQrForm(delegate ()
                {
                    qrCodeForm.Close();
                    _thread.Abort();
                })));
                return false;
            }
            return true;
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
                string trim_space = Regex.Replace(this.richTextBox1.Text, " ", string.Empty);
                input = new List<string>(trim_space
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
        
        private void KeyWordRichTextBox_TextChanged(object sender, EventArgs e)
        {
            List<string> input = new List<string>();
            try
            {
                string trim_space = Regex.Replace(this.keyWordRichTextBox.Text, " ", string.Empty);
                input = new List<string>(trim_space
                    .Trim(Environment.NewLine.ToCharArray())
                    .Split(Environment.NewLine.ToCharArray())
                    );
            }
            catch
            {
                ShowMessageBox("输出有误");
            }
            this.keyWordDataSource = input;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.label11.Visible = false;

            string wxid = wxid_prefix + textBox1.Text.Trim();
            ThoughtWorks.QRCode.Codec.QRCodeEncoder en = new ThoughtWorks.QRCode.Codec.QRCodeEncoder();
            try 
            {
                Bitmap image = en.Encode(wxid);
                this.pictureBox1.Image = image;
            }
            catch
            {
                this.label11.Visible = true;
            }
        }

        private void WXCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GroupCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void KeyWordCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
