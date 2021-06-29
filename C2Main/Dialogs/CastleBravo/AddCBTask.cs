﻿using C2.Business.CastleBravo;
using C2.Controls;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Dialogs.CastleBravo
{
    partial class AddCBTask : StandardDialog
    {
        public CastleBravoTaskInfo TaskInfo { set; get; }
        string TaskName { get => this.taskNameTextBox.Text; set => this.taskNameTextBox.Text = value; }
        string FilePath { get => this.filePathTextBox.Text; set => this.filePathTextBox.Text = value; }

        public AddCBTask()
        {
            InitializeComponent();
            InitTaskName();
        }

        private void BrowserButton_Click(object sender, EventArgs e)
        {
            this.FilePath = ofd.ShowDialog() == DialogResult.OK ? ofd.FileName : String.Empty;
        }

        private void InitTaskName()
        {
            TaskName = String.Format("喝彩城堡{0}", DateTime.Now.ToString("MMdd"));
        }

        protected override bool OnOKButtonClick()
        {
            TaskName = TaskName.Trim();//去掉首尾空白符
            if (!IsValidityTaskName() || !IsValidityFilePath())
                return false;

            List<string> md5List = GetUrlsFromFile(FilePath);
            if (md5List.Count == 0)
                return false;

            CastleBravoAPIResult result = new CastleBravoAPIResult();
            using (new GuarderUtil.CursorGuarder(Cursors.WaitCursor))
            {
                if (!CastleBravoAPI.GetInstance().StartTask(md5List, out result))
                    return false;
            }

            if (result.RespMsg != "Success")
            {
                HelpUtil.ShowMessageBox(result.RespMsg);
                return false;
            }

            HelpUtil.ShowMessageBox("任务下发成功");
            string destDirectory = Path.Combine(Global.UserWorkspacePath, "喝彩城堡");
            string destFilePath = Path.Combine(destDirectory, string.Format("{0}_{1}.txt", TaskName, result.Datas));
            FileUtil.CreateDirectory(destDirectory);
            using (File.Create(destFilePath)) { }

            TaskInfo = new CastleBravoTaskInfo(TaskName, result.Datas, FilePath, destFilePath, CastleBravoTaskStatus.Null);

            return base.OnOKButtonClick();
        }
        private List<string> GetUrlsFromFile(string filePath)
        {
            //TODO phx  MD5逆向解析读文件的最大行数
            int maxRow = 1000;

            List<string> md5List = new List<string>();
            if (!File.Exists(filePath))
            {
                HelpUtil.ShowMessageBox("该数据文件不存在");
                return md5List;
            }

            StreamReader sr = null;
            FileStream fs = null;
            try
            {
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs, Encoding.Default);
                for (int row = 0; row < maxRow && !sr.EndOfStream; row++)
                    md5List.Add(sr.ReadLine().Trim(new char[] { '\r', '\n'}));
            }
            catch
            {
                HelpUtil.ShowMessageBox(filePath + "文件加载出错，请检查文件内容。");
            }
            finally
            {
                if (fs != null)
                    fs.Close();
                if (sr != null)
                    sr.Close();
            }
            return md5List;
        }

        private bool IsValidityTaskName()
        {
            if (string.IsNullOrEmpty(TaskName))
            {
                HelpUtil.ShowMessageBox("任务名不能为空");
                return false;
            }

            if (FileUtil.IsContainIllegalCharacters(TaskName, "任务名") || FileUtil.NameTooLong(TaskName, "任务名"))
            {
                return false;
            }

            return true;
        }

        private bool IsValidityFilePath()
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                HelpUtil.ShowMessageBox("查询文件路径不能为空，请点击预览选择文件。");
                return false;
            }

            return true;
        }
    }
}
