using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WebEngine.Boss.Option.BaseOption;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace C2.IAOLab.WebEngine.Boss.Charts.Bar
{
    /// <summary>
    /// 堆叠柱状图
    /// </summary>
    public class StackBar : BaseCharts
    {
        public StackBar(DataTable dataTable, CompleteOption option, string[] chartOptions, string stack="'汇总'")
        {
            option.xAxis = new XAxis()
            {
                type = xAxisType.category
            };
            option.yAxis = new YAxis();
            option.dataset = "{ source: datas }";

            List<ISeries> series = new List<ISeries>();
            for (int i = 1; i < chartOptions.Length; i++)
                series.Add(new SeriesBar(chartOptions[0], chartOptions[i]){ stack = stack });

            option.series = new Series(series.ToArray());
            _initScript = option.ToString();
        }
    }
}
