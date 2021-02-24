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
    /// 基础柱状图
    /// </summary>
    public class SimpleBar: BaseCharts
    {
        public SimpleBar(DataTable dataTable, CompleteOption option, string[] simpleBarOptions)
        {
            option.xAxis = new XAxis()
            {
                type = xAxisType.category
            };
            option.yAxis = new YAxis();
            option.dataset = "{ source: data111 }";

            //dataset共用一份了
            //option.dataset = new DataSetSource()
            //{
            //    source = Common.GetDataSetSource(dataTable, categoryCol - 1),

            //};
            //option.series = new Series(Enumerable.Repeat(new SeriesBar(), dataTable.Columns.Count - 1).ToArray());

            List<ISeries> series = new List<ISeries>();
            series.Add(new SeriesBar(simpleBarOptions[0], simpleBarOptions[1]));
            series.Add(new SeriesBar(simpleBarOptions[0], simpleBarOptions[2]));
            option.series = new Series(series.ToArray());
            _initScript = option.ToString();
        }
    }
}
