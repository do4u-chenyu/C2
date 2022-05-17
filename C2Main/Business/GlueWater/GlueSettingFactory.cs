using C2.Business.GlueWater.Settings;

namespace C2.Business.GlueWater
{
    public enum GlueType
    {
        Gamble,   // 涉赌
        Gun,      // 涉枪
        Yellow,   // 涉黄
        Webshell, // 盗洞
        BBanshee, // 黑吃黑
        VB,       // 境外
    }
    class GlueSettingFactory
    {
        public static IGlueSetting GetSetting(GlueType type)
        {
            switch (type)
            {
                case GlueType.Gamble:
                    return DbGlueSetting.GetInstance();
                case GlueType.Gun:
                    //new Log.Log().LogManualButton("涉枪专项", "01");
                    return SqGlueSetting.GetInstance();
                case GlueType.Yellow:
                    //new Log.Log().LogManualButton("涉黄专项", "01");
                    return YellowGlueSetting.GetInstance();
                default:
                    return DbGlueSetting.GetInstance();
            }
        }
    }
}
