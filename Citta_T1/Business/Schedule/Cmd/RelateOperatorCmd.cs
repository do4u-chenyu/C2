using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Citta_T1.Business.Schedule.Cmd
{
    class RelateOperatorCmd : OperatorCmd
    {
        public RelateOperatorCmd(Triple triple) : base(triple)
        {
        }

        public List<string> GenCmd()
        {
            List<string> cmds = new List<string>();

            string inputFilePath1 = inputFilePaths.First();//左输入文件
            string inputFilePath2 = inputFilePaths.Count > 1 ? inputFilePaths[1] : "";//右输入文件
            string outfieldLine1 = option.GetOption("outfield0");//左输出字段
            string outfieldLine2 = option.GetOption("outfield1");//右输出字段
            string inputFileSeparator1 = "\"" + this.separators[0] + "\"";
            string inputFileSeparator2 = "\"" + this.separators[1] + "\"";
            string inputFileEncoding1 = JudgeInputFileEncoding(inputFilePath1).ToString();
            string inputFileEncoding2 = JudgeInputFileEncoding(inputFilePath2).ToString();

            //关联条件拼接
            List<string> relateList = new List<string>();
            relateList.Add("0," + option.GetOption("factor1"));
            for (int i = 2; i <= GetOptionFactorCount(); i++)
                relateList.Add(option.GetOption("factor" + i.ToString()));

            string relateOption = "\"" + string.Join("|",relateList) + "\"";

            ReWriteBCPFile("relate");

            cmds.Add(string.Format("sbin\\relate.exe {0} {1} {2} {3} {4} {5} {6} {7} {8}| sbin\\iconv.exe -f gbk -t utf-8  >> {9}", 
                inputFilePath1, inputFileEncoding1, inputFileSeparator1,
                inputFilePath2, inputFileEncoding2, inputFileSeparator2,
                outfieldLine1, outfieldLine2, relateOption, this.outputFilePath));

            return cmds;
        }

    }
}