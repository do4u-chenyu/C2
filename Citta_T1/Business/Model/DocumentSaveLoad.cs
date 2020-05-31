﻿using Citta_T1.Business.Option;
using Citta_T1.Controls.Move.Dt;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Controls.Move.Rs;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;

namespace Citta_T1.Business.Model
{
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
            this.screenFactor = model.WorldMap1.GetWmInfo().ScreenFactor;
        }
        public void WriteXml()
        {
            Directory.CreateDirectory(modelPath);
            Utils.FileUtil.AddPathPower(modelPath, "FullControl");
            XmlDocument xDoc = new XmlDocument();
            XmlElement modelDocumentXml = xDoc.CreateElement("ModelDocument");
            xDoc.AppendChild(modelDocumentXml);
            //写入版本号
            XmlElement versionElement = xDoc.CreateElement("Version");
            versionElement.InnerText = "V1.0";
            modelDocumentXml.AppendChild(versionElement);
            // 写坐标原点
            XmlElement mapOriginNode = xDoc.CreateElement("MapOrigin");
            mapOriginNode.InnerText = this.modelDocument.WorldMap1.GetWmInfo().MapOrigin.ToString();
            modelDocumentXml.AppendChild(mapOriginNode);
            // 写算子,数据源，Result
            List<ModelElement> modelElements = this.modelDocument.ModelElements;
            WriteModelElements(xDoc, modelDocumentXml, modelElements);
            // 写关系
            List<ModelRelation> modelRelations = this.modelDocument.ModelRelations;    
            WriteModelRelations(xDoc, modelDocumentXml, modelRelations);
            // 写备注
            WriteModelRemark(xDoc, modelDocumentXml, this.modelDocument.RemarkDescription);
            xDoc.Save(modelFilePath);
           
        }
        private void WriteModelElements(XmlDocument xDoc, XmlElement modelDocumentXml, List<ModelElement> modelElements)
        {
            foreach (ModelElement me in modelElements)
            {
                XmlElement modelElementXml = xDoc.CreateElement("ModelElement");
                modelDocumentXml.AppendChild(modelElementXml);

                XmlElement typeNode = xDoc.CreateElement("type");
                typeNode.InnerText = me.Type.ToString();
                modelElementXml.AppendChild(typeNode);
            
                XmlElement nameNode = xDoc.CreateElement("name");
                nameNode.InnerText = me.Description;
                modelElementXml.AppendChild(nameNode);

                XmlElement subTypeNode = xDoc.CreateElement("subtype");
                subTypeNode.InnerText = me.SubType.ToString();
                modelElementXml.AppendChild(subTypeNode);

                XmlElement locationNode = xDoc.CreateElement("location");
                int x = Convert.ToInt32(me.Location.X / screenFactor);
                int y = Convert.ToInt32(me.Location.Y / screenFactor);
                locationNode.InnerText = new Point(x,y).ToString();
                modelElementXml.AppendChild(locationNode);

                XmlElement statusNode = xDoc.CreateElement("status");
                statusNode.InnerText = me.Status.ToString();
                modelElementXml.AppendChild(statusNode);

                XmlElement idNode = xDoc.CreateElement("id");
                idNode.InnerText = me.ID.ToString();
                modelElementXml.AppendChild(idNode);

                if (me.Type == ElementType.DataSource)
                {
                    XmlElement pathNode = xDoc.CreateElement("path");
                    pathNode.InnerText = me.FullFilePath;
                    modelElementXml.AppendChild(pathNode);

                    XmlElement sepTypeNode = xDoc.CreateElement("separator"); 
                    sepTypeNode.InnerText = Convert.ToInt32(me.Separator).ToString(); 
                    modelElementXml.AppendChild(sepTypeNode);

                    XmlElement extTypeNode = xDoc.CreateElement("extType");
                    extTypeNode.InnerText = me.ExtType.ToString();
                    modelElementXml.AppendChild(extTypeNode);

                    XmlElement encodingNode = xDoc.CreateElement("encoding");
                    encodingNode.InnerText = me.Encoding.ToString();
                    modelElementXml.AppendChild(encodingNode);
                }
                if (me.Type == ElementType.Operator)
                {
                    
                    XmlElement enableoptionNode = xDoc.CreateElement("enableoption");
                    enableoptionNode.InnerText = (me.InnerControl as MoveOpControl).EnableOption.ToString();
                    modelElementXml.AppendChild(enableoptionNode);
                    //有配置信息才保存到xml中
                    if ((me.InnerControl as MoveOpControl).Option.OptionDict.Count() > 0)
                        WriteModelOption(me.SubType, (me.InnerControl as MoveOpControl).Option, xDoc, modelElementXml);
                }
                if (me.Type == ElementType.Result)
                {
                    XmlElement pathNode = xDoc.CreateElement("path");
                    pathNode.InnerText = me.FullFilePath;
                    modelElementXml.AppendChild(pathNode);

                    XmlElement separatorNode = xDoc.CreateElement("separator");
                    separatorNode.InnerText = Convert.ToInt32(me.Separator).ToString();
                    modelElementXml.AppendChild(separatorNode);

                    XmlElement encodingNode = xDoc.CreateElement("encoding");
                    encodingNode.InnerText = me.Encoding.ToString();
                    modelElementXml.AppendChild(encodingNode);
                }


            }
        }
        #region 配置信息存到xml
        private void WriteModelOption(ElementSubType type,OperatorOption option, XmlDocument xDoc, XmlElement modelElementXml)
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
        private void WriteModelRelations(XmlDocument xDoc, XmlElement modelDocumentXml,List<ModelRelation> modelRelations)
        {
           
            foreach (ModelRelation mr in modelRelations)
            {
                XmlElement modelElementXml = xDoc.CreateElement("ModelElement");
                modelDocumentXml.AppendChild(modelElementXml);

                XmlElement typeNode = xDoc.CreateElement("type");
                typeNode.InnerText = mr.Type.ToString();
                modelElementXml.AppendChild(typeNode);

                XmlElement startControlNode = xDoc.CreateElement("start");
                startControlNode.InnerText = mr.StartID.ToString();
                modelElementXml.AppendChild(startControlNode);

                XmlElement endControlNode = xDoc.CreateElement("end");
                endControlNode.InnerText = mr.EndID.ToString();
                modelElementXml.AppendChild(endControlNode);

                XmlElement startLocationNode = xDoc.CreateElement("startlocation");
                int x1 = Convert.ToInt32(mr.StartP.X / screenFactor);
                int y1 = Convert.ToInt32(mr.StartP.Y / screenFactor);
                startLocationNode.InnerText = new Point(x1, y1).ToString();
                modelElementXml.AppendChild(startLocationNode);

                XmlElement endLocationNode = xDoc.CreateElement("endlocation");
                int x2 = Convert.ToInt32(mr.EndP.X / screenFactor);
                int y2 = Convert.ToInt32(mr.EndP.Y / screenFactor);
                endLocationNode.InnerText = new Point(x2, y2).ToString();
                modelElementXml.AppendChild(endLocationNode);

                XmlElement endPinLabelNode = xDoc.CreateElement("endpin");
                endPinLabelNode.InnerText = mr.EndPin.ToString();
                modelElementXml.AppendChild(endPinLabelNode);
            }
        }
        private void WriteModelRemark(XmlDocument xDoc, XmlElement modelDocumentXml, string remarkDescription)
        {
            XmlElement modelElementXml = xDoc.CreateElement("ModelElement");

            XmlElement typeNode = xDoc.CreateElement("type");
            typeNode.InnerText = "Remark";
            modelElementXml.AppendChild(typeNode);

            XmlElement nameNode = xDoc.CreateElement("name");
            nameNode.InnerText = remarkDescription;
            modelElementXml.AppendChild(nameNode);
            modelDocumentXml.AppendChild(modelElementXml);
        }
        public void ReadXml()
        {
            TextInfo textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(modelFilePath);
            XmlNode rootNode = xDoc.SelectSingleNode("ModelDocument");
            try
            {             
                XmlNode mapOriginNode = rootNode.SelectSingleNode("MapOrigin");
                this.modelDocument.WorldMap1.GetWmInfo().MapOrigin = ToPointType(mapOriginNode.InnerText);
            }
            catch (Exception e) { log.Error(e.Message); }


            var nodeLists = rootNode.SelectNodes("ModelElement");
            foreach (XmlNode xn in nodeLists)
            {
                string type = xn.SelectSingleNode("type").InnerText;
                try
                {
                    if (type == "Operator")
                    {
                                              
                        String name = xn.SelectSingleNode("name").InnerText;
                        string status = xn.SelectSingleNode("status").InnerText;
                        status = textInfo.ToTitleCase(status).ToString();
                        string subType = xn.SelectSingleNode("subtype").InnerText;
                        int id = Convert.ToInt32(xn.SelectSingleNode("id").InnerText);
                        Point location = ToPointType(xn.SelectSingleNode("location").InnerText);
                        bool enableOption = Convert.ToBoolean(xn.SelectSingleNode("enableoption").InnerText);
                        MoveOpControl ctl = new MoveOpControl(0, name, OpUtil.SubTypeName(subType), location)
                        {
                            Type = ElementType.Operator,
                            Status = OpUtil.EStatus(status),
                            ID = id,
                            EnableOption = enableOption
                        };
                        ModelElement operatorElement = ModelElement.CreateModelElement(ctl);
                        this.modelDocument.ModelElements.Add(operatorElement);
                        if (xn.SelectSingleNode("option") != null)
                        {
                            ctl.Option = ReadOption(xn);
                            
                            if(ctl.SubTypeName == "AI实践" && ctl.Option.GetOption("columnname0") != "")
                            {
                                ctl.FirstDataSourceColumns = ctl.Option.GetOption("columnname0").Split('\t').ToList();
                            }
                            else if (ctl.Option.GetOption("columnname") != "")
                                ctl.FirstDataSourceColumns = ctl.Option.GetOption("columnname").Split('\t').ToList();
                            else if(ctl.Option.GetOption("columnname0") != "" && ctl.Option.GetOption("columnname1") != "")
                            {
                                ctl.FirstDataSourceColumns= ctl.Option.GetOption("columnname0").Split('\t').ToList();
                                ctl.SecondDataSourceColumns= ctl.Option.GetOption("columnname1").Split('\t').ToList();
                            }

                        }
                    }
                    else if (type == "DataSource")
                    {
                        String name = xn.SelectSingleNode("name").InnerText;
                        string fullFilePath = xn.SelectSingleNode("path").InnerText;
                        int id = Convert.ToInt32(xn.SelectSingleNode("id").InnerText);
                        Point location = ToPointType(xn.SelectSingleNode("location").InnerText);
                        char separator = ConvertUtil.TryParseAscii(xn.SelectSingleNode("separator").InnerText);
                        OpUtil.Encoding encoding = OpUtil.EncodingEnum(xn.SelectSingleNode("encoding").InnerText);
                        MoveDtControl cotl = new MoveDtControl(fullFilePath, 0, name, location)
                        {
                            Type = ElementType.DataSource,
                            ID = id,
                            Separator = separator,
                            Encoding = encoding
                        };
                        ModelElement dataSourceElement = ModelElement.CreateModelElement(cotl);
                        this.modelDocument.ModelElements.Add(dataSourceElement);
                    }
                    else if (type == "Remark")
                    {
                        String name = xn.SelectSingleNode("name").InnerText;
                        this.modelDocument.RemarkDescription = name;
                    }
                    else if (type == "Result")
                    {
                        String name = xn.SelectSingleNode("name").InnerText;
                        string status = xn.SelectSingleNode("status").InnerText;
                        status = textInfo.ToTitleCase(status).ToString();
                        int id = Convert.ToInt32(xn.SelectSingleNode("id").InnerText);
                        Point location = ToPointType(xn.SelectSingleNode("location").InnerText);
                        string fullFilePath = xn.SelectSingleNode("path").InnerText;
                        char separator = ConvertUtil.TryParseAscii(xn.SelectSingleNode("separator").InnerText);
                        OpUtil.Encoding encoding = xn.SelectSingleNode("encoding") == null ? OpUtil.Encoding.UTF8 : OpUtil.EncodingEnum(xn.SelectSingleNode("encoding").InnerText);

                        MoveRsControl ctl = new MoveRsControl(0, name, location)
                        {
                            Type = ElementType.Result,
                            ID = id,
                            Status = OpUtil.EStatus(status),
                            FullFilePath = fullFilePath,
                            Separator = separator,
                            Encoding = encoding
                        };
                        ModelElement resultElement = ModelElement.CreateModelElement(ctl);
                        this.modelDocument.ModelElements.Add(resultElement);
                    }
                    else if (type == "Relation")
                    {
                        int startID = Convert.ToInt32(xn.SelectSingleNode("start").InnerText);
                        int endID = Convert.ToInt32(xn.SelectSingleNode("end").InnerText);
                        PointF startLocation = ToPointFType(xn.SelectSingleNode("startlocation").InnerText);
                        PointF endLocation = ToPointFType(xn.SelectSingleNode("endlocation").InnerText);
                        int endPin = Convert.ToInt32(xn.SelectSingleNode("endpin").InnerText);
                        ModelRelation mr = new ModelRelation(startID, endID, startLocation, endLocation, endPin);
                        this.modelDocument.AddModelRelation(mr, false);
                    }
                }
                catch(Exception e) 
                { 
                    log.Error("读取xml文件出错， error: " + e.Message); 
                }
               
            }
        }
      
        private OperatorOption ReadOption(XmlNode xn)
        {
            OperatorOption option = new OperatorOption();
                foreach (XmlNode node in xn.SelectSingleNode("option").ChildNodes)
                    option.SetOption(node.Name, node.InnerText);
            return option;
        }


        private PointF ToPointFType(string point)
        {
            PointF location = new PointF();
            try
            {
                string coordinate = Regex.Replace(point, @"[^\d,-]*", "");
                string[] xy = coordinate.Split(',');
                location = new PointF(Convert.ToSingle(xy[0]), Convert.ToSingle(xy[1]));
            }
            catch (Exception e) { log.Error(e.Message); }
            return location;
        }
        private Point ToPointType(string point)
        {
            Point location = new Point();
            try
            {
                string coordinate = Regex.Replace(point, @"[^\d,-]*", "");
                string[] xy = coordinate.Split(',');
                location = new Point(Convert.ToInt32(xy[0]), Convert.ToInt32(xy[1]));
            }
            catch (Exception e) { log.Error(e.Message); }
            return location;
        }
       
    }
}
