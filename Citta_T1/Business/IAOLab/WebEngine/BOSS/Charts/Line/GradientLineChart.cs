using C2.IAOLab.WebEngine.Boss;
using C2.IAOLab.WebEngine.Boss.Charts;
using C2.IAOLab.WebEngine.Boss.Option;
using C2.IAOLab.WebEngine.Boss.Option.BaseOption;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType;
using C2.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption;
using System.Collections.Generic;
using System.Data;

namespace C2.Business.IAOLab.WebEngine.Boss.Charts.Line
{
    //渐变折线图
    public class GradientLineChart : BaseCharts
    {
         public GradientLineChart(DataTable dataTable,CompleteOption option, Dictionary<string,int[]> chartOptionDict)
        {
            if (!chartOptionDict.ContainsKey("GradientLineChart") || chartOptionDict["GradientLineChart"].Length == 0)
                return;

            int[] chartOptions = chartOptionDict["GradientLineChart"];

            option.title = new Title()
            {
                top = "'5%'",
                left = "'center'",
                textStyle = new TextStyle()
                {
                    align = "'center'",
                    color = "'#fff'",
                    fontSize = 20,
                },
            };

            option.tooltip = new Tooltip()
            {
                trigger = "'axis'",
                axisPointer = new AxisPointer()
                {
                    lineStyle = "{color: {type: 'linear',x: 0,y: 0,x2: 0,y2: 1,colorStops: [{offset: 0,color: 'rgba(0, 255, 233,0)'}, {offset: 0.5,color: 'rgba(255, 255, 255,1)',}, {offset: 1,color: 'rgba(0, 255, 233,0)'}],global: false}}",
                },
            };

            option.grid = new Grid()
            {
                top = "'15%'",
                left = "'5%'",
                right = "'5%'",
                bottom = "'15%'",
            };

            option.xAxis = new XAxis()
            {
                type = xAxisType.category,
                axisLine = "{show:true}",
                splitLine = "{show:false}", //没有按照我设置的来
                splitArea = "{color: '#f00',lineStyle: {color: '#f00'},}",
                axisLabel = "{color:'#fff'}",
                boundaryGap = "false",
            };

            //最后的4个都有问题，没有能够正确显示
            option.yAxis = new YAxis()
            {
                type = xAxisType.value,
                min = "0",
                splitNumber = 4,
                splitLine = "{show: true,lineStyle: {color: 'rgba(255,255,255,0.1)'}}",
                axisLine = "{show:true}",
                axisLabel = "{show: true,margin: 5,textStyle: {color: '#fff',},}",
                axisTick = "{show:false}",
            };

            option.dataset = Common.FormatDatas;

            List<ISeries> series = new List<ISeries>();

            series.Add(new SeriesLine()
            {
                name = Common.FormatString(dataTable.Columns[chartOptions[1]].ToString()),
                encode = new Encode()
                {
                    x = chartOptions[0],
                    y = chartOptions[1]
                },
                type = "'line'",
                showAllSymbol = "true",
                symbol = "'circle'",
                symbolSize = "10",

                lineStyle = new C2.IAOLab.WebEngine.Boss.Option.SeriesType.LineBaseOption.LineStyle()
                {
                    color = "'#6c50f3'",
                    shadowColor = "'rgba(0, 0, 0, .3)'",
                    shadowBlur = 0,
                    shadowOffsetY = 5,
                    shadowOffsetX = 5,
                },
                label = new Label()
                {
                    show = "false",
                    position = C2.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption.Position.top,
                    textStyle = "{ color: '#6c50f3',}",
                },

                itemStyle = new ItemStyle()
                {
                    color = "'#6c50fc'",
                    borderColor = "'#fff'",
                    borderWidth = 3,
                    shadowColor = "'rgba(0, 0, 0, .3)'",
                    shadowBlur = 0,
                    shadowOffsetX = 2,
                    shadowOffsetY = 2,
                },

                tooltip = new Tooltip()
                {
                
                    show = "false",
                },

                areaStyle = new C2.IAOLab.WebEngine.Boss.Option.SeriesType.LineBaseOption.AreaStyle()
                {
                    shadowColor = " 'rgba(108,80,243, 0.9)'",
                    shadowBlur = 20,
                    color = "new echarts.graphic.LinearGradient(0, 0, 0, 1, [{offset: 0,color: 'rgba(108,80,243,0.3)'},{offset: 1,color: 'rgba(108,80,243,0)'}], false)",
                    
                }

            }) ; 

            series.Add(new SeriesLine()
            {
                name = Common.FormatString(dataTable.Columns[chartOptions[2]].ToString()),
                encode = new Encode()
                {
                    x = chartOptions[0],
                    y = chartOptions[2]
                },
                type = "'line'",
                showAllSymbol = "true",
                symbol = "'circle'",
                symbolSize = "10",

                lineStyle = new C2.IAOLab.WebEngine.Boss.Option.SeriesType.LineBaseOption.LineStyle()
                {
                    color = "'#00ca95'",
                    shadowColor = "'rgba(0, 0, 0, .3)'",
                    shadowBlur = 0,
                    shadowOffsetY = 5,
                    shadowOffsetX = 5,
                },
                label = new Label()
                {
                    show = "false",
                    position = C2.IAOLab.WebEngine.Boss.Option.SeriesType.SeriesBaseOption.Position.top,
                    textStyle = "{ color: '#00ca95',}",
                },

                itemStyle = new ItemStyle()
                {
                    color = "'#00ca95'",
                    borderColor = "'#fff'",
                    borderWidth = 3,
                    shadowColor = "'rgba(0, 0, 0, .3)'",
                    shadowBlur = 0,
                    shadowOffsetX = 2,
                    shadowOffsetY = 2,
                },

                tooltip = new Tooltip()
                {
                    show = "false",
                },

                areaStyle = new C2.IAOLab.WebEngine.Boss.Option.SeriesType.LineBaseOption.AreaStyle()
                {
                    color = "new echarts.graphic.LinearGradient(0, 0, 0, 1, [{offset: 0,color: 'rgba(0,202,149,0.3)'},{offset: 1,color: 'rgba(0,202,149,0)'}], false)",
                    shadowColor = " 'rgba(0,202,149, 0.9)'",
                    shadowBlur = 20,
                },
            });

            option.series = new Series(series.ToArray());
            _initScript = option.ToString();

        }
         
    }
}
