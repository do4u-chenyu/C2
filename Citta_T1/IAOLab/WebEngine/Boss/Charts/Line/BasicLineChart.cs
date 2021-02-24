﻿using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WebEngine.Boss.Option.BaseOption;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace C2.IAOLab.WebEngine.Boss.Charts.Line
{
    /// <summary>
    /// 基础折线图
    /// </summary>
    public class BasicLineChart : BaseCharts
    {
        public BasicLineChart(DataTable dataTable, CompleteOption option, int categoryCol = 1)
        {
            option.xAxis = new XAxis()
            {
                type = xAxisType.category
            };
            option.yAxis = new YAxis()
            {
                type = Option.BaseOption.xAxisType.value,
            };
            //option.dataset = new DataSetSource()
            //{
            //    source = Common.GetDataSetSource(dataTable, categoryCol - 1),

            //};
            option.series = new Series(Enumerable.Repeat(new SeriesLine(), dataTable.Columns.Count - 1).ToArray());
            _initScript = option.ToString();
        }
    }
}
