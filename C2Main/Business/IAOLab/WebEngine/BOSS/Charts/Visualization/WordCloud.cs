using C2.Business.IAOLab.WebEngine.Boss.Option.BaseOption;
using C2.IAOLab.WebEngine.Boss.Charts;
using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WebEngine.Boss.Option.BaseOption;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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

            for (int i = 0; i < chartOptions.Length-1; i++)
            {
                for (int j = 0; j < chartOptions.Length-1; j++)
                {
                    if (i != j && dataTable.Columns[int.Parse(chartOptions[i])] == dataTable.Columns[int.Parse(chartOptions[j])])
                    {
                        MessageBox.Show("输入的数据中有相同的列，请重新输入！");
                        return;
                    }
                }
            }

            List<string> style1 = new List<string>() { "'circle'", "[-90,90]", "5", "'#F7F7F7'" };
            List<string> style2 = new List<string>() { "'diamond'", "[-45,90]", "10", "'#00023f'" };
            List<string> style3 = new List<string>() { "'star'", "[-45,45]", "5", "'#F7F7F7'" };
            List<string> style4 = new List<string>() { "'pentagon'", "[-90,90]", "20", "'#F7F7F7'" };
            Dictionary<string, List<string>> allStyle = new Dictionary<string, List<string>>();
            allStyle.Add("0", style1);
            allStyle.Add("1", style2);
            allStyle.Add("2", style3);
            allStyle.Add("3", style4);

            string data = Trans(dataTable, chartOptions);
            string styleIndex = chartOptions[2];
            List<ISeries> series = new List<ISeries>();
            option.title = new Title()
            {
                text = "'词云图'",
                left = "'center'",
                textStyle = new TextStyle()
                {
                    fontSize = 45,
                    fontFamily = FontFamily.serif,
                    fontWeight = FontWeight.normal,
                }
            };
           
            option.tooltip = new Tooltip()
            {
                show ="true",
                trigger = "'item'",
                triggerOn = TriggerOn.mousemove,
            };
            option.backgroundColor = allStyle[styleIndex][3];
            series.Add(new SeriesWordCloud()
            {
                type = "'wordCloud'",
                gridSize = allStyle[styleIndex][2],
                shape = allStyle[styleIndex][0],
                rotationRange = allStyle[styleIndex][1],
                rotationStep = "45",
                top = "'10%'",
                sizeRange = "[10,60]",
                drawOutOfBound = "false",
                data = data,
                textStyle = new TextStyle()
                {
                    normal = new Normal()
                    {
                        color = "function () {return ('rgb(' + [Math.round(Math.random() * 255),Math.round(Math.random() * 255),Math.round(Math.random() * 255)].join(',') + ')');}",
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
            string[] colName = new string[chartOptions.Length-1];
            string[][] colList = new string[chartOptions.Length-1][];
            for (int i = 0; i < chartOptions.Length-1; i++)
            {
                colList[i] = new string[dataTable.Rows.Count];
                colName[i] = dataTable.Columns[int.Parse(chartOptions[i])].ColumnName;
                colList[i] = dataTable.AsEnumerable().Select(peo => peo.Field<string>(colName[i])).ToArray();
            }
            

            string testData = "[";
            for (int i=0; i < dataTable.Rows.Count; i++)
            {
                Dictionary<string, string> wordDictionary = new Dictionary<string, string>();
                if (colList[1][i] != "")
                {
                    int len = colList[1][i].Length;
                    int k = 0;
                    ASCIIEncoding ascii = new ASCIIEncoding();
                    byte[] bytestr = ascii.GetBytes(colList[1][i]);

                    foreach (byte c in bytestr)
                    {
                        if (c >= 48 && c <= 57)
                        {
                            k = k + 1;
                        }
                    }
                    if (len == k)
                    {                     
                        wordDictionary.Add("name", colList[0][i]);
                        wordDictionary.Add("value", colList[1][i]);
                        JavaScriptSerializer jss = new JavaScriptSerializer();
                        string strData = jss.Serialize(wordDictionary);
                        testData = testData + strData + ",";
                    }
                }
            }
            return testData + "]";
        }
    }
}