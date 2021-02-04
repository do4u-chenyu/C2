﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2.IAOLab.WebEngine.Boss.Option.BaseOption
{
    public class AxisPointer : BaseEOption
    {
        public override string ToString()
        {
            return Common.GetOptionValue(this, this.FlagDic, this.TypeDic);
        }
        public AxisPointer()
        {
            Common.InitOptionFlag(this, FlagDic, TypeDic);
        }
    }
}
