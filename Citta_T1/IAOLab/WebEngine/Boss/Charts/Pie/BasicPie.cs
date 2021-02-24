using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace C2.IAOLab.WebEngine.Boss.Charts.Pie
{
    public class BasicPie : BaseCharts
    {
        public BasicPie(DataTable dataTable, CompleteOption option, string[] chartOptions) {
            
            if (!option.legend.FlagDic["orient"]) {
                option.legend.orient = Option.BaseOption.Orient.horizontal;
                option.legend.left = "'center'";
            }

            option.dataset = "{ source: datas }";
            option.series = new Series(
                new ISeries[] {
                new SeriesPie(chartOptions[0], chartOptions[1]) {
                    emphasis = new Option.SeriesType.SeriesBaseOption.Emphasis()
                    {
                        itemStyle = new Option.SeriesType.SeriesBaseOption.ItemStyle()
                        {
                            shadowBlur = 10,
                            shadowOffsetX = 0,
                            shadowColor = "'rgba(0, 0, 0, 0.5)'",
                        }
                    }


            }});
            _initScript = option.ToString();

        }
    }
}