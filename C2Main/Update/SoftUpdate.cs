using System;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using C2.Core;

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
        private string downloadC2Service;
        private string filenameOuter;
        private string filenameC2F;
        private const string updateUrl = "https://113.31.114.239:53376/C2/update.xml";//升级配置的XML文件地址  
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
        private string manulVersion;
        private string softName;
        private int isUpdate;

        /// <summary>  
        /// 或取是否需要更新  
        /// </summary>  
        public int IsUpdate
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
            if (isUpdate == 0)
                return;
            WebClient wc = new WebClient();
            string extenOuter = downloadC2Outer.Substring(downloadC2Outer.LastIndexOf("/")).Replace("/", string.Empty);
            string extenC2F = downloadC2F.Substring(downloadC2F.LastIndexOf("/")).Replace("/", string.Empty);
            filenameOuter = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "install") + "\\" + extenOuter;
            filenameC2F = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "install") + "\\" + extenC2F;

            if (isUpdate == 1)
            {
                MessageBox.Show("安装包下载中，下载完成后会弹出安装页面，请耐心等待!");
                wc.DownloadFile(downloadC2Outer, filenameOuter);
                Process.Start(filenameOuter);
                wc.Dispose();
            }
            else if(isUpdate == 2)
            {
                MessageBox.Show("战术手册下载中，下载完成后会弹出安装页面，请耐心等待!");
                wc.DownloadFile(downloadC2F, filenameC2F);
                Process.Start(filenameC2F);
                wc.Dispose();
            }
        }

        public void SaveOther(int flagNum = 0)
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
                        else if (xml.Name == "DownLoadC2Service")
                            downloadC2Service = xml.InnerText;
                    }
                }
            }
            if (!File.Exists(downloadPath))
                Directory.CreateDirectory(downloadPath);
            string extenInner = downloadC2Inner.Substring(downloadC2Inner.LastIndexOf("/")).Replace("/", string.Empty);
            string filenameInner = downloadPath + "\\" + extenInner;
            string extenF = downloadC2F.Substring(downloadC2F.LastIndexOf("/")).Replace("/", string.Empty);
            string filenameF = downloadPath + "\\" + extenF;
            string extentService = downloadC2Service.Substring(downloadC2Service.LastIndexOf("/")).Replace("/", string.Empty);
            string filenameService = downloadPath + "\\" + extentService;
            if (flagNum == 0)
            {
                MessageBox.Show("单兵作战(内网版)下载中，下载完成后会弹出安装目录，请耐心等待!");
                wc.DownloadFileCompleted += Client_DownloadFileCompleted;
                wc.DownloadFileTaskAsync(downloadC2Inner, filenameInner);
            }
            else if(flagNum == 1)
            {
                MessageBox.Show("战术手册下载中，下载完成后会弹出安装目录，请耐心等待!");
                wc.DownloadFileCompleted += Client_DownloadFileFCompleted; 
                wc.DownloadFileTaskAsync(downloadC2F, filenameF);
            }
            else
            {
                MessageBox.Show("服务版(内网)下载中，下载完成后会弹出安装目录，请耐心等待!");
                wc.DownloadFileCompleted += Client_DownloadFileServiceCompleted;
                wc.DownloadFileTaskAsync(downloadC2Service, filenameService);
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
        void Client_DownloadFileServiceCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.UserState != null)
                MessageBox.Show("服务(内网版)下载完成");
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
        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        /// <summary>  
        /// 检查是否需要更新  
        /// </summary>  
        public void CheckUpdate()
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
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
                            else if(xml.Name == "ManulVersion")
                                manulVersion = xml.InnerText;
                            else if(xml.Name == "DownLoadC2Outer")
                                downloadC2Outer = xml.InnerText;
                            else if (xml.Name == "DownLoadC2Inner")
                                downloadC2Inner = xml.InnerText;
                            else if (xml.Name == "DownLoadC2F")
                                downloadC2F = xml.InnerText;
                        }
                    }
                }
                // C2版本检测更新
                string localC2Verson = Assembly.LoadFrom(loadFile).GetName().Version.ToString();
                int tm = localC2Verson.Substring(0, 5).CompareTo(newVerson);
                int tmM = 1;
                //战术手册检测更新，检测修改日期是否一致
                string backManulDir = Path.Combine(Global.UserWorkspacePath, "备份数据");
                DirectoryInfo TheFolder = new DirectoryInfo(backManulDir);
                foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
                    if (!NextFolder.LastWriteTime.ToString().Contains(manulVersion))
                        tmM = -1;
                    
                if (tm >= 0 && tmM == 1)//皆不更新
                    isUpdate = 0;
                else if (tm < 0)//只更新C2
                    isUpdate = 1;
                else if (tmM == -1)//只更新战术手册
                    isUpdate = 2;
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