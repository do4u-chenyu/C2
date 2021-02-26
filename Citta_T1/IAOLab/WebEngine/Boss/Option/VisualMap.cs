using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.IAOLab.WebEngine.Boss.Option
{
    public class VisualMap : BaseEOption
    {
        public override string ToString()
        {
            return Common.GetOptionValue(this, this.FlagDic, this.TypeDic);
        }
        public VisualMap()
        {
            Common.InitOptionFlag(this, FlagDic, TypeDic);
            min = 0;
            splitNumber = 5;
            show = "true";
            x = Common.FormatString("left");
            y = Common.FormatString("center");
        }

        public string show { get { return _show; } set { FlagDic["show"] = true; _show = value; } }
        public string x { get { return _x; } set { FlagDic["x"] = true; _x = value; } }
        public string y { get { return _y; } set { FlagDic["y"] = true; _y = value; } }
        public int min { get { return _min; } set { FlagDic["min"] = true; _min = value; } }
        public int max { get { return _max; } set { FlagDic["max"] = true; _max = value; } }
        public int splitNumber { get { return _splitNumber; } set { FlagDic["splitNumber"] = true; _splitNumber = value; } }

        string _show;
        string _x;
        string _y;
        int _min;
        int _max;
        int _splitNumber;
    }
}
