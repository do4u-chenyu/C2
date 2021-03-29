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

        private String linuxWorkspace;

        private SSHClient ssh;

        // {workspace}/pid_taskcreatetime/result
        // {workspace}/pid_taskcreatetime/script/
        private String gambleScript = "batchquery_db_accountPass_version20210324.py";
        private String importSearchEnv = @". /home/search/search_profile;";
        public BastionAPI(String username, String password, String bIP, String sIP, String dir) 
        {
            this.username = username;
            this.password = password;
            this.bastionIP = bIP;
            this.searchAgentIP = sIP;
            this.linuxWorkspace = dir;

            this.ssh = new SSHClient(); 
        }

        public BastionAPI(TaskInfo task) : this(task.Username, task.Password, task.BastionIP, task.SearchAgentIP, task.RemoteWorkspace)
        { }

        public BastionAPI Login() 
        {
            // 这里通过抛出异常来报错
            ssh.Connect(this.username, this.password, this.bastionIP, this.searchAgentIP);
            return this; 
        }

        public String GambleTaskPID (String filter) 
        {
            String command = String.Format(@"ps aux | grep -i python | grep {0} | grep {1} | awk {{print $4}}", gambleScript, filter);
            return String.Empty; 
        }

        public String DownloadGambleTaskResult() { return String.Empty; }

        public String DeleteGambleTaskResult() 
        { 
            return String.Empty; 
        }

        public String GambleTaskStatus(String pid) 
        {
            // 1) pid不存在且有结果文件时, 为运行成功
            // 2) pid不存在但没有结果文件时, 为运行失败
            // 3) pid存在但没有结果文件且在未超时范围内, 为正在运行
            // 4) pid存在但有结果文件, 假定运行成功
            // 以上pid在获取时同时要判断执行cmd是否为模型脚本
            String command = String.Format(@"ps aux | grep -i python | grep {0} | grep {1}", gambleScript, pid);
            return String.Empty; 
        }

        public BastionAPI KillGambleTask(String pid) 
        {
            String command = String.Format("kill -9 {0}", pid);
            return this; 
        }

        public String RunGambleTask() 
        {
            String command = String.Format(importSearchEnv + "nohup python {0} && disown;", gambleScript);
            return String.Empty; 
        }

        public BastionAPI UploadGambleScript() { return this; }
        public BastionAPI CreateGambleTaskDirectory() 
        {
            String command = String.Format("mkdir -p {0}", linuxWorkspace);
            return this; 
        }

        public String YellowTaskPID() { return String.Empty; }

        public String GunTaskPID() { return String.Empty; }

        public String PlaneTaskPID() { return String.Empty; }
    }
}
