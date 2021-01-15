using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C2.Controls;

namespace C2.Dialogs.IAOLab
{
    public partial class ApkTool : BaseDialog
    {
        private readonly object _objLock = new object();
        
        public delegate void UpdateLog(string log);//声明一个更新主线程日志的委托
        public UpdateLog UpdateLogDelegate;
        public ApkTool()
        {
            InitializeComponent();

        }
        public string ExactApk(string apkpath, string jdkpath)
        {
            // 运行JAR包
            string tmpPath = Path.GetTempPath()+"ApkTool";
            if (!Directory.Exists(tmpPath))
            {
              Directory.CreateDirectory(tmpPath);
            }
            RunLinuxCommandApkTool(GetCmdCommand(apkpath, jdkpath));
            //GetApkInfo();
            DirectoryInfo dir = new DirectoryInfo(apkpath);
            //检索表示当前目录的文件和子目录
            FileSystemInfo[] fsinfos = dir.GetFileSystemInfos();
            //遍历检索的文件和子目录
            foreach (FileSystemInfo fsinfo in fsinfos)
            {
                GetApkInfo(fsinfo.FullName,fsinfo.Name);
                //先不用生成result,读取需要的数据加载到内存，并控件预览框展示

            }
            // 删除临时结果文件
            return null;
        }
        private string GetApkInfo(string apkPath,string apkname)
        {
            Path.GetTempPath();
            






            long size = GetApkSize(apkPath);
            return null;
        }
        private long GetApkSize(string filepath)
        {
            
            using (FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                long size = fileStream.Length;
                size /= (1024 * 1024);
                return size;
            }
        }
        public void ExporResult()
        {
            // 右键导出，生成结果文件到excel
            // python的save方法调用
        }
        public List<string> GetCmdCommand(string apkpath,string jdkpath)
        {
            string setJdkPath = "set path = " + jdkpath + "bin;% path %";
            List<string> cmdList = null;
            cmdList.Add(setJdkPath);
            DirectoryInfo dir = new DirectoryInfo(apkpath);
            //检索表示当前目录的文件和子目录
            FileSystemInfo[] fsinfos = dir.GetFileSystemInfos();
            //遍历检索的文件和子目录
            foreach (FileSystemInfo fsinfo in fsinfos)
            {
                
                string cmdApk = "java - jar apktool_2.4.1.jar d - f" + fsinfo.FullName+ "-o"+ Path.GetTempPath(); 
                
                
                cmdList.Add(cmdApk);
                
            }
            return cmdList;
        }
        public string ReadXmlFile(string filepath,string name)
        {

            return null;
        }
        public string RunLinuxCommandApkTool(List<string> cmds)
        {
            // 补充条件检查, cmds 不能为空
            if (cmds == null || !cmds.Any())
                return "";
            string errorMessage = String.Empty;

            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            //p.StartInfo.Arguments = "/c " + string.Join(";",cmds);
            p.StartInfo.UseShellExecute = false; // 不显示用户界面
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardInput = true;//可以重定向输入  
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;


            try
            {
                if (p.Start())//开始进程  
                {
                    foreach (string cmd in cmds)
                    {
                        UpdateLogDelegate("执行命令: " + cmd);
                        p.StandardInput.WriteLine(cmd);
                    }

                    //多线程下异步读取
                    //p.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);

                    p.BeginErrorReadLine();
                    p.BeginOutputReadLine();

                    p.StandardInput.WriteLine("exit");
                    p.WaitForExit(); //等待进程结束，等待时间为指定的毫秒

                    UpdateLogDelegate("退出码" + p.ExitCode.ToString());
                    if (p.ExitCode != 0)
                    {
                        errorMessage = "执行程序非正常退出，请检查程序后再运行。";
                        UpdateLogDelegate("执行程序非正常退出，请检查程序后再运行。");
                    }

                }
            }
            catch (System.InvalidOperationException)
            {
                //没有关联进程的异常，是由于用户点击终止按钮，导致进程被关闭
                //UpdateLogDelegate("InvalidOperationException: " + ex.Message);
            }
            catch (Exception ex)
            {
                //异常停止的处理方法
                errorMessage = ex.Message;
                UpdateLogDelegate("RunLinuxCommand进程异常: " + ex.Message);
            }
            finally
            {
                if (p != null)
                    p.Dispose();//释放资源
                p.Close();
               
            }
            return errorMessage;
        }
    }
}
