using C2.Controls;
using System.Collections.Generic;

namespace C2.Dialogs.CastleBravo
{
    partial class MLForm : StandardDialog
    {
        private readonly string[] ML;
        public MLForm(string[] ml)
        {
            this.ML = ml;
            InitializeComponent();
            InitializeOther();
            InitializeDGV();
        }

        private void InitializeOther()
        {

            this.OKButton.Size = new System.Drawing.Size(75, 27);
            this.CancelBtn.Size = new System.Drawing.Size(75, 27);
            this.DigButton.Size = new System.Drawing.Size(75, 27);
            this.ResetButton.Size = new System.Drawing.Size(75, 27);
        }

        private void InitializeDGV()
        {
            Reset();
        }

        private void Reset()
        {
            this.DGV.Rows.Clear();
            this.DGV.Rows.Add(new string[] { string.Empty, "123456", string.Empty, "admin"});
            this.DGV.Rows.Add();
            this.DGV.Rows.Add();
            this.DGV.Rows.Add();
            this.textBox1.Text = string.Join(System.Environment.NewLine, ML);
        }

        private void DigButton_Click(object sender, System.EventArgs e)
        {
            // string[] ml = new List<string>(ML).ToArray();
            
            // int ln = 0;    // 输入样例的编号
            // int mn = 0;    // 模式编号
            // 遍历 列表
            // 选取合适的行
            // 挨个尝试
            // 成功 输出 成功行
            // 失败 输出 失败行
        }

        private void ResetButton_Click(object sender, System.EventArgs e)
        {
            Reset();
        }
    }
}
