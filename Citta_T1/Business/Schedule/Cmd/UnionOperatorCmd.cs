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
            string inputFilePath1 = inputFilePaths.First();
            string inputFilePath2 = inputFilePaths.Count > 1 ? inputFilePaths[1] : "";
            //以后算子路径功能写完后去掉
            if (inputFilePath1 == "" || inputFilePath2 == "")
            {
                Thread.Sleep(5000);
                cmds.Add("echo union");
            }
            string filterBatPath1 = System.IO.Path.GetDirectoryName(this.outputFilePath) + "\\O" + this.operatorId + "_union1.tmp";

            string inputFiled1 = "";
            string inputFiled2 = "";
            for (int i = 1; i <= option.OptionDict.Count() - 2; i++)
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

            cmds.Add(string.Format("sbin\\tail.exe -n +2 {0} | sbin\\awk.exe -F'\\t' -v OFS='\\t' '{{print {1}}}' >> {2}",inputFilePath1,inputFiled1,filterBatPath1));
            cmds.Add(string.Format("sbin\\tail.exe -n +2 {0} | sbin\\awk.exe -F'\\t' -v OFS='\\t' '{{print {1}}}' >> {2}",inputFilePath2,inputFiled2,filterBatPath1));
            if (option.GetOption("noRepetition") == "True")
            {
                cmds.Add(string.Format("sbin\\sort.exe -u {0}>>{1}", filterBatPath1, this.outputFilePath));
            }
            else
            {
                cmds.Add(string.Format("sbin\\cat.exe {0}>>{1}", filterBatPath1, this.outputFilePath));
            }
            cmds.Add(string.Format("sbin\\rm.exe -f {0}", filterBatPath1));

            /*
            string leftfieldLine = TransOutputField(option.GetOption("leftfield").Split(','));
            string rightfieldLine = TransOutputField(option.GetOption("rightfield").Split(','));

            cmds.Add(string.Format("awk -F'\\t' '{{print {0}}}' {1}> tmp.txt", leftfieldLine, inputFilePath1));
            cmds.Add(string.Format("awk -F'\\t' '{{print {0}}}' {1}>> tmp.txt", rightfieldLine, inputFilePath2));
            cmds.Add(string.Format("sort tmp.txt | uniq >> {0}", this.outputFilePath));
            noRepetition true 去重
            */



            return cmds;
        }

    }
}