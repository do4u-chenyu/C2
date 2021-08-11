using C2.Business.IAOLab.WebEngine.Boss;
using C2.Business.Option;
using C2.Controls;
using C2.Controls.Common;
using C2.Core;
using C2.IAOLab.WebEngine.Boss;
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

namespace C2.Business.IAOLab.Visualization.Dialogs
{
    partial class SettingChartDialog : StandardDialog
    {
        private WebBrowser webBrowser;
        public string WebUrl;
        public Dictionary<string, string[]> Options;
        //type,各种idx

        private BcpInfo bcpInfo;
        private string dataFullPath;
        private List<string> chartTypeList;

        public SettingChartDialog()
        {
            InitializeComponent();
            WebUrl = Path.Combine(Application.StartupPath, "Business\\IAOLab\\WebEngine\\Html\\Visual.html");
            Options = new Dictionary<string, string[]>();
            this.chartType.SelectedIndex = 0;
            chartTypeList = new List<string> { "Organization", "WordCloud" };
        }

        public SettingChartDialog(Dictionary<string, string[]> options, WebBrowser browser) : this()
        {
            Options = options;
            webBrowser = browser;

            LoadOption();
        }

        protected override bool OnOKButtonClick()
        {

            //所有列的数据生成datatable、图表配置项生成chartOptions、生成js
            TranDataToHtml();
            return base.OnOKButtonClick();
        }

        private void TranDataToHtml()
        {
            DataTable dataTable = GenDataTable();
            SavaOption();
            GenVisualHtml.GetInstance().TransDataToHtml(dataTable, Options);
        }


        private void SavaOption()
        {
            Options["DataSourcePath"] = new string[] { this.dataSourcePath.Text };
            Options["ChartType"] = new string[] { chartTypeList[this.chartType.SelectedIndex] };
            //配置参数信息
            List<string> t = new List<string> { this.userCombox.SelectedIndex.ToString(), this.superiorCombox.SelectedIndex.ToString() };
            Options["Organization"] = TransListStringToInt(t, this.infoList.GetItemCheckIndex());
        }

        private string[] TransListStringToInt(List<string> t, List<int> listInt)
        {
            listInt.ForEach(c => t.Add(c.ToString()));
            return t.ToArray();
        }

        private void BrowserButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
            OpenFileDialog1.Filter = "文本文档 | *.txt;*.bcp";
            if (OpenFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            this.dataSourcePath.Text = OpenFileDialog1.FileName;
            dataFullPath = OpenFileDialog1.FileName;
            //同时需要把字段读出来data
            this.bcpInfo = new BcpInfo(dataFullPath, OpUtil.Encoding.UTF8, new char[] { '\t' });

            ChangeControlContent();
            Options = new Dictionary<string, string[]>();                
        }

        private void ChangeControlContent()
        {
            if(this.chartType.SelectedIndex == 0)
            {
                foreach (Control ct in this.panel1.Controls)
                {
                    if (ct is ComboBox)
                    {
                        (ct as ComboBox).Text = string.Empty;
                        (ct as ComboBox).Items.Clear();
                        (ct as ComboBox).Items.AddRange(bcpInfo.ColumnArray);
                    }
                    else if (ct is ComCheckBoxList)
                    {
                        (ct as ComCheckBoxList).ClearText();
                        (ct as ComCheckBoxList).Items.Clear();
                        (ct as ComCheckBoxList).Items.AddRange(bcpInfo.ColumnArray);
                    }
                }
            }
            else
            {
                foreach (Control ct in this.panel2.Controls)
                {
                    if (ct is ComboBox )
                    {
                        (ct as ComboBox).Text = string.Empty;
                        (ct as ComboBox).Items.Clear();
                        (ct as ComboBox).Items.AddRange(bcpInfo.ColumnArray);
                    }
                }
            }

        }

        private DataTable GenDataTable()
        {
            string fileContent;
            DataTable dataTable = new DataTable("temp");

            if (bcpInfo == null || bcpInfo.ColumnArray.IsEmpty())
                return dataTable;


            // 可能有同名列，这里需要重命名一下
            Dictionary<string, int> induplicatedName = new Dictionary<string, int>() { };
            foreach (string col in bcpInfo.ColumnArray)
            {
                if (!induplicatedName.ContainsKey(col))
                {
                    induplicatedName.Add(col, 0);
                    dataTable.Columns.Add(col);
                }
                else
                {
                    induplicatedName[col] += 1;
                    dataTable.Columns.Add(col + "_" + induplicatedName[col]);
                }
            }

            fileContent = BCPBuffer.GetInstance().GetCachePreviewBcpContent(dataFullPath, OpUtil.Encoding.UTF8);
            List<string> rows = new List<string>(fileContent.TrimEnd('\r').TrimEnd('\n').Split('\n'));
            //int maxLine = Math.Min(rows.Count, MaxLine);

            for (int i = 1; i < rows.Count; i++)
            {
                string[] rowList = rows[i].TrimEnd('\r').Split('\t');
                List<string> tmpRowList = new List<string>();
                for (int j = 0; j < bcpInfo.ColumnArray.Length; j++)
                {
                    string cellValue = j < rowList.Length ? rowList[j] : "0";
                    tmpRowList.Add(cellValue);
                }
                dataTable.Rows.Add(tmpRowList.ToArray());
            }
            return dataTable;
        }

        private void LoadOption()
        {
            LoadData();
            LoadChartOptions();
        }

        private void LoadData()
        {
            //初始化大屏类型
            if (Options.ContainsKey("ChartType") && Options["ChartType"].Length > 0)
                chartType.SelectedIndex = int.Parse(Options["ChartType"][0]);

            //初始化数据源
            if ( Options.ContainsKey("DataSourcePath") && Options["DataSourcePath"].Length > 0 && File.Exists(Options["DataSourcePath"][0]))
                dataSourcePath.Text = Options["DataSourcePath"][0];
            else
            {
                dataSourcePath.Text = string.Empty;
                return;//没有数据，后面全部清空；有数据，再考虑初始化其他的
            }

            //初始化参数
        }

        private void LoadChartOptions()
        {
            //加载数据源、类型加载图表配置

        }

        private void ChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.chartType.SelectedIndex == 0)
            {
                this.panel1.Show();
                this.panel2.Hide();
                this.pictureBox1.Image = C2.Properties.Resources.社交关系图样例;
            }
            else if(this.chartType.SelectedIndex == 1)
            {
                this.panel1.Hide();
                this.panel2.Show();
                this.pictureBox1.Image = C2.Properties.Resources.词云样例;
            }
                
        }
    }
}
