using C2.Core;
using C2.Dialogs;
using C2.Forms;
using C2.Model.Widgets;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;


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
            if (createNewModelForm.CheckNameExist(xmindName))
                return;
            tmpXmlFile = "C:\\c2"; //xmind临时存放地址
            tmpDir = Path.Combine(tmpXmlFile, xmindName);
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

            
            // 父主题结点有图片
            if (firstTopic["xhtml:img"] != null)
            {
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
                mindMapRoot.Widgets.Add(picutreWidget);
            }

            RecursionTopic(xmindName, firstTopic, mindMapRoot, xmlns);
            
        }


        public void RecursionTopic(string xmindName, XmlNode node, Topic mindMapRoot, XmlNamespaceManager xmlns)
        {

            XmlNodeList nodeList = node.SelectNodes("ns:children/ns:topics/ns:topic", xmlns);

            foreach (XmlNode nodeSingle in nodeList)
            {
                Topic xmlRoot = new Topic();
                String nodeTitle = nodeSingle["title"].InnerText;
                xmlRoot.Text = nodeTitle;


                //当前主题有【附件】
                if (nodeSingle.Attributes["xlink:href"] != null)
                {

                    firstFileName = nodeSingle.Attributes["xlink:href"].Value.Replace("xap:attachments/", "");
                    fileUrl = Path.Combine(imgXml, firstFileName);
                    tmpDir = Path.Combine(tmpXmlFile, xmindName);
                    filePath = Path.Combine(tmpDir, fileUrl);
                    
                    atw = xmlRoot.FindWidget<AttachmentWidget>();
                    if (atw == null)
                    {
                        xmlRoot.Widgets.Add(new AttachmentWidget { AttachmentPaths = new List<string> { filePath } });
                    }
                }

                //当前主题有【图片】
                if (nodeSingle["xhtml:img"] != null)
                {
                    
                    ImgeName = nodeSingle["xhtml:img"].GetAttribute("xhtml:src").Replace("xap:attachments/", "");
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
                }

   
                mindMapRoot.Children.Insert(mindMapRoot.Children.Count, xmlRoot);
                RecursionTopic(xmindName, nodeSingle, xmlRoot, xmlns);
            }
        }
    }
}
