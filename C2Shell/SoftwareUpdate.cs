using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace C2Shell
{
    public class SoftwareUpdate
    {
        private readonly string installPath = Path.Combine(Application.StartupPath, "update", "install");
        private readonly string updatePath = Path.Combine(Application.StartupPath, "update", "setup");
        private readonly string rollbackPath = Path.Combine(Application.StartupPath, "update", "backup");
        private readonly string C2Path = Path.Combine(Application.StartupPath, "C2.exe");
        private readonly string configFilePath = Path.Combine(Application.StartupPath, "C2.exe.config");
        private readonly string zipPattern = @"software-(\d+\.){2}\d+-\d{8}.zip";

        public string ZipName { get; set; }
        public SoftwareUpdate()
        {
        }


        public bool IsNeedUpdate()
        {
            try
            {
                string[] files = Directory.GetFiles(installPath);
                if (files.Length == 1 && Regex.IsMatch(files[0], zipPattern))
                {
                    ZipName = files[0];
                    return true;
                }
            }
            catch { }
            return false;
        }

        public bool ExecuteUpdate(string zipName)
        {
            string zipPath = Path.Combine(installPath, zipName);
            string scriptPath = Path.Combine(updatePath, "setup.bat");

            // 解压update目录 
            string errMsg = Utils.ZipUtil.UnZipFile(zipPath, updatePath);
            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBox.Show("安装包解压失败");
                return false;
            }

            // 执行 setup.bat脚本 ，进行文件备份和替换     
            if (File.Exists(scriptPath) && ExecuteCmdScript(scriptPath))
            {
                // 修改配置文件版本号
                string newVersion = Regex.Match(zipName, @"^(\d+\.){2}\d+").ToString();
                Utils.XmlUtil.UpdateVersion(configFilePath, newVersion);
                MessageBox.Show("C2升级成功，当前版本:" + newVersion);
                return true;
            }
            return false;
        }
        public void Rollback()
        {
            // 执行 rollback.bat脚本
            try
            {
                if (!Directory.Exists(rollbackPath)
                    || (Directory.GetDirectories(rollbackPath).Length > 0
                    && Directory.GetFiles(rollbackPath).Length > 0))
                {
                    return;
                }
                string scriptPath = Path.Combine(updatePath, "rollback.bat");
                if (File.Exists(scriptPath) && ExecuteCmdScript(scriptPath))
                    MessageBox.Show("回滚成功");
            }
            catch
            { }
        }
        public void Clear()
        {
            try
            {
                Directory.Delete(updatePath, true);
                Directory.Delete(rollbackPath, true);
            }
            catch
            { }
        }
        public void StartCoreProcess()
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = C2Path;
                process.Start();
            }
            catch
            { }

        }
        private bool ExecuteCmdScript(string scriptPath)
        {
            bool success = true;

            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = @"C:\Windows\cmd.exe";
            process.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            process.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            process.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            process.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            process.StartInfo.Verb = "runas";
            process.StartInfo.CreateNoWindow = true;//不显示程序窗口

            try
            {
                process.Start();//启动程序
                using (StreamReader sr = new StreamReader(scriptPath))
                {
                    process.StandardInput.WriteLine(sr.ReadToEnd());
                    process.StandardInput.WriteLine("exit");
                    process.StandardInput.Flush();
                }

                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    MessageBox.Show("更新脚本执行失败,错误码:" + process.ExitCode);
                    return !success;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("更新脚本执行失败:" + e.Message);
                return !success;
            }
            finally
            {
                if (process != null)
                    process.Dispose();//释放资源
            }
            return success;
        }

    }
}
