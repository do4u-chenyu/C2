using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C2.Utils;

namespace C2Shell
{
    public class SoftwareUpdate
    {
        private readonly string updatePath = Path.Combine(Application.StartupPath, "update");
        private readonly string rollbackPath = Path.Combine(Application.StartupPath, "rollback");
        private readonly string strPathExe = Path.Combine(Application.StartupPath, "C2.exe");

        //private static string tmp = @"D:\work\C2\Citta_T1\bin\Debug";
        //private readonly string updatePath = Path.Combine(tmp, "update");
        //private readonly string rollbackPath = Path.Combine(tmp, "rollback");
        //private readonly string strPathExe = Path.Combine(tmp, "C2.exe");

        public string ZipName  {get;set; }
        public SoftwareUpdate()
        {

        }


        public bool IsNeedUpdate()
        {
            // update目录是否为空

            string[] files = System.IO.Directory.GetFiles(updatePath);
            bool needUpdate = (files.Length == 1 && files[0].EndsWith(".zip"));
            if(needUpdate)
            {
                ZipName = files[0];
            }
            return needUpdate;

        }
        public  bool ExecuteUpdate(string zipName)
        {
            bool success = true;
            string scriptPath = Path.Combine(updatePath, "setup.bat");
            // 创建文件备份路径
            try 
            {
                Directory.CreateDirectory(rollbackPath);
            }
            catch
            {
                return !success;
            }

            // 解压update目录            
            string zipPath = Path.Combine(updatePath, zipName);
            string errMsg = ZipUtil.UnZipFile(zipPath, updatePath);
            if (!string.IsNullOrEmpty(errMsg))
                return !success;

            // 执行 setup.bat脚本 ，进行文件备份和替换       
            return ExecuteCmdScript(scriptPath);

        }
        public void Rollback()
        {
            // 执行 rollback.bat脚本
            if (!Directory.Exists(rollbackPath)
                || (Directory.GetDirectories(rollbackPath).Length > 0
                && Directory.GetFiles(rollbackPath).Length > 0))
            {
                return;
            }
            string scriptPath = Path.Combine(updatePath, "rollback.bat");
            ExecuteCmdScript(scriptPath);
        }
        public void Clean()
        {
            try 
            {
                Directory.Delete(updatePath, true);
                Directory.Delete(rollbackPath, true);
            }
            catch
            {
            }
        }
        public  void StartCoreProcess()
        {
            
            Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = strPathExe;
            process.Start();
            process.WaitForExit();
            if (process != null)
                process.Dispose();//释放资源
            process.Close();
        }
        private bool ExecuteCmdScript(string scriptPath)
        {
            bool success = true;
            if (!File.Exists(scriptPath))
                return !success;
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            process.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            process.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            process.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            process.StartInfo.CreateNoWindow = true;//不显示程序窗口
            process.Start();//启动程序
           
            try
            {

                using (StreamReader sr = new StreamReader(scriptPath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        process.StandardInput.WriteLine(line);
                        Console.WriteLine(line);
                    }
                    process.StandardInput.WriteLine("exit");
                }
            }
            catch 
            {
                return !success;
            }
            process.WaitForExit(60000); //等待进程结束，等待时间为指定的毫秒
            if (process.ExitCode != 0)
            {
                return !success;
            }
            if (process != null)
                process.Dispose();//释放资源
            process.Close();
            return success;
        }
    }
}
