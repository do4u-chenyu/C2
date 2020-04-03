using Citta_T1.Business.Option;
using Citta_T1.Controls.Move;
using System;
using System.Collections.Generic;
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

        public OperatorCmd(Triple triple)
        {
            this.triple = triple;
            triple.DataElements.ForEach(c => inputFilePaths.Add(c.GetPath()));
            option = (triple.OperateElement.GetControl as MoveOpControl).Option;
            outputFilePath = triple.ResultElement.GetPath();
        }

        public string TransChoiceToCmd(string choice)
        {
            switch (choice)
            {
                case "小于等于 ≦": return "<=";
                case "大于等于 ≥": return ">=";
                case "大于 &gt": return ">";
                case "小于 &lt": return "<";
                case "等于 =": return "=";
                case "不等于 ≠": return "!=";
                case "AND": return "&&";
                case "OR": return "||";
            }
            return "无该选项";
        }

        public string TransOutputField(string[] outfield)
        {
            string outfieldLine = " $" + (int.Parse(outfield[0]) + 1).ToString();
            for (int i = 1; i < outfield.Length; i++)
            {
                outfieldLine = outfieldLine + "\"\\t\"$" + (int.Parse(outfield[i]) + 1).ToString();
            }
            return outfieldLine;
        }

    }
}
