using C2.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.IAOLab.WebEngine.Boss.Option.SeriesType
{
    class SeriesMap : BaseEOption, ISeries
    {
        public override string ToString()
        {
            return Common.GetOptionValue(this, this.FlagDic, this.TypeDic, "{}", "{type:'bar'}");
        }
        public string GetTypeStr()
        {
            return this.ToString();
        }
        public SeriesMap()
        {
            Common.InitOptionFlag(this, FlagDic, TypeDic);
            type = Common.FormatString("map");
        }

        public string type { get { return _type; } set { FlagDic["type"] = true; _type = value; } }
        public string name { get { return _name; } set { FlagDic["name"] = true; _name = value; } }
        public string mapType { get { return _mapType; } set { FlagDic["mapType"] = true; _mapType = value; } }
        public string roam { get { return _roam; } set { FlagDic["roam"] = true; _roam = value; } }
        public MapLabel label { get { return _label; } set { FlagDic["label"] = true; _label = value; } }
        public Encode encode { get { return _encode; } set { FlagDic["encode"] = true; _encode = value; } }

        string _type;
        string _name;
        string _mapType;
        string _roam;
        MapLabel _label;
        Encode _encode;
    }
}
