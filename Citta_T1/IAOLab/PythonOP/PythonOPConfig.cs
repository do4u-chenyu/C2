using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.IAOLab.PythonOP
{
    class PythonOPConfig
    {
        private List<PythonInterpreterInfo> pythonInterpreterInfos;

        public PythonInterpreterInfo DefaultPythonInterpreterInfo { get { return pythonInterpreterInfos.Find(c => c.ChosenDefault); } }
        
        public PythonInterpreterInfo[] Others { get { return pythonInterpreterInfos.FindAll(c => !c.ChosenDefault).ToArray(); } }

        public PythonInterpreterInfo[] All { get { return pythonInterpreterInfos.ToArray(); } }

        public PythonOPConfig(string pythonConfigString)
        {
            pythonInterpreterInfos = new List<PythonInterpreterInfo>(16);
            // 样例
            // C:\PythonFake\Python37\python.exe|Python37|true;C:\PythonFake\Python37\python.exe|Python37|false;
            foreach (string pItem in pythonConfigString.Split(';'))
            {
                string[] oneConfig = pItem.Split('|');
                // 格式不对, 忽略
                if (oneConfig.Length != 3) continue;
                string pythonFFP = oneConfig[0].Trim();
                string alias = oneConfig[1].Trim();
                bool chosen = ConvertUtil.TryParseBool(oneConfig[2]);
                pythonInterpreterInfos.Add(new PythonInterpreterInfo(pythonFFP, alias, chosen));
            }
        }

      


    }
}
