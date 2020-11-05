using C2.Business.Option;
using C2.Core;
using C2.Dialogs.Base;
using C2.Dialogs.WidgetChart;
using C2.Model;
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
    public partial class VisualDisplayDialog : C1BaseOperatorView
    {
        private BcpInfo bcpInfo;
        private string filePath;
        private OpUtil.Encoding fileEncoding;
        private char fileSep;
        private string fileName;
        private DataItem hitItem;
        public VisualDisplayDialog(DataItem hitItem)
        {
            this.hitItem = hitItem;
            this.filePath = hitItem.FilePath;
            this.fileName = hitItem.FileName;
            this.fileEncoding = hitItem.FileEncoding;
            this.fileSep = hitItem.FileSep;
            InitializeComponent();
            InitializeDropDown(hitItem);
        }
        private void InitializeDropDown(DataItem hitItem)
        {
            this.bcpInfo = new BcpInfo(filePath, fileEncoding, new char[] { fileSep });
            this.comboBox0.Items.AddRange(bcpInfo.ColumnArray);
            this.outListCCBL0.Items.AddRange(bcpInfo.ColumnArray);
            this.chartTypesList.Items.Insert(0, "柱状图");
            this.chartTypesList.SelectedIndex = 0;
        }

        protected override void  ConfirmButton_Click(object sender, EventArgs e)
        {
            if (OptionNotReady())
                return;
            int upperLimit = 100;

            // 获得x,y轴数据的列索引
            int xIndex = comboBox0.Tag == null ? comboBox0.SelectedIndex : ConvertUtil.TryParseInt(comboBox0.Tag.ToString());
            List<int> indexs = new List<int>() { xIndex };
            indexs.AddRange(outListCCBL0.GetItemCheckIndex());
        

            // 获取选中输入、输出各列数据
            List<string> rows = new List<string>(BCPBuffer.GetInstance().GetCachePreViewBcpContent(filePath, fileEncoding).Split('\n'));
            upperLimit = Math.Min(rows.Count, upperLimit);

            List<List<string>> columnValues= Utils.FileUtil.GetColumns(indexs, hitItem, rows, upperLimit);
            if (columnValues.Count == 0)
            { 
                Close(); 
                return;
            }
       
            Utils.ControlUtil.PaintChart(columnValues, new List<string>() { this.fileName, this.fileName }, this.chartTypesList.Text);
            // 存储图表挂件需要的数据
            hitItem.ChartType = this.chartTypesList.Text;
            hitItem.SelectedIndexs = xIndex;
            this.DialogResult = DialogResult.OK;
            Close();
        }      
        private bool OptionNotReady()
        {
            int status0 = String.IsNullOrEmpty(this.chartTypesList.Text) ? 1 : 0;
            int status1 = String.IsNullOrEmpty(this.comboBox0.Text) ? 2 : 0;
            int status2 = this.outListCCBL0.GetItemCheckIndex().Count == 0 ? 4 : 0;
            if ((status0 | status1 | status2) > 0)
            {
                switch (status0 | status1 | status2)
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
 
        protected override void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
