using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.WebShellTool
{
    public class WebShellVersionSetting
    {

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        private Dictionary<string, string> versionPathDict;

        public string SPL;
        public string SPR;
        public string CODE;
        public string ACTION;
        public string PARAM1;
        public string PARAM2;
        public string PARAM3;
        public string PHP_BASE64;
        public string PHP_MAKE;
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
            string webShellFilePath = Path.Combine(Application.StartupPath, "Resources", "WebShellConfig");
            versionPathDict = new Dictionary<string, string>
            {
                { "中国菜刀16", Path.Combine(webShellFilePath, "Cknife16_Config.ini") },
                { "中国菜刀11", Path.Combine(webShellFilePath, "Cknife11_Config.ini") }
            };
        }

        public void LoadSetting(string version)
        {
            versionPathDict.TryGetValue(version, out string path);

            this.SPL = Read(version, "SPL", path);
            this.SPR = Read(version, "SPR", path);
            this.CODE = Read(version, "CODE", path);
            this.ACTION = Read(version, "ACTION", path);
            this.PARAM1 = Read(version, "PARAM1", path);
            this.PARAM2 = Read(version, "PARAM2", path);
            this.PARAM3 = Read(version, "PARAM3", path);
            this.PHP_BASE64 = Read(version, "PHP_BASE64", path);
            this.PHP_MAKE = Read(version, "PHP_MAKE", path);
            this.PHP_INDEX = Read(version, "PHP_INDEX", path);
            this.PHP_READDICT = Read(version, "PHP_READDICT", path);
            this.PHP_READFILE = Read(version, "PHP_READFILE", path);
            this.PHP_SAVEFILE = Read(version, "PHP_SAVEFILE", path);
            this.PHP_DELETE = Read(version, "PHP_DELETE", path);
            this.PHP_RENAME = Read(version, "PHP_RENAME", path);
            this.PHP_RETIME = Read(version, "PHP_RETIME", path);
            this.PHP_NEWDICT = Read(version, "PHP_NEWDICT", path);
            this.PHP_UPLOAD = Read(version, "PHP_UPLOAD", path);
            this.PHP_DOWNLOAD = Read(version, "PHP_DOWNLOAD", path);
            this.PHP_SHELL = Read(version, "PHP_SHELL", path);
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
            StringBuilder sb = new StringBuilder(1024);
            GetPrivateProfileString(section, key, "", sb, 1024, filePath);
            return sb.ToString();
        }
    }
}
