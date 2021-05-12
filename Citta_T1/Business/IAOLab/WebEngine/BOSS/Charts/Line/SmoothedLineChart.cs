using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WebEngine.Boss.Option.BaseOption;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption;
using System.Collections.Generic;
using System.Data;

namespace C2.IAOLab.WebEngine.Boss.Charts.Line
{
    public class SmoothedLineChart : BaseCharts
    {
        public SmoothedLineChart(DataTable dataTable, CompleteOption option, Dictionary<string, int[]> chartOptionDict)
        {
            if (!chartOptionDict.ContainsKey("SmoothedLineChart") || chartOptionDict["SmoothedLineChart"].Length == 0)
                return;

            int[] chartOptions = chartOptionDict["SmoothedLineChart"];
            option.xAxis = new XAxis(){
                type = xAxisType.category,
                data = Common.GetDataByIdx(dataTable, chartOptions[0])
            };
            option.yAxis = new YAxis(){
                type = xAxisType.value,
            };
            //option.dataset = Common.FormatDatas;

            List<ISeries> series = new List<ISeries>();
            for (int i = 1; i < chartOptions.Length; i++)
            {
                series.Add(new SeriesLine() {
                    name = Common.FormatString(dataTable.Columns[chartOptions[i]].ToString()),
                    data = Common.GetDataByIdx(dataTable, chartOptions[i]),
                    smooth = Common.FormatString("true")
                });
            }

            option.series = new Series(series.ToArray());
            _initScript = option.ToString();
        }
    }
}
