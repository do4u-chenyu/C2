using C2.IAOLab.Plugins;
using C2.Log;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Drawing.Imaging;

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
        private string ResultFilePath = @"C:\FiberHomeIAOModelDocument\IAO\实验楼\QQ爬虫";
        QrLogin keyWordLogin = new QrLogin();
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
            if (!Directory.Exists(this.ResultFilePath))  // 创建目标路径
            {
                Directory.CreateDirectory(this.ResultFilePath);
            }
        }
        private void InitializeForm()
        {
            session = new Session();
            this.richTextBox2.Text = String.Empty;
            this.richTextBox1.Text = String.Empty;
            DgvUtil.CleanDgv(this.dataGridView1);
            DgvUtil.CleanDgv(this.dataGridView2);
            DgvUtil.CleanDgv(this.dataGridView3);
            idDataSource = new List<string>();
            grpDataSource = new List<string>();
            keyWordDataSource = new List<string>();
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
                this.keyWordRichTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
                this.keyWordRichTextBox.Text = "请输入关键词，\n每行一个，\n建议不超过10个";
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
            new Log().LogManualButton("(网)QQ爬虫", "运行");
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
            new Log().LogManualButton("(网)QQ爬虫", "运行");
            DgvManager dgvMgr = new DgvManager(this.dataGridView3);
            List<string> dataSource = this.keyWordDataSource;
            if (dataSource.Count == 0)
            {
                ShowMessageBox("请先导入信息");
                return;
            }
            if (dataSource.Count > 10)
            {
                ShowMessageBox("查询关键词个数超过10个，请重新输入");
                return;
            }
            if (!KeyWordLogin())
                return;

            if (this.session.Ldw != "true")
            {
                ShowMessageBox("登录失败，请重新扫描登录");
                session = new Session();
                return;
            }

            dgvMgr.InitGroupResult(dataSource);  // 初始化关键词爬虫的dgv


            this.Cursor = Cursors.WaitCursor;  // 程序执行过程中箭头变为等待圆圈
            for (int i = 0; i < dataSource.Count; i++)
            {
                string keyword = dataSource[i];
                //this.QueryKeyWord(keyword, i, dgvMgr);
                this.QueryKeyWordExcel(keyword, i, dgvMgr);
                //string txtPath = this.ResultFilePath + "\\" + keyword + ".txt";
                //string excelPath = this.ResultFilePath + "\\" + keyword + ".xls";

                //Util.TxtToExcel(excelPath, txtPath);  // txt转excel
            }
            this.Cursor = Cursors.Arrow;
            ShowMessageBox("查询完成！");
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
                }
            }
            bar.Maximum = count;
            bar.Minimum = 0;
            bar.Value = 0;
        }

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
        
        private void ResultButton_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(this.ResultFilePath))
                System.Diagnostics.Process.Start(this.ResultFilePath);
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
            if (this.session.Cookies.GetCookieValue("p_skey") != "")  // 避免重复登录
            {
                return true;
            }

            byte[] imgBytes = keyWordLogin.GetKeyWordQRCode();
            if (imgBytes == null || imgBytes != null && imgBytes.Length == 0)
            {
                ShowMessageBox("本地网络错误或爬虫被限制");
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
                Dictionary<string, object> result = keyWordLogin.KeyWordLogin();
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
                    if (result.ContainsKey("session"))
                    {
                        this.session = (Session)result["session"];
                    }
                    this.session.Ldw = "true";
                    break;
                }
                Console.WriteLine(result.ToString(), "请使用QQ手机客户端扫码登录！");
                System.Threading.Thread.Sleep(1000);
                count += 1;
            }
            if (count == maxTimes)
            {
                Console.WriteLine("扫码超时!");
                this.Invoke(new CloseQrForm(new CloseQrForm(delegate ()
                {
                    qrCodeForm.Close();
                    _thread.Abort();
                })));
                //ShowMessageBox("扫码超时!");
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

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
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

        private void QueryKeyWord(string keyword, int id, DgvManager dgvM)
        {
            int resultCount = 0;

            string url = "https://qun.qq.com/cgi-bin/group_search/pc_group_search";
            Dictionary<string, string> param = new Dictionary<string, string>
            {
                {"from", "1" },
                { "keyword", keyword },
                { "wantnum", "24" },
                { "page", "0" },
                {"sort", "2"},  // sort type: 0 默认排序, 1 人数优先, 2 活跃优先
                {"isRecommend", "false"}
            };
            List<string> target_keys = new List<string>
            { "code", "name", "member_num", "max_member_num", "owner_uin", "qaddr", "gcate", "labels", "memo", "url"};
            string filePath = this.ResultFilePath + "\\" + keyword + ".txt";  // 创建储存当前词的txt文件
            FileStream fs = File.Open(filePath, FileMode.OpenOrCreate);
            string tmp = "群号\t群名称\t群人数\t群人数上限\t群主\t地域\t群分类\t群标签\t群简介\t群头像地址\n";  // 添加列名
            byte[] data = Encoding.Default.GetBytes(tmp);
            fs.Position = fs.Length;
            fs.Write(data, 0, data.Length);
            fs.Flush();
            int i = 0;
            int t = 0;
            bool isId = int.TryParse(keyword, out t);
            int maxPage = 10;
            if (isId)
            {
                maxPage = 1;
            }
            while (resultCount < 150 && i < maxPage)  // 一个词最多抓150条，且请求不超过10次
            {
                param["page"] = i.ToString();
                try
                {
                    Response resp = this.session.Post(url, param);
                    JObject json = JObject.Parse(resp.Text);
                    if (json["errcode"].ToString() == "0" && json["ec"].ToString() != "99997")  // 不满足这些条件，表示被爬虫被限制了
                    {
                        if (json.ContainsKey("group_list"))
                        {
                            string group_list_string = json["group_list"].ToString();
                            List<JToken> group_list = json["group_list"].ToList();
                            foreach (JToken x in group_list)
                            {
                                tmp = "";
                                foreach (string key in target_keys)
                                {
                                    if (x.SelectToken(key) != null)
                                    {
                                        string s = x[key].ToString();
                                        if (key == "qaddr")
                                        {
                                            StringBuilder qaddrSb = new StringBuilder();
                                            foreach (var q in x["qaddr"])
                                                qaddrSb.Append(q.ToString());
                                            s = qaddrSb.ToString();
                                        }
                                        if (key == "gcate")
                                        {
                                            StringBuilder gcateSb = new StringBuilder();
                                            foreach (var l in x["gcate"])
                                                gcateSb.Append(l.ToString()).Append("|");
                                            s = Util.GenRwWTS(gcateSb.ToString().Trim('|'));
                                        }
                                        if (key == "labels")
                                        {
                                            StringBuilder labelSb = new StringBuilder();
                                            foreach (var l in x["labels"])
                                                labelSb.Append(l["label"].ToString()).Append("|");
                                            s = Util.GenRwWTS(labelSb.ToString().Trim('|'));
                                        }
                                        tmp = tmp + "\t" + (s.Replace('\t', ','));
                                    }
                                    else
                                    {
                                        tmp = tmp + "\t" + "";
                                    }
                                }
                                data = Encoding.Default.GetBytes(tmp.Trim('\t').Replace('\n', ',')+"\n");
                                fs.Position = fs.Length;
                                fs.Write(data, 0, data.Length);
                                fs.Flush();
                                resultCount++;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                    dgvM.ChangeCellValue(id, 1, resultCount.ToString());
                    dgvM.ChangeCellValue(id, 2, ((i+1)*1.0 / maxPage * 100.0).ToString() + "%");
                }
                catch { }
                Thread.Sleep(new Random().Next(3000, 8000));  //随机停3到8秒
                i++;
            }
            fs.Close();
            dgvM.ChangeCellValue(id, 2, "100%");
        }

        private void keyWordRichTextBox_TextChanged(object sender, EventArgs e)
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

        private void keyWordRichTextBox_Click(object sender, EventArgs e)
        {
            if(keyWordRichTextBox.Text== "请输入关键词，\n每行一个，\n建议不超过10个")
            {
                keyWordRichTextBox.Clear();
                keyWordRichTextBox.ForeColor = SystemColors.WindowText;
            }
        }

        private void QueryKeyWordExcel(string keyword, int id, DgvManager dgvM)
        {
            int resultCount = 0;

            string url = "https://qun.qq.com/cgi-bin/group_search/pc_group_search";
            Dictionary<string, string> param = new Dictionary<string, string>
            {
                {"from", "1" },
                { "keyword", keyword },
                { "wantnum", "24" },
                { "page", "0" },
                {"sort", "2"},  // sort type: 0 默认排序, 1 人数优先, 2 活跃优先
                {"isRecommend", "false"}
            };
            List<string> target_keys = new List<string>
            { "url", "code", "name", "member_num", "max_member_num", "owner_uin", "qaddr", "gcate", "labels", "memo" };

            // excel相关
            string excelPath = this.ResultFilePath + "\\" + keyword + ".xls";  // 创建储存当前词的xls文件
            FileStream fs = new FileStream(excelPath, FileMode.Create, FileAccess.Write);
            short rowHeight = 800;
            List<string> headList = new List<string> { "群头像", "群ID", "群名称", "群人数", "群人数上限", "群主QQ", "群地址", "群分类", "群标签", "群简介" };
            HSSFWorkbook workbook;
            HSSFSheet sheet;
            int colCount = 10;  // excel列数
            workbook = new HSSFWorkbook();
            sheet = (HSSFSheet)workbook.CreateSheet("Sheet1");
            // 第一行处理
            try
            {
                IRow row = sheet.CreateRow(0);
                for (int j = 0; j < colCount; j++)
                {
                    ICell cell = row.CreateCell(j);
                    cell.SetCellValue(headList[j]);
                }
            }
            catch (Exception) { }

            int i = 0;  // 表示page
            // 判断是否为群号，若为群号则只翻一页，即maxPage为1，否则最多翻10页，即maxPage为10
            int t = 0;
            bool isId = int.TryParse(keyword, out t);
            int maxPage = 10;
            if (isId)
            {
                maxPage = 1;
            }
            // 开始翻页抓取
            string tmp;
            while (resultCount < 150 && i < maxPage)  // 一个词最多抓150条，且请求不超过10次
            {
                param["page"] = i.ToString();
                try
                {
                    Response resp = this.session.Post(url, param);
                    JObject json = JObject.Parse(resp.Text);
                    if (json["errcode"].ToString() == "0" && json["ec"].ToString() != "99997")  // 不满足这些条件，表示被爬虫被限制了
                    {
                        if (json.ContainsKey("group_list"))
                        {
                            string group_list_string = json["group_list"].ToString();
                            List<JToken> group_list = json["group_list"].ToList();
                            foreach (JToken x in group_list)  // 处理每个群
                            {
                                tmp = "";
                                foreach (string key in target_keys)  // 处理每个字段
                                {
                                    if (x.SelectToken(key) != null)
                                    {
                                        string s = x[key].ToString();
                                        if (key == "qaddr")
                                        {
                                            StringBuilder qaddrSb = new StringBuilder();
                                            foreach (var q in x["qaddr"])
                                                qaddrSb.Append(q.ToString());
                                            s = qaddrSb.ToString();
                                        }
                                        if (key == "gcate")
                                        {
                                            StringBuilder gcateSb = new StringBuilder();
                                            foreach (var l in x["gcate"])
                                                gcateSb.Append(l.ToString()).Append("|");
                                            s = Util.GenRwWTS(gcateSb.ToString().Trim('|'));
                                        }
                                        if (key == "labels")
                                        {
                                            StringBuilder labelSb = new StringBuilder();
                                            foreach (var l in x["labels"])
                                                labelSb.Append(l["label"].ToString()).Append("|");
                                            s = Util.GenRwWTS(labelSb.ToString().Trim('|'));
                                        }
                                        tmp = tmp + "\t" + (s.Replace('\t', ','));
                                    }
                                    else
                                    {
                                        tmp = tmp + "\t" + "";
                                    }
                                }
                                // 结果写入excel
                                if (tmp.StartsWith("\t"))
                                {
                                    tmp = tmp.Substring(1, tmp.Length-1);
                                }
                                string[] currentRowList = tmp.Split('\t');
                                if ((resultCount+1) >= 65536)
                                {
                                    break;
                                }
                                IRow row = sheet.CreateRow((resultCount + 1));
                                row.Height = rowHeight;
                                Console.WriteLine(currentRowList.Length.ToString());
                                for (int j = 0; j < colCount; j++)
                                {
                                    ICell cell = row.CreateCell(j);

                                    // 头像
                                    if (j == 0)
                                    {
                                        Image image;
                                        try
                                        {
                                            image = Util.GetImage(currentRowList[0]);
                                            byte[] bytes;
                                            using (var ms = new MemoryStream())
                                            {
                                                image.Save(ms, ImageFormat.Png);
                                                bytes = ms.ToArray();
                                            }
                                            int pictureIndex = workbook.AddPicture(bytes, PictureType.PNG);
                                            ICreationHelper helper = workbook.GetCreationHelper();
                                            IDrawing drawing = sheet.CreateDrawingPatriarch();
                                            IClientAnchor anchor = helper.CreateClientAnchor();
                                            anchor.Col1 = 0;//0 index based column
                                            anchor.Row1 = resultCount+1;//0 index based row
                                            IPicture picture = drawing.CreatePicture(anchor, pictureIndex);
                                            picture.Resize();
                                        }
                                        catch (Exception) { }
                                    }
                                    else
                                    {
                                        cell.SetCellValue(currentRowList[j]);
                                    }
                                }
                                resultCount++;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                    dgvM.ChangeCellValue(id, 1, resultCount.ToString());
                    dgvM.ChangeCellValue(id, 2, ((i + 1) * 1.0 / maxPage * 100.0).ToString() + "%");
                }
                catch { }
                Thread.Sleep(new Random().Next(3000, 8000));  //随机停3到8秒
                i++;
            }
            try
            {
                workbook.Write(fs);
            }
            catch { }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                workbook = null;
            }
            dgvM.ChangeCellValue(id, 2, "100%");
        }
    }
}
