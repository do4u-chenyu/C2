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
        private string FilePath { get => hitItem.FilePath; }
        private OpUtil.Encoding FileEncoding { get => hitItem.FileEncoding; }
        private char FileSep { get => hitItem.FileSep; }
        public OrganizationForm(DataItem hitItem)
        {
            this.hitItem = hitItem;
            InitializeComponent();
            InitializeDropDown();
        }

        private void InitializeDropDown()
        {
            Options = new Dictionary<string, string[]>();
            //this.comboBox1.SelectedIndex = 0;
            //this.comboBox2.SelectedIndex = 0;
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
            for (int i = 0; i < chartOptions.Length; i++)
            {
                for (int j = 0; j < chartOptions.Length; j++)
                {
                    if (i != j && dataTable.Columns[int.Parse(chartOptions[i])] == dataTable.Columns[int.Parse(chartOptions[j])])
                    {
                        MessageBox.Show("输入的数据中有相同的列，请重新输入！");
                        //return;
                    }
                }
            }
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
                //return;
            }
            List<string> returnList = Trans(dataTable, chartOptions, rootIndex);
            return base.OnOKButtonClick();
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


            List<string> rows = GetFileLines(path);

            for (int i = 1; i < rows.Count; i++)
            {
                string[] rowList = rows[i].TrimEnd('\r').Split('\t');
                List<string> tmpRowList = new List<string>();
                for (int j = 0; j < bcpInfo.ColumnArray.Length; j++)
                {
                    string cellValue = j < rowList.Length ? rowList[j] : "";
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
                    if (!lineStr.Equals(""))
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

            int inPut_Length = chartOptions.Length;
            int defaultLength = 5;

            string[] chartOptions_Select = new string[defaultLength];

            int restCount = 0;
            if (inPut_Length > defaultLength)
            {
                restCount = chartOptions.Length - defaultLength;
            }
            string[] colName_Rest = new string[restCount];
            string[][] colList_Rest = new string[restCount][];

            if (chartOptions.Length > defaultLength)
            {
                for (int i = defaultLength; i < chartOptions.Length; i++)
                {
                    colList_Rest[i - defaultLength] = new string[dataTable.Rows.Count];
                    colName_Rest[i - defaultLength] = dataTable.Columns[int.Parse(chartOptions[i])].ColumnName;
                    colList_Rest[i - defaultLength] = dataTable.AsEnumerable().Select(peo => peo.Field<string>(colName_Rest[i - defaultLength])).ToArray();
                }
                for (int j = 0; j < defaultLength; j++)
                {
                    chartOptions_Select[j] = chartOptions[j];
                }
                chartOptions = chartOptions_Select;
            }

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
                object labelDictionary = new Dictionary<string, object>();
                object toolDictionary = new Dictionary<string, object>();
                object infoDictionary = new Dictionary<string, object>();

                string secondList = "}\n{second|";
                List<int> lengthList = new List<int>();
                string toolFormatter = "";
                string secondFormatter = "";
                node.Add("user", colList[0][i]);
                node.Add("up", colList[1][i]);
                if (chartOptions.Length == 2)
                {
                    lengthList.Add(0);
                }
                else if (chartOptions.Length > 2)
                {
                    for (int j = 2; j < chartOptions.Length; j++)
                    {
                        int infoLength = 8;
                        secondFormatter = secondFormatter + " " + colName[j] + ":" + colList[j][i];
                        if (colList[j][i] == "")
                        {
                            secondList = secondList + colList[j][i];
                        }
                        else if (colList[j][i].Length <= infoLength)
                        {
                            secondList = secondList + colName[j] + ":" + colList[j][i];
                        }
                        else if (colList[j][i].Length > infoLength)
                        {
                            string infoValue = colList[j][i].Substring(0, infoLength) + "...";
                            secondList = secondList + "" + colName[j] + ":" + infoValue;
                            infoLength = infoValue.Length + colName[j].Length - 1;
                        }
                        if (j < chartOptions.Length - 1)
                        {
                            secondList = secondList + "\n";
                        }
                        lengthList.Add(infoLength);
                    }
                    if (inPut_Length > defaultLength)
                    {
                        secondList = secondList + "\n" + "...";
                        for (int j = 0; j < inPut_Length - defaultLength; j++)
                        {
                            secondFormatter = secondFormatter + "  " + colName_Rest[j] + ":" + colList_Rest[j][i];
                        }
                    }
                }
                toolFormatter = toolFormatter + secondFormatter;

                if (lengthList.Max() != 0)
                {
                    (labelDictionary as Dictionary<string, object>).Add("formatter", "{first|" + colList[0][i] + secondList + "}");
                }
                else
                {
                    (labelDictionary as Dictionary<string, object>).Add("formatter", "{first|" + colList[0][i] + "}");
                }

                (toolDictionary as Dictionary<string, object>).Add("formatter", toolFormatter);
                (labelDictionary as Dictionary<string, object>).Add("rich", infoDictionary);

                node.Add("label", toolFormatter);

                
                List<Dictionary<string, object>> nullList2 = new List<Dictionary<string, object>>();
                node.Add("children", nullList2);
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