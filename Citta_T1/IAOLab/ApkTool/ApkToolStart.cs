using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace C2.IAOLab.ApkToolStart
{
    public class ApkToolStart
    {
        public delegate void UpdateLog(string log);//声明一个更新主线程日志的委托

        private List<List<string>> result;
        private static ApkToolStart instance;
        public static ApkToolStart GetInstance()  // 单例模式
        {
            if (instance == null)
                instance = new ApkToolStart();
            return instance;
        }
        public ApkToolStart()
        {
            result = new List<List<string>>();
        }
        public List<List<string>> ExtractApk(string apkPath, string jdkPath)
        {
            string tmpPath = Path.Combine(Path.GetTempPath(), "ApkTool");
            if (Directory.Exists(tmpPath))
            {
                // 有坑
                Utils.FileUtil.DeleteDirectory(tmpPath);
                
            }
            Utils.FileUtil.CreateDirectory(tmpPath);

            RunLinuxCommandApkTool(GetCommand(apkPath, jdkPath));

            DirectoryInfo dir = new DirectoryInfo(apkPath);
            //检索表示当前目录的文件和子目录
            FileSystemInfo[] fsInfos = dir.GetFileSystemInfos();
            //遍历检索的文件和子目录
            foreach (FileSystemInfo fsInfo in fsInfos)
            {
                result.Add(GetApkInfo(fsInfo.FullName, fsInfo.Name));
                //先不用生成result,读取需要的数据加载到内存，并控件预览框展示
            }
            return result;
            
        }
        private List<string> GetApkInfo(string apkPath, string apkName)
        {
            apkName = apkName.Replace(".apk", String.Empty);
            
            string apkToolPath = Path.GetTempPath() + @"\ApkTool\" + apkName;
            return  new List<string>() { GetIcon(apkToolPath),
                                         apkName,
                                         GetApkName(apkToolPath),
                                         GetPackageName(apkToolPath),
                                         GetActivity(apkToolPath),
                                         GetApkSize(apkPath) };

        }
        private String GetApkSize(string filepath)
        {
            using (FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                return fileStream.Length / (1024 * 1024) + "M";
            }
        }
        public void ExportResult()
        {

            // 右键导出，生成结果文件到excel
            // python的save方法调用
        }

        public List<string> GetCommand(string apkPath, string jdkPath)
        {
            List<string> cmdList = new List<string>();
            //if (jdkpath != null) 
            //{ 
            //    string setJdkPath = "set path = " + jdkpath + "bin;% path %";
            //    cmdList.Add(setJdkPath);
            //}
            DirectoryInfo dir = new DirectoryInfo(apkPath);
            //检索表示当前目录的文件和子目录
            FileSystemInfo[] fsInfos = dir.GetFileSystemInfos();
            //遍历检索的文件和子目录
            foreach (FileSystemInfo fsInfo in fsInfos)
            {
                string apkToolPath = Application.StartupPath + @"sbin\apktool_2.3.0.jar"; 
                //string apkToolPath = @"D:\work\C2\C2\ThirdPartyLibrary\ApkTool.2.3.0\apktool_2.3.0.jar";
                //string cmdApk = @"java -jar "+ apkToolPath+ " d - f " + fsinfo.FullName + " -o " + Path.GetTempPath() + @"ApkTool\"+fsinfo.Name.Replace(".apk","");
                string cmdApk = String.Format(@"java -jar {0} d -f {1} -o {2}\ApkTool\{3}",
                                                                                            apkToolPath, 
                                                                                            fsInfo.FullName, 
                                                                                            Path.GetTempPath(), 
                                                                                            fsInfo.Name.Replace(".apk", String.Empty));
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
            string strValue;
            if (a.Attributes["android:icon"].Value.Split('/') != null) 
            { 
                 strValue = a.Attributes["android:icon"].Value.Split('/')[1]; 
            }
            else
            {
                return "未找到图标";
            }
            DirectoryInfo dir = new DirectoryInfo(iconPath);
            //检索表示当前目录的文件和子目录
            FileSystemInfo[] fsPathInfos = dir.GetFileSystemInfos();
            //遍历检索的文件和子目录
           
            string relIcon = "未找到图标";
            foreach (FileSystemInfo fsPath in fsPathInfos)
            {
                long size = 0;
                DirectoryInfo curDir = new DirectoryInfo(fsPath.FullName);
                //检索表示当前目录的文件和子目录
                FileSystemInfo[] fsInfos = curDir.GetFileSystemInfos();
                foreach (FileSystemInfo fsInfo in fsInfos) 
                {
                    FileInfo f = new FileInfo(fsInfo.FullName);
                    if (fsInfo.Name.Contains(strValue) && f.Length > size)
                    {
                        size = f.Length;
                        relIcon = fsInfo.FullName;
                    }
                }
            }
            return relIcon;
        }
        public string GetPackageName(string filePath)
        {
            filePath += @"\AndroidManifest.xml";
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filePath);
            XmlNode rootNode = xDoc.SelectSingleNode("manifest");
            return rootNode.Attributes["package"].Value;
        }
        public string GetApkName(string filePath)
        {
            string mainfestFilePath = filePath + @"\AndroidManifest.xml";
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(mainfestFilePath);
            XmlNode rootNode0 = xDoc.SelectSingleNode("manifest");
            XmlNode a = rootNode0.SelectSingleNode("//application");
            string labelName = a.Attributes["android:label"].Value;
            if (labelName.Contains("@"))
            {
                if (labelName.Split('/')[1] != null)
                labelName = labelName.Split('/')[1];
                filePath += @"\res\values\strings.xml";
                XmlDocument xDoc1 = new XmlDocument();
                xDoc1.Load(filePath);
                XmlNode rootNode1 = xDoc1.SelectSingleNode("resources");
                XmlNodeList nameNodes = rootNode1.SelectNodes("string");
                foreach (XmlNode nameNode in nameNodes)
                {
                    if ((nameNode as XmlElement).GetAttribute("name") == labelName)
                    {
                        return (nameNode as XmlElement).InnerText;
                    }
                }
                return "未找到软件名";
            }
            else
            {
                return labelName;
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
                if (node.Attributes["android:name"].Value.Contains("Activity"))
                {
                    return node.Attributes["android:name"].Value;
                }
                
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
                        
                        p.StandardInput.WriteLine(cmd);
                    }

                    //多线程下异步读取
                    //p.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);

                    p.BeginErrorReadLine();
                    p.BeginOutputReadLine();

                    p.StandardInput.WriteLine("exit");
                    p.WaitForExit(); //等待进程结束，等待时间为指定的毫秒

                    
                    if (p.ExitCode != 0)
                    {
                        errorMessage = "执行程序非正常退出，请检查程序后再运行。";
                        
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
