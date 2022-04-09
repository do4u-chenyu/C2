using C2.Controls;
using C2.Utils;
using System.Collections.Generic;

namespace C2.Dialogs.CastleBravo
{
    partial class MLForm : StandardDialog
    {
        private readonly Dictionary<string, string> MLD;
        public MLForm(Dictionary<string, string> mld)
        {
            this.MLD = mld;
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
            this.DGV.Rows.Add(new string[] { string.Empty, "123456"});
            this.DGV.Rows.Add(new string[] { string.Empty, "123456", string.Empty, "admin" });
            this.DGV.Rows.Add();
            this.resultDGV.Rows.Clear();
            foreach (string m in MLD.Keys)
                this.resultDGV.Rows.Add(new string[] { m });
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

        private void PasteButton_Click(object sender, System.EventArgs e)
        {
            FileUtil.TryClipboardSetText(string.Join(System.Environment.NewLine, MLD.Keys));
            HelpUtil.ShowMessageBox("模式列表已经复制到内存剪切板");
        }
    }
}
