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
        public BasicLineChart(DataTable dataTable, CompleteOption option, int[] chartOptions)
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
