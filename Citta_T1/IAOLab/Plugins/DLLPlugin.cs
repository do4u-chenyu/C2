using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.IAOLab.Plugins
{
    public class DLLPlugin : IPlugin
    {
        private Type type;
        private object obj;
        public static DLLPlugin Empty = new DLLPlugin(); 
        DLLPlugin()
        { }
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
            MethodInfo me = type.GetMethod("ShowFormDialog");
            return (DialogResult)me.Invoke(obj, null);
            
        }

        /// <summary>
        /// 异常:
        /// <para>DLLParsingFailureException</para>
        /// </summary>
        public Form GetForm()
        {
            MethodInfo showDialog = type.GetMethod("GetForm");
            return showDialog.Invoke(obj, null) as Form;
        }

    }
}
