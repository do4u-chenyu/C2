using C2.Business.CastleBravo;
using C2.Controls;
using C2.Core;
using C2.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
            if (TaskInfo.Status == CastleBravoTaskStatus.Done)
            {
                ShowResults();
                return;
            }
               
            CastleBravoAPIResponse resp = new CastleBravoAPIResponse();
            using (GuarderUtil.WaitCursor)
                resp = CastleBravoAPI.GetInstance().QueryTaskResultsById(TaskInfo.TaskID);    

            if (resp.StatusCode != HttpStatusCode.OK)
            {
                HelpUtil.ShowMessageBox(String.Format("访问服务器出现错误, 错误码 : [{0}],  详细 : [{1}]", 
                    resp.StatusCode, 
                    resp.Message), 
                    "提示");
                return;
            }

            UpdateTaskInfo(resp.Message, resp.Data);
            Global.GetCastleBravoControl().Save();//状态刷新，修改本地持久化文件
            FillDGV();
        }
        private bool ShowResults()
        {
            if (!File.Exists(TaskInfo.ResultFilePath))
                return false;

            Tuple<List<string>, List<List<string>>> headersAndRows = FileUtil.ReadBcpFile(TaskInfo.ResultFilePath, OpUtil.Encoding.UTF8, OpUtil.TabSeparator, int.MaxValue);
            TaskInfo.PreviewResults = TransListToCBResult(headersAndRows);
            
            UpdateTaskStatusLabel();
            FillDGV();
            return true;
        }

        private void UpdateTaskStatusLabel()
        {

            this.taskStatusLabel.Text = String.Format("[{0}]     成功率 : {1}/{2} = {3:0.00%}", 
                TaskInfo.Status, 
                TaskInfo.PreviewResults.Count, 
                TaskInfo.TaskCount,
                TaskInfo.PreviewResults.Count / Math.Max(1.0, ConvertUtil.TryParseDouble(TaskInfo.TaskCount)));
        }

        private List<CastleBravoResultOne> TransListToCBResult(Tuple<List<string>, List<List<string>>> headersAndRows)
        {
            List<CastleBravoResultOne> results = new List<CastleBravoResultOne>();
            List<string> headers = headersAndRows.Item1;
            foreach (List<string> content in headersAndRows.Item2)
            {
                if (content.Count < CastleBravoResultOne.ColumnsCount)
                    continue;

                results.Add(new CastleBravoResultOne(headers.Zip(content, (k, v) => new {k, v}).ToDictionary(x => x.k, x => x.v)));
            }

            return results;
        }

        private void UpdateTaskInfo(string statusMsg, string datas)
        {
            if (statusMsg == "Error")
            {
                TaskInfo.Status = CastleBravoTaskStatus.Fail;
                this.taskStatusLabel.Text = "Fail";
                return;
            }

            if (statusMsg == "Done")
                TaskInfo.Status = CastleBravoTaskStatus.Done;
            else if (statusMsg == "Running")
                TaskInfo.Status = CastleBravoTaskStatus.Running;

            TaskInfo.PreviewResults = DealData(TaskInfo.ResultFilePath, datas);
            UpdateTaskStatusLabel();
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
                    results.Add(new CastleBravoResultOne(jobj.ToObject<Dictionary<string, string>>()));
                
                // Linq 去重
                results = results.Where((x, i) => results.FindIndex(z => z.Content == x.Content) == i).ToList();
                
                foreach (var one in results)
                    sw.WriteLine(one.Content); 
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

                dr.Cells.Add(new DataGridViewTextBoxCell { Value = data.MD5 });
                dr.Cells.Add(new DataGridViewTextBoxCell { Value = CastleBravoTaskInfo.Model(data.Model) });
                dr.Cells.Add(new DataGridViewTextBoxCell { Value = data.Result });
                dr.Cells.Add(new DataGridViewTextBoxCell { Value = CastleBravoTaskInfo.Salt(data.Model) });

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
