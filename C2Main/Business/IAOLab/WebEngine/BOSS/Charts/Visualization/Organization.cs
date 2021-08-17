using C2.IAOLab.WebEngine.Boss.Charts;
using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType;
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
            //option.dataset = Common.FormatDatas;
            string data = Trans(dataTable, chartOptions);
            List<ISeries> series = new List<ISeries>();
            option.tooltip = new Tooltip()
            {
                trigger = "'item'",
                triggerOn = C2.IAOLab.WebEngine.Boss.Option.BaseOption.TriggerOn.mousemove
            };
            series.Add(new SeriesTree()
            {
                orient = "'TB'",
                type = "'tree'",
                symbol = "'rect'",
                edgeShape = "'polyline'",
                expandAndCollapse = "true",
                edgeForkPosition = "'50%'",
                initialTreeDepth = 3,
                animationDuration = 550,
                animationDurationUpdate = 750,
                data = data,
            });
            option.series = new Series(series.ToArray());
            _initScript = option.ToString();
        }

        public string Trans(DataTable dataTable, string[] chartOptions)
        {
            List<Dictionary<string, object>> testData = new List<Dictionary<string, object>>();

            if (chartOptions.Length > 5)
            {
                string[] chartOptions_Select = new string[5];
                for (int i = 0; i < 5; i++)
                    chartOptions_Select[i] = chartOptions[i];
                chartOptions = chartOptions_Select;
            }

            string[] colName = new string[chartOptions.Length];
            string[][] colList = new string[chartOptions.Length][];
            for (int i = 0; i < chartOptions.Length; i++)
            {
                colList[i] = new string[dataTable.Rows.Count];
                colName[i] = dataTable.Columns[int.Parse(chartOptions[i])].ColumnName;
                for (int j = 0; j < dataTable.Rows.Count; j++)
                {
                    colList[i] = dataTable.AsEnumerable().Select(peo => peo.Field<string>(colName[i])).ToArray();
                }
            }

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                object labelDictionary = new Dictionary<string, object>();
                object infoDictionary = new Dictionary<string, object>();
                object firstDictionary = new Dictionary<string, object>();
                object secondDictionary = new Dictionary<string, object>();
                object radius = new List<int>() { 5, 5, 5, 5 };
                object radius1 = new List<int>() { 5, 5, 0, 0 };
                object radius2 = new List<int>() { 0, 0, 5, 5 };

                (labelDictionary as Dictionary<string, object>).Add("backgroundColor", "#F4F4F4");
                (labelDictionary as Dictionary<string, object>).Add("borderRadius", radius);
                string secondList = "}\n{second|";
                int infoHeight = 15;

                List<int> lengthList = new List<int>();
                for (int j = 2; j < chartOptions.Length; j++)
                {
                    string infoValue = colList[j][i];
                    int infoLength = infoValue.Length;
                    if (infoValue == "")
                    {
                        secondList = secondList + infoValue + "\n";
                    }
                    else if (infoValue.Length <= 6)
                    {
                        infoLength = 6;
                        secondList = secondList + colName[j] + ":" + infoValue + "\n";
                    }
                    else if (infoValue.Length > 6)
                    {
                        infoValue = infoValue.Substring(0, 6) + "...";
                        secondList = secondList + colName[j] + ":" + infoValue + "\n";
                        infoLength = infoValue.Length + colName[j].Length;
                    }
                    lengthList.Add(infoLength);
                }

                if (chartOptions.Length < dataTable.Columns.Count)
                {
                    secondList = secondList + "...";
                }
                List<int> allLength = new List<int>() { lengthList.Max(), colList[0][i].Length, 6 };
                int labelWidth = allLength.Max() * 12;

                (firstDictionary as Dictionary<string, object>).Add("backgroundColor", "#078E34");
                (firstDictionary as Dictionary<string, object>).Add("color", "#fff");
                (firstDictionary as Dictionary<string, object>).Add("align", "center");
                (firstDictionary as Dictionary<string, object>).Add("width", labelWidth);

                if (lengthList.Max() != 0)
                {
                    (labelDictionary as Dictionary<string, object>).Add("formatter", "{first|" + colList[0][i] + secondList + "}");
                    (firstDictionary as Dictionary<string, object>).Add("height", 32);
                    (firstDictionary as Dictionary<string, object>).Add("borderRadius", radius1);
                    (secondDictionary as Dictionary<string, object>).Add("color", "#888");
                    (secondDictionary as Dictionary<string, object>).Add("align", "center");
                    (secondDictionary as Dictionary<string, object>).Add("width", labelWidth);
                    (secondDictionary as Dictionary<string, object>).Add("height", infoHeight);
                    (secondDictionary as Dictionary<string, object>).Add("borderRadius", radius2);
                    (infoDictionary as Dictionary<string, object>).Add("second", secondDictionary);
                }
                else
                {
                    (labelDictionary as Dictionary<string, object>).Add("formatter", "{first|" + colList[0][i] + "}");
                    (firstDictionary as Dictionary<string, object>).Add("height", 32 + infoHeight * (chartOptions.Length - 1));
                    (firstDictionary as Dictionary<string, object>).Add("borderRadius", radius);
                }

                (infoDictionary as Dictionary<string, object>).Add("first", firstDictionary);
                (labelDictionary as Dictionary<string, object>).Add("rich", infoDictionary);
                result.Add("label", labelDictionary);
                result.Add("name", colList[0][i]);
                result.Add("up", colList[1][i]);
                List<Dictionary<string, object>> nullList2 = new List<Dictionary<string, object>>();
                result.Add("children", nullList2);
                testData.Add(result);

            }
            for (int i = 0; i < testData.Count; i++)
            {
                for (int j = 0; j < testData.Count; j++)
                {
                    if (testData[j]["up"].ToString() == testData[i]["name"].ToString())
                    {
                        (testData[i]["children"] as List<Dictionary<string, object>>).Add(testData[j]);
                    }
                }
            }
            for (int i = 0; i < testData.Count; i++)
            {
                testData[i].Remove("name");
                testData[i].Remove("up");
            }
            int k = 0;
            int rootIndex = -1;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (colList[1].Except(colList[0]).ToList().Count == 1 && colList[1][i].ToString() == colList[1].Except(colList[0]).ToList()[0].ToString())
                {
                    k = k + 1;
                    rootIndex = i;
                }
            }

            if (k != 1)
            {
                MessageBox.Show("输入的数据根节点个数不正确，请重新输入");
                return "";
            }
            else
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                string strDATAJSON = jss.Serialize(testData[rootIndex]);
                return '[' + strDATAJSON + ']';
            }
        }
    }
}

