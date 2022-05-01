using C2.Controls;
using C2.Utils;
using System.Collections.Generic;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.VPN
{
    partial class RandomProbeForm : StandardDialog
    {
        public RandomProbeForm()
        {
            InitializeComponent();
        }
        public new RandomProbeConfig ShowDialog()
        {
            return base.ShowDialog() == System.Windows.Forms.DialogResult.OK ?
                new RandomProbeConfig() : RandomProbeConfig.Empty;
        }

        protected override bool OnOKButtonClick()
        {
            int timeout = ConvertUtil.TryParseInt(this.timeoutBox.Text.Trim());         
            if (timeout == 0 || timeout > 30)
                MessageBox.Show("超时时间需在(0,30)内。");
            int sendCount = ConvertUtil.TryParseInt(this.numBox.Text.Trim());
            if (sendCount == 0 || sendCount >150)
                MessageBox.Show("重试次数需在(1,150)内。");
            
            return base.OnOKButtonClick();
        }

    }
    public class RandomProbeConfig
    {
        public static readonly RandomProbeConfig Empty = new RandomProbeConfig();
        private int timeout;
        private int sendCount;
        private string lengthInterval;
        private string probeContent;
        public int Timeout { get { return timeout; } }
        public int SendCount { get { return sendCount; } }
        public string LengthInterval { get { return lengthInterval; } }
        public string ProbeContent { get { return probeContent; } }
        public RandomProbeConfig(int timeout, int sendCount, string lengthInterval, string probeContent)    
        {
            this.timeout = timeout;
            this.sendCount = sendCount;
            this.lengthInterval = lengthInterval;
            this.probeContent = probeContent;
        }
        public RandomProbeConfig() : this(0, 0, string.Empty, string.Empty) { }
    }

}
