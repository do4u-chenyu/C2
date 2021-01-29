﻿using C2.Utils;
using System;
using System.Windows.Forms;

namespace C2.IAOLab.Plugins
{
    enum PluginType
    {
        None,
        Fixed,      // 自带捆绑插件
        Pluggable   // 可插拔
    }
    interface IPlugin
    {
        String GetPluginName();
        String GetPluginDescription();
        String GetPluginVersion();
        DialogResult ShowDialog();
        PluginType GetPluginType();
    }

    #region 本地插件
    class EmptyPlugin : IPlugin
    {
        public string GetPluginDescription()
        {
            return "EmptyPlugin";
        }

        public string GetPluginName()
        {
            return "EmptyPlugin";
        }

        public PluginType GetPluginType()
        {
            return PluginType.None;
        }

        public string GetPluginVersion()
        {
            return "0.0.0.1";
        }

        public DialogResult ShowDialog()
        {
            return DialogResult.OK;
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

        public PluginType GetPluginType()
        {
            return PluginType.Fixed;
        }

        public string GetPluginVersion()
        {
            return "0.1";
        }

        public DialogResult ShowDialog()
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

        public PluginType GetPluginType()
        {
            return PluginType.Fixed;
        }

        public string GetPluginVersion()
        {
            return "0.1";
        }

        public DialogResult ShowDialog()
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

        public PluginType GetPluginType()
        {
            return PluginType.Fixed;
        }

        public string GetPluginVersion()
        {
            return "0.1";
        }

        public DialogResult ShowDialog()
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

        public PluginType GetPluginType()
        {
            return PluginType.Fixed;
        }

        public string GetPluginVersion()
        {
            return "0.1";
        }

        public DialogResult ShowDialog()
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

        public PluginType GetPluginType()
        {
            return PluginType.Fixed;
        }

        public string GetPluginVersion()
        {
            return "0.1";
        }

        public DialogResult ShowDialog()
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

        public PluginType GetPluginType()
        {
            return PluginType.Fixed;
        }

        public string GetPluginVersion()
        {
            return "0.1";
        }

        public DialogResult ShowDialog()
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}
