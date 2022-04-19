using C2.Controls;
using C2.Core;
using C2.Dialogs.CastleBravo.Mode;
using C2.Utils;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

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
            ResetDGV1();
            ResetDGV2();
        }

        private void ResetDGV1()
        {
            this.DGV.Rows.Clear();
            this.DGV.Rows.Add(new string[] { string.Empty, "123456", "salt", "admin" });
            this.DGV.Rows.Add();
            this.DGV.Rows.Add();
        }

        private void ResetDGV2()
        {
            this.resultDGV.Rows.Clear();
            foreach (string m in MLD.Keys)
                resultDGV.Rows.Add(new string[] { m });
        }

        private void DigButton_Click(object sender, System.EventArgs e)
        {
            ResetDGV2();
            string message = string.Empty;
            foreach(DataGridViewRow row in DGV.Rows)
            {
                if (row.Cells.Count < 4)
                    continue;

                string md5  = ST.GetValue<string>(row.Cells[0].Value, string.Empty).Trim();
                string pass = ST.GetValue<string>(row.Cells[1].Value, string.Empty).Trim();
                string salt = ST.GetValue<string>(row.Cells[2].Value, string.Empty).Trim();
                string user = ST.GetValue<string>(row.Cells[3].Value, string.Empty).Trim();

                if (md5.IsNullOrEmpty())
                    continue;
                if (pass.IsNullOrEmpty())
                    continue;

                message += Process(row.Index, md5, pass, salt, user);
            }

            if (message.Replace(',', '\x0').Trim().IsNullOrEmpty())
                HelpUtil.ShowMessageBox(string.Format("不符合当前{0}种加密模式的任一个", MLD.Count));
            else
                HelpUtil.ShowMessageBox(string.Format("符合加密模式:{0}, 细节查看结果表", message.Trim(',')));
            
        }

        private string Process(int cIndex, string md5, string pass, string salt, string user)
        {
            int rIndex = 0;
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in MLD)
            {
                rIndex++;
                // 没用到user字段,带$U参数的就可以跳过了
                if (user.IsNullOrEmpty() && kv.Key.Contains("$U"))
                    continue;
                string ret = TryProcess(kv.Value, pass, salt, user);

                if (!ret.Equals(md5))
                    continue;

                // 找到了, 确定resultDGV中打标的单元格
                this.resultDGV.Rows[rIndex - 1].Cells[cIndex + 1].Value = "√";
                sb.Append(',')
                  .Append(rIndex);
            }
            return sb.ToString();
        }

        private string TryProcess(string method, string pass, string salt, string user)
        {
            try
            {
                MethodInfo mi = typeof(ML).GetMethod(method, BindingFlags.Public | BindingFlags.Static);
                return (string)mi.Invoke(null, new object[] { pass, salt, user });
            }
            catch
            {
                return string.Empty;
            }
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
