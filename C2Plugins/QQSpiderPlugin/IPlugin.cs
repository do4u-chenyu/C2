using System;
using System.Drawing;
using System.Windows.Forms;

namespace C2.IAOLab.Plugins
{
    interface IPlugin
    {
        String GetPluginName();
        String GetPluginDescription();
        String GetPluginVersion();
        Image GetPluginImage();
        DialogResult ShowFormDialog();
    }
}
