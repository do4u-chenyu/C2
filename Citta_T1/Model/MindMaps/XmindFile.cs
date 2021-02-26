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
        const string attachmentTag = "[附件]";
        const string internalDataTag = "[本地数据]";
        const string externalDataTag = "[外部数据]";
        const string notSavedextErnalDataTag = "[外部数据(未缓存)]";
        const int defaultIconSize = 32;
        const int defaultTopicStyleIndex = 1;
        XmlDocument contentDom;
        XmlDocument styleDom;
        List<Attachment> attachments;
        string filename;
        XmlElement contentRoot;
        MindMapLayoutType layout;
        /// <summary>
        /// 只支持MindMap
        /// </summary>
        /// <param name="charts"></param>
        /// <param name="fn"></param>
        public XmindFileSaver(IEnumerable<ChartPage> charts, string fn)
        {
            Initialize(fn);

            int sheetID = 1;
            foreach (ChartPage chartPage in charts)
            {
                if (chartPage is MindMap)
                {
                    SaveSheet(chartPage as MindMap, contentRoot, sheetID);
                    sheetID += 1;
                }
                
            }
            contentDom.AppendChild(contentRoot);
        }
        public XmindFileSaver(MindMap mindMap, string fn)
        {
            Initialize(fn);

            SaveSheet(mindMap, contentRoot, 1);
            contentDom.AppendChild(contentRoot);
        }
        private void Initialize(string fn)
        {
            contentDom = new XmlDocument();
            styleDom = CreateStyleDOM();
            attachments = new List<Attachment>();
            filename = fn;

            contentRoot = CreateRootFromCTDOM(contentDom);
        }
        public void SaveFile()
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
                zip.Add(new StaticDataSource(contentDom), "content.xml");
                zip.Add(new StaticDataSource(styleDom), "styles.xml");
                // 添加所有附件
                foreach (Attachment attachment in attachments)
                    zip.Add(attachment.staticDataSource, "attachments/" + attachment.filename);
                // META-INF/manifest.xml
                zip.Add(CreateMetaInf(), "META-INF/manifest.xml");

                zip.CommitUpdate();
            }
        }
        /// <summary>
        /// 存一份默认的Style，Topic可选风格只有一种，目的只是让所有的Topic看起来一样
        /// </summary>
        /// <returns></returns>
        private XmlDocument CreateStyleDOM()
        {
            XmlDocument dom = new XmlDocument();
            dom.LoadXml(Properties.Resources.styles);
            return dom;
        }
        private XmlElement CreateRootFromCTDOM(XmlDocument dom)
        {
            XmlElement root = dom.CreateElement("xmap-content");
            root.SetAttribute("xmlns", "urn:xmind:xmap:xmlns:content:2.0");
            root.SetAttribute("xmlns:fo", "http://www.w3.org/1999/XSL/Format");
            root.SetAttribute("xmlns:svg", "http://www.w3.org/2000/svg");
            root.SetAttribute("xmlns:xhtml", "http://www.w3.org/1999/xhtml");
            root.SetAttribute("xmlns:xlink", "http://www.w3.org/1999/xlink");
            root.SetAttribute("version", "2.0");
            return root;
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
            root.AppendChild(CreateFileEntryNode(metaDom, "content.xml", "text/xml"));
            // styles.xml
            //root.AppendChild(NewFullEntryNode("styles.xml", "text/xml"))
            // attachments
            foreach (Attachment attachment in attachments)
                root.AppendChild(
                    CreateFileEntryNode(
                        metaDom,
                        "attachments/" + attachment.filename,
                        attachment.filename.EndsWith("png") ? "image / png" : ""
                    )
                );
            // META-INF
            root.AppendChild(CreateFileEntryNode(metaDom, "META-INF/", ""));
            root.AppendChild(CreateFileEntryNode(metaDom, "META-INF/manifest.xml", "text/xml"));
            // Thumbnails
            // Revisions

            metaDom.AppendChild(root);
            return new StaticDataSource(metaDom);
        }

        private void SaveSheet(MindMap mindMap, XmlElement parent, int sheetID)
        {
            // 单sheet / chart
            XmlElement sheet = contentDom.CreateElement("sheet");
            this.layout = mindMap.LayoutType;
            sheet.SetAttribute("id", sheetID.ToString());

            // topics
            SaveMindMap(sheet, mindMap.Root);
            SaveLink(sheet, mindMap.GetLinks(false), mindMap);

            XmlElement title = contentDom.CreateElement("title");
            title.InnerText = mindMap.Name;
            sheet.AppendChild(title);

            parent.AppendChild(sheet);
        }
        /// <summary>
        /// 递归C2 Chart 形成content.xml 和 attachments
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="topic"></param>
        private void SaveMindMap(XmlElement parent, Topic topic, bool isRoot = true)
        {
            if (parent == null || topic == null)
                return;
            XmlElement topicNode = contentDom.CreateElement("topic");
            // Attributes
            topicNode.SetAttribute("id", topic.ID);
            topicNode.SetAttribute("style-id", defaultTopicStyleIndex.ToString());
            if (topic.Folded)
                topicNode.SetAttribute("branch", "folded");
            if (isRoot)
            {
                topicNode.SetAttribute("structure-class", GetXmindLayout(this.layout));
                isRoot = false;
            }
            // Elements
            // title / text
            XmlElement title = contentDom.CreateElement("title");
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
                int firstInternalPWIndex = FindInternalPicWidgetIndex(widgets); // 找不到返回-1
                for (int i = 0; i < widgets.Length; i++)
                {
                    if (i == firstInternalPWIndex)
                        SavePWAsImg(topicNode, widgets[i]);
                    else
                        SavePwAsAttachment(topicNode, widgets[i]);
                }
            }
            /*
             * 附件挂件
             */
            if (topic.FindWidgets<AttachmentWidget>().Length > 0)
            {
                AttachmentWidget[] widgets = topic.FindWidgets<AttachmentWidget>();
                for (int i = 0; i < widgets.Length; i++)
                    SaveAwAsAttachment(widgets[i], topicNode);
            }
            /*
             * 图表挂件，目前没有持久化，不保存了
             */
            if (topic.FindWidgets<ChartWidget>().Length > 0)
            {

            }
            /*
             * 数据挂件
             */
            if (topic.FindWidgets<DataSourceWidget>().Length > 0)
            {
                // 本地数据和外部数据
                DataSourceWidget[] widgets = topic.FindWidgets<DataSourceWidget>();
                for (int i = 0; i < widgets.Length; i++)
                    SaveDataSource(widgets[i], topicNode);
            }
            /*
             * 
             */
            // notes / remark
            if (topic.FindWidgets<NoteWidget>().Length > 0)
            {
                NoteWidget[] widgets = topic.FindWidgets<NoteWidget>();
                for (int i = 0; i < widgets.Length; i++)
                    SaveRemark(topicNode, widgets[i].Remark);
            }

            // children
            if (topic.Children.Count > 0)
            {
                if (topicNode.SelectSingleNode("children/topics") != null)
                {
                    XmlElement tsn = (topicNode.SelectSingleNode("children/topics") as XmlElement);
                    foreach (Topic subTopic in topic.Children)
                        SaveMindMap(tsn, subTopic, isRoot);
                }
                else
                {
                    XmlElement tsn = contentDom.CreateElement("topics");
                    XmlElement cn = contentDom.CreateElement("children");
                    tsn.SetAttribute("type", "attached");

                    foreach (Topic subTopic in topic.Children)
                        SaveMindMap(tsn, subTopic, isRoot);

                    cn.AppendChild(tsn);
                    topicNode.AppendChild(cn);
                }
            }
            parent.AppendChild(topicNode);
        }

        private string GetXmindLayout(MindMapLayoutType mmlt)
        {
            switch (mmlt)
            {
                case MindMapLayoutType.MindMap:
                    return "org.xmind.ui.map.unbalanced";
                case MindMapLayoutType.OrganizationDown:
                    return "org.xmind.ui.org-chart.down";
                case MindMapLayoutType.OrganizationUp:
                    return "org.xmind.ui.org-chart.up";
                case MindMapLayoutType.TreeLeft:
                    return "org.xmind.ui.tree.left";
                case MindMapLayoutType.TreeRight:
                    return "org.xmind.ui.tree.right";
                case MindMapLayoutType.LogicLeft:
                    return "org.xmind.ui.logic.left";
                case MindMapLayoutType.LogicRight:
                    return "org.xmind.ui.logic.right";
                default:
                    return "org.xmind.ui.map.unbalanced";
            }
        }
        private string GetXmindAlignment(WidgetAlignment alignment)
        {
            switch (alignment)
            {
                case WidgetAlignment.Left:
                    return "left";
                case WidgetAlignment.Right:
                    return "right";
                case WidgetAlignment.Top:
                    return "top";
                case WidgetAlignment.Bottom:
                    return "bottom";
                default:
                    return "left";
            }
        }
        /// <summary>
        /// 将图片保存为Xmind附件，图片无需设置大小
        /// </summary>
        /// <param name="widget"></param>
        /// <param name="parent"></param>
        private void SavePwAsAttachment(XmlElement parent, PictureWidget widget)
        {
            Attachment attachment = new Attachment(
                attachments.Count.ToString(),
                attachmentTag + attachments.Count.ToString() + ".png",
                attachments.Count.ToString() + ".png",
                new StaticDataSource(widget.Data)
                );

            attachments.Add(attachment);
            SaveAttachment(attachment, parent);
        }
        private void SaveAwAsAttachment(AttachmentWidget attachmentWidget, XmlElement topicNode)
        {
            foreach(string filePath in attachmentWidget.AttachmentPaths)
            {
                Attachment attachment = new Attachment(
                                    attachments.Count.ToString(),
                                    attachmentTag + attachments.Count.ToString(),
                                    Path.GetFileName(filePath),
                                    new StaticDataSource(filePath)
                    );
                attachments.Add(attachment);
                SaveAttachment(attachment, topicNode);
            }
        }
        private void SaveDataSource(DataSourceWidget dataSourceWidget, XmlElement topicNode)
        {
            foreach (DataItem dataItem in dataSourceWidget.DataItems)
            {
                if (dataItem.IsDatabase() && !String.IsNullOrEmpty(dataItem.FilePath))
                {
                    SaveLabel(topicNode, notSavedextErnalDataTag + dataItem.DataType.ToString() + "-" + dataItem.FileName);
                    return;
                }

                Attachment attachment;
                if (dataItem.IsDatabase())
                {
                    attachment = new Attachment(
                                        attachments.Count.ToString(),
                                        externalDataTag + dataItem.FileName,
                                        dataItem.FileName,
                                        new StaticDataSource(dataItem.FilePath, Encoding.UTF8)
                                    );
                }
                else
                {
                    attachment = new Attachment(
                                        attachments.Count.ToString(),
                                        internalDataTag + dataItem.FileName,
                                        dataItem.FileName,
                                        new StaticDataSource(dataItem.FilePath)
                                );
                }
                if (attachment != null)
                {
                    attachments.Add(attachment);
                    SaveAttachment(attachment, topicNode);
                }
            }
        }

        private void SaveAttachment(Attachment attachment, XmlElement topicNode)
        {
            // Xmind中 附件以节点形式存在
            if (topicNode.SelectSingleNode("children/topics") != null)
            {
                XmlElement topic = CreateAtcmtTopicNode(String.Format("{0}-{1}", topicNode.GetAttribute("id"), attachment.id), attachment);

                topicNode.SelectSingleNode("children/topics").AppendChild(topic);
            }
            else
            {
                XmlElement cn = contentDom.CreateElement("children");
                XmlElement tn = contentDom.CreateElement("topics");
                tn.SetAttribute("type", "attached");

                XmlElement topic = CreateAtcmtTopicNode(String.Format("{0}-{1}", topicNode.GetAttribute("id"), attachment.id), attachment);

                tn.AppendChild(topic);
                cn.AppendChild(tn);
                topicNode.AppendChild(cn);
            }
        }
        private XmlElement CreateFileEntryNode(XmlDocument xd, string fullPath, string mediaType)
        {
            XmlElement node = xd.CreateElement("file-entry");
            node.SetAttribute("full-path", fullPath);
            node.SetAttribute("media-type", mediaType);
            return node;
        }
        private XmlElement CreateAtcmtTopicNode(string topicID, Attachment attachment)
        {
            return CreateTopicNode(topicID, attachment.name, attachment.filename);
        }

        private XmlElement CreateTopicNode(string nodeID, string titleText, string attachmentName="")
        {
            XmlElement topic = contentDom.CreateElement("topic");
            XmlElement title = contentDom.CreateElement("title");
            title.InnerText = titleText;
            topic.SetAttribute("id", nodeID);
            if (!String.IsNullOrEmpty(attachmentName)) 
                topic.SetAttribute("xlink:href", "xap:attachments/" + attachmentName);
            topic.AppendChild(title);
            return topic;
        }
        private XmlElement CreatePlainNode(string text)
        {
            XmlElement plain = contentDom.CreateElement("plain");
            plain.InnerText = text;
            return plain;
        }        
        private XmlElement CreateTitleNode(string text)
        {
            XmlElement title = contentDom.CreateElement("title");
            title.InnerText = text;
            return title;
        }
        private XmlElement CreateLablNode(string text)
        {
            XmlElement label = contentDom.CreateElement("label");
            label.InnerText = text;
            return label;
        }
        private XmlElement CreatControlPoint(int x, int y, int index)
        {
            XmlElement cp = contentDom.CreateElement("control-point");
            XmlElement pst = contentDom.CreateElement("position");

            cp.SetAttribute("index", index.ToString());
            pst.SetAttribute("svg:x", x.ToString());
            pst.SetAttribute("svg:y", y.ToString());

            cp.AppendChild(pst);
            return cp;
        }
        private void SaveLabel(XmlElement parent, string text)
        {
            if (parent == null || string.IsNullOrEmpty(text))
                return;

            if (parent.SelectSingleNode("labels") != null)
            {
                XmlElement label = CreateLablNode(text);
                parent.SelectSingleNode("labels").AppendChild(label);
            }
            else
            {
                XmlElement labels = contentDom.CreateElement("labels");

                XmlElement label = CreateLablNode(text);
                labels.AppendChild(label);

                parent.AppendChild(labels);
            }
        }
        /// <summary>
        /// 将C2图标变为Xmind Topic 图片，图片需要设置大小和布局
        /// </summary>
        /// <param name="widget"></param>
        /// <param name="parent"></param>
        private void SavePWAsImg(XmlElement parent, PictureWidget widget)
        {
            if (parent == null || widget.Data == null)
                return;

            Attachment attachment = new Attachment(
                attachments.Count.ToString(),
                attachmentTag + attachments.Count.ToString(),
                attachments.Count.ToString() + ".png",
                new StaticDataSource(widget.Data)
                );
            attachments.Add(attachment);
            XmlElement xe = parent.OwnerDocument.CreateElement("xhtml:img");
            xe.SetAttribute("xhtml:src", "xap:attachments/" + attachment.filename);
            xe.SetAttribute("svg:height", defaultIconSize.ToString());
            xe.SetAttribute("svg:width", defaultIconSize.ToString());
            xe.SetAttribute("align", GetXmindAlignment(widget.Alignment));

            parent.AppendChild(xe);
        }

        private void SaveRemark(XmlElement parent, string text)
        {
            if (parent == null || string.IsNullOrEmpty(text))
                return;

            if (parent.SelectSingleNode("notes") != null)
            {
                XmlElement plain = CreatePlainNode(text);
                parent.SelectSingleNode("notes").AppendChild(plain);
            }
            else
            {
                XmlElement notes = contentDom.CreateElement("notes");

                XmlElement plain = CreatePlainNode(text);
                notes.AppendChild(plain);

                parent.AppendChild(notes);
            }
        }

        private void SaveLink(XmlElement parent, Link[] links, MindMap mindMap)
        {
            if (parent == null || links == null)
                return;

            XmlElement node = contentDom.CreateElement("relationships");
            foreach (Link link in links)
                SaveLink(node, link, mindMap);

            parent.AppendChild(node);
        }
        private void SaveLink(XmlElement parent, Link link, MindMap mindMap)
        {
            XmlElement node = contentDom.CreateElement("relationship");
            // Attributes
            node.SetAttribute("end1", link.From.ID.ToString());
            node.SetAttribute("end2", link.Target.ID.ToString());
            node.SetAttribute("id", link.ID.ToString());
            // control-points
            XmlElement controlPoints = contentDom.CreateElement("control-points");
            controlPoints.AppendChild(CreatControlPoint(link.LayoutData.ControlPoint1.X, link.LayoutData.ControlPoint1.Y, 0));
            controlPoints.AppendChild(CreatControlPoint(link.LayoutData.ControlPoint2.X, link.LayoutData.ControlPoint2.Y, 1));
            // title
            node.AppendChild(CreateTitleNode(link.Text));
            node.AppendChild(controlPoints);
            parent.AppendChild(node);
        }

        private bool IsInternalImage(PictureWidget picWidget)
        {
            return String.IsNullOrEmpty(Path.GetDirectoryName(picWidget.ImageUrl));
        }
        private int FindInternalPicWidgetIndex(PictureWidget[] widgets)
        {
            for (int i = 0; i < widgets.Length; i++)
                if (IsInternalImage(widgets[i]))
                    return i;
            return -1;
        }
        class Attachment
        {
            public string id;
            public string name;
            public string filename;
            public IStaticDataSource staticDataSource;
            public Attachment(string id, string filename, IStaticDataSource staticDataSource)
            {
                this.id = id;
                this.name = filename;
                this.filename = Base64Encode(filename);
                this.staticDataSource = staticDataSource;
            }
            public Attachment(string id, string name, string filename, IStaticDataSource staticDataSource)
            {
                this.id = id;
                this.name = name;
                this.filename = Base64Encode(filename);
                this.staticDataSource = staticDataSource;
            }
        }
        class StaticDataSource : IStaticDataSource
        {
            byte[] bytes;

            public StaticDataSource(string str, Encoding encoding)
            {
                try
                {
                    this.bytes = encoding.GetBytes(str);
                }
                catch(Exception)
                {
                    this.bytes = new byte[0];
                }
            }
            public StaticDataSource(byte[] bytes)
            {
                this.bytes = bytes;
            }
            public StaticDataSource(XmlDocument dom)
            {
                try
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        dom.Save(ms);
                        this.bytes = ms.ToArray();
                    }
                }
                catch (Exception)
                {
                    this.bytes = new byte[0];
                }
            }
            public StaticDataSource(Image data)
            {
                try
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        data.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        this.bytes = ms.ToArray();
                    }
                }
                catch (Exception)
                {
                    this.bytes = new byte[0];
                }
            }
            public StaticDataSource(string filePath)
            {
                try
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {

                        this.bytes = new byte[fs.Length];
                        fs.Read(bytes, 0, (int)fs.Length);
                    }
                }
                catch (Exception)
                {
                    this.bytes = new byte[0];
                }
            }

            public Stream GetSource()
            {
                Stream s = new MemoryStream(bytes);
                return s;
            }
        }
        static string Base64Encode(string text)
        {
            if (string.IsNullOrEmpty(text))
                return String.Empty;
            byte[] buff = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(buff);
        }
    }
}
