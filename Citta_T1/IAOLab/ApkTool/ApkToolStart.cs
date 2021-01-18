using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using C2.Controls;

namespace C2.IAOLab.ApkToolStart
{
    public class ApkToolStart
    {
        public delegate void UpdateLog(string log);//声明一个更新主线程日志的委托
        public UpdateLog UpdateLogDelegate;
        List<string> Result = null;
        private static ApkToolStart instance;
        public static ApkToolStart GetInstance()
        {
            if (instance == null)
                instance = new ApkToolStart();
            return instance;
        }
        public List<string> ExactApk(string apkpath, string jdkpath)
        {
            // 运行JAR包
            string tmpPath = Path.GetTempPath() + "ApkTool";
            if (!Directory.Exists(tmpPath))
            {
                Directory.CreateDirectory(tmpPath);
            }
            RunLinuxCommandApkTool(GetCmdCommand(apkpath, jdkpath));

            DirectoryInfo dir = new DirectoryInfo(apkpath);
            //检索表示当前目录的文件和子目录
            FileSystemInfo[] fsinfos = dir.GetFileSystemInfos();
            //遍历检索的文件和子目录
            foreach (FileSystemInfo fsinfo in fsinfos)
            {
                string apkInfo = GetApkInfo(fsinfo.FullName, fsinfo.Name);
                Result.Add(apkInfo);
                //先不用生成result,读取需要的数据加载到内存，并控件预览框展示
            }
            // 删除临时结果文件
            return Result;
        }
        private string GetApkInfo(string apkPath, string apkName)
        {
            apkName = apkName.Replace(".apk", "\t").Split('\t')[0];
            string apkToolPath = Path.GetTempPath() + @"\ApkTool\" + apkName;
            string packageName = GetPackage(apkToolPath);
            string mainActivity = GetActivity(apkToolPath);
            string apkTrueName = GetApkName(apkToolPath);
            string apkIconFullName = GetIcon(apkToolPath);
            long size = GetApkSize(apkPath);
            return string.Format("{0}{1}{2}{3} {4}{5}", apkIconFullName, "\t", apkName, "\t", apkTrueName, "\t", packageName, "\t", mainActivity, "\t", size.ToString());
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

        public List<string> GetCmdCommand(string apkpath, string jdkpath)
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

                string cmdApk = "java - jar apktool_2.4.1.jar d - f" + fsinfo.FullName + "-o" + Path.GetTempPath() + "ApkTool";

                cmdList.Add(cmdApk);

            }
            return cmdList;
        }
        public string GetIcon(string filePath)
        {
            string iconPath = filePath + @"\res";
            filePath += @"\AndroidManifest.xml";
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filePath);
            XmlNode rootNode = xDoc.SelectSingleNode("manifest");
            XmlNode a = rootNode.SelectSingleNode("//application");
            string strValue = a.Attributes["android:icon"].Value.Split('/')[1];
            DirectoryInfo dir = new DirectoryInfo(iconPath);
            //检索表示当前目录的文件和子目录
            FileSystemInfo[] fsinfos = dir.GetFileSystemInfos();
            //遍历检索的文件和子目录
            //int i = 0;
            string relICon = " ";
            foreach (FileSystemInfo fsinfo in fsinfos)
            {
                using (FileStream fileStream = new FileStream(fsinfo.FullName, FileMode.Open, FileAccess.Read))
                {
                    long size = 0;
                    if (fsinfo.Name.Contains(strValue) && fileStream.Length > size)
                    {
                        size = fileStream.Length;
                        relICon = fsinfo.FullName;
                    }
                }
            }
            return relICon;
        }
        public string GetPackage(string filepath)
        {
            filepath += @"\AndroidManifest.xml";
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filepath);
            XmlNode rootNode = xDoc.SelectSingleNode("manifest");
            string strValue = rootNode.Attributes["package"].Value;
            return strValue;
        }
        public string GetApkName(string filepath)
        {
            string filepath1 = filepath + @"\AndroidManifest.xml";
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filepath1);
            XmlNode rootNode0 = xDoc.SelectSingleNode("manifest");
            XmlNode a = rootNode0.SelectSingleNode("//application");
            string strValue = a.Attributes["android:label"].Value;
            if (strValue.Contains("@"))
            {
                strValue = strValue.Split('/')[1];
                filepath += @"\apk_out\res\values\strings.xml";
                XmlDocument xDoc1 = new XmlDocument();
                xDoc.Load(filepath);
                XmlNode rootNode1 = xDoc1.SelectSingleNode("resources");
                XmlNodeList namenodes = rootNode1.SelectNodes("//string");
                foreach (XmlNode namenode in namenodes)
                {
                    if ((namenode as XmlElement).GetAttribute("name") == strValue)
                    {
                        return (namenode as XmlElement).InnerText;
                    }
                    return " ";
                }
                return " ";
            }
            else
            {
                return strValue;
            }
        }
        public string GetActivity(string filepath)
        {
            filepath += @"\AndroidManifest.xml";
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filepath);
            XmlNode rootNode = xDoc.SelectSingleNode("manifest");
            XmlNodeList a = rootNode.SelectNodes("//activity");
            foreach (XmlNode node in a)
            {
                if (node.Attributes["android:name"].Value.Contains(".MainActivity"))
                {
                    return node.Attributes["android:name"].Value;
                }
                return "未找到主函数";
            }
            return "未找到主函数";
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
