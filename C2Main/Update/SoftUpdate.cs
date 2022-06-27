using System;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
using System.ComponentModel;

namespace C2.Update
{
    /// <summary>  
    /// 更新完成触发的事件  
    /// </summary>  
    public delegate void UpdateState();
    /// <summary>  
    /// 程序更新  
    /// </summary>  
    public class SoftUpdate
    {
        /// <summary>  
        /// 如何对程序进行更新
        /// 1. 首先需要修改 Properties/AssemblyInfo里面的版本号为当前更新后的版本号
        /// 2. 服务器端上传更新后的分析师单兵作战装备内网、外网和战术手册
        /// 3. 更新服务端配置文件里面的下载文件地址和版本号
        /// </summary>  
        private string downloadC2Outer;
        private string downloadC2Inner;
        private string downloadC2F;
        private string filenameOuter;
        private const string updateUrl = "http://113.31.114.239:53373/C2/update.xml";//升级配置的XML文件地址  
        private readonly string downloadPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "install");

        #region 构造函数  
        public SoftUpdate() { }

        /// <summary>  
        /// 程序更新  
        /// </summary>  
        /// <param name="file">要更新的文件</param>  
        public SoftUpdate(string file, string softName)
        {
            this.LoadFile = file;
            this.SoftName = softName;
        }
        #endregion

        #region 属性  
        private string loadFile;
        private string newVerson;
        private string softName;
        private bool isUpdate;

        /// <summary>  
        /// 或取是否需要更新  
        /// </summary>  
        public bool IsUpdate
        {
            get
            {
                CheckUpdate();
                return isUpdate;
            }
        }

        /// <summary>  
        /// 要检查更新的文件  
        /// </summary>  
        public string LoadFile
        {
            get { return loadFile; }
            set { loadFile = value; }
        }

        /// <summary>  
        /// 程序集新版本  
        /// </summary>  
        public string NewVerson
        {
            get { return newVerson; }
        }

        /// <summary>  
        /// 升级的名称  
        /// </summary>  
        public string SoftName
        {
            get { return softName; }
            set { softName = value; }
        }

        #endregion

        /// <summary>  
        /// 更新完成时触发的事件  
        /// </summary>  
        public event UpdateState UpdateFinish;
        public event UpdateState SaveFinish;
        private void isFinish()
        {
            if (UpdateFinish != null)
                UpdateFinish();
        }
        private void IsFileFinish()
        {
            SaveFinish?.Invoke();
        }

        /// <summary>  
        /// 存储下载文件  
        /// </summary>  
        private void Save()
        {
            if (!isUpdate)
                return;
            WebClient wc = new WebClient();
            string extenOuter = downloadC2Outer.Substring(downloadC2Outer.LastIndexOf("/")).Replace("/", string.Empty);
            filenameOuter = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "install") + "\\" + extenOuter;
            MessageBox.Show("安装包下载中，下载完成后会弹出安装页面，请耐心等待!");
            wc.DownloadFile(downloadC2Outer, filenameOuter);
            Process.Start(filenameOuter);
            wc.Dispose();
        }

        public void SaveOther(bool flag = true)
        {
            WebClient wc = new WebClient();
            Stream stream = wc.OpenRead(updateUrl);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(stream);
            XmlNode list = xmlDoc.SelectSingleNode("Update");
            foreach (XmlNode node in list)
            {
                if (node.Name == "Soft")
                {
                    foreach (XmlNode xml in node)
                    {
                        if (xml.Name == "DownLoadC2Inner")  
                            downloadC2Inner = xml.InnerText;
                        else if (xml.Name == "DownLoadC2F")
                            downloadC2F = xml.InnerText;
                    }
                }
            }
            if (!File.Exists(downloadPath))
                Directory.CreateDirectory(downloadPath);
            string extenInner = downloadC2Inner.Substring(downloadC2Inner.LastIndexOf("/")).Replace("/", string.Empty);
            string filenameInner = downloadPath + "\\" + extenInner;
            string extenF = downloadC2F.Substring(downloadC2F.LastIndexOf("/")).Replace("/", string.Empty);
            string filenameF = downloadPath + "\\" + extenF;
            if (flag)
            {
                MessageBox.Show("单兵作战(内网版)下载中，下载完成后会弹出安装目录，请耐心等待!");
                wc.DownloadFileCompleted += Client_DownloadFileCompleted;
                wc.DownloadFileTaskAsync(downloadC2Inner, filenameInner);
            }
            else
            {
                MessageBox.Show("战术手册下载中，下载完成后会弹出安装目录，请耐心等待!");
                wc.DownloadFileCompleted += Client_DownloadFileFCompleted; 
                wc.DownloadFileTaskAsync(downloadC2F, filenameF);
            }
            wc.Dispose();
        }
        void Client_DownloadFileFCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.UserState != null)
                MessageBox.Show("战术手册下载完成");
            Process.Start(downloadPath);
        }
        void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.UserState != null)
                MessageBox.Show("单兵作战(内网版)下载完成");
            Process.Start(downloadPath);
        }
        /// <summary>  
        /// 下载更新  
        /// </summary>  
        public void Update()
        {
            try
            {
                Save();
                //isFinish();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>  
        /// 检查是否需要更新  
        /// </summary>  
        public void CheckUpdate()
        {
            try
            {
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(updateUrl);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(stream);
                XmlNode list = xmlDoc.SelectSingleNode("Update");
                foreach (XmlNode node in list)
                {
                    if (node.Name == "Soft" && node.Attributes["Name"].Value.ToLower() == SoftName.ToLower())
                    {
                        foreach (XmlNode xml in node)
                        {
                            if (xml.Name == "Verson")
                                newVerson = xml.InnerText;
                            else if(xml.Name == "DownLoadC2Outer")
                                downloadC2Outer = xml.InnerText;
                            else if (xml.Name == "DownLoadC2Inner")
                                downloadC2Inner = xml.InnerText;
                            else if (xml.Name == "DownLoadC2F")
                                downloadC2F = xml.InnerText;
                        }
                    }
                }
                Version ver = new Version(newVerson);
                Version verson = Assembly.LoadFrom(loadFile).GetName().Version;
                int tm = verson.CompareTo(ver);

                if (tm >= 0)
                    isUpdate = false;
                else
                    isUpdate = true;
            }
            catch(Exception ex)
            {
                if (ex.Message.Contains("分析 EntityName 时出错"))
                    return;
                else
                    MessageBox.Show(ex.Message);
            }
        }

        /// <summary>  
        /// 获取要更新的文件  
        /// </summary>  
        /// <returns></returns>  
        public override string ToString()
        {
            return this.loadFile;
        }
    }
}