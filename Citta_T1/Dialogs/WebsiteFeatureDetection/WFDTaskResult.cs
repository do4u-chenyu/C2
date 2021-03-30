using C2.Business.WebsiteFeatureDetection;
using C2.Controls;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;

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
            List<List<string>> tableCols;
            tableCols = GenDefaultContent();
            if (!string.IsNullOrEmpty(TaskInfo.PreviewResults))
                tableCols.AddRange(DbUtil.StringTo2DString(TaskInfo.PreviewResults));
            FileUtil.FillTable(dataGridView, tableCols);
            ResetColumnsWidth();
        }
        private void ResetColumnsWidth()
        {
            if (dataGridView.Columns.Count != 4)
                return;

            dataGridView.Columns[0].FillWeight = 50;  // URL列宽一些，其他列短一些
            dataGridView.Columns[1].FillWeight = 15;  
            dataGridView.Columns[2].FillWeight = 20;
            dataGridView.Columns[3].FillWeight = 15;
        }

        private List<List<string>> GenDefaultContent()
        {
            List<List<string>> datas = new List<List<string>>
            {
                new List<string>() { "url", "查询状态", "分类情况", "网站截图" }
            };

            return datas;
        }
        private void BrowserButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(this.TaskInfo.ResultFilePath))
                ProcessUtil.ProcessOpen(this.TaskInfo.ResultFilePath);
            else
                HelpUtil.ShowMessageBox("该文件不存在.", "提示");
        }
    }
}
