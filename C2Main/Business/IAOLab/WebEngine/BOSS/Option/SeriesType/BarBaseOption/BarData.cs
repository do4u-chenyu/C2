﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2.IAOLab.WebEngine.Boss.Option.SeriesType.BarBaseOption
{
    public class BarData : BaseEOption
    {
        public override string ToString()
        {
            return Common.GetOptionValue(this, this.FlagDic, this.TypeDic);
        }
        public BarData()
        {
            Common.InitOptionFlag(this, FlagDic, TypeDic);
        }
    }
}
