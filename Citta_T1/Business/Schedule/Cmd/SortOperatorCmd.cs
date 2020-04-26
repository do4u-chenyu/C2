using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Citta_T1.Business.Schedule.Cmd
{
    class SortOperatorCmd : OperatorCmd
    {
        public SortOperatorCmd(Triple triple) : base(triple)
        {
        }

        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            string inputFilePath = inputFilePaths.First();//输入文件
            string sortLine = TransInputLine(option.GetOption("sortfield"));//最大值字段
            string firstRow = int.Parse(option.GetOption("firstRow"))>0? option.GetOption("firstRow"):"1";//开始行数
            string endRow = int.Parse(option.GetOption("endRow"))>0? option.GetOption("endRow"):"1";//结束行数

            //是否去重(是对整个文件去重)、升降序
            string repetition = option.GetOption("noRepetition") == "True" ? " -u " : "";
            string order = option.GetOption("ascendingOrder") == "True" ? " -n " : "-nr ";

            cmds.Add(string.Format("{0} | sbin\\sort.exe {1} -k{2} {3} {4}  | sbin\\head.exe -n{5} | sbin\\tail.exe -n +{6} | sbin\\awk.exe -F'\\t' -v OFS='\\t' '{{ print $0}}'>> {7}", TransInputfileToCmd(inputFilePath), this.sortConfig, sortLine, repetition, order, endRow, firstRow, this.outputFilePath));

            return cmds;
        }

    }
}