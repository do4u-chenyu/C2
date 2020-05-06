using System;
using System.Text;
using System.Configuration;

namespace Citta_T1.Utils
{
    class ConfigUtil
    {
        public static string DefaultWorkspaceDirectory = @"C:\cittaModelDocument";
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

        // 变量环境变量Path和PYTHONHOME, 找到第一个可能的Python解释器的路径
        public static string GetDefaultPythonOpenFileDirectory()
        {
            string possiblePythonPath = String.Empty;
            StringBuilder sb = new StringBuilder(TryGetEnvironmentVariable("path").TrimEnd(';'));
            sb.Append(";").Append(TryGetEnvironmentVariable("PYTHONHOME"));
            string[] possiblePaths = sb.ToString().Split(';');
            foreach (string path in possiblePaths)
            {
                if (path.ToLower().Contains("python"))
                {
                    possiblePythonPath = path;
                    break;
                }
            }
            return possiblePythonPath;
        }
    }
}
