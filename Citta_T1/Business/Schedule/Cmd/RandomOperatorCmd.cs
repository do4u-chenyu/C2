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
            string randomnum = option.GetOption("randomnum");//随机条数
            string outfieldLine = TransOutputField(option.GetOption("outfield").Split(','));//输出字段

            cmds.Add(string.Format("{0} | sbin\\awk.exe 'BEGIN{{srand()}} {{print rand()\"\\t\"$0}}' | sbin\\sort.exe {1} -n -k1 | sbin\\head.exe -n {2} | sbin\\awk.exe 'sub($1\"\\t\",\"\")' | sbin\\awk.exe -F'\\t' -v OFS='\\t' '{{ print {3}}}' >> {4}" , TransInputfileToCmd(inputFilePath), this.sortConfig, randomnum, outfieldLine, this.outputFilePath));
            return cmds;

        }

    }
}