using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WebEngine.Boss.Option.BaseOption;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption;
using System.Collections.Generic;
using System.Data;

namespace C2.IAOLab.WebEngine.Boss.Charts.Bar
{
    /// <summary>
    /// 基础柱状图
    /// </summary>
    public class SimpleBar: BaseCharts
    {
        public SimpleBar(DataTable dataTable, CompleteOption option, Dictionary<string, int[]> chartOptionDict)
        {
            if (!chartOptionDict.ContainsKey("SimpleBar") || chartOptionDict["SimpleBar"].Length == 0)
                return;

            int[] chartOptions = chartOptionDict["SimpleBar"];
            option.xAxis = new XAxis() {
                type = xAxisType.category,
                data = Common.GetDataByIdx(dataTable, chartOptions[0])
            };
            option.yAxis = new YAxis();
            //option.dataset = Common.FormatDatas;

            option.grid = new Grid()
            {
                left = "'18%'",
            };

            List<ISeries> series = new List<ISeries>();
            for (int i = 1; i< chartOptions.Length; i++)
            {
                
                series.Add(new SeriesBar() {
                    name = Common.FormatString(dataTable.Columns[chartOptions[i]].ToString()),
                    data = Common.GetDataByIdx(dataTable, chartOptions[i])
                });
            }

            option.series = new Series(series.ToArray());
            _initScript = option.ToString();
        }
    }
}
