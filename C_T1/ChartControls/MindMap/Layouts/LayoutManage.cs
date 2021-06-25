﻿using C2.ChartControls.MindMap.Lines;
using C2.Core;
using C2.Model.MindMaps;

namespace C2.Controls.MapViews
{
    class LayoutManage
    {
        public static Layouter GetLayouter(MindMapLayoutType type)
        {
            switch (type)
            {
                case MindMapLayoutType.OrganizationDown:
                case MindMapLayoutType.OrganizationUp:
                    return new OrganizationLayout(type);
                case MindMapLayoutType.LogicLeft:
                case MindMapLayoutType.LogicRight:
                    return new LogicLayout(type);
                case MindMapLayoutType.TreeLeft:
                case MindMapLayoutType.TreeRight:
                    return new TreeLayout(type);
                case MindMapLayoutType.MindMap:
                default:
                    return new MindMapLayout();
            }
        }

        public static ILine GetLiner(MindMapLayoutType type)
        {
            switch (type)
            {
                case MindMapLayoutType.OrganizationDown:
                case MindMapLayoutType.OrganizationUp:
                case MindMapLayoutType.TreeLeft:
                case MindMapLayoutType.TreeRight:
                    return new BrokenLine();
                case MindMapLayoutType.LogicLeft:
                case MindMapLayoutType.LogicRight:
                case MindMapLayoutType.MindMap:
                default:
                    //return HandPaintLine.Default;
                    return new BezierLine();
            }
        }
    }
}
