using C2.Business.WebsiteFeatureDetection;
using C2.Controls;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace C2.Dialogs.WebsiteFeatureDetection
{
    partial class WFDTaskResult : StandardDialog
    {
        public WFDTaskInfo TaskInfo;
        private static readonly LogUtil log = LogUtil.GetInstance("WFDTaskResult");

        Dictionary<string, string> predictionCodeDict;

        public WFDTaskResult()
        {
            InitializeComponent();
            this.dataGridView.DoubleBuffered(true);
            InitPredictionCodeDict();
            

        }

        private void InitPredictionCodeDict()
        {
            predictionCodeDict = new Dictionary<string, string>
            {
                {"101090101", "贷款-P2P"},{"101090102", "贷款-抵押"},{"101090103", "贷款-小额"},{"101090104", "贷款-资讯"},{"101090105", "贷款-综合"},{"101090106", "贷款-租赁"},{"1010301", "赌-彩票预测"},{"1010302", "赌-赌场系"},{"1010303","赌-购彩系"},{"1010304", "赌-电子游戏"},{"1010305", "赌-球"},{"1010101", "黄-视频"},{"1010102", "黄-成人用品用药"},{"10111", "签名网站"},{"1010103", "黄-小说漫画"},{"1010104", "黄-性感图"},{"1010105", "黄-直播"},{"101020301", "宗教-场所"},{"101020302", "宗教-机构"},{"101020303", "宗教-文化"},{"101020304", "宗教-用品"},{"1010401", "Vpn-非法"},{"1010402", "Vpn-商务"},{"10106", "打码"},{"10112", "VPS"},{"10107", "短链接"},{"10108", "配资"},{"10105", "镜像"},{"10113", "四方支付"},{"10114", "云发卡"},{"10115", "流量刷单"},{"10116", "微交易"},{"10117", "云呼"},{"10118","CDN"},{"10119","第三方维护助手"},{"101110","广告联盟"},{"101111","代刷"},{"1010106","黄—外围"},{"101114","接码平台"},{"101112","后台登录"},{"101115","机场"},{"101116","政府"},{"101117","学校"},{"101118","医院"},{"101119","虚拟币交易"},{"101121","证券期货交易"},{"101124","网游加速器"},{"101122","外汇交易"},{"101120","游戏交易网站"},{"101123","客服"}
            };
        }

        public WFDTaskResult(WFDTaskInfo taskInfo) : this()
        {
            TaskInfo = taskInfo;

            this.taskNameLabel.Text = taskInfo.TaskName;
            this.taskIDLabel.Text = taskInfo.TaskID;
            this.taskStatusLabel.Text = taskInfo.Status.ToString();

        }

        private void WFDTaskResult_Shown(object sender, EventArgs e)
        {
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
            TaskInfo.PreviewResults = TransListToWFDResult(headersAndRows.Item2);

            FillDGV();
            return true;
        }

        private List<WFDResult> TransListToWFDResult(List<List<string>> contentList)
        {
            List<WFDResult> results = new List<WFDResult>();
            foreach(List<string> content in contentList)
            {
                if (content.Count < 10)
                    continue;
                results.Add(new WFDResult
                {
                    url = content[0],
                    cur_url = content[1],
                    title = content[2],
                    prediction = content[3],
                    prediction_ = content[4],
                    Fraud_label = content[5],
                    screen_shot = content[6],
                    login = content[7],
                    html_content_id = content[8],
                    html_content = content[9],
                });
            }

            return results;
        }

        private void UpdateTaskInfoByResp(string respMsg, string datas)
        {
            if (respMsg == "success")// && TaskInfo.Status != WFDTaskStatus.Done 考虑是否每次都刷新
            {
                TaskInfo.Status = WFDTaskStatus.Done;
                //httpresponse结果会返回一些python的参数，无法被c#正确解析，统一转成字符串
                datas = datas.Replace("None", "'None'").Replace("True", "'True'").Replace("False", "'False'");
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
                sw.WriteLine(new WFDResult().JoinMember());//将字段拼成表头
                results = new JavaScriptSerializer().Deserialize<List<WFDResult>>(apiResults);
                foreach (WFDResult result in results)
                    sw.WriteLine(result.JoinAllContent());
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

            //TODO 目前加载所有的，前100行未实现
            //datas = FileUtil.FormatDatas(datas, maxNumOfRow);
            dataGridView.Rows.Clear();

            foreach (WFDResult data in TaskInfo.PreviewResults)
            {
                DataGridViewRow dr = new DataGridViewRow();

                DataGridViewTextBoxCell textCell0 = new DataGridViewTextBoxCell();
                textCell0.Value = data.url;
                dr.Cells.Add(textCell0);

                DataGridViewTextBoxCell textCell1 = new DataGridViewTextBoxCell();
                textCell1.Value = data.prediction;
                dr.Cells.Add(textCell1);

                DataGridViewTextBoxCell textCell2 = new DataGridViewTextBoxCell();
                textCell2.Value = data.title;
                dr.Cells.Add(textCell2);

                if(data.screen_shot == "None")
                {
                    DataGridViewTextBoxCell textCell3 = new DataGridViewTextBoxCell();
                    textCell3.Value = "None";
                    dr.Cells.Add(textCell3);
                }
                else
                {
                    DataGridViewButtonCell button = new DataGridViewButtonCell();
                    button.Value = "下载截图";
                    button.Tag = data.screen_shot;
                    dr.Cells.Add(button);
                }

                DataGridViewTextBoxCell textCell4 = new DataGridViewTextBoxCell();
                textCell4.Value = data.html_content;
                dr.Cells.Add(textCell4);

                dataGridView.Rows.Add(dr);
            }
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

                SaveScreenshotsToLocal(new List<WFDResult>() { TaskInfo.PreviewResults[e.RowIndex] });
            }
        }

        private void SaveScreenshotsToLocal(List<WFDResult> results)
        {
            if (!WFDWebAPI.GetInstance().ReAuthBeforeQuery())
                return;

            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            string destPath = dialog.SelectedPath;
            string[] files = Directory.GetFiles(destPath);

            foreach (WFDResult result in results)
            {
                string picUrl = result.url.Replace("http://", "").Replace("https://", "").Split('/')[0];
                if (files._Contains(picUrl))//跳过已存在的文件
                    continue;

                WFDWebAPI.GetInstance().DownloadScreenshotById(result.screen_shot, out WFDAPIResult APIResult);
                if (APIResult.RespMsg == "success")
                    Base64StringToImage(Path.Combine(destPath, string.Format("{0}_{1}.png", result.prediction_, picUrl)), APIResult.Datas);
                else
                    HelpUtil.ShowMessageBox(APIResult.RespMsg);
                    
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
                log.Error(txtFileName + "生成图片失败。" + "base64为：" + base64);
            }
        }

        private void DownloadPicsButton_Click(object sender, EventArgs e)
        {
            SaveScreenshotsToLocal(TaskInfo.PreviewResults);
        }
    }
}
