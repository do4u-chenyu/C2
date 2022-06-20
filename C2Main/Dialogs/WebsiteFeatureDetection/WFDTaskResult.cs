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
        private bool needStop;

        private string lastErrorMessage;
        public WFDTaskResult()
        {
            InitializeComponent();
            this.dataGridView.DoubleBuffered(true);
            this.isDownloading = false;
            this.needStop = false;
            this.lastErrorMessage = string.Empty;
        }

        public WFDTaskResult(WFDTaskInfo taskInfo) : this()
        {
            TaskInfo = taskInfo;

            this.taskNameLabel.Text = taskInfo.TaskName;
            this.taskIDLabel.Text = taskInfo.TaskID;
            this.taskStatusLabel.Text = taskInfo.Status.ToString();
            this.screenShotGroupBox.Visible = taskInfo.Status == WFDTaskStatus.Done;//任务完成才显示截图下载控件
            this.downloadPicsButton.Visible = taskInfo.Status == WFDTaskStatus.Done;//任务完成才显示截图下载控件
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

            if (TaskInfo.IsOverTime())
            {
                HelpUtil.ShowMessageBox("任务已过期，请根据任务ID获取任务结果。");//24小时内获取任务结果
                return;
            }

            WFDAPIResult result = new WFDAPIResult();
            using (new GuarderUtil.CursorGuarder())
            {
                if (!WFDWebAPI.GetInstance().QueryTaskResultsById(TaskInfo.TaskID, out result))
                    return;
            }

            UpdateTaskInfoByResp(result);
            Global.GetWebsiteFeatureDetectionControl().Save();//状态刷新，修改本地持久化文件
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

        private string FormatProgressString(string str)
        {
            
            float val = ConvertUtil.TryParseFloat(str);
            if (float.IsNaN(val))
                return str;
            return string.Format("{0:F}%", val * 100);
        }
        
        private void UpdateTaskInfoByResp(WFDAPIResult result)
        {
            string respMsg = result.RespMsg;
            string datas = result.Datas;
            string taskInfo = result.TaskInfo;

            if (respMsg == "success")// && TaskInfo.Status != WFDTaskStatus.Done 考虑是否每次都刷新
            {
                TaskInfo.Status = WFDTaskStatus.Done;
                datas = datas.Replace("None", "''").Replace("True", "'True'").Replace("False", "'False'").Replace("''''","''");
                TaskInfo.PreviewResults = DealData(TaskInfo.ResultFilePath, datas);
                this.statusInfoLabel.Text = string.Empty;
                this.taskInfoLabel.Text = string.Empty;
            }
            else if (respMsg == "wait") 
            {
                TaskInfo.Status = WFDTaskStatus.Running;
                this.statusInfoLabel.Text = "[已处理:" + FormatProgressString(datas.Replace("\"", string.Empty)
                                                                                   .Replace("{",  string.Empty)
                                                                                   .Replace("}",  string.Empty)) + "]";
                // 改这段的时候,莫名其妙的难过
                this.taskInfoLabel.Text = "[" + taskInfo.Replace("\'", string.Empty)
                                                        .Replace("{",  string.Empty)
                                                        .Replace("}",  string.Empty)
                                                        .Replace("ahead_task", "排队数")
                                                        .Replace("will_finished", "预计等待") + "分钟]";
            }
            else if (respMsg == "fail")
            {
                TaskInfo.Status = WFDTaskStatus.Failed;
                this.statusInfoLabel.Text = string.Empty;
                this.taskInfoLabel.Text = string.Empty;
            }
                
            this.taskStatusLabel.Text = TaskInfo.Status.ToString();
            this.screenShotGroupBox.Visible = TaskInfo.Status == WFDTaskStatus.Done;//任务完成才显示截图下载控件
            this.downloadPicsButton.Visible = TaskInfo.Status == WFDTaskStatus.Done;//任务完成才显示截图下载控件
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

        public void FillDGV(int maxNumOfRow = 10000)
        {
            //TODO 看看有没有其他赋值方式
            List<WFDResult> datas = FormatWFDResults(TaskInfo.PreviewResults, maxNumOfRow);
            dataGridView.Rows.Clear();

            foreach (WFDResult data in datas)
            {
                DataGridViewRow dr = new DataGridViewRow();

                dr.Cells.Add(new DataGridViewTextBoxCell { Value = data.url });

                dr.Cells.Add(new DataGridViewTextBoxCell { Value = data.prediction_ });

                dr.Cells.Add(new DataGridViewTextBoxCell { Value = data.title });

                if(string.IsNullOrEmpty(data.screen_shot))
                    dr.Cells.Add(new DataGridViewTextBoxCell { Value = string.Empty });
                else
                    dr.Cells.Add(new DataGridViewLinkCell
                    {
                        Value = "下载截图",
                        Tag = data.screen_shot
                    });

                dr.Cells.Add(new DataGridViewTextBoxCell { Value = data.html_content });

                dr.Cells.Add(new DataGridViewTextBoxCell { Value = data.ip });

                dr.Cells.Add(new DataGridViewTextBoxCell { Value = data.ip_address });

                dataGridView.Rows.Add(dr);
            }
        }

        private List<WFDResult> FormatWFDResults(List<WFDResult> datas, int maxNumOfRow)
        {
            List<WFDResult> results = new List<WFDResult>();

            WFDResult blankRow = new WFDResult();//TODO 可能有坑，空结果类不会赋值，同一引用应该不会有问题

            for (int i = 0; i < Math.Min(maxNumOfRow, datas.Count + 10); i++)
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
            if (results == null || !WFDWebAPI.GetInstance().ReAuthBeforeQuery())
                return;

            var dialog = new FolderBrowserDialog
            {
                SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures)
            };

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
            if(results.Count == 0)//没有可下载的图片，流程还是要走完，提示下载完毕直接退出
            {
                HelpUtil.ShowMessageBox("网站截图下载完毕");
                return;
            }

            int doneNum = 0;
            int errorNum = 0;
            string finMsg = string.Empty;
            isDownloading = true;
            needStop = false;
            lastErrorMessage = string.Empty;

            string[] files = Directory.GetFiles(destPath);

            foreach (WFDResult result in results)
            {
                progressBar1.Value += progressBar1.Step;
                progressNum.Text = (progressBar1.Value * 100 / progressBar1.Maximum).ToString() + "%";
                //progressNum.Text = progressBar1.Value.ToString() + "%";

                string picUrl = result.url.Replace("http://", string.Empty).Replace("https://", string.Empty).Split('/')[0];
                picUrl = picUrl.Replace(":", "_");
                picUrl = Path.Combine(destPath, string.Format("{0}_{1}.png", result.prediction_, picUrl));
                if (files._Contains(picUrl))//跳过已存在的文件
                {
                    doneNum++;
                    finMsg = "success";
                }
                else
                {
                    WFDAPIResult APIResult = await WFDWebAPI.GetInstance().DownloadScreenshotById(result.screen_shot);
                    if (APIResult.RespMsg == "success" && Base64StringToImage(picUrl, APIResult.Datas))
                        doneNum++;
                    else
                        errorNum++;
                    finMsg = APIResult.RespMsg;
                }

                progressInfo.Text = string.Format("已完成{0}张，失败{1}张。", doneNum, errorNum);
                if (needStop)
                    break;
            }

            progressBar1.Value = progressBar1.Maximum;
            progressNum.Text = "100%";
            isDownloading = false;
            needStop = false;
            //单个网站下载时，弹窗提示错误信息
            if (results.Count > 1 || (finMsg == "success" && lastErrorMessage.IsNullOrEmpty()))
                HelpUtil.ShowMessageBox("网站截图下载完毕");
            else
                HelpUtil.ShowMessageBox(lastErrorMessage.IsNullOrEmpty() ? finMsg : lastErrorMessage);
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
            catch (Exception ex)
            {
                lastErrorMessage = txtFileName + "生成图片失败。" + ex.Message;
                log.Error(lastErrorMessage);
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
                HelpUtil.ShowMessageBox("当前有截图下载任务，请在下载结束后重试。");
                return false;
            }
            return true;
        }

        private void WFDTaskResult_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.cancel为true表示取消关闭，为false表示可以关闭窗口
            e.Cancel = !CanFormClose();
        }
        private void TaskIDLabel_MouseDoubleClick(object sender, EventArgs e)
        {
            if (FileUtil.TryClipboardSetText(TaskInfo.TaskID))
                HelpUtil.ShowMessageBox(String.Format("已复制任务ID[{0}]到剪切板", TaskInfo.TaskID));
        }

        private void SFileButton_Click(object sender, EventArgs e)
        {
            ProcessUtil.TryProcessOpen(this.TaskInfo.DatasourceFilePath);
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            needStop = true;
        }
    }
}
