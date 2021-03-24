using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using C2.Model.Widgets;
using NPOI.XWPF.UserModel;

namespace C2.Model.MindMaps
{
    class DocxFile
    {
    }

    class DocxFileSaver 
    {
        XWPFDocument MyDoc = new XWPFDocument();

        private void WriteToDocx() 
        { 

        }

        private void WriteImgToDocx() 
        { 
        }

        private void WriteTableToDocx() 
        { 
        }

        private List<Widget> GetTopicInfo(Topic topic) 
        {
            List<Widget> topicInfo = new List<Widget>();
            PictureWidget[] pictureWidgets = topic.FindWidgets<PictureWidget>();
            NoteWidget noteWidget = topic.FindWidget<NoteWidget>();
            //ChartWidget chartWidget =  topic.FindWidget<ChartWidget>();
            topicInfo.Add(noteWidget);
            //topicInfo.Add(chartWidget);
            foreach (PictureWidget pictureWidget in pictureWidgets) 
            {
                if(Directory.Exists(pictureWidget.ImageUrl) && (pictureWidget.ThumbImage.Width > 128 || pictureWidget.ThumbImage.Width >128)) 
                {
                    topicInfo.Add(pictureWidget);
                }
            }
            return topicInfo;
        }

        private void SaveAsDocx(Topic topic , string filePath, string fileName, bool isRoot = true) 
        {
            using (var dotStream = new FileStream(Path.Combine(Application.StartupPath, "Resources", "DocxFileExp", "DocxExample.dotx"), FileMode.Open, FileAccess.Read))
            {
                XWPFDocument DocxExample = new XWPFDocument(dotStream);
                using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create, FileAccess.Write))
                {
                    XWPFDocument docx = new XWPFDocument(fileStream);
                    XWPFStyles newStyles = docx.CreateStyles();
                    newStyles.SetStyles(DocxExample.GetCTStyle());
                    string title = topic.XmlElementName;
                    XWPFParagraph paragraph = docx.CreateParagraph();
                    if (isRoot)
                    {
                        paragraph.Style = "标题1";
                        XWPFRun xwpfRun = paragraph.CreateRun();
                        xwpfRun.SetText(title);
                        isRoot = false;
                    }
                    List<Widget> topicInfos = new List<Widget>();
                    topicInfos = GetTopicInfo(topic);
                    if(topicInfos[0] != null) 
                    {
                        XWPFParagraph noteText = docx.CreateParagraph();
                        noteText.Style = "正文";
                        XWPFRun xwpfRun = noteText.CreateRun();
                        xwpfRun.SetText(topicInfos[0].Text);
                    }
                    for(int i =1;i< topicInfos.Count; i++) 
                    {
                        
                    }
                }
                DocxExample.Close();
            }
        }
    }
}
