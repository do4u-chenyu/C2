using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.IO;
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

        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            string inputFilePath = inputFilePaths.First();
            //DSUtil.Encoding inputFileEncode = encodings.First();

            //以后算子路径功能写完后去掉
            if (inputFilePath == "")
            {
                Thread.Sleep(5000);
                cmds.Add("echo filter");
            }
            StreamWriter streamWriter = null;

            
            string filterBatPath = System.IO.Path.GetDirectoryName(this.outputFilePath) + "\\O" + this.operatorId + "_filterChoice.bat";
            if(this.encoding == DSUtil.Encoding.UTF8)
            {
                UTF8Encoding utf8 = new UTF8Encoding(false);
                streamWriter = new StreamWriter(filterBatPath, false, utf8);
            }
            else
            {
                streamWriter = new StreamWriter(filterBatPath, false, Encoding.GetEncoding("gbk"));

            }
            string outfieldLine = TransOutputField(option.GetOption("outfield").Split(','));

            string[] factor1 = option.GetOption("factor1").Split(',');
            string awkIfCmd ="(" + "$" + TransInputLine(factor1[0]) + TransChoiceToCmd(factor1[1]) + TransConditionToCmd(factor1[2]) + ")";
            for (int i = 2; i <= option.OptionDict.Count() - 1; i++)
            {
                string[] tmpfactor = option.GetOption("factor" + i.ToString()).Split(',');
                awkIfCmd = awkIfCmd + " " + TransAndOrToCmd(tmpfactor[0]) + "(" + " $" + TransInputLine(tmpfactor[1]) + TransChoiceToCmd(tmpfactor[2]) + TransConditionToCmd(tmpfactor[3]) + ")";
            }

            string awkExec = string.Format("{{if({0}){{print {1} }} }}", awkIfCmd, outfieldLine);

            streamWriter.Write(awkExec);
            streamWriter.Close();

            cmds.Add(string.Format("sbin\\tail.exe -n +2 {0} | sbin\\awk.exe -F'\\t' -v OFS='\\t' -E {1} >> {2}", inputFilePath, filterBatPath, this.outputFilePath));
            return cmds;
        }

    }
}