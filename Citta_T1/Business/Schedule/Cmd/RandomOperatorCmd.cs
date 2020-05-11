using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Citta_T1.Business.Schedule.Cmd
{
    class RandomOperatorCmd : OperatorCmd
    {
        public RandomOperatorCmd(Triple triple) : base(triple)
        {
        }

        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            string inputFilePath = inputFilePaths.First();//输入文件
            string outfieldLine = TransOutputField(option.GetOption("outfield").Split(','));//输出字段
            string randomNum = ConvertUtil.TryParseInt(option.GetOption("randomnum")) > 0 ? option.GetOption("randomnum") : "1";//随机条数

            //重写表头（覆盖）
            ReWriteBCPFile();

            cmds.Add(string.Format("{0} | sbin\\awk.exe 'BEGIN{{srand()}} {{print rand()\"{5}\"$0}}' | sbin\\sort.exe -t\"{5}\" {1} -n -k1 | sbin\\head.exe -n {2} | sbin\\awk.exe -F\"{5}\" -v OFS='\\t' '{{ $1=\"\";sub(\"\\t\",\"\");print $0}}' | sbin\\awk.exe -F'\\t' -v OFS='\\t' '{{ print {3}}}' >> {4}", TransInputfileToCmd(inputFilePath), this.sortConfig, randomNum, outfieldLine, this.outputFilePath, this.separators[0]));
            return cmds;

        }

    }
}