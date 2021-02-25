using C2.Business.Option;
using C2.Controls;
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
        public string WebUrl;
        public List<DataItem> DataItems;
        private BcpInfo bcpInfo;
        private DataItem selectData;
        private static readonly int MaxLine = 100;

        public SelectBossDialog(List<DataItem> dataItems)
        {
            InitializeComponent();
            WebUrl = Path.Combine(Application.StartupPath, "IAOLab\\WebEngine\\Html", "BossIndex01.html");
            DataItems = dataItems;
            foreach (DataItem dataItem in DataItems)
            {
                this.comboBox1.Items.Add(dataItem.FileName);
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
            Dictionary<string, string[]> chartOptions = new Dictionary<string, string[]>();  
            chartOptions.Add("SimpleBar", GetChartOption(simpleBarX.SelectedIndex, simpleBarY.GetItemCheckIndex()));
            chartOptions.Add("BasicLineChart", GetChartOption(basicLineChartX.SelectedIndex, basicLineChartY.GetItemCheckIndex()));
            chartOptions.Add("BasicScatter", GetChartOption(basicScatterX.SelectedIndex, basicScatterY.GetItemCheckIndex()));
            chartOptions.Add("SmoothedLineChart", GetChartOption(smoothedLineChartX.SelectedIndex, smoothedLineChartY.GetItemCheckIndex()));
            chartOptions.Add("StackBar", GetChartOption(stackBarX.SelectedIndex, stackBarY.GetItemCheckIndex()));
            chartOptions.Add("BasicPie", GetChartOption(basicPieX.SelectedIndex, new List<int>() { basicPieY.SelectedIndex }));
            chartOptions.Add("BasicMap", GetChartOption(basicMapX.SelectedIndex, new List<int>() { basicMapY.SelectedIndex }));

            //3、调用生成js
            GenBossHtml.GetInstance().TransDataToHtml(dataTable, chartOptions);
            return base.OnOKButtonClick();
        }

        private string[] GetChartOption(int idxX, List<int> idxY)
        {
            List<string> tmpList = new List<string>();
            if (idxX != -1 && idxY.Count > 0)
            {
                tmpList.Add(bcpInfo.ColumnArray[idxX]);
                idxY.ForEach(t => tmpList.Add(bcpInfo.ColumnArray[t]));
            }
            return tmpList.ToArray();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectData = DataItems[this.comboBox1.SelectedIndex];
            
            this.bcpInfo = new BcpInfo(selectData.FilePath, selectData.FileEncoding, new char[] { selectData.FileSep });
            this.simpleBarX.Items.Clear();
            this.simpleBarY.Items.Clear();
            this.basicLineChartX.Items.Clear();
            this.basicLineChartY.Items.Clear();
            this.basicScatterX.Items.Clear();
            this.basicScatterY.Items.Clear();
            this.smoothedLineChartX.Items.Clear();
            this.smoothedLineChartY.Items.Clear();
            this.stackBarX.Items.Clear();
            this.stackBarY.Items.Clear();
            this.basicPieX.Items.Clear();
            this.basicPieY.Items.Clear();
            this.basicMapX.Items.Clear();
            this.basicMapY.Items.Clear();

            this.simpleBarX.Items.AddRange(bcpInfo.ColumnArray);
            this.simpleBarY.Items.AddRange(bcpInfo.ColumnArray);
            this.basicLineChartX.Items.AddRange(bcpInfo.ColumnArray);
            this.basicLineChartY.Items.AddRange(bcpInfo.ColumnArray);
            this.basicScatterX.Items.AddRange(bcpInfo.ColumnArray);
            this.basicScatterY.Items.AddRange(bcpInfo.ColumnArray);
            this.smoothedLineChartX.Items.AddRange(bcpInfo.ColumnArray);
            this.smoothedLineChartY.Items.AddRange(bcpInfo.ColumnArray);
            this.stackBarX.Items.AddRange(bcpInfo.ColumnArray);
            this.stackBarY.Items.AddRange(bcpInfo.ColumnArray);
            this.basicPieX.Items.AddRange(bcpInfo.ColumnArray);
            this.basicPieY.Items.AddRange(bcpInfo.ColumnArray);
            this.basicMapX.Items.AddRange(bcpInfo.ColumnArray);
            this.basicMapY.Items.AddRange(bcpInfo.ColumnArray);
        }
    }
}
