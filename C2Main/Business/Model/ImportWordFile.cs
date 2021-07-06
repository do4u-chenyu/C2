using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Words;
using C2.Core;
using C2.Forms;
using C2.Model.MindMaps;

namespace C2.Business.Model
{
    public class ImportWordFile
    {
        private static ImportWordFile ImportWordFileInstance;
        public static ImportWordFile GetInstance()
        {
            if (ImportWordFileInstance == null)
            {
                ImportWordFileInstance = new ImportWordFile();
            }
            return ImportWordFileInstance;
        }
        public void Import(string path)
        {
            CreateC2(LoadWord(path),path);
        }
        private List<List<string>> LoadWord(string path)
        {
            List<List<string>> titles = new List<List<string>>();
            
            Document doc = new Document(path);
            // Get all paragraphs from the document.
            NodeCollection paragraphs = doc.GetChildNodes(NodeType.Paragraph, true);
            foreach (Paragraph paragraph in paragraphs)
            {
                if (paragraph.ParagraphFormat.Style.Name == "标题 1")
                {
                    List<string> titleInfo = new List<string>();
                    titleInfo.Add(paragraph.Range.Text);
                    titleInfo.Add("1");
                    titles.Add(titleInfo);
                }
                if (paragraph.ParagraphFormat.Style.Name == "标题 2")
                {
                    List<string> titleInfo = new List<string>();
                    titleInfo.Add(paragraph.Range.Text);
                    titleInfo.Add("2");
                    titles.Add(titleInfo);
                }
                if (paragraph.ParagraphFormat.Style.Name == "标题 3")
                {
                    List<string> titleInfo = new List<string>();
                    titleInfo.Add(paragraph.Range.Text);
                    titleInfo.Add("3");
                    titles.Add(titleInfo);
                }
                if (paragraph.ParagraphFormat.Style.Name == "标题 4")
                {
                    List<string> titleInfo = new List<string>();
                    titleInfo.Add(paragraph.Range.Text);
                    titleInfo.Add("4");
                    titles.Add(titleInfo);
                }
                
            }
            return titles;
        }
        private void CreateC2(List<List<string>> titles,string path)
        {
            string name = Path.GetFileNameWithoutExtension(path);
            C2.Model.Documents.Document doc = Global.GetMainForm().CreateNewMapForWord(name);
            DocumentForm form = new DocumentForm(doc);
            Global.GetMainForm().ShowFormWord(form);
            MindMap mindMap = form.ActivedChartPage.Chart as MindMap;
            var root = mindMap.Root;
            root.Children.Remove(root.GetChildByText("子主题 1"));
            if (titles[0][1] == "1")
                root.Text = titles[0][0];
            CreatTopic(titles,root,0);
        }
        private void CreatTopic(List<List<string>> titles, Topic lastTopic, int j) 
        {
            Topic topic = new Topic();
            topic.Text = titles[j + 1][0];
            if (int.Parse(titles[j + 1][1]) > int.Parse(titles[j][1])) 
            { 
                lastTopic.Children.Insert(0, topic);
               
            }
            if (int.Parse(titles[j + 1][1]) == int.Parse(titles[j][1]))
            {
                lastTopic.ParentTopic.Children.Insert(0, topic);

            }
            if (int.Parse(titles[j + 1][1]) - int.Parse(titles[j][1]) == -1)
            {
                lastTopic.ParentTopic.ParentTopic.Children.Insert(0, topic);

            }
            if (int.Parse(titles[j + 1][1]) - int.Parse(titles[j][1]) == -2)
            {
                lastTopic.ParentTopic.ParentTopic.ParentTopic.Children.Insert(0, topic);

            }
            j++;
            if (j + 1 < titles.Count())
                CreatTopic(titles, topic, j);
        }
    }
}
