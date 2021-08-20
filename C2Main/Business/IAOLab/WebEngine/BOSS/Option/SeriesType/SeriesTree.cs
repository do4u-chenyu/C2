using C2.IAOLab.WebEngine.Boss.Option.SeriesType.BarBaseOption;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType.LineBaseOption;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2.IAOLab.WebEngine.Boss.Option.SeriesType
{
    public class SeriesTree : BaseEOption, ISeries
    {
        public override string ToString()
        {
            return Common.GetOptionValue(this, this.FlagDic, this.TypeDic, "{}", "{type:'tree'}");
        }
        public string GetTypeStr()
        {
            return this.ToString();
        }
        public SeriesTree()
        {
            Common.InitOptionFlag(this, FlagDic, TypeDic);
            type = Common.FormatString("tree");
        }
        public string type { get { return _type; } set { FlagDic["type"] = true; _type = value; } }
        public string orient { get { return _orient; } set { FlagDic["orient"] = true; _orient = value; } }
        public string symbol { get { return _symbol; } set { FlagDic["symbol"] = true; _symbol = value; } }
        public string left { get { return _left; } set { FlagDic["left"] = true; _left = value; } }
        public string right { get { return _right; } set { FlagDic["right"] = true; _right = value; } }
        public string top { get { return _top; } set { FlagDic["top"] = true; _top = value; } }
        public string bottom { get { return _bottom; } set { FlagDic["bottom"] = true; _bottom = value; } }
        public string symbolSize { get { return _symbolSize; } set { FlagDic["symbolSize"] = true; _symbolSize = value; } }
        public string edgeShape { get { return _edgeShape; } set { FlagDic["edgeShape"] = true; _edgeShape = value; } }
        public string edgeForkPosition { get { return _edgeForkPosition; } set { FlagDic["edgeForkPosition"] = true; _edgeForkPosition = value; } }
        public string expandAndCollapse { get { return _expandAndCollapse; } set { FlagDic["expandAndCollapse"] = true; _expandAndCollapse = value; } }
        public Label label { get { return _label; } set { FlagDic["label"] = true; _label = value; } }
        public int initialTreeDepth { get { return _initialTreeDepth; } set { FlagDic["initialTreeDepth"] = true; _initialTreeDepth = value; } }
        public ItemStyle itemStyle { get { return _itemStyle; } set { FlagDic["itemStyle"] = true; _itemStyle = value; } }
        public string data { get { return _data; } set { FlagDic["data"] = true; _data = value; } }
        public int animationDuration { get { return _animationDuration; } set { FlagDic["animationDuration"] = true; _animationDuration = value; } }
        public int animationDurationUpdate { get { return _animationDurationUpdate; } set { FlagDic["animationDurationUpdate"] = true; _animationDurationUpdate = value; } }
        public Tooltip tooltip { get { return _tooltip; } set { FlagDic["tooltip"] = true; _tooltip = value; } }


        string _type;
        string _orient;
        string _symbol;
        string _left;
        string _right;
        string _bottom;
        string _top;
        string _symbolSize;
        string _edgeShape;
        string _edgeForkPosition;
        int _initialTreeDepth;
        Label _label;
        ItemStyle _itemStyle;
        string _expandAndCollapse;
        string _data;
        int _animationDuration;
        int _animationDurationUpdate;
        Tooltip _tooltip;
    }
}