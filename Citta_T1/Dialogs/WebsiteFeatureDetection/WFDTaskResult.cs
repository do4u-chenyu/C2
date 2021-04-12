using C2.Business.WebsiteFeatureDetection;
using C2.Controls;
using C2.Core;
using C2.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Dialogs.WebsiteFeatureDetection
{
    partial class WFDTaskResult : BaseDialog
    {
        public WFDTaskInfo TaskInfo;
        private static readonly LogUtil log = LogUtil.GetInstance("WFDTaskResult");
        private bool isDownloading;
        public WFDTaskResult()
        {
            InitializeComponent();
            this.dataGridView.DoubleBuffered(true);
            this.isDownloading = false;
        }

        public WFDTaskResult(WFDTaskInfo taskInfo) : this()
        {
            TaskInfo = taskInfo;

            this.taskNameLabel.Text = taskInfo.TaskName;
            this.taskIDLabel.Text = taskInfo.TaskID;
            this.taskStatusLabel.Text = taskInfo.Status.ToString();

        }

        #region 加载结果
        /* 
         *  1、打开结果窗口，窗口加载完成时开始2
         *  2、任务状态是否done?
         *      done ： 读本地文件所有内容到 List<List<string>> results = 内存， 前100行加载到table
         *      其他 ： 跳转3
         *  3、是否过期？
         *      是，提示，返回，此时窗体显示基本属性，详细信息为空；
         *      否，发起请求
         *  4、发起请求
         *      成功 ： 读返回报文内容到 List<List<string>> results = 内存， 前100行加载到table ， 状态刷新 ， 写入本地文件
         *      其他 ： 状态刷新
         */
        private void WFDTaskResult_Shown(object sender, EventArgs e)
        {
            if (TaskInfo.Status == WFDTaskStatus.Done && File.Exists(TaskInfo.ResultFilePath) && LoadLocalResultsFillTable())
                return;

            int validityPeriodTime = 86400;
            if (ConvertUtil.TryParseInt(ConvertUtil.TransToUniversalTime(DateTime.Now)) - ConvertUtil.TryParseInt(TaskInfo.TaskCreateTime) > validityPeriodTime)
            {
                HelpUtil.ShowMessageBox("任务已过期，请在下发24小时内获取结果。");
                return;
            }

            WFDAPIResult result = new WFDAPIResult();
            using (new GuarderUtil.CursorGuarder())
            {
                if (!WFDWebAPI.GetInstance().QueryTaskResultsById(TaskInfo.TaskID, out result))
                    return;
            }

            UpdateTaskInfoByResp(result.RespMsg, result.Datas);
            Global.GetWebsiteFeatureDetectionControl().SaveWFDTasksToXml();//状态刷新，修改本地持久化文件
            FillDGV();
        }
        private bool LoadLocalResultsFillTable()
        {
            Tuple<List<string>, List<List<string>>> headersAndRows = FileUtil.ReadBcpFile(TaskInfo.ResultFilePath, OpUtil.Encoding.UTF8, OpUtil.TabSeparator, int.MaxValue);
            //列数不对或为空文件，也要重新发起请求
            if (headersAndRows.Item1.Count == 0 || headersAndRows.Item2.Count == 0)
                return false;
            TaskInfo.PreviewResults = TransListToWFDResult(headersAndRows);

            FillDGV();
            return true;
        }

        private List<WFDResult> TransListToWFDResult(Tuple<List<string>, List<List<string>>> headersAndRows)
        {
            List<WFDResult> results = new List<WFDResult>();

            List<string> colList = headersAndRows.Item1;
            foreach(List<string> content in headersAndRows.Item2)
            {
                if (content.Count < 10)
                    continue;

                results.Add(new WFDResult( colList.Zip(content, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v) ));
            }

            return results;
        }
        
        private void UpdateTaskInfoByResp(string respMsg, string datas)
        {
            if (respMsg == "success")// && TaskInfo.Status != WFDTaskStatus.Done 考虑是否每次都刷新
            {
                TaskInfo.Status = WFDTaskStatus.Done;
                datas = datas.Replace("None", "''").Replace("True", "'True'").Replace("False", "'False'").Replace("''''","''");
                TaskInfo.PreviewResults = DealData(TaskInfo.ResultFilePath, datas);
            }
            else if (respMsg == "wait")
                TaskInfo.Status = WFDTaskStatus.Running;
            else if (respMsg == "fail")
                TaskInfo.Status = WFDTaskStatus.Failed;
            this.taskStatusLabel.Text = TaskInfo.Status.ToString();
        }

        private List<WFDResult> DealData(string resultFilePath, string apiResults)
        {
            //解析正确结果，同时写进本地文件，返回预览字符串
            List<WFDResult> results = new List<WFDResult>();
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(resultFilePath);
                JArray json = JArray.Parse(apiResults);
                foreach (JObject item in json)
                {
                    results.Add(new WFDResult(item.ToObject<Dictionary<string, string>>()));
                }

                if(results.Count > 0)
                    sw.WriteLine(results[0].AllCol);//将字段拼成表头
                foreach (WFDResult result in results)
                    sw.WriteLine(result.AllContent);
            }
            catch { }
            finally
            {
                if (sw != null)
                    sw.Close();
            }

            return results;
        }

        public void FillDGV(int maxNumOfRow = 100)
        {
            //TODO 看看有没有其他赋值方式
            List<WFDResult> datas = FormatWFDResults(TaskInfo.PreviewResults, maxNumOfRow);
            dataGridView.Rows.Clear();

            foreach (WFDResult data in datas)
            {
                DataGridViewRow dr = new DataGridViewRow();

                DataGridViewTextBoxCell textCell0 = new DataGridViewTextBoxCell();
                textCell0.Value = data.url;
                dr.Cells.Add(textCell0);

                DataGridViewTextBoxCell textCell1 = new DataGridViewTextBoxCell();
                textCell1.Value = data.prediction_;
                dr.Cells.Add(textCell1);

                DataGridViewTextBoxCell textCell2 = new DataGridViewTextBoxCell();
                textCell2.Value = data.title;
                dr.Cells.Add(textCell2);

                if(string.IsNullOrEmpty(data.screen_shot))
                {
                    DataGridViewTextBoxCell textCell3 = new DataGridViewTextBoxCell();
                    textCell3.Value = string.Empty;
                    dr.Cells.Add(textCell3);
                }
                else
                {
                    DataGridViewLinkCell link = new DataGridViewLinkCell();
                    link.Value = "下载截图";
                    link.Tag = data.screen_shot;
                    dr.Cells.Add(link);
                }

                DataGridViewTextBoxCell textCell4 = new DataGridViewTextBoxCell();
                textCell4.Value = data.html_content;
                dr.Cells.Add(textCell4);

                dataGridView.Rows.Add(dr);
            }
        }

        private List<WFDResult> FormatWFDResults(List<WFDResult> datas, int maxNumOfRow)
        {
            List<WFDResult> results = new List<WFDResult>();

            WFDResult blankRow = new WFDResult();//TODO 可能有坑，空结果类不会赋值，同一引用应该不会有问题

            for (int i = 0; i < maxNumOfRow; i++)
            {
                if (i >= datas.Count)
                    results.Add(blankRow);
                else
                    results.Add(datas[i]);
            }

            return results;
        }
        #endregion

        #region 下载截图
        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView.Columns[e.ColumnIndex] is DataGridViewLinkColumn && e.RowIndex > -1 && dataGridView.CurrentCell is DataGridViewLinkCell)
            {
                DataGridViewLinkCell cell = (DataGridViewLinkCell)dataGridView.CurrentCell;
                if (cell.Tag == null)
                    return;
                else if (isDownloading)
                {
                    HelpUtil.ShowMessageBox("当前有截图下载任务，请在下载结束后重试。");
                    return;
                }

                SaveScreenshotsToLocal(new List<WFDResult>() { TaskInfo.PreviewResults[e.RowIndex] });
            }
        }

        private async void SaveScreenshotsToLocal(List<WFDResult> results)
        {
            if (!WFDWebAPI.GetInstance().ReAuthBeforeQuery())
                return;

            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            string destPath = dialog.SelectedPath;

            progressNum.Text = "0%";
            progressInfo.Text = "已完成0张，失败0张。";
            progressBar1.Value = 0;
            progressBar1.Maximum = results.Count;
            progressBar1.Step = 1;
            //progressBar1.Step = (int)Math.Floor((decimal)100 / results.Count);

            await SavePicAndUpdateProgress(destPath, results);
        }

        private async Task SavePicAndUpdateProgress(string destPath, List<WFDResult> results)
        {
            int doneNum = 0;
            int errorNum = 0;
            string finMsg = string.Empty;
            isDownloading = true;

            string[] files = Directory.GetFiles(destPath);

            foreach (WFDResult result in results)
            {
                progressBar1.Value += progressBar1.Step;
                progressNum.Text = (progressBar1.Value * 100 / progressBar1.Maximum).ToString() + "%";
                //progressNum.Text = progressBar1.Value.ToString() + "%";

                string picUrl = result.url.Replace("http://", "").Replace("https://", "").Split('/')[0];
                if (files._Contains(picUrl))//跳过已存在的文件
                    continue;

                WFDAPIResult APIResult = await WFDWebAPI.GetInstance().DownloadScreenshotById(result.screen_shot);
                if (APIResult.RespMsg == "success" && Base64StringToImage(Path.Combine(destPath, string.Format("{0}_{1}.png", result.prediction_, picUrl)), APIResult.Datas))
                    doneNum++;
                else
                    errorNum++;
                    //HelpUtil.ShowMessageBox(APIResult.RespMsg);
                
                progressInfo.Text = string.Format("已完成{0}张，失败{1}张。", doneNum, errorNum);
                finMsg = APIResult.RespMsg;
            }

            progressBar1.Value = progressBar1.Maximum;
            progressNum.Text = "100%";
            isDownloading = false;
            //单个网站下载时，弹窗提示错误信息
            if (results.Count > 1 || finMsg == "success")
                HelpUtil.ShowMessageBox("网站截图下载完毕");
            else
                HelpUtil.ShowMessageBox(finMsg);
        }

        private bool Base64StringToImage(string txtFileName, string base64)
        {
            try
            {
                byte[] arr = Convert.FromBase64String(base64);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);
                ms.Close();
                bmp.Save(txtFileName, ImageFormat.Png);
                return true;
            }
            catch
            {
                log.Error(txtFileName + "生成图片失败。" + "base64为：" + base64);
                return false;
            }
        }

        private void DownloadPicsButton_Click(object sender, EventArgs e)
        {
            if (isDownloading)
            {
                HelpUtil.ShowMessageBox("当前有截图下载任务，请在下载结束后重试。");
                return;
            }
            if (TaskInfo.Status != WFDTaskStatus.Done)
            {
                HelpUtil.ShowMessageBox("任务还在执行中，请任务完成后重试。");
                return;
            }
            SaveScreenshotsToLocal(TaskInfo.PreviewResults.FindAll(t => !string.IsNullOrEmpty(t.screen_shot)));
        }
        #endregion
        
        private void BrowserButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(this.TaskInfo.ResultFilePath))
                ProcessUtil.ProcessOpen(this.TaskInfo.ResultFilePath);
            else
                HelpUtil.ShowMessageBox("该文件不存在。", "提示");
        }

        private bool CanFormClose()
        {
            if (isDownloading)
            {
                DialogResult result = MessageBox.Show("当前有截图下载任务，是否中止下载？", "关闭窗口", MessageBoxButtons.OKCancel);
                if (result == DialogResult.Cancel)
                    return false;
            }
            return true;
        }

        private void WFDTaskResult_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.cancel为true表示取消关闭，为false表示可以关闭窗口
            e.Cancel = !CanFormClose();
        }
    }
}
