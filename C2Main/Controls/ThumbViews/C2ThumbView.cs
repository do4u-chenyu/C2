using C2.Business.CastleBravo.Intruder;
using C2.Core;
using C2.Forms.Splash;
using C2.Utils;
using System;

namespace C2.Controls.ThumbViews
{
    class C2ThumbView : RecentFilesView
    {
        public C2ThumbView()
        {
            InitC2ThumbItems();
        }

        private void InitC2ThumbItems()
        {
            Items.Add(new C2ThumbItem("分析笔记", "承载分析师的分析思路、过程和结果", Properties.Resources.首页_分析笔记, ThumbItem.ModelTypes.Business));
            Items.Add(new C2ThumbItem("战术手册", "积累沉淀的战术战法和操作指导", Properties.Resources.首页_战术手册, ThumbItem.ModelTypes.Manual));
            Items.Add(new C2ThumbItem("喝彩城堡", "战术战法闭环配套的二进制和安全工具", Properties.Resources.首页_喝彩城堡, ThumbItem.ModelTypes.CastleBravo));
            Items.Add(new C2ThumbItem("实验楼", "常用分析小工具集合", Properties.Resources.首页_实验楼, ThumbItem.ModelTypes.IAOLab));
            Items.Add(new C2ThumbItem("网站侦察兵", "对网站分类、爬取、截图和信息侦察", Properties.Resources.首页_网站侦察兵, ThumbItem.ModelTypes.WTD));
            Items.Add(new C2ThumbItem("APK大眼睛", "APK逆向、信息提取和分析报告", Properties.Resources.首页_APK检测站, ThumbItem.ModelTypes.APK));
#if C2_Outer
            Items.Add(new C2ThumbItem("大马破门锤", "为大马模型定制化的破门锤工具", Properties.Resources.首页_知识库, ThumbItem.ModelTypes.Knowledge));
#else
            Items.Add(new C2ThumbItem("知识库", "各业务方向关键词库和线索库", Properties.Resources.首页_知识库, ThumbItem.ModelTypes.Knowledge));
#endif
            Items.Add(new C2ThumbItem("HIBU", "HI部23种人工智能分析工具", Properties.Resources.首页_HIBU, ThumbItem.ModelTypes.HIBU));
        }

        protected override void OnItemClick(ThumbItem item)
        {
            String chromePath = ProcessUtil.GetChromePath();
            switch (item.Types)
            {
                //分析笔记
                case ThumbItem.ModelTypes.Business:
                    Global.GetMainForm().NewDocumentForm_Click(item.Text);
                    break;
                //战术手册
                case ThumbItem.ModelTypes.Manual:
                    new ManualSplashForm().ShowDialog();
                    break;
                //喝彩城堡
                case ThumbItem.ModelTypes.CastleBravo:
                    new CastleBravoSplashForm().ShowDialog();
                    break;
                //实验楼
                case ThumbItem.ModelTypes.IAOLab:
                    new IAOLabelSplashForm().ShowDialog();
                    break;
                //侦察兵
                case ThumbItem.ModelTypes.WTD:
                    Global.GetWebsiteFeatureDetectionControl().AddLabelClick();
                    break;
                //APK大眼睛
                case ThumbItem.ModelTypes.APK:
                    System.Diagnostics.Process.Start(chromePath, Global.APKUrl);
                    break;

                //知识库
#if DEBUG
                case ThumbItem.ModelTypes.Knowledge:
                    new IntruderForm().ShowDialog();
                    break;
#else
                case ThumbItem.ModelTypes.Knowledge:
                    System.Diagnostics.Process.Start(chromePath, Global.KnowledgeUrl);
                    break;
#endif
                //HIBU
                case ThumbItem.ModelTypes.HIBU:
                    new HIBUSplashForm().ShowDialog();
                    break;
                default:
                    break;
            }
        }
    }
}
