using C2.Business.IAOLab.WebEngine.Boss.Charts.Visualization;
using C2.IAOLab.WebEngine.Boss;
using C2.IAOLab.WebEngine.Boss.Charts;
using C2.IAOLab.WebEngine.Boss.Option;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2.Business.IAOLab.WebEngine.Boss
{
    class GenVisualHtml
    {
        private static GenVisualHtml GenVisualHtmlInstance;
        public static GenVisualHtml GetInstance()
        {
            if (GenVisualHtmlInstance == null)
            {
                GenVisualHtmlInstance = new GenVisualHtml();
            }
            return GenVisualHtmlInstance;
        }


        public string TransDataToHtml(DataTable dataTable, Dictionary<string, string> chartOptions)
        {
            //创建布局，增加图，Show()显示图 
            Echarts echarts = new Echarts
            {
                dataTable = dataTable
            };
            echarts.AddTheme(Theme.phx);

            echarts[0] = GenChartByType(chartOptions["ChartType"], dataTable, chartOptions);
            return echarts.Show();
        }

        private BaseCharts GenChartByType(string chartType, DataTable dataTable, Dictionary<string, string> chartOptions)
        {
            switch (chartType)
            {
                case "0":
                    return new Organization(dataTable, new CompleteOption(), chartOptions);
                case "1":
                    return new WordCloud(dataTable, new CompleteOption(), chartOptions);
                default:
                    return new Organization(dataTable, new CompleteOption(), chartOptions);
            }
        }

    }
}
