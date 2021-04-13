using C2.Business.SSH;
using C2.SearchToolkit;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Dialogs
{
    class BastionDownloadProgressBar : UpdateProgressBar
    {
        private readonly String done;
        private readonly String temp;
        private BastionAPI api;
        public BastionDownloadProgressBar(TaskInfo task, String ffp)
        {
            api = new BastionAPI(task);
            done = ffp;
            temp = done + ".download";

            this.FormClosed += BastionDownloadProgressBar_FormClosed;
        }


        public bool Download()
        {
            bool succ = api.Login()
                           .DownloadGambleTaskResult(temp);
            if (succ) // 成功 临时文件转正
                FileUtil.FileMove(temp, done);
            else      // 失败 删除临时文件
                FileUtil.DeleteFile(temp);

            return succ;
        }

        private void BastionDownloadProgressBar_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            api.StopDownloadAsync();
        }
    }
}
