using C2Shell.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace C2Shell
{
    public class SoftwareUpdate
    {
        private readonly string updatePath = Path.Combine(Application.StartupPath, "update");
        private readonly string rollbackPath = Path.Combine(Application.StartupPath, "backup");
        private readonly string strPathExe = Path.Combine(Application.StartupPath, "C2.exe");
        private readonly string configFilePath = Path.Combine(Application.StartupPath, "C2.exe.config");
        private readonly string errorInfo = "更新失败,正在回滚更改";
        private UpdateProgressBar progress;

        public string ZipName  {get;set; }
        public SoftwareUpdate()
        {
            this.progress = new UpdateProgressBar();
        }


        public bool IsNeedUpdate()
        {
           
            // update目录是否为空
            bool needUpdate = true; 
            try
            {
                string[] files = System.IO.Directory.GetFiles(updatePath);
                needUpdate = (files.Length == 1 && files[0].EndsWith(".zip"));
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
        public  bool ExecuteUpdate(string zipName)
        {
            bool success = true;
            if (!zipName.EndsWith(".zip"))
                return !success;
            string scriptPath = Path.Combine(updatePath, "setup.bat");
          
            progress.Show();
            // 创建文件备份路径
            try
            {
               
                Directory.CreateDirectory(rollbackPath);
                Utils.FileUtil.AddPathPower(rollbackPath);
            }
            catch
            {
                progress.Status = errorInfo;
                return !success;
            }

            // 解压update目录 
            progress.SpeedValue = 10; // 进度
            string zipPath = Path.Combine(updatePath, zipName);
            string errMsg = Utils.ZipUtil.UnZipFile(zipPath, updatePath);
            if (!string.IsNullOrEmpty(errMsg))
            {
                progress.Status = errorInfo;
                return !success;
            }
            progress.SpeedValue = 20; // 进度
            // 执行 setup.bat脚本 ，进行文件备份和替换     
            if (ExecuteCmdScript(scriptPath, true))
            {
                // 修改配置文件版本号
                string newVersion = Path.GetFileNameWithoutExtension(zipName);
                Utils.XmlUtil.UpdateVersion(configFilePath, newVersion);
                progress.Status = "更新成功";
                return success;
             
            }
            progress.Status = errorInfo;
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
                ExecuteCmdScript(scriptPath,false);
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
        public  void StartCoreProcess()
        {
            
            Process process = new System.Diagnostics.Process();
            try
            {
                process.StartInfo.FileName = strPathExe;
                process.Start();
                process.WaitForExit();           
            }
            catch
            { }
            finally
            {
                if (process != null)
                    process.Dispose();//释放资源
                process.Close();
            }


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
                        process.StandardInput.WriteLine(line);
                       
                        if (isUpdate && this.progress.SpeedValue != 98)
                            this.progress.SpeedValue += 1; // 脚本超过68条命令，剩下2%就一直等待剩下所有命令完成
                        if(!isUpdate && this.progress.SpeedValue != 2)
                            this.progress.SpeedValue -= 1; 
                    }
                    process.StandardInput.WriteLine("exit");
                    if (isUpdate)
                        this.progress.SpeedValue = 100;//所有命令成功执行，进度则100%
                    else
                        this.progress.SpeedValue = 0;//回滚完成，进度则0%
                }
                process.WaitForExit(); 
                if (process.ExitCode != 0)
                {
                    return !success;
                }
            }
            catch
            {
                return !success;
            }
            finally
            {
                if (process != null)
                    process.Dispose();//释放资源
                process.Close();
            }        
            return success;
        }
       
 }
}
