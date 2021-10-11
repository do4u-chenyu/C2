using C2.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace C2.Business.CastleBravo.WebShellTool
{
    public class WebShellClient
    {
        private readonly string url;       
        private readonly IClient client;
        private string lastErrorMessage;
        public string FetchLog() 
        { 
            string ret = client.FetchLog() + lastErrorMessage;
            lastErrorMessage = string.Empty;
            return ret;
        }

        public WebShellClient(string url, string password, string clientSetting)
        {
            this.url = url;
            this.lastErrorMessage = string.Empty;
            this.client = ClientFactory.Create(password, clientSetting);
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
            return client.MidStrEx(Post(client.PHPInfo()));
        }

        public List<string> PHPIndex()
        {
            string[] result = client.MidStrEx(Post(client.PHPIndex())).Split('\t');
            return result.Skip(0).Take(result.Length - 1).ToList();
        }

        public string PHPReadDict(string dict)
        {
            return client.MidStrEx(Post(client.PHPReadDict(dict)));
        }

        public string PHPShell(string shellEnv, string command)
        {
            return client.MidStrEx(Post(client.PHPShell(shellEnv, command)));
        }

        private string Post(string payload)
        {
            this.lastErrorMessage = string.Empty;
            try
            {
                return WebClientEx.Post(this.url, payload);
            }
            catch (Exception e)
            {
                this.lastErrorMessage = e.Message;
                return string.Empty;
            }
            
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

