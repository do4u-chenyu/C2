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
        public string[] EndWithList { get => nameListTB.Text.Trim().Split("|", StringSplitOptions.RemoveEmptyEntries); } 
        public string ProbeStrategy { get => probeStrategyCB.Text.Trim(); }
    }
}
