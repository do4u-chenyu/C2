using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using C2.Canvas;
using C2.Core;
using C2.Model.Documents;
using C2.Model.Styles;
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
        const string notSavedeExternalDataTag = "[外部数据(未缓存)]";
        const string notSavedeInternalDataTag = "[内部数据(未缓存)]";
        const string defaultExt = ".txt";
        const int defaultIconSize = 32;
        const int defaultTopicStyleIndex = 1;
        List<string> defaultStyle = new List<string> { "centralTopic", "mainTopic", "relationship", "map" };
        XmlDocument contentDoc;
        XmlDocument styleDoc;
        XmlNamespaceManager contentNsMgr;
        XmlNamespaceManager styleNsMgr;
        List<AttachmentInfo> attachments;
        int linkCounter;
        string filename;
        int currSheetID;
        XmlElement contentRoot;
        XmlElement styleRoot;
        MindMapLayoutType layout;
        int styleCounter = 0;
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
                    SaveSheet(chartPage as MindMap, contentRoot, sheetID++);
            }
            contentDoc.AppendChild(contentRoot);
            styleDoc.AppendChild(styleRoot);
        }
        public XmindFileSaver(MindMap mindMap, string fn) : this(new ChartPage[] { mindMap }, fn) { }

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
                foreach (AttachmentInfo attachment in attachments)
                {
                    if (attachment.staticDataSource != null)
                        zip.Add(attachment.staticDataSource, "attachments/" + attachment.filename);
                    else
                        zip.Add(attachment.fullPath, "attachments/" + attachment.filename);
                }
                // META-INF/manifest.xml
                zip.Add(CreateMetaInf(), "META-INF/manifest.xml");

                zip.CommitUpdate();
            }
        }
        private void Initialize(string fn)
        {
            contentDoc = new XmlDocument();
            styleDoc = new XmlDocument();

            contentNsMgr = new XmlNamespaceManager(contentDoc.NameTable);
            contentNsMgr.AddNamespace("fo", "http://www.w3.org/1999/XSL/Format");
            contentNsMgr.AddNamespace("svg", "http://www.w3.org/2000/svg");
            contentNsMgr.AddNamespace("xhtml", "http://www.w3.org/1999/xhtml");
            contentNsMgr.AddNamespace("xlink", "http://www.w3.org/1999/xlink");

            styleNsMgr = new XmlNamespaceManager(styleDoc.NameTable);
            styleNsMgr.AddNamespace("fo", "http://www.w3.org/1999/XSL/Format");

            attachments = new List<AttachmentInfo>();
            linkCounter = 0;
            filename = fn;

            contentRoot = CreateRootFromCTDOM(contentDoc);
            styleRoot = CreateRootFromSTDOM(styleDoc);
        }
        private XmlElement CreateRootFromCTDOM(XmlDocument doc)
        {
            XmlElement root = doc.CreateElement("xmap-content");

            root.SetAttribute("xmlns", "urn:xmind:xmap:xmlns:content:2.0");
            root.Attributes.Append(CreateAttributeWithNs(doc, "xmlns", "fo", "http://www.w3.org/1999/XSL/Format"));
            root.Attributes.Append(CreateAttributeWithNs(doc, "xmlns", "svg", "http://www.w3.org/2000/svg"));
            root.Attributes.Append(CreateAttributeWithNs(doc, "xmlns", "xhtml", "http://www.w3.org/1999/xhtml"));
            root.Attributes.Append(CreateAttributeWithNs(doc, "xmlns", "xlink", "http://www.w3.org/1999/xlink"));
            root.SetAttribute("version", "2.0");

            return root;
        }
        private XmlElement CreateRootFromSTDOM(XmlDocument doc)
        {
            XmlElement root = doc.CreateElement("xmap-styles");

            root.SetAttribute("xmlns", "urn:xmind:xmap:xmlns:content:2.0");
            root.Attributes.Append(CreateAttributeWithNs(doc, "xmlns", "fo", "http://www.w3.org/1999/XSL/Format"));
            root.Attributes.Append(CreateAttributeWithNs(doc, "xmlns", "svg", "http://www.w3.org/2000/svg"));
            root.SetAttribute("version", "2.0");

            XmlElement stylesNode = styleDoc.CreateElement("styles");
            XmlElement autoStylesNode = styleDoc.CreateElement("automatic-styles");
            XmlElement masterStylesNode = styleDoc.CreateElement("master-styles");
            root.AppendChild(stylesNode);
            root.AppendChild(autoStylesNode);
            root.AppendChild(masterStylesNode);

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
            foreach (AttachmentInfo attachment in attachments)
                root.AppendChild(
                    CreateFileEntryNode(
                        metaDom,
                        "attachments/" + attachment.filename,
                        attachment.filename.EndsWith("png") ? "image/png" : String.Empty
                    )
                );
            // META-INF
            root.AppendChild(CreateFileEntryNode(metaDom, "META-INF/", String.Empty));
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
            sheet.SetAttribute("style-id", SaveSheetStyle(mindMap, sheetID));
            sheet.SetAttribute("theme", SaveSheetTheme(mindMap, sheetID));

            // topics
            SaveMindMap(sheet, mindMap.Root, mindMap);
            SaveLinks(sheet, mindMap.GetLinks(false), mindMap);

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
        /// <param name="mindMap"></param>
        /// <param name="isRoot"></param>
        private void SaveMindMap(XmlElement parent, Topic topic, MindMap mindMap, bool isRoot = true)
        {
            if (parent == null || topic == null)
                return;
            XmlElement topicNode = contentDoc.CreateElement("topic");
            // Attributes
            topicNode.SetAttribute("id", GenTopicID(topic));
            topicNode.SetAttribute("style-id", SaveTopicStyle(topic, mindMap));
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
            // 至少存在一个图表
            PictureWidget[] pictureWidgets = topic.FindWidgets<PictureWidget>();
            int firstInternalPWIndex = Math.Max(0, FindInternalPicWidgetIndex(pictureWidgets));
            for (int i = 0; i < pictureWidgets.Length; i++)
            {
                if (i == firstInternalPWIndex)
                    SavePwAsImg(topicNode, pictureWidgets[i]);
                else
                    SavePwAsAttachment(topicNode, pictureWidgets[i]);
                
            }
            /*
             * 附件挂件
             */
            //if (topic.FindWidgets<AttachmentWidget>().Length > 0)
            //{
            //    AttachmentWidget[] widgets = topic.FindWidgets<AttachmentWidget>();
            //    for (int i = 0; i < widgets.Length; i++)
            //        SaveAwAsAttachment(topicNode, widgets[i]);
            //}
            foreach (var widget in topic.FindWidgets<AttachmentWidget>())
                SaveAwAsAttachment(topicNode, widget);
            /*
             * 图表挂件，目前没有持久化，不保存了
             */
            if (topic.FindWidgets<ChartWidget>().Length > 0)
            {

            }
            /*
             * 数据挂件
             */
            // 本地数据和外部数据
            foreach(var widget in topic.FindWidgets<DataSourceWidget>())
                SaveDataSource(topicNode, widget);
            /*
             * 结果挂件
             * 1. 业务视图
             * 2. 高级模型
             */
            foreach(var widget in topic.FindWidgets<ResultWidget>())
                SaveResult(topicNode, widget);
            /*
             * 
             */
            // notes / remark
            foreach (var widget in topic.FindWidgets<NoteWidget>())
                SaveRemark(topicNode, widget.Text);

            // children
            if (topic.Children.Count > 0)
            {
                XmlElement tsn = (topicNode.SelectSingleNode("children/topics") as XmlElement);
                if (tsn == null)
                {
                    tsn = contentDoc.CreateElement("topics"); 
                    tsn.SetAttribute("type", "attached");
                    XmlElement cn = contentDoc.CreateElement("children");
                    cn.AppendChild(tsn);
                    topicNode.AppendChild(cn);
                }

                foreach (Topic subTopic in topic.Children)
                    SaveMindMap(tsn, subTopic, mindMap, isRoot);
            }
            parent.AppendChild(topicNode);
        }
        private string SaveSheetStyle(MindMap mindMap, int sheetID)
        {
            string id = String.Format("Sheet-{0}", sheetID);

            XmlElement styles = this.styleRoot.SelectSingleNode("styles") as XmlElement;
            XmlElement style = this.styleDoc.CreateElement("style");

            style.SetAttribute("id", id);
            style.SetAttribute("name", String.Empty);
            style.SetAttribute("type", "map");

            XmlElement map = this.styleDoc.CreateElement("map-properties");
            if (mindMap.BackColor != null)
                map.Attributes.Append(CreateAttributeWithNs(this.styleDoc, "svg", "fill", ArgbToRgb(mindMap.BackColor)));
            if (mindMap.ForeColor != null)
                map.Attributes.Append(CreateAttributeWithNs(this.styleDoc, "fo", "color", ArgbToRgb(mindMap.ForeColor)));
            if (mindMap.Font != null)
                map.Attributes.Append(CreateAttributeWithNs(this.styleDoc, "fo", "font-family", mindMap.Font.FontFamily.Name));
            if (mindMap.BorderColor != null)
                map.SetAttribute("border-line-color", ArgbToRgb(mindMap.BorderColor));
            if (mindMap.LineColor != null)
                map.SetAttribute("line-color", ArgbToRgb(mindMap.LineColor));
            map.SetAttribute("line-width", mindMap.LineWidth.ToString());

            style.AppendChild(map);
            styles.AppendChild(style);

            return id;
        }
        /// <summary>
        /// 仅保存sheet的style
        /// 1. centralTopic
        /// 2. mainTopic
        /// 3. relationship
        /// 4. map
        /// </summary>
        /// <param name="parent"></param>
        /// <param name=""></param>
        private string SaveSheetTheme(MindMap mindMap, int sheetID)
        {
            string id = String.Format("Sheet-theme-{0}", sheetID);

            XmlElement autoStyles = this.styleRoot.SelectSingleNode("automatic-styles") as XmlElement;
            XmlElement masterStyles = this.styleRoot.SelectSingleNode("master-styles") as XmlElement;

            // style
            XmlElement style = this.styleDoc.CreateElement("style");
            style.SetAttribute("id", id);
            style.SetAttribute("type", "theme");
            // theme-properties
            // default style
            XmlElement theme = this.styleDoc.CreateElement("theme-properties");
            foreach (string styleName in this.defaultStyle)
            {
                XmlElement dStyle = this.styleDoc.CreateElement("default-style");
                dStyle.SetAttribute("style-family", styleName);
                dStyle.SetAttribute("style-id", SaveThemeStyle(autoStyles, styleName, mindMap));
                theme.AppendChild(dStyle);
            }
            style.AppendChild(theme);
            masterStyles.AppendChild(style);
            return id;
        }
        private string SaveThemeStyle(XmlElement parent, string styleName, MindMap mindMap)
        {
            XmlElement style = this.styleDoc.CreateElement("style");
            style.SetAttribute("id", GenStyleID());
            switch (styleName)
            {
                case "subTopic":
                case "centralTopic":
                case "mainTopic":
                    XmlElement mainTopic = this.styleDoc.CreateElement("topic-properties");
                    if (mindMap.Font != null)
                    {
                        mainTopic.Attributes.Append(CreateAttributeWithNs(this.styleDoc, "fo", "font-family", mindMap.Font.FontFamily.ToString()));
                        mainTopic.Attributes.Append(CreateAttributeWithNs(this.styleDoc, "fo", "font-size", mindMap.Font.Size.ToString()));
                    }
                    if (mindMap.NodeForeColor != null)
                        mainTopic.Attributes.Append(CreateAttributeWithNs(this.styleDoc, "fo", "font-size", ArgbToRgb(mindMap.NodeForeColor)));
                    if (mindMap.BorderColor != null)
                        mainTopic.SetAttribute("border-line-color", ArgbToRgb(mindMap.BorderColor));
                    if (mindMap.LineColor != null)
                        mainTopic.SetAttribute("line-color", ArgbToRgb(mindMap.LineColor));
                    if (mindMap.BackColor != null)
                        mainTopic.Attributes.Append(CreateAttributeWithNs(this.styleDoc, "svg", "fill", ArgbToRgb(mindMap.NodeBackColor)));
                    mainTopic.SetAttribute("shape-class", GetShapeStyle(TopicShape.Default));
                    style.AppendChild(mainTopic);
                    parent.AppendChild(style);
                    break;
                case "relationship":
                    XmlElement relationship = this.styleDoc.CreateElement("relationship-properties");
                    if (mindMap.ForeColor != null)
                        relationship.Attributes.Append(CreateAttributeWithNs(this.styleDoc, "fo", "color", ArgbToRgb(mindMap.ForeColor)));
                    if (mindMap.Font != null)
                    {
                        relationship.Attributes.Append(CreateAttributeWithNs(this.styleDoc, "fo", "font-family", mindMap.Font.FontFamily.ToString()));
                        relationship.Attributes.Append(CreateAttributeWithNs(this.styleDoc, "fo", "font-size", mindMap.Font.Size.ToString()));
                    }
                    style.AppendChild(relationship);
                    parent.AppendChild(style);
                    break;
                case "map":
                    XmlElement map = this.styleDoc.CreateElement("map-properties");
                    map.SetAttribute("color-gradient", "none");
                    map.SetAttribute("line-tapered", "none");
                    map.SetAttribute("multi-line-colors", "none");
                    if (mindMap.BackColor != null)
                        map.Attributes.Append(CreateAttributeWithNs(this.styleDoc, "svg", "fill", ArgbToRgb(mindMap.BackColor)));
                    style.AppendChild(map);
                    parent.AppendChild(style);
                    break;
                default:
                    break;
            }
            return style.GetAttribute("id");
        }
        private string GenStyleID()
        {
            return String.Format("{0}", styleCounter++);
        }
        private string SaveTopicStyle(Topic topic, MindMap mindMap)
        {
            string id = GenStyleID();

            XmlElement styles = this.styleRoot.SelectSingleNode("styles") as XmlElement;
            XmlElement style = this.styleDoc.CreateElement("style");
            XmlElement topicStyle = this.styleDoc.CreateElement("topic-properties");

            style.SetAttribute("id", id);
            style.SetAttribute("name", String.Empty);
            style.SetAttribute("type", "topic");

            topicStyle.Attributes.Append(CreateAttributeWithNs(this.styleDoc, "fo", "text-align", GetAlignment(topic.TextAlignment)));
            if (topic.Font != null)
            {
                // 字体排版
                if ((topic.Font.Style & FontStyle.Bold) == FontStyle.Bold)
                    topicStyle.Attributes.Append(CreateAttributeWithNs(this.styleDoc, "fo", "font-weight", "bold"));
                if ((topic.Font.Style & FontStyle.Strikeout) == FontStyle.Strikeout)
                    topicStyle.Attributes.Append(CreateAttributeWithNs(this.styleDoc, "fo", "text-decoration", "line-through"));
                if ((topic.Font.Style & FontStyle.Italic) == FontStyle.Italic)
                    topicStyle.Attributes.Append(CreateAttributeWithNs(this.styleDoc, "fo", "font-style", "italic"));
                //字体族
                topicStyle.Attributes.Append(CreateAttributeWithNs(this.styleDoc, "fo", "font-family", topic.Font.FontFamily.Name));
                topicStyle.Attributes.Append(CreateAttributeWithNs(this.styleDoc, "fo", "font-size", String.Format("{0}pt", Convert.ToInt32(topic.Font.Size))));
            }
            // 基础样式
            if (topic.Style != null)
            {
                topicStyle.SetAttribute("shape-class", GetShapeStyle(topic.Style.Shape));
                // 以下四种为空时，会继承mindMap/sheet的属性
                topicStyle.SetAttribute("border-line-color",
                    topic.Style.BorderColor.IsEmpty ? ArgbToRgb(mindMap.BorderColor) : ArgbToRgb(topic.Style.BorderColor));
                topicStyle.SetAttribute("line-color",
                    topic.Style.LineColor.IsEmpty ? ArgbToRgb(mindMap.LineColor) : ArgbToRgb(topic.Style.LineColor));
                topicStyle.Attributes.Append(CreateAttributeWithNs(this.styleDoc, "fo", "color",
                    topic.Style.ForeColor.IsEmpty ? ArgbToRgb(mindMap.NodeForeColor) : ArgbToRgb(topic.Style.ForeColor)));
                topicStyle.Attributes.Append(CreateAttributeWithNs(this.styleDoc, "svg", "fill",
                    topic.Style.BackColor.IsEmpty
                    ?
                    topic.Style.Shape == TopicShape.BaseLine ? ArgbToRgb(mindMap.BackColor) : ArgbToRgb(mindMap.NodeBackColor)
                    :
                    ArgbToRgb(topic.Style.BackColor)));
            }
            style.AppendChild(topicStyle);
            styles.AppendChild(style);
            return id;
        }
        private string GetLinePattern(DashStyle lineStyle)
        {
            switch (lineStyle)
            {
                case DashStyle.Dash:
                    return "dash";
                case DashStyle.DashDot:
                    return "dash-dot";
                case DashStyle.DashDotDot:
                    return "dash-dot-dot";
                case DashStyle.Custom:
                    return "dash";
                case DashStyle.Solid:
                    return "solid";
                case DashStyle.Dot:
                default:
                    return "dot";
            }
        }
        private string GetArrorEndClass(LineAnchor endCap)
        {
            string prefix = "org.xmind.arrowShape.";
            switch (endCap)
            {
                case LineAnchor.Arrow:
                    return prefix + "spearhead";
                case LineAnchor.Diamond:
                    return prefix + "diamond";
                case LineAnchor.Round:
                    return prefix + "normal";
                case LineAnchor.Square:
                    return prefix + "square";
                case LineAnchor.None:
                default:
                    return prefix + "none";
            }
        }
        private string GetAlignment(HorizontalAlignment alignment)
        {
            switch (alignment)
            {
                case HorizontalAlignment.Right:
                    return "right";
                case HorizontalAlignment.Center:
                    return "center";
                case HorizontalAlignment.Left:
                default:
                    return "left";
            }
        }
        private string GetShapeStyle(TopicShape shape)
        {
            string prefix = "org.xmind.topicShape.";
            switch (shape)
            {
                case TopicShape.Ellipse:
                    return prefix + "ellipse";
                case TopicShape.BaseLine:
                    return prefix + "underline";
                case TopicShape.Rectangle:
                default:
                    return prefix + "roundedRect";
            }
        }
        private string GetXmindLayout(MindMapLayoutType mmlt)
        {
            switch (mmlt)
            {
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
                case MindMapLayoutType.MindMap:
                default:
                    return "org.xmind.ui.map.anticlockwise";
            }
        }
        private string GetXmindAlignment(WidgetAlignment alignment)
        {
            switch (alignment)
            {
                case WidgetAlignment.Right:
                    return "right";
                case WidgetAlignment.Top:
                    return "top";
                case WidgetAlignment.Bottom:
                    return "bottom";
                case WidgetAlignment.Left:
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
            if (!File.Exists(widget.ImageUrl) && Path.IsPathRooted(widget.ImageUrl))
                return;
            AttachmentInfo attachment = new AttachmentInfo(
                attachments.Count.ToString(),
                attachmentTag + attachments.Count.ToString() + ".png",
                attachments.Count.ToString() + ".png",
                IsInternalImage(widget) ? Path.Combine(Global.ResourcesPath, "PictureIconLib", widget.ImageUrl) : widget.ImageUrl
                );

            attachments.Add(attachment);
            SaveAttachment(parent, attachment);
        }
        private void SaveAwAsAttachment(XmlElement parent, AttachmentWidget attachmentWidget)
        {
            foreach (string filePath in attachmentWidget.AttachmentPaths)
            {
                string filename = Path.GetFileName(filePath);
                AttachmentInfo attachment = new AttachmentInfo(
                                    attachments.Count.ToString(),
                                    attachmentTag + filename,
                                    filename,
                                    filePath
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
                    SaveLabel(parent, notSavedeExternalDataTag + dataItem.DataType.ToString() + "-" + dataItem.FileName);
                }
                else if (!dataItem.IsDatabase() && !File.Exists(dataItem.FilePath))
                {
                    SaveLabel(parent, notSavedeInternalDataTag + "本地数据" + "-" + dataItem.FileName);
                }
                else
                {
                    AttachmentInfo attachment;
                    if (dataItem.IsDatabase())
                    {
                        // 外部数据都没有后缀名
                        attachment = new AttachmentInfo(
                                            attachments.Count.ToString(),
                                            externalDataTag + dataItem.FileName + defaultExt,
                                            dataItem.FileName + defaultExt,
                                            new StaticDataSource(BCPBuffer.GetInstance().GetCachePreviewTable(dataItem.DBItem), Encoding.UTF8)
                                        );
                    }
                    else
                    {
                        string filename = Path.GetFileName(dataItem.FilePath);
                        attachment = new AttachmentInfo(
                                            attachments.Count.ToString(),
                                            internalDataTag + filename,
                                            filename,
                                            dataItem.FilePath
                                    );
                    }
                    attachments.Add(attachment);
                    SaveAttachment(parent, attachment);
                }
            }
        }
        private void SaveResult(XmlElement parent, ResultWidget resultWidget)
        {
            foreach (DataItem dataItem in resultWidget.DataItems)
            {
                if (!File.Exists(dataItem.FilePath))
                {
                    SaveLabel(parent, notSavedeInternalDataTag + "结果文件" + "-" + dataItem.FileName);
                }
                else
                {
                    AttachmentInfo attachment = new AttachmentInfo(
                            attachments.Count.ToString(),
                            resultDataTag + dataItem.FileName + defaultExt,
                            dataItem.FileName + defaultExt,
                            dataItem.FilePath
                        );
                    attachments.Add(attachment);
                    SaveAttachment(parent, attachment);
                }
            }
        }
        private void SaveAttachment(XmlElement topicNode, AttachmentInfo attachment)
        {
            // Xmind中 附件以节点形式存在
            if (!(topicNode.SelectSingleNode("children/topics") is XmlElement tn))
            {
                tn = contentDoc.CreateElement("topics");
                tn.SetAttribute("type", "attached");
                XmlElement cn = contentDoc.CreateElement("children");
                cn.AppendChild(tn);
                topicNode.AppendChild(cn);
            }
            tn.AppendChild(CreateAtcmtTopicNode(GenAttachmentID(topicNode, attachment), attachment));
        }
        private string GenAttachmentID(XmlElement topicNode, AttachmentInfo attachment)
        {
            return String.Format("attachment-{0}-{1}", topicNode.GetAttribute("id"), attachment.id);
        }
        private string GenLinkID()
        {
            this.linkCounter += 1;
            return linkCounter.ToString();
        }
        private string GenTopicID(Topic topic)
        {
            return this.currSheetID.ToString() + "-" + topic.ID.ToString();
        }
        private XmlAttribute CreateAttributeWithNs(XmlDocument doc, string prefix, string localName, string value)
        {
            XmlAttribute xa = doc.CreateAttribute(prefix, localName, contentNsMgr.LookupNamespace(prefix));
            xa.Value = value;
            return xa;
        }
        private XmlElement CreateElementWithNs(string prefix, string localName)
        {
            return contentDoc.CreateElement(prefix, localName, contentNsMgr.LookupNamespace(prefix));
        }
        private XmlElement CreateFileEntryNode(XmlDocument xd, string fullPath, string mediaType)
        {
            XmlElement node = xd.CreateElement("file-entry");
            node.SetAttribute("full-path", fullPath);
            node.SetAttribute("media-type", mediaType);
            return node;
        }
        private XmlElement CreateAtcmtTopicNode(string topicID, AttachmentInfo attachment)
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
                topic.Attributes.Append(CreateAttributeWithNs(contentDoc, "xlink", "href", "xap:attachments/" + attachmentName));
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
        private XmlElement CreateLabelNode(string text)
        {
            XmlElement label = contentDoc.CreateElement("label");
            label.InnerText = text;
            return label;
        }
        private XmlElement CreatControlPoint(Link link, int index)
        {
            /* 
             * TODO 不同图的控制点原点不同
             * 逻辑图(右边) 的控制点原点是Topic的右边中点
             */
            XmlElement cp = contentDoc.CreateElement("control-point");
            XmlElement pst = contentDoc.CreateElement("position");

            cp.SetAttribute("index", index.ToString());
            if (index == 0)
            {
                Point relativePoint = new Point(link.From.Location.X + link.From.Width, link.From.Location.Y + link.From.Height / 2);
                pst.Attributes.Append(CreateAttributeWithNs(contentDoc, "svg", "x", (link.LayoutData.ControlPoint1.X - relativePoint.X).ToString()));
                pst.Attributes.Append(CreateAttributeWithNs(contentDoc, "svg", "y", (link.LayoutData.ControlPoint1.Y - relativePoint.Y).ToString()));
            }
            else
            {
                Point relativePoint = new Point(link.Target.Location.X + link.Target.Width, link.Target.Location.Y + link.Target.Height / 2);
                pst.Attributes.Append(CreateAttributeWithNs(contentDoc, "svg", "x", (link.LayoutData.ControlPoint2.X - relativePoint.X).ToString()));
                pst.Attributes.Append(CreateAttributeWithNs(contentDoc, "svg", "y", (link.LayoutData.ControlPoint2.Y - relativePoint.Y).ToString()));
            }

            cp.AppendChild(pst);
            return cp;
        }
        private void SaveLabel(XmlElement parent, string text)
        {
            if (parent == null || string.IsNullOrEmpty(text))
                return;

            if(!(parent.SelectSingleNode("labels") is XmlElement labels))
            {
                labels = contentDoc.CreateElement("labels");
                parent.AppendChild(labels);
            }
            labels.AppendChild(CreateLabelNode(text));
        }
        /// <summary>
        /// 将C2图标变为Xmind Topic 图片，图片需要设置大小和布局
        /// </summary>
        /// <param name="widget"></param>
        /// <param name="parent"></param>
        private void SavePwAsImg(XmlElement parent, PictureWidget widget)
        {
            if (parent == null || widget.Data == null)
                return;
            AttachmentInfo attachment = new AttachmentInfo(
                attachments.Count.ToString(),
                attachmentTag + attachments.Count.ToString(),
                attachments.Count.ToString() + ".png",
                IsInternalImage(widget) ? Path.Combine(Global.ResourcesPath, "PictureIconLib", widget.ImageUrl) : widget.ImageUrl
                );
            attachments.Add(attachment);
            XmlElement xe = CreateElementWithNs("xhtml", "img");
            xe.Attributes.Append(CreateAttributeWithNs(contentDoc, "xhtml", "src", "xap:attachments/" + attachment.filename));
            xe.Attributes.Append(CreateAttributeWithNs(contentDoc, "svg", "height", defaultIconSize.ToString()));
            xe.Attributes.Append(CreateAttributeWithNs(contentDoc, "svg", "width", defaultIconSize.ToString()));
            xe.SetAttribute("align", GetXmindAlignment(widget.Alignment));

            parent.AppendChild(xe);
        }
        private void SaveRemark(XmlElement parent, string text)
        {
            if (parent == null || string.IsNullOrEmpty(text))
                return;

            if (!(parent.SelectSingleNode("notes") is XmlElement notes))
            {
                notes = contentDoc.CreateElement("notes");
                parent.AppendChild(notes);
            }

            notes.AppendChild(CreatePlainNode(text));
        }
        private void SaveLinks(XmlElement parent, Link[] links, MindMap mindMap)
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
            node.SetAttribute("id", GenLinkID());
            node.SetAttribute("style-id", SaveLinkStyle(link, mindMap));
            // control-points
            XmlElement controlPoints = contentDoc.CreateElement("control-points");
            controlPoints.AppendChild(CreatControlPoint(link, 0));
            controlPoints.AppendChild(CreatControlPoint(link, 1));
            // title
            node.AppendChild(CreateTitleNode(link.Text));
            node.AppendChild(controlPoints);
            parent.AppendChild(node);
        }
        private string SaveLinkStyle(Link link, MindMap mindMap)
        {
            string id = GenStyleID();
            XmlElement styles = this.styleRoot.SelectSingleNode("styles") as XmlElement;
            XmlElement style = this.styleDoc.CreateElement("style");
            style.SetAttribute("id", id);
            style.SetAttribute("type", "relationship");

            XmlElement linkStyle = this.styleDoc.CreateElement("relationship-properties");
            linkStyle.Attributes.Append(CreateAttributeWithNs(this.styleDoc, "fo", "color", ArgbToRgb(mindMap.ForeColor)));
            linkStyle.SetAttribute("arrow-end-class", GetArrorEndClass(link.EndCap));
            linkStyle.SetAttribute("line-color",
                link.Color.IsEmpty ? ArgbToRgb(mindMap.LinkLineColor) : ArgbToRgb(link.Color));

            linkStyle.SetAttribute("line-pattern", GetLinePattern(link.LineStyle));
            linkStyle.SetAttribute("line-width", String.Format("{0}pt", link.LineWidth));

            style.AppendChild(linkStyle);
            styles.AppendChild(style);
            return id;
        }
        private bool IsInternalImage(PictureWidget picWidget)
        {
            return String.IsNullOrEmpty(Path.GetDirectoryName(picWidget.ImageUrl));
        }
        /// <summary>
        /// 找图标的索引
        /// </summary>
        /// <param name="widgets"></param>
        /// <returns></returns>
        private int FindInternalPicWidgetIndex(PictureWidget[] widgets)
        {
            for (int i = 0; i < widgets.Length; i++)
                if (IsInternalImage(widgets[i]))
                    return i;
            return -1;
        }
        private string ArgbToRgb(Color color)
        {
            return String.Format("#{0}", BitConverter.ToString(new byte[] { color.R, color.G, color.B })).Replace("-", String.Empty);
        }
        class AttachmentInfo
        {
            public string id;
            public string name;
            public string filename;
            public string fullPath;
            public IStaticDataSource staticDataSource;
            public AttachmentInfo(string id, string name, string filename, IStaticDataSource staticDataSource)
            {
                this.id = id;
                this.name = name;
                this.filename = ST.GenerateMD5(Path.GetFileNameWithoutExtension(filename)) + Path.GetExtension(filename);
                this.staticDataSource = staticDataSource;
            }
            public AttachmentInfo(string id, string name, string filename, string fullPath)
            {
                this.id = id;
                this.name = name;
                this.filename = ST.GenerateMD5(Path.GetFileNameWithoutExtension(filename)) + Path.GetExtension(filename);
                this.fullPath = fullPath;
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
                return new MemoryStream(bytes);
            }
        }
    }
}
