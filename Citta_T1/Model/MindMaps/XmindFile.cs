using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using C2.Canvas;
using C2.Controls.Paint;
using C2.Core;
using C2.Model;
using C2.Model.Documents;
using C2.Model.Widgets;
using ICSharpCode.SharpZipLib.Zip;

namespace C2.Model.MindMaps
{
    class XmindFile
    {
        public static void SaveFile(IEnumerable<ChartPage> charts, string filename)
        {
            new XmindFileSaver(charts, filename).SaveFile();
        }
        public static void SaveFile(MindMap mindMap, string filename)
        {
            new XmindFileSaver(mindMap, filename).SaveFile();
        }
    }
    class XmindFileSaver
    {
        const int defaultIconSize = 32;
        XmlDocument dom;
        List<Attachment> attachments;
        string filename;
        public XmindFileSaver(IEnumerable<ChartPage> charts, string fn)
        {
            dom = new XmlDocument();
            attachments = new List<Attachment>();
            filename = fn;

            XmlElement root = dom.CreateElement("xmap-content");
            root.SetAttribute("xmlns", "urn:xmind:xmap:xmlns:content:2.0");
            root.SetAttribute("xmlns:fo", "http://www.w3.org/1999/XSL/Format");
            root.SetAttribute("xmlns:svg", "http://www.w3.org/2000/svg");
            root.SetAttribute("xmlns:xhtml", "http://www.w3.org/1999/xhtml");
            root.SetAttribute("xmlns:xlink", "http://www.w3.org/1999/xlink");
            root.SetAttribute("version", "2.0");

            int sheetID = 1;
            foreach (ChartPage chartPage in charts)
            {
                if (chartPage is MindMap)
                    SaveSheet(chartPage as MindMap, root, sheetID);
                sheetID += 1;
            }
            dom.AppendChild(root);
        }
        public XmindFileSaver(MindMap mindMap, string fn)
        {
            dom = new XmlDocument();
            attachments = new List<Attachment>();
            filename = fn;
            //dom.CreateComment("xml version=\"1.0\" encoding=\"UTF - 8\" standalone=\"no\"");
            XmlElement root = dom.CreateElement("xmap-content");
            root.SetAttribute("xmlns", "urn:xmind:xmap:xmlns:content:2.0");
            root.SetAttribute("xmlns:fo", "http://www.w3.org/1999/XSL/Format");
            root.SetAttribute("xmlns:svg", "http://www.w3.org/2000/svg");
            root.SetAttribute("xmlns:xhtml", "http://www.w3.org/1999/xhtml");
            root.SetAttribute("xmlns:xlink", "http://www.w3.org/1999/xlink");
            root.SetAttribute("version", "2.0");

            SaveSheet(mindMap, root, 1);
            dom.AppendChild(root);
        }
        public void SaveFile()
        {
            SaveFileAsZip(dom, attachments, filename);
        }
        private void SaveFileAsZip(XmlDocument dom, List<Attachment> attachments, string filename)
        {
            /*
             * 添加Zip的几种方式
             * 1. 对于String类型的文件，均以UTF-8编码输出，使用`StringDataSource`这个类
             * 2. 对于非String类型的文件，需要以
             */
            using (ZipFile zip = ZipFile.Create(filename))
            {
                zip.BeginUpdate();

                zip.AddDirectory("attachments");
                zip.AddDirectory("META-INF");

                // 添加文件
                StaticDataSource content = new StaticDataSource(dom);
                zip.Add(content, "content.xml");
                // 添加所有附件
                foreach (Attachment attachment in attachments)
                    zip.Add(attachment.staticDataSource, "attachments/" + attachment.filename);
                // META-INF/manifest.xml
                StaticDataSource meta_inf = CreateMetaInf();
                zip.Add(meta_inf, "META-INF/manifest.xml");

                zip.CommitUpdate();
            }
        }
        /// <summary>
        /// 保存文件 META-INF/manifest.xml 不保存图片显示不出来
        /// </summary>
        /// <returns></returns>
        private StaticDataSource CreateMetaInf()
        {
            XmlDocument metaDom = new XmlDocument();
            XmlElement root = metaDom.CreateElement("manifest");
            root.SetAttribute("xmlns", "urn:xmind:xmap:xmlns:manifest:1.0");

            // content.xml
            root.AppendChild(NewFileEntryNode(metaDom, "content.xml", "text/xml"));
            // styles.xml
            //root.AppendChild(NewFullEntryNode("styles.xml", "text/xml"))
            // attachments
            foreach (Attachment attachment in attachments)
                root.AppendChild(
                    NewFileEntryNode(
                        metaDom,
                        "attachments/" + attachment.filename,
                        attachment.filename.EndsWith("png") ? "image / png" : ""
                    )
                );
            // META-INF
            root.AppendChild(NewFileEntryNode(metaDom, "META-INF/", ""));
            root.AppendChild(NewFileEntryNode(metaDom, "META-INF/manifest.xml", "text/xml"));
            // Thumbnails
            // Revisions
            metaDom.AppendChild(root);
            return new StaticDataSource(metaDom);
        }

