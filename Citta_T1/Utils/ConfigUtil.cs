using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    
        
    }
}
