using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.CastleBravo.WebShellTool
{
    class ClientFactory
    {
        public static IClient Create(string password, string clientSetting)
        {
            string mode = ClientSetting.WSDict.ContainsKey(clientSetting) ? ClientSetting.WSDict[clientSetting].Item2 : string.Empty ;
            switch (mode)
            {
                case "Common":
                    return new CommonClient(password, clientSetting);
                case "CKnife16EXE":
                    return new CKnife16EXEClient(password, clientSetting);
                default:
                    return new CommonClient(password, clientSetting);
            }
        }
    }
}
