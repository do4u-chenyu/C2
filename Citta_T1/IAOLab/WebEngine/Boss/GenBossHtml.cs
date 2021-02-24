using C2.IAOLab.WebEngine.Boss.Charts.Bar;
using C2.IAOLab.WebEngine.Boss.Charts.Line;
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

            echarts[1] = new SimpleBar(dataTable, new CompleteOption(), chartOptions["SimpleBar"]);
            //echarts[2] = new BasicLineChart(dataTable, new CompleteOption() { title = new Title() { text = "'基础折线图'", } }, 1);
            //echarts[1, 3] = new BasicScatter(dataTable, new CompleteOption() { title = new Title() { text = "'基础散点图'", } }, 1);
            ////echarts[1, 3] = new BasicAreachart(dataTable,new CompleteOption() { title = new Title() { text = "'基础面积图'", } }, 1);
            //echarts[2, 1] = new SmoothedLineChart(dataTable, new CompleteOption() { title = new Title() { text = "'基础曲线图'", } }, 1);
            //echarts[2, 2] = new StackBar(dataTable, new CompleteOption() { title = new Title() { text = "'堆叠柱状图'", } }, 1);
            //echarts[2, 3] = new BasicPie(dataTable, new CompleteOption(), 1);
            return echarts.Show();
        }

    }
}
