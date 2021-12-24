using System.Collections.Generic;
using System.IO;
using System.Text;
using C2.Model.Widgets;
using C2.Utils;

namespace C2.Business.Schedule.Cmd
{
    class PythonOperatorCmd : OperatorCmd
    {
        public PythonOperatorCmd(Triple triple) : base(triple)
        {
        }
        public PythonOperatorCmd(OperatorWidget operatorWidget) : base(operatorWidget)
        {
        }
        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            string cmdPython = option.GetOption("cmd");
            string path = System.IO.Path.GetDirectoryName(option.GetOption("pyFullPath"));
            List<string> pythonExePaths = GetPythonExePaths();

            string exePath = string.Empty;
            string pyPath = string.Empty;
            string pyParam = string.Empty;
            StringBuilder sb = new StringBuilder();
            //不能直接切分了，exe和py都有可能带空格，变成3个参数
            string[] cmdElements = cmdPython.Split(OpUtil.Blank);
            foreach (string ele in cmdElements)
            {
                if (ele.EndsWith(".exe"))
                {
                    exePath = sb.Append(" " + ele).ToString().TrimStart(OpUtil.Blank);
                    sb.Clear();
                }
                else if (ele.EndsWith(".py"))
                {
                    pyPath = sb.Append(" " + ele).ToString().TrimStart(OpUtil.Blank);
                    sb.Clear();
                }
                else
                    sb.Append(" " + ele);
            }
            pyParam = sb.ToString().TrimStart(OpUtil.Blank);
            exePath = GetRealExePath(pythonExePaths, exePath);


            //判断
            if (!File.Exists(pyPath))
                cmds.Add("echo " + pyPath + "脚本不存在");

            if (!File.Exists(exePath))
                cmds.Add("echo " + exePath + "虚拟机不存在");

            cmds.Add("cd /d " + "\"" + path + "\"");
            //不存在也必须要加上，否则后台认为python脚本运行正确，不会终止
            cmds.Add(string.Format("\"{0}\" \"{1}\" {2}", exePath, pyPath, pyParam));

            return cmds;
        }
        private List<string> GetPythonExePaths()
        {
            string value = ConfigUtil.TryGetAppSettingsByKey("python");
            List<string> paths = new List<string>();
            foreach (string pItem in value.Split(';'))
            {
                string[] oneConfig = pItem.Split('|');
                if (oneConfig.Length != 3)
                    continue;
                string pythonFFP = oneConfig[0].Trim();
                paths.Add(pythonFFP);                
            }
            return paths;
        }
        private bool IsExecutableCmd(List<string> paths, string cmdPython)
        {
  
            if (cmdPython.Split(OpUtil.Blank).Length < 1)
                return false;

            foreach (string path in paths)
            {
                if (cmdPython.Trim().StartsWith(path.Trim()))
                    return true;
            }
            return false;
        }

        private string GetRealExePath(List<string> paths, string exePath)
        {
            foreach (string path in paths)
            {
                if (exePath.Trim().StartsWith(path.Trim()))
                    return exePath;
            }
            return paths.Count == 0 ? string.Empty : paths[0];
        }
    }
}
