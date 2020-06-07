using Citta_T1.Business.Option;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;

namespace Citta_T1.Business.Model
{
    class ModelXmlWriter
    {
        private XmlDocument doc;

        public ModelXmlWriter(string nodeName, XmlDocument xmlDoc, XmlElement parent)
        {
            doc = xmlDoc;
            Element = doc.CreateElement(nodeName);
            parent.AppendChild(Element);
        }

        public ModelXmlWriter(string nodeName, XmlDocument xmlDoc)
        {
            doc = xmlDoc;
            Element = doc.CreateElement(nodeName);
            doc.AppendChild(Element);
        }

        public XmlElement Element { get; }

        public ModelXmlWriter Write(string key, string value)
        {
            XmlElement xe = doc.CreateElement(key);
            xe.InnerText = value;
            Element.AppendChild(xe);
            return this;
        }

        public ModelXmlWriter Write(string key, Enum value)
        {
            return Write(key, value.ToString());
        }

        public ModelXmlWriter Write(string key, int value)
        {
            return Write(key, value.ToString());
        }
        public ModelXmlWriter Write(string key, Point value)
        {
            return Write(key, value.ToString());
        }
    }
    class ModelXmlReader
    {
        private XmlNode xn;
        private Dictionary<string, string> dict;
        private static LogUtil log = LogUtil.GetInstance("DocumentSaveLoad");
        public ModelXmlReader(XmlNode xmlNode)
        {
            xn = xmlNode;
            dict = new Dictionary<string, string>();
        }
        public ModelXmlReader Read(string label)
        {
            try
            {
                string value = xn.SelectSingleNode(label).InnerText;
                dict[label] = value;

            }
            catch (Exception e)
            {
                log.Error("读取xml文件出错， error: " + e.Message);
            }
            return this;
        }
        public ModelElement Done()
        {
            return ModelElement.CreateModelElement(dict);
        }

        public ModelRelation RelationDone()
        {
            try
            {
                return new ModelRelation(Convert.ToInt32(dict["start"]),
                                         Convert.ToInt32(dict["end"]),
                                         OpUtil.ToPointFType(dict["startlocation"]),
                                         OpUtil.ToPointFType(dict["endlocation"]),
                                         Convert.ToInt32(dict["endpin"]));
            }
            catch (Exception e)
            {
                log.Info(e.Message);
                return ModelRelation.Empty;
            }

        }
    }

    class DocumentSaveLoad
    {
        private string modelPath;
        private string modelFilePath;
        private ModelDocument modelDocument;
        private float screenFactor;

        private static LogUtil log = LogUtil.GetInstance("DocumentSaveLoad");
        public DocumentSaveLoad(ModelDocument model)
        {
            this.modelPath = model.SavePath;
            this.modelFilePath = Path.Combine(this.modelPath, model.ModelTitle + ".xml");
            this.modelDocument = model;
            this.screenFactor = model.WorldMap.ScreenFactor;
        }
        public void WriteXml()
        {
            Directory.CreateDirectory(modelPath);
            Utils.FileUtil.AddPathPower(modelPath, "FullControl");
            XmlDocument xDoc = new XmlDocument();
            ModelXmlWriter mxw = new ModelXmlWriter("ModelDocument", xDoc);
            // 写入版本号 
            mxw.Write("Version", "V1.0");
            // 写坐标原点
            mxw.Write("MapOrigin", this.modelDocument.WorldMap.MapOrigin);
            // 写算子,数据源，Result
            WriteModelElements(xDoc, mxw.Element, this.modelDocument.ModelElements);
            // 写关系 
            WriteModelRelations(xDoc, mxw.Element, this.modelDocument.ModelRelations);
            // 写备注
            WriteModelRemark(xDoc, mxw.Element, this.modelDocument.RemarkDescription);
            xDoc.Save(modelFilePath);
        }
        private void WriteModelElements(XmlDocument xDoc, XmlElement modelDocumentXml, List<ModelElement> modelElements)
        {
            foreach (ModelElement me in modelElements)
            {
                ModelXmlWriter mexw = new ModelXmlWriter("ModelElement", xDoc, modelDocumentXml);

                mexw.Write("id", me.ID)
                    .Write("type", me.Type)
                    .Write("name", me.Description)
                    .Write("subtype", me.SubType)
                    .Write("location", LocationWithoutScale(me.Location))
                    .Write("status", me.Status);

                if (me.Type == ElementType.Operator)
                {
                    mexw.Write("enableoption", (me.InnerControl as MoveOpControl).EnableOption.ToString());
                    //有配置信息才保存到xml中
                    if ((me.InnerControl as MoveOpControl).Option.OptionDict.Count() > 0)
                        WriteModelOption((me.InnerControl as MoveOpControl).Option, xDoc, mexw.Element);
                    continue;
                }

                mexw.Write("path", me.FullFilePath)
                    .Write("separator", me.Separator)
                    .Write("encoding", me.Encoding);

                if (me.Type == ElementType.DataSource)
                    mexw.Write("extType", me.ExtType);
            }
        }
        #region 配置信息存到xml
        private void WriteModelOption(OperatorOption option, XmlDocument xDoc, XmlElement modelElementXml)
        {
            XmlElement optionNode = xDoc.CreateElement("option");
            modelElementXml.AppendChild(optionNode);
            foreach (KeyValuePair<string, string> kvp in option.OptionDict)
            {
                Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                XmlElement fieldNode = xDoc.CreateElement(kvp.Key);
                fieldNode.InnerText = kvp.Value;
                optionNode.AppendChild(fieldNode);
            }
        }

