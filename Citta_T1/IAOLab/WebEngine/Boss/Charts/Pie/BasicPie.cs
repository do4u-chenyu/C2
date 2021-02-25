using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption;

namespace C2.IAOLab.WebEngine.Boss.Charts.Pie
{
    public class BasicPie : BaseCharts
    {
        public BasicPie(CompleteOption option, string[] chartOptions) {
            
            if (!option.legend.FlagDic["orient"]) {
                option.legend.orient = Option.BaseOption.Orient.horizontal;
                option.legend.left = Common.FormatString("center");
            }
            option.dataset = Common.FormatDatas;

            option.series = new Series( new ISeries[] {
                new SeriesPie() {
                    encode = new Encode() {
                        itemName = Common.FormatString(chartOptions[0]),
                        value = Common.FormatString(chartOptions[1])
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