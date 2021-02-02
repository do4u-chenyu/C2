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
            MethodInfo showDialog = type.GetMethod("GetPluginDescription");
            return showDialog.Invoke(obj, null).ToString();
        }
        /// <summary>
        /// 异常:
        /// <para>DLLParsingFailureException</para>
        /// </summary>
        public string GetPluginName()
        {
            MethodInfo showDialog = type.GetMethod("GetPluginName");
            return showDialog.Invoke(obj, null).ToString();
        }
        /// <summary>
        /// 异常:
        /// <para>DLLParsingFailureException</para>
        /// </summary>
        public string GetPluginVersion()
        {
            MethodInfo showDialog = type.GetMethod("GetPluginVersion");
            return showDialog.Invoke(obj, null).ToString();
        }
        /// <summary>
        /// 异常:
        /// <para>DLLParsingFailureException</para>
        /// </summary>
        public Image GetPluginImage()
        {
            MethodInfo showDialog = type.GetMethod("GetPluginImage");
            return   showDialog.Invoke(obj, null) as Image;
        }

        /*public DialogResult ShowDialog()
        {
            throw new NotImplementedException();
        }*/

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
