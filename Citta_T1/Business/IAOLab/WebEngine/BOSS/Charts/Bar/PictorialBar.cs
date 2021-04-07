using C2.Business.IAOLab.WebEngine.Boss.Option.SeriesType;
using C2.IAOLab.WebEngine.Boss;
using C2.IAOLab.WebEngine.Boss.Charts;
using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WebEngine.Boss.Option.BaseOption;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption;
using System.Collections.Generic;
using System.Data;


namespace C2.Business.IAOLab.WebEngine.Boss.Charts.Bar
{
    //立体柱状图
    public class PictorialBar:BaseCharts
    {
        public PictorialBar(DataTable dataTable, CompleteOption option, Dictionary<string, int[]> chartOptionDict)
        {
            if (!chartOptionDict.ContainsKey("PictorialBar") || chartOptionDict["PictorialBar"].Length == 0)
                return;

            int[] chartOptions = chartOptionDict["PictorialBar"];


            option.animation = "false";

            option.tooltip = new Tooltip()
            {
                show = "true",
            };

            option.grid = new Grid()
            {
                top = "'15%'",
                left = "'5%'",
                right = "'5%'",
                bottom = "'15%'",
                containLabel = "true",
            };

            option.xAxis = new XAxis()
            {
                type = xAxisType.category,
                axisTick = "{'alignWithLabel': true}",
                nameTextStyle = new SubtextStyle
                {
                    color = "'#82b0ec'"
                },
                axisLine = "{show: false,'lineStyle': {'color': '#82b0ec'}}",
                axisLabel = "{'textStyle': {'color': '#fff'},margin: 30}",
            };

            option.yAxis = new YAxis()
            {
                show = "false",
                type = xAxisType.value,
                axisLabel = "{ 'textStyle': { 'color': '#fff' }}",
                splitLine = "{'lineStyle': {'color': '#0c2c5a'}}",
                axisLine = "{'show': false}",
            };

            option.dataset = Common.FormatDatas;
            List<ISeries> series = new List<ISeries>();

            series.Add(new SeriesPictorialBar()
            {
                encode = new Encode()
                {
                    x = chartOptions[0],
                    y = chartOptions[1]
                },
                symbolSize = "[50,15]",
                symbolOffset = "[0,12]",
                z = 10,
                itemStyle = new ItemStyle()
                {
                    color = "'transparent'",
                    borderColor = "'#2EA9E5'",
                    borderType = "'solid'",
                    borderWidth = 1,
                },
                label = new Label()
                {
                    show = "true",
                    position = C2.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption.Position.top,
                    fontSize = 15,
                    fontWeight = FontWeight.bold,
                    color = "'#34DCFF'",
                },
                color = "'#2DB1EF'",


            });

            series.Add(new SeriesBar()
            {
                barWidth = "'40'",
                barGap = "'10%'",
                barCategoryGap = "'10%'",
                itemStyle = new ItemStyle()
                {
                    color = "new echarts.graphic.LinearGradient(0, 0, 0, 0.7, [{offset: 0,color: '#38B2E6'},{offset: 1,color: '#0B3147'}]),opacity: 0.8",
                },
                encode = new Encode()
                {
                    x = chartOptions[0],
                    y = chartOptions[1]
                },

            });

            option.series = new Series(series.ToArray());
            _initScript = option.ToString();
        }
    }
}
