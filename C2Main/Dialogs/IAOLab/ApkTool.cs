﻿using C2.Controls;
using C2.Core;
using C2.IAOLab.ApkToolStart;
using C2.Utils;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace C2.Dialogs.IAOLab
{
    public partial class ApkTool : BaseDialog
    {
        private  List<List<string>> apkInfoListForEXL = new List<List<string>>();
        public delegate void UpdateLog(string log);//声明一个更新主线程日志的委托
        public UpdateLog UpdateLogDelegate;
        public Image image;
        public ApkTool()
        {
            InitializeComponent();
        }

        private bool IsReady()
        {
            // 是否配置完毕
            bool isEmpty = string.IsNullOrEmpty(this.inputPathTextBox.Text) ||
                           string.IsNullOrEmpty(this.jdkPathTextBox.Text);
            if (isEmpty)
            {
                MessageBox.Show("APK存放路径或JDK存放路径不能为空");
                return false;
            }
            if (!Directory.Exists(this.inputPathTextBox.Text)
                &&!File.Exists(this.inputPathTextBox.Text))
            {
                MessageBox.Show("APK存放路径不存在"); // 不存在
                return false;
            }
            if (!File.Exists(this.jdkPathTextBox.Text))
            {
                MessageBox.Show("JDK存放路径不存在");
                return false;
            }
            return true;
        }
        private void Analyse_Click(object sender, EventArgs e)
        {
            new Log.Log().LogManualButton("实验楼" + "-" + "APK小眼睛", "运行");
            if (!IsReady())
                return;
            this.Cursor = Cursors.WaitCursor;
            textBox1.Text = "正在清除历史数据";
            this.dataGridView1.Rows.Clear();
            apkInfoListForEXL = new List<List<string>>();
            if(image != null)
                image.Dispose();
            string tmpPath = Path.Combine(Path.GetTempPath(), "ApkTool");
            FileUtil.DeleteDirectory(tmpPath);
            FileUtil.CreateDirectory(tmpPath);
            int j = 0;
            DirectoryInfo dir = new DirectoryInfo(inputPathTextBox.Text);
            //检索表示当前目录的文件和子目录
            FileSystemInfo[] fsInfos = dir.GetFileSystemInfos();
            if(fsInfos.Length != 0)
            {
                //遍历检索的文件和子目录
                foreach (FileSystemInfo fsInfo in fsInfos)
                {
                    if (fsInfo.Name.EndsWith(".apk"))
                    {
                        j++;
                        textBox1.Text = string.Format("正在解析第{0}个apk文件", j);
                        List<string> apkInfoList = ApkToolStart.GetInstance().ExtractApk(fsInfo.FullName, fsInfo.Name, jdkPathTextBox.Text);
                        apkInfoListForEXL.Add(apkInfoList);//apk数据汇总，为写入excel做准备
                        if (apkInfoList.Count > 2) // (2, this.dataGridView1.Columns.Count]
                        {
                            // 将结果展示在窗体
                            int index = this.dataGridView1.Rows.Add();
                            try
                            {
                                this.dataGridView1.Rows[index].Cells[0].Value = GetImage(apkInfoList[0]);
                                
                            }
                            catch 
                            {
                                this.dataGridView1.Rows[index].Cells[0].Value = GetImage(Path.Combine(Application.StartupPath, @"C2Main\Resources\Images", "close.png"));
                               
                            }
                            for (int i = 1; i < this.dataGridView1.Columns.Count; i++)
                            {
                                this.dataGridView1.Rows[index].Cells[i].Value = apkInfoList[i];
                            }
                        }
                    }
                }
                if(j == 0)
                    MessageBox.Show("该目录下不存在.apk文件");
                textBox1.Text = string.Format("解析完成");
            }
            else
            {
                MessageBox.Show("apk目录为空");
            }
            this.Cursor = Cursors.Arrow;
        }
        public Image GetImage(string path)
        {
            try
            {
                image = Image.FromFile(path);
                return image;
            }
            catch
            {
                return null;
            }
        }

        private void InputPathPictureBox_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            if (fd.ShowDialog() == DialogResult.OK)
                inputPathTextBox.Text = fd.SelectedPath;
        }

        private void JdkPathPictureBox_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
            OpenFileDialog1.Filter = "java.exe | *.exe";
            string path = null;
            foreach (DictionaryEntry DEntry in Environment.GetEnvironmentVariables())
            {
                if (DEntry.Key.ToString() == "JAVA_HOME") 
                {
                    path = Path.Combine(DEntry.Value.ToString(), "bin");
                    break;
                }
            }
            if (path == string.Empty)
            {
                string firstPath = @"C:\Program Files\Java";
                if (Directory.Exists(firstPath))
                {
                    DirectoryInfo dir = new DirectoryInfo(firstPath);
                    FileSystemInfo[] fsInfos = dir.GetFileSystemInfos();
                    foreach (FileSystemInfo fsInfo in fsInfos)
                    {
                        if (fsInfo.Name.Contains("jdk") && Directory.Exists(Path.Combine(fsInfo.FullName, "bin")))
                        {
                            path = Path.Combine(fsInfo.FullName, "bin");
                            break;
                        }
                    }
                }
                else
                {
                    path = @"C:\Program Files";
                }

            }
            
            OpenFileDialog1.InitialDirectory = path;
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
                jdkPathTextBox.Text = OpenFileDialog1.FileName;
        }
        

        private void ExportToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.IsEmpty())
            {
                MessageBox.Show("当前无数据可导出!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                ExportDataToExcel();
            }
        }

        //从保存的apkInfoListForEXL中直接读取数据写入Excel
        public void ExportDataToExcel()
        {

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "请选择要导出的位置";
            saveDialog.Filter = "Excel文件|*.xls";
            saveDialog.ShowDialog();
            string path = saveDialog.FileName;
            if (path.IndexOf(":") < 0) return;
            IWorkbook workBook = null;  //保存的数据
            IRow row = null;
            ISheet sheet = null;  //生成表格
            ICell cell = null;
            try
            {
                string[] columnName = { "图标", "文件名", "Apk名", "包名", "主函数名", "大小" };
                workBook = new HSSFWorkbook();
                sheet = workBook.CreateSheet("Sheet0");//创建一个名称为Sheet0的表  
                int rowCount = this.apkInfoListForEXL.Count;//行数  
                int columnCount = columnName.Length;//列数  
                sheet.SetColumnWidth(0, 15 * 256);
                sheet.SetColumnWidth(1, 10 * 256);
                sheet.SetColumnWidth(2, 10 * 256);
                sheet.SetColumnWidth(3, 30 * 256);
                sheet.SetColumnWidth(4, 40 * 256);
                sheet.SetColumnWidth(5, 5 * 256);
                
                row = sheet.CreateRow(0);//excel第一行设为列头，在第一行中写入数据（所有列的第一行）
    
                    
                for (int c = 0; c < columnCount; c++)
                {
                    cell = row.CreateCell(c, CellType.Numeric);  //创建一个单元格，保留文本格式
                    cell.SetCellValue(columnName[c]);  //将数据写入到单元格中
                }

                //设置每行每列的单元格,  
                for (int i = 0; i < rowCount; i++)  //写完所有的行和列
                {
                    row = sheet.CreateRow(i+1); //这里是新开启一行，从第二行开始写，第一行上面已经写完
                    row.Height = 80 * 20;
                    try
                    {
                        cell = row.CreateCell(0);
                        byte[] bytes = File.ReadAllBytes(this.apkInfoListForEXL[i][0]);
                        int pictureIdx = workBook.AddPicture(bytes, PictureType.JPEG);
                        HSSFPatriarch patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();//前四个参数(dx1,dy1,dx2,dy2)为图片在单元格的边距                            //col1,col2表示图片插在col1和col2之间的单元格，索引从0开始                            //row1,row2表示图片插在第row1和row2之间的单元格，索引从1开始　　　　　　　　　　　　　　　　// 参数的解析: HSSFClientAnchor（int dx1,int dy1,int dx2,int dy2,int col1,int row1,int col2,int row2)            　　　　　　　　　//dx1:图片左边相对excel格的位置(x偏移) 范围值为:0~1023;即输100 偏移的位置大概是相对于整个单元格的宽度的100除以1023大概是10分之一            　　　　　　　　  //dy1:图片上方相对excel格的位置(y偏移) 范围值为:0~256 原理同上。            　　　　　　　　  //dx2:图片右边相对excel格的位置(x偏移) 范围值为:0~1023; 原理同上。            　　　　　　　　  //dy2:图片下方相对excel格的位置(y偏移) 范围值为:0~256 原理同上。            　　　　　　　　  //col1和row1 :图片左上角的位置，以excel单元格为参考,比喻这两个值为(1,1)，那么图片左上角的位置就是excel表(1,1)单元格的右下角的点(A,1)右下角的点。            　　　　　　　　  //col2和row2:图片右下角的位置，以excel单元格为参考,比喻这两个值为(2,2)，那么图片右下角的位置就是excel表(2,2)单元格的右下角的点(B,2)右下角的点。
                        HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 0, 0, 0, i + 1, 1, i + 2);
                        //把图片插到相应的位置
                        HSSFPicture pict = (HSSFPicture)patriarch.CreatePicture(anchor, pictureIdx);
                    }
                    catch
                    {
                        cell = row.CreateCell(0);
                        cell.SetCellValue("图片不存在");
                    }
                    for (int j = 1; j < columnCount; j++)  // 
                    {
                        cell = row.CreateCell(j);
                        cell.SetCellValue(this.apkInfoListForEXL[i][j]); //创建列的单元格的内容                             
                    }
                }
                using (FileStream fs = File.OpenWrite(path))
                {
                    workBook.Write(fs);//向打开的这个xls文件中写入数据  
                }
                workBook.Close();
                sheet = null;
                workBook = null;
                row = null;
                MessageBox.Show("导出成功", "提示",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
             catch (Exception ex)
            {
                    MessageBox.Show("导出失败:" + ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
        }


        private void DataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dataGridView1.ContextMenuStrip = contextMenuStrip1;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private void ApkTool_FormClosed(object sender, EventArgs e)
        {
            Clear();
        }
        private void Clear()
        {
            textBox1.Text = "正在清除临时文件";
            this.dataGridView1.Rows.Clear();
            if (image != null)
                image.Dispose();
            string tmpPath = Path.Combine(Path.GetTempPath(), "ApkTool");
            //FileUtil.DeleteDirectory(tmpPath);
            //FileUtil.CreateDirectory(tmpPath);
            this.textBox1.Clear();
            this.inputPathTextBox.Clear();
        }
    }
}
