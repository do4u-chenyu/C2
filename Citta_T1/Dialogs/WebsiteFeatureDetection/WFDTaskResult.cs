using C2.Business.WebsiteFeatureDetection;
using C2.Controls;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace C2.Dialogs.WebsiteFeatureDetection
{
    partial class WFDTaskResult : StandardDialog
    {
        public WFDTaskInfo TaskInfo;
        private static readonly LogUtil log = LogUtil.GetInstance("WFDTaskResult");
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

                if(data[3] == "None")
                {
                    DataGridViewTextBoxCell textCell3 = new DataGridViewTextBoxCell();
                    textCell3.Value = "None";
                    dr.Cells.Add(textCell3);
                }
                else
                {
                    DataGridViewButtonCell button = new DataGridViewButtonCell();
                    button.Value = "下载截图";
                    button.Tag = data[3];
                    dr.Cells.Add(button);
                }

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
            if(dataGridView.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex > -1 && dataGridView.CurrentCell is DataGridViewButtonCell)
            {
                DataGridViewButtonCell cell = (DataGridViewButtonCell)dataGridView.CurrentCell;
                if (cell.Tag == null)
                    return;

                SaveScreenshotsToLocal(new List<string>() { cell.Tag.ToString() });
            }
        }

        private void SaveScreenshotsToLocal(List<string> screenshotIds)
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            string destPath = dialog.SelectedPath;
            foreach(string id in screenshotIds)
            {
                WFDWebAPI.GetInstance().DownloadScreenshotById(id, out string respMsg, out string datas);
                if (respMsg == "success")
                    Base64StringToImage(Path.Combine(destPath, id + ".png"), datas);
            }
            HelpUtil.ShowMessageBox("网站截图下载完毕");
        }

        private void Base64StringToImage(string txtFileName, string base64)
        {
            try
            {
                byte[] arr = Convert.FromBase64String(base64);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);
                ms.Close();
                bmp.Save(txtFileName, ImageFormat.Png);
            }
            catch
            {
                log.Error(txtFileName + "生成图片失败。");
            }
        }

        private void DownloadPicsButton_Click(object sender, EventArgs e)
        {
            List<string> screenshotIds = new List<string>();

            if (File.Exists(this.TaskInfo.ResultFilePath))
            {
                //TODO 读本地文件，获取id和分类结果两列
                SaveScreenshotsToLocal(screenshotIds);
            }
            else
                HelpUtil.ShowMessageBox("任务结果文件不存在。", "提示");
        }
    }
}
