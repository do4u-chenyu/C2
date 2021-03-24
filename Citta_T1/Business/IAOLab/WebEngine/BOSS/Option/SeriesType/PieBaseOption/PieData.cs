using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2.IAOLab.WebEngine.Boss.Option.SeriesType.PieBaseOption
{
    public class PieData : BaseEOption
    {
        public override string ToString()
        {
            return Common.GetOptionValue(this, this.FlagDic, this.TypeDic);
        }
        public PieData()
        {
            Common.InitOptionFlag(this, FlagDic, TypeDic);
        }
    }
}
