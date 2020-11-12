using C2.Business.Option;
using C2.Controls.Move.Op;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml;

namespace C2.Business.Model
{
    class ModelXmlWriter
    {
        private  readonly XmlDocument doc;

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
        public ModelXmlWriter(string nodeName, XmlElement parent)
        {
            doc = parent.OwnerDocument;
            Element = doc.CreateElement(nodeName);
            parent.AppendChild(Element);
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

        public ModelXmlWriter Write(string key, bool value)
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

        public ModelXmlWriter WriteAttribute(string key, string value)
        {
            Element.SetAttribute(key, value);
            return this;
        }
        public ModelXmlWriter WriteAttribute(string key, Enum value)
        {
            return WriteAttribute(key, value.ToString());
        }
        public ModelXmlWriter WriteAttribute(string key, int value)
        {
            return Write(key, value.ToString());
        }
    }
    class ModelXmlReader
    {
        private readonly XmlNode xn;
        private readonly Dictionary<string, string> dict;
        private static readonly LogUtil log = LogUtil.GetInstance("DocumentSaveLoad");
        public ModelXmlReader(XmlNode xmlNode)
        {
            xn = xmlNode;
            dict = new Dictionary<string, string>();
        }
        public ModelXmlReader Read(string label)
        {
            dict[label] = string.Empty;
            try
            {
                XmlNode node = xn.SelectSingleNode(label);
                if (node == null)
                    return this;
                dict[label] = node.InnerText;

            }
            catch (Exception e)
            {
                log.Error("读取xml文件出错这里， error: " + e.Message);
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
                if(String.IsNullOrEmpty(dict["start"])
                    || String.IsNullOrEmpty(dict["end"])
                    || String.IsNullOrEmpty(dict["startlocation"])
                    || String.IsNullOrEmpty(dict["endlocation"])
                    || String.IsNullOrEmpty(dict["endpin"]))
                    return ModelRelation.Empty;
                return new ModelRelation(Convert.ToInt32(dict["start"]),
                                         Convert.ToInt32(dict["end"]),
                                         OpUtil.ToPointFType(dict["startlocation"]),
                                         OpUtil.ToPointFType(dict["endlocation"]),
                                         Convert.ToInt32(dict["endpin"]));
            }
            catch (Exception)
            {
                //log.Info(e.Message);
                return ModelRelation.Empty;
            }

        }
    }

    class DocumentSaveLoad
    {
        private readonly string modelPath;
        private readonly string modelFilePath;
        private readonly ModelDocument modelDocument;
        private readonly float screenFactor;

        private static readonly LogUtil log = LogUtil.GetInstance("DocumentSaveLoad");
        public DocumentSaveLoad(ModelDocument model)
        {
            this.modelPath = model.SavePath;
            this.modelFilePath = Path.Combine(this.modelPath, model.Name + ".xml");
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
                    MoveOpControl moc = me.InnerControl as MoveOpControl;
                    mexw.Write("enableoption", moc.EnableOption);
                    WriteModelOption(moc.Option, xDoc, mexw.Element);
                    continue;
                }

                mexw.Write("path", me.FullFilePath)
                    .Write("separator", me.Separator)
                    .Write("encoding", me.Encoding);

            }
        }
        #region 配置信息存到xml
        public void WriteModelOption(OperatorOption option, XmlDocument xDoc, XmlElement modelElementXml)
        {
            XmlElement optionNode = xDoc.CreateElement("option");
            modelElementXml.AppendChild(optionNode);
            foreach (KeyValuePair<string, string> kvp in option.OptionDict)
            {
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
            string text = String.Empty;
            try
            {
                if (node.SelectSingleNode(nodeName) == null)
                    return text;
                text = node.SelectSingleNode(nodeName).InnerText;
            }
            catch (Exception e)
            {
                log.Error("DocumentSaveLoad 读取InnerText: " + e.Message);
            }
            return text;
        }
        public void ReadXml()
        {
            XmlNodeList nodeLists;
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(modelFilePath);
                XmlNode rootNode = xDoc.SelectSingleNode("ModelDocument");
                this.modelDocument.WorldMap.MapOrigin = OpUtil.ToPointType(GetXmlNodeInnerText(rootNode, "MapOrigin"));
                nodeLists = rootNode.SelectNodes("ModelElement");
                if (rootNode == null || nodeLists == null)
                    return;
            }
            catch (Exception e)
            {
                log.Error("DocumentSaveLoad Xml文件格式存在问题: " + e.Message);
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
                    if (type != "Operator")
                        continue;
                    MoveOpControl ctl = element.InnerControl as MoveOpControl;
                    ReadOption(xn, ctl);
                    ctl.FirstDataSourceColumns = ctl.Option.GetOptionSplit("columnname0");
                    ctl.SecondDataSourceColumns = ctl.Option.GetOptionSplit("columnname1");
                    /*
                     * 外部Xml文件修改等情况，检查并处理异常配置内容
                     */

                    ctl.Option.OptionValidating();     
                }
            }
            //检查是否relation单独存在，两侧算子至少与一个缺失
            CheckXmlRalations();

        }
        private void CheckXmlRalations()
        { //检查是否relation单独存在，两侧算子消失
            List<ModelRelation> relationsCopy = new List<ModelRelation>(this.modelDocument.ModelRelations);
            List<ModelElement> elementsCopy = new List<ModelElement>(this.modelDocument.ModelElements);
            foreach (ModelRelation relation in relationsCopy)
            {
                ModelElement startElement = elementsCopy.Find(me => me.ID == relation.StartID);
                bool hasStartControl = startElement != null;
                ModelElement endElement = elementsCopy.Find(me => me.ID == relation.EndID);
                bool hasEndControl = endElement != null;

                // StartControl存在 EndControl存在
                if (hasStartControl && hasEndControl)
                    continue;
                else if (!hasStartControl && hasEndControl)
                {
                    // StartControl不存在 EndControl存在
                    this.modelDocument.ModelRelations.Remove(relation);
                    if (endElement.Type == ElementType.Result)
                        this.modelDocument.ModelElements.Remove(endElement);
                    else if (endElement.Type == ElementType.Operator)
                    {
                        (endElement.InnerControl as MoveOpControl).EnableOption = false;
                        (endElement.InnerControl as MoveOpControl).Status = ElementStatus.Null;
                    }
                       
                }
                else
                {
                    startElement.Status = ElementStatus.Null;
                    this.modelDocument.ModelRelations.Remove(relation);
                }
                    

            }
        }
        private void ReadOption(XmlNode xn, MoveOpControl opControl)
        {
            try
            {
                XmlNode node = xn.SelectSingleNode("option");
                if (node == null)
                    return;
                foreach (XmlNode child in node.ChildNodes)
                    opControl.Option.SetOption(child.Name, child.InnerText);
            }
            catch (Exception e)
            {
                log.Error("读配置出错 ： " + e.Message);
            }
        }
    }
}
