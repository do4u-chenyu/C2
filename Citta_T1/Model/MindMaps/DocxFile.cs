using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using C2.Model.Widgets;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;

namespace C2.Model.MindMaps
{
    public class DocxFile
    {
        private int imgNo;
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
        private void WriteImgToDocx(PictureWidget pictureWidget, XWPFDocument docx, int imgNo) //TODO
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

        private void WriteTitleToDocx(string title, XWPFDocument docx, int layer, string serialNumber)
        {
            XWPFParagraph paragraphTitle = docx.CreateParagraph();
            
            paragraphTitle.Style = layer > 4 ? "a0" : layer.ToString();

            XWPFRun xwpfRun = paragraphTitle.CreateRun();
            if (layer > 4 )
            {
                xwpfRun.FontFamily = "宋体";
                xwpfRun.SetText(title);
            }
            if(layer == 1) 
            {
                xwpfRun.SetText(title);
            }
            else
            {
                xwpfRun.SetText(string.Format("{0} {1}", serialNumber, title));
            }
        }

        private Widget GetTopicNote(Topic topic)
        {
            return topic.FindWidget<NoteWidget>();
        }
        private PictureWidget[] GetTopicPictures(Topic topic)
        {
            return topic.FindWidgets<PictureWidget>(e => File.Exists(e.ImageUrl) && (e.Data.Width > 128 || e.Data.Height > 128));
        }
        private void WriteToDocx(Topic topic, XWPFDocument DocxExample, XWPFDocument docx,string serialNumber = "1") 
        {
            XWPFStyles newStyles = docx.CreateStyles();
            newStyles.SetStyles(DocxExample.GetCTStyle());//复制模板格式

            //写入标题
            WriteTitleToDocx(topic.Text, docx, topic.GetDepth(topic), serialNumber);

            //在标题后写入内容
            
            WriteNoteToDocx(GetTopicNote(topic), docx);

            //在内容后插入图片
            foreach (var pictureWidget in GetTopicPictures(topic))
            {
                WriteImgToDocx(pictureWidget, docx, imgNo++);
            }

            string nSerialNumber;
            for (int j =1; j < topic.Children.Count+1; j++) 
            {
                if (topic.IsRoot)
                    nSerialNumber = (j).ToString();
                else
                    nSerialNumber = string.Format("{0}.{1}",serialNumber, j.ToString());
                WriteToDocx(topic.Children[j - 1], DocxExample, docx, nSerialNumber);
            }


        }
        public void Save(Topic topic, string fileName) 
        {
            try
            {
                XWPFDocument DocxExample;
                using (var dotStream = new FileStream(Path.Combine(Application.StartupPath, "Resources", "Templates", "DocxExample.dotx"), FileMode.Open, FileAccess.Read))
                {

                    DocxExample = new XWPFDocument(dotStream);
                    dotStream.Close();
                }

                using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    imgNo = 0;

                    XWPFDocument docx = new XWPFDocument();
                    WriteToDocx(topic, DocxExample, docx);
                    try
                    {
                        docx.Write(fileStream);
                        docx.Close();
                        fileStream.Close();
                    }
                    catch 
                    {
                        docx.Close();
                        fileStream.Close();
                    }
                }

                DocxExample.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("错误:" + ex.ToString());
            }
        
    }
}

    
}
