using C2.Utils;
using System;
using System.Windows.Forms;


namespace C2.Controls.Bottom
{
    public partial class BottomLogControl : UserControl
    {
        private int maxLineCount = 10000;
        public delegate void NeedShowEventHandler(string log);
        private static readonly LogUtil log = LogUtil.GetInstance("BottomLogControl"); // 获取日志模块
        public BottomLogControl()
        {
            InitializeComponent();
        }

        public void LogUpdate(string log)
        {
            this.textBox1.AppendText(log + System.Environment.NewLine);
            if (this.textBox1.Lines.Length > maxLineCount)
            {
                string[] newlines = new string[maxLineCount];
                Array.Copy(this.textBox1.Lines, this.textBox1.Lines.Length - maxLineCount, newlines, 0, maxLineCount);
                this.textBox1.Lines = newlines;
            }

        }
        public void ActiveUpdateLog(string logInfo)
        {
            this.Invoke(new NeedShowEventHandler(delegate (string tlog)
            {
                log.Info(tlog);
            }), logInfo);
        }
        private void MenuItemClearAll_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
        }

        private void MenuItemSelectAll_Click(object sender, EventArgs e)
        {
            this.textBox1.SelectAll();
            string copy = this.textBox1.SelectedText;
            Clipboard.SetDataObject(copy);
        }
    }
}
