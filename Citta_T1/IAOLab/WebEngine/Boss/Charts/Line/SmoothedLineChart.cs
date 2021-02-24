using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace C2.IAOLab.WebEngine.Boss.Charts.Line
{
    public class SmoothedLineChart : BaseCharts
    {
        public SmoothedLineChart(DataTable dataTable, CompleteOption option, string[] chartOptions)
        {
            option.xAxis = new XAxis()
            {
                type = Option.BaseOption.xAxisType.category,
            };
            option.yAxis = new YAxis()
            {
                type = Option.BaseOption.xAxisType.value,
            };
            option.dataset = "{ source: datas }";

            List<ISeries> series = new List<ISeries>();
            for (int i = 1; i < chartOptions.Length; i++)
                series.Add(new SeriesLine(chartOptions[0], chartOptions[i]) { smooth = "'true'"});

            option.series = new Series(series.ToArray());
            _initScript = option.ToString();
        }
    }
}
