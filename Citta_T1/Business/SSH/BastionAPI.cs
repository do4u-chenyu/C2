using C2.SearchToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.SSH
{
    class BastionAPI
    {
        private String username;
        private String password;
        private String bastionIP;
        private String searchAgentIP;

        private SSHClient ssh;
        public BastionAPI(String username, String password, String bIP, String sIP) 
        {
            this.username = username;
            this.password = password;
            this.bastionIP = bIP;
            this.searchAgentIP = sIP;

            this.ssh = new SSHClient(); 
        }

        public BastionAPI(TaskInfo task) : this(task.Username, task.Password, task.BastionIP, task.SearchAgentIP)
        { }

        public BastionAPI Login() 
        {
            // 这里通过抛出异常来报错
            ssh.Connect(this.username, this.password, this.bastionIP, this.searchAgentIP);
            return this; 
        }

        public String GambleTaskPID () { return String.Empty; }

        public String DownloadGambleTaskResult() { return String.Empty; }

        public String DeleteGambleTaskResult() { return String.Empty; }

        public String GambleTaskStatus() { return String.Empty; }

        public BastionAPI KillGambleTask() { return this; }

        public String RunGambleTask() { return String.Empty; }

        public BastionAPI UploadGambleScript() { return this; }
        public BastionAPI CreateGambleTaskDirectory() { return this; }

        public String YellowTaskPID() { return String.Empty; }

        public String GunTaskPID() { return String.Empty; }

        public String PlaneTaskPID() { return String.Empty; }
    }
}
