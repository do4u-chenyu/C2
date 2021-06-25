using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace C2.IAOLab.Plugins
{
    public class DLLPlugin : IPlugin
    {
        private Type type;
        private object obj;
        public static readonly IPlugin Empty = new EmptyPlugin(); 

        public DLLPlugin(Type type ,object obj)
        {
            this.type = type;
            this.obj = obj;
        }
        /// <summary>
        /// 异常:
        /// <para>DLLParsingFailureException</para>
        /// </summary>
        public string GetPluginDescription()
        {
            MethodInfo method = type.GetMethod("GetPluginDescription");
            return method.Invoke(obj, null).ToString();
        }
        /// <summary>
        /// 异常:
        /// <para>DLLParsingFailureException</para>
        /// </summary>
        public string GetPluginName()
        {
            MethodInfo method = type.GetMethod("GetPluginName");
            return method.Invoke(obj, null).ToString();
        }
        /// <summary>
        /// 异常:
        /// <para>DLLParsingFailureException</para>
        /// </summary>
        public string GetPluginVersion()
        {
            MethodInfo method = type.GetMethod("GetPluginVersion");
            return method.Invoke(obj, null).ToString();
        }
        /// <summary>
        /// 异常:
        /// <para>DLLParsingFailureException</para>
        /// </summary>
        public Image GetPluginImage()
        {
            MethodInfo method = type.GetMethod("GetPluginImage");
            return method.Invoke(obj, null) as Image;
        }

        public DialogResult ShowFormDialog()
        {
            MethodInfo method = type.GetMethod("ShowFormDialog");
            return (DialogResult)method.Invoke(obj, null);
            
        }

    }
}
