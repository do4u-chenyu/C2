using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Text;

namespace Citta_T1.Business.Schedule.Cmd
{
    class OperatorCmd
    {
        public Triple triple;
        public List<string> inputFilePaths = new List<string>();
        public OperatorOption option;
        public string outputFilePath;
        public string operatorId;
        public string sortConfig;
        public string outputFileTitle;
        public List<string> separators;
        public OperatorCmd(Triple triple)
        {
            this.triple = triple;
            triple.DataElements.ForEach(c => inputFilePaths.Add(c.FullFilePath));
            this.option = (triple.OperateElement.InnerControl as MoveOpControl).Option;
            this.outputFileTitle = this.option.GetOption("columnname0");
            this.outputFilePath = triple.ResultElement.FullFilePath;
            this.operatorId = triple.OperateElement.ID.ToString();
            this.sortConfig = " -S 200M -T " + Global.WorkspaceDirectory;
            InitSeparator();
        }


        public void ReWriteBCPFile(string className = "null")
        {
            using (StreamWriter sw = new StreamWriter(this.outputFilePath, false, Encoding.UTF8))
            {
                string columns = String.Join("\t", GenOutTitleList(className));
                sw.WriteLine(columns);
                sw.Flush();
            }
            //File.WriteAllText(this.outputFilePath, columns);

        }

        public List<string> GenOutTitleList(string className)
        {
            string[] titleList = this.outputFileTitle.Split('\t');
            List<string> outTitleList = new List<string>();
            if (className == "relate")
            {
                string[] col0 = this.option.GetOptionSplit("columnname0");
                string[] col1 = this.option.GetOptionSplit("columnname1");

                foreach (string ind in option.GetOptionSplit("outfield0"))
                {
                    outTitleList.Add(col0[int.Parse(ind)]);
                }
                foreach (string ind in option.GetOptionSplit("outfield1"))
                {
                    outTitleList.Add(col1[int.Parse(ind)]);
                }
            }
            else if (className == "union" || className == "format")
            {
                string[] col0 = this.option.GetOptionSplit("outname");
                foreach (string ind in col0)
                {
                    outTitleList.Add(ind);
                }
            }
            else if (className == "differ" || className == "collide")
            {
                string[] col0 = option.GetOptionSplit("columnname0");
                foreach (string ind in option.GetOptionSplit("outfield0"))
                {
                    outTitleList.Add(col0[int.Parse(ind)]);
                }
            }
            else
            {
                foreach (string ind in option.GetOptionSplit("outfield0"))
                {
                    outTitleList.Add(titleList[int.Parse(ind)]);
                }
                if (className == "freq")
                {
                    outTitleList.Add("频率统计结果");
                }
            }
            return outTitleList;

        }

        public void InitSeparator()
        {
            this.separators = new List<string>();
            foreach (ModelElement me in triple.DataElements)
            {
                separators.Add(me.Type == ElementType.DataSource ? me.Separator.ToString() : "\t");
            }
        }

        public string TransOFSToCmd(string separator)
        {
            return separator == "|" ? "\t" : "|" ;
        }

        public string TransChoiceToCmd(string choice)
        {
            switch (choice)
            {
                case "0": return ">";
                case "1": return "<";
                case "2": return "==";
                case "3": return ">=";
                case "4": return "<=";
                case "5": return "!=";
                default: return "=="; //设一个参数越界的默认值，除非用户自己修改xml文件，一般不会进来
            }
        }

        public string TransAndOrToCmd(string choice)
        {
            switch (choice)
            {
                case "0": return "&&";
                case "1": return "||";
                default: return "&&"; //设一个参数越界的默认值，除非用户自己修改xml文件，一般不会进来
            }
        }

        public string TransInputLine(string optionLine)
        {
            return (int.Parse(optionLine) + 1).ToString();
        }

        public string TransOutputField(string[] outField)
        {
            string outFieldLine = " $" + (int.Parse(outField[0]) + 1).ToString();
            for (int i = 1; i < outField.Length; i++)
            {
                outFieldLine = outFieldLine + ",$" + (int.Parse(outField[i]) + 1).ToString();
            }
            return outFieldLine;
        }

        public string TransDifferOutputField(string[] outField)
        {
            string outFieldLine = " $" + (int.Parse(outField[0]) + 2).ToString();
            for (int i = 1; i < outField.Length; i++)
            {
                outFieldLine = outFieldLine + ",$" + (int.Parse(outField[i]) + 2).ToString();
            }
            return outFieldLine;
        }
        public string TransConditionToCmd(string condition)
        {
            try
            {
                int.Parse(condition);
                return condition;
            }
            catch
            {
                if (condition.IndexOf('\\') >= 0)
                    condition = condition.Replace("\\", "\\\\");
                if (condition.IndexOf('\"') >= 0)
                    condition = condition.Replace("\"", "\\\"");
                return "\"" + condition + "\"";
            }
        }




        public string TransInputfileToCmd(string inputFile)
        {
            /*
             * 判断条件
             * 1、是否是excel,是:cat_xls，否:2
             * 2、bcp是什么格式，判断encoding,是gbk:tail | iconv ,否：tail
             */
            string filename = System.IO.Path.GetFileName(inputFile);
            if (filename.IndexOf(".xls") > 0)
            {
                return string.Format("sbin\\cat_xls.exe {0} | sbin\\iconv.exe -f gbk -t utf-8 -c | sbin\\tr.exe -d '\\r' ", inputFile);
            }
            else if (JudgeInputFileEncoding(inputFile) == OpUtil.Encoding.GBK)
            {
                return string.Format("sbin\\tail.exe -n +2  {0} | sbin\\iconv.exe -f gbk -t utf-8 -c | sbin\\tr.exe -d '\\r' ", inputFile);
            }
            else
            {
                return string.Format("sbin\\tail.exe -n +2 {0} | sbin\\tr.exe -d '\\r' ", inputFile);
            }
        }

        public int GetOptionFactorCount()
        {
            return option.KeysCount("factor");
        }

        public OpUtil.Encoding JudgeInputFileEncoding(string inputFile)
        {
            return triple.DataElements[inputFilePaths.IndexOf(inputFile)].Encoding;
        }

    }
}
