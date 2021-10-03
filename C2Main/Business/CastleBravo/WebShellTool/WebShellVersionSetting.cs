using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.WebShellTool
{
    public class WebShellVersionSetting
    {

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        private static readonly string WebShellFilePath = Path.Combine(Application.StartupPath, "Resources", "WebShellConfig");
        private static readonly Dictionary<string, string> VersionPathDict = new Dictionary<string, string>
        {
            { "中国菜刀16", Path.Combine(WebShellFilePath, "Cknife16_Config.ini") },
            { "中国菜刀11", Path.Combine(WebShellFilePath, "Cknife11_Config.ini") }
        };

        public string SPL;
        public string SPR;
        public string CODE;
        public string ACTION;
        public string PARAM1;
        public string PARAM2;
        public string PARAM3;
        public string PHP_BASE64;
        public string PHP_MAKE;
        public string PHP_INFO;
        public string PHP_INDEX;
        public string PHP_READDICT;
        public string PHP_READFILE;
        public string PHP_SAVEFILE;
        public string PHP_DELETE;
        public string PHP_RENAME;
        public string PHP_RETIME;
        public string PHP_NEWDICT;
        public string PHP_UPLOAD;
        public string PHP_DOWNLOAD;
        public string PHP_SHELL;

        public WebShellVersionSetting()
        {
        }

        public static WebShellVersionSetting LoadSetting(string version)
        {
            string path = VersionPathDict[version];
            return new WebShellVersionSetting() {
                SPL = Read(version, "SPL", path),
                SPR = Read(version, "SPR", path),
                CODE = Read(version, "CODE", path),
                ACTION = Read(version, "ACTION", path),
                PARAM1 = Read(version, "PARAM1", path),
                PARAM2 = Read(version, "PARAM2", path),
                PARAM3 = Read(version, "PARAM3", path),
                PHP_BASE64 = Read(version, "PHP_BASE64", path),
                PHP_MAKE = Read(version, "PHP_MAKE", path),
                PHP_INFO = Read(version, "PHP_INFO", path),
                PHP_INDEX = Read(version, "PHP_INDEX", path),
                PHP_READDICT = Read(version, "PHP_READDICT", path),
                PHP_READFILE = Read(version, "PHP_READFILE", path),
                PHP_SAVEFILE = Read(version, "PHP_SAVEFILE", path),
                PHP_DELETE = Read(version, "PHP_DELETE", path),
                PHP_RENAME = Read(version, "PHP_RENAME", path),
                PHP_RETIME = Read(version, "PHP_RETIME", path),
                PHP_NEWDICT = Read(version, "PHP_NEWDICT", path),
                PHP_UPLOAD = Read(version, "PHP_UPLOAD", path),
                PHP_DOWNLOAD = Read(version, "PHP_DOWNLOAD", path),
                PHP_SHELL = Read(version, "PHP_SHELL", path),
            };
        }

        /// <summary>
        /// 读取INI文件值
        /// </summary>
        /// <param name="section">节点名</param>
        /// <param name="key">键</param>
        /// <param name="filePath">INI文件完整路径</param>
        /// <returns>读取的值</returns>
        public static string Read(string section, string key, string filePath)
        {
            int nSize = 1024 * 4;
            StringBuilder sb = new StringBuilder(nSize);
            GetPrivateProfileString(section, key, String.Empty, sb, nSize, filePath);
            return sb.ToString();
        }
    }
}
