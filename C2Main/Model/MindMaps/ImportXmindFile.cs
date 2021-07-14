
using C2.Core;
using C2.Forms;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;


namespace C2.Model.MindMaps
{
    public class ImportXmindFile
    {
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
            CreateC2(xmindName, xmlRoot, searchTopic, xmlns);
            DeleteDir(tmpDir);
        }

        public void CreateC2(string xmindName, Topic xmlRoot, XmlNodeList SearchTopic, XmlNamespaceManager xmlns)
        {
            C2.Model.Documents.Document doc2 = Global.GetMainForm().CreateNewMapForWord(xmindName);
            doc2.FileName = xmindName;
            DocumentForm form = new DocumentForm(doc2);
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

                mindMapRoot.Children.Insert(mindMapRoot.Children.Count, newXmlRoot);
                RecursionTopic(node, newXmlRoot, xmlns);
            }
        }

        public void RecursionTopic(XmlNode node, Topic mindMap_root, XmlNamespaceManager xmlns)
        {
            XmlNodeList nodeList = node.SelectNodes("ns:children/ns:topics/ns:topic", xmlns);
            foreach (XmlNode nodeSingle in nodeList)
            {
                Topic xmlRoot = new Topic();
                String nodeTitle = nodeSingle["title"].InnerText;
                xmlRoot.Text = nodeTitle;
                mindMap_root.Children.Insert(mindMap_root.Children.Count, xmlRoot);
                RecursionTopic(nodeSingle, xmlRoot, xmlns);
            }
        }

        private void DeleteDir(string tmpDir)
        {
            FileUtil.DeleteDirectory(tmpDir);
        }
    }
}
