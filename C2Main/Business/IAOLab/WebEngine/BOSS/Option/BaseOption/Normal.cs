using C2.IAOLab.WebEngine.Boss;
using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WebEngine.Boss.Option.BaseOption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.IAOLab.WebEngine.Boss.Option.BaseOption
{
    public class Normal : BaseEOption
    {

        public override string ToString()
        {
            return Common.GetOptionValue(this, this.FlagDic, this.TypeDic);
        }
        public Normal()
        {
            Common.InitOptionFlag(this, FlagDic, TypeDic);
        }

        public string color { get { return _color; } set { FlagDic["color"] = true; _color = value; } }
        public FontStyle fontStyle { get { return _fontStyle; } set { FlagDic["fontStyle"] = true; _fontStyle = value; } }
        public FontWeight fontWeight { get { return _fontWeight; } set { FlagDic["fontWeight"] = true; _fontWeight = value; } }
        public FontFamily fontFamily { get { return _fontFamily; } set { FlagDic["fontFamily"] = true; _fontFamily = value; } }
        public int fontSize { get { return _fontSize; } set { FlagDic["fontSize"] = true; _fontSize = value; } }


        string _color;
        FontStyle _fontStyle;
        FontWeight _fontWeight;
        FontFamily _fontFamily;
        int _fontSize;

    }
}
