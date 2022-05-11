using Amib.Threading;
using C2.Business.CastleBravo.VPN.Probe;
using C2.Core;
using C2.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using static C2.Utils.GuarderUtil;

namespace C2.Business.CastleBravo.VPN
{
    
    public partial class VPNMainForm
    {
        private DateTime s;  // 自动保存
        #region 随机探针
        // 选定项发探针
        // 重新发探针
        // 继续发探针

        private void SendRandomProbe(IList items)
        {

            foreach (ListViewItem lvi in items)
            {
                if (actionNeedStop)
                    break;
                UpdateOneRandomProbeResult(lvi);
                // 5分钟保存一次
            }
        }

        private void UpdateOneRandomProbeResult(ListViewItem lvi)
        {
            VPNTaskConfig task = lvi.Tag as VPNTaskConfig;
            int port = ConvertUtil.TryParseInt(task.Port.Trim());
            if (port == 0)
            {
                lvi.SubItems[8].Text = "失败，端口字段格式不正确";
                return;
            }
            if (string.IsNullOrEmpty(task.IP))
            {
                lvi.SubItems[8].Text = "失败，IP字段不能为空";
                return;
            }

            string resultPath = WriteResult(task.IP, port.ToString());
            if (string.IsNullOrEmpty(resultPath))
            {
                lvi.SubItems[8].Text = string.Format("创建输出文件失败:{0}", resultPath);
                return;
            }
            using (StreamWriter sw = new StreamWriter(resultPath, false, Encoding.Default))
            {
                foreach (int index in RndProbeConfig.LengthValues)
                {
                    for (int i = 0; i < RndProbeConfig.SendCount; i++)
                    {
                        string data = GenRndProbeRequest(index);
                        sw.WriteLine(SocketClient.RndProbeResponse(task.IP, port, data,RndProbeConfig.Timeout));
                    }
                    sw.Flush();
                }
            }           
            lvi.SubItems[8].Text = resultPath;
        }

        private string GenRndProbeRequest(int length)
        {
            if (RndProbeConfig.LengthValues.Count == 0)
                return RndProbeConfig.ProbeContent;
            return ProbeFactory.GetRandomString(length);
        }
        public string WriteResult(string ip, string port)
        {
            string time = DateTime.Now.ToString("yyyyMMddhhmmss");
            try
            {
                string path = Path.Combine(Global.UserWorkspacePath, "探针结果采集", "随机探针");
                Directory.CreateDirectory(path);
                string filename = string.Format("{0}_{1}_{2}.txt", ip, port, time);
                string filePath = Path.Combine(path, filename);
                return filePath;
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion
        #region 重放探针
        #endregion
    }
}
