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
        class StringDataSource : IStaticDataSource
        {
            public string Str { get; set; }

            public StringDataSource(string str)
            {
                this.Str = str;
            }

            public Stream GetSource()
            {
                Stream s = new MemoryStream(Encoding.UTF8.GetBytes(Str));
                return s;
            }
        }
        public static void SaveFile(IEnumerable<ChartPage> charts, string filename)
        {
            XmlDocument dom = new XmlDocument();

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
                    SaveSheet(chartPage as MindMap, root, sheetID); // TODO 考虑一下sheet的顺序问题
                sheetID += 1;
            }
            dom.AppendChild(root);
            SaveFile(dom, filename);
        }

        public static void SaveFile(MindMap mindMap, string filename)
        {
            XmlDocument dom = new XmlDocument();

            XmlElement root = dom.CreateElement("xmap-content");
            root.SetAttribute("xmlns", "urn:xmind:xmap:xmlns:content:2.0");
            root.SetAttribute("xmlns:fo", "http://www.w3.org/1999/XSL/Format");
            root.SetAttribute("xmlns:svg", "http://www.w3.org/2000/svg");
            root.SetAttribute("xmlns:xhtml", "http://www.w3.org/1999/xhtml");
            root.SetAttribute("xmlns:xlink", "http://www.w3.org/1999/xlink");
            root.SetAttribute("version", "2.0");

            SaveSheet(mindMap, root, 1);
            dom.AppendChild(root);
            SaveFile(dom, filename);
        }
        private static void SaveFile(XmlDocument dom, string filename)
        {
            using (ZipFile zip = ZipFile.Create(filename))
            {
                zip.BeginUpdate();
                StringDataSource content = new StringDataSource(dom.OuterXml);
                //添加文件
                zip.Add(content, "content.xml");
                zip.CommitUpdate();
            }
        }
        private static void SaveSheet(MindMap mindMap, XmlElement parent, int sheetID)
        {
            // 单sheet / chart
            XmlElement sheet = parent.OwnerDocument.CreateElement("sheet");
            sheet.SetAttribute("id", sheetID.ToString());

            // topics
            SaveMindMap(sheet, mindMap.Root, mindMap);
            SaveLink(sheet, mindMap.GetLinks(false), mindMap);

            parent.AppendChild(sheet);
        }

        static void SaveMindMap(XmlElement parent, Topic topic, MindMap mindMap)
        {
            if (parent == null || topic == null)
                return;
            XmlDocument dom = parent.OwnerDocument;
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
            // image / picture
            /* TODO 暂时不做
             * Xmind image. 只能存一张图片，内置的图标能存很多
             * C2内置的图片和图标都是同一种控件，不好搞
            */
            if (topic.FindWidget<PictureWidget>() != null)
            {

            }
            // notes / remark
            if (String.IsNullOrEmpty(topic.Remark))
            {
                SaveRemark(topicNode, topic.Remark);
            }

            // children
            if (topic.Children.Count > 0)
            {
                XmlElement children = dom.CreateElement("children");
                XmlElement topics = dom.CreateElement("topics");
                topics.SetAttribute("type", "attached");


                foreach (Topic subTopic in topic.Children)
                {
                    SaveMindMap(topics, subTopic, mindMap);
                }

                children.AppendChild(topics);
                topicNode.AppendChild(children);
            }
            parent.AppendChild(topicNode);
        }

        static void SaveRemark(XmlElement parent, string remark)
        {
            if (parent == null || string.IsNullOrEmpty(remark))
                return;

            XmlElement notes = parent.OwnerDocument.CreateElement("notes");
            notes.Value = remark;

            parent.AppendChild(notes);
        }
        static void SaveLink(XmlElement parent, Link[] links, MindMap mindMap)
        {
            if (parent == null || links == null)
                return;
            XmlElement node = parent.OwnerDocument.CreateElement("relationships");
            foreach (Link link in links)
                SaveLink(node, link, mindMap);

            parent.AppendChild(node);
        }
        static void SaveLink(XmlElement parent, Link link, MindMap mindMap)
        {
            if (parent == null || link == null || string.IsNullOrEmpty(link.TargetID))
                return;
            XmlElement node = parent.OwnerDocument.CreateElement("relationship");
            node.SetAttribute("end1", link.From.ID.ToString());
            node.SetAttribute("end2", link.Target.ID.ToString());
            node.SetAttribute("id", link.ID.ToString());

            parent.AppendChild(node);
        }
    }
}
