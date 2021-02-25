using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WebEngine.Boss.Option.BaseOption;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption;
using System.Collections.Generic;

namespace C2.IAOLab.WebEngine.Boss.Charts.Bar
{
    /// <summary>
    /// 堆叠柱状图
    /// </summary>
    public class StackBar : BaseCharts
    {
        public StackBar(CompleteOption option, string[] chartOptions, string stack="汇总")
        {
            option.xAxis = new XAxis() {
                type = xAxisType.category
            };
            option.yAxis = new YAxis();
            option.dataset = Common.FormatDatas;

            List<ISeries> series = new List<ISeries>();
            for (int i = 1; i < chartOptions.Length; i++)
            {
                series.Add(new SeriesBar() {
                    name = Common.FormatString(chartOptions[i]),
                    encode = new Encode() {
                        x = Common.FormatString(chartOptions[0]),
                        y = Common.FormatString(chartOptions[i])
                    },
                    stack = Common.FormatString(stack)
                });
            }
            option.series = new Series(series.ToArray());
            _initScript = option.ToString();
        }
    }
}
