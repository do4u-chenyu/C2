using C2.Business.IAOLab.WebEngine.Boss.Option.BaseOption;
using C2.IAOLab.WebEngine.Boss.Charts;
using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WebEngine.Boss.Option.BaseOption;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace C2.Business.IAOLab.WebEngine.Boss.Charts.Visualization
{
    class WordCloud : BaseCharts
    {
        public WordCloud(DataTable dataTable, CompleteOption option, Dictionary<string, string[]> chartOptionDict)
        {
            if (!chartOptionDict.ContainsKey("WordCloud") || chartOptionDict["WordCloud"].Length == 0)
                return;

            string[] chartOptions = chartOptionDict["WordCloud"];

            for (int i = 0; i < chartOptions.Length; i++)
            {
                for (int j = 0; j < chartOptions.Length; j++)
                {
                    if (i != j && dataTable.Columns[int.Parse(chartOptions[i])] == dataTable.Columns[int.Parse(chartOptions[j])])
                    {
                        MessageBox.Show("输入的数据中有相同的列，请重新输入！");
                        return;
                    }
                }
            }

            string data = Trans(dataTable, chartOptions);
            List<ISeries> series = new List<ISeries>();
            option.title = new Title()
            {
                text = "'词云图'",
                left = "'center'",
                textStyle = new TextStyle()
                {
                    fontSize = 30,
                    fontFamily = FontFamily.serif,
                    fontWeight = FontWeight.normal,
                }
            };
            option.backgroundColor = "'#F7F7F7'";
            option.tooltip = new Tooltip()
            {
                show ="true",
                trigger = "'item'",
                triggerOn = TriggerOn.mousemove,
            };
            series.Add(new SeriesWordCloud()
            {
                type = "'wordCloud'",
                gridSize = "5",
                shape = "'circle'",
                rotationRange = "[-45,45]",
                rotationStep = "45",
                sizeRange = "[10,60]",
                drawOutOfBound = "false",
                data = data,
                textStyle = new TextStyle()
                {
                    normal = new Normal()
                    {
                        color = "function () {return ('rgb(' + [Math.round(Math.random() * 160),Math.round(Math.random() * 160),Math.round(Math.random() * 160)].join(',') + ')');}",
                        fontFamily = FontFamily.serif,
                        fontWeight = FontWeight.normal,
                    },
                    emphasis = new Emphasis()
                    {
                        shadowBlur = 5,
                        shadowColor = "'#333'",
                    },
                },
            }) ;

            option.series = new Series(series.ToArray());
            _initScript = option.ToString();
        }

        public string Trans(DataTable dataTable, string[] chartOptions)
        {
            string[] colName = new string[chartOptions.Length];
            string[][] colList = new string[chartOptions.Length][];
            for (int i = 0; i < chartOptions.Length; i++)
            {
                colList[i] = new string[dataTable.Rows.Count];
                colName[i] = dataTable.Columns[int.Parse(chartOptions[i])].ColumnName;
                colList[i] = dataTable.AsEnumerable().Select(peo => peo.Field<string>(colName[i])).ToArray();
            }
            
            string testData = "[";
            for (int i=0; i < dataTable.Rows.Count; i++)
            {
                if (colList[1][i] != "")
                {
                    Dictionary<string, string> wordDictionary = new Dictionary<string, string>();
                    wordDictionary.Add("name", colList[0][i]);
                    wordDictionary.Add("value", colList[1][i]);
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    string strData = jss.Serialize(wordDictionary);
                    testData = testData + strData + ",";
                }
            }
            return testData+ "]";
        }
    }
}