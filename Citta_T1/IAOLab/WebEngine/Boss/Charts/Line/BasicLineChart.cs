using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WebEngine.Boss.Option.BaseOption;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption;
using System.Collections.Generic;

namespace C2.IAOLab.WebEngine.Boss.Charts.Line
{
    /// <summary>
    /// 基础折线图
    /// </summary>
    public class BasicLineChart : BaseCharts
    {
        public BasicLineChart(CompleteOption option, string[] chartOptions)
        {
            option.xAxis = new XAxis() {
                type = xAxisType.category
            };
            option.yAxis = new YAxis() {
                type = xAxisType.value,
            };
            option.dataset = Common.FormatDatas;

            List<ISeries> series = new List<ISeries>();
            for (int i = 1; i < chartOptions.Length; i++) 
            { 
                series.Add(new SeriesLine() { 
                    name = Common.FormatString(chartOptions[i]),
                    encode = new Encode() { 
                        x = Common.FormatString(chartOptions[0]), 
                        y = Common.FormatString(chartOptions[i])
                    }
                });
            }
            option.series = new Series(series.ToArray());
            _initScript = option.ToString();
        }
    }
}
