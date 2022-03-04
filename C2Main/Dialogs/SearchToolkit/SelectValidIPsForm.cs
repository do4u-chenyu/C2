using C2.Business.SSH;
using C2.Controls;
using C2.Core;
using C2.SearchToolkit;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace C2.Dialogs.SearchToolkit
{
    partial class SelectValidIPsForm : StandardDialog
    {
        private SearchTaskInfo task;
        public SelectValidIPsForm(SearchTaskInfo task)
        {
            InitializeComponent();
            this.task = task;
            RefreshIPListView();
            this.label3.Text = BastionAPI.availSize;

        }

        private void RefreshIPListView()
        {
            int ipCount = 0;
            this.dictDataGridView.Rows.Clear();

            if (task.DaemonIP.Count == 1)
                this.label4.Text = "1个)";

            for (int i = 0; i < task.DaemonIP.Count; i++)
            {
                string ipAndPort = task.DaemonIP[i];
                if (!ipAndPort.Contains(":"))
                    continue;
                ipCount++;

                DataGridViewRow dr = new DataGridViewRow();

                DataGridViewCheckBoxCell cell1 = new DataGridViewCheckBoxCell();
                if (i < 2) cell1.Value = true;
                dr.Cells.Add(cell1);

                DataGridViewTextBoxCell textCell2 = new DataGridViewTextBoxCell();
                textCell2.Value = ipCount + string.Empty;
                dr.Cells.Add(textCell2);

                DataGridViewTextBoxCell textCell3 = new DataGridViewTextBoxCell();
                textCell3.Value = ipAndPort.Split(":")[0];
                dr.Cells.Add(textCell3);

                DataGridViewTextBoxCell textCell4 = new DataGridViewTextBoxCell();
                textCell4.Value = ipAndPort.Split(":")[1];
                dr.Cells.Add(textCell4);

                this.dictDataGridView.Rows.Add(dr);
            }
        }

        protected override bool OnOKButtonClick()
        {
            int selectNum = 0;
            List<string> selectIPList = new List<string>();

            foreach (DataGridViewRow row in this.dictDataGridView.Rows)
            {
                if (row.Cells[0].EditedFormattedValue.ToString() == "True")
                {
                    selectIPList.Add(row.Cells[2].Value.ToString() + ":" + row.Cells[3].Value.ToString());
                    selectNum++;
                }
            }
            if (selectNum == 0)
            {
                HelpUtil.ShowMessageBox("未选中任何ip，请重新选择!");
                return false;
            }
            string txtPath = Path.Combine(Global.TempDirectory, "select_valid_ips.txt");
            WriteResult(txtPath, selectIPList);
            //List<string> ip = new List<string> { "15.73.15.101:1190" };
            //WriteResult(txtPath, ip);
            task.SelectDaemonIPCount = selectIPList.Count.ToString();
            return base.OnOKButtonClick();
        }

        private void WriteResult(string txtPath, List<string> selectIPList)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(txtPath, false, System.Text.Encoding.Default))
                {
                    foreach (string selectIP in selectIPList)
                        sw.Write(selectIP + "\n");
                }
            }
            catch (Exception ex)
            {
                HelpUtil.ShowMessageBox(ex.Message);
            }
        }

        private void AllSelectBtn_Click(object sender, EventArgs e)
        {
            int ipCount = 0;
            foreach (DataGridViewRow row in this.dictDataGridView.Rows)
            {
                row.Cells[0].Value = true;
                ipCount++;
            }
            this.label4.Text = ipCount.ToString() + "个)";
        }


        private void NoSelectBtn_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dictDataGridView.Rows)
                row.Cells[0].Value = null;
            this.label4.Text = "0个)";
        }

        private void DictDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dictDataGridView.Rows.Count > 0)
            {
                int ipCount = 0;
                foreach (DataGridViewRow row in this.dictDataGridView.Rows)
                    if (row.Cells[0].EditedFormattedValue.ToString() == "True")
                        ipCount++;
                this.label4.Text = ipCount.ToString() + "个)";
            }
        }
    }
}
