using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2.IAOLab.WebEngine.Boss.Option.BaseOption
{
    public enum xAxisType
    {
        [Remark("'value'")]
        value = 1,
        [Remark("'category'")]
        category,
        [Remark("'time'")]
        time ,
        [Remark("'log'")]
        log
    }
}
