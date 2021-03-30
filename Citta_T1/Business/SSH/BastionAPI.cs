using C2.SearchToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using System.Text.RegularExpressions;

namespace C2.Business.SSH
{
    public class BastionAPI
    {
        private SshClient ssh;
        private TaskInfo task;

        private String GambleScript = "batchquery_db_accountPass_version20210324.py";
       

        // {workspace}/pid_taskcreatetime
        private String GambleWorkspace { get => String.Format("{0}/{1}_{2}", task.RemoteWorkspace, task.PID, task.TaskCreateTime); }
        public BastionAPI(TaskInfo task) 
        {
            this.task = task;
            this.ssh = new SshClient(new PasswordConnectionInfo("114.55.248.85", "root", "aliyun.123")); 
        }

        public void Close()
        {
            if (ssh != null && ssh.IsConnected)
                ssh.Disconnect();
        }

        public BastionAPI Login() 
        {
            // 这里通过抛出异常来报错
            ssh.Connect();
            return this; 
        }

        public String RunCommand(String command)
        {
            if (ssh.IsConnected)
                return ssh.RunCommand(command).Result;

            return String.Empty;
        }

        // 执行命令且必须成功返回
        public bool SuccessRunCommand(String command)
        {
            if (ssh.IsConnected)
                return ssh.RunCommand(command).ExitStatus == 0;
            return false;
        }

        private bool ImportSearchEnv()
        {
            String command = @". /home/search/search_profile;";
            return SuccessRunCommand(command);
        }

        public String GambleTaskPID () 
        {
            String command = String.Format(@"ps aux | grep -i python | grep {0} | awk {{print $2}}", GambleScript);
            String result = RunCommand(command);
            return Regex.IsMatch(result, @"^\d+$") ? result : String.Empty;
        }

        public String DownloadGambleTaskResult() { return String.Empty; }

        public BastionAPI DeleteGambleTask() 
        {
            if (IsNotSafe(GambleWorkspace))
                return this;
            // 删除 临时目录
            RunCommand(String.Format("rm -rf {0};", GambleWorkspace));  
            // 删除 留存的进程
            if (IsAliveGambleTask())
                KillGambleTask();

            return this; 
        }

        private bool IsAliveGambleTask()
        {
            String result = RunCommand(String.Format("ps -q {0} -o cmd | grep {1}", task.PID, GambleScript));
            return result.Contains(GambleScript);
        }

        private bool IsNotSafe(String value)
        {
            // 在服务器上删东西 尽量严格
            return !value.StartsWith("/tmp/iao/search_toolkit/") || Regex.IsMatch(value, @"\s");
        }

        private bool EnterGambleWorkspace()
        {
            String command = String.Format("cd {0}", GambleWorkspace);
            return SuccessRunCommand(command);
        }

        public String GambleTaskStatus(String pid) 
        {
            // 1) pid不存在且有结果文件时, 为运行成功
            // 2) pid不存在但没有结果文件时, 为运行失败
            // 3) pid存在但没有结果文件且在未超时范围内, 为正在运行
            // 4) pid存在但有结果文件, 假定运行成功
            // 以上pid在获取时同时要判断执行cmd是否为模型脚本
            String command = String.Format(@"ps aux | grep -i python | grep {0} | grep {1}", GambleScript, pid);
            return String.Empty; 
        }

        public BastionAPI KillGambleTask() 
        {
            String command = String.Format("kill -9 {0}", task.PID);
            RunCommand(command);
            return this; 
        }

        public String RunGambleTask() 
        {
            bool succ = EnterGambleWorkspace() && ImportSearchEnv();
            if (!succ)
                return String.Empty;

            String command = String.Format("python {0} && disown;", GambleScript);
            String ret = RunCommand(command);
            Match match = Regex.Match(ret, @"\[\d+\]\s*(\d\d*)");
            if (match.Success && match.Groups[1].Success)
                return match.Groups[1].Value;
            return String.Empty;
        }

        public BastionAPI UploadGambleScript() { return this; }
        public BastionAPI CreateGambleTaskDirectory() 
        {
            String command = String.Format("mkdir -p {0}", GambleWorkspace);
            RunCommand(command);
            return this; 
        }

        public String YellowTaskPID() { return String.Empty; }

        public String GunTaskPID() { return String.Empty; }

        public String PlaneTaskPID() { return String.Empty; }
    }
}
