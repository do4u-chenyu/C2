using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.IAOLab.WebEngine.Boss.Option
{
    class MapLabel : BaseEOption
    {
        public override string ToString()
        {
            return Common.GetOptionValue(this, this.FlagDic, this.TypeDic);
        }
        public MapLabel()
        {
            Common.InitOptionFlag(this, FlagDic, TypeDic);
        }

        public string normal { get { return _normal; } set { FlagDic["normal"] = true; _normal = value; } }
        public string emphasis { get { return _emphasis; } set { FlagDic["emphasis"] = true; _emphasis = value; } }

        string _normal;
        string _emphasis;

    }

}
