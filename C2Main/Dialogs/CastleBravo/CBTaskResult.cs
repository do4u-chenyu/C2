using C2.Business.CastleBravo;
using C2.Controls;
using C2.Core;
using C2.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace C2.Dialogs.CastleBravo
{
    partial class CBTaskResult : BaseDialog
    {
        private readonly CastleBravoTaskInfo TaskInfo;

        public CBTaskResult()
        {
            InitializeComponent();
            this.dataGridView.DoubleBuffered(true);
        }

        public CBTaskResult(CastleBravoTaskInfo taskInfo) : this()
        {
            TaskInfo = taskInfo;

            this.taskNameLabel.Text = taskInfo.TaskName;
            this.taskIdLabel.Text = taskInfo.TaskID;
            this.taskStatusLabel.Text = taskInfo.Status.ToString();
        }

        private void CBTaskResult_Shown(object sender, EventArgs e)
        {
            if (TaskInfo.Status == CastleBravoTaskStatus.Done && ShownResults())
                return;

            CastleBravoAPIResponse resp = new CastleBravoAPIResponse();
            using (GuarderUtil.WaitCursor)
                if (!CastleBravoAPI.GetInstance().QueryTaskResultsById(TaskInfo.TaskID, out resp))
                    return;

            UpdateTaskInfoByResp(resp.Message, resp.Data);
            Global.GetWebsiteFeatureDetectionControl().SaveWFDTasksToXml();//状态刷新，修改本地持久化文件
            FillDGV();
        }
        private bool ShownResults()
        {
            if (!File.Exists(TaskInfo.ResultFilePath))
                return false;

            Tuple<List<string>, List<List<string>>> headersAndRows = FileUtil.ReadBcpFile(TaskInfo.ResultFilePath, OpUtil.Encoding.UTF8, OpUtil.TabSeparator, int.MaxValue);
            //列数不对或为空文件，也要重新发起请求
            if (headersAndRows.Item1.Count == 0 || headersAndRows.Item2.Count == 0)
                return false;

            TaskInfo.PreviewResults = TransListToCBResult(headersAndRows);
            FillDGV();
            return true;
        }

        private List<CastleBravoResultOne> TransListToCBResult(Tuple<List<string>, List<List<string>>> headersAndRows)
        {
            List<CastleBravoResultOne> results = new List<CastleBravoResultOne>();

            List<string> headers = headersAndRows.Item1;
            foreach (List<string> content in headersAndRows.Item2)
            {
                if (content.Count < 4)
                    continue;

                results.Add(new CastleBravoResultOne(headers.Zip(content, (k, v) => new {k, v}).ToDictionary(x => x.k, x => x.v)));
            }

            return results;
        }

        private void UpdateTaskInfoByResp(string respMsg, string datas)
        {
            if (respMsg == "Done")
            {
                TaskInfo.Status = CastleBravoTaskStatus.Done;;
                TaskInfo.PreviewResults = DealData(TaskInfo.ResultFilePath, datas);
            }
            else if (respMsg == "Running")
            {
                TaskInfo.Status = CastleBravoTaskStatus.Running;
                TaskInfo.PreviewResults = DealData(TaskInfo.ResultFilePath, datas);
            }
            else if (respMsg == "Error")
            {
                TaskInfo.Status = CastleBravoTaskStatus.Fail;
            }

            this.taskStatusLabel.Text = TaskInfo.Status.ToString();
        }

        private List<CastleBravoResultOne> DealData(string resultFilePath, string apiResults)
        {
            //解析正确结果，同时写进本地文件，返回预览字符串
            List<CastleBravoResultOne> results = new List<CastleBravoResultOne>();
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(resultFilePath);
                sw.WriteLine(CastleBravoResultOne.Columns); //将字段拼成表头

                JArray ja = JArray.Parse(apiResults);
                foreach (JObject jobj in ja)
                {
                    CastleBravoResultOne one = new CastleBravoResultOne(jobj.ToObject<Dictionary<string, string>>());
                    results.Add(one);
                    sw.WriteLine(one.Content);
                }         
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
            List<CastleBravoResultOne> datas = FormatCBResults(TaskInfo.PreviewResults, maxNumOfRow);
            dataGridView.Rows.Clear();

            foreach (CastleBravoResultOne data in datas)
            {
                DataGridViewRow dr = new DataGridViewRow();

                dr.Cells.Add(new DataGridViewTextBoxCell { Value = data.Md5 });
                dr.Cells.Add(new DataGridViewTextBoxCell { Value = data.Model });
                dr.Cells.Add(new DataGridViewTextBoxCell { Value = data.Result });
                dr.Cells.Add(new DataGridViewTextBoxCell { Value = data.Salt });

                dataGridView.Rows.Add(dr);
            }
        }

        private List<CastleBravoResultOne> FormatCBResults(List<CastleBravoResultOne> datas, int maxNumOfRow)
        {
            for (int i = Math.Min(datas.Count, maxNumOfRow); i < maxNumOfRow; i++)
                datas.Add(CastleBravoResultOne.Empty);

            return datas;
        }

        private void BrowserButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(this.TaskInfo.ResultFilePath))
                ProcessUtil.ProcessOpen(this.TaskInfo.ResultFilePath);
            else
                HelpUtil.ShowMessageBox("该文件不存在。", "提示");
        }

    }
}
