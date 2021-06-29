using System;
using System.Collections.Generic;
using System.Linq;
using C2.Model.Widgets;

namespace C2.Business.Schedule.Cmd
{
    class GroupOperatorCmd : OperatorCmd
    {
        public GroupOperatorCmd(Triple triple) : base(triple)
        {
        }
        public GroupOperatorCmd(OperatorWidget operatorWidget) : base(operatorWidget)
        {
        }
        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            string inputFilePath = inputFilePaths.First();//输入文件
            string outField = TransOutputField(option.GetOptionSplit("outfield0"));//输出字段

            //是否去重(是对整个文件去重)、升降序
            string repetition = option.GetOption("noRepetition").ToLower() == "true" ? string.Format("sbin\\sort.exe {0} -u | ", this.sortConfig) : String.Empty;
            string order = option.GetOption("ascendingOrder").ToLower() == "true" ? String.Empty : "-r ";
            string type = option.GetOption("sortByNum").ToLower() == "true" ? "-n" : String.Empty;

            //拼接分组字段
            string groupFieldCmd = "-k" + TransInputLine(option.GetOption("factor0"));

            for (int i = 1; i < GetOptionFactorCount(); i++)
            {
                string tmpFactor = option.GetOption("factor" + i.ToString());
                groupFieldCmd = groupFieldCmd + " -k" + TransInputLine(tmpFactor);
            }

            //重写表头（覆盖）
            ReWriteBCPFile();

            cmds.Add(string.Format("{0} | {1} sbin\\sort.exe -t\"{7}\" {8} {2} {3} {4} | sbin\\awk.exe -F\"{7}\" -v OFS='\\t' '{{ print {5}}}'>> {6}", TransInputfileToCmd(inputFilePath), repetition, this.sortConfig, order, groupFieldCmd, outField, this.outputFilePath, this.separators[0], type));

            return cmds;
        }
    }
}