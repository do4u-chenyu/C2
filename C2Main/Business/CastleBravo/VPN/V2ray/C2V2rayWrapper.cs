using C2.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using v2rayN.Handler;

namespace C2.Business.CastleBravo.VPN.V2ray
{

    /**
     * 大部分代码是从v2rayN中移植过来的
     */
    class C2V2rayWrapper
    {
        private static string GetRealPingTime(string url, WebProxy webProxy, out int responseTime)
        {
            string msg = string.Empty;
            responseTime = -1;
            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                myHttpWebRequest.Timeout = 5000;
                myHttpWebRequest.Proxy = webProxy;//new WebProxy(Global.Loopback, Global.httpPort);

                Stopwatch timer = new Stopwatch();
                timer.Start();

                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                if (myHttpWebResponse.StatusCode != HttpStatusCode.OK
                    && myHttpWebResponse.StatusCode != HttpStatusCode.NoContent)
                {
                    msg = myHttpWebResponse.StatusDescription;
                }
                timer.Stop();
                responseTime = timer.Elapsed.Milliseconds;

                myHttpWebResponse.Close();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }


        private static bool IsV2raySupport(VPNTaskConfig task)
        {
            bool ret = false;
            switch (task.SSVersion.ToLower())
            {
                case "ss":
                case "vmess":
                case "vless":
                case "trojan":
                    ret = true;
                    break;
            }
             return ret;
        }
        public static void RunRealPing(List<ListViewItem> lv, Action<ListViewItem, string, bool> _updateFunc, Action<ListViewItem> _redrawFunc)
        {
            //  选择端口
            int startPort = v2rayN.Utils.GetAvailablePort();
            startPort = 10911;  // 测试用

            //  构造v2ray配置文件, 并启动v2ray进程
            int pid = new V2rayHandler().LoadV2rayConfigString(lv, startPort);

            //  并发访问代理端口,设置好回调更新函数
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < lv.Count; i++)
            {
                ListViewItem lvi = lv[i];
                VPNTaskConfig vtc = lvi.Tag as VPNTaskConfig;
                if (!IsV2raySupport(vtc))
                {
                    _updateFunc(lvi, string.Format("{0}不支持验活", vtc.SSVersion), false);
                    continue;
                }
                int dummyI = i;

                tasks.Add(Task.Run(() =>
                {
                    try
                    {
                        WebProxy webProxy = new WebProxy(v2rayN.Global.Loopback, startPort + dummyI);
                        int responseTime = -1;

                        // 境外
                        string status0 = GetRealPingTime(v2rayN.Global.AbroadGenerate204, webProxy, out responseTime);
                        string output0 = string.IsNullOrEmpty(status0) ? FormatOut(responseTime, "ms", "境外") : FormatOut(status0, string.Empty, "境外");
                        // 境内
                        string status1 = GetRealPingTime(v2rayN.Global.ChinaGenerate204, webProxy, out responseTime);
                        string output1 = string.IsNullOrEmpty(status1) ? FormatOut(responseTime, "ms", "境内") : FormatOut(status1, string.Empty, "境内");

                        // 境内,境外一个就够
                        bool status = string.IsNullOrEmpty(status0) || string.IsNullOrEmpty(status1);
                        
                        _updateFunc(lvi, output1 + "|" + output0, status);
                    }
                    catch  { }
                }));
            }

            // 这一块是让我彻底写塌方了
            Task[] ts = tasks.ToArray();

            Dictionary<Task, int> indexDict = new Dictionary<Task, int>();
            for (int i = 0; i < tasks.Count; i++)
                indexDict[tasks[i]] = i;

            for (int i = 0; i < lv.Count; i++)
            {
                int index = Task.WaitAny(tasks.ToArray());
                _redrawFunc(lv[indexDict[tasks[index]]]);
                tasks.RemoveAt(index);
            }

            //  Join等待并发结束
            Task.WaitAll(ts);

            // 结束进程
            if (pid > 0) V2rayHandler.V2rayStopPid(pid);
        }

        private static string FormatOut(object time, string unit, string prefix)
        {
            if (time.ToString().Equals("-1"))
            {
                return "Timeout";
            }
            return string.Format("{2}:{0}{1}", time, unit, prefix).PadLeft(6, OpUtil.Blank);
        }
    }
}
