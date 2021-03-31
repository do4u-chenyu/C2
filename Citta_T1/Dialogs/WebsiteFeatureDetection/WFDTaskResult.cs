using C2.Business.WebsiteFeatureDetection;
using C2.Controls;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
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

        private void WFDTaskResult_Shown(object sender, EventArgs e)
        {
            /* 
             * 判断task是否过期
             *    过期直接弹框不查询，显示上次记录的信息（结果详情为空）
             *    未过期开始查询
             * ？如果task已经是done状态，是否还要再发起一次请求？
             */
            int validityPeriodTime = 86400;
            if (ConvertUtil.TryParseInt(ConvertUtil.TransToUniversalTime(DateTime.Now)) - ConvertUtil.TryParseInt(TaskInfo.TaskCreateTime) > validityPeriodTime)
            {
                HelpUtil.ShowMessageBox("任务已过期，请在下发24小时内获取结果。");
                return;
            }

            string respMsg = string.Empty;
            string datas = string.Empty;
            using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
            {
                if (!WFDWebAPI.GetInstance().QueryTaskResultsById(TaskInfo.TaskID, out respMsg, out datas))
                    return;
            }

            UpdateTaskInfoByResp(respMsg, datas);
            Global.GetWebsiteFeatureDetectionControl().SaveWFDTasksToXml();//状态刷新，修改本地持久化文件
        }

        private void UpdateTaskInfoByResp(string respMsg, string datas)
        {
            if (respMsg == "success")// && TaskInfo.Status != WFDTaskStatus.Done 考虑是否每次都刷新
            {
                TaskInfo.Status = WFDTaskStatus.Done;
                //httpresponse结果会返回一些python的参数，无法被c#正确解析，统一转成字符串
                datas = datas.Replace("None", "'None'").Replace("True", "'True'").Replace("False", "'False'");
                TaskInfo.PreviewResults = DealDatas(TaskInfo.ResultFilePath, datas);
            }
            else if (respMsg == "wait")
                TaskInfo.Status = WFDTaskStatus.Running;
            else if (respMsg == "fail")
                TaskInfo.Status = WFDTaskStatus.Failed;
        }

        private string DealDatas(string resultFilePath, string results)
        {
            StringBuilder sb = new StringBuilder(1024 * 16);
            //TODO 解析正确结果，同时写进本地文件，返回预览字符串
            //List<string> dataList = JsonConvert.DeserializeObject<List<string>>(datas);
            StreamWriter sw = new StreamWriter(resultFilePath);

            List<WFDResult> resultList = new JavaScriptSerializer().Deserialize<List<WFDResult>>(results);
            foreach (WFDResult result in resultList)
            {
                sb.Append(result.url).Append(OpUtil.TabSeparator)
                  .Append(result.prediction).Append(OpUtil.TabSeparator)
                  .Append(result.title).Append(OpUtil.TabSeparator)
                  .Append(result.screen_shot).Append(OpUtil.TabSeparator)
                  .Append(result.html_content).Append(OpUtil.LineSeparator);

                sw.WriteLine(result.JoinAllContent());
            }

            sw.Close();
            sw.Dispose();
            return sb.ToString().TrimEnd(OpUtil.LineSeparator);
        }
    }
}
