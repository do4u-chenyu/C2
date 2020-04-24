using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Citta_T1.Business.Schedule.Cmd
{
    class GroupOperatorCmd : OperatorCmd
    {
        public GroupOperatorCmd(Triple triple) : base(triple)
        {
        }

        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            string inputFilePath = inputFilePaths.First();//输入文件
            string outfieldLine = TransOutputField(option.GetOption("outField").Split(','));//输出字段

            //是否去重(是对整个文件去重)、升降序
            string repetition = option.GetOption("noRepetition") == "True" ? string.Format("sbin\\sort.exe {0} -u | ",this.sortConfig) : "";
            string order = option.GetOption("ascendingOrder") == "True" ? " -n " : "-nr ";

            //拼接分组字段
            string sortLineCmd = "-k" + TransInputLine(option.GetOption("factor1"));
            for (int i = 2; i <= GetOptionFactorCount(); i++)
            {
                string tmpfactor = option.GetOption("factor" + i.ToString());
                sortLineCmd = sortLineCmd + " -k" + TransInputLine(tmpfactor);
            }

            cmds.Add(string.Format("{0} | {1} sbin\\sort.exe {2} {3} {4} | sbin\\tr.exe -d '\\r' | sbin\\awk.exe -F'\\t' -v OFS='\\t' '{{ print {5}}}'>> {6}", TransInputfileToCmd(inputFilePath), repetition, this.sortConfig, order, sortLineCmd, outfieldLine, this.outputFilePath));

            return cmds;
        }
    }
}