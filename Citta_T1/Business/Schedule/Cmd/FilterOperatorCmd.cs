using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using C2.Model.Widgets;

namespace C2.Business.Schedule.Cmd
{
    class FilterOperatorCmd : OperatorCmd
    {
        public FilterOperatorCmd(Triple triple) : base(triple)
        {
        }
        public FilterOperatorCmd(OperatorWidget operatorWidget) : base(operatorWidget)
        {
        }
        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            string inputFilePath = inputFilePaths.First();//输入文件
            string outField = TransOutputField(option.GetOptionSplit("outfield0"));//输出字段

            //过滤条件拼接

            string[] factor1 = option.GetOptionSplit("factor0");
            string awkIfCmd = String.Format("(${0}{1}{2})", TransInputLine(factor1[0]), TransChoiceToCmd(factor1[1]), TransConditionToCmd(factor1[2]));
            for (int i = 1; i < GetOptionFactorCount(); i++)
            {
                string[] tmpFactor = option.GetOptionSplit("factor" + i.ToString());
                awkIfCmd = String.Format("{0} {1}(${2}{3}{4})", awkIfCmd, TransAndOrToCmd(tmpFactor[0]), TransInputLine(tmpFactor[1]), TransChoiceToCmd(tmpFactor[2]), TransConditionToCmd(tmpFactor[3]));
            }
            string awkExec = string.Format("{{if({0}){{print {1} }} }}", awkIfCmd, outField);

            //过滤条件写入临时配置文件，需判断输入文件格式。（解决条件为中文时的编码问题）
            string filterBatPath = System.IO.Path.GetDirectoryName(this.outputFilePath) + "\\O" + this.operatorId + "_filterChoice.bat";

            //目前输入文件统一转换为utf-8
            UTF8Encoding utf8 = new UTF8Encoding(false);
            StreamWriter streamWriter = new StreamWriter(filterBatPath, false, utf8);
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