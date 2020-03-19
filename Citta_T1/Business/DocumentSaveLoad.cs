using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private ModelDocument modelDocument;

        public DocumentSaveLoad(ModelDocument model)
        {
            this.modelPath = model.SavePath;
            this.modelFilePath = this.modelPath +  model.ModelTitle + ".xml";
            this.modelDocument = model;
        }
        public void WriteXml()
        {
            Directory.CreateDirectory(modelPath);
            XmlDocument xDoc = new XmlDocument();
            XmlElement modelDocumentXml = xDoc.CreateElement("ModelDocument");
            xDoc.AppendChild(modelDocumentXml);
            List<ModelElement> elementList = this.modelDocument.ModelElements;
            List<ModelRelation> modelRelations = this.modelDocument.ModelRelations;
            foreach (ModelElement me in elementList)
            {
                XmlElement modelElementXml = xDoc.CreateElement("ModelElement");
                modelDocumentXml.AppendChild(modelElementXml);
              
                XmlElement typeNode = xDoc.CreateElement("type");
                typeNode.InnerText = me.Type.ToString();
                modelElementXml.AppendChild(typeNode);

                if (me.Type == ElementType.DataSource || me.Type == ElementType.Operator)//类型判断，如是否为算子类型
                {
                    XmlElement nameNode = xDoc.CreateElement("name");
                    nameNode.InnerText = me.GetDescription();
                    modelElementXml.AppendChild(nameNode);

                    XmlElement subTypeNode = xDoc.CreateElement("subtype");
                    subTypeNode.InnerText = me.SubType.ToString();
                    modelElementXml.AppendChild(subTypeNode);

                    XmlElement locationNode = xDoc.CreateElement("location");
                    locationNode.InnerText = me.Location.ToString();
                    modelElementXml.AppendChild(locationNode);

                    XmlElement statusNode = xDoc.CreateElement("status");
                    statusNode.InnerText = me.Status.ToString();
                    modelElementXml.AppendChild(statusNode);

                    XmlElement identifyingNode = xDoc.CreateElement("identifying");
                    identifyingNode.InnerText = me.ID.ToString();
                    modelElementXml.AppendChild(identifyingNode);

                    if (me.Type == ElementType.DataSource)
                    {
                        XmlElement pathNode = xDoc.CreateElement("path");
                        pathNode.InnerText = me.GetPath();
                        modelElementXml.AppendChild(pathNode);
                    }
                }
                else if (me.Type == ElementType.Remark)
                {
                    XmlElement nameNode = xDoc.CreateElement("name");
                    nameNode.InnerText = me.RemarkName;
                    modelElementXml.AppendChild(nameNode);
                }           
            }
            foreach (ModelRelation mr in modelRelations)
            {
                XmlElement modelElementXml = xDoc.CreateElement("ModelElement");
                modelDocumentXml.AppendChild(modelElementXml);

                XmlElement typeNode = xDoc.CreateElement("type");
                typeNode.InnerText = mr.Type.ToString();
                modelElementXml.AppendChild(typeNode);

                XmlElement startControlNode = xDoc.CreateElement("startcontrol");
                startControlNode.InnerText = mr.StartControl;
                modelElementXml.AppendChild(startControlNode);

                XmlElement endControlNode = xDoc.CreateElement("endcontrol");
                endControlNode.InnerText = mr.EndControl;
                modelElementXml.AppendChild(endControlNode);

                XmlElement startLocationNode = xDoc.CreateElement("startlocation");
                startLocationNode.InnerText = mr.StartLocation;
                modelElementXml.AppendChild(startLocationNode);

                XmlElement endLocationNode = xDoc.CreateElement("endlocation");
                endLocationNode.InnerText = mr.EndLocation;
                modelElementXml.AppendChild(endLocationNode);

                XmlElement endPinLabelNode = xDoc.CreateElement("endpinlabel");
                endPinLabelNode.InnerText = mr.EndPinLabel;
                modelElementXml.AppendChild(endPinLabelNode);
            }
            xDoc.Save(modelFilePath);
        }
       
       
        public void ReadXml()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(modelFilePath);

            XmlNode rootNode = xDoc.SelectSingleNode("ModelDocument");           
            var nodeLists = rootNode.SelectNodes("ModelElement");
            foreach (XmlNode xn in nodeLists)
            {
                string type = xn.SelectSingleNode("type").InnerText;
                if (type == "Operator")
                {
                    String name = xn.SelectSingleNode("name").InnerText;
                    string coordinate = Regex.Replace(xn.SelectSingleNode("location").InnerText, @"[^\d,]*", "");
                    string[] location = coordinate.Split(',');
                    string status = xn.SelectSingleNode("status").InnerText;
                    string subType = xn.SelectSingleNode("subtype").InnerText;
                    int identifying = Convert.ToInt32(xn.SelectSingleNode("identifying").InnerText);
                    Point loc = new Point(Convert.ToInt32(location[0]), Convert.ToInt32(location[1]));
                    MoveOpControl ctl = new MoveOpControl(0, name, loc);
                    ctl.textBox.Text = name;
                    ctl.Location = loc;
                    ModelElement operatorElement = ModelElement.CreateOperatorElement(ctl, name, EStatus(status), SEType(subType), identifying);
                    this.modelDocument.ModelElements.Add(operatorElement);
                }
                else if (type == "DataSource")
                {
                    String name = xn.SelectSingleNode("name").InnerText;
                    string coordinate = Regex.Replace(xn.SelectSingleNode("location").InnerText, @"[^\d,]*", "");
                    string[] location = coordinate.Split(',');
                    string status = xn.SelectSingleNode("status").InnerText;
                    string subType = xn.SelectSingleNode("subtype").InnerText;
                    string bcpPath = xn.SelectSingleNode("path").InnerText;
                    int identifying = Convert.ToInt32(xn.SelectSingleNode("identifying").InnerText);
                    Point xnlocation = new Point(Convert.ToInt32(location[0]), Convert.ToInt32(location[1]));

                    MoveDtControl cotl = new MoveDtControl(bcpPath, 0, name, xnlocation);//暂时定为为moveopctrol
                                                                                         //cotl.textBox1.Text = name;//暂时定为为moveopctrol
                    ModelElement dataSourceElement = ModelElement.CreateDataSourceElement(cotl, name, bcpPath, identifying);
                    this.modelDocument.ModelElements.Add(dataSourceElement);
                }
                else if (type == "Remark")
                {
                    String name = xn.SelectSingleNode("name").InnerText;
                    ModelElement remarkElement = ModelElement.CreateRemarkElement(name);
                    this.modelDocument.ModelElements.Add(remarkElement);
                }
                else if (type == "Relation")
                {
                    string startControl = xn.SelectSingleNode("startcontrol").InnerText;
                    string endControl = xn.SelectSingleNode("endcontrol").InnerText;
                    string startLocation = xn.SelectSingleNode("startlocation").InnerText;
                    string endLocation = xn.SelectSingleNode("endlocation").InnerText;
                    string endPinLabel = xn.SelectSingleNode("endpinlabel").InnerText;
                    ModelRelation modelRelationElement = new ModelRelation(startControl, endControl, startLocation, endLocation, endPinLabel);
                    this.modelDocument.ModelRelations.Add(modelRelationElement);
                }
            }
        }
        public ElementType EType(string type)
        { return (ElementType)Enum.Parse(typeof(ElementType), type); }
        public ElementSubType SEType(string subType)
        { return (ElementSubType)Enum.Parse(typeof(ElementSubType), subType); }
        public ElementStatus EStatus(string status)
        { return (ElementStatus)Enum.Parse(typeof(ElementStatus), status); }

    }
}
