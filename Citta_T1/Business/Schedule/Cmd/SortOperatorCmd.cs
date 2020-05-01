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
            string outfieldLine = TransOutputField(option.GetOption("outfield").Split(','));//输出字段

            //是否去重(是对整个文件去重)、升降序
            string repetition = option.GetOption("noRepetition") == "True" ? " -u " : "";
            string order = option.GetOption("ascendingOrder") == "True" ? " -n " : "-nr ";

            //重写表头（覆盖）
            cmds.Add(string.Format("sbin\\echo.exe \"{0}\" | sbin\\iconv.exe -f gbk -t utf-8 | sbin\\awk.exe -F\"{3}\" -v OFS='\\t' '{{ print {1} }}' > {2}", this.outputFileTitle, outfieldLine, this.outputFilePath, this.separators[0]));

            cmds.Add(string.Format("{0} | sbin\\sort.exe {1} -k{2} {3} {4}  | sbin\\head.exe -n{5} | sbin\\tail.exe -n +{6} | sbin\\awk.exe -F\"{9}\" -v OFS='\\t' '{{ print {7}}}'>> {8}", TransInputfileToCmd(inputFilePath), this.sortConfig, sortLine, repetition, order, endRow, firstRow, outfieldLine,this.outputFilePath, this.separators[0]));

            return cmds;
        }

    }
}