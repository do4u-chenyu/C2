using System;
using System.IO;
using System.Text;
using System.Configuration;
using System.ComponentModel;
using System.Diagnostics;

namespace Citta_T1.Utils
{
    class ConfigUtil
    {
        public static string DefaultWorkspaceDirectory = @"C:\FiberHomeIAOModelDocument";
        public static string TryGetAppSettingsByKey(string key, string defaultValue = "")
        {
            string value;
            try
            {
                value = ConfigurationManager.AppSettings[key];
            } 
            catch (ConfigurationErrorsException)
            {
                value = defaultValue;
            }
            return String.IsNullOrEmpty(value) ? defaultValue : value.Trim(); 
        }
        public static bool TrySetAppSettingsByKey(string key, string configValue)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            try 
            {
                // 先清空
                config.AppSettings.Settings.Remove(key);
                config.AppSettings.Settings.Add(key, configValue);
                // 保存
                config.Save(ConfigurationSaveMode.Modified);
                // 刷新内存
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static string TryGetEnvironmentVariable(string key, string defaultValue = "")
        {
            string value = defaultValue;
            try
            {
                value = Environment.GetEnvironmentVariable(key);
            }
            catch
            {
                value = defaultValue;
            }
            return value ?? String.Empty;  // 微软这都弄的啥破新语法糖啊
        }

        // 变量环境变量Path和PYTHONHOME和C盘根目录, 找到第一个可能的Python解释器的路径
        public static string GetDefaultPythonOpenFileDirectory()
        {
            string possiblePythonPath = String.Empty;
            // 找环境变量
            StringBuilder sb = new StringBuilder(TryGetEnvironmentVariable("path").TrimEnd(';'));
            sb.Append(";").Append(TryGetEnvironmentVariable("PYTHONHOME"));
            string[] possiblePaths = sb.ToString().Split(';');
            possiblePythonPath = FindContainsPythonPath(possiblePaths);

            // 找C盘
            if (!String.IsNullOrEmpty(possiblePythonPath))
                return possiblePythonPath;
            possiblePaths = FileUtil.TryListDirectory(@"C:\");
            possiblePythonPath = FindContainsPythonPath(possiblePaths);

            // 找D盘
            if (!String.IsNullOrEmpty(possiblePythonPath))
                return possiblePythonPath;
            possiblePaths = FileUtil.TryListDirectory(@"D:\");
            possiblePythonPath = FindContainsPythonPath(possiblePaths);
            return possiblePythonPath;
        }

        private static string FindContainsPythonPath(string[] possiblePaths)
        {
            string possiblePythonPath = String.Empty;
            foreach (string path in possiblePaths)
            {   // 目录存在;目录里有python.exe
                if (Directory.Exists(path) && File.Exists(Path.Combine(path, "python.exe")))
                {
                    possiblePythonPath = path;
                    break;
                }
            }
            return possiblePythonPath;
        }

        public static bool IsDesignMode()
        {
            bool returnFlag = false;

#if DEBUG
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                returnFlag = true;
            }
            else if (Process.GetCurrentProcess().ProcessName == "devenv")
            {
                returnFlag = true;
            }
#endif

            return returnFlag;
        }
    }
}
