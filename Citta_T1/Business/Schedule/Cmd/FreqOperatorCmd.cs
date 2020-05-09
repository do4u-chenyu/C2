using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Citta_T1.Business.Schedule.Cmd
{
    class FreqOperatorCmd : OperatorCmd
    {
        public FreqOperatorCmd(Triple triple) : base(triple)
        {
        }

        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            string inputFilePath = inputFilePaths.First();//输入文件

            //是否去重(是对整个文件去重)、升降序
            string repetition = option.GetOption("noRepetition").ToLower() == "true" ? string.Format("sbin\\sort.exe {0} -u |",this.sortConfig) : "";
            string order = option.GetOption("ascendingOrder").ToLower() == "true" ? string.Format("sbin\\sort.exe {0} ",this.sortConfig) : string.Format("sbin\\sort.exe {0} -r ",this.sortConfig);

            //待统计频率字段合并
            string infieldLine = TransOutputField(option.GetOption("outfield").Split(','));
            int count = option.GetOption("outfield").Split(',').Count()+1;
            string outfieldLine = "$2";
            if (count > 2)
            {
                for (int i = 3; i <= count; i++)
                {
                    outfieldLine = outfieldLine + ",$" + i.ToString();
                }
            }
            outfieldLine += ",$1";

            //重写表头（覆盖）
            //cmds.Add(string.Format("sbin\\echo.exe \"{0}\" | sbin\\iconv.exe -f gbk -t utf-8 | sbin\\awk.exe -F\"{3}\" -v OFS='\\t' '{{ print {1} }}' > {2}", this.outputFileTitle, outfieldLine, this.outputFilePath, this.separators[0]));
            ReWriteBCPFile("freq");

            cmds.Add(string.Format("{0} | {1} sbin\\awk.exe -F\"{7}\" -v OFS='\\t' '{{ print {2}}}' | sbin\\sort.exe {3} | sbin\\uniq.exe -c | {4} | sbin\\awk.exe -F' ' -v OFS='\\t' '{{ print {5}}}'>> {6}", TransInputfileToCmd(inputFilePath),repetition, infieldLine,this.sortConfig, order, outfieldLine, this.outputFilePath, this.separators[0]));

            return cmds;
        }

    }
}