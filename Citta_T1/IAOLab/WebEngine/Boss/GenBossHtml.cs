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


        public string TransDataToHtml(object[] args = null)//参数待设计
        {
            DataTable dataTable = new DataTable("temp");
            dataTable.Columns.Add("产品", typeof(string));
            dataTable.Columns.Add("2015", typeof(float));
            dataTable.Columns.Add("2016", typeof(float));
            dataTable.Columns.Add("2017", typeof(float));
            dataTable.Rows.Add("中国", 43.3, 85.8, 93.7);
            dataTable.Rows.Add("美国", 83.1, 73.4, 55.1);
            dataTable.Rows.Add("日本", 86.4, 65.2, 82.5);
            dataTable.Rows.Add("英国", 72.4, 53.9, 39.1);

            //03.创建布局，增加图，Show()显示图 
            Echarts echarts = new Echarts();

            return ShowECharts(dataTable, echarts);
        }

        private string ShowECharts(DataTable dataTable, Echarts echarts)
        {
            int row = 2;
            int col = 3;

            echarts.AddTheme(Theme.roma);
            echarts.CreateTableLayout(row, col, 1000 / col, 600 / row);

            echarts[1, 1] = new SimpleBar(dataTable, new CompleteOption() { title = new Title() { text = "'基础柱状图'", } }, 1);
            echarts[1, 2] = new BasicLineChart(dataTable, new CompleteOption() { title = new Title() { text = "'基础折线图'", } }, 1);
            echarts[1, 3] = new BasicScatter(dataTable, new CompleteOption() { title = new Title() { text = "'基础散点图'", } }, 1);
            //echarts[1, 3] = new BasicAreachart(dataTable,new CompleteOption() { title = new Title() { text = "'基础面积图'", } }, 1);
            echarts[2, 1] = new SmoothedLineChart(dataTable, new CompleteOption() { title = new Title() { text = "'基础曲线图'", } }, 1);
            echarts[2, 2] = new StackBar(dataTable, new CompleteOption() { title = new Title() { text = "'堆叠柱状图'", } }, 1);
            echarts[2, 3] = new BasicPie(dataTable, new CompleteOption(), 1);
            return echarts.Show();
        }

    }
}
