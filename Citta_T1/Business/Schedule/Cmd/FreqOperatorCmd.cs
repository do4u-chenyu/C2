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
            string inputFilePath = inputFilePaths.First();

            //以后算子路径功能写完后去掉
            if (inputFilePath == "")
            {
                Thread.Sleep(5000);
                cmds.Add("echo freq");
            }
            Thread.Sleep(5000);

            //去重是对整个文件去重
            string cmd1 = option.GetOption("repetition") == "True" ? "sbin\\sort.exe -u |" : "";
            string cmd2 = option.GetOption("ascendingOrder") == "True" ? "sbin\\sort.exe -n " : "sbin\\sort.exe -nr ";

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

            cmds.Add(string.Format("sbin\\tail.exe -n +2 {0}| {1} sbin\\awk.exe -F'\\t' -v OFS='\\t' '{{ print {2}}}' | sbin\\sort.exe | sbin\\uniq.exe -c | {3} | sbin\\awk.exe -F' ' -v OFS='\\t' '{{ print {4}}}'>> {5}", inputFilePath,cmd1, infieldLine, cmd2, outfieldLine, this.outputFilePath));

            return cmds;
        }

    }
}