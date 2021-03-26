using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using C2.Model.Documents;
using C2.Model.Widgets;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;

namespace C2.Model.MindMaps
{
    class DocxFile
    {
       
    }

    public class DocxFileSaver 
    {
       
        private void WriteNoteToDocx(Widget topicNote,XWPFDocument docx) 
        {
            if (topicNote != null)
            {
                XWPFParagraph noteText = docx.CreateParagraph();
                noteText.Style = "a0";
                XWPFRun xwpfRun = noteText.CreateRun();
                xwpfRun.FontFamily = "宋体";
                xwpfRun.SetText(topicNote.Text);
            }
        }
        private List<int> ChangeImgSize(int width, int height) 
        {
            List<int> size = new List<int>();
            if (width > height)
            {
                height = height * 768 / width;
                width = 768;
            }
            else 
            {
                width = width * 768 / height;
                height = 768;
            }
            size.Add(width);
            size.Add(height);
            return size;
        }
        private void WriteImgToDocx(PictureWidget pictureWidget, XWPFDocument docx,int imgNo) 
        {
            string picturePath = pictureWidget.ImageUrl;
            int width = pictureWidget.ThumbImage.Width;
            int height = pictureWidget.ThumbImage.Height;
            if (!File.Exists(picturePath))
                return;
            try
            {
                var fileStream = new FileStream(picturePath, FileMode.Open);
                CT_P m_p = docx.Document.body.AddNewP();
                m_p.AddNewPPr().AddNewJc().val = ST_Jc.center;
                XWPFParagraph paragraphIMG = new XWPFParagraph(m_p,docx);
                XWPFRun xwpfRun = paragraphIMG.CreateRun();
                List<int> size = new List<int>();
                size = ChangeImgSize(width, height);
                if(size.Count == 2)
                    xwpfRun.AddPicture(fileStream, (int)PictureType.JPEG, "test.png", size[0] * 846, size[1] * 846);//长宽单位为emu，在1080分辨率下换算单位为1像素等于846emu
                xwpfRun.FontFamily = "宋体";
                xwpfRun.SetText("图" + imgNo);
            }
            catch 
            { 

            }
            
        }

        private void WriteTitleToDocx(string title, XWPFDocument docx, int layer) 
        {
         
            XWPFParagraph paragraphTitle = docx.CreateParagraph();          
            if (layer < 4)
            {
                switch (layer)
                {
                    case 0:
                        paragraphTitle.Style = "1";
                        break;
                    case 1:
                        paragraphTitle.Style = "2";
                        break;
                    case 2:
                        paragraphTitle.Style = "3";
                        break;
                    case 3:
                        paragraphTitle.Style = "4";
                        break;     
                }
                XWPFRun xwpfRun = paragraphTitle.CreateRun();
                xwpfRun.SetText(title);
            }
            else
            {
                paragraphTitle.Style = "a0";
                XWPFRun xwpfRun = paragraphTitle.CreateRun();
                xwpfRun.FontFamily = "宋体";
                xwpfRun.SetText(title);
            }
           
        }

        private Widget GetTopicNote(Topic topic) 
        {
            
            NoteWidget noteWidget = topic.FindWidget<NoteWidget>();
            return noteWidget;
        }
        private List<PictureWidget> GetTopicPicture(Topic topic) 
        {
            List<PictureWidget> topicPictures = new List<PictureWidget>();
            PictureWidget[] pictureWidgets = topic.FindWidgets<PictureWidget>();
            foreach (PictureWidget pictureWidget in pictureWidgets)
            {
                if (Directory.Exists(pictureWidget.ImageUrl) && (pictureWidget.ThumbImage.Width > 128 || pictureWidget.ThumbImage.Width > 128))
                {
                    topicPictures.Add(pictureWidget);
                }
            }
            return topicPictures;
        }
        private void WriteToDocx(Topic topic, XWPFDocument DocxExample, XWPFDocument docx, int layer) 
        {
            XWPFStyles newStyles = docx.CreateStyles();
            newStyles.SetStyles(DocxExample.GetCTStyle());//复制模板格式

            //写入标题
           
            string title = topic.Text;
            WriteTitleToDocx(title, docx, layer);
          
            //在标题后写入内容

            Widget topicNote = GetTopicNote(topic);
            WriteNoteToDocx(topicNote, docx);

            //在内容后插入图片
            int imgNo = 0;
            List<PictureWidget> topicPictures = GetTopicPicture(topic);
            for (int i = 0; i < topicPictures.Count; i++)
            {
                PictureWidget pictureWidget = topicPictures[i];
                WriteImgToDocx(pictureWidget, docx, imgNo);
                imgNo++;
            }
            layer++;
            if (topic.Children.Count > 0)
            {
                foreach (Topic subTopic in topic.Children)
                {
                    WriteToDocx(subTopic, DocxExample, docx, layer);
                }
            }

        }
        public void SaveAsDocx(Topic topic , string fileName) 
        {
            try
            {
                using (var dotStream = new FileStream(Path.Combine(Application.StartupPath, "Resources", "DocxFileExp", "DocxExample.dotx"), FileMode.Open, FileAccess.Read))
                {

                    XWPFDocument DocxExample = new XWPFDocument(dotStream);
                    try
                    {
                        using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                        {
                            int layer = 0;
                            XWPFDocument docx = new XWPFDocument();
                            WriteToDocx(topic, DocxExample, docx, layer);
                            docx.Write(fileStream);
                            docx.Close();
                            fileStream.Close();
                        }

                        DocxExample.Close();
                        dotStream.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("错误:" + ex);
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("错误:"+ ex);
            }
        }
    }
}
