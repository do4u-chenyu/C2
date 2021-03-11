using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace C2Shell
{
    public class SoftwareUpdate
    {
        private readonly string installPath = Path.Combine(Application.StartupPath, "update", "install");
        private readonly string updatePath = Path.Combine(Application.StartupPath, "update", "setup");
        private readonly string rollbackPath = Path.Combine(Application.StartupPath, "update", "backup");
        private readonly string strPathExe = Path.Combine(Application.StartupPath, "C2.exe");
        private readonly string configFilePath = Path.Combine(Application.StartupPath, "C2.exe.config");




        public string ZipName { get; set; }
        public SoftwareUpdate()
        {
        }


        public bool IsNeedUpdate()
        {

            // update目录是否为空
            bool needUpdate = true;
            try
            {
                string[] files = System.IO.Directory.GetFiles(installPath);
                string pattern = @"software-(\d+\.){2}\d+-\d{8}.zip";
                Regex rgx = new Regex(pattern);
                needUpdate = (files.Length == 1 && rgx.IsMatch(files[0]));
                if (needUpdate)
                {
                    ZipName = files[0];
                }
                return needUpdate;
            }
            catch
            {
                return !needUpdate;
            }
        }

        public bool ExecuteUpdate(string zipName)
        {
            bool success = true;
            if (!zipName.EndsWith(".zip"))
                return !success;
            string scriptPath = Path.Combine(updatePath, "setup.bat");

            // 解压update目录 
            string zipPath = Path.Combine(installPath, zipName);
            string errMsg = Utils.ZipUtil.UnZipFile(zipPath, updatePath);
            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBox.Show("解压失败");
                return !success;
            }
            MessageBox.Show("解压成功");
            // 执行 setup.bat脚本 ，进行文件备份和替换     
            if (ExecuteCmdScript(scriptPath, true))
            {
                MessageBox.Show("执行文件替换成功");
                // 修改配置文件版本号
                string pattern = @"(\d+\.){2}\d+";
                Regex rgx = new Regex(pattern);
                string newVersion = rgx.Match(zipName).ToString();
                Utils.XmlUtil.UpdateVersion(configFilePath, newVersion);
                return success;

            }
            return !success;

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
                MessageBox.Show("执行回滚");
                ExecuteCmdScript(scriptPath, false);
            }
            catch
            { }
        }
        public void Clean()
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

            Process process = new System.Diagnostics.Process();
            try
            {
                process.StartInfo.FileName = strPathExe;
                process.Start();

            }
            catch
            { }

        }
        public bool ExecuteCmdScript(string scriptPath, bool isUpdate)
        {
            bool success = true;
            if (!File.Exists(scriptPath))
                return !success;
            System.Diagnostics.Process process = new System.Diagnostics.Process();
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
                    string line;

                    while (!string.IsNullOrEmpty(line = sr.ReadLine()))
                    {
                        MessageBox.Show("执行命令：" + line);
                        process.StandardInput.WriteLine(line);
                    }
                    process.StandardInput.WriteLine("exit");
                }
                process.StandardInput.Close();
                MessageBox.Show("标准输出" + process.StandardOutput.ReadToEnd());
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    MessageBox.Show("进入process.ExitCode, 更新脚本执行失败");
                    return !success;
                }
            }
            catch
            {
                MessageBox.Show("进入catch, 更新脚本执行失败");
                return !success;
            }
            finally
            {
                if (process != null)
                {
                    process.Dispose();//释放资源
                }
                process.Close();
            }
            return success;
        }

    }
}
