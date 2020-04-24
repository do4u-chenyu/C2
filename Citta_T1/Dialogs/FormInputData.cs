using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Citta_T1.Utils;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using static Citta_T1.Utils.DSUtil;

namespace Citta_T1.Dialogs
{
    // 
    public delegate void delegateInputData(string name, string filePath, DSUtil.ExtType extType, DSUtil.Encoding encoding);
    public partial class FormInputData : Form
    {
        private DSUtil.Encoding encoding = DSUtil.Encoding.GBK;
        private ExtType extType = ExtType.Unknow;
        private string m_filePath;
        private int m_maxNumOfRow = 100;
        private Font bold_font = new Font("微软雅黑", 12F, (FontStyle.Bold | FontStyle.Underline), GraphicsUnit.Point, 134);
        private Font font = new Font("微软雅黑", 12F, FontStyle.Underline, GraphicsUnit.Point, 134);
        private bool textboxHasText = false;
        private char separator = '\t';
        private LogUtil log = LogUtil.GetInstance("FormInputData"); // 获取日志模块

        public FormInputData()
        {
            InitializeComponent();
            this.textBox1.LostFocus += new EventHandler(this.textBox1_Leave);
            this.textBox1.GotFocus += new EventHandler(this.textBox1_Enter);
        }


        private void textBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            /*
             * 数据预览
             */
            string fileName="";
            string ext;
            OpenFileDialog fd = new OpenFileDialog();           
            fd.Filter = "files|*.txt;*.bcp;*.xls;*.xlsx";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                m_filePath = fd.FileName;     
                fileName = Path.GetFileNameWithoutExtension(@m_filePath);
                ext = Path.GetExtension(m_filePath);
                if (ext == ".xls" || ext == ".xlsx")
                {
                    this.extType = ExtType.Excel;
                    PreViewExcelFile();
                }
                else
                {
                    this.extType = ExtType.Text;
                    PreViewBcpFile();
                }          
            }
            if (this.textBox1.Text == "请输入数据名称" || this.textBox1.Text == "")
                this.textBox1.Text = fileName;

        }


        // 添加按钮
        public event delegateInputData InputDataEvent;
        private void button2_Click(object sender, EventArgs e)
        {
            string name = this.textBox1.Text;
            if (this.textBox1.Text == "请输入数据名称")
            {
                MessageBox.Show("请输入数据名称！");
            }
            else if (m_filePath == null)
            {
                MessageBox.Show("请选择数据路径！");
            }
            else if (Global.GetMainForm().dataSourceControl.dataSourceDictI2B.ContainsKey(m_filePath))
            {
                String dsName = Global.GetMainForm().dataSourceControl.dataSourceDictI2B[m_filePath].txtButton.Text;
                MessageBox.Show("该文件已存在，数据名为：" + dsName);
            }
            else
            {
                if (m_filePath.EndsWith(".xls") || m_filePath.EndsWith(".xlsx"))
                    this.extType = DSUtil.ExtType.Excel;
                else
                    this.extType = DSUtil.ExtType.Text;
                BCPBuffer.GetInstance().TryLoadBCP(m_filePath, this.extType, this.encoding);
                InputDataEvent(name, m_filePath, this.extType, this.encoding);
                DvgClean();
                Close();
            }
            this.extType = DSUtil.ExtType.Unknow;
            this.encoding = DSUtil.Encoding.UTF8;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 关闭按钮
            DvgClean();
            Close();
        }

        private void UTF8Lable_Click(object sender, EventArgs e)
        {
            if (this.extType == ExtType.Text)
            {
                this.gbkLable.Font = font;
                this.utf8Lable.Font = bold_font;
                this.encoding = DSUtil.Encoding.UTF8;
                PreViewBcpFile();
            }
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
            System.IO.StreamReader sr;
            if (this.encoding == DSUtil.Encoding.UTF8)
            {
                sr = File.OpenText(m_filePath);
            }
            else
            {
                FileStream fs = new FileStream(m_filePath, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs, System.Text.Encoding.Default);
            }
            String header = sr.ReadLine();
            String[] headers = header.Split(this.separator);
            int numOfCol = header.Split(this.separator).Length;
            DataGridViewTextBoxColumn[] ColumnList = new DataGridViewTextBoxColumn[numOfCol];
            this.dataGridView1.DataSource = null;
            DvgClean(false);
            try
            {
                DataTable table = new DataTable();
                DataColumn column;
                DataRow row;
                DataView view;
                DataColumn[] cols = new DataColumn[numOfCol];
                for (int i = 0; i < numOfCol; i++)
                {
                    cols[i] = new DataColumn();
                    cols[i].ColumnName = headers[i];
                }

                table.Columns.AddRange(cols);

                for (int rowIndex = 0; rowIndex < m_maxNumOfRow && !sr.EndOfStream; rowIndex++)
                {
                    String line = sr.ReadLine();
                    if (line == null)
                        continue;
                    row = table.NewRow();
                    String[] eles = line.Split(this.separator);
                    if (eles.Length != numOfCol)
                        continue;
                    for (int colIndex = 0; colIndex < numOfCol; colIndex++)
                    {
                        row[colIndex] = eles[colIndex];
                    }
                    table.Rows.Add(row);
                    log.Info("tabel.rowNum = " + table.Rows.Count);
                }

                view = new DataView(table);
                this.dataGridView1.DataSource = view;
            }
            catch (Exception ex)
            {
                Console.WriteLine("FromInputData.OverViewFile occurs error! " + ex);
            }
        }

        private void PreViewExcelFile(string sheetName = null, bool isFirstRowColumn = true)
        {
            ISheet sheet = null;
            XSSFWorkbook workbook2007;
            HSSFWorkbook workbook2003;
            FileStream fs;
            DataTable data = new DataTable();
            int startRow = 0;
            //
            try
            {
                fs = new FileStream(m_filePath, FileMode.Open, FileAccess.Read);
                if (m_filePath.IndexOf(".xlsx") > 0) // 2007版本
                {
                    workbook2007 = new XSSFWorkbook(fs);
                    if (sheetName != null)
                    {
                        sheet = workbook2007.GetSheet(sheetName);
                    }
                    else
                    {
                        sheet = workbook2007.GetSheetAt(0);
                    }
                }
                else
                {
                    workbook2003 = new HSSFWorkbook(fs);   // 2003版本
                    if (sheetName != null)
                    {
                        sheet = workbook2003.GetSheet(sheetName);
                    }
                    else
                    {
                        sheet = workbook2003.GetSheetAt(0);
                    }
                }

                if (sheet != null)
                {
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
                    for (int i = 0; i <= Math.Min(m_maxNumOfRow, rowCount); ++i)
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
                                log.Info("i: " + i + ", j: " + j + ", content: " + content);
                                this.dataGridView1.Rows[i].Cells[j].Value = content;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        public void DvgClean(bool isCleanDataName = true)
        {
            if (isCleanDataName) { this.textBox1.Text = null; }
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Columns.Clear();
        }

        private void GBKLable_Click(object sender, EventArgs e)
        {
            if (this.extType == ExtType.Text)
            {
                this.gbkLable.Font = bold_font;
                this.utf8Lable.Font = font;
                this.encoding = DSUtil.Encoding.GBK;
                PreViewBcpFile();
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.extType == ExtType.Text)
            {
                this.separator = '\t';
                PreViewBcpFile();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.extType == ExtType.Text)
            {
                this.separator = ',';
                PreViewBcpFile();
            }

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.extType == ExtType.Text && this.textBoxEx1.Text != null && this.textBoxEx1.Text != "")
            {
                try
                {
                    this.separator = this.textBoxEx1.Text.ToCharArray()[0];
                }
                catch (Exception ex)
                {
                    MessageBox.Show("指定的分隔符有误！目前分隔符为：" + this.textBoxEx1.Text);
                }
                PreViewBcpFile();
            }
        }
        private void radioButton1_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.extType != ExtType.Text)
                return;
        }

        private void radioButton2_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.extType != ExtType.Text)
                return;
        }
        private void radioButton3_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.extType == ExtType.Text)
            {
                if (this.textBoxEx1.Text == null || this.textBoxEx1.Text == "")
                {
                    MessageBox.Show("未指定分隔符");
                    return;
                }
            }
        }
        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <returns>返回的DataTable</returns>
        public DataTable ExcelToDataTable(string fileName, string sheetName, bool isFirstRowColumn)
        {
            ISheet sheet = null;
            XSSFWorkbook workbook2007;
            HSSFWorkbook workbook2003;
            FileStream fs;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                {
                    workbook2007 = new XSSFWorkbook(fs);
                    if (sheetName != null)
                    {
                        sheet = workbook2007.GetSheet(sheetName);
                    }
                    else
                    {
                        sheet = workbook2007.GetSheetAt(0);
                    }
                }
                else
                {
                    workbook2003 = new HSSFWorkbook(fs);   // 2003版本
                    if (sheetName != null)
                    {
                        sheet = workbook2003.GetSheet(sheetName);
                    }
                    else
                    {
                        sheet = workbook2003.GetSheetAt(0);
                    }
                }

                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数
                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            DataColumn column = new DataColumn(firstRow.GetCell(i).StringCellValue);
                            data.Columns.Add(column);
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }
                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }
    }
}
