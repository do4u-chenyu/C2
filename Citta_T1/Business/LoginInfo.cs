using Citta_T1.Core;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Citta_T1.Business
{
    class LoginInfo
    {
        private string path;
        private string UserInfoPath;
        private static readonly LogUtil log = LogUtil.GetInstance("LoginInfo");
        public LoginInfo()
        {
            this.path = Global.WorkspaceDirectory;
            this.UserInfoPath = Path.Combine(path, "UserInformation.xml");
        }
        public void CreatNewXml()
        {
            Directory.CreateDirectory(path);
            // 添加权限
            Utils.FileUtil.AddPathPower(path, "FullControl");

            if (!File.Exists(UserInfoPath))
            {
                CreatNewXmlHead();
            }

        }
        private void CreatNewXmlHead()
        {
            XmlDocument xDoc = new XmlDocument();
            XmlElement rootElement = xDoc.CreateElement("login");
            xDoc.AppendChild(rootElement);


            XmlElement versionElement = xDoc.CreateElement("Version");
            versionElement.InnerText = "V1.0";
            rootElement.AppendChild(versionElement);
            xDoc.Save(UserInfoPath);
        }
        public void WriteUserInfo(string userName)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlNode node;
            try
            {              
                xDoc.Load(UserInfoPath);
                node = xDoc.SelectSingleNode("login");
            }
            catch (XmlException e)
            {
                log.Error("LoginInfo Xml文件格式存在问题: " + e.Message);
                // 创建、重写Xml文件
                Directory.CreateDirectory(path);
                Utils.FileUtil.AddPathPower(path, "FullControl");
                CreatNewXmlHead();

                xDoc.Load(UserInfoPath);
                node = xDoc.SelectSingleNode("login");
            }                      
            XmlElement userNode = xDoc.CreateElement("user");
            node.AppendChild(userNode);
            XmlElement nameNode = xDoc.CreateElement("name");
            nameNode.InnerText = userName;
            userNode.AppendChild(nameNode);
            xDoc.Save(UserInfoPath);
        }
        public void WriteLastLogin(string userName)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(UserInfoPath);
            var node = xDoc.SelectSingleNode("login");
            XmlNodeList bodyNode = xDoc.GetElementsByTagName("lastlogin");
            if (bodyNode.Count > 0)
            {
                for (int i = 0; i < bodyNode.Count; i++)
                    node.RemoveChild(bodyNode[i]);
            }
            XmlElement childElement = xDoc.CreateElement("lastlogin");
            node.AppendChild(childElement);
            XmlElement nameNode = xDoc.CreateElement("name");
            nameNode.InnerText = userName;
            childElement.AppendChild(nameNode);
            xDoc.Save(UserInfoPath);
        }
        public List<string> LoadUserInfo(string userType)
        {
            XmlDocument xDoc = new XmlDocument();
            List<string> usersList = new List<string>();
            if (!File.Exists(UserInfoPath))
                return usersList;
            try
            {
                xDoc.Load(UserInfoPath);
                XmlNode node = xDoc.SelectSingleNode("login");
                XmlNodeList nodeLists = node.ChildNodes;
                foreach (XmlNode xn in nodeLists)
                    if (xn.Name == userType && xn.SelectSingleNode("name") != null)
                        usersList.Add(xn.SelectSingleNode("name").InnerText);
                return usersList;
            }
            catch (XmlException e)
            {
                log.Error("LoginInfo Xml文件格式存在问题: " + e.Message);
                return usersList;
            }
           
        }



    }
}
