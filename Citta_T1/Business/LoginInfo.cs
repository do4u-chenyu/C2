using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Citta_T1.Business
{
    class LoginInfo
    {
        private string path;
        private string UserInfoPath;
        public LoginInfo()
        {
           
            this.path = Directory.GetCurrentDirectory().ToString() + "\\cittaModelDocument";
            this.UserInfoPath = path + "\\UserInformation.xml";
        }
        public void CreatNewXml()
        {
            Directory.CreateDirectory(path);
            if (!File.Exists(UserInfoPath))
            {
                FileStream fs = new FileStream(UserInfoPath, FileMode.Create);
                fs.Close();
                XmlDocument xDoc = new XmlDocument();
                XmlElement rootElement = xDoc.CreateElement("login");
                xDoc.AppendChild(rootElement);
                xDoc.Save(UserInfoPath);
            }
           
        }
        public void WriteUserInfo(string userName)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(UserInfoPath);
            var node = xDoc.SelectSingleNode("login");
            XmlElement childElement = xDoc.CreateElement("user");
            node.AppendChild(childElement);
            childElement.InnerText = userName;
            xDoc.Save(UserInfoPath);
        }
        public void WriteLastLogin(string userName)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(UserInfoPath);
            var node = xDoc.SelectSingleNode("login");
            XmlNodeList bodyNode = xDoc.GetElementsByTagName("lastlogin");
            if (bodyNode.Count == 0)
            {
                XmlElement childElement = xDoc.CreateElement("lastlogin");
                node.AppendChild(childElement);
                childElement.InnerText = userName;
            }
            else
                bodyNode[0].InnerText = userName;
            xDoc.Save(UserInfoPath);
        }
        public List<string> LoadUserInfo(string userType)
        {
            XmlDocument xDoc = new XmlDocument();
            List<string> usersList = new List<string>();
            if (!File.Exists(UserInfoPath))
                return usersList;
            xDoc.Load(UserInfoPath);
            XmlNode node = xDoc.SelectSingleNode("login");
            XmlNodeList nodeLists = node.ChildNodes;
            foreach (XmlNode xn in nodeLists)
                if (xn.Name == userType)
                    usersList.Add(xn.InnerText);
            return usersList;
        }
    }
}
