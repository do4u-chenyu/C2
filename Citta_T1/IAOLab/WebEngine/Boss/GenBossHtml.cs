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
            Echarts echarts = new Echarts
            {
                dataTable = dataTable
            };
            echarts.AddTheme(Theme.phx);
            foreach(string eType in chartOptions.Keys)
            {
                switch (eType)
                {
                    case "SimpleBar":
                        echarts[1] = new SimpleBar(new CompleteOption(), chartOptions["SimpleBar"]);
                        break;
                    case "BasicLineChart":
                        echarts[2] = new BasicLineChart(new CompleteOption(), chartOptions["BasicLineChart"]);
                        break;
                    case "BasicScatter":
                        echarts[3] = new BasicScatter(new CompleteOption(), chartOptions["BasicScatter"]);
                        break;
                    case "SmoothedLineChart":
                        echarts[4] = new SmoothedLineChart(new CompleteOption(), chartOptions["SmoothedLineChart"]);
                        break;
                    case "StackBar":
                        echarts[5] = new StackBar(new CompleteOption(), chartOptions["StackBar"]);
                        break;
                    case "BasicPie":
                        echarts[6] = new BasicPie(new CompleteOption(), chartOptions["BasicPie"]);
                        break;
                    case "BasicMap":
                        echarts[7] = new BasicMap(new CompleteOption("map"), chartOptions["BasicMap"]);
                        break;
                    default:
                        break;
                }
            }

            return echarts.Show();
        }

    }
}
