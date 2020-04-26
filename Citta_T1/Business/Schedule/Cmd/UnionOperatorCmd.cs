using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Citta_T1.Business.Schedule.Cmd
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
            string inputFilePath2 = inputFilePaths.Count > 1 ? inputFilePaths[1] : "";//右输入文件

            //两个文件的输出字段分别合并
            string inputFiled1 = "";
            string inputFiled2 = "";
            for (int i = 1; i <= GetOptionFactorCount(); i++)
            {
                string[] tmpfactor = option.GetOption("factor" + i.ToString()).Split(',');
                if (i == 1)
                {
                    inputFiled1 = "$" + TransInputLine(tmpfactor[0]);
                    inputFiled2 = "$" + TransInputLine(tmpfactor[1]);
                }
                else
                {
                    inputFiled1 = inputFiled1 + ",$" + TransInputLine(tmpfactor[0]);
                    inputFiled2 = inputFiled2 + ",$" + TransInputLine(tmpfactor[1]);
                }
            }

            //并集生成1个临时文件
            string filterBatPath1 = System.IO.Path.GetDirectoryName(this.outputFilePath) + "\\O" + this.operatorId + "_union1.tmp";

            cmds.Add(string.Format("{0}  | sbin\\awk.exe -F'\\t' -v OFS='\\t' '{{print {1}}}' >> {2}", TransInputfileToCmd(inputFilePath1),inputFiled1,filterBatPath1));
            cmds.Add(string.Format("{0}  | sbin\\awk.exe -F'\\t' -v OFS='\\t' '{{print {1}}}' >> {2}", TransInputfileToCmd(inputFilePath2),inputFiled2,filterBatPath1));
            
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