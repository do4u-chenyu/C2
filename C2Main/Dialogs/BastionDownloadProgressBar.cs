using C2.Business.SSH;
using C2.SearchToolkit;
using C2.Utils;
using System;
using System.Threading;
using System.Windows.Forms;

namespace C2.Dialogs
{
    class BastionDownloadProgressBar : UpdateProgressBar
    {
        private readonly String done;
        private readonly String temp;
        private readonly BastionAPI api;
        private DateTime time;
        private long count;

        private void UpdateProgressBar(String pogressValue, long left, long fileLength)
        {
            this.ProgressPercentage = pogressValue;
            this.CurrentValue = ConvertUtil.TryParseInt(pogressValue);

            this.SetFileLength(FormatLength(fileLength));

            if (fileLength > 1024 * 1024 * 210)
                ShowBigFileWarning();

            // 完成时提示
            if (pogressValue == "100%" && left == 0)
            {
                DialogResult dr = MessageBox.Show("下载完成，点击确定打开下载目录", 
                    "下载完成", 
                    MessageBoxButtons.OKCancel, 
                    MessageBoxIcon.Information);

                if (dr != DialogResult.OK)
                    return;
                if (done.Contains("外网_"))
                    FileUtil.TryExploreDirectory(done.Replace("外网_", ""));
                else
                    FileUtil.TryExploreDirectory(done);

                return;
            }

            Double secondsSpan = (DateTime.Now - time).TotalMilliseconds;
            
            if (secondsSpan < 1000)
                return;

            long bytesRead = count - left;
            long speed = (long)Math.Max(0, Math.Abs(bytesRead * 1000 / secondsSpan));


            // TODO 去掉噪音数据
            if (speed < 0)
                return;

           
            this.SetDownloadSpeed(FormatLength(speed) + "/s");
            time = DateTime.Now;
            count = left;

            Application.DoEvents();
        }
        public BastionDownloadProgressBar(SearchTaskInfo task, String ffp)
        {
            api = new BastionAPI(task);
            api.DownloadProgressEvent += UpdateProgressBar;
            done = ffp;
            temp = done + ".download";
            this.FormClosed += BastionDownloadProgressBar_FormClosed;
            time = DateTime.Now;
            count = 0;
        }


        public bool Download()
        {
            BastionAPI aPI = api.Login();
            bool succ = aPI.DownloadTaskResult(temp);
            if (temp.Contains("外网_"))
            {
                Thread.Sleep(1000);
                succ = succ && aPI.DownloadTaskResult(temp.Replace("外网_", ""));
            }
               
            if (succ) // 成功 临时文件转正
            {
                FileUtil.DeleteFile(done);     // 先删除重名文件,要确认下载成功后再删,以免文件没下载,以前的也没有了
                if (temp.Contains("外网_"))
                {
                    FileUtil.FileMove(temp, done.Replace(".tgz", ".net"));
                    FileUtil.DeleteFile(done.Replace("外网_", ""));
                    FileUtil.FileMove(temp.Replace("外网_", ""), done.Replace("外网_", ""));
                }
                else
                    FileUtil.FileMove(temp, done);
            }
            api.Close();
            return succ;
        }

        private void BastionDownloadProgressBar_FormClosed(object sender, FormClosedEventArgs e)
        {
            api.StopDownloadAsync();
        }

        private String FormatLength(long value)
        {
            return value < 1048576 ?
                String.Format("{0:.#}K", value / 1024.0) :
                String.Format("{0:.#}M", value / 1048576.0);
        }
    }
}
