using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WebEngine.Boss.Option.BaseOption;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption;
using System.Collections.Generic;
using System.Data;

namespace C2.IAOLab.WebEngine.Boss.Charts.Scatter
{
    public class BasicScatter : BaseCharts
    {
        public BasicScatter(DataTable dataTable, CompleteOption option, Dictionary<string, int[]> chartOptionDict)
        {
            if (!chartOptionDict.ContainsKey("BasicScatter") || chartOptionDict["BasicScatter"].Length == 0)
                return;

            int[] chartOptions = chartOptionDict["BasicScatter"];
            option.xAxis = new XAxis() {
                type = xAxisType.category
            };
            option.yAxis = new YAxis();
            option.dataset = Common.FormatDatas;

            List<ISeries> series = new List<ISeries>();
            for (int i = 1; i < chartOptions.Length; i++)
            {
                series.Add(new SeriesScatter() {
                    name = Common.FormatString(dataTable.Columns[chartOptions[i]].ToString()),
                    encode = new Encode() {
                        x = chartOptions[0],
                        y = chartOptions[i]
                    }
                });
            }

            option.series = new Series(series.ToArray());
            _initScript = option.ToString();
        }
    }
}
