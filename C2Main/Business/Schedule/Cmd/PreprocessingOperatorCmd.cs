using C2.Model.Widgets;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.Schedule.Cmd
{
    class PreprocessingOperatorCmd : OperatorCmd
    {
        public PreprocessingOperatorCmd(Triple triple) : base(triple)
        {
        }
        public PreprocessingOperatorCmd(OperatorWidget operatorWidget) : base(operatorWidget)
        {
        }

        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            string inputFilePath = inputFilePaths.First();//输入文件
            string preType = TransInputLine(option.GetOption("pretype"));//取平均值字段

            //cmds.Add(string.Format("sbin\\preprocess.exe {0} {1} {2}", inputFilePath, this.outputFilePath, preType));
            return cmds;
        }
    }
}
