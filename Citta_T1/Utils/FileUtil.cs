using System;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Windows.Forms;

namespace Citta_T1.Utils
{
    class FileUtil
    {
        public static void AddPathPower(string pathName, string power)
        {
            string userName = System.Environment.UserName;
            DirectoryInfo dirInfo = new DirectoryInfo(pathName);

            if ((dirInfo.Attributes & FileAttributes.ReadOnly) != 0)
            {
                dirInfo.Attributes = FileAttributes.Normal;
            }

            //取得访问控制列表   
            DirectorySecurity dirsecurity = dirInfo.GetAccessControl();

            switch (power)
            {
                case "FullControl":
                    dirsecurity.AddAccessRule(new FileSystemAccessRule(userName, FileSystemRights.FullControl, InheritanceFlags.ContainerInherit, PropagationFlags.InheritOnly, AccessControlType.Allow));
                    break;
                case "ReadOnly":
                    dirsecurity.AddAccessRule(new FileSystemAccessRule(userName, FileSystemRights.Read, AccessControlType.Allow));
                    break;
                case "Write":
                    dirsecurity.AddAccessRule(new FileSystemAccessRule(userName, FileSystemRights.Write, AccessControlType.Allow));
                    break;
                case "Modify":
                    dirsecurity.AddAccessRule(new FileSystemAccessRule(userName, FileSystemRights.Modify, AccessControlType.Allow));
                    break;
            }
            dirInfo.SetAccessControl(dirsecurity);
        }

        // 实践中发现复制粘贴板有时会出异常
        // 非核心功能,捕捉异常忽略之
        public static bool TryClipboardSetText(string text)
        {
            bool ret = true;
            try { Clipboard.SetText(text); }
            catch { ret = false; }
            return ret;
        }
        public static void ExploreDirectory(string fullFilePath)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = "explorer.exe";  //资源管理器
                processStartInfo.Arguments = "/e,/select," + fullFilePath;
                System.Diagnostics.Process.Start(processStartInfo);
            }
            catch (Exception)
            {
                //某些机器直接打开文档目录会报“拒绝访问”错误，此时换一种打开方式
                FileUtil.AnotherOpenFilePathMethod(fullFilePath);
            }
        }


        private static void AnotherOpenFilePathMethod(string fullFilePath)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = "explorer.exe";  //资源管理器
                processStartInfo.Arguments = System.IO.Path.GetDirectoryName(fullFilePath);
                System.Diagnostics.Process.Start(processStartInfo);
            }
            catch { }; // 非核心功能, Double异常就不用管了
        }

        public static void DeleteDirectory(string directoryPath)
        {
            try
            {
                System.IO.Directory.Delete(directoryPath, true);
            }
            catch
            {
                // 如果无法回滚的话,这个地方只能直接忽略了
            }
        }

        public static bool CreateDirectory(string dicectoryPath)
        {
            bool ret = true;
            try
            {
                Directory.CreateDirectory(dicectoryPath);
            }
            catch { ret = false; }
            return ret;
        }

        public static bool FileMove(string oldFFP, string newFFP)
        {
            bool ret = true;
            try
            {
                File.Move(oldFFP, newFFP);
            }
            catch { ret = false; }
            return ret;
        }

        public static bool DirecotryMove(string oldDirectory, string newDirectory)
        {
            bool ret = true;
            try
            {
                Directory.Move(oldDirectory, newDirectory);
            }
            catch { ret = false; }
            return ret;
        }

        public static string TryGetPathRoot(string path)
        {
            string root;
            try
            {
                root = Path.GetPathRoot(path);
            }
            catch (ArgumentException)
            {
                root = String.Empty;
            }
            return root;
        }

        public static string[] TryListDirectory(string path)
        {
            try
            {
                return System.IO.Directory.GetDirectories(path);
            }
            catch
            {
                return new string[0];
            }
        }
    }
}
