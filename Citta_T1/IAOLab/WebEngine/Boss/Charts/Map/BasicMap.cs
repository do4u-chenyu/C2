using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption;
using System.Data;

namespace C2.IAOLab.WebEngine.Boss.Charts.Map
{
    class BasicMap : BaseCharts
    {
        public BasicMap(DataTable dataTable, CompleteOption option, int[] chartOptions)
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
                        itemName = chartOptions[0],
                        value = chartOptions[1]
                    }
            }});
            _initScript = option.ToString();
        }
    }
}
