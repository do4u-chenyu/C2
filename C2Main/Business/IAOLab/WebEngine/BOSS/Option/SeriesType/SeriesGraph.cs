using C2.Business.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption;
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
        public string symbol { get { return _symbol; } set { FlagDic["symbol"] = true; _symbol = value; } }
        public string symbolSize { get { return _symbolSize; } set { FlagDic["symbolSize"] = true; _symbolSize = value; } }
        public string smooth { get { return _smooth; } set { FlagDic["smooth"] = true; _smooth = value; } }
        public string draggable { get { return _draggable; } set { FlagDic["draggable"] = true; _draggable = value; } }
        public string categories { get { return _categories; } set { FlagDic["categories"] = true; _categories = value; } }
        public Label label { get { return _label; } set { FlagDic["label"] = true; _label = value; } }
        public Force force { get { return _force; } set { FlagDic["force"] = true; _force = value; } }
        public ItemStyle itemStyle { get { return _itemStyle; } set { FlagDic["itemStyle"] = true; _itemStyle = value; } }
        public LineStyle lineStyle { get { return _lineStyle; } set { FlagDic["lineStyle"] = true; _lineStyle = value; } }
        public string data { get { return _data; } set { FlagDic["data"] = true; _data = value; } }
        public string roam { get { return _roam; } set { FlagDic["roam"] = true; _roam = value; } }
        public string links { get { return _links; } set { FlagDic["links"] = true; _links = value; } }
        public Tooltip tooltip { get { return _tooltip; } set { FlagDic["tooltip"] = true; _tooltip = value; } }


        string _type;
        string _layout;
        string _symbol;
        string _symbolSize;
        string _smooth;
        string _draggable;
        string _categories;
        Label _label;
        Force _force;
        ItemStyle _itemStyle;
        LineStyle _lineStyle;
        string _data;
        string _roam;
        string _links;
        Tooltip _tooltip;
    }
}