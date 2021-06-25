﻿using System;
using System.Drawing;
using System.Drawing.Printing;
using C2.Canvas;
using C2.Canvas.GdiPlus;
using C2.Configuration;
using C2.Core;
using C2.Model.MindMaps;

namespace C2.Controls.MapViews
{
    public class RenderArgs
    {
        RenderMode _Mode = RenderMode.UserInface;

        public RenderArgs()
        {
            ShowRemarkIcon = Options.Current.GetBool(OptionNames.Charts.ShowRemarkIcon);
            ShowLineArrowCap = Options.Current.GetBool(OptionNames.Charts.ShowLineArrowCap);
        }

        public RenderArgs(IGraphics graphics, MindMap chart, IFont font)
            : this()
        {
            Mode = RenderMode.Export;
            Chart = chart;
            Graphics = graphics;
            Font = font;
        }

        public RenderArgs(RenderMode mode, Graphics graphics, MindMap chart, Font font)
            : this()
        {
            Mode = mode;
            Graphics = new GdiGraphics(graphics);
            Chart = chart;
            Font = new GdiFont(font);
        }

        public RenderArgs(RenderMode mode, Graphics graphics, MindMapView view, Font font)
            : this()
        {
            Mode = mode;
            Graphics = new GdiGraphics(graphics);
            View = view;
            Font = new GdiFont(font);

            if (view != null)
                Chart = view.Map;
        }

        public MindMap Chart { get; private set; }

        public IGraphics Graphics { get; private set; }

        /// <summary>
        /// 这个值是为了提供对象的状态(如选中,滑过,按下等)
        /// 注意, 该值可能为空
        /// </summary>
        public MindMapView View { get; private set; }

        public IFont Font { get; private set; }

        public int ItemsSpace
        {
            get { return Chart.ItemsSpace; }// (int)Math.Ceiling(View.Map.Style.ItemsSpace * Zoom); }
        }

        public int LayerSpace
        {
            get { return Chart.LayerSpace; }// (int)Math.Ceiling(View.Map.Style.LayerSpace * Zoom); }
        }

        public RenderMode Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }

        public bool ShowRemarkIcon { get; private set; }

        public bool ShowLineArrowCap { get; set; }
    }
}
