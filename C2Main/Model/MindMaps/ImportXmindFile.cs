using C2.Core;
using C2.Forms;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using C2.Model.Widgets;
using System.Windows.Forms;
using C2.Globalization;
using C2.Dialogs;


namespace C2.Model.MindMaps
{
    public class ImportXmindFile
    {
        public PictureWidget.PictureDesign CurrentObject;
        string firstImgeName;
        string firstFileName;
        string imgXml = "attachments";
        string tmpDir;
        string fileUrl;
        string filePath;
        string ImgeName;
        string imageUrl;
        string imagePath;
        string tmpXmlFile;
        PictureWidget picutreWidget;
        AttachmentWidget atw;
        private static ImportXmindFile C2FileInstance;


        public static ImportXmindFile GetInstance()
        {
            if (C2FileInstance == null)
            {
                C2FileInstance = new ImportXmindFile();
            }
            return C2FileInstance;
        }

        public void LoadXml(String path)
        {
            string xmindName = Path.GetFileNameWithoutExtension(path);
            CreateNewModelForm createNewModelForm = new CreateNewModelForm();
            // 防止同名文件出现
            if (createNewModelForm.CheckNameWord(xmindName))
                return;
            tmpXmlFile = "C:\\c2"; //xmind临时存放地址
            tmpDir = Path.Combine(tmpXmlFile, xmindName);
            //string tmpDir = Path.Combine(Global.TempDirectory, xmindName);
            FileUtil.DeleteDirectory(Global.TempDirectory);
            FileUtil.CreateDirectory(tmpDir);
            string zipFilePath = path;
            string errMsg = ZipUtil.UnZipFile(zipFilePath, tmpDir);
            if (!string.IsNullOrEmpty(errMsg))
            {
                HelpUtil.ShowMessageBox(errMsg);
            }
            string pathXml = "content.xml";
            string targetPath = Path.Combine(tmpDir, pathXml);

            XmlDocument doc = new XmlDocument();
            doc.Load(targetPath);
            XmlNamespaceManager xmlns = new XmlNamespaceManager(doc.NameTable);
            xmlns.AddNamespace("ns", "urn:xmind:xmap:xmlns:content:2.0");
            XmlNode sheet = doc.SelectSingleNode("//ns:sheet", xmlns);

            XmlNode firstTopic = sheet.SelectSingleNode("ns:topic", xmlns);
            String firstTitle = firstTopic["title"].InnerText;
            Topic xmlRoot = new Topic();
            xmlRoot.Text = firstTitle;

            
            C2.Model.Documents.Document newDoc = Global.GetMainForm().CreateNewMapForWord(xmindName);
            newDoc.FileName = xmindName;
            DocumentForm form = new DocumentForm(newDoc);
            Global.GetMainForm().ShowFormWord(form);
            MindMap mindMap = form.ActivedChartPage.Chart as MindMap;
            Topic mindMapRoot = mindMap.Root;
            mindMapRoot.Children.Remove(mindMapRoot.GetChildByText("子主题 1"));
            mindMapRoot.Text = xmlRoot.Text;

            
            // 父主题结点没有图片
            if (firstTopic["xhtml:img"] == null)
            {
                RecursionTopic(xmindName, firstTopic, mindMapRoot, xmlns);
                //DeleteDir(tmpDir);
                return;
            }


            //父主题结点有图片
            //图片挂件
            firstImgeName = firstTopic["xhtml:img"].GetAttribute("xhtml:src").Replace("xap:attachments/", "");
            imageUrl = Path.Combine(imgXml, firstImgeName);
            imagePath = Path.Combine(tmpDir, imageUrl);
            PictureWidget picutreWidget = new PictureWidget();
            CurrentObject = new PictureWidget.PictureDesign
            {
                SourceType = PictureSource.File,
                Url = imagePath,
                AddToLibrary = false,
                LimitImageSize = true,
                Name = Path.GetFileNameWithoutExtension(imagePath),
                EmbedIn = false
            };
            picutreWidget.Image = CurrentObject;
            //xmlRoot.Widgets.Add(picutreWidget);
            mindMapRoot.Widgets.Add(picutreWidget);

            RecursionTopic(xmindName, firstTopic, mindMapRoot, xmlns);
            //DeleteDir(tmpDir);  
        }


