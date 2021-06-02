using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using C2.Model.Widgets;
using Aspose.Words;
using System.Collections.Generic;
using Aspose.Words.Drawing;

namespace C2.Model.MindMaps
{
    public class tmpDocxFile
    {
        private int imgNo;
        private List<string> unsucceedAttachment = new List<string>();
        private List<string> overweightAttachment = new List<string>();
        private void WriteNoteToDocx(Widget topicNote, Document docx)
        {
            if (topicNote != null)
            {
                string relRemark = topicNote.Remark;
                string tmpRemark = topicNote.Remark.Replace("</p>", @"$</p>");
                topicNote.Remark = tmpRemark;
                string[] paragraphs = topicNote.Text.Split('$');
                foreach (string paragraph in paragraphs)
                {
                    if (paragraph != string.Empty)
                        WriteParagraph(paragraph, docx);
                }
                topicNote.Remark = relRemark;
            }
        }
        private void WriteParagraph(string text, Document docx)
        {
            DocumentBuilder builder = new DocumentBuilder(docx);
            builder.MoveToDocumentEnd();
            builder.Font.Size = 10.5;
            //builder.Font.Bold = true;
            builder.Font.Name = "宋体";
            builder.Font.Name = "TimesNewRoma";
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
            builder.ParagraphFormat.FirstLineIndent = 21;
            builder.Font.Bold = false;
            builder.Writeln(text);
        }
        private Size RotateImageSize(int width, int height)
        {
            return width > height ? new Size(384, height * 384 / width) : new Size(width * 384 / height, 384);
        }
        
        private void WriteImgToDocx(PictureWidget pictureWidget, Document docx, int imgNo) 
        {
            string picturePath = pictureWidget.ImageUrl;
            if (!File.Exists(picturePath))
                return;
            string fileName = Path.GetFileName(picturePath);
            try
            {
                DocumentBuilder builder = new DocumentBuilder(docx);
                builder.MoveToDocumentEnd();

                int width = pictureWidget.Data.Width;
                int height = pictureWidget.Data.Height;
                Size size = RotateImageSize(width, height);
                builder.InsertImage(picturePath,size.Width,size.Height);
                builder.Writeln("");//把光标移动到下一行

                builder.Font.Bold = false;
                builder.Font.Name = "宋体";
                builder.Font.Name = "TimesNewRoma";
                builder.Font.Size = 9;
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                builder.Writeln("图" + (imgNo + 1) + ":" + fileName);

            }
            catch (Exception ex)
            {
                MessageBox.Show("错误:" + ex.ToString());
            }

        }

        private void WriteTitleToDocx(string title, Document docx, int layer, string serialNumber)
        {
            DocumentBuilder builder = new DocumentBuilder(docx);
            builder.MoveToDocumentEnd();
            switch (layer)
            {
                case 1:
                    builder.Font.Size = 21;
                    builder.Font.Bold = true;
                    builder.Font.Name = "黑体";
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                    builder.Writeln(title);
                    break;
                case 2:
                    builder.Font.Size = 16;
                    //builder.Font.Bold = true;
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
                    builder.ParagraphFormat.FirstLineIndent = 0;
                    builder.Font.Name = "黑体";
                    builder.Writeln(string.Format("{0} {1}", serialNumber, title));
                    break;
                case 3:
                    builder.Font.Size = 14;
                    builder.Font.Bold = true;
                    builder.Font.Name = "宋体";
                    builder.Font.Name = "TimesNewRoma";
                    builder.ParagraphFormat.FirstLineIndent = 0;
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
                    builder.Writeln(string.Format("{0} {1}", serialNumber, title));
                    break;
                case 4:
                    builder.Font.Size = 10.5;
                    builder.Font.Bold = true;
                    builder.Font.Name = "宋体";
                    builder.Font.Name = "TimesNewRoma";
                    builder.ParagraphFormat.FirstLineIndent = 0;
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
                    builder.Writeln(string.Format("{0} {1}", serialNumber, title));
                    break;
                default:
                    builder.Font.Size = 10.5;
                    builder.Font.Bold = false;
                    builder.Font.Name = "宋体";
                    builder.Font.Name = "TimesNewRoma";
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
                    builder.ParagraphFormat.FirstLineIndent = 21;
                    builder.Writeln(string.Format("{0} {1}", serialNumber, title));
                    break;
            }
           
        }
        private void WriteAttachmentToDocx(AttachmentWidget attachmentWidget, Document docx)
        {
            if (attachmentWidget == null)
                return;
            DocumentBuilder builder = new DocumentBuilder(docx);
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            builder.MoveToDocumentEnd();
            foreach (string path in attachmentWidget.AttachmentPaths) 
           {
                if (!File.Exists(path))
                {
                    unsucceedAttachment.Add(path);
                    continue;
                }
                var size = new FileInfo(path).Length;
                if (size > 524288000) 
                {
                    overweightAttachment.Add(path);
                    continue;
                }
                
                try
                {
                    byte[] bs = File.ReadAllBytes(path);
                    using (Stream stream = new MemoryStream(bs))
                    {
                        Shape shape = builder.InsertOleObject(path,false, true, null);
                        OlePackage olePackage = shape.OleFormat.OlePackage;
                        string name = Path.GetFileName(path);
                        olePackage.FileName = ".txt";
                        olePackage.DisplayName = "1";
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR");
                }
            }
            builder.Writeln("");
        }
        private Widget GetTopicNote(Topic topic)
        {
            return topic.FindWidget<NoteWidget>();
        }
        private AttachmentWidget GetTopicAttachment(Topic topic)
        {
            return topic.FindWidget<AttachmentWidget>();
        }
        private PictureWidget[] GetTopicPictures(Topic topic)
        {
            return topic.FindWidgets<PictureWidget>(e => File.Exists(e.ImageUrl) && (e.Data.Width > 128 || e.Data.Height > 128));
        }
        private void WriteToDocx(Topic topic, Document docx, string serialNumber = "1")
        {
            
            //写入标题
            WriteTitleToDocx(topic.Text, docx, topic.GetDepth(topic), serialNumber);

            //在标题后写入内容

            WriteNoteToDocx(GetTopicNote(topic), docx);

            //在内容后插入图片
            foreach (var pictureWidget in GetTopicPictures(topic))
            {
                WriteImgToDocx(pictureWidget, docx, imgNo++);
            }

            //在图片后插入附件
          
            WriteAttachmentToDocx(GetTopicAttachment(topic), docx);
           

            for (int i = 0; i < topic.Children.Count; i++)//迭代写入word
            {
                string nSerialNumber = (i + 1).ToString();
                if (!topic.IsRoot)
                    nSerialNumber = string.Format("{0}.{1}", serialNumber, (i + 1).ToString());
                WriteToDocx(topic.Children[i], docx, nSerialNumber);
            }

            
        }
        public void SaveAsDocx(Topic topic, string fileName)
        {
            try
            {
                imgNo = 0;//word中图片编号初始化
                Document docx = new Document();
                WriteToDocx(topic, docx);
                try
                {
                    docx.Save(fileName);
                }
                catch
                {
                        
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("错误:" + ex.Message);
                throw (ex);
            }

        }
    }


}
