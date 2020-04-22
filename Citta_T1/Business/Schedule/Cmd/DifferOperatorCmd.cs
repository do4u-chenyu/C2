using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Citta_T1.Business.Schedule.Cmd
{
    class DifferOperatorCmd : OperatorCmd
    {
        public DifferOperatorCmd(Triple triple) : base(triple)
        {
        }

        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            List<List<string[]>> differList = new List<List<string[]>>();

            string inputFilePath1 = inputFilePaths.First();
            string inputFilePath2 = inputFilePaths.Count > 1 ? inputFilePaths[1] : "";
            if (inputFilePath1 == "" || inputFilePath2 == "")
            {
                Thread.Sleep(5000);
                cmds.Add("echo differ");
            }


            //目前一个算子固定生成4个临时文件
            string filterBatPath1 = System.IO.Path.GetDirectoryName(this.outputFilePath) + "\\O" + this.operatorId + "_differ1.tmp";
            string filterBatPath2 = System.IO.Path.GetDirectoryName(this.outputFilePath) + "\\O" + this.operatorId + "_differ2.tmp";
            string filterBatPath3 = System.IO.Path.GetDirectoryName(this.outputFilePath) + "\\O" + this.operatorId + "_differ3.tmp";
            string filterBatPath4 = System.IO.Path.GetDirectoryName(this.outputFilePath) + "\\O" + this.operatorId + "_differ4.tmp";

            string outfieldLine = TransDifferOutputField(option.GetOption("outfield").Split(','));

            List<string[]> differTmpList = new List<string[]>();
            string[] factor1 = option.GetOption("factor1").Split(',');
            differTmpList.Add(factor1);

            for (int i = 2; i <= option.OptionDict.Count() - 1; i++)
            {
                string[] tmpfactor = option.GetOption("factor" + i.ToString()).Split(',');
                string andor = tmpfactor[0];
                //string[] differfactor = tmpfactor[1:];
                if(andor == "0")
                {
                    //如果是AND，那么添加到当前列表
                    differTmpList.Add(tmpfactor.Skip(1).Take(2).ToArray());
                }
                else
                {
                    //如果是or，开启一个新列表
                    differList.Add(differTmpList);
                    differTmpList.Clear();
                    differTmpList.Add(tmpfactor.Skip(1).Take(2).ToArray());
                }
            }
            differList.Add(differTmpList);

            foreach (List<string[]> tmpList in differList)
            {
                string inputFiled1 = "$" + TransInputLine(tmpList[0][0]);
                string inputFiled2 = "$" + TransInputLine(tmpList[0][1]);
                for(int tt = 1; tt < tmpList.Count(); tt++)
                {
                    inputFiled1 = inputFiled1 + ",$" + TransInputLine(tmpList[tt][0]);
                    inputFiled2 = inputFiled2 + ",$" + TransInputLine(tmpList[tt][1]);
                }
                //每个循环处理一关系
                cmds.Add(string.Format("sbin\\tail.exe -n +2 {0} | sbin\\awk.exe -F'\\t' -v OFS=\"|\" '{{print {1}}}' | sbin\\sort.exe -u > {2}", inputFilePath1, inputFiled1, filterBatPath1));
                cmds.Add(string.Format("sbin\\tail.exe -n +2 {0} | sbin\\awk.exe -F'\\t' -v OFS=\"|\" '{{print {1}}}' | sbin\\sort.exe -u > {2}", inputFilePath2, inputFiled2, filterBatPath2));
                cmds.Add(string.Format("sbin\\comm.exe -23 {0} {1} > {2}", filterBatPath1, filterBatPath2, filterBatPath3));
                cmds.Add(string.Format("sbin\\tail.exe -n +2 {0} | sbin\\awk.exe -F'\\t' -v OFS=\"|\"  '{{print {1}\"\\t\"$0}}' | sbin\\sort.exe -u > {2}", inputFilePath1, inputFiled1, filterBatPath4));
                cmds.Add(string.Format("sbin\\join.exe {0} {1} | sbin\\awk.exe -F' ' -v OFS='\\t' '{{print {2}}}' >> {3}", filterBatPath3, filterBatPath4, outfieldLine, this.outputFilePath));
            }

            cmds.Add(string.Format("sbin\\rm.exe -f {0} {1} {2} {3}", filterBatPath1, filterBatPath2, filterBatPath3, filterBatPath4));

            return cmds;
        }

    }
}