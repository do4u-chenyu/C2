namespace C2.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption
{
    public class Encode : BaseEOption
    {
        public override string ToString()
        {
            return Common.GetOptionValue(this, this.FlagDic, this.TypeDic);
        }
        public Encode()
        {
            Common.InitOptionFlag(this, FlagDic, TypeDic);
        }

        public string x { get { return _x; } set { FlagDic["x"] = true; _x = value; } }
        public string y { get { return _y; } set { FlagDic["y"] = true; _y = value; } }
        public string itemName { get { return _itemName; } set { FlagDic["itemName"] = true; _itemName = value; } }
        public string value { get { return _value; } set { FlagDic["value"] = true; _value = value; } }

        string _x;
        string _y;
        string _itemName;
        string _value;
    }
}
