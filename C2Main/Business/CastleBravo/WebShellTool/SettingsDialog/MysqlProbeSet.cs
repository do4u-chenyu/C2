﻿using C2.Controls;
using C2.Utils;
using C2.Core;
using System;

namespace C2.Business.CastleBravo.WebShellTool.SettingsDialog
{
    partial class MysqlProbeSet : StandardDialog
    {
        public MysqlProbeSet()
        {
            InitializeComponent();
            this.probeStrategyCB.SelectedIndex = 0;
        }

        public int TimeoutSeconds { get => ConvertUtil.TryParseInt(timeoutTB.Text.Trim(), 600); }
        public string SearchFiles { get => fileList.Text.Trim(); }
        public string SearchFields { get => fieldList.Text.Trim(); }
        public string ProbeStrategy { get => probeStrategyCB.Text.Trim(); }

        protected override bool OnOKButtonClick()
        {
            //判断必填是否有值
            if (timeoutTB.Text.IsNullOrEmpty() ||
                probeStrategyCB.SelectedIndex < 0 ||
                fieldList.Text.IsNullOrEmpty() ||
                fileList.Text.IsNullOrEmpty())
            {
                HelpUtil.ShowMessageBox("各配置项不能为空。");
                return false;
            }
            return base.OnOKButtonClick();
        }


    }
}
