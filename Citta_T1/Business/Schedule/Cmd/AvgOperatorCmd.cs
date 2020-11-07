using System.Collections.Generic;
using System.Linq;
using C2.Model.Widgets;

namespace C2.Business.Schedule.Cmd
{
    class AvgOperatorCmd : OperatorCmd
    {
        public AvgOperatorCmd(Triple triple) : base(triple)
        {
        }
        public AvgOperatorCmd(OperatorWidget operatorWidget) : base(operatorWidget)
        {
        }
        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            string inputFilePath = inputFilePaths.First();//输入文件
            string avgField = TransInputLine(option.GetOption("avgfield"));//取平均值字段

            //重写表头（覆盖）
            ReWriteBCPFile();

            //满足数字、小数的才算，结果保留3位小数
            cmds.Add(string.Format("{0} | sbin\\awk.exe -F\"{1}\" -v OFS='\\t' \"BEGIN{{count=0;}}{{if( ${2}~/^[0-9]+\\.?[0-9]*$/) {{count+=1;a+=${2} }} }}END{{if(count>0){{printf(\\\"%.3f\\\",a/count)}} }}\" >> {3}", TransInputfileToCmd(inputFilePath), this.separators[0], avgField, this.outputFilePath));
            return cmds;
        }
    }
}
