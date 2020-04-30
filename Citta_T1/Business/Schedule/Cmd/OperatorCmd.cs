using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Business.Schedule.Cmd
{
    class OperatorCmd
    {
        Triple triple;
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
            triple.DataElements.ForEach(c => inputFilePaths.Add(c.GetPath()));
            this.option = (triple.OperateElement.GetControl as MoveOpControl).Option;
            this.outputFilePath = triple.ResultElement.GetPath();
            this.outputFileTitle = this.option.GetOption("columnname");
            this.operatorId = triple.OperateElement.ID.ToString();
            this.sortConfig = " -S 200M -T " + Global.WorkspaceDirectory;
            InitSeparator();
        }

        public void InitSeparator()
        {
            this.separators = new List<string>();
            foreach (ModelElement me in triple.DataElements)
            {
                if (me.Type == ElementType.DataSource)
                {
                    separators.Add((me.GetControl as MoveDtControl).Separator.ToString());
                }
                else
                {
                    separators.Add("\t");
                }
            }
        }

        public string TransOFSToCmd(string separator)
        {
            if(separator == "|")
            {
                return "\t";
            }
            else
            {
                return "|";
            }
            
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
            }
            return "无该选项";
        }

        public string TransAndOrToCmd(string choice)
        {
            switch (choice)
            {
                case "0":return "&&";
                case "1":return "||";
            }
            return "and、or无该选项";
        }

        public string TransInputLine(string optionLine)
        {
            return (int.Parse(optionLine) + 1).ToString();
        }

        public string TransOutputField(string[] outfield)
        {
            string outfieldLine = " $" + (int.Parse(outfield[0]) + 1).ToString();
            for (int i = 1; i < outfield.Length; i++)
            {
                outfieldLine = outfieldLine + ",$" + (int.Parse(outfield[i]) + 1).ToString();
            }
            return outfieldLine;
        }

        public string TransDifferOutputField(string[] outfield)
        {
            string outfieldLine = " $" + (int.Parse(outfield[0]) + 2).ToString();
            for (int i = 1; i < outfield.Length; i++)
            {
                outfieldLine = outfieldLine + ",$" + (int.Parse(outfield[i]) + 2).ToString();
            }
            return outfieldLine;
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
                return '"' + condition + '"';
            }
        }




        public string TransInputfileToCmd(string inputfile)
        {
            /*
             * 判断条件
             * 1、是否是excel,是:cat_xls，否:2
             * 2、bcp是什么格式，判断encoding,是gbk:tail | iconv ,否：tail
             */
            string filename = System.IO.Path.GetFileName(inputfile);
            if (filename.IndexOf(".xls") > 0)
            {
                return string.Format("sbin\\cat_xls.exe {0} | sbin\\iconv.exe -f gbk -t utf-8 | sbin\\tr.exe -d '\\r' ", inputfile);
            }
            else
            {
                if(JudgeInputFileEncoding(inputfile) == DSUtil.Encoding.GBK)
                {
                    return string.Format("sbin\\tail.exe -n +2  {0} | sbin\\iconv.exe -f gbk -t utf-8 | sbin\\tr.exe -d '\\r' ", inputfile);
                }
                else
                {
                    return string.Format("sbin\\tail.exe -n +2 {0} | sbin\\tr.exe -d '\\r' ", inputfile);
                }
            }
        }

        public int GetOptionFactorCount()
        {
            int num = 0;
            foreach (string key in option.OptionDict.Keys)
            {
                if (key.Contains("factor"))
                {
                    num++;
                }
            }
            return num; 
        }

        public DSUtil.Encoding JudgeInputFileEncoding(string inputfile)
        {
            return triple.DataElements[inputFilePaths.IndexOf(inputfile)].Encoding;
        }

    }
}
