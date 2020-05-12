using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Citta_T1.IAOLab.PythonOP
{
    class PythonInterpreterInfo
    {
        private string pythonFFP;    // Python解释器全路径
        private string pythonAlias;  // 别名
        private bool chosenDefault;  // 被选中为Python算子默认采用的
        private Control console;     // 解释器对应的输出控制台


        public PythonInterpreterInfo(string pythonFFP, string alias, bool chosen)
        {
            this.pythonFFP = pythonFFP;
            this.pythonAlias = alias;
            this.chosenDefault = chosen;
            this.console = null;
        }

        public string PythonFFP { get => pythonFFP; }
        public string PythonAlias { get => pythonAlias; }
        public bool ChosenDefault { get => chosenDefault; }
        public Control Console { get => console; set => console = value; }
    }
}
