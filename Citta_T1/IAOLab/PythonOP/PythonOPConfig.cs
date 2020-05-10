using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.IAOLab.PythonOP
{
    class PythonOPConfig
    {
        private PythonInterpreterInfo defaultPythonInterpreterInfo;
        private PythonInterpreterInfo[] others;

        public PythonInterpreterInfo DefaultPythonInterpreterInfo { get => defaultPythonInterpreterInfo; set => defaultPythonInterpreterInfo = value; }
        public PythonInterpreterInfo[] Others { get => others; set => others = value; }

        public PythonOPConfig(string pythonConfigString)
        {
            // 样例
            // <add key="python" value="C:\PythonFake\Python37\python.exe|Python37|true;C:\PythonFake\Python37\python.exe|Python37|false;" />

        }

      


    }
}
