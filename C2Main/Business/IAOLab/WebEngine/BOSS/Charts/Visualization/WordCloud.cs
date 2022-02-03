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
using System.Text.RegularExpressions;
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
            string data = Trans(dataTable, chartOptions);
            string styleIndex = chartOptions[2];

            Dictionary<string, List<string>> allStyle = new Dictionary<string, List<string>>();
            allStyle.Add("0", new List<string>() { "'circle'", "[-90,90]", "5", "'#F7F7F7'" });
            allStyle.Add("1", new List<string>() { "'diamond'", "[-45,90]", "10", "'#00023f'" });
            allStyle.Add("2", new List<string>() { "'star'", "[-45,45]", "5", "'#F7F7F7'" });
            allStyle.Add("3", new List<string>() { "'pentagon'", "[-90,90]", "20", "'#F7F7F7'" });

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
            string testData = "[";
            for (int i=0; i < dataTable.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dataTable.Rows[i][int.Parse(chartOptions[1])].ToString()))
                {
                    Dictionary<string, string> wordDictionary = new Dictionary<string, string>();
                    Regex rx = new Regex("[^0-9]");
                    if (!rx.IsMatch(dataTable.Rows[i][int.Parse(chartOptions[1])].ToString()))
                    {                     
                        wordDictionary.Add("name", dataTable.Rows[i][int.Parse(chartOptions[0])].ToString());
                        wordDictionary.Add("value", dataTable.Rows[i][int.Parse(chartOptions[1])].ToString());
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