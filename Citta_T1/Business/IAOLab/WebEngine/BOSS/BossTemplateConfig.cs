using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Business.IAOLab.WebEngine.Boss
{
    class BossTemplateConfig
    {
        public Dictionary<int, BossTemplate> BossTemplateDict { set; get; }

        private static BossTemplateConfig BossTemplateConfigInstance;
        public static BossTemplateConfig GetInstance()
        {
            if (BossTemplateConfigInstance == null)
            {
                BossTemplateConfigInstance = new BossTemplateConfig();
            }
            return BossTemplateConfigInstance;
        }

        public BossTemplateConfig()
        {

            this.BossTemplateDict = new Dictionary<int, BossTemplate>();
            BossTemplateDict.Add(0, new BossTemplate(new List<BossChartConfig>() 
                                                    { new BossChartConfig("SimpleBar", "柱状图（左上方）", 1), 
                                                      new BossChartConfig("BasicLineChart", "折线图（左下方）", 2), 
                                                      new BossChartConfig("BasicScatter", "点状图（中间下方）", 3), 
                                                      new BossChartConfig("GradientLineChart", "曲线图（中间下方）", 4), 
                                                      new BossChartConfig("StackBar", "堆叠柱状图（右上方）", 5), 
                                                      new BossChartConfig("PictorialBar", "渐变柱状图（右下方）", 6), 
                                                      new BossChartConfig("BasicMap", "地市分布图（中间上方）", 7) }));

            BossTemplateDict.Add(1, new BossTemplate(new List<BossChartConfig>() 
                                                    { new BossChartConfig("SimpleBar", "柱状图（左上方）", 1),
                                                      new BossChartConfig("BasicLineChart", "折线图（右上方）", 2),
                                                      new BossChartConfig("BasicScatter", "点状图（左下方）", 3),
                                                      new BossChartConfig("GradientLineChart", "曲线图（右下方）", 4) }));
        }


    }

    class BossTemplate
    {
        public List<BossChartConfig> BossCharts { set; get; }
        public Image ThumbImage { set; get; }
        public string WebUrl { set; get; }

        public BossTemplate( List<BossChartConfig> bossCharts)
        {
            this.BossCharts = bossCharts;
        }
    }

    class BossChartConfig
    {
        public string Type { set; get; }
        public string CaptionText { set; get; }
        public Point LocationPanel { set; get; }

        public BossChartConfig(string type, string captionText, int idx)
        {
            Type = type;
            CaptionText = captionText;
            LocationPanel = new Point(3, (idx - 1) * 90 + 3);//idx从1开始
        }
    }

}
