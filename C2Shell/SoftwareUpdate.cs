using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using C2.Utils;

namespace C2Shell
{
    public class SoftwareUpdate
    {

        private SoftwareUpdate()
        { 

        }

        private  void IsNeedUpdate()
        {
            // update目录是否为空

            string packagePath = Path.Combine(Application.StartupPath, "update");
            string[] files = System.IO.Directory.GetFiles(packagePath);
           
            if (files.Length != 1 || !files[0].EndsWith(".zip"))
                return;
            if (!ExecuteUpdate(files[0]))
            {
                Rollback();
            }

        }
        private static bool ExecuteUpdate(string zipPath)
        {
            // 解压update目录

            ZipUtil.UnZipFile(zipPath, Path.GetDirectoryName(zipPath));
            // 更新进度窗体
            // 执行 setup.bat脚本
            return true;
        }
        private static void Rollback()
        {
            // 执行 rollback.bat脚本
        }
        private static void StartCoreProcess()
        {
            string strPathExe = Application.StartupPath + "\\C2.exe";
            Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = strPathExe;
            process.Start();

        }
    }
}
