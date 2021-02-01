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
        private DLLPlugin()
        { }
        public DLLPlugin(Type type ,object obj)
        {
            this.type = type;
            this.obj = obj;
        }
        //public readonly static DLLPlugin Empty = new DLLPlugin(string.Empty);
        public string GetPluginDescription()
        {
            MethodInfo showDialog = type.GetMethod("ShowFrom");
            return  showDialog.Invoke(obj, null).ToString() ;
        }

        public string GetPluginName()
        {
            throw new NotImplementedException();
        }

        public string GetPluginVersion()
        {
            throw new NotImplementedException();
        }
        public Image GetPluginImage()
        {
            throw new NotImplementedException();
        }

        /*public DialogResult ShowDialog()
        {
            throw new NotImplementedException();
        }*/
        public Form GetForm()
        {
            throw new NotImplementedException();
        }
    }
}
