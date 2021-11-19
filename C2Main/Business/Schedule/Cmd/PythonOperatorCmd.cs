using System.Collections.Generic;
using System.IO;
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
            string[] cmdElements=  cmdPython.Split(OpUtil.Blank);

            cmds.Add("cd /d " + path);

            string[] pythonScript = cmdPython.Split(' ');
            if (!File.Exists(@pythonScript[1]))
            {
                cmds.Add("echo " + pythonScript[1] + "脚本不存在");
            }

            if (!IsExecutableCmd(pythonExePaths, cmdPython))
            {
                if (pythonExePaths.Count == 0)
                {
                    cmds.Add("echo 虚拟机不存在");
                    cmds.Add(cmdPython);
                }
                else if (pythonExePaths.Count > 0)
                {
                    // 针对模型市场中Python算子在不同电脑python.exe路径未知,不重新配置python算子就无法正确运行问题
                    cmdElements[0] = pythonExePaths[0];
                    string newCmd = string.Join(OpUtil.StringBlank, cmdElements);
                    cmds.Add(newCmd);
                }
            }
            else
            {
                cmds.Add(cmdPython);
            }
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
    }
}
