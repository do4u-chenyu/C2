using System;
using System.Collections.Generic;
using System.Linq;

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

            string inputFilePath1 = inputFilePaths.First();//左输入文件
            string inputFilePath2 = inputFilePaths.Count > 1 ? inputFilePaths[1] : String.Empty;//右输入文件
            string outField = TransDifferOutputField(option.GetOptionSplit("outfield"));//输出字段

            //目前一个算子固定生成4个临时文件
            string filterBatPath1 = System.IO.Path.GetDirectoryName(this.outputFilePath) + "\\O" + this.operatorId + "_differ1.tmp";
            string filterBatPath2 = System.IO.Path.GetDirectoryName(this.outputFilePath) + "\\O" + this.operatorId + "_differ2.tmp";
            string filterBatPath3 = System.IO.Path.GetDirectoryName(this.outputFilePath) + "\\O" + this.operatorId + "_differ3.tmp";
            string filterBatPath4 = System.IO.Path.GetDirectoryName(this.outputFilePath) + "\\O" + this.operatorId + "_differ4.tmp";

            //取差条件拼接
            List<string[]> differTmpList = new List<string[]>();
            string[] factor1 = option.GetOptionSplit("factor0");
            differTmpList.Add(factor1);
            for (int i = 1; i < GetOptionFactorCount(); i++)
            {

                string[] tmpFactor = option.GetOptionSplit("factor" + i.ToString());
                string andOr = tmpFactor[0];
                if (andOr == "0")
                {
                    //如果是AND，那么添加到当前列表
                    differTmpList.Add(tmpFactor.Skip(1).Take(2).ToArray());
                }
                else
                {
                    //如果是or，开启一个新列表
                    differList.Add(differTmpList);
                    differTmpList = new List<string[]> { tmpFactor.Skip(1).Take(2).ToArray() };
                }
            }
            differList.Add(differTmpList);

            //重写表头（覆盖）
            ReWriteBCPFile("differ");

            foreach (List<string[]> tmpList in differList)
            {
                string inputField1 = "$" + TransInputLine(tmpList[0][0]);
                string inputField2 = "$" + TransInputLine(tmpList[0][1]);
                for (int i = 1; i < tmpList.Count; i++)
                {
                    inputField1 = inputField1 + ",$" + TransInputLine(tmpList[i][0]);
                    inputField2 = inputField2 + ",$" + TransInputLine(tmpList[i][1]);
                }
                //每个循环处理一关系
                cmds.Add(string.Format("{0} | sbin\\awk.exe -F\"{4}\" -v OFS=\"{5}\" '{{print {1}}}' | sbin\\sort.exe {2} -u > {3}", TransInputfileToCmd(inputFilePath1), inputField1, this.sortConfig, filterBatPath1, this.separators[0], TransOFSToCmd(this.separators[0])));
                cmds.Add(string.Format("{0} | sbin\\awk.exe -F\"{4}\" -v OFS=\"{5}\" '{{print {1}}}' | sbin\\sort.exe {2} -u > {3}", TransInputfileToCmd(inputFilePath2), inputField2, this.sortConfig, filterBatPath2, this.separators[1], TransOFSToCmd(this.separators[1])));
                cmds.Add(string.Format("sbin\\comm.exe -23 {0} {1} > {2}", filterBatPath1, filterBatPath2, filterBatPath3));
                cmds.Add(string.Format("{0} | sbin\\awk.exe -F\"{4}\" -v OFS=\"{5}\"  '{{print {1}\"{4}\"$0}}' | sbin\\sort.exe {2} -u > {3}", TransInputfileToCmd(inputFilePath1), inputField1, this.sortConfig, filterBatPath4, this.separators[0], TransOFSToCmd(this.separators[0])));
                cmds.Add(string.Format("sbin\\join.exe -t\"{4}\" {0} {1} | sbin\\awk.exe -F\"{4}\" -v OFS='\\t' '{{print {2}}}' >> {3}", filterBatPath3, filterBatPath4, outField, this.outputFilePath, this.separators[0]));
            }

            cmds.Add(string.Format("sbin\\rm.exe -f {0} {1} {2} {3}", filterBatPath1, filterBatPath2, filterBatPath3, filterBatPath4));

            return cmds;
        }

    }
}