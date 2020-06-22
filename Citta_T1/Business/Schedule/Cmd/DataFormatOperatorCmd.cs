using System.Collections.Generic;
using System.Linq;

namespace Citta_T1.Business.Schedule.Cmd
{
    class DataFormatOperatorCmd : OperatorCmd
    {
        public DataFormatOperatorCmd(Triple triple) : base(triple)
        {
        }
        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            string inputFilePath = inputFilePaths.First();//输入文件

            //拼接输出字段facotr1\factor2...\factorN
            string outField = "$" + TransInputLine(option.GetOptionSplit("factor0")[0]);
            for (int i = 1; i < GetOptionFactorCount(); i++)
            {
                string[] tmpFactor = option.GetOptionSplit("factor" + i.ToString());
                outField = outField + ",$" + TransInputLine(tmpFactor[0]);
            }

            //重写表头（覆盖）
            ReWriteBCPFile("format");

            cmds.Add(string.Format("{0} | sbin\\awk.exe -F\"{1}\" -v OFS='\\t' '{{print {2}}}' >> {3}", TransInputfileToCmd(inputFilePath), this.separators[0], outField, this.outputFilePath));
            return cmds;
        }
    }
}
