using C2.Business.CastleBravo;
using C2.Controls;
using C2.Core;
using C2.Utils;
using Newtonsoft.Json.Linq;
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

namespace C2.Dialogs.CastleBravo
{
    partial class CBTaskResult : BaseDialog
    {
        public CastleBravoTaskInfo TaskInfo;

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
            if (TaskInfo.Status == CastleBravoTaskStatus.Done && File.Exists(TaskInfo.ResultFilePath) && LoadLocalResultsFillTable())
                return;

            CastleBravoAPIResult result = new CastleBravoAPIResult();
            using (new GuarderUtil.CursorGuarder())
            {
                if (!CastleBravoAPI.GetInstance().QueryTaskResultsById(TaskInfo.TaskID, out result))
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
            TaskInfo.PreviewResults = TransListToCBResult(headersAndRows);

            FillDGV();
            return true;
        }

        private List<CastleBravoResult> TransListToCBResult(Tuple<List<string>, List<List<string>>> headersAndRows)
        {
            List<CastleBravoResult> results = new List<CastleBravoResult>();

            List<string> colList = headersAndRows.Item1;
            foreach (List<string> content in headersAndRows.Item2)
            {
                if (content.Count < 4)
                    continue;

                results.Add(new CastleBravoResult(colList.Zip(content, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v)));
            }

            return results;
        }

        private void UpdateTaskInfoByResp(string respMsg, string datas)
        {
            if (respMsg == "Done")// && TaskInfo.Status != WFDTaskStatus.Done 考虑是否每次都刷新
            {
                TaskInfo.Status = CastleBravoTaskStatus.Done;
                //datas = datas.Replace("None", "''").Replace("True", "'True'").Replace("False", "'False'").Replace("''''", "''");
                TaskInfo.PreviewResults = DealData(TaskInfo.ResultFilePath, datas);
            }
            else if (respMsg == "Running")
            {
                TaskInfo.Status = CastleBravoTaskStatus.Running;
                TaskInfo.PreviewResults = DealData(TaskInfo.ResultFilePath, datas);
            }
            else if (respMsg == "Error")
            {
                TaskInfo.Status = CastleBravoTaskStatus.Failed;
            }

            this.taskStatusLabel.Text = TaskInfo.Status.ToString();
        }

        private List<CastleBravoResult> DealData(string resultFilePath, string apiResults)
        {
            //解析正确结果，同时写进本地文件，返回预览字符串
            List<CastleBravoResult> results = new List<CastleBravoResult>();
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(resultFilePath);
                JArray json = JArray.Parse(apiResults);
                foreach (JObject item in json)
                {
                    results.Add(new CastleBravoResult(item.ToObject<Dictionary<string, string>>()));
                }

                if (results.Count > 0)
                    sw.WriteLine(results[0].AllCol);//将字段拼成表头
                foreach (CastleBravoResult result in results)
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
            List<CastleBravoResult> datas = FormatWFDResults(TaskInfo.PreviewResults, maxNumOfRow);
            dataGridView.Rows.Clear();

            foreach (CastleBravoResult data in datas)
            {
                DataGridViewRow dr = new DataGridViewRow();

                DataGridViewTextBoxCell textCell0 = new DataGridViewTextBoxCell();
                textCell0.Value = data.md5;
                dr.Cells.Add(textCell0);

                DataGridViewTextBoxCell textCell1 = new DataGridViewTextBoxCell();
                textCell1.Value = data.type;
                dr.Cells.Add(textCell1);

                DataGridViewTextBoxCell textCell2 = new DataGridViewTextBoxCell();
                textCell2.Value = data.trans;
                dr.Cells.Add(textCell2);

                DataGridViewTextBoxCell textCell3 = new DataGridViewTextBoxCell();
                textCell3.Value = data.salt;
                dr.Cells.Add(textCell3);

                dataGridView.Rows.Add(dr);
            }
        }

        private List<CastleBravoResult> FormatWFDResults(List<CastleBravoResult> datas, int maxNumOfRow)
        {
            List<CastleBravoResult> results = new List<CastleBravoResult>();

            CastleBravoResult blankRow = new CastleBravoResult();//TODO 可能有坑，空结果类不会赋值，同一引用应该不会有问题

            for (int i = 0; i < maxNumOfRow; i++)
            {
                if (i >= datas.Count)
                    results.Add(blankRow);
                else
                    results.Add(datas[i]);
            }

            return results;
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
