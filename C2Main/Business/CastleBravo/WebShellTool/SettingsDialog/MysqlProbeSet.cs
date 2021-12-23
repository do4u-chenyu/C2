using C2.Controls;
using C2.Core;
using C2.Utils;
using System;

namespace C2.Business.CastleBravo.WebShellTool.SettingsDialog
{
    partial class MysqlProbeSet : StandardDialog
    {
        public MysqlProbeSet()
        {
            InitializeComponent();
            this.probeStrategyCB.SelectedIndex = 1;
        }

        public int TimeoutSeconds { get => ConvertUtil.TryParseInt(timeoutTB.Text.Trim(), 600); }
        public string SearchFiles { get => fileList.Text.Trim(); }
        public string SearchFields { get => fieldList.Text.Trim(); }
        public int ProbeStrategy { get => Math.Max(0, probeStrategyCB.SelectedIndex); }

        protected override bool OnOKButtonClick()
        {
            //判断必填是否有值
            if (timeoutTB.Text.IsNullOrEmpty() ||
                fieldList.Text.IsNullOrEmpty() ||
                fileList.Text.IsNullOrEmpty() ||
                probeStrategyCB.SelectedIndex < 0)
            {
                HelpUtil.ShowMessageBox("各配置项不能为空。");
                return false;
            }

            return base.OnOKButtonClick();
        }
    }
}
