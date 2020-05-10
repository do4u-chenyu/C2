using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Citta_T1.IAOLab.PythonOP
{
    class PythonInterpreterInfo
    {
        private string pythonFFP;    // Python解释器全路径
        private string pythonAlias;  // 别名
        private bool chosenDefault;  // 被选中为Python算子默认采用的
        private string pythonVersion;// python版本

        private static readonly Regex PythonVersionRegex = new Regex(@"^Python\s*(\d{1,2}\.\d{1,2}\.\d+)$", RegexOptions.IgnoreCase|RegexOptions.Singleline|RegexOptions.Compiled);

        public PythonInterpreterInfo(string pythonFFP, string alias, bool chosen)
        {
            this.PythonFFP = pythonFFP;
            this.PythonAlias = alias;
            this.ChosenDefault = chosen;
            this.pythonVersion = String.Empty;
        }

        public string PythonFFP { get => pythonFFP; set => pythonFFP = value; }
        public string PythonAlias { get => pythonAlias; set => pythonAlias = value; }
        public bool ChosenDefault { get => chosenDefault; set => chosenDefault = value; }
        public int  InitPythonInterpreterVersion()
        {
            int defaultExitCode = 1;
            Process p = new Process();
            p.StartInfo.FileName = String.Format("{0}", this.PythonFFP);
            p.StartInfo.Arguments = "--version";
            p.StartInfo.UseShellExecute = false; // 不显示用户界面
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardInput = true;//可以重定向输入  
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;

            try
            {
                if (!p.Start())
                    return defaultExitCode;

                string version = p.StandardOutput.ReadToEnd();

                p.WaitForExit(10 * 1000);

                if (p.ExitCode != 0)
                    return p.ExitCode;

                // 能匹配中就OK
                Match mat = PythonVersionRegex.Match(version);
                if (mat.Success)
                    this.pythonVersion = mat.Groups[1].Value;

                return p.ExitCode;
            }
            catch
            {
                this.pythonVersion = String.Empty;
                return defaultExitCode;
            }
            finally
            {
                if (p != null)
                    p.Close();
            }
        }
    }
}
