using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Citta_T1.Business.Schedule.Cmd
{
    class MaxOperatorCmd : OperatorCmd
    {
        public MaxOperatorCmd(Triple triple) : base(triple)
        {
        }

        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            string inputFilePath = inputFilePaths.First();//输入文件
            string inputfieldLine = TransInputLine(option.GetOption("maxfield"));//最大值字段
            string outfieldLine = TransOutputField(option.GetOption("outfield").Split(','));//输出字段

            //重写表头（覆盖）
            cmds.Add(string.Format("sbin\\echo.exe \"{0}\" | sbin\\iconv.exe -f gbk -t utf-8 | sbin\\awk.exe -F\"{3}\" -v OFS='\\t' '{{ print {1} }}' > {2}", this.outputFileTitle,outfieldLine,this.outputFilePath,this.separators[0]));
            
            cmds.Add(string.Format("{0}| sbin\\sort.exe {1} -nr -k {2} | sbin\\head.exe -n1 | sbin\\awk.exe -F\"{5}\" -v OFS='\\t' '{{ print {3}}}'>> {4}", TransInputfileToCmd(inputFilePath), this.sortConfig, inputfieldLine, outfieldLine,this.outputFilePath, this.separators[0]));

            return cmds;
        }
    }
}
