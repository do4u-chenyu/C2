using C2Plugins.Plugins;
using System.Windows.Forms;

namespace C2Plugins
{
    public class TestPlugin : IPlugin
    {
        public string GetPluginDescription()
        {
            return "用于插件系统自测的Demo插件";
        }

        public string GetPluginName()
        {
            return "自测插件";
        }
        public string GetPluginVersion()
        {
            return "0.0.1";
        }

        public DialogResult ShowDialog()
        {
            throw new System.NotImplementedException();
        }
    }
}
