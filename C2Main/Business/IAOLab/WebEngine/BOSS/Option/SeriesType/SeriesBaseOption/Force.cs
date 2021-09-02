using C2.IAOLab.WebEngine.Boss;
using C2.IAOLab.WebEngine.Boss.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption
{
    public class Force : BaseEOption
    {
        public override string ToString()
        {
            return Common.GetOptionValue(this, this.FlagDic, this.TypeDic);
        }
        public Force()
        {
            Common.InitOptionFlag(this, FlagDic, TypeDic);
        }
        public int repulsion { get { return _repulsion; } set { FlagDic["repulsion"] = true; _repulsion = value; } }
        public string gravity { get { return _gravity; } set { FlagDic["gravity"] = true; _gravity = value; } }
        public int friction { get { return _friction; } set { FlagDic["friction"] = true; _friction = value; } }
        public int edgeLength { get { return _edgeLength; } set { FlagDic["edgeLength"] = true; _edgeLength = value; } }
        public string layoutAnimation { get { return _layoutAnimation; } set { FlagDic["layoutAnimation"] = true; _layoutAnimation = value; } }

        int _repulsion;
        string _gravity;
        int _friction;
        int _edgeLength;
        string _layoutAnimation;

    }
}
