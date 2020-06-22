using Citta_T1.Business.Model;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Utils;
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
        private MoveOpControl opControl;
        private int maxIndex0;
        private int maxIndex1;
        public OperatorOption(MoveOpControl opControl)
        {
            optionDict = new Dictionary<string, string>();
            this.opControl = opControl;
            OptionPrefixInit();
        }
        private void OptionPrefixInit()
        {          
            this.allRegisterInfo = new Dictionary<ElementSubType, RegisterInfo[]>
            {
                [ElementSubType.AvgOperator] = new RegisterInfo[]        { new RegisterInfo("avgfield", TypeCode.Int32, -1) },

                [ElementSubType.CollideOperator] = new RegisterInfo[]    { new RegisterInfo("outfield0", TypeCode.Int32, -1),
                                                                           new RegisterInfo("factor0", TypeCode.Int32, new int[] { -1, -2 }),
                                                                           new RegisterInfo("factorH", TypeCode.Int32, new int[] { 1, -1, -2 })},

                [ElementSubType.DataFormatOperator] = new RegisterInfo[] { new RegisterInfo("factor0", TypeCode.Int32, new int[] { -1}),
                                                                           new RegisterInfo("factorH", TypeCode.Int32, new int[] { -1})},

                [ElementSubType.DifferOperator] = new RegisterInfo[]     { new RegisterInfo("outfield0", TypeCode.Int32, -1),
                                                                           new RegisterInfo("factor0", TypeCode.Int32, new int[] { -1, -2 }),
                                                                           new RegisterInfo("factorH", TypeCode.Int32, new int[] { 1, -1, -2 }) },

                [ElementSubType.FilterOperator] = new RegisterInfo[]     { new RegisterInfo("outfield0", TypeCode.Int32, -1),
                                                                           new RegisterInfo("factor0", TypeCode.Int32, new int[] { -1, 5 }),
                                                                           new RegisterInfo("factorH", TypeCode.Int32, new int[] { 1, -1, 5 }) },

                [ElementSubType.FreqOperator] = new RegisterInfo[]       { new RegisterInfo("outfield0", TypeCode.Int32, -1) },

                [ElementSubType.GroupOperator] = new RegisterInfo[]      { new RegisterInfo("factor0", TypeCode.Int32, new int[] { -1 }),
                                                                           new RegisterInfo("factorH", TypeCode.Int32, new int[] { -1 })},

                [ElementSubType.KeywordOperator] = new RegisterInfo[]    { new RegisterInfo("outfield0", TypeCode.Int32, -1),
                                                                           new RegisterInfo("dataSelectIndex", TypeCode.Int32, -1),
                                                                           new RegisterInfo("conditionSlect", TypeCode.Int32, 1),
                                                                           new RegisterInfo("keySelectIndex", TypeCode.Int32, -2),},

                [ElementSubType.MaxOperator] = new RegisterInfo[]        { new RegisterInfo("outfield0", TypeCode.Int32, -1),
                                                                           new RegisterInfo("maxfield", TypeCode.Int32, -1) },

                [ElementSubType.MinOperator] = new RegisterInfo[]        { new RegisterInfo("outfield0", TypeCode.Int32, -1),
                                                                           new RegisterInfo("minfield", TypeCode.Int32, -1) },

                [ElementSubType.RandomOperator] = new RegisterInfo[]     { new RegisterInfo("outfield0", TypeCode.Int32, -1),
                                                                           new RegisterInfo("randomnum", TypeCode.Int32) },

                [ElementSubType.RelateOperator] = new RegisterInfo[]    { new RegisterInfo("outfield0", TypeCode.Int32, -1),
                                                                          new RegisterInfo("outfield1", TypeCode.Int32, -2),
                                                                          new RegisterInfo("factor0", TypeCode.Int32, new int[] { -1, -2 }),
                                                                          new RegisterInfo("factorH", TypeCode.Int32, new int[] { 1, -1, -2 })},

                [ElementSubType.SortOperator] = new RegisterInfo[]      { new RegisterInfo("outfield0", TypeCode.Int32, -1),
                                                                          new RegisterInfo("sortfield", TypeCode.Int32, -1), },

                [ElementSubType.UnionOperator] = new RegisterInfo[]     { new RegisterInfo("factor0", TypeCode.Int32, new int[] { -1, -2 }),
                                                                          new RegisterInfo("factorH", TypeCode.Int32, new int[] { -1, -2 })},

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
            foreach (string key in this.OptionDict.Keys)
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



        public void OptionValidating()
        {
            
            // 判断表头是否存在
            if (WithoutInputColumns())
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
                    DealNonIntType(ori);
            }
            if (factorInfo.Count == 0)
                return;
            //包含Factor字段的数据异常类型检查与处理
            DealFactorNotIntType(factorInfo);
        }
        private bool WithoutInputColumns()
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
        private void DealNonIntType(RegisterInfo ori)
        {
            // 非整形检查与处理
           
            string key = ori.Prefix;
            string[] items = GetOptionSplit(key);
            bool notInt = key.Contains("outfield") ? IsNotAllInt(items) : !ConvertUtil.IsInt(this[key]);
            if (notInt)
            {
                ModifyInfo(key);
                return;
            }

            // 判断是否需要超限检查
            if (ori.OtherIndexLimit == 0)
                return;
            // 索引值超限检查与处理
            
            bool outRange = false;
            int maxIndex = Array.ConvertAll(items, int.Parse).Max();
            if (ori.OtherIndexLimit == -1)
                outRange = maxIndex > maxIndex0;
            else if (ori.OtherIndexLimit == -2 )
                outRange = maxIndex > maxIndex1;
            if (outRange)
                ModifyInfo(key);

        }
        private void DealFactorNotIntType(Dictionary<string, RegisterInfo> factorInfo)
        {
            List<string> factors = Keys.FindAll(x => x.Contains("factor"));
            if (factors.Count == 0) return;
            int realCount = GetRealFactorOder(factors);
            int[] limit0 = factorInfo["factor0"].FactorIndexLimit;
            int[] limit1 = factorInfo["factorH"].FactorIndexLimit;
            for (int i = 0; i < realCount; i++)
            {
                string factor = "factor" + i.ToString();
                if (string.IsNullOrEmpty(GetOption(factor)))
                {
                    opControl.Status = ElementStatus.Null;
                    continue;
                }

                string[] items = GetOptionSplit(factor);
                // Int型数据检查
                bool notInt = factor.Contains("factor0") ? CheckFactorNonInt(items, limit0) : CheckFactorNonInt(items, limit1);
                // 非Int型-异常情况处理
                if (notInt)
                    ModifyInfo(factor);
            }
        }
        private int GetRealFactorOder(List<string> factors)
        {
            List<int> nums = new List<int>();
            foreach (string factor in factors)             
            {
                string num = factor.Substring(factor.Length - 1, 1);
                if (ConvertUtil.IsInt(num))
                    nums.Add(int.Parse(num));
            }
            return nums.Max();
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
                if (maxIndexs[i] == -1)
                    outRange = index > maxIndex0;
                else if (maxIndexs[i] == -2)
                    outRange = index > maxIndex1;
                else if ( maxIndexs[i] >=0)
                    outRange = index > maxIndexs[i];
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
        private void ModifyInfo(string key)
        {
            this[key] = String.Empty;
            opControl.Status = ElementStatus.Null;
        }
    }
    

}