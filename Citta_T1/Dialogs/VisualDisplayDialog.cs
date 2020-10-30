using C2.Business.Option;
using C2.Model;
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
    public partial class VisualDisplayDialog : Form
    {
        private BcpInfo bcpInfo;
        public VisualDisplayDialog(DataItem hitItem)
        {
            InitializeComponent();
            InitializeDropDown(hitItem);
        }
        private void InitializeDropDown(DataItem hitItem)
        {
            this.bcpInfo = new BcpInfo(hitItem.FilePath, hitItem.FileEncoding, new char[] { hitItem.FileSep });
            this.xValue.Items.AddRange(bcpInfo.ColumnArray);
            this.yValue.Items.AddRange(bcpInfo.ColumnArray);
            this.chartTypesList.Items.Insert(0, "柱状图");
            this.chartTypesList.SelectedIndex = 0;
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            if (IsOptionNotReady())
                return;

        }
        private bool IsOptionNotReady()
        {
            int status0 = String.IsNullOrEmpty(this.chartTypesList.Text) ? 1 : 0;
            int status1 = String.IsNullOrEmpty(this.xValue.Text) ? 2 : 0;
            int status2 = this.yValue.GetItemCheckIndex().Count == 0 ? 4 : 0;
            if ((status0& status1&status2) >0)
            {
                switch (status0 & status1 & status2)
                {
                    case 7:
                    case 5:
                    case 3:
                    case 1:
                        MessageBox.Show("请设置图表类型.");
                        break;
                    case 6:
                    case 2:
                        MessageBox.Show("请设置输入维度.");
                        break;
                    case 4:
                        MessageBox.Show("请设置输出维度.");
                        break;

                }
               
                return true;
            }
            return false;
        }
 
        private void cancle_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
