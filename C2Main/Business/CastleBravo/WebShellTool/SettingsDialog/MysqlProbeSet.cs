using C2.Controls;
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
        public int ProbeStrategy { get => probeStrategyCB.SelectedIndex; }

        protected override bool OnOKButtonClick()
        {
            //判断必填是否有值
            int index = probeStrategyCB.SelectedIndex;
            if (timeoutTB.Text.IsNullOrEmpty() ||
                fieldList.Text.IsNullOrEmpty() ||
                fileList.Text.IsNullOrEmpty() ||
                (index > 0 && hostDir.Text.IsEmpty())
                )
            {
                HelpUtil.ShowMessageBox("各配置项不能为空。");
                return false;
            }

            return base.OnOKButtonClick();
        }

        private void ProbeStrategyCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            hostDir.Enabled = probeStrategyCB.SelectedIndex > 0;
        }
    }
}
