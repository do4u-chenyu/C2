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
            string inputFilePath = inputFilePaths.First();//输入文件
            string outfieldLine = TransOutputField(option.GetOption("outfield").Split(','));//输出字段


            //过滤条件拼接
            string[] factor1 = option.GetOption("factor1").Split(',');
            string awkIfCmd ="(" + "$" + TransInputLine(factor1[0]) + TransChoiceToCmd(factor1[1]) + TransConditionToCmd(factor1[2]) + ")";
            for (int i = 2; i <= GetOptionFactorCount(); i++)
            {
                string[] tmpfactor = option.GetOption("factor" + i.ToString()).Split(',');
                awkIfCmd = awkIfCmd + " " + TransAndOrToCmd(tmpfactor[0]) + "(" + " $" + TransInputLine(tmpfactor[1]) + TransChoiceToCmd(tmpfactor[2]) + TransConditionToCmd(tmpfactor[3]) + ")";
            }
            string awkExec = string.Format("{{if({0}){{print {1} }} }}", awkIfCmd, outfieldLine);
            
            //过滤条件写入临时配置文件，需判断输入文件格式。（解决条件为中文时的编码问题）
            StreamWriter streamWriter = null;
            string filterBatPath = System.IO.Path.GetDirectoryName(this.outputFilePath) + "\\O" + this.operatorId + "_filterChoice.bat";
            
            //目前输入文件统一转换为utf-8
            UTF8Encoding utf8 = new UTF8Encoding(false);
            streamWriter = new StreamWriter(filterBatPath, false, utf8);   
            streamWriter.Write(awkExec);
            streamWriter.Close();

            //重写表头（覆盖）
            ReWriteBCPFile();

            cmds.Add(string.Format("{0} | sbin\\awk.exe -F\"{3}\" -v OFS='\\t' -E {1} >> {2}", TransInputfileToCmd(inputFilePath), filterBatPath, this.outputFilePath, this.separators[0]));
            cmds.Add(string.Format("sbin\\rm.exe -f {0}", filterBatPath));
            return cmds;
        }

    }
}