using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Citta_T1.Business.Schedule.Cmd
{
    class FilterOperatorCmd : OperatorCmd
    {
        public FilterOperatorCmd(Triple triple) : base(triple)
        {
        }

        public string GenCmd()
        {
            string inputFilePath = inputFilePaths.First();

            //以后算子路径功能写完后去掉
            if (inputFilePath == "")
            {
                Thread.Sleep(5000);
                return "echo filter";
            }

            string outfieldLine = TransOutputField(option.GetOption("outfield").Split(','));

            string[] factor1 = option.GetOption("factor1").Split(',');
            string awkIfCmd = "$" + factor1[0] + TransChoiceToCmd(factor1[1]) + factor1[1];
            for (int i = 2; i <= option.OptionDict.Count() - 1; i++)
            {
                string[] tmpfactor = option.GetOption("factor" + i.ToString()).Split(',');
                awkIfCmd = awkIfCmd + " " + TransChoiceToCmd(tmpfactor[0]) + " $" + tmpfactor[1] + TransChoiceToCmd(tmpfactor[2]) + tmpfactor[3];
            }

            string cmd = string.Format("sbin\\awk.exe -F'\\t' '{{if({0}) print {1} }}' >> {2}", awkIfCmd, outfieldLine, this.outputFilePath);
            return cmd;
        }

    }
}