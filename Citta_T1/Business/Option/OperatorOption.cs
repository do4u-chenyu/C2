using Citta_T1.Business.Model;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Utils;
using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Citta_T1.Business.Option
{
     
    public class RegisterInfo
    {

        private string prefix;
        private int limit; //-1,-2代表左表右表，其他>=0的数字代表最大索引
        private int[] limitList;
        private TypeCode dataType;
        public RegisterInfo(string prefix, TypeCode dataType)
        {
            Init(prefix, dataType);
        }
        public RegisterInfo(string prefix, TypeCode dataType, int limit)
        {
            Init(prefix, dataType);
            this.limit = limit;
        }
        public RegisterInfo(string prefix, TypeCode dataType, int[] limitList)
        {
            Init(prefix, dataType);
            this.limitList = limitList;
        }
        private void Init(string prefix, TypeCode dataType)
        {
            this.prefix = prefix;
            this.dataType = dataType;
        }
        public string Prefix { get => prefix; set => prefix = value; }                       //配置字段名称
        public TypeCode DataType { get => dataType; set => dataType = value; }             
        public int[] FactorIndexLimit { get => limitList; set => limitList = value; }        //多个单选框( >=1 )索引构成的"factor" ,对应索引的上限列表
        public int OtherIndexLimit { get => limit; set => limit = value; }                   //单选框、复选框索引上限    


       

    }
    public class OperatorOption
    {
        private Dictionary<string, string> optionDict;
        private Dictionary<ElementSubType, RegisterInfo[]> allRegisterInfo;
        private int maxIndex0;
        private int maxIndex1;
        public OperatorOption()
        {
            optionDict = new Dictionary<string, string>();
            OptionPrefixInit();
        }
        private void OptionPrefixInit()
        {          
            this.allRegisterInfo = new Dictionary<ElementSubType, RegisterInfo[]>
            {
                [ElementSubType.AvgOperator] = new RegisterInfo[]        { new RegisterInfo("avgfield", TypeCode.Int32, -1) },

                [ElementSubType.CollideOperator] = new RegisterInfo[]    { new RegisterInfo("outfield", TypeCode.Int32, -1),
                                                                           new RegisterInfo("factor1", TypeCode.Int32, new int[] { -1, -2 }),
                                                                           new RegisterInfo("factorI", TypeCode.Int32, new int[] { 1, -1, -2 })},

                [ElementSubType.DataFormatOperator] = new RegisterInfo[] { new RegisterInfo("factor1", TypeCode.Int32, new int[] { -1}),
                                                                           new RegisterInfo("factorI", TypeCode.Int32, new int[] { -1})},

                [ElementSubType.DifferOperator] = new RegisterInfo[]     { new RegisterInfo("outfield", TypeCode.Int32, -1),
                                                                           new RegisterInfo("factor1", TypeCode.Int32, new int[] { -1, -2 }),
                                                                           new RegisterInfo("factorI", TypeCode.Int32, new int[] { 1, -1, -2 }) },

                [ElementSubType.FilterOperator] = new RegisterInfo[]     { new RegisterInfo("outfield", TypeCode.Int32, -1),
                                                                           new RegisterInfo("factor1", TypeCode.Int32, new int[] { -1, 5 }),
                                                                           new RegisterInfo("factorI", TypeCode.Int32, new int[] { 1, -1, 5 }) },

                [ElementSubType.FreqOperator] = new RegisterInfo[]      { new RegisterInfo("outfield", TypeCode.Int32, -1) },

                [ElementSubType.GroupOperator] = new RegisterInfo[]     {  new RegisterInfo("factor1", TypeCode.Int32, new int[] { -1 }),
                                                                           new RegisterInfo("factorI", TypeCode.Int32, new int[] { -1 })},

                [ElementSubType.KeywordOperator] = new RegisterInfo[]   { new RegisterInfo("outfield", TypeCode.Int32, -1),
                                                                          new RegisterInfo("dataSelectIndex", TypeCode.Int32, -1),
                                                                          new RegisterInfo("conditionSlect", TypeCode.Int32, 1),
                                                                          new RegisterInfo("keySelectIndex", TypeCode.Int32, -2),},

                [ElementSubType.MaxOperator] = new RegisterInfo[]       { new RegisterInfo("outfield", TypeCode.Int32, -1),
                                                                          new RegisterInfo("maxfield", TypeCode.Int32, -1) },

                [ElementSubType.MinOperator] = new RegisterInfo[]       { new RegisterInfo("outfield", TypeCode.Int32, -1),
                                                                          new RegisterInfo("minfield", TypeCode.Int32, -1) },

                [ElementSubType.RandomOperator] = new RegisterInfo[]    { new RegisterInfo("outfield", TypeCode.Int32, -1),
                                                                          new RegisterInfo("randomnum", TypeCode.Int32) },

                [ElementSubType.RelateOperator] = new RegisterInfo[]    { new RegisterInfo("outfield0", TypeCode.Int32, -1),
                                                                          new RegisterInfo("outfield1", TypeCode.Int32, -2),
                                                                          new RegisterInfo("factor1", TypeCode.Int32, new int[] { -1, -2 }),
                                                                          new RegisterInfo("factorI", TypeCode.Int32, new int[] { 1, -1, -2 })},

                [ElementSubType.SortOperator] = new RegisterInfo[]      { new RegisterInfo("outfield", TypeCode.Int32, -1),
                                                                          new RegisterInfo("sortfield", TypeCode.Int32, -1), },

                [ElementSubType.UnionOperator] = new RegisterInfo[]     { new RegisterInfo("factor1", TypeCode.Int32, new int[] { -1, -2 }),
                                                                          new RegisterInfo("factorI", TypeCode.Int32, new int[] { -1, -2 })},

                [ElementSubType.CustomOperator1] = new RegisterInfo[]   { new RegisterInfo("outfield0", TypeCode.Int32, -1),
                                                                          new RegisterInfo("path", TypeCode.String)},

                [ElementSubType.CustomOperator2] = new RegisterInfo[]   { new RegisterInfo("outfield0", TypeCode.Int32, -1),
                                                                          new RegisterInfo("outfield1", TypeCode.Int32, -2),
                                                                          new RegisterInfo("path", TypeCode.String)},
               
                [ElementSubType.PythonOperator] = new RegisterInfo[]    { new RegisterInfo("pyFullPath", TypeCode.String),
                                                                          new RegisterInfo("virtualMachine", TypeCode.String)},
            };

        }


        public Dictionary<string, string> OptionDict { get => optionDict; set => this.optionDict = value; }

        public string GetOption(string OpKey, string defaultValue = "")
        {
            if (!OptionDict.ContainsKey(OpKey) || String.IsNullOrEmpty(OptionDict[OpKey]))
                return defaultValue;
            return OptionDict[OpKey];
        }

        public string this[string key] { get { return optionDict[key]; } set { optionDict[key] = value; } }
        public List<string> Keys { get { return optionDict.Keys.ToList(); } }

        public override string ToString()
        {
            return string.Join(",", OptionDict.ToList());
        }

        public string[] GetOptionSplit(string OpKey, char separator = '\t', string defaultValue = "")
        {
            string[] ret = GetOption(OpKey, defaultValue).Split(separator);
            if (ret.Length == 1 && String.IsNullOrEmpty(ret[0]))
                ret = new string[0];
            return ret;
        }

        public void SetOption(string OpKey, string OpVaule)
        {
            OptionDict[OpKey] = OpVaule;
        }

        public void SetOption(string OpKey, int OpVaule)
        {
            SetOption(OpKey, OpVaule.ToString());
        }

        public void SetOption(string OpKey, bool OpVaule)
        {
            SetOption(OpKey, OpVaule.ToString());
        }

        public void SetOption(string OpKey, string[] OpVauleList)
        {
            SetOption(OpKey, String.Join("\t", OpVauleList));
        }

        public void SetOption(string OpKey, List<int> OpVauleList)
        {
            SetOption(OpKey, String.Join("\t", OpVauleList));
        }

        public void SetOption(string OpKey, List<string> OpVauleList)
        {
            SetOption(OpKey, String.Join("\t", OpVauleList));
        }

        public int KeysCount(string name)
        {
            int count = 0;
            List<string> keys = this.OptionDict.Keys.ToList();
            foreach (string key in keys)
            {
                if (key.Contains(name))
                    count += 1;
            }
            return count;
        }
        public void Clear()
        {
            OptionDict.Clear();
        }



        public void DealAbnormalOption(MoveOpControl opControl)
        {
            
            // 判断表头是否存在
            if (WithoutInputColumns(opControl))
                return;
            maxIndex0 = GetOptionSplit("columnname0").Length - 1;
            maxIndex1 = GetOptionSplit("columnname1").Length - 1;

            ElementSubType subType = OpUtil.SEType(opControl.SubTypeName);
            RegisterInfo[] registerInfo = this.allRegisterInfo[subType];
            Dictionary<string, RegisterInfo> factorInfo = new Dictionary<string, RegisterInfo>();
            foreach (RegisterInfo ori in registerInfo)
            {

                if (ori.Prefix.Contains("factor"))
                {
                    factorInfo[ori.Prefix] = ori;
                    continue;
                }
                // 配置项丢失检查
                if (String.IsNullOrEmpty(GetOption(ori.Prefix)))
                {
                    opControl.Status = ElementStatus.Null;
                    continue;
                }
                // 数据异常类型检查与处理
                if (ori.DataType == TypeCode.Int32)
                    DealNonIntType(ori, opControl);
            }
            if (factorInfo.Count == 0)
                return;
            //包含Factor字段的数据异常类型检查与处理
            DealFactorNotIntType(factorInfo, opControl);
        }
        private bool WithoutInputColumns(MoveOpControl opControl)
        {
            bool notHasInput0 = String.IsNullOrEmpty(GetOption("columnname0"));
            bool notHasInput1 = String.IsNullOrEmpty(GetOption("columnname1"));
            bool binaryInput = opControl.IsBinaryDimension();

            //表头不存在，索引越界无法判断，因而直接清空配置信息
            if (notHasInput1 && binaryInput || notHasInput0)
            {
                this.Clear();
                opControl.Status = ElementStatus.Null;
                return true;
            }
            return false;
        }
        private void DealNonIntType(RegisterInfo ori, MoveOpControl opControl)
        {
            // 非整形检查与处理
            bool notInt = false;
            string key = ori.Prefix;
            if (key.Contains("outfield") && IsNotAllInt(GetOptionSplit(key)))
                notInt = true;
            else if (!key.Contains("outfield") && !ConvertUtil.IsInt(this[ori.Prefix]))
                notInt = true;
            if (notInt)
            {
                ModifyInfo(opControl, key);
                return;
            }

            // 判断是否需要超限检查
            if (ori.OtherIndexLimit==0)
                return;
            // 索引值超限检查与处理
            
            bool outRange = false;
            string[] items = GetOptionSplit(key);
            int maxIndex = Array.ConvertAll(items, int.Parse).Max();
            if (ori.OtherIndexLimit == -1 && maxIndex > maxIndex0)
                outRange = true;
            else if (ori.OtherIndexLimit == -2 && maxIndex > maxIndex1)
                outRange = true;
            if (outRange)
                ModifyInfo(opControl,key);

        }
        private void DealFactorNotIntType(Dictionary<string, RegisterInfo> factorInfo, MoveOpControl opControl)
        {
            List<string> factors = Keys.FindAll(x => x.Contains("factor"));
            int[] limit0 = factorInfo["factor1"].FactorIndexLimit;
            int[] limit1 = factorInfo["factorI"].FactorIndexLimit;
            foreach (string factor in factors)
            {
                if (string.IsNullOrEmpty(this[factor]))
                {
                    opControl.Status = ElementStatus.Null;
                    continue;
                }

                string[] items = GetOptionSplit(factor);
                // Int型数据检查
                bool notInt=false;
                if (factor.Contains("factor1") && CheckFactorNonInt(items, limit0))
                    notInt = true;
                else if (!factor.Contains("factor1") && CheckFactorNonInt(items, limit1))
                    notInt = true;
                // 非Int型-异常情况处理
                if (notInt)
                    ModifyInfo(opControl, factor);
            }
        }
        private bool CheckFactorNonInt(String[] itemList, int[] maxIndexs)
        {
            // 索引数目小于正常数目，异常：直接返回
            int count = maxIndexs.Length;
            if (itemList.Length < count || IsNotAllInt(itemList.Take(count).ToArray()))
                return true;
            // 索引超限检查
            bool outRange = false;
            for (int i = 0; i < count; i++)
            {
                int index = Convert.ToInt32(itemList[i]);
                if (maxIndexs[i] == -1 && index > maxIndex0)
                    outRange = true;
                else if (maxIndexs[i] == -2 && index > maxIndex1)
                    outRange = true;
                else if ( maxIndexs[i] >=0 && maxIndexs[i] < index)
                    outRange = true;
            }
            return outRange;

        }
        private bool IsNotAllInt(string[] indexs)
        {
            foreach (string index in indexs)
            {
                if (!ConvertUtil.IsInt(index))
                    return true;
            }
            return false;
        }
        private void ModifyInfo(MoveOpControl control,string key)
        {
            this[key] = String.Empty;
            control.Status = ElementStatus.Null;
        }
    }
    

}