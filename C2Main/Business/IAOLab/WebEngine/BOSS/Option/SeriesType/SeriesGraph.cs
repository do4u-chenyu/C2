using C2.IAOLab.WebEngine.Boss.Option.SeriesType.BarBaseOption;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType.LineBaseOption;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2.IAOLab.WebEngine.Boss.Option.SeriesType
{
    public class SeriesGraph : BaseEOption, ISeries
    {
        public override string ToString()
        {
            return Common.GetOptionValue(this, this.FlagDic, this.TypeDic, "{}", "{type:'graph'}");
        }
        public string GetTypeStr()
        {
            return this.ToString();
        }
        public SeriesGraph()
        {
            Common.InitOptionFlag(this, FlagDic, TypeDic);
            type = Common.FormatString("tree");
        }
        public string type { get { return _type; } set { FlagDic["type"] = true; _type = value; } }
        public string layout { get { return _layout; } set { FlagDic["layout"] = true; _layout = value; } }
        public string draggable { get { return _draggable; } set { FlagDic["draggable"] = true; _draggable = value; } }
        public string categories { get { return _categories; } set { FlagDic["categories"] = true; _categories = value; } }
        public Label label { get { return _label; } set { FlagDic["label"] = true; _label = value; } }
        public ItemStyle itemStyle { get { return _itemStyle; } set { FlagDic["itemStyle"] = true; _itemStyle = value; } }
        public string data { get { return _data; } set { FlagDic["data"] = true; _data = value; } }
        public string links { get { return _links; } set { FlagDic["links"] = true; _links = value; } }
        public Tooltip tooltip { get { return _tooltip; } set { FlagDic["tooltip"] = true; _tooltip = value; } }


        string _type;
        string _layout;
        string _draggable;
        string _categories;
        Label _label;
        ItemStyle _itemStyle;
        string _data;
        string _links;
        Tooltip _tooltip;
    }
}