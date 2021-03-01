﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using C2.Core;
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
        const string resultDataTag = "[结果文件]";
        const string notSavedextErnalDataTag = "[外部数据(未缓存)]";
        const string defaultExt = ".txt";
        const int defaultIconSize = 32;
        const int defaultTopicStyleIndex = 1;
        XmlDocument contentDoc;
        XmlDocument styleDoc;
        XmlNamespaceManager nsmgr;
        List<Attachment> attachments;
        string filename;
        int currSheetID;
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
            contentDoc.AppendChild(contentRoot);
        }
        public XmindFileSaver(MindMap mindMap, string fn)
        {
            Initialize(fn);

            SaveSheet(mindMap, contentRoot, 1);
            contentDoc.AppendChild(contentRoot);
        }
        private void Initialize(string fn)
        {
            contentDoc = new XmlDocument();

            nsmgr = new XmlNamespaceManager(contentDoc.NameTable);
            nsmgr.AddNamespace("fo", "http://www.w3.org/1999/XSL/Format");
            nsmgr.AddNamespace("svg", "http://www.w3.org/2000/svg");
            nsmgr.AddNamespace("xhtml", "http://www.w3.org/1999/xhtml");
            nsmgr.AddNamespace("xlink", "http://www.w3.org/1999/xlink");

            styleDoc = CreateStyleDOM();
            attachments = new List<Attachment>();
            filename = fn;

            contentRoot = CreateRootFromCTDOM(contentDoc);


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
                zip.Add(new StaticDataSource(contentDoc), "content.xml");
                zip.Add(new StaticDataSource(styleDoc), "styles.xml");
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
        private XmlElement CreateRootFromCTDOM(XmlDocument doc)
        {
            XmlElement root = doc.CreateElement("xmap-content");

            root.SetAttribute("xmlns", "urn:xmind:xmap:xmlns:content:2.0");
            root.Attributes.Append(CreateAttributeWithNs("xmlns", "fo", "http://www.w3.org/1999/XSL/Format"));
            root.Attributes.Append(CreateAttributeWithNs("xmlns", "svg", "http://www.w3.org/2000/svg"));
            root.Attributes.Append(CreateAttributeWithNs("xmlns", "xhtml", "http://www.w3.org/1999/xhtml"));
            root.Attributes.Append(CreateAttributeWithNs("xmlns", "xlink", "http://www.w3.org/1999/xlink"));
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
            root.AppendChild(CreateFileEntryNode(metaDom, "styles.xml", "text/xml"));
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
            XmlElement sheet = contentDoc.CreateElement("sheet");
            this.layout = mindMap.LayoutType;
            this.currSheetID = sheetID;
            sheet.SetAttribute("id", sheetID.ToString());

            // topics
            SaveMindMap(sheet, mindMap.Root);
            SaveLink(sheet, mindMap.GetLinks(false), mindMap);

            XmlElement title = contentDoc.CreateElement("title");
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
            XmlElement topicNode = contentDoc.CreateElement("topic");
            // Attributes
            topicNode.SetAttribute("id", this.currSheetID.ToString() + "-" + topic.ID.ToString());
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
            XmlElement title = contentDoc.CreateElement("title");
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
                    SaveAwAsAttachment(topicNode, widgets[i]);
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
                    SaveDataSource(topicNode, widgets[i]);
            }
            /*
             * 结果挂件
             * 1. 业务视图
             * 2. 高级模型
             */
            if (topic.FindWidgets<ResultWidget>().Length > 0)
            {
                // 本地数据和外部数据
                ResultWidget[] widgets = topic.FindWidgets<ResultWidget>();
                for (int i = 0; i < widgets.Length; i++)
                    SaveResult(topicNode, widgets[i]);
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
                    XmlElement tsn = contentDoc.CreateElement("topics");
                    XmlElement cn = contentDoc.CreateElement("children");
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
                    return "org.xmind.ui.map.anticlockwise";
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
                    return "org.xmind.ui.map.anticlockwise";
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
            SaveAttachment(parent, attachment);
        }
        private void SaveAwAsAttachment(XmlElement parent, AttachmentWidget attachmentWidget)
        {
            foreach (string filePath in attachmentWidget.AttachmentPaths)
            {
                string filename = Path.GetFileName(filePath);
                Attachment attachment = new Attachment(
                                    attachments.Count.ToString(),
                                    attachmentTag + filename,
                                    Path.GetFileName(filePath),
                                    new StaticDataSource(filePath)
                    );
                attachments.Add(attachment);
                SaveAttachment(parent, attachment);
            }
        }
        /// <summary>
        /// 数据源包含多种情况
        /// 1. 外部数据
        /// 2. 本地数据
        /// 3. 结果文件
        /// 其中外部数据如果拉取到本地则保存方式和本地数据、结果文件一致，保存为附件
        /// 如外部数据没有拉取到本地则保存为标签
        /// </summary>
        /// <param name="dataSourceWidget"></param>
        /// <param name="parent"></param>
        private void SaveDataSource(XmlElement parent, DataSourceWidget dataSourceWidget)
        {
            foreach (DataItem dataItem in dataSourceWidget.DataItems)
            {
                if (dataItem.IsDatabase() && !BCPBuffer.GetInstance().IsDBDataCached(dataItem.DBItem))
                {
                    SaveLabel(parent, notSavedextErnalDataTag + dataItem.DataType.ToString() + "-" + dataItem.FileName);
                    return;
                }

                Attachment attachment;
                if (dataItem.IsDatabase())
                {
                    // 外部数据都没有后缀名
                    attachment = new Attachment(
                                        attachments.Count.ToString(),
                                        externalDataTag + dataItem.FileName + defaultExt,
                                        dataItem.FileName + defaultExt,
                                        new StaticDataSource(BCPBuffer.GetInstance().GetCachePreviewTable(dataItem.DBItem), Encoding.UTF8)
                                    );
                }
                else
                {
                    string filename = Path.GetFileName(dataItem.FilePath);
                    attachment = new Attachment(
                                        attachments.Count.ToString(),
                                        internalDataTag + filename,
                                        filename,
                                        new StaticDataSource(dataItem.FilePath)
                                );
                }
                if (attachment != null)
                {
                    attachments.Add(attachment);
                    SaveAttachment(parent, attachment);
                }
            }
        }
        private void SaveResult(XmlElement parent, ResultWidget resultWidget)
        {
            foreach (DataItem dataItem in resultWidget.DataItems)
            {
                Attachment attachment = new Attachment(
                                            attachments.Count.ToString(),
                                            resultDataTag + dataItem.FileName + defaultExt,
                                            dataItem.FileName + defaultExt,
                                            new StaticDataSource(dataItem.FilePath)
                                        );
                attachments.Add(attachment);
                SaveAttachment(parent, attachment);
            }
        }
        private void SaveAttachment(XmlElement topicNode, Attachment attachment)
        {
            // Xmind中 附件以节点形式存在
            if (topicNode.SelectSingleNode("children/topics") != null)
            {
                XmlElement topic = CreateAtcmtTopicNode(String.Format("{0}-{1}", topicNode.GetAttribute("id"), attachment.id), attachment);

                topicNode.SelectSingleNode("children/topics").AppendChild(topic);
            }
            else
            {
                XmlElement cn = contentDoc.CreateElement("children");
                XmlElement tn = contentDoc.CreateElement("topics");
                tn.SetAttribute("type", "attached");

                XmlElement topic = CreateAtcmtTopicNode(String.Format("{0}-{1}", topicNode.GetAttribute("id"), attachment.id), attachment);

                tn.AppendChild(topic);
                cn.AppendChild(tn);
                topicNode.AppendChild(cn);
            }
        }
        private XmlAttribute CreateAttributeWithNs(string prefix, string localName, string value)
        {
            XmlAttribute xa = contentDoc.CreateAttribute(prefix, localName, nsmgr.LookupNamespace(prefix));
            xa.Value = value;
            return xa;
        }
        private XmlElement CreateElementWithNs(string prefix, string localName)
        {
            return contentDoc.CreateElement(prefix, localName, nsmgr.LookupNamespace(prefix));
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

        private XmlElement CreateTopicNode(string nodeID, string titleText, string attachmentName = "")
        {
            XmlElement topic = contentDoc.CreateElement("topic");
            XmlElement title = contentDoc.CreateElement("title");
            title.InnerText = titleText;
            topic.SetAttribute("id", nodeID);
            if (!String.IsNullOrEmpty(attachmentName))
                topic.Attributes.Append(CreateAttributeWithNs("xlink", "href", "xap:attachments/" + attachmentName));
            topic.AppendChild(title);
            return topic;
        }
        private XmlElement CreatePlainNode(string text)
        {
            XmlElement plain = contentDoc.CreateElement("plain");
            plain.InnerText = text;
            return plain;
        }
        private XmlElement CreateTitleNode(string text)
        {
            XmlElement title = contentDoc.CreateElement("title");
            title.InnerText = text;
            return title;
        }
        private XmlElement CreateLablNode(string text)
        {
            XmlElement label = contentDoc.CreateElement("label");
            label.InnerText = text;
            return label;
        }
        private XmlElement CreatControlPoint(Link link, int index)
        {
            /* 
             * TODO 不同图的控制点原点不同
             * 逻辑图(右边) 的控制点原点是Topic的
             */
            XmlElement cp = contentDoc.CreateElement("control-point");
            XmlElement pst = contentDoc.CreateElement("position");

            cp.SetAttribute("index", index.ToString());
            if (index == 0)
            {
                Point relativePoint = new Point(link.From.Location.X + link.From.Width, link.From.Location.Y + link.From.Height / 2);
                pst.Attributes.Append(CreateAttributeWithNs("svg", "x", (link.LayoutData.ControlPoint1.X - relativePoint.X).ToString()));
                pst.Attributes.Append(CreateAttributeWithNs("svg", "y", (link.LayoutData.ControlPoint1.Y - relativePoint.Y).ToString()));
            }
            else
            {
                Point relativePoint = new Point(link.Target.Location.X + link.Target.Width, link.Target.Location.Y + link.Target.Height / 2);
                pst.Attributes.Append(CreateAttributeWithNs("svg", "x", (link.LayoutData.ControlPoint2.X - relativePoint.X).ToString()));
                pst.Attributes.Append(CreateAttributeWithNs("svg", "y", (link.LayoutData.ControlPoint2.Y - relativePoint.Y).ToString()));
            }

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
                XmlElement labels = contentDoc.CreateElement("labels");

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
            XmlElement xe = CreateElementWithNs("xhtml", "img");
            xe.Attributes.Append(CreateAttributeWithNs("xhtml", "src", "xap:attachments/" + attachment.filename));
            xe.Attributes.Append(CreateAttributeWithNs("svg", "height", defaultIconSize.ToString()));
            xe.Attributes.Append(CreateAttributeWithNs("svg", "width", defaultIconSize.ToString()));
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
                XmlElement notes = contentDoc.CreateElement("notes");

                XmlElement plain = CreatePlainNode(text);
                notes.AppendChild(plain);

                parent.AppendChild(notes);
            }
        }

        private void SaveLink(XmlElement parent, Link[] links, MindMap mindMap)
        {
            if (parent == null || links == null)
                return;

            XmlElement node = contentDoc.CreateElement("relationships");
            foreach (Link link in links)
                SaveLink(node, link, mindMap);

            parent.AppendChild(node);
        }
        private void SaveLink(XmlElement parent, Link link, MindMap mindMap)
        {
            XmlElement node = contentDoc.CreateElement("relationship");
            // Attributes
            node.SetAttribute("end1", this.currSheetID.ToString() + "-" + link.From.ID.ToString());
            node.SetAttribute("end2", this.currSheetID.ToString() + "-" + link.Target.ID.ToString());
            node.SetAttribute("id", this.currSheetID.ToString() + "-" + link.ID.ToString());
            // control-points
            XmlElement controlPoints = contentDoc.CreateElement("control-points");
            controlPoints.AppendChild(CreatControlPoint(link, 0));
            controlPoints.AppendChild(CreatControlPoint(link, 1));
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
            public Attachment(string id, string name, string filename, IStaticDataSource staticDataSource)
            {
                this.id = id;
                this.name = name;
                this.filename = GenerateMD5(Path.GetFileNameWithoutExtension(filename)) + Path.GetExtension(filename);
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
                catch (Exception)
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
        static string GenerateMD5(string text)
        {
            using (MD5 mi = System.Security.Cryptography.MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(text);
                //开始加密
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
