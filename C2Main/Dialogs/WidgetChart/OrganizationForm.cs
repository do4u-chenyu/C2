using C2.Business.Option;
using C2.Controls;
using C2.Core;
using C2.Dialogs.Base;
using C2.Model;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace C2.Dialogs.WidgetChart
{
    partial class OrganizationForm : StandardDialog
    {
        private BcpInfo bcpInfo;
        public Dictionary<string, string[]> Options;
        private DataItem hitItem;
        public List<string> returnList;

        private string FilePath { get => hitItem.FilePath; }
        private OpUtil.Encoding FileEncoding { get => hitItem.FileEncoding; }
        private char FileSep { get => hitItem.FileSep; }
        public OrganizationForm(DataItem hitItem, List<string> returnList)
        {
            this.hitItem = hitItem;
            this.returnList = returnList;
            InitializeComponent();
            InitializeDropDown();
        }

        private void InitializeDropDown()
        {
            Options = new Dictionary<string, string[]>();
            this.bcpInfo = new BcpInfo(FilePath, FileEncoding, new char[] { FileSep });
            this.comboBox1.Items.AddRange(bcpInfo.ColumnArray);
            this.comboBox2.Items.AddRange(bcpInfo.ColumnArray);
            this.infoList.Items.AddRange(bcpInfo.ColumnArray);

        }
        public OrganizationForm(Dictionary<string, string[]> options)
        {
            this.Options = options;
        }

        protected override bool OnOKButtonClick()
        {
            if (this.comboBox1.SelectedIndex == -1 || this.comboBox2.SelectedIndex == -1)
            {
                HelpUtil.ShowMessageBox("有未配置的参数，请重新配置后再确定。");
                return false;
            }
            
            DataTable dataTable = GenDataTable(FilePath, bcpInfo);
            List<string> t = new List<string> { this.comboBox1.SelectedIndex.ToString(), this.comboBox2.SelectedIndex.ToString() };
            Options["Organization"] = TransListStringToInt(t, this.infoList.GetItemCheckIndex());

            string[] chartOptions = Options["Organization"];
            string[] colName = new string[chartOptions.Length];
            string[][] colList = new string[chartOptions.Length][];
            for (int i = 0; i < chartOptions.Length; i++)
            {
                colList[i] = new string[dataTable.Rows.Count];
                colName[i] = dataTable.Columns[int.Parse(chartOptions[i])].ColumnName;
                colList[i] = dataTable.AsEnumerable().Select(peo => peo.Field<string>(colName[i])).ToArray();
            }
            int k = 0;
            int rootIndex = -1;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (colList[1].Except(colList[0]).ToList().Count == 1 && colList[1][i] == colList[1].Except(colList[0]).ToList()[0])
                {
                    k = k + 1;
                    rootIndex = i;
                }
            }
            if (k != 1)
            {
                MessageBox.Show("输入的数据根节点个数不正确，请重新输入");
                return false;
            }
            else
            {
                returnList = Trans(dataTable, chartOptions, rootIndex);
                return base.OnOKButtonClick();
            }
            
        }

        private string[] TransListStringToInt(List<string> t, List<int> listInt)
        {
            listInt.ForEach(c => t.Add(c.ToString()));
            return t.ToArray();
        }

        private DataTable GenDataTable(string path, BcpInfo bcpInfo)
        {
            DataTable dataTable = new DataTable(Path.GetFileNameWithoutExtension(path));

            if (bcpInfo == null || bcpInfo.ColumnArray.IsEmpty())
                return dataTable;

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
            List<string> rows = GetFileLines(path);
            for (int i = 1; i < rows.Count; i++)
            {
                string[] rowList = rows[i].TrimEnd('\r').Split('\t');
                List<string> tmpRowList = new List<string>();
                for (int j = 0; j < bcpInfo.ColumnArray.Length; j++)
                {
                    string cellValue = j < rowList.Length ? rowList[j] : string.Empty;
                    tmpRowList.Add(cellValue);
                }
                dataTable.Rows.Add(tmpRowList.ToArray());
            }
            return dataTable;
        }

        private List<string> GetFileLines(string path)
        {
            int lineCount = 0;
            List<string> contentList = new List<string>();
            FileStream fs_dir = null;
            StreamReader reader = null;
            try
            {
                fs_dir = new FileStream(path, FileMode.Open, FileAccess.Read);
                reader = new StreamReader(fs_dir);
                string lineStr;
                while ((lineStr = reader.ReadLine()) != null && lineCount < 1001)
                {
                    if (!string.IsNullOrEmpty(lineStr))
                    {
                        contentList.Add(lineStr);
                        lineCount++;
                    }
                }
            }
            catch { }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (fs_dir != null)
                {
                    fs_dir.Close();
                }
            }
            return contentList;
        }

        public List<string> Trans(DataTable dataTable, string[] chartOptions, int index)
        {
            List<Dictionary<string, object>> testData = new List<Dictionary<string, object>>();

            string[] colName = new string[chartOptions.Length];
            string[][] colList = new string[chartOptions.Length][];
            for (int i = 0; i < chartOptions.Length; i++)
            {
                colList[i] = new string[dataTable.Rows.Count];
                colName[i] = dataTable.Columns[int.Parse(chartOptions[i])].ColumnName;
                colList[i] = dataTable.AsEnumerable().Select(peo => peo.Field<string>(colName[i])).ToArray();
            }

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Dictionary<string, object> node = new Dictionary<string, object>();
                string infoFormatter = string.Empty;
                string secondFormatter = string.Empty;
                node.Add("user", colList[0][i]);
                node.Add("up", colList[1][i]);
                if (chartOptions.Length > 2)
                {
                    for (int j = 2; j < chartOptions.Length; j++)
                    {
                        secondFormatter = secondFormatter + colName[j] + ": " + colList[j][i]+ "<br>";
                    }
                }
                infoFormatter = infoFormatter + secondFormatter;
                node.Add("label", infoFormatter);

                List<Dictionary<string, object>> nullList = new List<Dictionary<string, object>>();
                node.Add("children", nullList);
                testData.Add(node);
            }

            for (int i = 0; i < testData.Count; i++)
            {
                for (int j = 0; j < testData.Count; j++)
                {
                    if (testData[j]["up"].ToString() == testData[i]["user"].ToString())
                    {
                        (testData[i]["children"] as List<Dictionary<string, object>>).Add(testData[j]);
                    }
                }
            }

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string strDATAJSON = jss.Serialize(testData[index]);
            List<string> returnList = new List<string>() { '[' + strDATAJSON + ']'};
            return returnList;
        }
    }
}