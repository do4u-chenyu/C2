using C2.Globalization;
using C2.Model.MindMaps;


namespace C2.Design
{
    class WaterMarkTypeConverter : BaseTypeConverter
    {
        protected override object ConvertValueToString(object value)
        {
            if (value is WaterMarkType)
            {
                return _ConvertToString((WaterMarkType)value);
            }

            return base.ConvertValueToString(value);
        }

        private object _ConvertToString(WaterMarkType type)
        {
            switch (type)
            {
                case WaterMarkType.Default:
                    return Lang._("Default");
                case WaterMarkType.Flat:
                    return Lang._("Flat");
                case WaterMarkType.Rain:
                    return Lang._("Rain");
                default:
                    return type.ToString();
            }
        }
    }
}
