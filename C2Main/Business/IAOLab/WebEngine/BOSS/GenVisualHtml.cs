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


        public string TransDataToHtml(DataTable dataTable, Dictionary<string, string[]> chartOptions)
        {
            //创建布局，增加图，Show()显示图 
            Echarts echarts = new Echarts
            {
                dataTable = dataTable
            };
            echarts.AddTheme(Theme.phx);

            echarts[1] = GenChartByType(dataTable, chartOptions);
            return echarts.ShowVisual();
        }

        private BaseCharts GenChartByType(DataTable dataTable, Dictionary<string, string[]> chartOptions)
        {
            switch (chartOptions["ChartType"][0])
            {
                case "Organization":
                    return new Organization(dataTable, new CompleteOption(), chartOptions);
                case "WordCloud":
                    return new WordCloud(dataTable, new CompleteOption(), chartOptions);
                default:
                    return new Organization(dataTable, new CompleteOption(), chartOptions);
            }
        }

    }
}
