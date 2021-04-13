using C2.Business.IAOLab.WebEngine.Boss;
using C2.Business.IAOLab.WebEngine.Boss.Charts.Bar;
using C2.Business.IAOLab.WebEngine.Boss.Charts.Line;
using C2.IAOLab.WebEngine.Boss.Charts;
using C2.IAOLab.WebEngine.Boss.Charts.Bar;
using C2.IAOLab.WebEngine.Boss.Charts.Line;
using C2.IAOLab.WebEngine.Boss.Charts.Map;
using C2.IAOLab.WebEngine.Boss.Charts.Pie;
using C2.IAOLab.WebEngine.Boss.Charts.Scatter;
using C2.IAOLab.WebEngine.Boss.Option;
using System.Collections.Generic;
using System.Data;

namespace C2.IAOLab.WebEngine.Boss
{
    class GenBossHtml
    {
        private static GenBossHtml GenBossHtmlInstance;
        public static GenBossHtml GetInstance()
        {
            if (GenBossHtmlInstance == null)
            {
                GenBossHtmlInstance = new GenBossHtml();
            }
            return GenBossHtmlInstance;
        }


        public string TransDataToHtml(DataTable dataTable ,Dictionary<string, int[]> chartOptions)
        {
            //创建布局，增加图，Show()显示图 
            Echarts echarts = new Echarts
            {
                dataTable = dataTable
            };
            echarts.AddTheme(Theme.phx);

            int bossType = chartOptions["BossType"][0];

            //获取当前模板的所有图表
            List<BossChartConfig> bossCharts = BossTemplateCollection.GetInstance().GetTemplateByIdx(bossType).BossCharts;
            foreach(BossChartConfig chart in bossCharts)
                echarts[chart.Idx] = GenChartByType(chart.Type, dataTable, chartOptions);

            return echarts.Show();
        }

        private BaseCharts GenChartByType(string chartType, DataTable dataTable, Dictionary<string, int[]> chartOptions)
        {
            switch (chartType)
            {
                case "SimpleBar":
                    return new SimpleBar(dataTable, new CompleteOption(), chartOptions);
                case "BasicLineChart":
                    return new BasicLineChart(dataTable, new CompleteOption(), chartOptions);
                case "BasicScatter":
                    return new BasicScatter(dataTable, new CompleteOption(), chartOptions);
                case "GradientLineChart":
                    return new GradientLineChart(dataTable, new CompleteOption(), chartOptions);
                case "StackBar":
                    return new StackBar(dataTable, new CompleteOption(), chartOptions);
                case "PictorialBar":
                    return new PictorialBar(dataTable, new CompleteOption(), chartOptions);
                case "BasicMap":
                    return new BasicMap(dataTable, new CompleteOption("map"), chartOptions);
                case "BasicPie":
                    return new BasicPie(dataTable, new CompleteOption(), chartOptions);
                default:
                    return new SimpleBar(dataTable, new CompleteOption(), chartOptions);
            }
        }

    }
}
