using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Citta_T1.Business.Schedule.Cmd
{
    class UnionOperatorCmd : OperatorCmd
    {
        public UnionOperatorCmd(Triple triple) : base(triple)
        {
        }

        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            string inputFilePath1 = inputFilePaths.First();
            string inputFilePath2 = inputFilePaths.Count > 1 ? inputFilePaths[1] : "";
            //以后算子路径功能写完后去掉
            if (inputFilePath1 == "" || inputFilePath2 == "")
            {
                Thread.Sleep(5000);
                cmds.Add("echo union");
            }
            Thread.Sleep(5000);
            /*
            string leftfieldLine = TransOutputField(option.GetOption("leftfield").Split(','));
            string rightfieldLine = TransOutputField(option.GetOption("rightfield").Split(','));

            cmds.Add(string.Format("awk -F'\\t' '{{print {0}}}' {1}> tmp.txt", leftfieldLine, inputFilePath1));
            cmds.Add(string.Format("awk -F'\\t' '{{print {0}}}' {1}>> tmp.txt", rightfieldLine, inputFilePath2));
            cmds.Add(string.Format("sort tmp.txt | uniq >> {0}", this.outputFilePath));
            */


            return cmds;
        }

    }
}