using Citta_T1.Business.Model;
using Citta_T1.Controls.Move.Op;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Citta_T1.Utils
{
    public class CheckUtil
    {
        /*
         * 仍在施工中
         */
        private MoveOpControl opControl;
        private Dictionary<ElementSubType, List<string>> optionPrefix;
        public CheckUtil(MoveOpControl opControl)
        {
            this.opControl = opControl;
            OptionPrefixInit();
        }
        private void OptionPrefixInit()
        {
            optionPrefix = new Dictionary<ElementSubType, List<string>>
            {
                [ElementSubType.AvgOperator] = new List<string>()        { "avgfield" },
                [ElementSubType.CollideOperator] = new List<string>()    { "outfield", "factor1" },
                [ElementSubType.DataFormatOperator] = new List<string>() { "outfield", "factor1" },
                [ElementSubType.DifferOperator] = new List<string>()     { "outfield", "factor1" },
                [ElementSubType.FilterOperator] = new List<string>()     { "outfield", "factor1" },
                [ElementSubType.FreqOperator] = new List<string>()       { "outfield",  "repetition", "noRepetition",
                                                                           "ascendingOrder", "descendingOrder" },
                [ElementSubType.GroupOperator] = new List<string>()      { "factor1", "repetition", "noRepetition",
                                                                           "ascendingOrder", "descendingOrder", "sortByNum", "sortByString" },
                [ElementSubType.KeywordOperator] = new List<string>()    { "outfield", "dataSelectIndex", "keySelectIndex", "conditionSlect" },
                [ElementSubType.MaxOperator] = new List<string>()        { "outfield", "maxfield" },
                [ElementSubType.MinOperator] = new List<string>()        { "outfield", "minfield" },
                [ElementSubType.RandomOperator] = new List<string>()     { "outfield", "randomnum" },
                [ElementSubType.RelateOperator] = new List<string>()     { "factor1" },
                [ElementSubType.SortOperator] = new List<string>()       { "outfield", "sortfield", "repetition", "noRepetition", 
                                                                           "ascendingOrder", "descendingOrder", "sortByNum", "sortByString", "firstRow" },
                [ElementSubType.UnionOperator] = new List<string>()      { "outname", "noRepetition", "repetition" , "factor1" },
                [ElementSubType.CustomOperator1] = new List<string>()    { "outfield0" },
                [ElementSubType.CustomOperator2] = new List<string>()    { "outfield0", "outfield1" },
                [ElementSubType.PythonOperator] = new List<string>()     { "virtualMachine" }
            };

        }
        public void CheckOption()
        {
            /*
             * 配置项丢失检查
             */
           foreach (string prefix in optionPrefix[OpUtil.SEType(opControl.SubTypeName)])
           {
                opControl.Status = String.IsNullOrEmpty(opControl.Option.GetOption(prefix)) ? ElementStatus.Null : opControl.Status;
           }
            /*
             * 数据类型、字段数目检测
             */
            foreach (string key in opControl.Option.OptionDict.Keys.ToArray())
            {
                // 不包含outfield就没必要检查了
                if (!key.Contains("outfield"))
                    continue;
                string[] indexs = opControl.Option.GetOptionSplit(key);
                foreach (string index in indexs)
                {
                    if (!ConvertUtil.IsInt(index))
                    {
                        opControl.Option[key] = String.Empty;
                        opControl.Status = ElementStatus.Null;
                    }
                }
            }

            /*
            *  索引越界检测
            */

        }
    }
}
