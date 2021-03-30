﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using C2.Model.Widgets;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;

namespace C2.Model.MindMaps
{
    class DocxFile
    {
       //TODO Save
    }

    public class DocxFileSaver 
    {
        private int imgNo = 0;
        private void WriteNoteToDocx(Widget topicNote, XWPFDocument docx) 
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
        private Size RotateImageSize(int width, int height) 
        {
            return width > height ? new Size(768, height * 768 / width) : new Size(width * 768 / height, 768);
        }
        private void WriteImgToDocx(PictureWidget pictureWidget, XWPFDocument docx,int imgNo) //TODO
        {
            string picturePath = pictureWidget.ImageUrl;
            if (!File.Exists(picturePath))
                return;
            string fileName = Path.GetFileName(picturePath);
            try
            {
                int width = pictureWidget.Data.Width;
                int height = pictureWidget.Data.Height;
                var fileStream = new FileStream(picturePath, FileMode.Open);  // TODO
                //CT_P m_p = docx.Document.body.AddNewP();
                //m_p.AddNewPPr().AddNewJc().val = ST_Jc.center;   // TODO 返回null就崩了
                //XWPFParagraph paragraphIMG = new XWPFParagraph(m_p,docx); //TODO
                XWPFParagraph paragraphIMG = docx.CreateParagraph(); ;
                Size size = RotateImageSize(width, height);

                //长宽单位为emu，在1080分辨率下换算单位为1像素等于846emu
                XWPFRun xwpfRun = paragraphIMG.CreateRun();
                xwpfRun.AddPicture(fileStream, (int)PictureType.PNG, fileName, size.Width * 846, size.Height * 846);
                //NPOI.OpenXmlFormats.Dml.WordProcessing.CT_Inline inline = xwpfRun.GetCTR().GetDrawingList()[0].inline[0];
                //inline.docPr.id = (uint)imgNo;
                xwpfRun.FontFamily = "宋体";
                xwpfRun.SetText("图" + imgNo);
                fileStream.Close();
            }
            catch 
            { 
                // TODO
            }
            
        }

        private void WriteTitleToDocx(string title, XWPFDocument docx, int layer) 
        {   
            XWPFParagraph paragraphTitle = docx.CreateParagraph();

            paragraphTitle.Style = layer >= 4 ? "a0" : layer.ToString();

            XWPFRun xwpfRun = paragraphTitle.CreateRun();
            if (layer >= 4)
                xwpfRun.FontFamily = "宋体";
            xwpfRun.SetText(title);
        }

        private Widget GetTopicNote(Topic topic) 
        {
            return topic.FindWidget<NoteWidget>();
        }
        private PictureWidget[] GetTopicPictures(Topic topic) 
        {
            return topic.FindWidgets<PictureWidget>(e => File.Exists(e.ImageUrl) && (e.Data.Width > 128 || e.Data.Height > 128));
        }
        private void WriteToDocx(Topic topic, XWPFDocument DocxExample, XWPFDocument docx) 
        {
            XWPFStyles newStyles = docx.CreateStyles();
            newStyles.SetStyles(DocxExample.GetCTStyle());//复制模板格式

            //写入标题
            WriteTitleToDocx(topic.Text, docx, topic.GetDepth(topic));
          
            //在标题后写入内容
            Widget topicNote = GetTopicNote(topic);
            WriteNoteToDocx(topicNote, docx);

            //在内容后插入图片
           
            PictureWidget[] topicPictures = GetTopicPictures(topic);
            foreach(var pictureWidget in topicPictures)
            {
                WriteImgToDocx(pictureWidget, docx, imgNo++);
            }
            
            
            
            foreach (Topic subTopic in topic.Children)
            {
                WriteToDocx(subTopic, DocxExample, docx);
            }
            

        }
        public void Save(Topic topic , string fileName) //TODO
        {
            try
            {
                XWPFDocument DocxExample;//TODO
                using (var dotStream = new FileStream(Path.Combine(Application.StartupPath, "Resources", "DocxFileExp", "DocxExample.dotx"), FileMode.Open, FileAccess.Read))
                {

                    DocxExample = new XWPFDocument(dotStream);
                    dotStream.Close();
                }

                using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    imgNo = 0;
                    
                    XWPFDocument docx = new XWPFDocument();
                    WriteToDocx(topic, DocxExample, docx);
                    docx.Write(fileStream);
                    docx.Close();
                    fileStream.Close();
                }

                DocxExample.Close();

            }
            catch (Exception ex) 
            {
                MessageBox.Show("错误:"+ ex.ToString());
            }
        }
    }
}
