using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace C2.IAOLab.WebEngine.Boss
{
    /// <summary>
    /// 主题类，预配置官方主题
    /// </summary>
    public class Theme
    {
        //public readonly static Theme vintage = new Theme("vintage", "./theme/vintage.js");
        //public readonly static Theme dark = new Theme("dark", "./theme/dark.js");
        //public readonly static Theme macarons = new Theme("macarons", "./theme/macarons.js");
        //public readonly static Theme infographic = new Theme("infographic", "./theme/infographic.js");
        //public readonly static Theme shine = new Theme("shine", "./theme/shine.js");
        //public readonly static Theme roma = new Theme("roma", "./theme/roma.js");
        public readonly static Theme phx = new Theme("phx", Path.Combine(Application.StartupPath, "IAOLab\\WebEngine\\JS", "phx.js"));

        public Theme(string name, string scriptPath)
        {
            Name = name;
            ScriptPath = scriptPath;
        }
        /// <summary>
        /// 主题名称
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 在html中引用的脚本路径
        /// </summary>
        public string ScriptPath { get; private set; }
    }
}
