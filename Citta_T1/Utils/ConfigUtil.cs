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
    }
}
