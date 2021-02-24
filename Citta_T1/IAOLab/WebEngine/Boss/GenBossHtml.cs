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


        public string TransDataToHtml(DataTable dataTable ,Dictionary<string, string[]> chartOptions)//参数待设计
        {
            //创建布局，增加图，Show()显示图 
            Echarts echarts = new Echarts();
            echarts.dataTable = dataTable;
            echarts.AddTheme(Theme.phx);

            echarts[1] = new SimpleBar(dataTable, new CompleteOption(), chartOptions["SimpleBar"]);      //柱状图
            echarts[2] = new BasicLineChart(dataTable, new CompleteOption(), chartOptions["SimpleBar"]); //折线图
            echarts[3] = new BasicScatter(dataTable, new CompleteOption(), chartOptions["SimpleBar"]);   //散点图
            echarts[4] = new SmoothedLineChart(dataTable, new CompleteOption(), chartOptions["SimpleBar"]);  //曲线图
            echarts[5] = new StackBar(dataTable, new CompleteOption(), chartOptions["SimpleBar"]);  //堆叠柱状图
            echarts[6] = new BasicPie(dataTable, new CompleteOption(), chartOptions["SimpleBar"]);  //饼状图
            echarts[7] = new BasicMap(dataTable, new CompleteOption("map"), chartOptions["SimpleBar"]);  //地市分布图
            return echarts.Show();
        }

    }
}
