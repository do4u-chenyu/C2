using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2.Model
{
    interface IRemark
    {
        event EventHandler RemarkChanged;

        string Remark { get; set; }
    }
}
