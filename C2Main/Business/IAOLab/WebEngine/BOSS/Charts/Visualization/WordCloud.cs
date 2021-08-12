﻿using C2.IAOLab.WebEngine.Boss;
using C2.IAOLab.WebEngine.Boss.Charts;
using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.IAOLab.WebEngine.Boss.Charts.Visualization
{
    class WordCloud : BaseCharts
    {
        public WordCloud(DataTable dataTable, CompleteOption option, Dictionary<string, string[]> chartOptionDict)
        {
            if (!chartOptionDict.ContainsKey("WordCloud") || chartOptionDict["WordCloud"].Length == 0)
                return;

            string[] chartOptions = chartOptionDict["WordCloud"];
            option.xAxis = new XAxis()
            {

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
