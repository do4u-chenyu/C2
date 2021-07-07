﻿using C2.Core;
using C2.Model;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace C2.Controls.Bottom
{
    public partial class BottomPreviewControl : UserControl
    {
        private int maxNumOfRows = 20;
        public BottomPreviewControl()
        {
            InitializeComponent();
            InitializeDataGridView();
            InitializeDataGridViewStyle();
        }

        private void InitializeDataGridView()
        {
            List<List<string>> datas = new List<List<string>>
            {
                new List<string>() { "姓名", "身份证号", "手机号", "年龄", "地址" }
            };
            List<string> headers = datas[0];
            int numOfCols = headers.Count;
            for (int i = 0; i < maxNumOfRows; i++)
                datas.Add(new List<string>(5));

            InitializeDGV(datas, headers, numOfCols);
            
        }
        private void InitializeDGV(List<List<string>> datas, List<string> headers, int numOfCol)
        {
            DataColumn[] cols = new DataColumn[numOfCol];
            Dictionary<string, int> induplicatedName = new Dictionary<string, int>();

            // 可能有同名列，这里需要重命名一下
            for (int i = 0; i < numOfCol; i++)
            {
                cols[i] = new DataColumn();
                string headerText = headers[i];
                if (induplicatedName.ContainsKey(headerText))
                    induplicatedName[headerText]++;
                else
                    induplicatedName[headerText] = 0;

                cols[i].ColumnName = induplicatedName[headerText] + "_" + headerText;
            }

            DataTable table = new DataTable();
            table.Columns.AddRange(cols);

            for (int rowIndex = 1; rowIndex < Math.Min(maxNumOfRows, datas.Count - 1); rowIndex++)
            {
                List<String> elements = datas[rowIndex];
                if (elements == null)
                    continue;
                DataRow row = table.NewRow();
                for (int colIndex = 0; colIndex < Math.Min(numOfCol, elements.Count); colIndex++)
                    row[colIndex] = elements[colIndex];
                table.Rows.Add(row);
            }
            this.dataGridView.DataSource = new DataView(table);
            DgvUtil.ResetColumnsWidth(this.dataGridView);

            // 取消重命名
            for (int i = 0; i < this.dataGridView.Columns.Count; i++)
            {
                try
                {
                    this.dataGridView.Columns[i].HeaderText = this.dataGridView.Columns[i].Name.Split(new char[] { '_' }, 2)[1];
                }
                catch
                {
                    this.dataGridView.Columns[i].HeaderText = this.dataGridView.Columns[i].Name;
                }
            }
            // 取消排序功能
            DgvUtil.DisableOrder(this.dataGridView);
        }

        private void InitializeDataGridViewStyle()
        {
            this.dataGridView.DoubleBuffered(true);
            this.dataGridView.RowHeadersWidth = 28;
            this.dataGridView.EnableHeadersVisualStyles = true;
            this.dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.BackgroundColor = Color.FromArgb(230, 237, 246);
        }
        
        public void PreViewDataByFullFilePath(string fullFilePath,
            char separator = '\t',
            OpUtil.ExtType extType = OpUtil.ExtType.Text,
            OpUtil.Encoding encoding = OpUtil.Encoding.UTF8,
            bool isForceRead = false,
            int maxNumOfFile = 100
            )
        {
            List<List<string>> datas = new List<List<string>> { };
            List<string> rows;
            // 将来有可能新增文件类型,这里不能只用二元逻辑
            if (extType == OpUtil.ExtType.Excel)
            {
                separator = OpUtil.TabSeparator;  // 当文件类型是Excel是,内部分隔符自动为'\t',此时用其他分隔符没有意义
                rows = new List<string>(BCPBuffer.GetInstance().GetCachePreviewExcelContent(fullFilePath, isForceRead).Split('\n'));
            }
            else if (extType == OpUtil.ExtType.Text)
                rows = new List<string>(BCPBuffer.GetInstance().GetCachePreviewBcpContent(fullFilePath, encoding, isForceRead).Split('\n'));
            else
                rows = new List<string>();

            for (int i = 0; i < Math.Min(rows.Count, maxNumOfFile); i++)
                datas.Add(new List<string>(rows[i].TrimEnd(OpUtil.TabSeparator).Split(separator)));                                                 // TODO 没考虑到分隔符
            FileUtil.FillTable(this.dataGridView, datas, maxNumOfFile);
        }

        public void PreViewDataByDatabase(DataItem item)
        {
            this.PreViewDataByDatabase(item.DBItem);
        }
        private void PreViewDataByDatabase(DatabaseItem dbItem, int maxNumOfFile = 100)
        {
            List<List<string>> datas = new List<List<string>> { };
            List<string>  rows = new List<string>();
            rows = new List<string>(BCPBuffer.GetInstance().GetCachePreviewTable(dbItem, maxNumOfFile).Split(OpUtil.LineSeparator));
            for (int i = 0; i < Math.Min(rows.Count, maxNumOfFile); i++)
                datas.Add(new List<string>(rows[i].TrimEnd('\r').Split(OpUtil.TabSeparator)));                                                 // TODO 没考虑到分隔符
            FileUtil.FillTable(this.dataGridView, datas, maxNumOfFile);
        }
        private void DataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rect = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y,
                this.dataGridView.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                this.dataGridView.RowHeadersDefaultCellStyle.Font, rect,
                this.dataGridView.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
    }
}
