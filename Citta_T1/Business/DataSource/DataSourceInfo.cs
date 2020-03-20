using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Citta_T1.Controls.Left;


namespace Citta_T1.Business.DataSource
{
    class DataSourceInfo
    {
        private string userPath;
        private string DataSourcePath;
        public DataSourceInfo(string userName)
        {
           this.userPath = Directory.GetCurrentDirectory().ToString() + "\\cittaModelDocument\\" + userName;
           this.DataSourcePath = this.userPath + "\\DataSourceInformation.xml";
        
         }
       
        public void WriteDataSourceInfo(DataButton db)
        {
            Directory.CreateDirectory(userPath);
            XmlDocument xDoc = new XmlDocument();
            if (!File.Exists(DataSourcePath))
            {              
                XmlElement rootElement = xDoc.CreateElement("DataSourceDocument");
                xDoc.AppendChild(rootElement);
                xDoc.Save(DataSourcePath);
            }
            xDoc.Load(DataSourcePath);
            var node = xDoc.SelectSingleNode("DataSourceDocument");

            XmlElement dataSourceNode = xDoc.CreateElement("DataSource");
            node.AppendChild(dataSourceNode);
            XmlElement nameNode = xDoc.CreateElement("name");
            nameNode.InnerText = "";
            dataSourceNode.AppendChild(nameNode);

            XmlElement codeNode = xDoc.CreateElement("code");//encode
            nameNode.InnerText = "";//该字段不存在
            dataSourceNode.AppendChild(codeNode);

            XmlElement pathNode = xDoc.CreateElement("path");
            nameNode.InnerText = "";
            dataSourceNode.AppendChild(pathNode);

            XmlElement frequencyNode = xDoc.CreateElement("frequency");//count
            nameNode.InnerText = "0";//默认为0
            dataSourceNode.AppendChild(frequencyNode);
            xDoc.Save(DataSourcePath);
        }
        public List<DataButton> LoadDataSourceInfo() 
        {
            XmlDocument xDoc = new XmlDocument();
            List<DataButton> dataSourceList = new List<DataButton>();
            if (!File.Exists(DataSourcePath))
                return dataSourceList;
            xDoc.Load(DataSourcePath);
            XmlNodeList nodeList = xDoc.SelectNodes("DataSource");
            foreach (XmlNode xn in nodeList)
            {
                DataButton dataButton = new DataButton();
                //TODO
                //dataButton的属性设置通过xn得到
                dataSourceList.Add(dataButton);
            }
            return dataSourceList;
        }
    }
}
