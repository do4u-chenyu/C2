using Newtonsoft.Json.Linq;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QQSpiderPlugin
{
    static class Util
    {
        public static string GenQrToken(string qrSig)
        {
            int e = 0;
            for (int i = 0; i < qrSig.Length; i++)
                e += (e << 5) + Convert.ToInt32(qrSig[i]);
            int qrToken = (e & 2147483647);
            return qrToken.ToString();
        }
        public static string GenBkn(string skey)
        {
            int b = 5381;
            for (int i = 0; i < skey.Length; i++)
                b += (b << 5) + Convert.ToInt32(skey[i]);
            int bkn = (b & 2147483647);
            return bkn.ToString();
        }
        public static string GenRwWTS(string content)
        {
            string pattern = @"\[em\]e\d{4}\[/em\]|&nbsp;|<br>|[\r\n\t]";
            string result = Regex.Replace(content, pattern, "");
            return result.Replace("&amp;", "&").Trim();
        }
        /// <summary>
        /// 毫米级别的时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetTimeStamp()
        {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
        }

        public static void WriteToDisk<T>(string file, T obj)
        {
            using (Stream stream = File.Create(file))
            {
                try
                {
                    Console.Out.Write("Writing object to disk... ");
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, obj);
                    Console.Out.WriteLine("Done.");
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine("Problem writing object to disk: " + e.GetType());
                }
            }
        }
        public static Session ReadFromDisk(string file)
        {
            try
            {
                using (Stream stream = File.Open(file, FileMode.Open))
                {
                    Console.Out.Write("Reading object from disk... ");
                    BinaryFormatter formatter = new BinaryFormatter();
                    Console.Out.WriteLine("Done.");
                    return (Session)formatter.Deserialize(stream);
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Problem reading cookies from disk: " + e.GetType());
                return new Session();
            }
        }
        public static string TryGetSysTempDir()
        {
            string tempDir;
            try
            {
                tempDir = Path.GetTempPath();
            }
            catch (System.Security.SecurityException)
            {
                tempDir = string.Empty;
            }
            return tempDir;
        }
        public static string TryGetStringFromJToken(JToken token, string value)
        {
            string result = String.Empty;
            try
            {
                result = (string)token[value];
            }
            catch
            {
                
            }
            return result;
        }
        public static int TryGetIntFromJToken(JToken token, string value)
        {
            int result = 0;
            try
            {
                result = (int)token[value];
            }
            catch
            {

            }
            return result;
        }

        public static bool SaveToExcel(string filePath, DataGridView dataGridView)
        {
            short rowHeight = 800;
            bool result = true;

            FileStream fs = null;
            HSSFWorkbook workbook = null;
            HSSFSheet sheet = null;

            int rowCount = dataGridView.RowCount;
            int colCount = dataGridView.ColumnCount;


            try
            {
                fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                workbook = new HSSFWorkbook();
                sheet = (HSSFSheet)workbook.CreateSheet("Sheet1");
                IRow row = sheet.CreateRow(0);
                for (int j = 0; j < colCount; j++)
                {
                    if (dataGridView.Columns[j].Visible && dataGridView.Rows[0].Cells[j].Value != null)
                    {
                        ICell cell = row.CreateCell(j);
                        cell.SetCellValue(dataGridView.Columns[j].HeaderText.ToString());                  
                    }
                }
            }
            catch(Exception)
            {
                result = false;
                return result;
            }

            for (int i = 0; i < rowCount; i++)
            {
                if (i >= 65536)
                {
                    result = false;
                    break;
                }
                IRow row = sheet.CreateRow(1 + i);
                row.Height = rowHeight;
                for (int j = 0; j < colCount; j++)
                {
                    if (dataGridView.Columns[j].Visible && dataGridView.Rows[i].Cells[j].Value != null)
                    {
                        ICell cell = row.CreateCell(j);
                        // 头像
                        if (j == 0)
                        {
                            Image image;
                            try
                            {
                                image = (Image)dataGridView.Rows[i].Cells[j].Value;
                                byte[] bytes;
                                using (var ms = new MemoryStream())
                                {
                                    image.Save(ms, ImageFormat.Png);
                                    bytes = ms.ToArray();
                                }
                                int pictureIndex = workbook.AddPicture(bytes, PictureType.PNG);
                                ICreationHelper helper = workbook.GetCreationHelper();
                                IDrawing drawing = sheet.CreateDrawingPatriarch();
                                IClientAnchor anchor = helper.CreateClientAnchor();
                                anchor.Col1 = 0;//0 index based column
                                anchor.Row1 = i + 1;//0 index based row
                                IPicture picture = drawing.CreatePicture(anchor, pictureIndex);
                                picture.Resize();
                            }
                            catch(Exception)
                            {
                            }
                        }
                        else
                            cell.SetCellValue(dataGridView.Rows[i].Cells[j].Value.ToString());   // TODO Insert image           
                    }
                }
            }
            try
            {
                workbook.Write(fs);
            }
            catch
            {
                result = false;
                return result;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                workbook = null;
            }
            return result;
        }
        public static bool KeyWordSaveToExcel(string filePath, Dictionary<string, List<string>> resultDic)
        {
            short rowHeight = 800;
            bool result = true;
            List<string> headList = new List<string> { "群头像", "群ID", "群名称", "群人数", "群人数上限", "群主QQ", "群地址", "群分类", "群标签", "群简介" };
            FileStream fs = null;
            HSSFWorkbook workbook = null;
            HSSFSheet sheet = null;
            
            try
            {
                fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                workbook = new HSSFWorkbook();
                foreach (string key in resultDic.Keys) 
                {
                    sheet = (HSSFSheet)workbook.CreateSheet(key);
                    IRow row = sheet.CreateRow(0);
                    List<string> resultList = resultDic[key];
                    for (int j = 0; j < headList.Count; j++)
                    {
                        ICell cell = row.CreateCell(j);
                        cell.SetCellValue(headList[j]); 
                    }
                    for (int i = 0; i < resultList.Count; i++)
                    {
                        if (i >= 65536)
                        {
                            result = false;
                            break;
                        }
                        IRow rows = sheet.CreateRow(1 + i);
                        rows.Height = rowHeight;
                        string[] resultarray = resultList[i].Split('\t');
                        for (int j = 0; j < resultarray.Length; j++)
                        {
                            // 头像
                            if (j == resultarray.Length - 1)
                            {
                                ICell pngcell = rows.CreateCell(0);
                                Image image;
                                try
                                {
                                    image = (Image)GetImage(resultarray[j]);
                                    byte[] bytes;
                                    using (var ms = new MemoryStream())
                                    {
                                        image.Save(ms, ImageFormat.Png);
                                        bytes = ms.ToArray();
                                    }
                                    int pictureIndex = workbook.AddPicture(bytes, PictureType.PNG);
                                    ICreationHelper helper = workbook.GetCreationHelper();
                                    IDrawing drawing = sheet.CreateDrawingPatriarch();
                                    IClientAnchor anchor = helper.CreateClientAnchor();
                                    anchor.Col1 = 0;//0 index based column
                                    anchor.Row1 = i + 1;//0 index based row
                                    IPicture picture = drawing.CreatePicture(anchor, pictureIndex);
                                    picture.Resize();
                                }
                                catch (Exception)
                                {
                                }
                            }
                            else
                            {
                                ICell pngcell = rows.CreateCell(j+1);
                                pngcell.SetCellValue(resultarray[j]);   // TODO Insert image 
                            }
                                          
                        }
                    }
                }
                
            }
            catch (Exception)
            {
                result = false;
                return result;
            }
            
            try
            {
                workbook.Write(fs);
            }
            catch
            {
                result = false;
                return result;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                workbook = null;
            }
            return result;
        }

        public static Image ResizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        public static Image GetImage(string url)
        {
            byte[] data = new byte[0];
            int imgSize = 50;
            Image img;
            try
            {
                using (WebClient client = new WebClient())
                {
                    data = client.DownloadData(url);
                    using (MemoryStream mem = new MemoryStream(data))
                        img = Image.FromStream(mem);
                }
            }
            catch (Exception)
            {
                img = (Image)Properties.Resources.not_found;
            }
            return Util.ResizeImage(img, new Size(imgSize, imgSize));
        }
    }
    static class DgvUtil
    {
        public static void CleanDgv(DataGridView dgv)
        {
            dgv.DataSource = null; // System.InvalidOperationException:“操作无效，原因是它导致对 SetCurrentCellAddressCore 函数的可重入调用。”
            dgv.Rows.Clear();
        }
        public static void ResetColumnsWidth(DataGridView dgv, int minWidth = 50, bool autoSize = true)
        {
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                dgv.Columns[i].MinimumWidth = minWidth;
            }
            if (autoSize)
            {
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
        }
        public static void DisableOrder(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
    }
}
