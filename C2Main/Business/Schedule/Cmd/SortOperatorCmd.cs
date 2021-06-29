﻿using C2.Model.Widgets;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace C2.Business.Schedule.Cmd
{
    class SortOperatorCmd : OperatorCmd
    {
        public SortOperatorCmd(Triple triple) : base(triple)
        {
        }
        public SortOperatorCmd(OperatorWidget operatorWidget) : base(operatorWidget)
        {
        }
        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();
            string inputFilePath = inputFilePaths.First();//输入文件
            string sortLine = TransInputLine(option.GetOption("sortfield"));//待排序字段

            string tmpFirstRow = option.GetOption("firstRow");
            string tmpEndRow = option.GetOption("endRow");
            string firstRow = ConvertUtil.TryParseInt(tmpFirstRow) > 0 ? tmpFirstRow : "1";//开始行数
            string endRowCmd = String.Empty;
            //endrow不填默认输出firstrow到最后一行
            if (!String.IsNullOrEmpty(tmpEndRow))
            {
                string endRow = ConvertUtil.TryParseInt(tmpEndRow) <= 0 || ConvertUtil.TryParseInt(tmpEndRow) - ConvertUtil.TryParseInt(firstRow) <= 0 ? firstRow : tmpEndRow;//结束行数
                endRowCmd = string.Format("| sbin\\head.exe -n{0}", endRow);
            }
            string outField = TransOutputField(option.GetOptionSplit("outfield0"));//输出字段

            //是否去重(是对整个文件去重)、升降序
            string repetition = option.GetOption("noRepetition").ToLower() == "true" ? string.Format("sbin\\sort.exe {0} -u |", this.sortConfig) : "";
            string order = option.GetOption("ascendingOrder").ToLower() == "true" ? String.Empty : "-r";
            string type = option.GetOption("sortByNum").ToLower() == "true" ? "-n" : String.Empty;

            //重写表头（覆盖）
            ReWriteBCPFile();

            cmds.Add(string.Format("{0} | {1} sbin\\sort.exe {2} -t\"{9}\" {10} -k{3} {4} {5} | sbin\\tail.exe -n +{6} | sbin\\awk.exe -F\"{9}\" -v OFS='\\t' '{{ print {7}}}'>> {8}", TransInputfileToCmd(inputFilePath), repetition, this.sortConfig, sortLine, order, endRowCmd, firstRow, outField, this.outputFilePath, this.separators[0], type));
            return cmds;
        }

    }
}