﻿using System;
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

namespace Citta_T1.Controls.Bottom
{
    public partial class BottomPreviewControl : UserControl
    {
        public BottomPreviewControl()
        {
            InitializeComponent();
            this.dataGridView.DoubleBuffered(true);
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridView.ReadOnly = true;
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
            Dictionary<string, int> induplicatedName = new Dictionary<string, int>() { };
            string headerText;
            int count;
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

            // 取消重命名
            for (int i = 0; i < this.dataGridView.Columns.Count; i++)
            {
                try
                {
                    this.dataGridView.Columns[i].HeaderText = this.dataGridView.Columns[i].Name.Split(seperator, 2)[1];
                }
                catch
                {
                    this.dataGridView.Columns[i].HeaderText = this.dataGridView.Columns[i].Name;
                }
            }
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

        public void PreViewDataByFullFilePath(string fullFilePath,
            char separator = '\t',
            DSUtil.ExtType extType = DSUtil.ExtType.Text, 
            DSUtil.Encoding encoding = DSUtil.Encoding.UTF8,
            bool isForceRead = false,
            int maxNumOfFile = 100
            )
        {
            List<List<string>> datas = new List<List<string>> { };
            List<string> rows;
            List<string> blankRow = new List<string> { };
            // 将来有可能新增文件类型,这里不能只用二元逻辑
            if (extType == DSUtil.ExtType.Excel)
                rows = new List<string>(BCPBuffer.GetInstance().GetCachePreViewExcelContent(fullFilePath, isForceRead).Split('\n'));
            else if (extType == DSUtil.ExtType.Text)
                rows = new List<string>(BCPBuffer.GetInstance().GetCachePreViewBcpContent(fullFilePath, encoding, isForceRead).Split('\n'));
            else
                rows = new List<string>();

            int numOfRows = rows.Count;
            for (int i = 0; i < numOfRows; i++)
            {
                blankRow.Add(" ");
            }
            for (int i = 0; i < Math.Max(numOfRows, maxNumOfFile); i++)
            {
                if (i >= numOfRows)
                    datas.Add(blankRow);
                else
                    datas.Add(new List<string>(rows[i].TrimEnd('\r').Split(separator)));                                                 // TODO 没考虑到分隔符
            }

            List<string> headers = datas[0];
            int numOfCols = headers.Count;
            // 不足100行时,补足剩余的空行,这样视觉效果上好一些
            for (int i = 0; i < maxNumOfFile - numOfRows; i++)
            {
                datas.Add(new List<string>(numOfCols));
            }


            DvgClean();
            _InitializeDGV(datas, headers, numOfCols);
            ControlUtil.DisableOrder(this.dataGridView);
        }
    }
}
