using C2.Business.GlueWater.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.GlueWater
{
    class GlueSettingFactory
    {
        public static IGlueSetting GetSetting(string type)
        {
            switch (type)
            {
                case "涉赌专项":
                    return DbGlueSetting.GetInstance();
                case "涉枪专项":
                    return SqGlueSetting.GetInstance();
                default:
                    return DbGlueSetting.GetInstance();
            }
        }
    }
}
