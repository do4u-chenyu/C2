using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Citta_T1.Business.Schedule
{
    class Engine
    {
        private Triple triple;
        private List<string> inputFilePaths = new List<string>();
        private OperatorOption option;
        private string outputFilePath;

        public delegate string FuncDelegate();
        Dictionary<ElementSubType, FuncDelegate> operatorFuncDict = new Dictionary<ElementSubType, FuncDelegate>();

        public Engine(Triple triple)
        {
            this.triple = triple;
            triple.DataElements.ForEach(c => inputFilePaths.Add(c.GetPath()));
            option = (triple.OperateElement.GetControl as MoveOpControl).Option;
            outputFilePath = triple.ResultElement.GetPath();
            AddDict();
        }

        public string ExcuteCmd()
        {
            return FuncCmd(operatorFuncDict[triple.OperateElement.SubType]);
        }

        private void AddDict()
        {
            operatorFuncDict.Add(ElementSubType.MaxOperator, MaxUnit);
            operatorFuncDict.Add(ElementSubType.FilterOperator, FilterUnit);
            operatorFuncDict.Add(ElementSubType.JoinOperator, JoinUnit);
            operatorFuncDict.Add(ElementSubType.CollideOperator, CollideUnit);
            operatorFuncDict.Add(ElementSubType.UnionOperator, UnionUnit);
            operatorFuncDict.Add(ElementSubType.DifferOperator, DifferUnit);
            operatorFuncDict.Add(ElementSubType.RandomOperator, RandomUnit);
            operatorFuncDict.Add(ElementSubType.MinOperator, MinUnit);
            operatorFuncDict.Add(ElementSubType.AvgOperator, AvgUnit);
            operatorFuncDict.Add(ElementSubType.SortOperator, SortUnit);
            operatorFuncDict.Add(ElementSubType.FreqOperator, FreqUnit);
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


        private string FreqUnit()
        {
            Thread.Sleep(5000);
            return "echo freq";
        }

        private string SortUnit()
        {
            Thread.Sleep(5000);
            return "echo sort";
        }

        private string AvgUnit()
        {
            Thread.Sleep(5000);
            return "echo avg";
        }

        private string MinUnit()
        {
            Thread.Sleep(5000);
            return "echo min";
        }

        private string RandomUnit()
        {
            Thread.Sleep(5000);
            return "echo random";
        }

        private string DifferUnit()
        {
            Thread.Sleep(5000);
            return "echo differ";
        }

        private string UnionUnit()
        {
            Thread.Sleep(5000);
            return "echo union";
        }

        private string CollideUnit()
        {
            Thread.Sleep(5000);
            return "echo collide";
        }

        private string JoinUnit()
        {
            Thread.Sleep(5000);
            return "echo join";
        }

        public string FilterUnit()
        {
            string inputFilePath = inputFilePaths.First();

            //以后算子路径功能写完后去掉
            if (inputFilePath == "")
            {
                Thread.Sleep(5000);
                return "echo filter";
            }

            string outfieldLine = TransOutputField(option.GetOption("outfield").Split(','));

            string[] factor1 = option.GetOption("factor1").Split(',');
            string awkIfCmd = "$" + factor1[0] + TransChoiceToCmd(factor1[1]) + factor1[1];
            for (int i = 2; i <= option.OptionDict.Count() - 1; i++)
            {
                string[] tmpfactor = option.GetOption("factor" + i.ToString()).Split(',');
                awkIfCmd = awkIfCmd + " " + TransChoiceToCmd(tmpfactor[0]) + " $" + tmpfactor[1] + TransChoiceToCmd(tmpfactor[2]) + tmpfactor[3];
            }

            string cmd = string.Format("sbin\\awk.exe -F'\\t' '{{if({0}) print {1} }}' >> {2}", awkIfCmd, outfieldLine, this.outputFilePath);
            return cmd;
        }

        public string MaxUnit()
        {
            string inputFilePath = inputFilePaths.First();

            //以后算子路径功能写完后去掉
            if (inputFilePath == "")
            {
                Thread.Sleep(5000);
                return "echo max";
            }

            string inputfieldLine = (int.Parse(option.GetOption("maxfield")) + 1).ToString();
            string outfieldLine = TransOutputField(option.GetOption("outfield").Split(','));

            string cmd = string.Format("sbin\\sort.exe -k {0} {1} | sbin\\head.exe -n1 | sbin\\awk.exe -F'\\t' '{{ print {2}}}'>> {3}", inputfieldLine, inputFilePath, outfieldLine, this.outputFilePath);
            return cmd;
        }



        public string FuncCmd(FuncDelegate OperatorUnit)
        {
            return OperatorUnit();
        }

    }
}
