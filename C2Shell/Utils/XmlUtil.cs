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
            XmlDocument xDoc = new XmlDocument();
            try
            {               
                xDoc.Load(path);
                var rootNode = xDoc.SelectSingleNode("configuration");
                XmlNode node = rootNode.SelectSingleNode("//appSettings//add[@key='version']");
                if (node == null)
                    return;
                foreach (XmlAttribute attribute in node.Attributes)
                {
                    if (attribute.Name.Equals("value"))
                        attribute.InnerText = newVersion;
                }
                xDoc.Save(path);
               

            }
            catch
            { }

           
        }
    }
}
