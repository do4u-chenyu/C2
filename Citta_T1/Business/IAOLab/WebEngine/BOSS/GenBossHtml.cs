using C2.IAOLab.WebEngine.Boss.Charts.Bar;
using C2.IAOLab.WebEngine.Boss.Charts.Line;
using C2.IAOLab.WebEngine.Boss.Charts.Map;
using C2.IAOLab.WebEngine.Boss.Charts.Pie;
using C2.IAOLab.WebEngine.Boss.Charts.Scatter;
using C2.IAOLab.WebEngine.Boss.Option;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            echarts[1] = new SimpleBar(dataTable, new CompleteOption(), chartOptions); //柱状图
            echarts[2] = new BasicLineChart(dataTable, new CompleteOption(), chartOptions); //折线图
            echarts[3] = new BasicScatter(dataTable, new CompleteOption(), chartOptions); //散点图
            echarts[4] = new SmoothedLineChart(dataTable, new CompleteOption(), chartOptions); //曲线图

            if(bossType == 0 || bossType == 2)
                echarts[5] = new StackBar(dataTable, new CompleteOption(), chartOptions);  //堆叠柱状图
            if(bossType == 0)
            {
                echarts[6] = new BasicPie(dataTable, new CompleteOption(), chartOptions); //饼图
                echarts[7] = new BasicMap(dataTable, new CompleteOption("map"), chartOptions); //大地图
            }

            return echarts.Show();
        }

    }
}
