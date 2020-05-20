﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Citta_T1.Core;
using Citta_T1.Utils;

namespace Citta_T1.Business
{
    class LoginInfo
    {
        private string path;
        private string UserInfoPath;
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
                XmlDocument xDoc = new XmlDocument();
                XmlElement rootElement = xDoc.CreateElement("login");
                xDoc.AppendChild(rootElement);
                

                XmlElement versionElement = xDoc.CreateElement("Version");
                versionElement.InnerText = "V1.0";
                rootElement.AppendChild(versionElement);
                xDoc.Save(UserInfoPath);
            }
           
        }
        public void WriteUserInfo(string userName)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(UserInfoPath);
            var node = xDoc.SelectSingleNode("login");
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
            xDoc.Load(UserInfoPath);
            XmlNode node = xDoc.SelectSingleNode("login");
            XmlNodeList nodeLists = node.ChildNodes;
            foreach (XmlNode xn in nodeLists)
                if (xn.Name == userType && xn.SelectSingleNode("name") != null)
                    usersList.Add(xn.SelectSingleNode("name").InnerText);
            return usersList;
        }



    }
}
