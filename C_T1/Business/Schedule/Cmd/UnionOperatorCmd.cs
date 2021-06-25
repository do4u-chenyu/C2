using System;
using System.Collections.Generic;
using System.Linq;

namespace C2.Business.Schedule.Cmd
{
    class UnionOperatorCmd : OperatorCmd
    {
        public UnionOperatorCmd(Triple triple) : base(triple)
        {
        }

        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            string inputFilePath1 = inputFilePaths.First();//左输入文件
            string inputFilePath2 = inputFilePaths.Count > 1 ? inputFilePaths[1] : String.Empty;//右输入文件

            //两个文件的输出字段分别合并
            string inputField1 = "$" + TransInputLine(option.GetOptionSplit("factor0")[0]);
            string inputField2 = "$" + TransInputLine(option.GetOptionSplit("factor0")[1]);
            for (int i = 1; i < GetOptionFactorCount(); i++)
            {
                string[] tmpfactor = option.GetOptionSplit("factor" + i.ToString());
                inputField1 = inputField1 + ",$" + TransInputLine(tmpfactor[0]);
                inputField2 = inputField2 + ",$" + TransInputLine(tmpfactor[1]);
            }

            //并集生成1个临时文件
            string filterBatPath1 = System.IO.Path.GetDirectoryName(this.outputFilePath) + "\\O" + this.operatorId + "_union1.tmp";

            //重写表头（覆盖）
            ReWriteBCPFile("union");

            cmds.Add(string.Format("{0}  | sbin\\awk.exe -F\"{3}\" -v OFS='\\t' '{{print {1}}}' >> {2}", TransInputfileToCmd(inputFilePath1), inputField1, filterBatPath1, this.separators[0]));
            cmds.Add(string.Format("{0}  | sbin\\awk.exe -F\"{3}\" -v OFS='\\t' '{{print {1}}}' >> {2}", TransInputfileToCmd(inputFilePath2), inputField2, filterBatPath1, this.separators[1]));

            //是否合并后的文件去重
            if (option.GetOption("noRepetition") == "True")
            {
                cmds.Add(string.Format("sbin\\sort.exe {0} -u {1}>>{2}", this.sortConfig, filterBatPath1, this.outputFilePath));
            }
            else
            {
                cmds.Add(string.Format("sbin\\cat.exe {0}>>{1}", filterBatPath1, this.outputFilePath));
            }
            cmds.Add(string.Format("sbin\\rm.exe -f {0}", filterBatPath1));

            return cmds;
        }

    }
}