using System.Xml;

namespace C2Shell.Utils
{
    public class XmlUtil
    {
        private static string versionXPath = "//configuration//appSettings//add[@key='version']";
        public static void UpdateVersion(string path,string newVersion)
        {
            XmlDocument xDoc = new XmlDocument();
            try
            {               
                xDoc.Load(path);
                XmlNode node = xDoc.SelectSingleNode(versionXPath);
                
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
                XmlNode node = xDoc.SelectSingleNode(versionXPath);

               return node.Attributes["value"].InnerText;
            }
            catch
            { }
            return string.Empty;
        }
    }
}
