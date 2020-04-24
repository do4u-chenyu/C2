using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Citta_T1.Utils;

namespace Citta_T1
{
    public partial class DataGridView0 : UserControl
    {
        public DataGridView0()
        {
            InitializeComponent();
            InitializeDgv();
        }

        private void DataGridView_Load(object sender, EventArgs e)
        {
            //_InitializeRows();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void InitializeDgv()
        {
            List<List<string>> datas = new List<List<string>>();
            datas.Add(new List<string>() { "姓名", "身份证号", "手机号", "年龄", "地址" });
            List<string> headers = datas[0];
            int numOfCols = headers.Count;
            for (int i = 0; i < maxNumOfRows; i++)
                datas.Add(new List<string>() { "", "", "", "", "", "" });
            _InitializeColumns(headers);
            _InitializeRowse(datas.GetRange(1, datas.Count - 1), numOfCols);

        }
        private void _InitializeColumns(List<string> headers)
        {
            /*
             * 初始化列
             */
            int numOfCols = headers.Count;
            System.Windows.Forms.DataGridViewTextBoxColumn[] ColumnList = new System.Windows.Forms.DataGridViewTextBoxColumn[numOfCols];
            for (int i = 0; i < numOfCols; i++)
            {
                ColumnList[i] = new System.Windows.Forms.DataGridViewTextBoxColumn();
                ColumnList[i].HeaderText = headers[i];
                ColumnList[i].Name = "Col_" + i.ToString();
            }
            this.dataGridView.Columns.AddRange(ColumnList);
        }

        private void _InitializeRowse(List<List<string>> datas, int numOfCols)
        {
            /*
             * 初始化行
             * 使用样例数据
             */
            string data;
            for (int i = 0; i < maxNumOfRows; i = this.dataGridView.Rows.Add())
            {
                //this.dataGridView1.Rows.Add();
                for (int j = 0; j < numOfCols; j++)
                {
                    try
                    {
                        data = datas[i][j];
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        data = "";
                        Console.WriteLine("DataGridView0.Designer.cs._InitializeRowse occurs error!");
                    }
                    this.dataGridView.Rows[i].Cells[j].Value = data;
                }
            }
        }

        private List<List<string>> PreViewFileFromPath(string fileNameOrFile = "", int maxNumOfFile = 50, char sep = '\t')
        {
            List<List<string>> datas = new List<List<string>> { };
            System.IO.StreamReader file = new System.IO.StreamReader(fileNameOrFile);
            int rowCounter = 0;
            string line;
            while (((line = file.ReadLine()) != null) && (rowCounter < maxNumOfFile))
            {
                List<string> eles = new List<string>(line.Split(sep));
                datas.Add(eles);
            }
            return datas;
        }
        private List<List<string>> PreViewFileFromResx(string resx = "", int maxNumOfFile = 50, char sep = '\t')
        {
            List<List<string>> datas = new List<List<string>> { };
            string[] contents = resx.Split('\n');
            int numOfRows = contents.Length;
            List<string> rows = new List<string>(contents);
            for (int i = 0; i < (numOfRows < maxNumOfFile ? numOfRows : maxNumOfRows); i++)
            {
                datas.Add(new List<string>(rows[i].Split('\t')));
            }
            return datas;
        }
        public void PreViewDataByBcpPath(string bcpPath,
            char separator = '\t',
            DSUtil.ExtType extType = DSUtil.ExtType.Text, 
            DSUtil.Encoding encoding = DSUtil.Encoding.UTF8,
            int maxNumOfFile = 100
            )
        {
            List<List<string>> datas = new List<List<string>> { };
            List<string> rows;
            // TODO [DK] 支持多种数据格式
            if (extType == DSUtil.ExtType.Excel)
                rows = new List<string>(BCPBuffer.GetInstance().GetCacheExcelPreVewContent(bcpPath).Split('\n'));
            else
                rows = new List<string>(BCPBuffer.GetInstance().GetCacheBcpPreVewContent(bcpPath, encoding).Split('\n')); 
            int numOfRows = rows.Count;
            for (int i = 0; i < Math.Min(numOfRows, maxNumOfFile); i++)
            {
                datas.Add(new List<string>(rows[i].TrimEnd('\r').Split(separator)));                                                 // TODO 没考虑到分隔符
            }

            this.dataGridView.Rows.Clear();
            this.dataGridView.Columns.Clear();
            this.dataGridView.DataSource = null;
            List<string> headers = datas[0];
            int numOfCols = headers.Count;
            _InitializeColumns(headers);
            _InitializeRowse(datas.GetRange(1, datas.Count - 1), numOfCols);
        }
    }
}
