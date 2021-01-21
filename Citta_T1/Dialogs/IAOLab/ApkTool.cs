using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using C2.Controls;
using C2.Core;
using C2.IAOLab.ApkToolStart;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
namespace C2.Dialogs.IAOLab
{
    public partial class ApkTool : BaseDialog
    {
        private  List<List<string>> apkInfoList = new List<List<string>>();
        public delegate void UpdateLog(string log);//声明一个更新主线程日志的委托
        public UpdateLog UpdateLogDelegate;
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
                MessageBox.Show("请完善apk存放路径、jdk存放路径。");
                return false;
            }
            if (!Directory.Exists(this.inputPathTextBox.Text)
                &&!File.Exists(this.inputPathTextBox.Text))
            {
                MessageBox.Show("不合法的apk存放路径"); // 不存在
                return false;
            }
            if (!Directory.Exists(this.jdkPathTextBox.Text))
            {
                MessageBox.Show("不合法的jdk存放路径");
                return false;
            }
            return true;
        }
        private void Analyse_Click(object sender, EventArgs e)
        {
            
            if (!IsReady())
                return;

            apkInfoList = ApkToolStart.GetInstance().ExtractApk(inputPathTextBox.Text, jdkPathTextBox.Text);
            foreach (List<string> apkInfo in apkInfoList)
            {
 
                // 将结果展示在窗体
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells[0].Value = GetImage(apkInfo[0]);
                for (int i = 1; i < this.dataGridView1.Columns.Count; i++)
                {
                    this.dataGridView1.Rows[index].Cells[i].Value = apkInfo[i];
                }
                
            }
            //string tmpPath = Path.Combine(Path.GetTempPath(), "ApkTool");
            //DirectoryInfo di1 = new DirectoryInfo(tmpPath);
            //di1.Delete(true);
        }
        public Image GetImage(string path)
        {
            try 
            {
                Image image = Image.FromFile(path);
                return image;
            }
            catch
            {
                return null;
            }
            
        }

       

        private void InputPathPictureBox_Click(object sender, EventArgs e)
        {
            OpenFileFolder(inputPathTextBox);
        }

        private void JdkPathPictureBox_Click(object sender, EventArgs e)
        {
            OpenFileFolder(jdkPathTextBox);
        }
        private void OpenFileFolder(TextBox control)
        {
            FolderBrowserDialog fd2 = new FolderBrowserDialog();
            if (fd2.ShowDialog() == DialogResult.OK)
                control.Text = fd2.SelectedPath;
        }

        private void ExportToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.IsEmpty())
            {
                MessageBox.Show("当前无数据可导出!");
            }
            else
            {
                ExportDataToExcel(apkInfoList);
            }
        }


        public void ExportDataToExcel(List<List<string>> apkInfoList)
        {

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "请选择要导出的位置";
            saveDialog.Filter = "Excel文件|*.xls";
            saveDialog.ShowDialog();
            string path = saveDialog.FileName;
            if (path.IndexOf(":") < 0) return;
            try
            {
                IWorkbook workbook = null;  //保存的数据
                FileStream fs = null;
                IRow row = null;
                ISheet sheet = null;  //生成表格
                ICell cell = null;
                try
                {
                    string[] columnName = { "ICON", "文件名", "Apk名", "包名", "主函数名", "大小" };
                    workbook = new HSSFWorkbook();
                    sheet = workbook.CreateSheet("Sheet0");//创建一个名称为Sheet0的表  
                    int rowCount = apkInfoList.Count;//行数  
                    int columnCount = 5;//列数  
                    sheet.SetColumnWidth(0, 20 * 256);
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
                        row = sheet.CreateRow(i + 1); //这里是新开启一行，从第二行开始写，第一行上面已经写完
                        row.Height = 40 * 40;
                        for (int j = 0; j <= columnCount; j++)
                        {
                            cell = row.CreateCell(j);//excel第二行开始写入数据，一列一列的分别创建单 元格 
                            if(j == 0)
                            {
                                byte[] bytes = File.ReadAllBytes(apkInfoList[i][0]);
                                int pictureIdx = workbook.AddPicture(bytes, PictureType.JPEG);
                                HSSFPatriarch patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();//前四个参数(dx1,dy1,dx2,dy2)为图片在单元格的边距                            //col1,col2表示图片插在col1和col2之间的单元格，索引从0开始                            //row1,row2表示图片插在第row1和row2之间的单元格，索引从1开始　　　　　　　　　　　　　　　　// 参数的解析: HSSFClientAnchor（int dx1,int dy1,int dx2,int dy2,int col1,int row1,int col2,int row2)            　　　　　　　　　//dx1:图片左边相对excel格的位置(x偏移) 范围值为:0~1023;即输100 偏移的位置大概是相对于整个单元格的宽度的100除以1023大概是10分之一            　　　　　　　　  //dy1:图片上方相对excel格的位置(y偏移) 范围值为:0~256 原理同上。            　　　　　　　　  //dx2:图片右边相对excel格的位置(x偏移) 范围值为:0~1023; 原理同上。            　　　　　　　　  //dy2:图片下方相对excel格的位置(y偏移) 范围值为:0~256 原理同上。            　　　　　　　　  //col1和row1 :图片左上角的位置，以excel单元格为参考,比喻这两个值为(1,1)，那么图片左上角的位置就是excel表(1,1)单元格的右下角的点(A,1)右下角的点。            　　　　　　　　  //col2和row2:图片右下角的位置，以excel单元格为参考,比喻这两个值为(2,2)，那么图片右下角的位置就是excel表(2,2)单元格的右下角的点(B,2)右下角的点。
                                HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 0, 0, 0, i + 1, 1, i + 2);
                                //把图片插到相应的位置
                                HSSFPicture pict = (HSSFPicture)patriarch.CreatePicture(anchor, pictureIdx);
                            }
                            else
                            cell.SetCellValue(apkInfoList[i][j]); //创建列的单元格的内容                             
                        }
                    }
                    using (fs = File.OpenWrite(path))
                    {
                        workbook.Write(fs);//向打开的这个xls文件中写入数据  

                    }
                    workbook.Close();
                    sheet = null;
                    workbook = null;
                    row = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导出失败:" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出失败:" + ex.Message);
            }
        }


        private void DataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dataGridView1.ContextMenuStrip = contextMenuStrip1;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string tmpPath = Path.Combine(Path.GetTempPath(), "ApkTool");
            Utils.FileUtil.DeleteDirectory(tmpPath);
        }
    }
}
