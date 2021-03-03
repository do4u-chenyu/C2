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
        public SoftwareUpdate()
        {

        }


        public bool IsNeedUpdate()
        {
            // update目录是否为空

            string[] files = System.IO.Directory.GetFiles(updatePath);
            bool needUpdate = (files.Length == 1 && files[0].EndsWith(".zip"));
            return needUpdate;

        }
        public  bool ExecuteUpdate(string zipPath)
        {
            bool success = true;
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
            string scriptPath = Path.Combine(Path.GetDirectoryName(zipPath), "setup.bat");
            string errMsg=ZipUtil.UnZipFile(zipPath, Path.GetDirectoryName(zipPath));
            if (!string.IsNullOrEmpty(errMsg))               
                return !success;

            // 执行 setup.bat脚本 ，进行文件备份和替换       
            return ExecuteCmdScript(scriptPath);

        }
        public  void Rollback(string zipPath)
        {
            // 执行 rollback.bat脚本
            string scriptPath = Path.Combine(Path.GetDirectoryName(zipPath), "rollback.bat");
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
            string strPathExe = Path.Combine(Application.StartupPath + "C2.exe");
            Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = strPathExe;
            process.Start();

        }
        private bool ExecuteCmdScript(string scriptPath)
        {
            bool success = true;
            if (!File.Exists(scriptPath))
                return !success;
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.StandardInput.AutoFlush = true;
            p.Start();//启动程序
           
            try
            {

                using (StreamReader sr = new StreamReader(scriptPath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        p.StandardInput.WriteLine(line);
                        Console.WriteLine(line);
                    }
                    p.StandardInput.WriteLine("exit");
                }
            }
            catch 
            {
                return !success;
            }
            
            p.WaitForExit(15000);
            p.Close();
            return success;
        }
    }
}
