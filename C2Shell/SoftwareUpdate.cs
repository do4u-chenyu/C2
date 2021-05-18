﻿using System;
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
        private string webVersion;

        private string zipName;
        public SoftwareUpdate()
        {
            webVersion = string.Empty;
            zipName = string.Empty;
        }


        public bool IsNeedUpdate()
        {
            try
            {
                string[] files = Directory.GetFiles(installPath);
                if (files.Length == 1 && Regex.IsMatch(files[0], zipPattern))
                {
                    zipName = files[0];
                    // 获取新版本号
                    webVersion = Regex.Match(zipName, @"(\d+\.){2}\d+").ToString();
                    //获取当前版本号
                    string currentVersion = Utils.XmlUtil.CurrentVersion(configFilePath);
                    return IsNewVersion(webVersion, currentVersion);
                }
            }
            catch { }
            return false;
        }
        private bool IsNewVersion(string webVersion, string currentVersion)
        {

            try
            {
                return new Version(webVersion) > new Version(currentVersion);
            }
            catch
            {
                return false;
            }

        }
        public bool ExecuteUpdate()
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
                Utils.XmlUtil.UpdateVersion(configFilePath, webVersion);
                MessageBox.Show("C2升级成功，当前版本:" + webVersion);
                return true;
            }
            return false;
        }
        public void Rollback()
        {
            // 执行 rollback.bat脚本
            try
            {
                string scriptPath = Path.Combine(updatePath, "rollback.bat");
                if (File.Exists(scriptPath) && ExecuteCmdScript(scriptPath))
                    MessageBox.Show("回滚成功");
            }

            catch
            {
                MessageBox.Show("回滚失败");
            }
        }
        public void Clear()
        {
            try
            {
                if (Directory.Exists(installPath))
                    Directory.Delete(installPath, true);
                if (Directory.Exists(updatePath))
                    Directory.Delete(updatePath, true);
                if (Directory.Exists(rollbackPath))
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
            // InvalidOperationException：表示当前状态下不允许进行此操作（也就是说存在着允许进行此操作的另一种状态）
            // ObjectDisposedException：表示对象已经 Dispose 过了，不能再使用了
            // Win32Exception：环境错误
            // PlatformNotSupportedException ：表示在此平台下不支持（如果程序跨平台的话）
            catch 
            {
                MessageBox.Show("找不到或无法加载主程序");
            }

        }
        private bool ExecuteCmdScript(string scriptPath)
        {
            bool success = true;

            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe"; //cmd中运行命令
            process.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            process.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            process.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            process.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            process.StartInfo.Verb = "runas";// Admin身份运行
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
                    // MessageBox.Show("更新脚本执行失败,错误码:" + process.ExitCode);
                    return !success;
                }
            }
            catch 
            {
                // MessageBox.Show("更新脚本执行失败:" + e.Message);
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
