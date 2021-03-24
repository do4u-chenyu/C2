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
                XmlNode node = xDoc.SelectSingleNode("//configuration//appSettings//add[@key='version']");
                
                node.Attributes["value"].InnerText = newVersion;
                xDoc.Save(path);              
            }
            catch
            { }         
        }


        public static string CurrentVersion(string path)
        { 
            XmlDocument xDoc = new XmlDocument();
            try
            {
                xDoc.Load(path);
                XmlNode node = xDoc.SelectSingleNode("configuration")
                                   .SelectSingleNode("//appSettings//add[@key='version']");

               return node.Attributes["value"].InnerText;
            }
            catch
            { }
            return string.Empty;
        }
    }
}
