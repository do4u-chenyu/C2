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
            string outField = "$" + TransInputLine(option.GetOption("factor1"));
            for (int i = 2; i <= GetOptionFactorCount(); i++)
            {
                string tmpFactor = option.GetOption("factor" + i.ToString());
                outField = outField + ",$" + TransInputLine(tmpFactor);
            }

            //重写表头（覆盖）
            ReWriteBCPFile();

            //TODO
            //cmds.Add(string.Format("{0}| sbin\\sort.exe {1} -t\"{5}\" -nr -k {2} | sbin\\head.exe -n1 | sbin\\awk.exe -F\"{5}\" -v OFS='\\t' '{{ print {3}}}'>> {4}", TransInputfileToCmd(inputFilePath), this.sortConfig, inputfieldLine, outfieldLine, this.outputFilePath, this.separators[0]));
            cmds.Add("sbin\\echo.exe dataformat");

            return cmds;
        }
    }
}