        public void RecursionTopic(string xmindName, XmlNode node, Topic mindMap_root, XmlNamespaceManager xmlns)
        {

            XmlNodeList nodeList = node.SelectNodes("ns:children/ns:topics/ns:topic", xmlns);

            foreach (XmlNode nodeSingle in nodeList)
            {
                Topic xmlRoot = new Topic();
                String nodeTitle = nodeSingle["title"].InnerText;
                xmlRoot.Text = nodeTitle;


                //当前主题只有结点
                if (nodeSingle["xhtml:img"] == null && nodeSingle.Attributes["xlink:href"] == null)
                {
                    mindMap_root.Children.Insert(mindMap_root.Children.Count, xmlRoot);
                    RecursionTopic(xmindName, nodeSingle, xmlRoot, xmlns);
                    continue;
                }

                //当前主题有【附件】没有【图片】
                if (nodeSingle["xhtml:img"] == null && nodeSingle.Attributes["xlink:href"] != null)
                {

                    firstFileName = nodeSingle.Attributes["xlink:href"].Value.Replace("xap:attachments/", "");
                    //if (!File.Exists(imgXml))
                        //imgXml = "resources";
                    fileUrl = Path.Combine(imgXml, firstFileName);
                    tmpDir = Path.Combine(tmpXmlFile, xmindName);
                    filePath = Path.Combine(tmpDir, fileUrl);
                    
                    atw = xmlRoot.FindWidget<AttachmentWidget>();
                    if (atw == null)
                    {
                        xmlRoot.Widgets.Add(new AttachmentWidget { AttachmentPaths = new List<string> { filePath } });
                    }

                    mindMap_root.Children.Insert(mindMap_root.Children.Count, xmlRoot);
                    RecursionTopic(xmindName, nodeSingle, xmlRoot, xmlns);
                    continue;
                }

                //当前主题有【图片】没有【附件】
                if (nodeSingle["xhtml:img"] != null && nodeSingle.Attributes["xlink:href"] == null)
                {
                    
                    ImgeName = nodeSingle["xhtml:img"].GetAttribute("xhtml:src").Replace("xap:attachments/", "");
                    imgXml = "attachments";
                    //if (!File.Exists(imgXml))
                        //imgXml = "resources";
                    imageUrl = Path.Combine(imgXml, ImgeName);
                    tmpDir = Path.Combine(tmpXmlFile, xmindName);
                    imagePath = Path.Combine(tmpDir, imageUrl);
                    picutreWidget = new PictureWidget();
                    CurrentObject = new PictureWidget.PictureDesign
                    {
                        SourceType = PictureSource.File,
                        Url = imagePath,
                        AddToLibrary = false,
                        LimitImageSize = true,
                        Name = Path.GetFileNameWithoutExtension(imagePath),
                        EmbedIn = false
                    };
                    picutreWidget.Image = CurrentObject;
                    xmlRoot.Widgets.Add(picutreWidget);

                    mindMap_root.Children.Insert(mindMap_root.Children.Count, xmlRoot);
                    RecursionTopic(xmindName, nodeSingle, xmlRoot, xmlns);
                    continue;
                }
                    
                //图片挂件
                ImgeName = nodeSingle["xhtml:img"].GetAttribute("xhtml:src").Replace("xap:attachments/", "");
                imgXml = "attachments";
                //if (!File.Exists(imgXml))
                    //imgXml = "resources";
                imageUrl = Path.Combine(imgXml, ImgeName);
                tmpDir = Path.Combine(tmpXmlFile, xmindName);
                imagePath = Path.Combine(tmpDir, imageUrl);
                picutreWidget = new PictureWidget();
                CurrentObject = new PictureWidget.PictureDesign
                {
                    SourceType = PictureSource.File,
                    Url = imagePath,
                    AddToLibrary = false,
                    LimitImageSize = true,
                    Name = Path.GetFileNameWithoutExtension(imagePath),
                    EmbedIn = false
                };
                picutreWidget.Image = CurrentObject;
                xmlRoot.Widgets.Add(picutreWidget);


                //附件挂件 
                firstFileName = nodeSingle.Attributes["xlink:href"].Value.Replace("xap:attachments/", "");
                //if (!File.Exists(imgXml))
                    //imgXml = "resources";
                fileUrl = Path.Combine(imgXml, firstFileName);
                filePath = Path.Combine(tmpDir, fileUrl);
                
                atw = xmlRoot.FindWidget<AttachmentWidget>();
                if (atw == null)
                {
                    xmlRoot.Widgets.Add(new AttachmentWidget { AttachmentPaths = new List<string> { filePath } });
                }
                

                mindMap_root.Children.Insert(mindMap_root.Children.Count, xmlRoot);
                RecursionTopic(xmindName, nodeSingle, xmlRoot, xmlns);
            }
        }

        private void DeleteDir(string tmpDir)
        {
            FileUtil.DeleteDirectory(tmpDir);
        }
    }
}
