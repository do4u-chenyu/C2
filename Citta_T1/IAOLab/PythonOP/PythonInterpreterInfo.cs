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



        public PythonInterpreterInfo(string pythonFFP, string alias, string version, bool chosen)
        {
            this.PythonFFP = pythonFFP;
            this.PythonAlias = alias;
            this.ChosenDefault = chosen;
            this.PythonVersion = version;
        }

        public string PythonFFP { get => pythonFFP; set => pythonFFP = value; }
        public string PythonAlias { get => pythonAlias; set => pythonAlias = value; }
        public bool ChosenDefault { get => chosenDefault; set => chosenDefault = value; }
        public string PythonVersion { get => pythonVersion; set => pythonVersion = value; }

    }
}
