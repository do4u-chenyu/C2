using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using C2.Model.Widgets;
using NPOI.OpenXmlFormats.Dml;
using NPOI.OpenXmlFormats.Dml.WordProcessing;
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
                noteText.IndentationFirstLine = 400;
                XWPFRun xwpfRun = noteText.CreateRun();
                xwpfRun.FontFamily = "宋体";
                xwpfRun.SetText(topicNote.Text);
            }
        }
        private Size RotateImageSize(int width, int height)
        {
            return width > height ? new Size(512, height * 512 / width) : new Size(width * 512 / height, 512);
        }
        private void CreatePicture(string filePath, XWPFDocument docx, Size size)
        {
            using (FileStream fsImg = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                var picID = docx.AddPictureData(fsImg, (int)PictureType.JPEG);
                int EMU = 9525;
                int width = size.Width * EMU;
                int height = size.Height * EMU;
                //长宽单位为emu，在1080分辨率下换算单位为1像素等于9525emu
                string picXml = "<pic:pic xmlns:pic=\"http://schemas.openxmlformats.org/drawingml/2006/picture\">"
                                + "<pic:nvPicPr>"
                                     + $"<pic:cNvPr id=\"{imgNo + 1}\" name=\"图片 {imgNo + 1}\"/>"
                                     + "<pic:cNvPicPr/>"
                                 + "</pic:nvPicPr>"
                                 + "<pic:blipFill>"
                                     + $"<a:blip r:embed=\"{picID}\" xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\">"
                                     + "</a:blip>"
                                     + "<a:stretch>"
                                         + "<a:fillRect/>"
                                     + "</a:stretch>"
                                 + "</pic:blipFill>"
                                 + "<pic:spPr>"
                                     + "<a:xfrm>"
                                         + "<a:off x=\"0\" y=\"0\"/>"
                                         + $"<a:ext cx=\"{width}\" cy=\"{height}\"/>"
                                     + "</a:xfrm>"
                                     + "<a:prstGeom prst=\"rect\">"
                                         + "<a:avLst/>"
                                     + "</a:prstGeom>"
                                 + "</pic:spPr>"
                             + "</pic:pic>";
                XWPFParagraph newPara = docx.CreateParagraph();
                newPara.Alignment = ParagraphAlignment.CENTER;
                XWPFRun imageCellRunn = newPara.CreateRun();
                CT_Inline inline = imageCellRunn.GetCTR().AddNewDrawing().AddNewInline();

                inline.graphic = new CT_GraphicalObject();
                inline.graphic.graphicData = new CT_GraphicalObjectData();
                inline.graphic.graphicData.uri = "http://schemas.openxmlformats.org/drawingml/2006/picture";
                try
                {
                    inline.graphic.graphicData.AddPicElement(picXml);
                }
                catch(XmlException xe)
                {
                    MessageBox.Show("错误:" + xe.ToString());
                    fsImg.Close();
                }

                NPOI.OpenXmlFormats.Dml.WordProcessing.CT_PositiveSize2D extent = inline.AddNewExtent();
                extent.cx = width;
                extent.cy = height;

                NPOI.OpenXmlFormats.Dml.WordProcessing.CT_NonVisualDrawingProps docPr = inline.AddNewDocPr();
                docPr.id = (uint)imgNo + 1;
                docPr.name = "图片 " + (imgNo + 1);
                fsImg.Close();
            }
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
                Size size = RotateImageSize(width, height);
                CreatePicture(picturePath, docx, size);

                XWPFParagraph paragraphIMG = docx.CreateParagraph();
                paragraphIMG.Alignment = ParagraphAlignment.CENTER;
                XWPFRun xwpfRun = paragraphIMG.CreateRun();
                xwpfRun.FontFamily = "宋体";
                xwpfRun.SetText("图" + (imgNo + 1) +":" + fileName);
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("错误:" + ex.ToString());
            }

        }

        private void WriteTitleToDocx(string title, XWPFDocument docx, int layer, string serialNumber)
        {
            XWPFParagraph paragraphTitle = docx.CreateParagraph();
            
            paragraphTitle.Style = layer > 4 ? "a0" : layer.ToString();

            XWPFRun xwpfRun = paragraphTitle.CreateRun();
           
            if(layer == 1) 
            {
                xwpfRun.SetText(title);
            }
            if(layer <= 4 && layer > 1)
            {
                xwpfRun.SetText(string.Format("{0} {1}", serialNumber, title));
            }
            if (layer > 4)
            {
                paragraphTitle.IndentationFirstLine = 400;
                xwpfRun.FontFamily = "宋体";
                xwpfRun.SetText(title);
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
        private void WriteToDocx(Topic topic, XWPFDocument DocxExample, XWPFDocument docx, string serialNumber = "1") 
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

            
            for (int i = 0; i < topic.Children.Count; i++) 
            {
                string nSerialNumber = (i + 1).ToString();
                if (!topic.IsRoot)
                    nSerialNumber = string.Format("{0}.{1}", serialNumber, (i + 1).ToString());
                WriteToDocx(topic.Children[i], DocxExample, docx, nSerialNumber);
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
                    imgNo = 0;//word中图片编号初始化
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
