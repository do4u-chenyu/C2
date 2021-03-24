using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2.IAOLab.WebEngine.Boss.Option.SeriesType.LineBaseOption
{
    public class MarkPointData : BaseEOption
    {
        public override string ToString()
        {
            return Common.GetOptionValue(this, this.FlagDic, this.TypeDic);
        }
        public MarkPointData()
        {
            Common.InitOptionFlag(this, FlagDic, TypeDic);
        }
    }
}
