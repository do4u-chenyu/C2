using C2.Business.WebsiteFeatureDetection;
using C2.Controls;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace C2.Dialogs.WebsiteFeatureDetection
{
    partial class WFDTaskResult : StandardDialog
    {
        public WFDTaskInfo TaskInfo;
        
        public WFDTaskResult()
        {
            InitializeComponent();
            this.dataGridView.DoubleBuffered(true);
        }

        public WFDTaskResult(WFDTaskInfo taskInfo) : this()
        {
            TaskInfo = taskInfo;

            this.taskNameLabel.Text = taskInfo.TaskName;
            this.taskIDLabel.Text = taskInfo.TaskID;
            this.taskStatusLabel.Text = taskInfo.Status.ToString();
            RefreshDGV();
        }

        private void RefreshDGV()
        {
            List<List<string>> tableCols = new List<List<string>>();
            //tableCols = GenDefaultContent();
            if (!string.IsNullOrEmpty(TaskInfo.PreviewResults))
                tableCols.AddRange(DbUtil.StringTo2DString(TaskInfo.PreviewResults));
            FillTable(tableCols);
            ResetColumnsWidth();
        }


        public void FillTable(List<List<string>> datas, int maxNumOfRow = 100)
        {
            //TODO 看看有没有其他赋值方式
            //datas = FileUtil.FormatDatas(datas, maxNumOfRow);
            dataGridView.Rows.Clear();

            foreach (List<string> data in datas)
            {
                DataGridViewRow dr = new DataGridViewRow();

                DataGridViewTextBoxCell textCell0 = new DataGridViewTextBoxCell();
                textCell0.Value = data[0];
                dr.Cells.Add(textCell0);

                DataGridViewTextBoxCell textCell1 = new DataGridViewTextBoxCell();
                textCell1.Value = data[1];
                dr.Cells.Add(textCell1);

                DataGridViewTextBoxCell textCell2 = new DataGridViewTextBoxCell();
                textCell2.Value = data[2];
                dr.Cells.Add(textCell2);

                DataGridViewButtonCell button = new DataGridViewButtonCell();
                button.Value = "下载截图";
                button.Tag = data[3];
                dr.Cells.Add(button);

                DataGridViewTextBoxCell textCell4 = new DataGridViewTextBoxCell();
                textCell4.Value = data[4];
                dr.Cells.Add(textCell4);

                dataGridView.Rows.Add(dr);
            }
        }


        private void ResetColumnsWidth()
        {
            if (dataGridView.Columns.Count != 5)
                return;

            dataGridView.Columns[0].FillWeight = 50;  // URL列宽一些，其他列短一些
            dataGridView.Columns[1].FillWeight = 15;  
            dataGridView.Columns[2].FillWeight = 20;
            dataGridView.Columns[3].FillWeight = 15;
            dataGridView.Columns[4].FillWeight = 55;
        }

        private void BrowserButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(this.TaskInfo.ResultFilePath))
                ProcessUtil.ProcessOpen(this.TaskInfo.ResultFilePath);
            else
                HelpUtil.ShowMessageBox("该文件不存在。", "提示");
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex > -1)
            {
                DataGridViewButtonCell cell = (DataGridViewButtonCell)dataGridView.CurrentCell;
                if(cell.Tag != null)
                    HelpUtil.ShowMessageBox(cell.Tag.ToString());
            }

        }

        private void DownloadPicsButton_Click(object sender, EventArgs e)
        {

        }
    }
}
