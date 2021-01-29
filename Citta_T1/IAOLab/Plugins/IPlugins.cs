using System;
using System.Windows.Forms;

namespace C2.IAOLab.Plugins
{
    interface IPlugins
    {
        String GetPluginName();
        String GetPluginDescription();
        String GetPluginVersion();
        DialogResult ShowDialog();
    }

    class EmptyPlugin : IPlugins
    {
        public string GetPluginDescription()
        {
            return "EmptyPlugin";
        }

        public string GetPluginName()
        {
            return "EmptyPlugin";
        }

        public string GetPluginVersion()
        {
            return "0.0.0.0";
        }

        public DialogResult ShowDialog()
        {
            return DialogResult.OK;
        }
    }
}
