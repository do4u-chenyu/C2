
using C2.Core;
using C2.Forms;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using C2.Model.Widgets;


namespace C2.Model.MindMaps
{
    public class ImportXmindFile
    {
        public PictureWidget.PictureDesign CurrentObject;
        string firstImgeName;
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
            string tmpDir = Path.Combine(Global.TempDirectory, xmindName);
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

            XmlNodeList searchTopic = firstTopic.SelectNodes("ns:children/ns:topics/ns:topic", xmlns);

            Topic xmlRoot = new Topic();
            xmlRoot.Text = firstTitle;

            
            if (firstTopic["xhtml:img"] == null)
            {
                CreateC2Exception(xmindName, xmlRoot, searchTopic, xmlns);
                return;
            }
            
            
 
            firstImgeName = firstTopic["xhtml:img"].GetAttribute("xhtml:src").Replace("xap:attachments/", "");
            string imgXml = "attachments";
            string imageUrl = Path.Combine(imgXml, firstImgeName);
            string imagePath = Path.Combine(tmpDir, imageUrl);
            // 图片挂件           
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
            xmlRoot.Widgets.Add(picutreWidget);

            CreateC2(xmindName, xmlRoot, searchTopic, xmlns, picutreWidget);
            //DeleteDir(tmpDir);  
        }


        
        public void CreateC2Exception(string xmindName, Topic xmlRoot, XmlNodeList SearchTopic, XmlNamespaceManager xmlns)
        {
            C2.Model.Documents.Document doc = Global.GetMainForm().CreateNewMapForWord(xmindName);
            doc.FileName = xmindName;
            DocumentForm form = new DocumentForm(doc);
            Global.GetMainForm().ShowFormWord(form);
            MindMap mindMap = form.ActivedChartPage.Chart as MindMap;
            var mindMapRoot = mindMap.Root;
            mindMapRoot.Children.Remove(mindMapRoot.GetChildByText("子主题 1"));
            mindMapRoot.Text = xmlRoot.Text;

            foreach (XmlNode node in SearchTopic)
            {
                Topic newXmlRoot = new Topic();
                String nodeTitle = node["title"].InnerText;
                newXmlRoot.Text = nodeTitle;

                if (node["xhtml:img"] == null)
                {
                    mindMapRoot.Children.Insert(mindMapRoot.Children.Count, newXmlRoot);
                    RecursionTopic(xmindName, node, newXmlRoot, xmlns);
                    continue;
                }

                String ImgeName = node["xhtml:img"].GetAttribute("xhtml:src").Replace("xap:attachments/", "");


                string imgXml = "attachments";
                string imageUrl = Path.Combine(imgXml, ImgeName);
                string tmpDir = Path.Combine(Global.TempDirectory, xmindName);
                string imagePath = Path.Combine(tmpDir, imageUrl);


                PictureWidget picutreWidget2 = new PictureWidget();
                CurrentObject = new PictureWidget.PictureDesign
                {
                    SourceType = PictureSource.File,
                    Url = imagePath,
                    AddToLibrary = false,
                    LimitImageSize = true,
                    Name = Path.GetFileNameWithoutExtension(imagePath),
                    EmbedIn = false
                };

                picutreWidget2.Image = CurrentObject;


                newXmlRoot.Widgets.Add(picutreWidget2);



                mindMapRoot.Children.Insert(mindMapRoot.Children.Count, newXmlRoot);
                RecursionTopic(xmindName, node, newXmlRoot, xmlns);
            }
        }
        
        

            public void CreateC2(string xmindName, Topic xmlRoot, XmlNodeList SearchTopic, XmlNamespaceManager xmlns, PictureWidget picutreWidget)
        {
            C2.Model.Documents.Document doc = Global.GetMainForm().CreateNewMapForWord(xmindName);
            doc.FileName = xmindName;
            DocumentForm form = new DocumentForm(doc);
            Global.GetMainForm().ShowFormWord(form);
            MindMap mindMap = form.ActivedChartPage.Chart as MindMap;
            var mindMapRoot = mindMap.Root;
            mindMapRoot.Children.Remove(mindMapRoot.GetChildByText("子主题 1"));
            mindMapRoot.Text = xmlRoot.Text;
            mindMapRoot.Widgets.Add(picutreWidget); 


            foreach (XmlNode node in SearchTopic)
            {
                Topic newXmlRoot = new Topic();
                String nodeTitle = node["title"].InnerText;
                newXmlRoot.Text = nodeTitle;

                if (node["xhtml:img"] == null)
                {
                    mindMapRoot.Children.Insert(mindMapRoot.Children.Count, newXmlRoot);
                    RecursionTopic(xmindName, node, newXmlRoot, xmlns);
                    continue;
                }
                   
                String ImgeName = node["xhtml:img"].GetAttribute("xhtml:src").Replace("xap:attachments/", "");
                
                
                string imgXml = "attachments";
                string imageUrl = Path.Combine(imgXml, ImgeName);
                string tmpDir = Path.Combine(Global.TempDirectory, xmindName);
                string imagePath = Path.Combine(tmpDir, imageUrl);


                PictureWidget picutreWidget2 = new PictureWidget();
                CurrentObject = new PictureWidget.PictureDesign
                {
                    SourceType = PictureSource.File,
                    Url = imagePath,
                    AddToLibrary = false,
                    LimitImageSize = true,
                    Name = Path.GetFileNameWithoutExtension(imagePath),
                    EmbedIn = false
                };

                picutreWidget2.Image = CurrentObject;


                newXmlRoot.Widgets.Add(picutreWidget2);
               
                

                mindMapRoot.Children.Insert(mindMapRoot.Children.Count, newXmlRoot);
                RecursionTopic(xmindName, node, newXmlRoot, xmlns);
            }
        }




        public void RecursionTopic(string xmindName, XmlNode node, Topic mindMap_root, XmlNamespaceManager xmlns)
        {
            XmlNodeList nodeList = node.SelectNodes("ns:children/ns:topics/ns:topic", xmlns);
            foreach (XmlNode nodeSingle in nodeList)
            {
                Topic xmlRoot = new Topic();
                String nodeTitle = nodeSingle["title"].InnerText;
                xmlRoot.Text = nodeTitle;

                if (nodeSingle["xhtml:img"] == null)
                {
                    mindMap_root.Children.Insert(mindMap_root.Children.Count, xmlRoot);
                    continue;
                }

                String ImgeName = nodeSingle["xhtml:img"].GetAttribute("xhtml:src").Replace("xap:attachments/", "");


                string imgXml = "attachments";
                string imageUrl = Path.Combine(imgXml, ImgeName);
                string tmpDir = Path.Combine(Global.TempDirectory, xmindName);
                string imagePath = Path.Combine(tmpDir, imageUrl);


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


                xmlRoot.Widgets.Add(picutreWidget);


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
