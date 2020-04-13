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
            string inputFilePath = inputFilePaths.First();

            //以后算子路径功能写完后去掉
            if (inputFilePath == "")
            {
                Thread.Sleep(5000);
                cmds.Add("echo random");
            }
            Thread.Sleep(5000);

            string randomnum = (int.Parse(option.GetOption("randomnum")) + 1).ToString();
            string outfieldLine = TransOutputField(option.GetOption("outfield").Split(','));

            cmds.Add(string.Format("sbin\\awk.exe 'BEGIN{{srand()}} {{print rand()\"\\t\"$0}}' {0} | sbin\\sort.exe - nk 1 | sbin\\head.exe - n {1} | sbin\\awk.exe 'sub($1\"\\t\",\"\")' | | sbin\\awk.exe -F'\\t' '{{ print {2}}}' >> {3}", inputFilePath, randomnum, outfieldLine, this.outputFilePath));
            return cmds;

        }

    }
}