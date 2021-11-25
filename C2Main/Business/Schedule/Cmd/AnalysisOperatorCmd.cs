using C2.Model.Widgets;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.Schedule.Cmd
{
    class AnalysisOperatorCmd : OperatorCmd
    {
        public AnalysisOperatorCmd(Triple triple) : base(triple)
        {
        }
        public AnalysisOperatorCmd(OperatorWidget operatorWidget) : base(operatorWidget)
        {
        }

        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            string inputFilePath1 = inputFilePaths.First();//左输入文件
            string inputFilePath2 = inputFilePaths.Count > 1 ? inputFilePaths[1] : String.Empty;//右输入文件
            string analysisType = option.GetOption("analysisType");//分析类型

            cmds.Add(string.Format("sbin\\analysis.exe {0} {1} {2} {3}", inputFilePath1, inputFilePath2, this.outputFilePath, analysisType));

            return cmds;
        }
        
    }
}
