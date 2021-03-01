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

        public int x { get { return _x; } set { FlagDic["x"] = true; _x = value; } }
        public int y { get { return _y; } set { FlagDic["y"] = true; _y = value; } }
        public int itemName { get { return _itemName; } set { FlagDic["itemName"] = true; _itemName = value; } }
        public int value { get { return _value; } set { FlagDic["value"] = true; _value = value; } }

        int _x;
        int _y;
        int _itemName;
        int _value;
    }
}
