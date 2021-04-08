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
using System.Drawing;
using System.IO;
using System.Linq;
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
        private int oldDataIdx;
        public Dictionary<string, int[]> ChartOptions;
        private WebBrowser webBrowser;

        private List<Image> bossTypeDict;

        public SelectBossDialog(List<DataItem> dataItems, Dictionary<string, int[]> options, WebBrowser browser)
        {
            InitializeComponent();
            oldDataIdx = -1;
            DataItems = dataItems;
            ChartOptions = options;
            bossTypeDict = new List<Image>() { Properties.Resources.BossStyle01 , Properties.Resources.BossStyle02 , Properties.Resources.BossStyle03, Properties.Resources.BossStyle04, Properties.Resources.BossStyle05 };
            LoadOption();
            webBrowser = browser;
        }

        //图表关联，保存配置
        private void SavaOption()
        {
            SavaDataOption("Datasource", datasource.SelectedIndex);
            SavaDataOption("BossType", bossType.SelectedIndex);
            SaveChartOption("SimpleBar", simpleBarX.SelectedIndex, simpleBarY.GetItemCheckIndex());
            SaveChartOption("BasicLineChart", basicLineChartX.SelectedIndex, basicLineChartY.GetItemCheckIndex());
            SaveChartOption("BasicScatter", basicScatterX.SelectedIndex, basicScatterY.GetItemCheckIndex());
            //SaveChartOption("SmoothedLineChart", smoothedLineChartX.SelectedIndex, smoothedLineChartY.GetItemCheckIndex());
            SaveChartOption("GradientLineChart", smoothedLineChartX.SelectedIndex, smoothedLineChartY.GetItemCheckIndex());
            SaveChartOption("StackBar", stackBarX.SelectedIndex, stackBarY.GetItemCheckIndex());
            //SaveChartOption("BasicPie", basicPieX.SelectedIndex, new List<int>() { basicPieY.SelectedIndex });
            SaveChartOption("PictorialBar", basicPieX.SelectedIndex, new List<int>() { basicPieY.SelectedIndex });
            SaveChartOption("BasicMap", basicMapX.SelectedIndex, new List<int>() { basicMapY.SelectedIndex });
        }

        private void LoadOption()
        {
            LoadData();

            //加载数据源、类型加载图表配置
            LoadChartOption("SimpleBar", simpleBarX, simpleBarY);
            LoadChartOption("BasicLineChart", basicLineChartX, basicLineChartY);
            LoadChartOption("BasicScatter", basicScatterX, basicScatterY);
            //LoadChartOption("SmoothedLineChart", smoothedLineChartX, smoothedLineChartY);
            LoadChartOption("GradientLineChart", smoothedLineChartX, smoothedLineChartY);
            LoadChartOption("StackBar", stackBarX, stackBarY);
            //LoadChartOption("BasicPie", basicPieX, basicPieY);
            LoadChartOption("PictorialBar", basicPieX, basicPieY);
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

            //初始化数据源
            foreach (DataItem dataItem in DataItems)
            {
                this.datasource.Items.Add(dataItem.FileName);
            }
            if (DataItems.Count == 0)//如果节点没数据，直接返回
                return;
            else if(!ChartOptions.ContainsKey("Datasource") || ChartOptions["Datasource"].Length == 0)//ChartOptions未配置，默认展示第一个文件的第1列
                selectData = DataItems[0];
            else
                selectData = DataItems[ChartOptions["Datasource"][0]];

            datasource.Text = selectData.FileName;
            this.bcpInfo = new BcpInfo(selectData.FilePath, selectData.FileEncoding, new char[] { selectData.FileSep });
        }

        private void LoadChartOption(string chartType, Control controlX, Control controlY)
        {
            int defaultIdx = 0;
            int[] tmpIdx;
            if (bcpInfo == null || bcpInfo.ColumnArray.Length == 0)
                return;

            if (!ChartOptions.ContainsKey(chartType) || ChartOptions["Datasource"].Length == 0)
                tmpIdx = new int[] { defaultIdx, defaultIdx};
            else
                tmpIdx = ChartOptions[chartType];

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
            if(oldDataIdx != -1)
                ChartOptions = new Dictionary<string, int[]>();
            oldDataIdx = datasource.SelectedIndex;
        }

        private void ChangeControlContent()
        {
            foreach(Control ct in this.panel1.Controls)
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

            if (bcpInfo == null || bcpInfo.ColumnArray.IsEmpty())
                return dataTable;
            foreach (string col in bcpInfo.ColumnArray)
                dataTable.Columns.Add(col);

            if (selectData.FileType == OpUtil.ExtType.Excel)
                fileContent = BCPBuffer.GetInstance().GetCachePreviewExcelContent(selectData.FilePath);
            else
                fileContent = BCPBuffer.GetInstance().GetCachePreviewBcpContent(selectData.FilePath, selectData.FileEncoding);
            List<string> rows = new List<string>(fileContent.TrimEnd('\r').TrimEnd('\n').Split('\n'));
            int maxLine = Math.Min(rows.Count, MaxLine);

            for (int i = 1; i < maxLine; i++)
            {
                string[] rowList = rows[i].TrimEnd('\r').Split(selectData.FileSep);
                List<string> tmpRowList = new List<string>();
                for (int j = 0; j< bcpInfo.ColumnArray.Length; j++)
                {
                    string cellValue = j < rowList.Length ? rowList[j] : "0";
                    tmpRowList.Add(cellValue);
                }
                dataTable.Rows.Add(tmpRowList.ToArray());
            }
            return dataTable;
        }

        void Enlarge()
        {
            var image = this.pictureBox1.Image;
            if (image == null)
                return;

            var dialog = new C2.Dialogs.PictureViewDialog(image);
            dialog.ImageName = this.bossType.Text;
            dialog.SetZoomType(ZoomType.FitPage);
            dialog.ShowDialog();

        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            Enlarge();
        }

        private void BossType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pictureBox1.Image = bossTypeDict[bossType.SelectedIndex];
            //this.label19.Text = bossType.Text;
            string str = bossType.Text;
            int arr = str.IndexOf("）")-1 - str.IndexOf("（");
            String str2 = str.Substring(str.IndexOf("（") + 1, arr);
            this.label19.Text = str2;

            WebUrl = Path.Combine(Application.StartupPath, "Business\\IAOLab\\WebEngine\\Html", string.Format("BossIndex0{0}.html",(bossType.SelectedIndex+1).ToString()));
            //切换样式，每个图表的配置标题要发生变化
            ChangeCaptionText();
        }


        private void ChangeCaptionText()
        {
            if (bossType.SelectedIndex == 0)
            {
                simpleBarCaption.Text = "柱状图（左上方）";
                basicLineChartCaption.Text = "折线图（左下方）";
                basicScatterCaption.Text = "点状图（中间下方）";
                smoothedLineChartCaption.Text = "曲线图（中间下方）";
                stackBarCaption.Text = "堆叠柱状图（右上方）";
                basicPieCaption.Text = "饼状图（右下方）";
                basicMapCaption.Text = "地市分布图（中间上方）";
            }
            else if (bossType.SelectedIndex == 1)
            {
                simpleBarCaption.Text = "柱状图（左上方）";
                basicLineChartCaption.Text = "折线图（右上方）";
                basicScatterCaption.Text = "点状图（左下方）";
                smoothedLineChartCaption.Text = "曲线图（右下方）";
                stackBarCaption.Text = "堆叠柱状图（不展示）";
                basicPieCaption.Text = "饼状图（不展示）";
                basicMapCaption.Text = "地市分布图（不展示）";
            }
            else if (bossType.SelectedIndex == 2)
            {
                simpleBarCaption.Text = "柱状图（左上方）";
                basicLineChartCaption.Text = "折线图（左下方）";
                basicScatterCaption.Text = "点状图（正中间）";
                smoothedLineChartCaption.Text = "曲线图（右上方）";
                stackBarCaption.Text = "堆叠柱状图（右下方）";
                basicPieCaption.Text = "饼状图（不展示）";
                basicMapCaption.Text = "地市分布图（不展示）";
            }
            else if (bossType.SelectedIndex == 3)
            {
                simpleBarCaption.Text = "柱状图（左上方）";
                basicLineChartCaption.Text = "折线图（右上方）";
                basicScatterCaption.Text = "点状图（左下方）";
                smoothedLineChartCaption.Text = "曲线图（右下方）";
                stackBarCaption.Text = "堆叠柱状图（不展示）";
                basicPieCaption.Text = "饼状图（不展示）";
                basicMapCaption.Text = "地市分布图（不展示）";
            }
            else if (bossType.SelectedIndex == 4)
            {
                simpleBarCaption.Text = "柱状图（左上方）";
                basicLineChartCaption.Text = "折线图（右上方）";
                basicScatterCaption.Text = "点状图（左下方）";
                smoothedLineChartCaption.Text = "曲线图（右下方）";
                stackBarCaption.Text = "堆叠柱状图（不展示）";
                basicPieCaption.Text = "饼状图（不展示）";
                basicMapCaption.Text = "地市分布图（不展示）";
            }
        }

        private async void PreviewBtn_ClickAsync(object sender, EventArgs e)
        {
            DataTable dataTable = GenDataTable();
            SavaOption();
            GenBossHtml.GetInstance().TransDataToHtml(dataTable, ChartOptions);
            webBrowser.Navigate(WebUrl);

            this.Cursor = Cursors.WaitCursor;
            await PageLoad(10);
            this.Cursor = Cursors.Default;

            Bitmap bitmap = new Bitmap(webBrowser.Width, webBrowser.Height);
            Rectangle rectangle = new Rectangle(0, 0, webBrowser.Width, webBrowser.Height);  // 绘图区域
            webBrowser.DrawToBitmap(bitmap, rectangle);
            this.pictureBox1.Image = bitmap;
        }

        private async Task PageLoad(int TimeOut)
        {
            TaskCompletionSource<bool> PageLoaded = null;
            PageLoaded = new TaskCompletionSource<bool>();
            int TimeElapsed = 0;
            webBrowser.DocumentCompleted += (s, e) =>
            {
                if (webBrowser.ReadyState != WebBrowserReadyState.Complete) 
                    return;
                if (PageLoaded.Task.IsCompleted) 
                    return; 
                PageLoaded.SetResult(true);
            };
            //
            while (PageLoaded.Task.Status != TaskStatus.RanToCompletion)
            {
                await Task.Delay(2000);//interval of 10 ms worked good for me
                TimeElapsed++;
                if (TimeElapsed >= TimeOut * 100) 
                    PageLoaded.TrySetResult(true);
            }
        }

        private void ZoomInBtn_Click(object sender, EventArgs e)
        {
            Enlarge();
        }
    }
}
