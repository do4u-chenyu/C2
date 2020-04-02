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
        private string inputFilePath;
        private OperatorOption option;

        public delegate string FuncDelegate();
        Dictionary<ElementSubType, FuncDelegate> operatorFuncDict = new Dictionary<ElementSubType, FuncDelegate>();

        public Engine(Triple triple)
        {
            this.triple = triple;
            inputFilePath = triple.DataElements.First().GetPath();
            option = (triple.OperateElement.GetControl as MoveOpControl).Option;
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
            Thread.Sleep(5000);
            return "echo filter";
        }

        public string MaxUnit()
        {
            if(inputFilePath == "")
            {
                Thread.Sleep(5000);
                return "echo max";
            }
            string maxfield = option.GetOption("maxfield");
            string[] outfield = option.GetOption("outfield").Split(',');

            string inputfieldLine = (int.Parse(maxfield) + 1).ToString();
            string outfieldLine = "{print $" + (int.Parse(outfield[0]) + 1).ToString();
            for (int i = 1; i < outfield.Length; i++)
            {
                outfieldLine = outfieldLine + "\"\\t\"$" + (int.Parse(outfield[i]) + 1).ToString();
            }
            outfieldLine = outfieldLine + "}";
            string cmd = string.Format("sbin\\sort.exe -k {0} {1} | sbin\\head.exe -n1 | sbin\\awk.exe -F'\\t' '{2}'> 1.txt",inputfieldLine, inputFilePath, outfieldLine);

            return cmd;
        }



        public string FuncCmd(FuncDelegate OperatorUnit)
        {
            return OperatorUnit();
        }

    }
}
