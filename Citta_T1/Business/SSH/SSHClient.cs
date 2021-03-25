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
        public String RunLinuxCmd(String username, String password, String bIP, String sIP, String command)
        {
            String retCode = String.Empty;
            return retCode;
        }

        public SSHClient Connect(String username, String password, String bIP, String sIP) { return this; }
    }
}
