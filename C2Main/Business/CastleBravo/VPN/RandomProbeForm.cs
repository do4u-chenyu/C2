using C2.Controls;
using C2.Utils;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.VPN
{
    partial class RandomProbeForm : StandardDialog
    {
        int timeout;
        int sendCount;
        List<int> lengthValues;
        string probeContent;
        public RandomProbeForm()
        {
            InitializeComponent();
            InitializeOther();
        }

        private void InitializeOther()
        {
            this.lengthValues = new List<int>();
            this.OKButton.Size = new Size(75, 27);
            this.CancelBtn.Size = new Size(75, 27);
        }
        public new RandomProbeConfig ShowDialog()
        {
            return base.ShowDialog() == DialogResult.OK ?
                new RandomProbeConfig(
                    this.timeout, 
                    this.sendCount, 
                    this.lengthValues,
                    this.probeContent
                    ) : RandomProbeConfig.Empty;
        }

        protected override bool OnOKButtonClick()
        {
            this.timeout = ConvertUtil.TryParseInt(this.timeoutBox.Text.Trim());
            if (timeout == 0 || timeout > 30)
            {
                MessageBox.Show("超时时间需在(0,30)内。");
                return false;
            }
               

            this.sendCount = ConvertUtil.TryParseInt(this.numBox.Text.Trim());
            if (sendCount == 0 || sendCount > 150)
            {
                MessageBox.Show("重试次数需在(1,150)内。");
                return false;
            }
               
            if (!contentCheckBox.Checked)
            {
                this.lengthValues.AddRange(GenLengthSet(lengthBox.Text.Trim()));
                if (this.lengthValues.Count() == 0)
                {
                    MessageBox.Show("长度区间设置有误，取值需在(1,225)内。");
                    return false;
                }
            }
            this.probeContent = probeContentBox.Text;
            return base.OnOKButtonClick();
        }
        private List<int> GenLengthSet(string length)
        {

            if (string.IsNullOrEmpty(length))
                return new List<int>();

            string[] intervals = length.Split(',');
            if (intervals.Length > 225)
                return new List<int>();
            List<int> result = new List<int>();
            foreach (string interval in intervals)
            {
                if (!interval.Contains("-"))
                {
                    int value = ConvertUtil.TryParseInt(interval.Trim());
                    if (value == 0 || value > 225)
                        return new List<int>();
                    result.Add(value);
                    continue;
                }
                string[] endpoints = interval.Trim().Split('-');
                if (endpoints.Length != 2)
                    return new List<int>();

                int start = ConvertUtil.TryParseInt(endpoints[0].Trim());
                int end = ConvertUtil.TryParseInt(endpoints[1].Trim());
                if (start == 0 || end == 0 || start >= end || start > 225 || end > 225)
                    return new List<int>();
                result.AddRange(Enumerable.Range(start, end));
            }
            return result;
        }

        private void ContentCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            bool status = contentCheckBox.Checked;
            this.lengthBox.ReadOnly = status;
            this.probeContentBox.ReadOnly = !status;
            this.lengthBox.BackColor = status? SystemColors.Menu: Color.White;
            this.probeContentBox.BackColor = status ? Color.White : SystemColors.Menu;
        }
    }
    public class RandomProbeConfig
    {
        public static readonly RandomProbeConfig Empty = new RandomProbeConfig();

        private readonly int timeout;
        private readonly int sendCount;
        private readonly List<int> lengthValues;
        private readonly string probeContent;

        public int TimeoutSeconds { get { return timeout; } }
        public int SendCount { get { return sendCount; } }
        public List<int> LengthValues { get { return lengthValues; } }
        public string ProbeContent { get { return probeContent; } }
        public RandomProbeConfig(int timeout, int sendCount, List<int> lengthValues, string probeContent)    
        {
            this.timeout = timeout;
            this.sendCount = sendCount;
            this.lengthValues = lengthValues;
            this.probeContent = probeContent;
        }
        public RandomProbeConfig() : this(0, 0, new List<int>(),  string.Empty) { }
        public bool IsEmpty()
        {
            return this.Equals(Empty);
        }
    }

}
