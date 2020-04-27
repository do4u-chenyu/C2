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
using System.Reflection;

namespace Citta_T1
{
    public partial class DataGridView0 : UserControl
    {
        public DataGridView0()
        {
            InitializeComponent();
            this.dataGridView.DoubleBuffered(true);
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            InitializeDgv();
        }

        private void InitializeDgv()
        {
            List<List<string>> datas = new List<List<string>>();
            datas.Add(new List<string>() { "姓名", "身份证号", "手机号", "年龄", "地址" });
            List<string> headers = datas[0];
            int numOfCols = headers.Count;
            for (int i = 0; i < maxNumOfRows; i++)
                datas.Add(new List<string>() { "", "", "", "", "", "" });
            //_InitializeColumns(headers);
            //_InitializeRowse(datas.GetRange(1, datas.Count - 1), numOfCols);
            _InitializeDGV(datas, headers, numOfCols);

        }
        public void DvgClean(bool isCleanDataName = true)
        {
            this.dataGridView.DataSource = null;
            this.dataGridView.Rows.Clear();
            this.dataGridView.Columns.Clear();
        }
        private void _InitializeDGV(List<List<string>> datas, List<string> headers, int numOfCol)
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

            for (int rowIndex = 1; rowIndex < Math.Min(maxNumOfRows, datas.Count - 1); rowIndex++)
            {
                List<String> eles = datas[rowIndex];
                if (eles == null)
                    continue;
                row = table.NewRow();
                for (int colIndex = 0; colIndex < Math.Min(numOfCol, eles.Count); colIndex++)
                {
                    row[colIndex] = eles[colIndex];
                }
                table.Rows.Add(row);
            }
            view = new DataView(table);
            this.dataGridView.DataSource = view;
            this.ResetColumnsWidth();
        }

        private void ResetColumnsWidth(int minWidth = 50)
        {
            for (int i = 0; i < this.dataGridView.Columns.Count; i++)
            {
                this.dataGridView.Columns[i].MinimumWidth = minWidth;
            }
            for (int i = 0; i < this.dataGridView.Columns.Count; i++)
            {
                this.dataGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
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

            List<string> headers = datas[0];
            int numOfCols = headers.Count;
            DvgClean();
            _InitializeDGV(datas, headers, numOfCols);
        }
    }
}
