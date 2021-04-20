using C2.Business.SSH;
using C2.SearchToolkit;
using C2.Utils;
using System;
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

            Double secondsSpan = (DateTime.Now - time).TotalMilliseconds; 
            long bytesRead = count - left;
            long speed = (long)Math.Max(0, Math.Abs(bytesRead * 1000 / secondsSpan));


            // TODO 去掉噪音数据
            if (speed >= 0)
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
            bool succ = api.Login()
                           .DownloadTaskResult(temp);
            if (succ) // 成功 临时文件转正
            {
                FileUtil.DeleteFile(done);     // 先删除重名文件,要确认下载成功后再删,以免文件没下载,以前的也没有了
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
