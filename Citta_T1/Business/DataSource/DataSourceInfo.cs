using C2.Business.Model;
using C2.Controls.Left;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace C2.Business.DataSource
{
    class DataSourceInfo
    {
        private readonly string userPath;
        private readonly string dataSourcePath;
        private readonly static LogUtil log = LogUtil.GetInstance("DataSourceInfo");
        public DataSourceInfo(string userName, string filePath = "DataSourceInformation.xml")
        {
            this.userPath = Path.Combine(Global.WorkspaceDirectory, userName);
            this.dataSourcePath = Path.Combine(this.userPath, filePath);
        }


        public void WriteDataSourceInfo(DataButton db)
        {
            Directory.CreateDirectory(userPath);
            Utils.FileUtil.AddPathPower(userPath, "FullControl");
            XmlDocument xDoc = new XmlDocument();
            if (!File.Exists(dataSourcePath))
                CreatNewXmlHead(xDoc);
            try
            { 
                xDoc.Load(dataSourcePath);
            }
            catch (XmlException e)
            {
                log.Error("LoginInfo Xml文件格式存在问题: " + e.Message);
                CreatNewXmlHead(xDoc);
            }
          
            WriteOneDataSource(db, xDoc);
            xDoc.Save(dataSourcePath);
        }
        private void CreatNewXmlHead(XmlDocument xDoc)
        {
            XmlElement rootElement = xDoc.CreateElement("DataSourceDocument");
            xDoc.AppendChild(rootElement);

            XmlElement versionElement = xDoc.CreateElement("Version");
            versionElement.InnerText = "V1.0";
            rootElement.AppendChild(versionElement);
            xDoc.Save(dataSourcePath);
        }
        public void SaveDataSourceInfo(DataButton[] dbs, string rootName = "DataSourceDocument")
        {
            Directory.CreateDirectory(userPath);
            XmlDocument xDoc = new XmlDocument();
            XmlElement rootElement = xDoc.CreateElement(rootName);
            xDoc.AppendChild(rootElement);

            foreach (DataButton db in dbs)
                WriteOneDataSource(db, xDoc);
            // 保存时覆盖原文件
            xDoc.Save(dataSourcePath);
        }

        private void WriteOneDataSource(DataButton db, XmlDocument xDoc)
        {
            XmlNode node = xDoc.SelectSingleNode("DataSourceDocument");

            ModelXmlWriter mxw = new ModelXmlWriter("DataSource" , node);

           mxw.Write("name", db.DataSourceName)
                .Write("separator", Convert.ToInt32(db.Separator))
                .Write("extType", db.ExtType)
                .Write("encoding", db.Encoding)
                .Write("path", db.FullFilePath)
                .Write("count", "0");//默认为0

        }

        public List<DataButton> LoadDataSourceInfo()
        {
            XmlDocument xDoc = new XmlDocument();
            List<DataButton> dataSourceList = new List<DataButton>();
            if (!File.Exists(dataSourcePath))
                return dataSourceList;
            XmlNodeList nodeList;
            try
            {
                xDoc.Load(dataSourcePath);
                XmlNode rootNode = xDoc.SelectSingleNode("DataSourceDocument");
                nodeList = rootNode.SelectNodes("DataSource");
            }
            catch (XmlException e)
            {
                log.Error("DocumentSaveLoad Xml文件格式存在问题: " + e.Message);
                return dataSourceList; 
            }           
            foreach (XmlNode xn in nodeList)
            {
                try
                {
                    string fullFilePath = xn.SelectSingleNode("path").InnerText;
                    string dataName = xn.SelectSingleNode("name").InnerText;
                    if (string.IsNullOrEmpty(fullFilePath)|| string.IsNullOrEmpty(dataName))
                        continue;
                    char separator = ConvertUtil.TryParseAscii(xn.SelectSingleNode("separator").InnerText);
                    OpUtil.ExtType extType = OpUtil.ExtTypeEnum(xn.SelectSingleNode("extType").InnerText);
                    OpUtil.Encoding encoding = OpUtil.EncodingEnum(xn.SelectSingleNode("encoding").InnerText);
                    DataButton dataButton = new DataButton(fullFilePath, dataName, separator, extType, encoding)
                    {
                        Count = ConvertUtil.TryParseInt(xn.SelectSingleNode("count").InnerText)
                    };
                    dataSourceList.Add(dataButton);
                }
                catch (XmlException e) { log.Error("LoadDataSourceInfo 发生错误，错误 :" + e.Message); }
            }
            return dataSourceList;
        }
    }
}
