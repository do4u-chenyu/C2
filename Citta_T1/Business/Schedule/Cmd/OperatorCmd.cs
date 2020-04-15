﻿using Citta_T1.Business.Option;
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

        public string TransConditionToCmd(string condition)
        {
            try
            {
                int.Parse(condition);
                return condition;
            }
            catch
            {
                return "'" + condition + "'";
            }


        }

    }
}
