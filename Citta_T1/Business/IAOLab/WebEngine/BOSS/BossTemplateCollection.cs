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
    class BossTemplateCollection
    {
        Dictionary<int, BossTemplate> bossTemplateDict;
        List<BossChartConfig> commonConfig01;
        List<BossChartConfig> commonConfig02;


        private static BossTemplateCollection BossTemplateConfigInstance;
        public static BossTemplateCollection GetInstance()
        {
            if (BossTemplateConfigInstance == null)
            {
                BossTemplateConfigInstance = new BossTemplateCollection();
            }
            return BossTemplateConfigInstance;
        }

        public BossTemplateCollection()
        {
            commonConfig01 = new List<BossChartConfig>()
                                                    { new BossChartConfig("SimpleBar", "柱状图（左上方）", 1),
                                                      new BossChartConfig("BasicLineChart", "折线图（右上方）", 2),
                                                      new BossChartConfig("BasicScatter", "点状图（左下方）", 3),
                                                      new BossChartConfig("GradientLineChart", "曲线图（右下方）", 4) };

            commonConfig02 = new List<BossChartConfig>()
                                                    { new BossChartConfig("SimpleBar", "柱状图（左上方）", 1),
                                                      new BossChartConfig("BasicLineChart", "折线图（左下方）", 2),
                                                      new BossChartConfig("BasicMap", "地市分布图（中间）", 3),
                                                      new BossChartConfig("GradientLineChart", "曲线图（右上方）", 4),
                                                      new BossChartConfig("StackBar", "堆叠柱状图（右下方）", 5) };

            bossTemplateDict = new Dictionary<int, BossTemplate>();
            bossTemplateDict.Add(0, new BossTemplate(new List<BossChartConfig>() 
                                                    { new BossChartConfig("SimpleBar", "柱状图（左上方）", 1), 
                                                      new BossChartConfig("BasicLineChart", "折线图（左下方）", 2), 
                                                      new BossChartConfig("BasicScatter", "点状图（中间下方）", 3), 
                                                      new BossChartConfig("GradientLineChart", "曲线图（中间下方）", 4), 
                                                      new BossChartConfig("StackBar", "堆叠柱状图（右上方）", 5), 
                                                      new BossChartConfig("PictorialBar", "渐变柱状图（右下方）", 6), 
                                                      new BossChartConfig("BasicMap", "地市分布图（中间上方）", 7) }));

            bossTemplateDict.Add(1, new BossTemplate(commonConfig01));

            bossTemplateDict.Add(2, new BossTemplate(new List<BossChartConfig>()
                                                    { new BossChartConfig("SimpleBar", "柱状图（左上方）", 1),
                                                      new BossChartConfig("BasicLineChart", "折线图（左下方）", 2),
                                                      new BossChartConfig("BasicScatter", "点状图（中间）", 3),
                                                      new BossChartConfig("GradientLineChart", "曲线图（右上方）", 4),
                                                      new BossChartConfig("StackBar", "堆叠柱状图（右下方）", 5) }));

            bossTemplateDict.Add(3, new BossTemplate(commonConfig01));
            bossTemplateDict.Add(4, new BossTemplate(commonConfig01));
            bossTemplateDict.Add(5, new BossTemplate(commonConfig01));

            bossTemplateDict.Add(6, new BossTemplate(new List<BossChartConfig>()
                                                    { new BossChartConfig("SimpleBar", "柱状图（左上方）", 1),
                                                      new BossChartConfig("BasicLineChart", "折线图（左下方）", 2),
                                                      new BossChartConfig("BasicScatter", "点状图（右上方）", 3),
                                                      new BossChartConfig("StackBar", "堆叠柱状图（右下方）", 4) }));

            bossTemplateDict.Add(7, new BossTemplate(new List<BossChartConfig>()
                                                    { new BossChartConfig("SimpleBar", "柱状图（左上方）", 1),
                                                      new BossChartConfig("BasicLineChart", "折线图（左下方）", 2),
                                                      new BossChartConfig("BasicScatter", "点状图（中间）", 3),
                                                      new BossChartConfig("GradientLineChart", "曲线图（右上方）", 4),
                                                      new BossChartConfig("StackBar", "堆叠柱状图（右下方）", 5) }));

            bossTemplateDict.Add(8, new BossTemplate(commonConfig02));

            bossTemplateDict.Add(9, new BossTemplate(new List<BossChartConfig>()
                                                    { new BossChartConfig("SimpleBar", "柱状图（左侧）", 1),
                                                      new BossChartConfig("BasicMap", "地市分布图（中上方）", 2),
                                                      new BossChartConfig("BasicPie", "饼状图（中左方）", 3),
                                                      new BossChartConfig("GradientLineChart", "曲线图（中右方）", 4),
                                                      new BossChartConfig("StackBar", "堆叠柱状图（右侧）", 5) }));

            bossTemplateDict.Add(10, new BossTemplate(commonConfig02));
            bossTemplateDict.Add(11, new BossTemplate(commonConfig02));

            bossTemplateDict.Add(12, new BossTemplate(new List<BossChartConfig>()
                                                    { new BossChartConfig("SimpleBar", "柱状图（左上方）", 1),
                                                      new BossChartConfig("BasicLineChart", "折线图（左下方）", 2),
                                                      new BossChartConfig("BasicScatter", "点状图（中间下方）", 3),
                                                      new BossChartConfig("GradientLineChart", "曲线图（中间下方）", 4),
                                                      new BossChartConfig("StackBar", "堆叠柱状图（右上方）", 5),
                                                      new BossChartConfig("PictorialBar", "渐变柱状图（右下方）", 6),
                                                      new BossChartConfig("BasicMap", "地市分布图（中间上方）", 7) }));

            bossTemplateDict.Add(13, new BossTemplate(new List<BossChartConfig>()
                                                    { new BossChartConfig("SimpleBar", "柱状图（左上方）", 1),
                                                      new BossChartConfig("GradientLineChart", "曲线图（左下方）", 2),
                                                      new BossChartConfig("BasicMap", "地市分布图（右侧）", 3) }));

            bossTemplateDict.Add(14, new BossTemplate(new List<BossChartConfig>()
                                                    { new BossChartConfig("SimpleBar", "柱状图（左侧）", 1),
                                                      new BossChartConfig("BasicMap", "地市分布图（中间）", 2),
                                                      new BossChartConfig("GradientLineChart", "曲线图（右上方）", 3),
                                                      new BossChartConfig("StackBar", "堆叠柱状图（右下方）", 4) }));
        }

        public BossTemplate GetTemplateByIdx(int idx)
        {
            return bossTemplateDict.ContainsKey(idx) ? bossTemplateDict[idx] : new BossTemplate(commonConfig01);
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
        public int Idx { set; get; }

        public BossChartConfig(string type, string captionText, int idx)
        {
            Type = type;
            CaptionText = captionText;
            Idx = idx;
            LocationPanel = new Point(3, (idx - 1) * 90 + 3);//idx从1开始
        }
    }

}
