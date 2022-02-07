using C2.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace C2.Controls.Bottom
{
    public partial class BottomLogControl : UserControl
    {
        private readonly int maxLineCount = 10000;
        private static readonly LogUtil log = LogUtil.GetInstance("BottomLogControl"); // 获取日志模块
        public BottomLogControl()
        {
            InitializeComponent();
        }

        public void LogUpdateWarn(string log)
        {
            this.textBox1.SelectionColor = Color.Red;
            LogUpdateText(log);
        }

        public void LogUpdateInfo(string log)
        {
            this.textBox1.SelectionColor = Color.Black;
            LogUpdateText(log);
        }

        public void LogUpdateDebug(string log)
        {
            this.textBox1.SelectionColor = Color.Blue;
            LogUpdateText(log);
        }

        public void LogUpdateError(string log)
        {
            this.textBox1.SelectionColor = Color.Red;
            LogUpdateText(log);
        }

        public void LogUpdateFatal(string log)
        {
            this.textBox1.SelectionColor = Color.Red;
            this.textBox1.SelectionFont = new Font(textBox1.SelectionFont, FontStyle.Bold);
            LogUpdateText(log);
        }

        private void LogUpdateText(string log)
        {
            this.textBox1.HideSelection = false;

            this.textBox1.AppendText(log + Environment.NewLine);
            if (this.textBox1.Lines.Length > maxLineCount)
            {
                string[] newlines = new string[maxLineCount];
                Array.Copy(this.textBox1.Lines, this.textBox1.Lines.Length - maxLineCount, newlines, 0, maxLineCount);
                this.textBox1.Lines = newlines;
            }
        }

        public void ActiveUpdateLog(string logInfo)
        {
            InvokeUtil.Invoke(this, new InvokeUtil.AsynCallback_S1(delegate (string tlog)
            {
                log.Info(tlog);   // 文本日志记录 
            }), logInfo);

            InvokeUtil.Invoke(this, new InvokeUtil.AsynCallback_S1(delegate (string tlog)
            {
                LogUpdateText(tlog);  // 日志面板记录
            }), logInfo);
        }
        private void MenuItemClearAll_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
        }

        private void MenuItemSelectAll_Click(object sender, EventArgs e)
        {
            this.textBox1.SelectAll();
            string copy = this.textBox1.SelectedText;
            Clipboard.SetDataObject(copy);
        }

        // 定位到最后一行
        public void FocusLocation()
        {
            this.textBox1.ScrollToCaret();
        }
    }
}
