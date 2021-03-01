using C2.Business.Option;
using C2.Controls;
using C2.Controls.Common;
using C2.Core;
using C2.IAOLab.WebEngine.Boss;
using C2.Model;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
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
        public Dictionary<string, int[]> ChartOptions;

        public SelectBossDialog(List<DataItem> dataItems, Dictionary<string, int[]> options)
        {
            InitializeComponent();

            DataItems = dataItems;
            ChartOptions = options;
            LoadOption();
        }

        private void SavaOption()
        {
            SavaDataOption("Datasource", datasource.SelectedIndex);
            SavaDataOption("BossType", bossType.SelectedIndex);
            SaveChartOption("SimpleBar", simpleBarX.SelectedIndex, simpleBarY.GetItemCheckIndex());
            SaveChartOption("BasicLineChart", basicLineChartX.SelectedIndex, basicLineChartY.GetItemCheckIndex());
            SaveChartOption("BasicScatter", basicScatterX.SelectedIndex, basicScatterY.GetItemCheckIndex());
            SaveChartOption("SmoothedLineChart", smoothedLineChartX.SelectedIndex, smoothedLineChartY.GetItemCheckIndex());
            SaveChartOption("StackBar", stackBarX.SelectedIndex, stackBarY.GetItemCheckIndex());
            SaveChartOption("BasicPie", basicPieX.SelectedIndex, new List<int>() { basicPieY.SelectedIndex });
            SaveChartOption("BasicMap", basicMapX.SelectedIndex, new List<int>() { basicMapY.SelectedIndex });
        }

        private void LoadOption()
        {
            LoadData();

            //加载数据源、类型加载图表配置
            LoadChartOption("SimpleBar", simpleBarX, simpleBarY);
            LoadChartOption("BasicLineChart", basicLineChartX, basicLineChartY);
            LoadChartOption("BasicScatter", basicScatterX, basicScatterY);
            LoadChartOption("SmoothedLineChart", smoothedLineChartX, smoothedLineChartY);
            LoadChartOption("StackBar", stackBarX, stackBarY);
            LoadChartOption("BasicPie", basicPieX, basicPieY);
            LoadChartOption("BasicMap", basicMapX, basicMapY);
        }

        protected override bool OnOKButtonClick()
        {
            //前100行所有列的数据生成datatable、图表配置项生成chartOptions、生成js
            DataTable dataTable = GenDataTable(); 
            SavaOption();
            GenBossHtml.GetInstance().TransDataToHtml(dataTable, ChartOptions);

            return base.OnOKButtonClick();
        }
        private void SavaDataOption(string dataType, int idx)
        {
            ChartOptions[dataType] = new int[] { idx };
        }

        private void SaveChartOption(string chartType, int idxX, List<int> idxY)
        {
            List<int> tmpList = new List<int>();
            if (idxX < 0 || idxY.Count == 0 || (idxY.Count == 1 && idxY[0] == -1))
                return;

            tmpList.Add(idxX);
            idxY.ForEach(t => tmpList.Add(t));
            if (ChartOptions.ContainsKey(chartType))
                ChartOptions[chartType] = tmpList.ToArray();
            else
                ChartOptions.Add(chartType, tmpList.ToArray());
        }
        private void LoadData()
        {
            //初始化大屏类型
            if (!ChartOptions.ContainsKey("BossType") || ChartOptions["BossType"].Length == 0)
                bossType.SelectedIndex = 0;
            else
                bossType.SelectedIndex = ChartOptions["BossType"][0];
            //TODO phx 预览图也要跟着调整
            WebUrl = Path.Combine(Application.StartupPath, "IAOLab\\WebEngine\\Html", "BossIndex01.html");

            //初始化数据源
            foreach (DataItem dataItem in DataItems)
            {
                this.datasource.Items.Add(dataItem.FileName);
            }
            if (!ChartOptions.ContainsKey("Datasource") || ChartOptions["Datasource"].Length == 0)
                return;
            datasource.SelectedIndex = ChartOptions["Datasource"][0];
            selectData = DataItems[ChartOptions["Datasource"][0]];
            this.bcpInfo = new BcpInfo(selectData.FilePath, selectData.FileEncoding, new char[] { selectData.FileSep });
        }

        private void LoadChartOption(string chartType, Control controlX, Control controlY)
        {
            if (!ChartOptions.ContainsKey(chartType))
                return;
            //x控件一定是ComboBox,y控件可能是ComboBox或者是ComCheckBoxList
            int[] tmpIdx = ChartOptions[chartType];
            (controlX as ComboBox).SelectedIndex = tmpIdx[0];
            (controlX as ComboBox).Text = bcpInfo.ColumnArray[tmpIdx[0]];

            if (controlY is ComboBox)
            {
                (controlY as ComboBox).SelectedIndex = tmpIdx[1];
                (controlY as ComboBox).Text = bcpInfo.ColumnArray[tmpIdx[1]];
            }
            else if (controlY is ComCheckBoxList)
            {
                (controlY as ComCheckBoxList).LoadItemCheckIndex(tmpIdx.Skip(1).ToArray());
            }
        }

        private void Datasource_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectData = DataItems[this.datasource.SelectedIndex];
            
            this.bcpInfo = new BcpInfo(selectData.FilePath, selectData.FileEncoding, new char[] { selectData.FileSep });
            ChangeControlContent();
        }

        private void ChangeControlContent()
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
        private DataTable GenDataTable()
        {
            string fileContent;
            DataTable dataTable = new DataTable("temp");

            foreach (string col in bcpInfo.ColumnArray)
                dataTable.Columns.Add(col);

            if (selectData.FileType == OpUtil.ExtType.Excel)
                fileContent = BCPBuffer.GetInstance().GetCachePreviewExcelContent(selectData.FilePath);
            else
                fileContent = BCPBuffer.GetInstance().GetCachePreviewBcpContent(selectData.FilePath, selectData.FileEncoding);
            List<string> rows = new List<string>(fileContent.TrimEnd('\r').TrimEnd('\n').Split('\n'));
            int maxLine = Math.Min(rows.Count, MaxLine);

            //文件仅有表头无法画图表
            //if (maxLine <= 1)
            //    return false;

            for (int i = 1; i < maxLine; i++)
            {
                string row = rows[i].TrimEnd('\r');
                dataTable.Rows.Add(row.Split(selectData.FileSep));
            }

            return dataTable;
        }
    }
}
