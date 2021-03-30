using C2.SearchToolkit;
using Renci.SshNet;
using System;
using System.Text.RegularExpressions;

namespace C2.Business.SSH
{
    public class BastionAPI
    {
        private readonly SshClient ssh;
        private readonly TaskInfo task;
        private String GambleScript { get => String.Format("batchquery_db_accountPass_version{0}.py", task.TaskCreateTime); }
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

        private String RunCommand(String command)
        {
            if (ssh.IsConnected)
                return ssh.RunCommand(command).Result;

            return String.Empty;
        }

        // 执行命令且必须成功返回
        private bool SuccessRunCommand(String command)
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

        private bool IsGambleResultFileReady()
        {
            return true;
        }

        private bool IsTaskTimeout()
        {
            return true;
        }

        private bool IsNotSafe(String value)
        {
            // 在服务器上删东西 尽量严格, 尤其不能有"空格/"或"空格/空格"
            return !value.StartsWith("/tmp/iao/search_toolkit/") || Regex.IsMatch(value, @"\s");
        }

        private bool EnterGambleWorkspace()
        {
            String command = String.Format("cd {0}", GambleWorkspace);
            return SuccessRunCommand(command);
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

        public String QueryGambleTaskStatus()
        {
            bool isTimeout = IsTaskTimeout();
            bool isAlive = IsAliveGambleTask();
            bool isGRFReady = IsGambleResultFileReady();

            // 1) pid不存在且有结果文件时, 为运行成功
            if (!isAlive && isGRFReady)
                return "DONE";

            // 2) pid不存在且没有结果文件时, 为运行失败
            if (!isAlive && !isGRFReady)
                return "FAIL";

            // 3) pid存在且没有结果文件且在未超时范围内, 为正在运行
            if (isAlive && !isGRFReady && !isTimeout)
                return "RUNNING";

            // 4) pid存在且没有结果文件且超出运行时间(24 * 4小时), 为超时
            if (isAlive && !isGRFReady && isTimeout)
                return "TIMEOUT";

            // 5) pid存在但有结果文件, 这种情况按道理不应该发生, 暂时假定运行成功
            if (isAlive && isGRFReady)
                return "DONE";

            // 其他情况, 按道理不应该发生, 全部默认为失败
            return "FAIL";
        }

        public String YellowTaskPID() { return String.Empty; }

        public String GunTaskPID() { return String.Empty; }

        public String PlaneTaskPID() { return String.Empty; }
    }
}
