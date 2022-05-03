using Amib.Threading;
using C2.Business.CastleBravo.VPN.Probe;
using C2.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Windows.Forms;
using static C2.Utils.GuarderUtil;

namespace C2.Business.CastleBravo.VPN
{
    
    public partial class VPNMainForm
    {
        private readonly int maxThread = 50;
        List<string> resultList = new List<string>();
        private DateTime s;  // 自动保存
        #region 随机探针
        // 选定项发探针
        // 重新发探针
        // 继续发探针

        private void SendRandomProbe(IList items)
        {
            s = DateTime.Now;
            using (new ToolStripItemTextGuarder(this.actionStatusLabel, "进行中", "已完成"))
                foreach (ListViewItem lvi in items)
                {
                    if (actionNeedStop)
                        break;
                    UpdateOneRandomProbeResult(lvi);
                                          // 5分钟保存一次
                }
        }
        private SmartThreadPool stp = null;
        private void UpdateOneRandomProbeResult(ListViewItem lvi)
        {
            VPNTaskConfig task = lvi.Tag as VPNTaskConfig;
            int port = ConvertUtil.TryParseInt(task.Port.Trim());
            if (port == 0) return;


            this.resultList.Clear();
             stp = new SmartThreadPool
            {
                MaxThreads = maxThread
            };
            foreach (int index in RndProbeConfig.LengthValues)
            {
                for (int i = 0; i < RndProbeConfig.SendCount; i++)
                {
                    string data = GenRndProbeRequest(index);
                    stp.QueueWorkItem<string, int, string>(RndProbeResponse, task.IP, port, data);
                    stp.WaitFor(maxThread);
                }
            }
            stp.WaitForIdle();
            stp.Shutdown();
            lvi.SubItems[8].Text = DealResult(resultList);
        }
        private void RndProbeResponse(string ip, int port, string data)
        {
            Socket s = null;
            string result = string.Empty;
            try
            {
                s = SocketClient.Send(ip, port, data, RndProbeConfig.Timeout * 1000);
                result = SocketClient.Receive(s);

            }
            catch (Exception e)
            {
                result = e.Message;
            }
            finally
            {
                lock (resultList)
                {
                    resultList.Add(data.Length + "#" + result);
                }
                SocketClient.DestroySocket(s);
            }
           
            
        }
        private string GenRndProbeRequest(int length)
        {
            if (RndProbeConfig.LengthValues.Count == 0)
                return RndProbeConfig.ProbeContent;
            return ProbeFactory.GetRandomString(length);
        }
        private string DealResult(List<string> result)
        {
            if (result == null || result.Count() == 0)
                return string.Empty;
            string formatResult = string.Empty;
            var lst = from v in result
                      group v by v into G
                      orderby G.Key
                      select new
                      {
                          data = G.Key,
                          count = G.Count()
                      };
            foreach (var v in lst)
            {
                formatResult +=  v.count + "#" + v.data + ";";
            }
            return formatResult.Trim(';');
        }
        #endregion
        #region 重放探针
        #endregion
    }
}
