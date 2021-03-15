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
                XmlNode node = xDoc.SelectSingleNode("configuration")
                                   .SelectSingleNode("//appSettings//add[@key='version']");
                
                node.Attributes["value"].InnerText = newVersion;
                xDoc.Save(path);              
            }
            catch
            { }

           
        }
    }
}
