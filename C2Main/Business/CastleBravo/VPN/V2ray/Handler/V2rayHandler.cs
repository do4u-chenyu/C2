using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace v2rayN.Handler
{

    /// <summary>
    /// 消息委托
    /// </summary>
    /// <param name="notify">是否显示在托盘区</param>
    /// <param name="msg">内容</param>
    public delegate void ProcessDelegate(bool notify, string msg);

    /// <summary>
    /// v2ray进程处理类
    /// </summary>
    class V2rayHandler
    {
        private readonly List<string> lstV2ray;
        public event ProcessDelegate ProcessEvent;

        public V2rayHandler()
        {
            lstV2ray = new List<string>
            {
                "wv2ray",
                "v2ray"
            };
        }

        /// <summary>
        /// 新建进程，载入V2ray配置文件字符串
        /// 返回新进程pid。
        /// </summary>
        public int LoadV2rayConfigString(List<ListViewItem> lv)
        {
            int pid = -1;
            string configStr = V2rayConfigHandler.GenerateClientSpeedtestConfigString(lv);
            if (configStr == string.Empty)
            {
                ShowMsg(false, string.Empty);
            }
            else
            {
                ShowMsg(false, string.Empty);
                pid = V2rayStartNew(configStr);
            }
            return pid;
        }


             /// <summary>
        /// V2ray停止
        /// </summary>
        public void V2rayStopPid(int pid)
        {
            try
            {
                Process _p = Process.GetProcessById(pid);
                KillProcess(_p);
            }
            catch (Exception ex)
            {
                Utils.SaveLog(ex.Message, ex);
            }
        }

        private string V2rayFindexe() {
            //查找v2ray文件是否存在
            string fileName = string.Empty;
            lstV2ray.Reverse();
            foreach (string name in lstV2ray)
            {
                string vName = string.Format("{0}.exe", name);
                vName = Path.Combine(Utils.V2rayStartupPath(), vName);
                if (File.Exists(vName))
                {
                    fileName = vName;
                    break;
                }
            }
            if (Utils.IsNullOrEmpty(fileName))
            {
                ShowMsg(false, "NotFoundCore");
            }
            return fileName;
        }

            /// <summary>
        /// V2ray启动，新建进程，传入配置字符串
        /// </summary>
        private int V2rayStartNew(string configStr)
        {
            ShowMsg(false, string.Format("StartService:{0}", DateTime.Now.ToString()));

            try
            {
                string fileName = V2rayFindexe();
                if (fileName == string.Empty) return -1;

                Process p = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = fileName,
                        Arguments = "-config stdin:",
                        WorkingDirectory = Utils.V2rayStartupPath(),
                        UseShellExecute = false,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true,
                        StandardOutputEncoding = Encoding.UTF8
                    }
                };
                p.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
                {
                    if (!String.IsNullOrEmpty(e.Data))
                    {
                        string msg = e.Data + Environment.NewLine;
                        ShowMsg(false, msg);
                    }
                });
                p.Start();
                p.BeginOutputReadLine();

                p.StandardInput.Write(configStr);
                p.StandardInput.Close();

                if (p.WaitForExit(1000))
                {
                    throw new Exception(p.StandardError.ReadToEnd());
                }

                Global.processJob.AddProcess(p.Handle);
                return p.Id;
            }
            catch (Exception ex)
            {
                Utils.SaveLog(ex.Message, ex);
                string msg = ex.Message;
                ShowMsg(false, msg);
                return -1;
            }
        }

        /// <summary>
        /// 消息委托
        /// </summary>
        /// <param name="updateToTrayTooltip">是否更新托盘图标的工具提示</param>
        /// <param name="msg">输出到日志框</param>
        private void ShowMsg(bool updateToTrayTooltip, string msg)
        {
            ProcessEvent?.Invoke(updateToTrayTooltip, msg);
        }

        private void KillProcess(Process p)
        {
            try
            {
                p.CloseMainWindow();
                p.WaitForExit(100);
                if (!p.HasExited)
                {
                    p.Kill();
                    p.WaitForExit(100);
                }
            }
            catch (Exception ex)
            {
                Utils.SaveLog(ex.Message, ex);
            }
        }         
    }
}
