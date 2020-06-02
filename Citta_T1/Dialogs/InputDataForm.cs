using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Citta_T1.Core;
using Citta_T1.Utils;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Citta_T1.Dialogs
{
    // 
    public delegate void DelegateInputData(string name, string filePath, char separator, OpUtil.ExtType extType, OpUtil.Encoding encoding);
    public partial class InputDataForm : Form
    {
        private static LogUtil log = LogUtil.GetInstance("InputDataForm"); // 获取日志模块

        private OpUtil.Encoding encoding = OpUtil.Encoding.GBK;
        private OpUtil.ExtType extType = OpUtil.ExtType.Unknow;
        private string fullFilePath;
        private int maxNumOfRow = 100;
        private Font bold_font = new Font("微软雅黑", 12F, (FontStyle.Bold | FontStyle.Underline), GraphicsUnit.Point, 134);
        private Font font = new Font("微软雅黑", 12F, FontStyle.Underline, GraphicsUnit.Point, 134);
        private char separator = '\t';
        
        private string invalidChars = "&\"' ";
        private string invalidCharsPattern;
        private string[] invalidStringArr;

        private char emptySep = '\0';

        public InputDataForm()
        {
            InitializeComponent();
            this.dataGridView1.DoubleBuffered(true);
            this.InitInvalidCharPattern();
        }


        public void ReSetParams()
        {
            this.encoding = OpUtil.Encoding.GBK;
            this.gbkLable.Font = bold_font;
            this.utf8Lable.Font = font;

            this.radioButton1.Checked = true;
            this.separator = OpUtil.DefaultSeparator;

            this.extType = OpUtil.ExtType.Text;

            this.Clean();
        }
        private void PreviewButton_Click(object sender, EventArgs e)
        {
            /*
             * 数据预览
             */
            string fileName = "";
            string ext;
            OpenFileDialog fd = new OpenFileDialog();           
            fd.Filter = "files|*.txt;*.csv;*.bcp;*.xls;*.xlsx";
            if (this.gbkLable.Font.Bold)
                this.encoding = OpUtil.Encoding.GBK;
            else
                this.encoding = OpUtil.Encoding.UTF8;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                fullFilePath = fd.FileName;     
                fileName = Path.GetFileNameWithoutExtension(fullFilePath);
                ext = Path.GetExtension(fullFilePath);
                if (ext == ".xls" || ext == ".xlsx")
                {
                    this.extType = OpUtil.ExtType.Excel;
                    PreViewExcelFile();
                }
                else
                {
                    this.extType = OpUtil.ExtType.Text;
                    PreViewBcpFile();
                }          
            }
            //if (this.textBox1.Text == "请输入数据名称" || this.textBox1.Text == "")
            this.textBox1.Text = fileName;
            ControlUtil.DisableOrder(this.dataGridView1);
        }
        

        // 添加按钮
        public event DelegateInputData InputDataEvent;
        private void AddButton_Click(object sender, EventArgs e)
        {
            string name = this.textBox1.Text;
            if (this.textBox1.Text == "请输入数据名称")
            {
                MessageBox.Show("请输入数据名称！");
            }
            else if (String.IsNullOrEmpty(this.fullFilePath))
            {
                MessageBox.Show("请选择数据路径！");
            }
            else if (Global.GetDataSourceControl().DataSourceDictI2B.ContainsKey(this.fullFilePath))
            {
                String dsName = Global.GetDataSourceControl().DataSourceDictI2B[this.fullFilePath].DataSourceName;
                MessageBox.Show("该文件已导入，数据源名为：" + dsName + ", 如需重新导入请先卸载该数据");
            }
            else if (IsContainsInvalidChars(this.fullFilePath))
            {
                MessageBox.Show(String.Format("数据源路径中不能有空格、&、\"等特殊字符,当前选择的路径为: {0}", this.fullFilePath));
            }
            // 非法字符不得成为文件名
            //else if (IsContainsInvalidChars(this.textBox1.Text))
            //{
            //    MessageBox.Show(String.Format("数据源命名不能有、&、\"等特殊字符, 当前数据源名为: {0}", this.textBox1.Text));
            //}
            else
            {
                if (this.fullFilePath.EndsWith(".xls") || this.fullFilePath.EndsWith(".xlsx"))
                    this.extType = OpUtil.ExtType.Excel;
                else
                    this.extType = OpUtil.ExtType.Text;

                BCPBuffer.GetInstance().TryLoadFile(this.fullFilePath, this.extType, this.encoding);
                InputDataEvent(name, this.fullFilePath, this.separator, this.extType, this.encoding);
                DvgClean();
                Close();
            }
            this.extType = OpUtil.ExtType.Unknow;
            this.encoding = OpUtil.Encoding.UTF8;
        }
        private void InitInvalidCharPattern()
        {
            this.invalidStringArr = new string[this.invalidChars.Length];
            for (int i = 0; i < this.invalidChars.Length; i++)
            {
                invalidStringArr[i] = this.invalidChars[i].ToString();
            }
            this.invalidCharsPattern = string.Join("|", invalidStringArr);
        }
        private bool IsContainsInvalidChars(string text)
        {
            return Regex.Matches(text, this.invalidCharsPattern).Count > 0;
        }
        private void CancelButton_Click(object sender, EventArgs e)
        {
            // 关闭按钮
            DvgClean();
            this.ReSetParams();
            Close();
        }

        private void UTF8Lable_Click(object sender, EventArgs e)
        {
            if (this.extType == OpUtil.ExtType.Text)
            {
                this.gbkLable.Font = font;
                this.utf8Lable.Font = bold_font;
                this.encoding = OpUtil.Encoding.UTF8;
                PreViewBcpFile();
            }
        }
        private void Clean()
        {
            this.fullFilePath = null;
            this.textBox1.Text = null;
            this.textBoxEx1.Text = null;
            this.DvgClean();
        }

        private void PreViewBcpFile()
        {
            /*
             * @param this.isUTF8
             * @param this.fileName
             * 预览文件
             * 1. 编码格式
             * 2. 分隔符
             * 3. 初始化表头
             * 4. 清理表格数据
             * 5. 写入数据
             */
            if (this.fullFilePath == null)
                return;
            System.IO.StreamReader sr;
            FileStream fs = null;
            if (this.encoding == OpUtil.Encoding.UTF8)
            {
                sr = File.OpenText(fullFilePath);
            }
            else
            {
                fs = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs, System.Text.Encoding.Default);
            }
            String header = sr.ReadLine();
            if (header == null)
            {
                MessageBox.Show("导入文件错误，文件\"" + this.fullFilePath + "\" 为空");
                this.Clean();
                return;
            }
            String[] headers = header.Split(this.separator);
            int numOfCol = header.Split(this.separator).Length;
            DataGridViewTextBoxColumn[] ColumnList = new DataGridViewTextBoxColumn[numOfCol];
            this.dataGridView1.DataSource = null;
            DvgClean(false);
            try
            {
                DataTable table = new DataTable();
                DataRow row;
                DataView view;
                DataColumn[] cols = new DataColumn[numOfCol];
                Dictionary<string, int> induplicatedName = new Dictionary<string, int>() { };
                string headerText;
                char[] seperator = new char[] { '_' };

                // 可能有同名列，这里需要重命名一下
                for (int i = 0; i < numOfCol; i++)
                {
                    cols[i] = new DataColumn();
                    headerText = headers[i];
                    if (!induplicatedName.ContainsKey(headerText))
                        induplicatedName.Add(headerText, 0);
                    else
                    {
                        induplicatedName[headerText] += 1;
                        induplicatedName[headerText] = induplicatedName[headerText]; 
                    }
                    headerText = induplicatedName[headerText] + "_" + headerText;
                    cols[i].ColumnName = headerText;
                }

                table.Columns.AddRange(cols);

                for (int rowIndex = 0; rowIndex < maxNumOfRow && !sr.EndOfStream; rowIndex++)
                {
                    String line = sr.ReadLine();
                    if (line == null)
                        continue;
                    row = table.NewRow();
                    String[] eles = line.Split(this.separator);
                    for (int colIndex = 0; colIndex < Math.Min(numOfCol, eles.Length); colIndex++)
                    {
                        row[colIndex] = eles[colIndex];
                    }
                    table.Rows.Add(row);
                }

                view = new DataView(table);
                this.dataGridView1.DataSource = view;


                // 取消重命名
                for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
                {
                    try
                    {
                        this.dataGridView1.Columns[i].HeaderText = this.dataGridView1.Columns[i].Name.Split(seperator, 2)[1];
                    }
                    catch
                    {
                        this.dataGridView1.Columns[i].HeaderText = this.dataGridView1.Columns[i].Name;
                        log.Error("读取文件时，去重命名过程出错");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("FromInputData.OverViewFile occurs error! " + ex);
            }
            finally
            {
                if (fs != null)
                    fs.Close();
                sr.Close();
            }
        }

        private void PreViewExcelFile(string sheetName = null, bool isFirstRowColumn = true)
        {
            ISheet sheet = null;
            IWorkbook workbook = null;
            FileStream fs = null;
            DataTable data = new DataTable();
            int startRow = 0;
            //
            try
            {
                fs = new FileStream(this.fullFilePath, FileMode.Open, FileAccess.Read);
                if (this.fullFilePath.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else
                    workbook = new HSSFWorkbook(fs);   // 2003版本
                if (sheetName != null)
                    sheet = workbook.GetSheet(sheetName);
                else
                    sheet = workbook.GetSheetAt(0);

                if (sheet == null)
                {
                    fs.Close();
                    return;
                }
                
                IRow firstRow = sheet.GetRow(0);
                int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数
                DataGridViewTextBoxColumn[] ColumnList = new DataGridViewTextBoxColumn[cellCount];
                DvgClean(false);
                if (isFirstRowColumn)
                {
                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                    {
                        ColumnList[i] = new DataGridViewTextBoxColumn();
                        ColumnList[i].HeaderText = firstRow.GetCell(i).StringCellValue;
                        ColumnList[i].Name = "Col " + i.ToString();
                    }
                    this.dataGridView1.Columns.AddRange(ColumnList);
                    startRow = sheet.FirstRowNum + 1;
                }
                else
                {
                    startRow = sheet.FirstRowNum;
                }
                //最后一列的标号
                int rowCount = sheet.LastRowNum;
                for (int i = 0; i <= Math.Min(maxNumOfRow, rowCount); ++i)
                {
                    IRow row = sheet.GetRow(i + startRow);
                    if (row == null) continue; //没有数据的行默认是null　　　　　　　

                    DataGridViewRow dr = new DataGridViewRow();
                    this.dataGridView1.Rows.Add(dr);
                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                    {
                        if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                        {
                            string content = row.GetCell(j).ToString();
                            this.dataGridView1.Rows[i].Cells[j].Value = content;
                        }
                    }
                }
            }
            catch (System.IO.IOException ex)
            {
                MessageBox.Show(string.Format("文件{0}已被打开，请先关闭该文件", this.fullFilePath));
                log.Error("预读Excel: " + this.fullFilePath + " 失败, error: " + ex.Message);
            }
            catch (Exception ex)
            {
                log.Error("预读Excel: " + this.fullFilePath + " 失败, error: " + ex.Message);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                if (workbook != null)
                    workbook.Close();
            }
        }

        public void DvgClean(bool isCleanDataName = true)
        {
            if (isCleanDataName) { this.textBox1.Text = null; }
            this.dataGridView1.DataSource = null; // System.InvalidOperationException:“操作无效，原因是它导致对 SetCurrentCellAddressCore 函数的可重入调用。”
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Columns.Clear();
        }

        private void GBKLable_Click(object sender, EventArgs e)
        {
            if (this.extType == OpUtil.ExtType.Text)
            {
                this.gbkLable.Font = bold_font;
                this.utf8Lable.Font = font;
                this.encoding = OpUtil.Encoding.GBK;
                PreViewBcpFile();
            }

        }

        private void radioButton1_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.extType != OpUtil.ExtType.Text)
                return;
            this.separator = '\t';
            PreViewBcpFile();
        }
        private void radioButton2_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.extType != OpUtil.ExtType.Text)
                return;
            this.separator = ',';
            PreViewBcpFile();

        }
        private void radioButton3_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.extType != OpUtil.ExtType.Text)
                return;
            this.radioButton3.Checked = true;
            this.textBoxEx1.Focus();
            if (this.textBoxEx1.Text == null || this.textBoxEx1.Text == "")
            {
                this.separator = this.emptySep;
                return;
            }
            try
            {
                this.separator = Regex.Unescape(this.textBoxEx1.Text).ToCharArray()[0];
                PreViewBcpFile();
            }
            catch (Exception)
            {
                MessageBox.Show("指定的分隔符有误！目前分隔符为：" + this.textBoxEx1.Text);
            }

        }

        private void textBoxEx1_TextChanged(object sender, EventArgs e)
        {
            if (this.extType == OpUtil.ExtType.Text)
            {
                this.radioButton3.Checked = true;
                if (this.textBoxEx1.Text == null || this.textBoxEx1.Text == "")
                    this.separator = this.emptySep;
                else
                    try
                    {
                        this.separator = Regex.Unescape(this.textBoxEx1.Text).ToCharArray()[0];
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex.ToString());
                        MessageBox.Show("指定的分隔符有误！目前分隔符为：" + this.textBoxEx1.Text);
                    }
                PreViewBcpFile();
            }
        }

        private void textBoxEx1_MouseDown(object sender, MouseEventArgs e)
        {
            this.radioButton3.Checked = true;
        }

        public void Demo(string demo, string src)
        {
            this.saveFileDialog.FileName = demo;
            DialogResult dr = this.saveFileDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string srcFilePath = Path.Combine(Application.StartupPath, src);
                string dstFilePath = this.saveFileDialog.FileName;
                try
                {
                    FileInfo file = new FileInfo(srcFilePath);
                    file.CopyTo(dstFilePath, true);
                }
                catch (Exception ex)
                {
                    log.Error("导出文件出错:" + ex.Message);
                }
            }

        }
        private void DemoDownloadExcel_Click(object sender, EventArgs e)
        {
            Demo("demo_excel.xlsx", @"\Demo\demo_excel.xlsx");
        }

        private void DemoDownloadCsv_Click(object sender, EventArgs e)
        {
            Demo("demo_csv.csv", @"\Demo\demo_csv.csv");
        }

        private void DemoDownloadTxt_Click(object sender, EventArgs e)
        {
            Demo("demo_txt.txt", @"\Demo\demo_txt.txt");
        }

        private void DemoDownloadBcp_Click(object sender, EventArgs e)
        {
            Demo("demo_bcp.bcp", @"\Demo\demo_bcp.bcp");
        }
    }
}
