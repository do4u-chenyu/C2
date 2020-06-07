using System.Collections.Generic;
using System.Linq;

namespace Citta_T1.Business.Schedule.Cmd
{
    class MaxOperatorCmd : OperatorCmd
    {
        public MaxOperatorCmd(Triple triple) : base(triple)
        {
        }

        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            string inputFilePath = inputFilePaths.First();//输入文件
            string maxField = TransInputLine(option.GetOption("maxfield"));//最大值字段
            string outField = TransOutputField(option.GetOptionSplit("outfield"));//输出字段

            //重写表头（覆盖）
            ReWriteBCPFile();

            cmds.Add(string.Format("{0}| sbin\\sort.exe {1} -t\"{5}\" -nr -k {2} | sbin\\head.exe -n1 | sbin\\awk.exe -F\"{5}\" -v OFS='\\t' '{{ print {3}}}'>> {4}", TransInputfileToCmd(inputFilePath), this.sortConfig, maxField, outField, this.outputFilePath, this.separators[0]));

            return cmds;
        }
    }
}
