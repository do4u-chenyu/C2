﻿using C2.Core;
using C2.Forms;
using C2.Forms.Splash;
using C2.Utils;
using System;
using System.IO;
using System.Windows.Forms;

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
            Items.Add(new C2ThumbItem("知识库", "各业务方向关键词库和线索库", Properties.Resources.首页_知识库, ThumbItem.ModelTypes.Knowledge));
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
                    if (!Global.GetMainForm().manualControl.Visible || Global.GetMainForm().isLeftViewPanelMinimum)
                        Global.GetMainForm().SelectLeftPanel(Global.GetMainForm().manualButton, Global.GetMainForm().manualControl);
                    new ManualSplashForm().ShowDialog();
                    break;
                //喝彩城堡
                case ThumbItem.ModelTypes.CastleBravo:
                    if (!Global.GetMainForm().castleBravoControl.Visible || Global.GetMainForm().isLeftViewPanelMinimum)
                        Global.GetMainForm().ShowLeftPanel(Global.GetMainForm().castleBravoButton, Global.GetMainForm().castleBravoControl);
                    Global.GetCastleBravoControl().AddLabelClick();
                    break;
                //实验楼
                case ThumbItem.ModelTypes.IAOLab:
                    if (!Global.GetMainForm().iaoLabControl.Visible || Global.GetMainForm().isLeftViewPanelMinimum)  // 避免反复点击时的闪烁
                        Global.GetMainForm().SelectLeftPanel(Global.GetMainForm().iaoLabButton, Global.GetMainForm().iaoLabControl);
                    new IAOLabelSplashForm().ShowDialog();
                    break;
                //侦察兵
                case ThumbItem.ModelTypes.WTD:
                    if (!Global.GetMainForm().websiteFeatureDetectionControl.Visible || Global.GetMainForm().isLeftViewPanelMinimum)
                        Global.GetMainForm().ShowLeftPanel(Global.GetMainForm().detectionButton, Global.GetMainForm().websiteFeatureDetectionControl);
                    Global.GetWebsiteFeatureDetectionControl().AddLabelClick();
                    break;
                //APK监测站
                case ThumbItem.ModelTypes.APK:
                    if (!Global.GetMainForm().iaoLabControl.Visible || Global.GetMainForm().isLeftViewPanelMinimum)  // 避免反复点击时的闪烁
                        Global.GetMainForm().SelectLeftPanel(Global.GetMainForm().iaoLabButton, Global.GetMainForm().iaoLabControl);
                    if (!string.IsNullOrEmpty(chromePath))
                    {
                        System.Diagnostics.Process.Start(chromePath, "http://113.31.110.244:6663/ns/APPtest/home");
                    }
                    else
                        MessageBox.Show("未能找到chrome启动路径");
                    break;
                //知识库
                case ThumbItem.ModelTypes.Knowledge:
                    if (!Global.GetMainForm().iaoLabControl.Visible || Global.GetMainForm().isLeftViewPanelMinimum)  // 避免反复点击时的闪烁
                        Global.GetMainForm().SelectLeftPanel(Global.GetMainForm().iaoLabButton, Global.GetMainForm().iaoLabControl);
                    if (!string.IsNullOrEmpty(chromePath))
                    {
                        System.Diagnostics.Process.Start(chromePath, "15.73.3.241:19001/KnowledgeBase/");
                    }
                    else
                        MessageBox.Show("未能找到chrome启动路径");
                    break;
                //HIBU
                case ThumbItem.ModelTypes.HIBU:
                    if (!Global.GetMainForm().HIBUControl.Visible || Global.GetMainForm().isLeftViewPanelMinimum)  // 避免反复点击时的闪烁
                        Global.GetMainForm().SelectLeftPanel(Global.GetMainForm().HIBUButton, Global.GetMainForm().HIBUControl);
                    new HIBUSplashForm().ShowDialog();
                    break;
                case ThumbItem.ModelTypes.Model:
                    CanvasForm cf = Global.GetMainForm().SearchCanvasForm(Path.Combine(Global.MarketViewPath, item.Text));
                    if (cf != null)
                        Global.GetMainForm().SelectForm(cf);
                    else
                        Global.GetMainForm().LoadCanvasFormByXml(Global.MarketViewPath, item.Text);
                    break;
                default:
                    break;
            }
        }
    }
}
