using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace C2.Dialogs
{
    // 
    public delegate void DelegateInputData(string name, string filePath, char separator, OpUtil.ExtType extType, OpUtil.Encoding encoding);
    public partial class InputDataForm : Form
    {
        private static LogUtil log = LogUtil.GetInstance("InputDataForm"); // 获取日志模块
        private string[] illegalCharacters = new string[] { "*", "\\", "/", "$", "[", "]", "+", "-", "&", "%", "#", "!", "~", "`", " ", "\\t", "\\n", "\\r", ":" };
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
            this.separator = OpUtil.DefaultFieldSeparator;

            this.extType = OpUtil.ExtType.Text;

            this.Clear();
        }
        private void PreviewButton_Click(object sender, EventArgs e)
        {
            /*
             * 数据预览
             */
            string fileName = String.Empty;
            string ext;
            OpenFileDialog fd = new OpenFileDialog
            {
                Filter = "files|*.txt;*.csv;*.bcp;*.xls;*.xlsx"
            };
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
                    PreviewExcelFileNew();
                }
                else
                {
                    this.extType = OpUtil.ExtType.Text;
                    PreviewBcpFile();
                }
            }
            this.textBox1.Text = fileName;
            DgvUtil.DisableOrder(this.dataGridView1);
        }


        // 添加按钮
        public event DelegateInputData InputDataEvent;
        private void AddButton_Click(object sender, EventArgs e)
        {
            /*
             * 导入规则 0722
             * 1. 关于数据源名称。
             *      必须修改名字。即名字不能为默认
             *      名字不能为空。
             * 2. 关于数据源路径。路径必须不为空
             * 3. 关于是否可导入重复数据。重复数据源不予导入
             * 4. 关于数据源路径是否可包含所有字符。数据源路径不包含非法字符（如空格、等于号、大于号小于号等）
             * 
             * 导入规则 0727
             * 1. 先没有表头的文件可以预览但是不能导入
             */
            string name = this.textBox1.Text;
            if (name == "请输入数据名称" || name == "" || String.IsNullOrEmpty(name))
                HelpUtil.ShowMessageBox("请输入数据名称！");
            else if (FileUtil.IsContainIllegalCharacters(name, "数据名称", false))
                HelpUtil.ShowMessageBox("数据名称中存在非法字符，请检查数据名称！非法字符" + System.Environment.NewLine + "非法字符包含：*, \\, $, [, ], +, -, &, %, #, !, ~, `, \\t, \\n, \\r, :, 空格");
            else if (String.IsNullOrEmpty(this.fullFilePath))
                HelpUtil.ShowMessageBox("请选择数据路径！");
            else if (Global.GetDataSourceControl().DataSourceDictI2B.ContainsKey(this.fullFilePath))
            {
                String dsName = Global.GetDataSourceControl().DataSourceDictI2B[this.fullFilePath].DataSourceName;
                HelpUtil.ShowMessageBox("该文件已导入，数据源名为：" + dsName + ", 如需重新导入请先卸载该数据");
            }
            else if (IsContainsInvalidChars(this.fullFilePath))
                MessageBox.Show(String.Format("数据源路径中不能有空格、&、\"等特殊字符,当前选择的路径为: {0}", this.fullFilePath));
            else
            {
                if (this.fullFilePath.EndsWith(".xls") || this.fullFilePath.EndsWith(".xlsx"))
                    this.extType = OpUtil.ExtType.Excel;
                else
                    this.extType = OpUtil.ExtType.Text;

                bool success = BCPBuffer.GetInstance().TryLoadFile(this.fullFilePath, this.extType, this.encoding, this.separator);
                if (!success)
                    return;
                if (BCPBuffer.GetInstance().IsEmptyHeader(this.fullFilePath))
                {
                    BCPBuffer.GetInstance().Remove(this.fullFilePath);
                    HelpUtil.ShowMessageBox("数据源表头不能为空，请检查数据源文件。");
                    return;
                }
                InputDataEvent(name, this.fullFilePath, this.separator, this.extType, this.encoding);
                DvgClear();
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
            DvgClear();
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
                PreviewBcpFile();
            }
        }
        private void Clear()
        {
            this.fullFilePath = null;
            this.textBox1.Text = null;
            this.textBoxEx1.Text = null;
            this.DvgClear();
        }

        
        private void PreviewBcpFile()
        {
            Tuple<List<string>, List<List<string>>> headersAndRows = FileUtil.ReadBcpFile(this.fullFilePath, this.encoding, this.separator, this.maxNumOfRow);
            if (headersAndRows.Item1.Count == 0 && headersAndRows.Item2.Count == 0)
            {
                MessageBox.Show("导入文件错误，文件\"" + this.fullFilePath + "\" 为空");
                this.Clear();
                return;
            }
            // 特殊情况，表头有一行数据
            List<List<string>> rows = new List<List<string>>();
            rows.Add(headersAndRows.Item1);
            rows.AddRange(headersAndRows.Item2);
            FileUtil.FillTable(this.dataGridView1, rows, this.maxNumOfRow);
        }

        private void PreviewExcelFileNew()
        {
            this.Cursor = Cursors.WaitCursor;
            bool isReadSucc = BCPBuffer.GetInstance().TryLoadFile(this.fullFilePath, this.extType, this.encoding, this.separator);
            this.Cursor = Cursors.Default;
            if (!isReadSucc)
                return;
            List<List<String>> rowContentList = StringTo2DList(BCPBuffer.GetInstance().GetCachePreviewExcelContent(this.fullFilePath), '\t');
            if (rowContentList.Count == 0)
            {
                this.Clear();
                return;
            }

            int cellCount = rowContentList[0].Count;
            DataGridViewTextBoxColumn[] ColumnList = new DataGridViewTextBoxColumn[cellCount];
            DvgClear(false);
            for (int i = 0; i < cellCount; i++)
            {
                ColumnList[i] = new DataGridViewTextBoxColumn
                {
                    HeaderText = rowContentList[0][i],
                    Name = "Col " + i.ToString()
                };
            }
            this.dataGridView1.Columns.AddRange(ColumnList);
            for (int i = 1; i < rowContentList.Count; i++)
            {
                DataGridViewRow dr = new DataGridViewRow();
                this.dataGridView1.Rows.Add(dr);
                for (int j = 0; j < cellCount; ++j)
                    this.dataGridView1.Rows[i-1].Cells[j].Value = rowContentList[i][j];
            }
        }

        private List<List<String>> StringTo2DList(string raw, char sep)
        {
            /*
             * raw = ""date\tdelay\tdistance\torigin\tdestination\r\n1011245\t6\t602\tABE\tATL\r\n"
             */
            char[] lineSep = "\r\n".ToCharArray();
            List<List<String>> rawContents = new List<List<string>>();
            foreach (string line in raw.Split(lineSep))
            {
                if (line != "")
                    rawContents.Add(new List<string>(line.Split(sep)));
            }
            return rawContents;
        }

        public void DvgClear(bool isCleanDataName = true)
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
                PreviewBcpFile();
            }

        }

        private void RadioButton1_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.extType != OpUtil.ExtType.Text)
                return;
            this.separator = '\t';
            PreviewBcpFile();
        }
        private void RadioButton2_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.extType != OpUtil.ExtType.Text)
                return;
            this.separator = ',';
            PreviewBcpFile();

        }
        private void RadioButton3_MouseDown(object sender, MouseEventArgs e)
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
                PreviewBcpFile();
            }
            catch (Exception)
            {
                MessageBox.Show("指定的分隔符有误！目前分隔符为：" + this.textBoxEx1.Text);
            }

        }

        private void TextBoxEx1_TextChanged(object sender, EventArgs e)
        {
            if (this.fullFilePath == null)
                return;
            if (this.extType == OpUtil.ExtType.Text)
            {
                this.radioButton3.Checked = true;
                // 没有指定分隔符
                this.separator = this.emptySep;
                if (!string.IsNullOrEmpty(this.textBoxEx1.Text))
                {
                    char[] seps = Regex.Unescape(this.textBoxEx1.Text).ToCharArray();
                    if (seps.Length > 0)
                        this.separator = seps[0];
                }
                PreviewBcpFile();
            }
        }

        private void TextBoxEx1_MouseDown(object sender, MouseEventArgs e)
        {
            this.radioButton3.Checked = true;
        }

        public void Demo(string demo, string src)
        {
            SaveFileDialog saveDataSend = new SaveFileDialog();
            saveDataSend.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            saveDataSend.Filter = "files|*.txt;*.csv;*.bcp;*.xls;*.xlsx";
            saveDataSend.FileName = demo;
            if (saveDataSend.ShowDialog() == DialogResult.OK)
            {
                string srcFilePath = Application.StartupPath + src;
                string dstFilePath = saveDataSend.FileName;
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


        private void DataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rect = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y,
                this.dataGridView1.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                this.dataGridView1.RowHeadersDefaultCellStyle.Font, rect,
                this.dataGridView1.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void InputDataForm_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = "请输入数据名称";
        }
    }
}
