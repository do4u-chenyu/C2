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
            if (ClientSetting.CKnifeDict.ContainsKey(clientSetting))
                return new CKnifeClient(password, clientSetting);
            return new CKnifeClient(password, clientSetting);
        }
    }
}
