using System;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;

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

        private string download;
        private string exten;
        private string filename;
        private const string updateUrl = "http://113.31.114.239:53373/C2/update.xml";//升级配置的XML文件地址  

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
                checkUpdate();
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
        private void Save()
        {
            if (!isUpdate)
                return;
            WebClient wc = new WebClient();
            exten = download.Substring(download.LastIndexOf("/")).Replace("/", string.Empty);
            filename = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "install") + "\\" + exten;
            MessageBox.Show("安装包下载中，下载完成后会弹出安装页面，请耐心等待!");
            wc.DownloadFile(download, filename);
            wc.Dispose();
        }

        /// <summary>  
        /// 下载更新  
        /// </summary>  
        public void Update()
        {
            try
            {
                Save();
                Process.Start(filename);
                //isFinish();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>  
        /// 存储下载文件  
        /// </summary>  
        public void SaveFile()
        {
            try
            {
                Save();
                IsFileFinish();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>  
        /// 检查是否需要更新  
        /// </summary>  
        public void checkUpdate()
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
                            else
                                download = xml.InnerText;
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
            catch
            {
                throw new Exception("更新出现错误，请确认网络连接无误后重试！");
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