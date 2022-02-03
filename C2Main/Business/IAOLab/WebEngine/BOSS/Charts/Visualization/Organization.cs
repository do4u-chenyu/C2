using C2.IAOLab.WebEngine.Boss.Charts;
using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType;
using C2.IAOLab.WebEngine.Boss.Option.BaseOption;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace C2.Business.IAOLab.WebEngine.Boss.Charts.Visualization
{
    class Organization : BaseCharts
    {
        public Organization(DataTable dataTable, CompleteOption option, Dictionary<string, string[]> chartOptionDict)
        {
            if (!chartOptionDict.ContainsKey("Organization") || chartOptionDict["Organization"].Length == 0)
                return;
            /*
             * chartOptions数组索引对应关系
             * 0     ：下级所在列
             * 1     ：上级所在列
             * 2-len ：信息所在列
             * 
             * 注：chartOptions[i] 得到的列为string，需要int.Parse转换后再去dataTable取值
             */
            string[] chartOptions = chartOptionDict["Organization"];

            for (int i = 0; i<chartOptions.Length; i++)
            {
                for (int j = 0; j < chartOptions.Length; j++)
                {
                    if  (i != j && dataTable.Columns[int.Parse(chartOptions[i])] == dataTable.Columns[int.Parse(chartOptions[j])])
                    {
                        MessageBox.Show("输入的数据中有相同的列，请重新输入！");
                        return;
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
                return;
            }
            //option.dataset = Common.FormatDatas;
            List<string> returnList = Trans(dataTable, chartOptions, rootIndex);
            string data = returnList[0];
            string tooltipHeight = returnList[1];

            List<ISeries> series = new List<ISeries>();
            option.tooltip = new Tooltip()
            {
                trigger = "'item'",
                triggerOn = TriggerOn.mousemove,
            };
            series.Add(new SeriesTree()
            {
                orient = "'TB'",
                type = "'tree'",
                symbol = "'roundRect'",
                symbolSize = "[90,"+tooltipHeight+"]",
                left = "'1%'",
                right = "'1%'",
                edgeShape = "'polyline'",
                expandAndCollapse = "true",
                edgeForkPosition = "'50%'",
                initialTreeDepth = 3,
                animationDuration = 550,
                animationDurationUpdate = 750,
                data = data,
            }) ;
            option.series = new Series(series.ToArray());
            _initScript = option.ToString();
        }

        public List<string> Trans(DataTable dataTable, string[] chartOptions, int index)
        {
            List<Dictionary<string, object>> testData = new List<Dictionary<string, object>>();

            int inPut_Length = chartOptions.Length;
            int defaultLength = 5;
            int initialHeight = 0;
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
                    initialHeight = 1;
                }
                chartOptions = chartOptions_Select;
            }
            int infoHeight = 18;
            int toolHeight = 45 + infoHeight * (chartOptions.Length - 2 + initialHeight);

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
                object firstDictionary = new Dictionary<string, object>();
                object secondDictionary = new Dictionary<string, object>();
                object radius = new List<int>() { 5, 5, 5, 5 };
                object radius1 = new List<int>() { 5, 5, 0, 0 };
                object radius2 = new List<int>() { 0, 0, 5, 5 };

                (labelDictionary as Dictionary<string, object>).Add("backgroundColor", "#F4F4F4");
                (labelDictionary as Dictionary<string, object>).Add("borderRadius", radius);
                (firstDictionary as Dictionary<string, object>).Add("backgroundColor", "#078E34");
                (firstDictionary as Dictionary<string, object>).Add("color", "#fff");
                (firstDictionary as Dictionary<string, object>).Add("align", "center");
                (firstDictionary as Dictionary<string, object>).Add("fontsize", 20);

                string secondList = "}\n{second|";
                List<int> lengthList = new List<int>();
                string toolFormatter = colName[0] + ":" + colList[0][i];
                string secondFormatter = "  ";

                if (chartOptions.Length == 2)
                {
                    lengthList.Add(0);
                    //secondList = string.Empty;
                }
                else if (chartOptions.Length > 2)
                {
                    for (int j = 2; j < chartOptions.Length; j++)
                    {
                        int infoLength = 8;
                        secondFormatter = secondFormatter + "  " + colName[j] + ":" + colList[j][i];
                        if (colList[j][i] == "")
                        {
                            infoLength = colList[j][i].Length;
                            secondList = secondList + colList[j][i];
                        }
                        else if (colList[j][i].Length <= infoLength)
                        {
                            secondList = secondList + colName[j] + ":" + colList[j][i];
                            infoLength = colList[j][i].Length + colName[j].Length;
                        }
                        else if (colList[j][i].Length > infoLength)
                        {
                            string infoValue = colList[j][i].Substring(0, infoLength) + "...";
                            secondList = secondList + "  " + colName[j] + ":" + infoValue;
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
                if (colList[0][i].Length > 8)
                {
                    colList[0][i] = colList[0][i].Substring(0, 8) + "...";
                }
                List<int> allLength = new List<int>() { lengthList.Max(), colList[0][i].Length, 8 };
                int labelWidth = allLength.Max() * 12;
                (firstDictionary as Dictionary<string, object>).Add("width", labelWidth);

                if (lengthList.Max() != 0)
                {
                    (labelDictionary as Dictionary<string, object>).Add("formatter", "{first|" + colList[0][i] + secondList + "}");
                    (firstDictionary as Dictionary<string, object>).Add("height", 50);
                    (firstDictionary as Dictionary<string, object>).Add("borderRadius", radius1);
                    (secondDictionary as Dictionary<string, object>).Add("color", "#888");
                    (secondDictionary as Dictionary<string, object>).Add("align", "center");
                    (secondDictionary as Dictionary<string, object>).Add("width", labelWidth);
                    (secondDictionary as Dictionary<string, object>).Add("height", infoHeight);
                    (secondDictionary as Dictionary<string, object>).Add("borderRadius", radius2);
                    (secondDictionary as Dictionary<string, object>).Add("fontsize", 20);
                    (infoDictionary as Dictionary<string, object>).Add("second", secondDictionary);
                }
                else
                {
                    (labelDictionary as Dictionary<string, object>).Add("formatter", "{first|" + colList[0][i] + "}");
                    (firstDictionary as Dictionary<string, object>).Add("height", 50 + infoHeight * (chartOptions.Length - 2 + initialHeight));
                    (firstDictionary as Dictionary<string, object>).Add("borderRadius", radius);
                }

                (toolDictionary as Dictionary<string, object>).Add("formatter", toolFormatter);
                (infoDictionary as Dictionary<string, object>).Add("first", firstDictionary);
                (labelDictionary as Dictionary<string, object>).Add("rich", infoDictionary);

                node.Add("label", labelDictionary);
                node.Add("tooltip", toolDictionary);

                node.Add("user", colList[0][i]);
                node.Add("up", colList[1][i]);
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
            for (int i = 0; i < testData.Count; i++)
            {
                testData[i].Remove("user");
                testData[i].Remove("up");
            }

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string strDATAJSON = jss.Serialize(testData[index]);
            List<string> returnList = new List<string>() { '[' + strDATAJSON + ']', toolHeight.ToString() };
            return returnList;
        }
    }
}

