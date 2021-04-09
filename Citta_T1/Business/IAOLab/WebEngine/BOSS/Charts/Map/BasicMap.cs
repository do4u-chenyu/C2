using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption;
using C2.Utils;
using System.Collections.Generic;
using System.Data;

namespace C2.IAOLab.WebEngine.Boss.Charts.Map
{
    class BasicMap : BaseCharts
    {
        public BasicMap(DataTable dataTable, CompleteOption option, Dictionary<string, int[]> chartOptionDict)
        {
            if (!chartOptionDict.ContainsKey("BasicMap") || chartOptionDict["BasicMap"].Length <= 1)
                return;

            int[] chartOptions = chartOptionDict["BasicMap"];

            option.tooltip = new Tooltip() {
                trigger = Common.FormatString("item")
            };

            option.visualMap = new VisualMap() {
                max = GetMapMaxValue(dataTable, chartOptions[1]),
            };

            option.dataset = Common.FormatDatas;
            option.geo = "{'map':'china','roam':1,'scaleLimit':{'min':1,'max':2},'zoom':1,'label':{'show':false,'color':'#fff','fontSize':10},'emphasis':{'label':{'show':false,'color':'#fff','fontSize':10}},'itemStyle':{'normal':{'borderColor':'rgba(19,198,249,0.45)','borderWidth':1,'color':'rgba(0,0,0,0)','areaColor':{'type':'radial','x':0.5,'y':0.5,'r':0.8,'colorStops':[{'offset':0,'color':'rgba(0,0,0,0)'},{'offset':1,'color':'rgba(19,198,249,0.15)'}],'global':false},'shadowColor':'rgba(19,198,249,1)','shadowOffsetX':0,'shadowOffsetY':0,'shadowBlur':10},'emphasis':{}},}";

            option.series = new Series(new ISeries[] {
                new SeriesMap() {
                    name = Common.FormatString("数据"),
                    roam = Common.FormatString("true"),
                    geoIndex = 0,
                    encode = new Encode() {
                        itemName = chartOptions[0],
                        value = chartOptions[1]
                    }
            }});
            _initScript = option.ToString();
        }

        private int GetMapMaxValue(DataTable dataTable, int idx)
        {
            int mapMaxValue = 0;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                int tmpNum = ConvertUtil.TryParseInt(dataRow[idx].ToString());
                mapMaxValue = mapMaxValue > tmpNum ? mapMaxValue : tmpNum;
            }
            mapMaxValue += (5 - mapMaxValue % 5); //为了被5整除

            return mapMaxValue;
        }
    }
}
