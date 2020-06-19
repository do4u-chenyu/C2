using Citta_T1.Controls.Left;
using Citta_T1.Core;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Citta_T1.Business.DataSource
{
    class DataSourceInfo
    {
        private string userPath;
        private string dataSourcePath;
        private static LogUtil log = LogUtil.GetInstance("DataSourceInfo");
        public DataSourceInfo(string userName)
        {
            this.userPath = Path.Combine(Global.WorkspaceDirectory, userName);
            this.dataSourcePath = Path.Combine(this.userPath, "DataSourceInformation.xml");
        }

        public void WriteDataSourceInfo(DataButton db)
        {
            Directory.CreateDirectory(userPath);
            Utils.FileUtil.AddPathPower(userPath, "FullControl");
            XmlDocument xDoc = new XmlDocument();
            if (!File.Exists(dataSourcePath))
            {
                XmlElement rootElement = xDoc.CreateElement("DataSourceDocument");
                xDoc.AppendChild(rootElement);

                XmlElement versionElement = xDoc.CreateElement("Version");
                versionElement.InnerText = "V1.0";
                rootElement.AppendChild(versionElement);
                xDoc.Save(dataSourcePath);
            }
            xDoc.Load(dataSourcePath);
            WriteOneDataSource(db, xDoc);
            xDoc.Save(dataSourcePath);
        }

        public void SaveDataSourceInfo(DataButton[] dbs)
        {
            Directory.CreateDirectory(userPath);
            XmlDocument xDoc = new XmlDocument();
            XmlElement rootElement = xDoc.CreateElement("DataSourceDocument");
            xDoc.AppendChild(rootElement);

            foreach (DataButton db in dbs)
            {
                WriteOneDataSource(db, xDoc);
            }
            // 保存时覆盖原文件
            xDoc.Save(dataSourcePath);
        }

        private void WriteOneDataSource(DataButton db, XmlDocument xDoc)
        {
            XmlNode node = xDoc.SelectSingleNode("DataSourceDocument");
            XmlElement dataSourceNode = xDoc.CreateElement("DataSource");
            node.AppendChild(dataSourceNode);
            XmlElement nameNode = xDoc.CreateElement("name");
            nameNode.InnerText = db.DataSourceName;
            dataSourceNode.AppendChild(nameNode);

            XmlElement sepNode = xDoc.CreateElement("separator");
            sepNode.InnerText = Convert.ToInt32(db.Separator).ToString();
            dataSourceNode.AppendChild(sepNode);

            XmlElement extTypeNode = xDoc.CreateElement("extType");
            extTypeNode.InnerText = db.ExtType.ToString();
            dataSourceNode.AppendChild(extTypeNode);

            XmlElement codeNode = xDoc.CreateElement("encoding");
            codeNode.InnerText = db.Encoding.ToString();
            dataSourceNode.AppendChild(codeNode);

            XmlElement pathNode = xDoc.CreateElement("path");
            pathNode.InnerText = db.FullFilePath;
            dataSourceNode.AppendChild(pathNode);

            XmlElement countNode = xDoc.CreateElement("count");
            countNode.InnerText = "0";//默认为0
            dataSourceNode.AppendChild(countNode);
        }

        public List<DataButton> LoadDataSourceInfo()
        {
            XmlDocument xDoc = new XmlDocument();
            List<DataButton> dataSourceList = new List<DataButton>();
            if (!File.Exists(dataSourcePath))
                return dataSourceList;
            xDoc.Load(dataSourcePath);
            XmlNode rootNode = xDoc.SelectSingleNode("DataSourceDocument");
            XmlNodeList nodeList = rootNode.SelectNodes("DataSource");
            foreach (XmlNode xn in nodeList)
            {
                try
                {
                    string fullFilePath = xn.SelectSingleNode("path").InnerText;
                    string dataName = xn.SelectSingleNode("name").InnerText;
                    char separator = ConvertUtil.TryParseAscii(xn.SelectSingleNode("separator").InnerText);
                    OpUtil.ExtType extType = OpUtil.ExtTypeEnum(xn.SelectSingleNode("extType").InnerText);
                    OpUtil.Encoding encoding = OpUtil.EncodingEnum(xn.SelectSingleNode("encoding").InnerText);
                    DataButton dataButton = new DataButton(fullFilePath, dataName, separator, extType, encoding)
                    {
                        Count = ConvertUtil.TryParseInt(xn.SelectSingleNode("count").InnerText)
                    };
                    dataSourceList.Add(dataButton);
                }
                catch (Exception e) { log.Error("DataSourceInfo :" + e.Message); }
            }
            return dataSourceList;
        }
    }
}
