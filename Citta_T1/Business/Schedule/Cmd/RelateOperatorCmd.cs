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
            string outfieldLine1 = TransDifferOutputField(option.GetOption("outfield1").Split(','));//输出字段?
            string outfieldLine2 = TransDifferOutputField(option.GetOption("outfield2").Split(','));//输出字段?
            string inputFileSeparator1 = this.separators[0];
            string inputFileSeparator2 = this.separators[1];
            string inputFileEncoding1 = JudgeInputFileEncoding(inputFilePath1).ToString();
            string inputFileEncoding2 = JudgeInputFileEncoding(inputFilePath2).ToString();

            //关联条件拼接
            List<List<string>> relateList = new List<List<string>>();
            List<string> relateTmpList = new List<string>();
            string factor1 = option.GetOption("factor1");
            relateTmpList.Add(factor1);
            for (int i = 2; i <= GetOptionFactorCount(); i++)
            {
                string[] tmpfactor = option.GetOption("factor" + i.ToString()).Split(',');
                string andor = tmpfactor[0];
                if (andor == "0")
                {
                    //如果是AND，那么添加到当前列表
                    relateTmpList.Add(string.Join(",",tmpfactor.Skip(1).Take(2).ToArray()));
                }
                else
                {
                    //如果是or，开启一个新列表
                    relateList.Add(relateTmpList);
                    relateTmpList.Clear();
                    relateTmpList.Add(string.Join(",",tmpfactor.Skip(1).Take(2).ToArray()));
                }
            }
            relateList.Add(relateTmpList);


            foreach (List<string> tmpList in relateList)
            {
                //每个循环处理一组关系
                string factor = string.Join("|",tmpList);
                cmds.Add(string.Format("sbin\\relate.exe {0} {1} {2} {3} {4} {5} {6} {7} {8} ", inputFilePath1, inputFileEncoding1, inputFileSeparator1, inputFilePath2, inputFileEncoding2, inputFileSeparator2, outfieldLine1, outfieldLine2, factor));
            }

            return cmds;
        }

    }
}