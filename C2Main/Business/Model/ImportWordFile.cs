using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.Words;
using C2.Core;
using C2.Dialogs;
using C2.Forms;
using C2.Model.MindMaps;

namespace C2.Business.Model
{
    public class ImportWordFile
    {
        private int rootCount = 0;
        private static ImportWordFile ImportWordFileInstance;
        public static ImportWordFile GetInstance()
        {
            if (ImportWordFileInstance == null)
            {
                ImportWordFileInstance = new ImportWordFile();
            }
            return ImportWordFileInstance;
        }
        private bool IsRoot() 
        {
            if (rootCount > 1)
                return false;
            return true;
        }
        public void Import(string path)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("文件不存在");
                return;
            }
            rootCount = 0;
            CreateC2(LoadWord(path),path);
        }
        private List<List<string>> LoadWord(string path)
        {
            
            List<List<string>> titles = new List<List<string>>();
            try
            {
                Document doc = new Document(path);
                NodeCollection paragraphs = doc.GetChildNodes(NodeType.Paragraph, true);
                foreach (Paragraph paragraph in paragraphs)
                {
                    if (paragraph.Range.Text == string.Empty)
                        continue;
                    if (paragraph.ParagraphFormat.Style.StyleIdentifier == StyleIdentifier.Heading1)
                    {
                        List<string> titleInfo = new List<string>();
                        titleInfo.Add(paragraph.Range.Text);
                        titleInfo.Add("1");
                        titles.Add(titleInfo);
                        rootCount++;
                    }
                    if (paragraph.ParagraphFormat.Style.StyleIdentifier == StyleIdentifier.Heading2)
                    {
                        List<string> titleInfo = new List<string>();
                        titleInfo.Add(paragraph.Range.Text);
                        titleInfo.Add("2");
                        titles.Add(titleInfo);
                    }
                    if (paragraph.ParagraphFormat.Style.StyleIdentifier == StyleIdentifier.Heading3)
                    {
                        List<string> titleInfo = new List<string>();
                        titleInfo.Add(paragraph.Range.Text);
                        titleInfo.Add("3");
                        titles.Add(titleInfo);
                    }
                    if (paragraph.ParagraphFormat.Style.StyleIdentifier == StyleIdentifier.Heading4)
                    {
                        List<string> titleInfo = new List<string>();
                        titleInfo.Add(paragraph.Range.Text);
                        titleInfo.Add("4");
                        titles.Add(titleInfo);
                    }

                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
            return titles;
        }
        private void CreateC2(List<List<string>> titles,string path)
        {
            if (titles.Count == 0) 
            {
                MessageBox.Show("文档格式不正确，无法读取标题", "ERROR");
                return;
            }   
            string name = Path.GetFileNameWithoutExtension(path);
            CreateNewModelForm createNewModelForm = new CreateNewModelForm();
            if(createNewModelForm.CheckNameExist(name))
                return;
            C2.Model.Documents.Document doc = Global.GetMainForm().CreateNewMapForWord(name);
            doc.FileName = name;
            DocumentForm form = new DocumentForm(doc);
            Global.GetMainForm().ShowFormWord(form);
            MindMap mindMap = form.ActivedChartPage.Chart as MindMap;
            var root = mindMap.Root;
            root.Children.Remove(root.GetChildByText("子主题 1"));
            if (titles[0][1] == "1" && IsRoot())
                root.Text = titles[0][0].Trim('\f');
            try
            {
                if (IsRoot())
                {
                    CreatTopic(titles, root, 0);
                }
                else 
                {
                    Topic topic = new Topic();
                    topic.Text = titles[0][0].Trim('\f');
                    root.Children.Insert(0, topic);
                    CreatTopic(titles, topic, 0); 
                }
            }
            catch  
            {
                MessageBox.Show("文档格式不正确","ERROR");
            }
        }
        private void CreatTopic(List<List<string>> titles, Topic lastTopic, int j) 
        {
            if (titles.Count == 1)
                return;
            Topic topic = new Topic();
            topic.Text = titles[j + 1][0].Trim('\f');
            if (int.Parse(titles[j + 1][1]) > int.Parse(titles[j][1])) 
            { 
                lastTopic.Children.Insert(lastTopic.Children.Count, topic);
            }
            if (int.Parse(titles[j + 1][1]) == int.Parse(titles[j][1]))
            {
                lastTopic.ParentTopic.Children.Insert(lastTopic.ParentTopic.Children.Count, topic);
            }
            if (int.Parse(titles[j + 1][1]) - int.Parse(titles[j][1]) == -1)
            {
                lastTopic.ParentTopic.ParentTopic.Children.Insert(lastTopic.ParentTopic.ParentTopic.Children.Count, topic);
            }
            if (int.Parse(titles[j + 1][1]) - int.Parse(titles[j][1]) == -2)
            {
                lastTopic.ParentTopic.ParentTopic.ParentTopic.Children.Insert(lastTopic.ParentTopic.ParentTopic.ParentTopic.Children.Count, topic);
            }
            if (int.Parse(titles[j + 1][1]) - int.Parse(titles[j][1]) == -3)
            {
                lastTopic.ParentTopic.ParentTopic.ParentTopic.ParentTopic.Children.Insert(lastTopic.ParentTopic.ParentTopic.ParentTopic.ParentTopic.Children.Count, topic);
            }
            j++;
            if (j + 1 < titles.Count())
            {
                CreatTopic(titles, topic, j); 
            }
        }
    }
}
