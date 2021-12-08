﻿using C2.Controls;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.WebShellTool.SettingsDialog
{
    partial class WebConfigScan : StandardDialog
    {
        private string payload;
        public WebConfigScan()
        {
            InitializeComponent();
        }

        protected override bool OnOKButtonClick()
        {
            //判断必填是否有值
            if (filePathTextBox.Text.IsNullOrEmpty() || scanFieldTextBox.Text.IsNullOrEmpty())
            {
                HelpUtil.ShowMessageBox("【配置文件路径】 和 【扫描字段】 不能为空。");
                return false;
            }
            string[] configFields = scanFieldTextBox.Text.Split(',');
            if (scanFieldTextBox.Text.Equals("账号字段,密码字段") || configFields.Length != 2)
            {
                HelpUtil.ShowMessageBox(" 【扫描字段】 格式设置有误。");
                return false;
            }
            payload =string.Format( ClientSetting.InfoPayloadDict[InfoType.MysqlConfigField],
                                          "{0}",
                                          ST.EncodeBase64(filePathTextBox.Text.Trim()), 
                                          configFields[0].Trim(),
                                          configFields[1].Trim());
            return base.OnOKButtonClick();
        }
        public new string ShowDialog()
        {
            return base.ShowDialog() == System.Windows.Forms.DialogResult.OK ? payload : string.Empty;
        }

    }
}
