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

namespace C2.Dialogs
{
    public partial class UpdateProgressBar : Form
    {
        public UpdateProgressBar()
        {
            InitializeComponent();
        }
       
        public string Status { get => this.status.Text; set => this.status.Text = value; }
        public int MinimumValue { get => this.proBarDownLoad.Minimum; set => this.proBarDownLoad.Minimum = value; }
        public int MaximumValue { get => this.proBarDownLoad.Maximum; set => this.proBarDownLoad.Maximum = value; }
        public int CurrentValue { get => this.proBarDownLoad.Value; set => this.proBarDownLoad.Value = value; }
        public string ProgressPercentage { get => this.speedValue.Text; set => this.speedValue.Text = value; }

        private void UpdateProgressBar_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 会有没下载完出现下载完成的bug，防止用户无法关闭弹窗
            if (Status.Equals("下载完成,请重启软件实现更新"))
                return;

            if (!ProgressPercentage.Equals("100%"))
            {
                e.Cancel = true;
                HelpUtil.ShowMessageBox("下载未完成，无法结束当前更新任务");
            }
                
        }
    }
}
