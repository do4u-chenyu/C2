using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using C2.Model.Widgets;
using Aspose.Words;
using System.Collections.Generic;
using Aspose.Words.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace C2.Model.MindMaps
{
    public class APEDocxFile
    {
        private int imgNo;
        private List<string> unsucceedAttachment = new List<string>();
        private void WriteNoteToDocx(Widget topicNote, Document docx, StyleCollection styles)
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
                        WriteParagraph(paragraph, docx, styles);
                }
                topicNote.Remark = relRemark;
            }
        }
        private void WriteParagraph(string text, Document docx, StyleCollection styles)
        {
            DocumentBuilder builder = new DocumentBuilder(docx);
            builder.MoveToDocumentEnd();
            text = "      " + text;//段落开头两个空格
            builder.Writeln("");
            //builder.Font.Size = 10.5;
            ////builder.Font.Bold = true;
            //builder.Font.Name = "宋体";
            //builder.Font.Name = "Times New Roman";
            //builder.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
            //builder.ParagraphFormat.LineSpacing = 18;
            //builder.Font.Bold = false;

            builder.ParagraphFormat.Style = builder.ParagraphFormat.Style.Styles["IAO正文"];
            //builder.ParagraphFormat.FirstLineIndent = 21;
            builder.Write(text);
           
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
            string fileName = Path.GetFileNameWithoutExtension(picturePath);
            try
            {
                DocumentBuilder builder = new DocumentBuilder(docx);
                builder.MoveToDocumentEnd();
                builder.Writeln("");
                builder.ParagraphFormat.Style = builder.ParagraphFormat.Style.Styles["tips"];

                int width = pictureWidget.Data.Width;
                int height = pictureWidget.Data.Height;
                Size size = RotateImageSize(width, height);
                builder.InsertImage(picturePath, size.Width, size.Height);
                builder.Writeln("");//把光标移动到下一行

                //builder.Font.Bold = false;
                //builder.Font.Name = "宋体";
                //builder.Font.Name = "Times New Roman";
                //builder.Font.Size = 9;
                //builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;

                builder.Writeln("图" + (imgNo + 1) + ":" + fileName);
                builder.Writeln("");//把光标移动到下一行

            }
            catch (Exception ex)
            {
                MessageBox.Show("错误:" + ex.ToString());
            }

        }
        private bool IsLast(Topic topic) 
        { 
            if(topic.Children.Count == 0) 
            {
                return true;
            }
            else 
            { 
                return false;
            }
        }
        private void WriteTitleToDocx(string title, Document docx, Topic topic, string serialNumber, StyleCollection styles)
        {
            int layer = topic.GetDepth(topic);
            DocumentBuilder builder = new DocumentBuilder(docx);
            builder.MoveToDocumentEnd();
            builder.Writeln("");
            switch (layer)
            {
                case 1:
                    //builder.Font.Size = 20;
                    //builder.Font.Bold = true;
                    //builder.Font.Name = "黑体";
                    //builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                    //builder.ParagraphFormat.LineSpacing = 18;
                    builder.ParagraphFormat.Style.Styles.AddCopy(styles["Heading 1"]);//第一次创建光标时添加所有样式
                    builder.ParagraphFormat.Style.Styles.AddCopy(styles["Heading 2"]);
                    builder.ParagraphFormat.Style.Styles.AddCopy(styles["Heading 3"]);
                    builder.ParagraphFormat.Style.Styles.AddCopy(styles["Heading 4"]);
                    builder.ParagraphFormat.Style.Styles.AddCopy(styles["IAO正文"]);
                    builder.ParagraphFormat.Style.Styles.AddCopy(styles["tips"]);
                    builder.ParagraphFormat.Style = builder.ParagraphFormat.Style.Styles["Heading 1"];
                    builder.Write(title);
                    break;
                case 2:

                    //builder.Font.Size = 16;
                    //builder.Font.Bold = true;
                    //builder.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
                    //builder.ParagraphFormat.LineSpacing = 18;
                    //builder.ParagraphFormat.FirstLineIndent = 0;
                    //builder.Font.Name = "黑体";
                    builder.ParagraphFormat.Style = builder.ParagraphFormat.Style.Styles["Heading 2"];
                    builder.Write(string.Format("{0} {1}", serialNumber, title));
                    break;
                case 3:
                    //builder.Font.Size = 14;
                    //builder.Font.Bold = true;
                    //builder.Font.Name = "黑体";
                    //builder.Font.Name = "Times New Roman";
                    //builder.ParagraphFormat.FirstLineIndent = 0;
                    //builder.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
                    //builder.ParagraphFormat.LineSpacing = 18;

                    builder.ParagraphFormat.Style = builder.ParagraphFormat.Style.Styles["Heading 3"];
                    builder.Write(string.Format("{0} {1}", serialNumber, title));
                    break;
                case 4:
                    //builder.Font.Size = 10.5;
                    //builder.Font.Bold = false;
                    //builder.Font.Name = "黑体";
                    //builder.Font.Name = "Times New Roman";
                    //builder.ParagraphFormat.FirstLineIndent = 0;
                    //builder.ParagraphFormat.LineSpacing = 18;
                    //builder.ParagraphFormat.Alignment = ParagraphAlignment.Justify;

                    builder.ParagraphFormat.Style = builder.ParagraphFormat.Style.Styles["Heading 4"];
                    builder.Write(string.Format("{0} {1}", serialNumber, title));
                    break;
                default:
                    //builder.Font.Size = 10.5;
                    //builder.Font.Bold = false;
                    //builder.Font.Name = "宋体";
                    //builder.Font.Name = "Times New Roman";
                    //builder.ParagraphFormat.LineSpacing = 18;
                    //builder.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
                    //builder.ParagraphFormat.FirstLineIndent = 21;
                    builder.ParagraphFormat.Style.Styles.AddCopy(styles["IAO正文"]);
                    builder.ParagraphFormat.Style = builder.ParagraphFormat.Style.Styles["IAO正文"];
                    title = "      " + title;
                    builder.Write(title);
                    break;
            }

        }
        private void WriteDataSourceToDocx(DataSourceWidget dataSourceWidget, Document docx, int layer)
        {
            if (dataSourceWidget == null)
                return;
            DocumentBuilder builder = new DocumentBuilder(docx);
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            builder.MoveToDocumentEnd();
            builder.Writeln("");
            int i = 0;
            foreach (var dataSource in dataSourceWidget.DataItems)
            {
                if (dataSource.IsDatabase())
                    return;
                string path = dataSource.FilePath;
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
                        builder.ParagraphFormat.Style = builder.ParagraphFormat.Style.Styles["tips"];
                        Image image = GetImage(path);
                        Shape shape = builder.InsertOleObject(stream, "Package", true, image);
                        OlePackage olePackage = shape.OleFormat.OlePackage;
                        string name = Path.GetFileName(path);
                        olePackage.FileName = string.Format("dataSource{0}.{1}{2}", layer, i, Path.GetExtension(path));
                        i++;
                        builder.Write(" ");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR");
                }
            }
            builder.Writeln("");
        }

   
        private void WriteAttachmentToDocx(AttachmentWidget attachmentWidget, Document docx,int layer)
        {
            if (attachmentWidget == null)
                return;
            DocumentBuilder builder = new DocumentBuilder(docx);
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
            builder.MoveToDocumentEnd();
            builder.Writeln("");
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
                        builder.ParagraphFormat.Style = builder.ParagraphFormat.Style.Styles["tips"];
                        Image image = GetImage(path);
                        Shape shape = builder.InsertOleObject(stream, "Package", true, image);
                        OlePackage olePackage = shape.OleFormat.OlePackage;
                        string name = Path.GetFileName(path);
                        olePackage.FileName = string.Format("{0}.{1}{2}",layer,i, Path.GetExtension(path));
                        i++;
                        builder.Write(" ");
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
            StringBuilder ab = new StringBuilder();
            StringBuilder left = new StringBuilder();
            string firstName = "文件名超长";
            for (int i = 0; i < name.Length; i++)
            {
                char j = name[i];
                ushort s = j;
                if (s >= 0x4E00 && s <= 0x9FA5)
                {
                    ab.Append(name[i]);
                    l += 2;
                    chinese++;
                    if (l > 20)
                    {
                        firstName = ab.ToString();
                        left.Append(name[i]);
                    }
                }
                else 
                {
                    ab.Append(name[i]);
                    l += 1;
                    if (l > 20)
                    {
                        firstName = ab.ToString();
                        left.Append(name[i]);
                    }
                }
            }
            string leftname = left.ToString();
            int wordLength = (int)length + chinese;//一个字符4.5个像素
            double addLength = 45;
            if (wordLength >= 20) 
            {
                addLength = 92;
            }
            if (wordLength < 3)
            {
                firstName = name;
                addLength = 22;
            }
            if(3 <= wordLength && wordLength < 21)
            {
                firstName = name;
                addLength = wordLength * 4.6;
            }
             
            int Width = Img.Width;
            int Height = Img.Height;
            //获取图片水平和垂直的分辨率
            float dpiX = Img.HorizontalResolution;
            float dpiY = Img.VerticalResolution;
            //创建一个位图文件
            Bitmap BitmapResult = new Bitmap(Width + (int)addLength, Height + 40, PixelFormat.Format24bppRgb);
            //设置位图文件的水平和垂直分辨率  与Img一致
            BitmapResult.SetResolution(dpiX, dpiY);
            //在位图文件上填充一个矩形框
            Graphics Grp = Graphics.FromImage(BitmapResult);
            Rectangle Rec = new Rectangle(0, 0, Width + (int)addLength, Height + 40);
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

            
            System.Drawing.Font font = new System.Drawing.Font("宋体", 9);//插入第一行文件名
            SolidBrush sbrush = new SolidBrush(Color.Black);
            Grp.DrawString(firstName, font, sbrush, new PointF(0, 40));
            if(leftname != null) //插入第二行文件名
            {
                Grp.DrawString(leftname, font, sbrush, new PointF(0, 55));
            }
            
            Grp.Dispose();
            GC.Collect();
            return BitmapResult;
        }

        private Widget GetTopicNote(Topic topic)
        {
            return topic.FindWidget<NoteWidget>();
        }
        private DataSourceWidget GetDataSource(Topic topic) 
        {
            return topic.FindWidget<DataSourceWidget>();
        }
        private AttachmentWidget GetTopicAttachment(Topic topic)
        {
            return topic.FindWidget<AttachmentWidget>();
        }
        private PictureWidget[] GetTopicPictures(Topic topic)
        {
            return topic.FindWidgets<PictureWidget>(e => File.Exists(e.ImageUrl) && (e.Data.Width > 128 || e.Data.Height > 128));
        }
        private void WriteToDocx(Topic topic, Document docx, StyleCollection styles, string serialNumber = "1")
        {
            
            //写入标题
            WriteTitleToDocx(topic.Text, docx, topic, serialNumber, styles);

            //在标题后写入内容

            WriteNoteToDocx(GetTopicNote(topic), docx, styles);

            //在内容后插入图片
            foreach (var pictureWidget in GetTopicPictures(topic))
            {
                WriteImgToDocx(pictureWidget, docx, imgNo++);
            }

            //在图片后插入附件
            WriteAttachmentToDocx(GetTopicAttachment(topic), docx, topic.GetDepth(topic));

            //数据源同样以附件形式插入
            WriteDataSourceToDocx(GetDataSource(topic), docx, topic.GetDepth(topic));




            for (int i = 0; i < topic.Children.Count; i++)//迭代写入word
            {
                string nSerialNumber = (i + 1).ToString();
                if (!topic.IsRoot)
                    nSerialNumber = string.Format("{0}.{1}", serialNumber, (i + 1).ToString());
                WriteToDocx(topic.Children[i], docx, styles, nSerialNumber);
            }

        }
        public void SaveAsDocx(Topic topic, string fileName)
        {
            try
            {
                imgNo = 0;//word中图片编号初始化
                Document docx = new Document();
                
                string examplePath = Path.Combine(Application.StartupPath, "Resources", "Templates", "DocxExample.dotx");//获得模板样式
                if (!File.Exists(examplePath))
                {
                    MessageBox.Show("word模板文件不存在", "ERROR");
                    return;
                }
                Document doct = new Document(examplePath);
                StyleCollection styles = doct.Styles;

                WriteToDocx(topic, docx,styles);
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
                catch(Exception ex)
                {
                    MessageBox.Show("错误:" + ex.Message);
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
