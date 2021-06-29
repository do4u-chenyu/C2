﻿using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WebEngine.Boss.Option.BaseOption;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption;
using System.Collections.Generic;
using System.Data;

namespace C2.IAOLab.WebEngine.Boss.Charts.Bar
{
    /// <summary>
    /// 堆叠柱状图
    /// </summary>
    public class StackBar : BaseCharts
    {
        public StackBar(DataTable dataTable, CompleteOption option, Dictionary<string, int[]> chartOptionDict, string stack="汇总")
        {
            if (!chartOptionDict.ContainsKey("StackBar") || chartOptionDict["StackBar"].Length == 0)
                return;

            int[] chartOptions = chartOptionDict["StackBar"];
            option.xAxis = new XAxis() {
                type = xAxisType.category,
                data = Common.GetDataByIdx(dataTable, chartOptions[0])
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
                series.Add(new SeriesBar() {
                    name = Common.FormatString(dataTable.Columns[chartOptions[i]].ToString()),
                    data = Common.GetDataByIdx(dataTable, chartOptions[i]),
                    stack = Common.FormatString(stack)
                });
            }
            option.series = new Series(series.ToArray());
            _initScript = option.ToString();
        }
    }
}
