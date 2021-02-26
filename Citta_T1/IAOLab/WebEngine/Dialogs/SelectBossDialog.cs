using C2.Business.Option;
using C2.Controls;
using C2.Controls.Common;
using C2.Core;
using C2.IAOLab.WebEngine.Boss;
using C2.Model;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.IAOLab.WebEngine.Dialogs
{
    partial class SelectBossDialog : StandardDialog
    {
        private static readonly int MaxLine = 100;
        public string WebUrl;
        public List<DataItem> DataItems;
        private BcpInfo bcpInfo;
        private DataItem selectData;
        private Dictionary<string, string[]> chartOptions;

        public SelectBossDialog(List<DataItem> dataItems)
        {
            InitializeComponent();
            chartOptions = new Dictionary<string, string[]>();
            WebUrl = Path.Combine(Application.StartupPath, "IAOLab\\WebEngine\\Html", "BossIndex01.html");
            DataItems = dataItems;
            foreach (DataItem dataItem in DataItems)
            {
                this.datasource.Items.Add(dataItem.FileName);
            }
        }


        protected override bool OnOKButtonClick()
        {
            string fileContent;
            DataTable dataTable = new DataTable("temp");

            //1、选中的数据源前100行生成dataTable
            foreach (string col in bcpInfo.ColumnArray)
                dataTable.Columns.Add(col);

            if (selectData.FileType == OpUtil.ExtType.Excel)
                fileContent = BCPBuffer.GetInstance().GetCachePreviewExcelContent(selectData.FilePath);
            else
                fileContent = BCPBuffer.GetInstance().GetCachePreviewBcpContent(selectData.FilePath, selectData.FileEncoding);
            List<string> rows = new List<string>(fileContent.TrimEnd('\r').TrimEnd('\n').Split('\n'));
            int maxLine = Math.Min(rows.Count, MaxLine);
            
            //文件仅有表头无法画图表
            if (maxLine <= 1)
                return false;

            for (int i = 1; i < maxLine; i++)
            {
                string row = rows[i].TrimEnd('\r');
                dataTable.Rows.Add(row.Split(selectData.FileSep));
            }

            //2、图表字段配置项生成chartOptions
            SetChartOption("SimpleBar", simpleBarX.SelectedIndex, simpleBarY.GetItemCheckIndex());
            SetChartOption("BasicLineChart", basicLineChartX.SelectedIndex, basicLineChartY.GetItemCheckIndex());
            SetChartOption("BasicScatter", basicScatterX.SelectedIndex, basicScatterY.GetItemCheckIndex());
            SetChartOption("SmoothedLineChart", smoothedLineChartX.SelectedIndex, smoothedLineChartY.GetItemCheckIndex());
            SetChartOption("StackBar", stackBarX.SelectedIndex, stackBarY.GetItemCheckIndex());
            SetChartOption("BasicPie", basicPieX.SelectedIndex, new List<int>() { basicPieY.SelectedIndex });
            SetChartOption("BasicMap", basicMapX.SelectedIndex, new List<int>() { basicMapY.SelectedIndex });

            //3、调用生成js
            GenBossHtml.GetInstance().TransDataToHtml(dataTable, chartOptions);
            return base.OnOKButtonClick();
        }

        private void SetChartOption(string chartType, int idxX, List<int> idxY)
        {
            List<string> tmpList = new List<string>();
            if (idxX < 0 || idxY.Count == 0 || (idxY.Count == 1 && idxY[0] == -1))
                return;

            tmpList.Add(bcpInfo.ColumnArray[idxX]);
            idxY.ForEach(t => tmpList.Add(bcpInfo.ColumnArray[t]));
            if (chartOptions.ContainsKey(chartType))
                chartOptions[chartType] = tmpList.ToArray();
            else
                chartOptions.Add(chartType, tmpList.ToArray());
        }

        private void Datasource_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectData = DataItems[this.datasource.SelectedIndex];
            
            this.bcpInfo = new BcpInfo(selectData.FilePath, selectData.FileEncoding, new char[] { selectData.FileSep });
            GetOptionControls();
        }

        private void GetOptionControls()
        {
            foreach(Control ct in this.Controls)
            {
                if(ct is ComboBox && (ct.Name.EndsWith("X") || ct.Name.EndsWith("Y")))
                {
                    (ct as ComboBox).Text = string.Empty;
                    (ct as ComboBox).Items.Clear();
                    (ct as ComboBox).Items.AddRange(bcpInfo.ColumnArray);
                }else if(ct is ComCheckBoxList)
                {
                    (ct as ComCheckBoxList).ClearText();
                    (ct as ComCheckBoxList).Items.Clear();
                    (ct as ComCheckBoxList).Items.AddRange(bcpInfo.ColumnArray);
                }
            }

        }
    }
}
