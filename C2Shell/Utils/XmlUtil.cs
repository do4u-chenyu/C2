using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace C2Shell.Utils
{
    public class XmlUtil
    {
        public static void UpdateVersion(string path,string newVersion)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(path);
                var rootNode = xDoc.SelectSingleNode("configuration");
                var nodes = rootNode.SelectSingleNode("//add[@key='version']");
                foreach (XmlAttribute node in nodes.Attributes)
                {
                    if (node.Name.Equals("value"))
                    {
                        node.InnerText = newVersion;
                    }
                       
                }
                xDoc.Save(path);
               
            }
            catch
            { }
           
        }
    }
}