        private XmlElement NewFileEntryNode(XmlDocument xd, string fullPath, string mediaType)
        {
            XmlElement node = xd.CreateElement("file-entry");
            node.SetAttribute("full-path", fullPath);
            node.SetAttribute("media-type", mediaType);
            return node;
        }

        private void SaveSheet(MindMap mindMap, XmlElement parent, int sheetID)
        {
            // 单sheet / chart
            XmlElement sheet = dom.CreateElement("sheet");
            sheet.SetAttribute("id", sheetID.ToString());

            // topics
            SaveMindMap(sheet, mindMap.Root, mindMap);
            SaveLink(sheet, mindMap.GetLinks(false), mindMap);

            parent.AppendChild(sheet);
        }
        /// <summary>
        /// 递归C2 Chart 形成content.xml 和 attachments
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="topic"></param>
        /// <param name="mindMap"></param>
        private void SaveMindMap(XmlElement parent, Topic topic, MindMap mindMap)
        {
            if (parent == null || topic == null)
                return;
            XmlElement topicNode = dom.CreateElement("topic");
            // Attributes
            topicNode.SetAttribute("id", topic.ID);
            if (topic.Folded)
                topicNode.SetAttribute("branch", "folded");
            // Elements
            // title / text
            XmlElement title = dom.CreateElement("title");
            title.InnerText = topic.Text;
            topicNode.AppendChild(title);
            /* 
             * 图片挂件
             * 优先存C2内部图表，有图表存图表，其他全为附件，没有图表放一张图，其他全为附件
             * 
            */
            if (topic.FindWidgets<PictureWidget>().Length > 0)
            {
                // 至少存在一个图表
                PictureWidget[] widgets = topic.FindWidgets<PictureWidget>();
                int firstInternalPWIndex = FindInternalPicWidgetIndex(widgets);
                for (int i = 0; i < widgets.Length; i++)
                {
                    if (i == firstInternalPWIndex)
                        SavePWAsImg(widgets[i], topicNode);
                    else
                        SavePWAsAttachment(widgets[i], topicNode);
                }
            }
            /*
             * 附件挂件
             */
            if (topic.FindWidgets<AttachmentWidget>().Length > 0)
            {

            }
            /*
             * 图表挂件
             */
            if (topic.FindWidgets<ChartWidget>().Length > 0)
            {

            }
            /*
             * 数据挂件
             */
            if (topic.FindWidgets<DataSourceWidget>().Length > 0)
            {

            }
            /*
             * 
             */
            // notes / remark
            if (String.IsNullOrEmpty(topic.Remark))
            {
                SaveRemark(topicNode, topic.Remark);
            }

            // children
            if (topic.Children.Count > 0)
            {
                if (topicNode.SelectSingleNode("topics") != null)
                {
                    XmlElement tn = (topicNode.SelectSingleNode("topics") as XmlElement);
                    foreach (Topic subTopic in topic.Children)
                        SaveMindMap(tn, subTopic, mindMap);
                }
                else
                {
                    XmlElement tn = dom.CreateElement("topics");
                    XmlElement cn = dom.CreateElement("children");
                    tn.SetAttribute("type", "attached");


                    foreach (Topic subTopic in topic.Children)
                        SaveMindMap(tn, subTopic, mindMap);

                    cn.AppendChild(tn);
                    topicNode.AppendChild(cn);
                }
            }
            parent.AppendChild(topicNode);
        }
        /// <summary>
        /// 将图片保存为Xmind附件，图片无需设置大小
        /// </summary>
        /// <param name="widget"></param>
        /// <param name="topicNode"></param>
        private void SavePWAsAttachment(PictureWidget widget, XmlElement topicNode)
        {
            Attachment attachment = new Attachment(
                attachments.Count.ToString(),
                new StaticDataSource(widget.Data)
                );
            attachments.Add(attachment);
            // Xmind中 附件以节点形式存在
            if (topicNode.SelectSingleNode("topics") != null)
            {
                XmlElement xe = dom.CreateElement("topic");
                xe.SetAttribute("xlink:href", "xap:attachments/" + attachment.filename);
                topicNode.SelectSingleNode("topics").AppendChild(xe);
            }
            else
            {
                XmlElement tn = dom.CreateElement("topics");
                XmlElement cn = dom.CreateElement("children");
                tn.SetAttribute("type", "attached");

                XmlElement xe = dom.CreateElement("topic");
                xe.SetAttribute("id", String.Format("{0}-{1}", topicNode.GetAttribute("id"), attachment.id));
                xe.SetAttribute("xlink:href", "xap:attachments/" + attachment.filename);

                tn.AppendChild(xe);
                cn.AppendChild(tn);
                topicNode.AppendChild(cn);
            }
        }
        /// <summary>
        /// 将C2图标变为Xmind Topic 图片，图片需要设置大小
        /// </summary>
        /// <param name="widget"></param>
        /// <param name="topicNode"></param>
        private void SavePWAsImg(PictureWidget widget, XmlElement topicNode)
        {
            Attachment attachment = new Attachment(
                attachments.Count.ToString(),
                new StaticDataSource(widget.Data)
                );
            attachments.Add(attachment);
            XmlElement xe = topicNode.OwnerDocument.CreateElement("xhtml:img");
            xe.SetAttribute("xhtml:src", "xap:attachments/" + attachment.filename);
            Size imgSize = new Size(defaultIconSize, defaultIconSize);
            xe.SetAttribute("svg:height", imgSize.Height.ToString());
            xe.SetAttribute("svg:width", imgSize.Width.ToString());

            topicNode.AppendChild(xe);
        }

