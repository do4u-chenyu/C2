﻿using System;
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
            string inputFilePath = inputFilePaths.First();//输入文件

            //是否去重(是对整个文件去重)、升降序
            string repetition = option.GetOption("noRepetition") == "True" ? string.Format("sbin\\sort.exe {0} -u |",this.sortConfig) : "";
            string order = option.GetOption("ascendingOrder") == "True" ? string.Format("sbin\\sort.exe {0} -n ",this.sortConfig) : string.Format("sbin\\sort.exe {0} -nr ",this.sortConfig);

            //待统计频率字段合并
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

            cmds.Add(string.Format("{0} | {1} sbin\\awk.exe -F'\\t' -v OFS='\\t' '{{ print {2}}}' | sbin\\sort.exe {3} | sbin\\uniq.exe -c | {4} | sbin\\awk.exe -F' ' -v OFS='\\t' '{{ print {5}}}'>> {6}", TransInputfileToCmd(inputFilePath),repetition, infieldLine,this.sortConfig, order, outfieldLine, this.outputFilePath));

            return cmds;
        }

    }
}