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
                case "mode1":
                    return new CKnifeClient(password, clientSetting);
                default:
                    return new CKnifeClient(password, clientSetting);
            }
        }
    }
}
