using C2.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace C2.IAOLab.Plugins
{
    public interface IPlugin
    {
        String GetPluginName();
        String GetPluginDescription();
        String GetPluginVersion();
        Image GetPluginImage(); 
        
        DialogResult ShowFormDialog();
    }

    #region 本地插件
    public class EmptyPlugin : IPlugin
    {
        public string GetPluginDescription()
        {
            return String.Empty;
        }

        public string GetPluginName()
        {
            return String.Empty;
        }

        public string GetPluginVersion()
        {
            return String.Empty;
        }
        public Image GetPluginImage()
        {
            return null;
        }


        public DialogResult ShowFormDialog()
        {
            throw new NotImplementedException();
        }
    }

    class ApkToolPlugin : IPlugin
    {
        public string GetPluginDescription()
        {
            return HelpUtil.ApkToolFormHelpInfo;
        }

        public string GetPluginName()
        {
            return "非法APK分析";
        }

        public string GetPluginVersion()
        {
            return "2.3.0";
        }
        public Image GetPluginImage()
        {
            return null;
        }


        public DialogResult ShowFormDialog()
        {
            throw new NotImplementedException();
        }
    }

    class BankPlugin : IPlugin
    {
        public string GetPluginDescription()
        {
            return HelpUtil.BankToolFormHelpInfo;
        }

        public string GetPluginName()
        {
            return "银行卡信息查询";
        }

        public string GetPluginVersion()
        {
            return "1.0.9";
        }
        public Image GetPluginImage()
        {
            return null;
        }

        public DialogResult ShowFormDialog()
        {
            throw new NotImplementedException();
        }
    }

    class BaseStationPlugin : IPlugin
    {
        public string GetPluginDescription()
        {
            return HelpUtil.BaseStationFormHelpInfo;
        }

        public string GetPluginName()
        {
            return "基站查询";
        }

        public string GetPluginVersion()
        {
            return "2.4.11";
        }
        public Image GetPluginImage()
        {
            return null;
        }


        public DialogResult ShowFormDialog()
        {
            throw new NotImplementedException();
        }
    }

    class GPSTransformPlugin : IPlugin
    {
        public string GetPluginDescription()
        {
            return HelpUtil.GPSTransformFormHelpInfo;
        }

        public string GetPluginName()
        {
            return "经纬度坐标系转换";
        }
        public string GetPluginVersion()
        {
            return "1.1.3";
        }
        public Image GetPluginImage()
        {
            return null;
        }


        public DialogResult ShowFormDialog()
        {
            throw new NotImplementedException();
        }
    }

    class TimeAndIPTransformPlugin : IPlugin
    {
        public string GetPluginDescription()
        {
            return HelpUtil.TimeAndIPTransformFormHelpInfo;
        }

        public string GetPluginName()
        {
            return "时间和IP转换";
        }
        public string GetPluginVersion()
        {
            return "0.0.1";
        }
        public Image GetPluginImage()
        {
            return null;
        }
















        public DialogResult ShowFormDialog()
        {
            throw new NotImplementedException();
        }
    }

    class WifiLocationPlugin : IPlugin
    {
        public string GetPluginDescription()
        {
            return HelpUtil.WifiLocationFormHelpInfo;
        }

        public string GetPluginName()
        {
            return "Wifi查询";
        }

        public string GetPluginVersion()
        {
            return "0.3.2";
        }
        public Image GetPluginImage()
        {
            return null;
        }


        public DialogResult ShowFormDialog()
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}
