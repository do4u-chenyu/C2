using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WebEngine.Boss.Option.BaseOption;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption;
using System.Collections.Generic;
using System.Data;

namespace C2.IAOLab.WebEngine.Boss.Charts.Line
{
    /// <summary>
    /// 基础折线图
    /// </summary>
    public class BasicLineChart : BaseCharts
    {
        public BasicLineChart(DataTable dataTable, CompleteOption option, Dictionary<string, int[]> chartOptionDict)
        {
            if (!chartOptionDict.ContainsKey("BasicLineChart") || chartOptionDict["BasicLineChart"].Length == 0)
                return;

            int[] chartOptions = chartOptionDict["BasicLineChart"];
            option.xAxis = new XAxis() {
                type = xAxisType.category,
                data = Common.GetDataByIdx(dataTable, chartOptions[0])
            };
            option.yAxis = new YAxis() {
                type = xAxisType.value,
            };
            option.grid = new Grid()
            {
                left = "'18%'",
            };
            //option.dataset = Common.FormatDatas;

            List<ISeries> series = new List<ISeries>();
            for (int i = 1; i < chartOptions.Length; i++) 
            { 
                series.Add(new SeriesLine() { 
                    name = Common.FormatString(dataTable.Columns[chartOptions[i]].ToString()),
                    data = Common.GetDataByIdx(dataTable, chartOptions[i])
                });
            }
            option.series = new Series(series.ToArray());
            _initScript = option.ToString();
        }
    }
}
