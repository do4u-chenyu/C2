using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using C2.Model.Widgets;
using Aspose.Words;
using System.Collections.Generic;
using Aspose.Words.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics.SymbolStore;
using System.Text;

namespace C2.Model.MindMaps
{
    public class tmpDocxFile
    {
        private int imgNo;
        private List<string> unsucceedAttachment = new List<string>();
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
            builder.ParagraphFormat.LineSpacing = 18;
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
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;

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
                    builder.Font.Size = 20;
                    builder.Font.Bold = true;
                    builder.Font.Name = "黑体";
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                    builder.ParagraphFormat.LineSpacing = 18;
                    builder.Writeln(title);
                    break;
                case 2:
                    builder.Font.Size = 16;
                    builder.Font.Bold = true;
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
                    builder.ParagraphFormat.LineSpacing = 18;
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
                    builder.ParagraphFormat.LineSpacing = 18;
                    builder.Writeln(string.Format("{0} {1}", serialNumber, title));
                    break;
                case 4:
                    builder.Font.Size = 10.5;
                    builder.Font.Bold = true;
                    builder.Font.Name = "宋体";
                    builder.Font.Name = "TimesNewRoma";
                    builder.ParagraphFormat.FirstLineIndent = 0;
                    builder.ParagraphFormat.LineSpacing = 18;
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
                    builder.Writeln(string.Format("{0} {1}", serialNumber, title));
                    break;
                default:
                    builder.Font.Size = 10.5;
                    builder.Font.Bold = false;
                    builder.Font.Name = "宋体";
                    builder.Font.Name = "TimesNewRoma";
                    builder.ParagraphFormat.LineSpacing = 18;
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
                    builder.ParagraphFormat.FirstLineIndent = 21;
                    builder.Writeln(string.Format("{0} {1}", serialNumber, title));
                    break;
            }
           
        }
        private void WriteAttachmentToDocx(AttachmentWidget attachmentWidget, Document docx,int layer)
        {
            if (attachmentWidget == null)
                return;
            DocumentBuilder builder = new DocumentBuilder(docx);
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            builder.MoveToDocumentEnd();
            int i= 0;
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
                    unsucceedAttachment.Add(path);
                    continue;
                }
                try
                {
                    byte[] bs = File.ReadAllBytes(path);
                    using (Stream stream = new MemoryStream(bs))
                    {
                        Image image = GetImage(path);
                        Shape shape = builder.InsertOleObject(stream, "Package", true, image);
                        OlePackage olePackage = shape.OleFormat.OlePackage;
                        string name = Path.GetFileName(path);
                        olePackage.FileName = string.Format("{0}.{1}{2}",layer,i, Path.GetExtension(path));
                        i++;
                        olePackage.DisplayName = "1.txt";
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR");
                }
            }
            builder.Writeln("");
        }
        private Image GetImage(string path) 
        {
            string filename = Path.GetExtension(path);
            string name = Path.GetFileName(path);
            Image tmpImg;
            try
            {
                string fullFileName = Path.Combine(Path.GetTempPath(), "tmp" + filename);
                if (File.Exists(fullFileName))
                    tmpImg = Icon.ExtractAssociatedIcon(fullFileName).ToBitmap();
                else
                { 
                    File.Create(fullFileName).Close();
                    tmpImg = Icon.ExtractAssociatedIcon(fullFileName).ToBitmap();
                    File.Delete(fullFileName);
                }
             }
            catch 
            {
                tmpImg = Properties.Resources.copyFilePath;
            }
            Image img = AddNameToImg(tmpImg, name);
            return img;
        }

        public Bitmap AddNameToImg(Image Img,string name) 
        {
            var length = (UInt32)name.Length;
            int chinese = 0;
            int l = 0;
            StringBuilder sb = new StringBuilder();
            string abbreviate = "文件名超长";
            for (int i = 0; i < name.Length; i++)
            {
                char j = name[i];
                ushort s = j;
                if (s >= 0x4E00 && s <= 0x9FA5)
                {
                    sb.Append(name[i]);
                    l += 2;
                    chinese++;
                    if (l == 18 || l == 17)
                        abbreviate = sb.ToString();
                }
                else 
                {
                    sb.Append(name[i]);
                    l += 1;
                    if (l == 18 || l == 17)
                        abbreviate = sb.ToString();
                }
            }
            int wordLength = (int)length + chinese;//一个字符4.5个像素
            double addLength = 45;
            if (wordLength >= 20) 
            {
                addLength = 90;
                name = abbreviate + "...";
            }                
            if(wordLength < 3)
                addLength = 22;
            if(3 <= wordLength && wordLength < 21)
                addLength = wordLength * 4.3;
            int Width = Img.Width;
            int Height = Img.Height;
            //获取图片水平和垂直的分辨率
            float dpiX = Img.HorizontalResolution;
            float dpiY = Img.VerticalResolution;
            //创建一个位图文件
            Bitmap BitmapResult = new Bitmap(Width + (int)addLength, Height + 30, PixelFormat.Format24bppRgb);
            //设置位图文件的水平和垂直分辨率  与Img一致
            BitmapResult.SetResolution(dpiX, dpiY);
            //在位图文件上填充一个矩形框
            Graphics Grp = Graphics.FromImage(BitmapResult);
            Rectangle Rec = new Rectangle(0, 0, Width + (int)addLength, Height + 30);
            //定义一个白色的画刷
            SolidBrush mySolidBrush = new SolidBrush(Color.White);
            //Grp.Clear(Color.White);
            //将矩形框填充为白色
            Grp.FillRectangle(mySolidBrush, Rec);
            //Grp.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            //Grp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //Grp.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //向矩形框内填充Img
            Grp.DrawImage(Img, (int)addLength /2, 5, Rec, GraphicsUnit.Pixel);
            //返回位图文件

            
            System.Drawing.Font font = new System.Drawing.Font("宋体", 9);
            SolidBrush sbrush = new SolidBrush(Color.Black);
            Grp.DrawString(name, font, sbrush, new PointF(0, 40));
            Grp.Dispose();
            GC.Collect();
            return BitmapResult;
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
          
            WriteAttachmentToDocx(GetTopicAttachment(topic), docx, topic.GetDepth(topic));
           

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
                    if (unsucceedAttachment.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (string path in unsucceedAttachment)
                        {
                            sb.Append(path);
                            sb.Append(";");
                        }
                        MessageBox.Show("以下附件不存在或大小超过500M:"+sb.ToString(), "提示");
                    }
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
