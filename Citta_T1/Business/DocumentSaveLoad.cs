using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Citta_T1.Controls;

namespace Citta_T1.Business
{
    class DocumentSaveLoad
    {
        private string modelPath;
        private string modelFilePath;
        private string rootName;
        public DocumentSaveLoad(string userName, string modelTitle, string rootName)
        {
            this.modelPath = Directory.GetCurrentDirectory().ToString() + "\\cittaModelDocument\\" + userName + "\\" + modelTitle + "\\";
            this.modelFilePath = modelPath + modelTitle + ".xml";
            this.rootName = rootName;

        }
        public void CreatNewXml()
        {

            Directory.CreateDirectory(modelPath);
            FileStream fs1 = new FileStream(modelFilePath, FileMode.Create);
            fs1.Close();
            XmlDocument xDoc = new XmlDocument();
            XmlElement rootElement = xDoc.CreateElement(rootName);
            xDoc.AppendChild(rootElement);
            xDoc.Save(modelFilePath);

        }
        public void WriteXml(List<ModelElement> elementList, string subNodeName)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(modelFilePath);
            var node2 = xDoc.SelectSingleNode(rootName);
            foreach (ModelElement mele in elementList)
            {
                XmlElement childElement1 = xDoc.CreateElement(subNodeName);
                node2.AppendChild(childElement1);
                XmlElement nameNode = xDoc.CreateElement("name");
                childElement1.AppendChild(nameNode);
                nameNode.InnerText = mele.GetName();
                XmlElement typeNode = xDoc.CreateElement("type");
                childElement1.AppendChild(typeNode);
                typeNode.InnerText = mele.Type.ToString();
                if (mele.Type == ElementType.DataSource || mele.Type == ElementType.Operate)//类型判断，如是否为算子类型
                {

                    XmlElement subTypeNode = xDoc.CreateElement("subtype");
                    childElement1.AppendChild(subTypeNode);
                    subTypeNode.InnerText = mele.SubType.ToString();
                    XmlElement locationNode = xDoc.CreateElement("location");
                    childElement1.AppendChild(locationNode);
                    locationNode.InnerText = mele.Location.ToString();
                    XmlElement statusNode = xDoc.CreateElement("status");
                    childElement1.AppendChild(statusNode);
                    statusNode.InnerText = mele.Status.ToString();
                    if (mele.Type == ElementType.DataSource)
                    {
                        XmlElement pathNode = xDoc.CreateElement("path");
                        childElement1.AppendChild(pathNode);
                        pathNode.InnerText = mele.GetPath();
                    }
                }
                xDoc.Save(modelFilePath);
            }

        }
        public List<ModelElement> ReadXml()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(modelFilePath);
            List<ModelElement> modelElements = new List<ModelElement>();
            XmlNode node = xDoc.SelectSingleNode(rootName);
            var nodeLists = node.SelectNodes("modelelement");
            foreach (XmlNode xn1 in nodeLists)
            {
                string type = xn1.SelectSingleNode("type").InnerText;
                String name = xn1.SelectSingleNode("name").InnerText;
                if ("Operate".Equals(type))
                {
                    string[] location = xn1.SelectSingleNode("location").InnerText.Trim('{', 'X', '=', '}').Split(',');
                    string status = xn1.SelectSingleNode("status").InnerText;
                    string subType = xn1.SelectSingleNode("subtype").InnerText;
                    MoveOpControl cotl = new MoveOpControl();
                    cotl.textBox1.Text = name;
                    cotl.Location = new Point(Convert.ToInt32(location[0]), Convert.ToInt32(location[1].Trim('Y', '=')));
                    ModelElement mElement = new ModelElement(EType(type), name, cotl, EStatus(status), SEType(subType));
                    modelElements.Add(mElement);

                }
                if (type == "DataSource")
                {
                    string[] location = xn1.SelectSingleNode("location").InnerText.Trim('{', 'X', '=', '}').Split(',');
                    string status = xn1.SelectSingleNode("status").InnerText;
                    string subType = xn1.SelectSingleNode("subtype").InnerText;
                    string path = xn1.SelectSingleNode("path").InnerText;
                    MoveOpControl cotl = new MoveOpControl();//暂时定为为moveopctrol
                    cotl.textBox1.Text = name;//暂时定为为moveopctrol
                    cotl.Location = new Point(Convert.ToInt32(location[0]), Convert.ToInt32(location[1].Trim('Y', '=')));
                    ModelElement mElement = new ModelElement(EType(type), name, cotl, EStatus(status), SEType(subType), path);
                    modelElements.Add(mElement);
                }
                if (type == "remark")
                {
                    RemarkControl cotl = new RemarkControl();
                    ModelElement mElement = new ModelElement(EType(type), name, cotl);
                    modelElements.Add(mElement);
                }



            }
            return modelElements;

        }
        public ElementType EType(string type)
        { return (ElementType)Enum.Parse(typeof(ElementType), type); }
        public ElementSubType SEType(string subType)
        { return (ElementSubType)Enum.Parse(typeof(ElementSubType), subType); }
        public ElementStatus EStatus(string status)
        { return (ElementStatus)Enum.Parse(typeof(ElementStatus), status); }

    }
}
