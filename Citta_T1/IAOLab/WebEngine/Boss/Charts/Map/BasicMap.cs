using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WebEngine.Boss.Option.BaseOption;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.IAOLab.WebEngine.Boss.Charts.Map
{
    class BasicMap : BaseCharts
    {
        public BasicMap(DataTable dataTable, CompleteOption option, string[] chartOptions)
        {
            option.tooltip = new Tooltip()
            {
                trigger = "'item'"
            };

            option.visualMap = new VisualMap()
            {
                show = "true",
                x = "'left'",
                y = "'center'",
                min = 0,
                max = 400,
                splitNumber = 5
            };
            option.dataset = "{ source: datas }";

            option.series = new Series(
                new ISeries[] {
                new SeriesMap() {
                    name = "'数据'",
                    mapType = "'china'",
                    roam = "'true'",
                    label = new MapLabel()
                    {
                        normal = "{show : true}",
                        emphasis = "{show : true}"
                    },
                    encode = "{ itemName: '" + chartOptions[0] + "' , value:'" + chartOptions[1] + "'}"
            }});
            _initScript = option.ToString();
        }
    }
}
