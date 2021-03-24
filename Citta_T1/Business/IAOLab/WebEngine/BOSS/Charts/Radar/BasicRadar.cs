using C2.IAOLab.WebEngine.Boss.Option;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace C2.IAOLab.WebEngine.Boss.Charts.Radar
{
    public class BasicRadar : BaseCharts
    {
        public BasicRadar(DataTable dataTable, CompleteOption option, int categoryCol = 1)
        {
            option.xAxis = new XAxis();
            option.yAxis = new YAxis();
            //option.dataset = new DataSetSource()
            //{
            //    source = Common.GetDataSetSource(dataTable, categoryCol - 1),
            //};
            //option.series = new Series(Enumerable.Repeat(new SeriesScatter(), dataTable.Columns.Count - 1).ToArray());
            //_initScript = option.ToString();
        }
    }
}