        #endregion
        private void WriteModelRelations(XmlDocument xDoc, XmlElement modelDocumentXml, List<ModelRelation> modelRelations)
        {

            foreach (ModelRelation mr in modelRelations)
            {
                ModelXmlWriter mexw = new ModelXmlWriter("ModelElement", xDoc, modelDocumentXml);
                mexw.Write("type", mr.Type)
                    .Write("start", mr.StartID)
                    .Write("end", mr.EndID)
                    .Write("startlocation", LocationWithoutScale(mr.StartP))
                    .Write("endlocation", LocationWithoutScale(mr.EndP))
                    .Write("endpin", mr.EndPin);
            }
        }
        private Point LocationWithoutScale(PointF point)
        {
            int x = Convert.ToInt32(point.X / screenFactor);
            int y = Convert.ToInt32(point.Y / screenFactor);
            return new Point(x, y);
        }
        private void WriteModelRemark(XmlDocument xDoc, XmlElement modelDocumentXml, string remarkDescription)
        {

            ModelXmlWriter mexw = new ModelXmlWriter("ModelElement", xDoc, modelDocumentXml);
            mexw.Write("type", "Remark")
                .Write("name", remarkDescription);
        }
        private string GetXmlNodeInnerText(XmlNode node, string nodeName)
        {
            string text = "";
            try
            { text = node.SelectSingleNode(nodeName).InnerText; }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
            return text;
        }
        public void ReadXml()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(modelFilePath);
            XmlNodeList nodeLists;
            try
            {
                XmlNode rootNode = xDoc.SelectSingleNode("ModelDocument");
                this.modelDocument.WorldMap.MapOrigin = OpUtil.ToPointType(GetXmlNodeInnerText(rootNode, "MapOrigin"));
                nodeLists = rootNode.SelectNodes("ModelElement");
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return;
            }

            foreach (XmlNode xn in nodeLists)
            {
                string type = GetXmlNodeInnerText(xn, "type");
                if (type == "Remark")
                    this.modelDocument.RemarkDescription = GetXmlNodeInnerText(xn, "name");
                else if (type == "Relation")
                {
                    ModelXmlReader mxr1 = new ModelXmlReader(xn);
                    mxr1.Read("start")
                        .Read("end")
                        .Read("startlocation")
                        .Read("endlocation")
                        .Read("endpin");
                    if (mxr1.RelationDone() == ModelRelation.Empty)
                        continue;
                    this.modelDocument.AddModelRelation(mxr1.RelationDone());
                }
                else
                {
                    ModelXmlReader mxr0 = new ModelXmlReader(xn);
                    mxr0.Read("name")
                        .Read("status")
                        .Read("subtype")
                        .Read("id")
                        .Read("location")
                        .Read("enableoption")
                        .Read("type")
                        .Read("path")
                        .Read("separator")
                        .Read("encoding");


                    ModelElement element = mxr0.Done();
                    if (element == ModelElement.Empty)
                        continue;
                    this.modelDocument.ModelElements.Add(element);
                    if (GetXmlNodeInnerText(xn, "type") != "Operator")
                        continue;
                    MoveOpControl ctl = element.InnerControl as MoveOpControl;
                    ctl.Option = ReadOption(xn);
                    ctl.FirstDataSourceColumns = ctl.Option.GetOptionSplit("columnname0");
                    ctl.SecondDataSourceColumns = ctl.Option.GetOptionSplit("columnname1");
                }
            }
        }

        private OperatorOption ReadOption(XmlNode xn)
        {
            OperatorOption option = new OperatorOption();
            try
            {
                foreach (XmlNode node in xn.SelectSingleNode("option").ChildNodes)
                    option.SetOption(node.Name, node.InnerText);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                option = new OperatorOption();
            }
            return option;
        }
    }
}
