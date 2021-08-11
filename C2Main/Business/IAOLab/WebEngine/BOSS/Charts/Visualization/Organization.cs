using C2.IAOLab.WebEngine.Boss;
using C2.IAOLab.WebEngine.Boss.Charts;
using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WebEngine.Boss.Option.BaseOption;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.IAOLab.WebEngine.Boss.Charts.Visualization
{
    class Organization : BaseCharts
    {
        public Organization(DataTable dataTable, CompleteOption option, Dictionary<string, string[]> chartOptionDict)
        {
            if (!chartOptionDict.ContainsKey("Organization") || chartOptionDict["Organization"].Length == 0)
                return;


            /*
             * chartOptions数组索引对应关系
             * 0     ：下级所在列
             * 1     ：上级所在列
             * 2-len ：信息所在列
             * 
             * 注：chartOptions[i] 得到的列为string，需要int.Parse转换后再去dataTable取值
             */
            string[] chartOptions = chartOptionDict["Organization"];
            option.xAxis = new XAxis()
            {
                type = xAxisType.category,
                data = Common.GetDataByIdx(dataTable, int.Parse(chartOptions[0]))
            };
            option.yAxis = new YAxis();
            //option.dataset = Common.FormatDatas;
            option.grid = new Grid()
            {
                top = "'15%'",
                left = "'1%'",
                bottom = "'15%'",
                containLabel = "true",
            };

            List<ISeries> series = new List<ISeries>();
            for (int i = 1; i < chartOptions.Length; i++)
            {
                series.Add(new SeriesBar()
                {
                    name = Common.FormatString(dataTable.Columns[int.Parse(chartOptions[i])].ToString()),
                    data = Common.GetDataByIdx(dataTable, int.Parse(chartOptions[i]))
                });
            }
            option.series = new Series(series.ToArray());
            _initScript = option.ToString();
        }
    }
}
