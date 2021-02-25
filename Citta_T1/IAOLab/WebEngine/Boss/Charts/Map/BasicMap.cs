using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption;

namespace C2.IAOLab.WebEngine.Boss.Charts.Map
{
    class BasicMap : BaseCharts
    {
        public BasicMap(CompleteOption option, string[] chartOptions)
        {
            option.tooltip = new Tooltip() {
                trigger = Common.FormatString("item")
            };

            option.visualMap = new VisualMap() {
                max = 400, //TODO phx 暂时写死，chartOptions[2]
            };
            option.dataset = Common.FormatDatas;

            option.series = new Series(new ISeries[] {
                new SeriesMap() {
                    name = Common.FormatString("数据"),
                    mapType = Common.FormatString("china"),
                    roam = Common.FormatString("true"),
                    label = new MapLabel(){
                        normal = "{show : true}",
                        emphasis = "{show : true}"
                    },
                    encode = new Encode() { 
                        itemName = Common.FormatString(chartOptions[0]),
                        value = Common.FormatString(chartOptions[1])
                    }
            }});
            _initScript = option.ToString();
        }
    }
}
