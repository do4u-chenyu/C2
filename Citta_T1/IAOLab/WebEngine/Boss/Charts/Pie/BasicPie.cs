using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption;
using System.Data;

namespace C2.IAOLab.WebEngine.Boss.Charts.Pie
{
    public class BasicPie : BaseCharts
    {
        public BasicPie(DataTable dataTable, CompleteOption option, int[] chartOptions) {
            
            if (!option.legend.FlagDic["orient"]) {
                option.legend.orient = Option.BaseOption.Orient.horizontal;
                option.legend.left = Common.FormatString("center");
            }
            option.dataset = Common.FormatDatas;

            option.series = new Series( new ISeries[] {
                new SeriesPie() {
                    encode = new Encode() {
                        itemName = chartOptions[0],
                        value = chartOptions[1]
                    },
                    emphasis = new Emphasis() {
                        itemStyle = new ItemStyle() {
                            shadowBlur = 10,
                            shadowOffsetX = 0,
                            shadowColor = Common.FormatString("rgba(0, 0, 0, 0.5)"),
            }}}});

            _initScript = option.ToString();
        }
    }
}