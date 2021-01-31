using System;
using System.Windows.Forms;

namespace C2Plugins.Plugins
{
    interface IPlugin
    {
        String GetPluginName();
        String GetPluginDescription();
        String GetPluginVersion();
        DialogResult ShowDialog();
    }
}