        private void SaveRemark(XmlElement parent, string remark)
        {
            if (parent == null || string.IsNullOrEmpty(remark))
                return;

            XmlElement notes = dom.CreateElement("notes");
            notes.Value = remark;

            parent.AppendChild(notes);
        }
        private void SaveLink(XmlElement parent, Link[] links, MindMap mindMap)
        {
            if (parent == null || links == null)
                return;
            XmlElement node = dom.CreateElement("relationships");
            foreach (Link link in links)
                SaveLink(node, link, mindMap);

            parent.AppendChild(node);
        }
        private void SaveLink(XmlElement parent, Link link, MindMap mindMap)
        {
            if (parent == null || link == null || string.IsNullOrEmpty(link.TargetID))
                return;
            XmlElement node = dom.CreateElement("relationship");
            node.SetAttribute("end1", link.From.ID.ToString());
            node.SetAttribute("end2", link.Target.ID.ToString());
            node.SetAttribute("id", link.ID.ToString());

            parent.AppendChild(node);
        }
        private bool isInternalImage(PictureWidget picWidget)
        {
            return String.IsNullOrEmpty(Path.GetDirectoryName(picWidget.ImageUrl));
        }
        private int FindInternalPicWidgetIndex(PictureWidget[] widgets)
        {
            for (int i = 0; i < widgets.Length; i++)
                if (isInternalImage(widgets[i]))
                    return i;
            return -1;
        }
        class Attachment
        {
            public string id;
            public string filename;
            public IStaticDataSource staticDataSource;
            public Attachment(string id, IStaticDataSource staticDataSource)
            {
                this.id = id;
                this.filename = id + ".png";
                this.staticDataSource = staticDataSource;
            }
            public Attachment(string id, string filename, IStaticDataSource staticDataSource)
            {
                this.id = id;
                this.filename = filename;
                this.staticDataSource = staticDataSource;
            }
        }
        class StaticDataSource : IStaticDataSource
        {
            byte[] bytes;
            private PictureWidget.PictureDesign image;
            private Image data;

            public StaticDataSource(string str, Encoding encoding)
            {
                this.bytes = encoding.GetBytes(str);
            }
            public StaticDataSource(byte[] bytes)
            {
                this.bytes = bytes;
            }
            public StaticDataSource(XmlDocument dom)
            {
                MemoryStream ms = new MemoryStream();
                dom.Save(ms);
                this.bytes = ms.ToArray();
            }
            public StaticDataSource(Image data)
            {
                MemoryStream ms = new MemoryStream();
                data.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                this.bytes = ms.ToArray();
            }

            public Stream GetSource()
            {
                Stream s = new MemoryStream(bytes);
                return s;
            }
        }
    }
}
