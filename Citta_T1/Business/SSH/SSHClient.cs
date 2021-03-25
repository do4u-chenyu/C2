using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.SSH
{
    class SSHClient
    {
        // 执行远程命令，返回ReturnCode
        public String RunLinuxCmd(String user, String pass, String bIP, String sIP, String cmd)
        {
            String retCode = String.Empty;
            return retCode;
        }

        public String mkdir(String user, String pass, String bIP, String sIP, String workspace) { return String.Empty; }

        public String ps_aux(String user, String pass, String bIP, String sIP) { return String.Empty; }

        public SSHClient Connect(String user, String pass, String bIP, String sIP) { return this; }

        public String Upload(String user, String pass, String bIP, String sIP, String localFFP, String remoteFFP) { return String.Empty; }
    }
}
