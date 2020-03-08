using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Citta_T1.Controls.Flow;
using Citta_T1.Controls.Move;

namespace Citta_T1.Business
{
    class DocumentSaveLoad
    {
        private string modelPath;
        private string modelFilePath;
        public DocumentSaveLoad(string modelPath, string modelTitle)
        {
            this.modelPath = modelPath;
            this.modelFilePath = modelPath + modelTitle + ".xml";
        }
        public void WriteXml(List<ModelElement> elementList)
        {
            Directory.CreateDirectory(modelPath);
            XmlDocument xDoc = new XmlDocument();
            XmlElement modelDocumentXml = xDoc.CreateElement("ModelDocument");
            xDoc.AppendChild(modelDocumentXml);
            //没有模型元素，只写入根节点
            if (elementList.Count == 0)//----------------------------------------------------------
            {
                xDoc.Save(modelFilePath);
                return;
            }              
            foreach (ModelElement me in elementList)
            {
                XmlElement modelElementXml = xDoc.CreateElement("ModelElement");
                modelDocumentXml.AppendChild(modelElementXml);

                XmlElement nameNode = xDoc.CreateElement("name");
                nameNode.InnerText = me.GetName();
                modelElementXml.AppendChild(nameNode);

               
                XmlElement typeNode = xDoc.CreateElement("type");
                typeNode.InnerText = me.Type.ToString();
                modelElementXml.AppendChild(typeNode);
                
                if (me.Type == ElementType.DataSource || me.Type == ElementType.Operate)//类型判断，如是否为算子类型
                {

                    XmlElement subTypeNode = xDoc.CreateElement("subtype");
                    subTypeNode.InnerText = me.SubType.ToString();
                    modelElementXml.AppendChild(subTypeNode);
                    
                    XmlElement locationNode = xDoc.CreateElement("location");
                    locationNode.InnerText = me.Location.ToString();
                    modelElementXml.AppendChild(locationNode);
                    

                    XmlElement statusNode = xDoc.CreateElement("status");
                    statusNode.InnerText = me.Status.ToString();
                    modelElementXml.AppendChild(statusNode);

                    XmlElement indexLNode = xDoc.CreateElement("index");
                    indexLNode.InnerText = me.GetSizeL;
                    modelElementXml.AppendChild(indexLNode);

                    if (me.Type == ElementType.DataSource)
                    {
                        XmlElement pathNode = xDoc.CreateElement("path");
                        pathNode.InnerText = me.GetPath();
                        modelElementXml.AppendChild(pathNode);            
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
            XmlNode rootNode = xDoc.SelectSingleNode("ModelDocument");
            XmlNode me = rootNode.SelectSingleNode("ModelElement");
            if (me == null)
                return modelElements;//------------------------------------------
            var nodeLists = rootNode.SelectNodes("ModelElement");
            foreach (XmlNode xn in nodeLists)
            {
                string type = xn.SelectSingleNode("type").InnerText;
                String name = xn.SelectSingleNode("name").InnerText;
                if ("Operate".Equals(type))
                {
                    string[] location = xn.SelectSingleNode("location").InnerText.Trim('{', 'X', '=', '}').Split(',');
                    string status = xn.SelectSingleNode("status").InnerText;
                    string subType = xn.SelectSingleNode("subtype").InnerText;
                    Point loc = new Point(Convert.ToInt32(location[0]), Convert.ToInt32(location[1].Trim('Y', '=')));
                    MoveOpControl cotl = new MoveOpControl(0,name, loc);
                    cotl.textBox1.Text = name;
                    cotl.Location = loc;
                    ModelElement mElement = new ModelElement(EType(type), name, cotl, EStatus(status), SEType(subType));
                    modelElements.Add(mElement);

                }
                else if (type == "DataSource")
                {
                    string[] location = xn.SelectSingleNode("location").InnerText.Trim('{', 'X', '=', '}').Split(',');
                    string status = xn.SelectSingleNode("status").InnerText;
                    string subType = xn.SelectSingleNode("subtype").InnerText;
                    string path = xn.SelectSingleNode("path").InnerText;
                    string index =xn.SelectSingleNode("index").InnerText;
                    Point xnlocation = new Point(Convert.ToInt32(location[0]), Convert.ToInt32(location[1].Trim('Y', '=')));
                    MoveOpControl cotl = new MoveDtControl(index,0,name, xnlocation);//暂时定为为moveopctrol
                    //cotl.textBox1.Text = name;//暂时定为为moveopctrol
                    
                    ModelElement mElement = new ModelElement(EType(type), name, cotl, EStatus(status), SEType(subType), path);
                    modelElements.Add(mElement);
                }
                else if (type == "remark")
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
