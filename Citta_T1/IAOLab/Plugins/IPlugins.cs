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
}
