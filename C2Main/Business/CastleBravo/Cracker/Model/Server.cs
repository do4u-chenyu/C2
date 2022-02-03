using C2.Business.Cracker.rdp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Business.Cracker.Model
{
    public class Server
    {
        public long id = 0;
        public bool isSuccess = false;
        public string banner = string.Empty;
        public string ip = string.Empty;
        public string serverName = string.Empty;
        public int port = 0;
        public string username = string.Empty;
        public string password = string.Empty;
        public Boolean isDisConnected = false;
        public int timeout = 10;
        public AutoResetEvent isEndMRE = new AutoResetEvent(false);
        public Boolean isConnected = false;
        public RdpClient client = null;
        public long userTime = 0;
        public TabPage tp = null;
        public TabControl tc = null;
    }
}
