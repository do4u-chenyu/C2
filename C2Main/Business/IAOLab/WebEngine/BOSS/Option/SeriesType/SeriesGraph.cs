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
        public string left { get { return _left; } set { FlagDic["left"] = true; _left = value; } }
        public string right { get { return _right; } set { FlagDic["right"] = true; _right = value; } }
        public string top { get { return _top; } set { FlagDic["top"] = true; _top = value; } }
        public string bottom { get { return _bottom; } set { FlagDic["bottom"] = true; _bottom = value; } }

        public bool animation { get { return _animation; } set { FlagDic["animation"] = true; _animation = value; } }
        public int animationThreshold { get { return _animationThreshold; } set { FlagDic["animationThreshold"] = true; _animationThreshold = value; } }
        public int animationDuration { get { return _animationDuration; } set { FlagDic["animationDuration"] = true; _animationDuration = value; } }
        public string animationEasing { get { return _animationEasing; } set { FlagDic["animationEasing"] = true; _animationEasing = value; } }
        public string animationDelay { get { return _animationDelay; } set { FlagDic["animationDelay"] = true; _animationDelay = value; } }
        public int animationDurationUpdate { get { return _animationDurationUpdate; } set { FlagDic["animationDurationUpdate"] = true; _animationDurationUpdate = value; } }
        public string animationEasingUpdate { get { return _animationEasingUpdate; } set { FlagDic["animationEasingUpdate"] = true; _animationEasingUpdate = value; } }
        public string animationDelayUpdate { get { return _animationDelayUpdate; } set { FlagDic["animationDelayUpdate"] = true; _animationDelayUpdate = value; } }



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
        string _left;
        string _right;
        string _bottom;
        string _top;
        bool _animation;
        int _animationThreshold;
        int _animationDuration;
        string _animationEasing;
        string _animationDelay;
        int _animationDurationUpdate;
        string _animationEasingUpdate;
        string _animationDelayUpdate;
    }
}