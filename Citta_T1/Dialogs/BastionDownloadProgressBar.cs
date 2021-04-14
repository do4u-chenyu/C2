﻿using C2.Business.SSH;
using C2.SearchToolkit;
using C2.Utils;
using System;

namespace C2.Dialogs
{
    class BastionDownloadProgressBar : UpdateProgressBar
    {
        private readonly String done;
        private readonly String temp;
        private BastionAPI api;
        //private long fileLength;

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
            {
                FileUtil.DeleteFile(done);     // 先删除重名文件,要确认下载成功后再删,以免文件没下载,以前的也没有了
                FileUtil.FileMove(temp, done);
            }          
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