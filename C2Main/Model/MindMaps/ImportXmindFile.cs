
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
        string Dpan;
        PictureWidget picutreWidget;
        PictureWidget picutreWidget2;
        OpenFileDialog fd;
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
            if (createNewModelForm.CheckNameWord(xmindName))
                return;
            Dpan = "C:\\c2";
            tmpDir = Path.Combine(Dpan, xmindName);
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
            XmlNodeList searchTopic = firstTopic.SelectNodes("ns:children/ns:topics/ns:topic", xmlns);
            Topic xmlRoot = new Topic();
            xmlRoot.Text = firstTitle;


            // 父主题结点没有图片
            if (firstTopic["xhtml:img"] == null)
            {
                CreateC2Exception(xmindName, xmlRoot, searchTopic, xmlns);
                //DeleteDir(tmpDir);
                return;
            }



            //图片挂件
            firstImgeName = firstTopic["xhtml:img"].GetAttribute("xhtml:src").Replace("xap:attachments/", "");
            imgXml = "attachments";
            //if (!File.Exists(imgXml))
                //imgXml = "resources";
            string imageUrl = Path.Combine(imgXml, firstImgeName);
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



                //当前主题只有结点
                if (node["xhtml:img"] == null && node.Attributes["xlink:href"] == null)
                {
                    mindMapRoot.Children.Insert(mindMapRoot.Children.Count, newXmlRoot);
                    RecursionTopic(xmindName, node, newXmlRoot, xmlns);
                    continue;
                }

                //当前主题有【附件】没有【图片】
                if (node["xhtml:img"] == null && node.Attributes["xlink:href"] != null)
                {

                    firstFileName = node.Attributes["xlink:href"].Value.Replace("xap:attachments/", "");
                    string imgXml3 = "attachments";
                    //if (!File.Exists(imgXml3))
                        //imgXml3 = "resources";
                    fileUrl = Path.Combine(imgXml3, firstFileName);
                    string tmpDir2 = Path.Combine(Dpan, xmindName);
                    filePath = Path.Combine(tmpDir2, fileUrl);

                    fd = new OpenFileDialog
                    {
                        //Filter = "文件|*.docx;*.xlsx;*.doc;*.xls;*.pdf;*.txt;*.bcp;*.xmind",
                        Filter = null,
                        Title = Lang._("AddAttachment")
                    };

                    atw = newXmlRoot.FindWidget<AttachmentWidget>();
                    if (atw == null)
                    {
                        newXmlRoot.Widgets.Add(new AttachmentWidget { AttachmentPaths = new List<string> { filePath } });
                    }

                    mindMapRoot.Children.Insert(mindMapRoot.Children.Count, newXmlRoot);
                    RecursionTopic(xmindName, node, newXmlRoot, xmlns);
                    continue;
                }


                //当前主题有【图片】没有【附件】
                if (node["xhtml:img"] != null && node.Attributes["xlink:href"] == null)
                {
                    //图片挂件
                    ImgeName = node["xhtml:img"].GetAttribute("xhtml:src").Replace("xap:attachments/", "");
                    string imgXml4 = "attachments";
                    //string imgXml = File.Exists("attachments") ? "attachments" : "resources";
                    imageUrl = Path.Combine(imgXml4, ImgeName);
                    string tmpDir4 = Path.Combine(Dpan, xmindName);
                    imagePath = Path.Combine(tmpDir4, imageUrl);
                    picutreWidget2 = new PictureWidget();
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
                    continue;
                }


                //图片挂件
                ImgeName = node["xhtml:img"].GetAttribute("xhtml:src").Replace("xap:attachments/", "");
                string imgXml = "attachments";
                //string imgXml = File.Exists("attachments") ? "attachments" : "resources";
                imageUrl = Path.Combine(imgXml, ImgeName);
                string tmpDir = Path.Combine(Dpan, xmindName);
                imagePath = Path.Combine(tmpDir, imageUrl);
                picutreWidget2 = new PictureWidget();
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


                
                //附件挂件
                firstFileName = node.Attributes["xlink:href"].Value.Replace("xap:attachments/", "");
                string imgXml2 = "attachments";
                fileUrl = Path.Combine(imgXml2, firstFileName);
                tmpDir = Path.Combine(Dpan, xmindName);
                filePath = Path.Combine(tmpDir, fileUrl);

                fd = new OpenFileDialog
                {
                    //Filter = "文件|*.docx;*.xlsx;*.doc;*.xls;*.pdf;*.txt;*.bcp;*.xmind",
                    Filter = null,
                    Title = Lang._("AddAttachment")
                };

                atw = newXmlRoot.FindWidget<AttachmentWidget>();
                if (atw == null)
                {
                    newXmlRoot.Widgets.Add(new AttachmentWidget { AttachmentPaths = new List<string> { filePath } });
                }
                


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



                //当前主题只有结点
                if (node["xhtml:img"] == null && node.Attributes["xlink:href"] == null)
                {
                    mindMapRoot.Children.Insert(mindMapRoot.Children.Count, newXmlRoot);
                    RecursionTopic(xmindName, node, newXmlRoot, xmlns);
                    continue;
                }

                //当前主题有【附件】没有【图片】
                if (node["xhtml:img"] == null && node.Attributes["xlink:href"] != null)
                {

                    firstFileName = node.Attributes["xlink:href"].Value.Replace("xap:attachments/", "");
                    //if (!File.Exists(imgXml))
                        //imgXml = "resources";
                    fileUrl = Path.Combine(imgXml, firstFileName);
                    filePath = Path.Combine(tmpDir, fileUrl);
                    fd = new OpenFileDialog
                    {
                        //Filter = "文件|*.docx;*.xlsx;*.doc;*.xls;*.pdf;*.txt;*.bcp;*.xmind",
                        Filter = null,
                        Title = Lang._("AddAttachment")
                    };
                    atw = newXmlRoot.FindWidget<AttachmentWidget>();
                    if (atw == null)
                    {
                        newXmlRoot.Widgets.Add(new AttachmentWidget { AttachmentPaths = new List<string> { filePath } });
                    }

                    mindMapRoot.Children.Insert(mindMapRoot.Children.Count, newXmlRoot);
                    RecursionTopic(xmindName, node, newXmlRoot, xmlns);
                    continue;
                }

                //当前主题有【图片】没有【附件】
                if (node["xhtml:img"] != null && node.Attributes["xlink:href"] == null)
                {

                    ImgeName = node["xhtml:img"].GetAttribute("xhtml:src").Replace("xap:attachments/", "");
                    //if (!File.Exists(imgXml))
                        //imgXml = "resources";
                    imageUrl = Path.Combine(imgXml, ImgeName);
                    tmpDir = Path.Combine(Dpan, xmindName);
                    imagePath = Path.Combine(tmpDir, imageUrl);
                    picutreWidget2 = new PictureWidget();
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
                    continue;
                }


                
                //图片挂件
                ImgeName = node["xhtml:img"].GetAttribute("xhtml:src").Replace("xap:attachments/", "");
                //if (!File.Exists(imgXml))
                    //imgXml = "resources";
                imageUrl = Path.Combine(imgXml, ImgeName);
                tmpDir = Path.Combine(Dpan, xmindName);
                imagePath = Path.Combine(tmpDir, imageUrl);
                picutreWidget2 = new PictureWidget();
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


                //附件挂件 
                firstFileName = node.Attributes["xlink:href"].Value.Replace("xap:attachments/", "");
                //if (!File.Exists(imgXml))
                    //imgXml = "resources";
                fileUrl = Path.Combine(imgXml, firstFileName);
                filePath = Path.Combine(tmpDir, fileUrl);
                fd = new OpenFileDialog
                {
                    //Filter = "文件|*.docx;*.xlsx;*.doc;*.xls;*.pdf;*.txt;*.bcp;*.xmind",
                    Filter = null,
                    Title = Lang._("AddAttachment")
                };
                atw = newXmlRoot.FindWidget<AttachmentWidget>();
                if (atw == null)
                {
                   newXmlRoot.Widgets.Add(new AttachmentWidget { AttachmentPaths = new List<string> { filePath } });
                }
                

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
                    tmpDir = Path.Combine(Dpan, xmindName);
                    filePath = Path.Combine(tmpDir, fileUrl);
                    fd = new OpenFileDialog
                    {
                        //Filter = "文件|*.docx;*.xlsx;*.doc;*.xls;*.pdf;*.txt;*.bcp;*.xmind",
                        Filter = null,
                        Title = Lang._("AddAttachment")
                    };
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
                    tmpDir = Path.Combine(Dpan, xmindName);
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
                tmpDir = Path.Combine(Dpan, xmindName);
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
                fd = new OpenFileDialog
                {
                    //Filter = "文件|*.docx;*.xlsx;*.doc;*.xls;*.pdf;*.txt;*.bcp;*.xmind",
                    Filter = null,
                    Title = Lang._("AddAttachment")
                };
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
