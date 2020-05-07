using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.IAOLab.PythonOP
{
    class PythonInterpreterInfo
    {
        private string pythonFFP;    // Python解释器全路径
        private string pythonAlias;  // 别名
        private bool chosenDefault;  // 被选中为Python算子默认采用的

        public PythonInterpreterInfo(string pythonFFP, string alias, bool chosen)
        {
            this.PythonFFP = pythonFFP;
            this.PythonAlias = alias;
            this.ChosenDefault = chosen;
        }

        public string PythonFFP { get => pythonFFP; set => pythonFFP = value; }
        public string PythonAlias { get => pythonAlias; set => pythonAlias = value; }
        public bool ChosenDefault { get => chosenDefault; set => chosenDefault = value; }
    }
}
