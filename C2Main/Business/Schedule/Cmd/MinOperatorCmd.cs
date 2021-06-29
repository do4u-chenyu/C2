﻿using System.Collections.Generic;
using System.Linq;
using C2.Model.Widgets;

namespace C2.Business.Schedule.Cmd
{
    class MinOperatorCmd : OperatorCmd
    {
        public MinOperatorCmd(Triple triple) : base(triple)
        {
        }
        public MinOperatorCmd(OperatorWidget operatorWidget) : base(operatorWidget)
        {
        }

        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            string inputFilePath = inputFilePaths.First();//输入文件
            string minField = TransInputLine(option.GetOption("minfield")); //最小值字段
            string outField = TransOutputField(option.GetOptionSplit("outfield0"));//输出字段

            //重写表头（覆盖）
            ReWriteBCPFile();

            cmds.Add(string.Format("{0} | sbin\\sort.exe {1} -t\"{5}\" -n -k {2} | sbin\\head.exe -n1 | sbin\\awk.exe -F\"{5}\" -v OFS='\\t' '{{ print {3}}}'>> {4}", TransInputfileToCmd(inputFilePath), this.sortConfig, minField, outField, this.outputFilePath, this.separators[0]));
            return cmds;
        }

    }
}