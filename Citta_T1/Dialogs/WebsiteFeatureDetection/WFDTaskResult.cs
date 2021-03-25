using C2.Business.WebsiteFeatureDetection;
using C2.Controls;
using C2.Controls.C1.Left;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Dialogs.WebsiteFeatureDetection
{
    partial class WFDTaskResult : StandardDialog
    {
        public string UrlResults;
        public WebsiteFeatureDetectionTaskInfo TaskInfo;
        
        public WFDTaskResult()
        {
            InitializeComponent();
            this.dataGridView.DoubleBuffered(true);
        }

        public WFDTaskResult(WebsiteFeatureDetectionTaskInfo taskInfo, string results) : this()
        {
            TaskInfo = taskInfo;
            UrlResults = results;

            this.taskNameLabel.Text = taskInfo.TaskName;
            this.taskIdLabel.Text = taskInfo.TaskId;
            this.statusLabel.Text = taskInfo.Status.ToString();
            RefreshDGV();
        }

        private void RefreshDGV()
        {
            List<List<string>> tableCols;
            if (string.IsNullOrEmpty(UrlResults))
                tableCols = GenDefaultContent();
            else
                tableCols = DbUtil.StringTo2DString(UrlResults);
            FileUtil.FillTable(dataGridView, tableCols);
        }

        private List<List<string>> GenDefaultContent()
        {
            List<List<string>> datas = new List<List<string>>
            {
                new List<string>() { "url", "查询状态", "分类情况", "网站截图"}
            };

            for (int i = 0; i < 7; i++)
                datas.Add(new List<string>() { "", "", "", ""});

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
