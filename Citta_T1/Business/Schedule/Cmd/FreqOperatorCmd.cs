using System.Collections.Generic;
using System.Linq;

namespace Citta_T1.Business.Schedule.Cmd
{
    class FreqOperatorCmd : OperatorCmd
    {
        public FreqOperatorCmd(Triple triple) : base(triple)
        {
        }
        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            string inputFilePath = inputFilePaths.First();//输入文件

            //是否去重(是对整个文件去重)、升降序
            string repetition = option.GetOption("noRepetition").ToLower() == "true" ? string.Format("sbin\\sort.exe {0} -u |", this.sortConfig) : "";
            string order = option.GetOption("ascendingOrder").ToLower() == "true" ? string.Format("sbin\\sort.exe {0} ", this.sortConfig) : string.Format("sbin\\sort.exe {0} -r ", this.sortConfig);

            //待统计频率字段合并
            //uniq统计频率的结果第一列是频次。需要修改输出顺序，把频率结果放在最后一列
            string inputFields = TransOutputField(option.GetOptionSplit("outfield"));
            int outFieldCount = option.GetOptionSplit("outfield").Count() + 1;//多了一列频率结果
            string outField = "$2";
            for (int i = 3; i <= outFieldCount; i++)
            {
                outField = outField + ",$" + i.ToString();
            }
            outField += ",$1";

            //重写表头（覆盖）
            ReWriteBCPFile("freq");

            cmds.Add(string.Format("{0} | {1} sbin\\awk.exe -F\"{7}\" -v OFS='\\t' '{{ print {2}}}' | sbin\\sort.exe {3} | sbin\\uniq.exe -c | {4} | sbin\\awk.exe -F' ' -v OFS='\\t' '{{ print {5}}}'>> {6}", TransInputfileToCmd(inputFilePath), repetition, inputFields, this.sortConfig, order, outField, this.outputFilePath, this.separators[0]));

            return cmds;
        }

    }
}