using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using v2rayN.Handler;

namespace C2.Business.CastleBravo.VPN.V2ray
{
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
            return false;
        }
        public static void RunRealPing(List<ListViewItem> lv)
        {
            int pid = -1;
            _ = lv;

            //  选择端口
            int startPort = v2rayN.Utils.GetAvailablePort();
            _ = startPort;

            //  构造v2ray配置文件, 并启动v2ray进程
            pid = new V2rayHandler().LoadV2rayConfigString(lv);
            _ = pid;

            List<Task> tasks = new List<Task>();

            foreach (ListViewItem lvi in lv)
            {
                if (IsV2raySupport(lvi.Tag as VPNTaskConfig))
                    continue;

                tasks.Add(Task.Run(() =>
                {
                    try
                    {
                        WebProxy webProxy = new WebProxy(v2rayN.Global.Loopback, startPort);
                        int responseTime = -1;
                        string status = GetRealPingTime(v2rayN.Global.SpeedPingTestUrl, webProxy, out responseTime);
                        //string output = status.IsNullOrEmpty() ? FormatOut(responseTime, "ms") : FormatOut(status, "");
                        //_updateFunc(itemIndex, output);
                    }
                    catch 
                    {
                        //Utils.SaveLog(ex.Message, ex);
                    }
                }));

            }

            //  Join等待并发结束
            Task.WaitAll(tasks.ToArray());

            // 结束进程
            if (pid > 0) V2rayHandler.V2rayStopPid(pid);

            //  并发访问代理端口,设置好回调更新函数

        }
    }
}
