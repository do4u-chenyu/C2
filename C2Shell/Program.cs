using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2Shell
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==============ok========");
            IsNeedUpdate();

            StartCoreProcess();

        }
        private static void IsNeedUpdate()
        {
            // update路径是否为空
            //  string packagePath = Path.Combine(Application.StartupPath, "update");
            string packagePath = string.Empty;
            if (Directory.Exists(packagePath))
                return;
            if (!ExecuteUpdate())
            {
                Rollback();
            }

        }
        private static bool ExecuteUpdate()
        {
            // 解压update目录
            // 更新进度窗体
            // 执行 setup.bat脚本
            return true;
        }
        private static void  Rollback()
        {
            // 执行 rollback.bat脚本
        }
        private static void StartCoreProcess()
        {
            //string strPathExe = Environment.CurrentDirectory + "\\FaceRecognition" + "\\IDFaceDemo.exe";
            //Process process = new System.Diagnostics.Process();
            //process.StartInfo.FileName = strPathExe;

            //process.Start();

        }
    }
}
