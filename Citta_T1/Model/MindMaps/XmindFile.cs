using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using C2.Canvas;
using C2.Controls.Paint;
using C2.Core;
using C2.Model;
using C2.Model.Documents;
using C2.Model.Widgets;

namespace C2.Model.MindMaps
{
    class XmindFile
    {
        static String GenUUID()
        {
            return System.Guid.NewGuid().ToString();
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

            foreach (ChartPage chartPage in charts)
            {
                if (chartPage is MindMap)
                    SaveSheet(chartPage as MindMap, root); // TODO 考虑一下sheet的顺序问题
            }
            dom.Save(filename);
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

            SaveSheet(mindMap, root);
            dom.Save(filename);
        }
        private static void SaveSheet(MindMap mindMap, XmlElement parent)
        {
            // 单sheet / chart
            XmlDocument dom = parent.OwnerDocument;
            XmlElement sheet = dom.CreateElement("sheet");
            sheet.SetAttribute("id", GenUUID());
            parent.AppendChild(sheet);

            dom.AppendChild(parent);
            // topics
            SaveMindMap(parent, mindMap.Root, mindMap);
            // Links / relations
            /* TODO
             * 有点麻烦 Xmind里topic和relations是分来的
             * SaveRelations();
             */
        }
        private static void SaveRelations()
        {
            throw new NotImplementedException();
        }

        static void SaveMindMap(XmlElement parent, Topic topic, MindMap mindMap)
        {
            if (parent == null || topic == null)
                return;
            XmlDocument dom = parent.OwnerDocument;
            XmlElement topicNode = dom.CreateElement("topic");
            // Attributes
            topicNode.SetAttribute("id", GenUUID());
            if (topic.Folded)
                topicNode.SetAttribute("branch", "folded");
            // Elements
            // title / text
            XmlElement title = dom.CreateElement("title");
            title.Value = topic.Text;
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
                SerializeRemark(topicNode, topic.Remark);
            }

            // children
            if (topic.Children.Count > 0)
            {
                XmlElement children = dom.CreateElement("children");
                XmlElement topics = dom.CreateElement("topics");
                topics.SetAttribute("type", "attached");

                children.AppendChild(topics);
                topicNode.AppendChild(children);
                foreach (Topic subTopic in topic.Children)
                {
                    SaveMindMap(topicNode, subTopic, mindMap);
                }
            }
        }

        static void SerializeRemark(XmlElement node, string remark)
        {
            if (node == null || string.IsNullOrEmpty(remark))
                return;

            XmlElement notes = node.OwnerDocument.CreateElement("notes");
            notes.Value = remark;
            node.AppendChild(notes);
        }

        static void SaveLink(XmlElement parent, Link link, MindMap mindMap)
        {
            if (parent == null || link == null || string.IsNullOrEmpty(link.TargetID))
                return;

            XmlElement relationship = parent.OwnerDocument.CreateElement("relationship");
            // TODO 需要在dom里面找到起点终点的id
            relationship.SetAttribute("end1", "");
            relationship.SetAttribute("end2", "");
            relationship.SetAttribute("id", GenUUID());
        }
    }
}
