﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using C2.Configuration;
using C2.Core;
using C2.Globalization;
using NPOI.SS.Formula.Functions;

namespace C2.Model.MindMaps
{
    public partial class Topic
    {
        int? _CustomWidth;
        int? _CustomHeight;
        //const int MaxWidth = 10000;
        //const int MinWidth = 10;
        //const int MaxHeight = 10000;
        //const int MinHeight = 10;

        //Rectangle _TextBounds;

        [DefaultValue(null), LocalDisplayName("Custom Width"), LocalCategory("Layout")]
        public int? CustomWidth
        {
            get { return _CustomWidth; }
            set
            {
                if (_CustomWidth != value)
                {
                    _CustomWidth = value;
                    OnWidthChanged();
                }
            }
        }

        [DefaultValue(null), LocalDisplayName("Custom Height"), LocalCategory("Layout")]
        public int? CustomHeight
        {
            get { return _CustomHeight; }
            set
            {
                //if(value!=null)
                   // value = Math.Max(MinHeight, Math.Min(MaxHeight,(int)value));
                if (_CustomHeight != value)
                {
                    _CustomHeight = value;
                    OnHeightChanged();
                }
            }
        }

        [Browsable(false)]
        public Rectangle TextBounds { get; set; }

        /// <summary>
        /// 已经经过坐标转换
        /// 相对 Topic 左上角
        /// </summary>
        [Browsable(false)]
        public Rectangle RemarkIconBounds
        {
            get;
            set;
            //get
            //{
            //    const int IconSize = 16;
            //    const int Space = 2;

            //    return new Rectangle(0,
            //        Height + Space,
            //        IconSize,
            //        IconSize);
            //}
        }

        [Browsable(false)]
        public Rectangle FullBounds
        {
            get
            {
                var rect = Bounds;
                if (this.HaveRemark && Options.Current.GetBool(C2.Configuration.OptionNames.Charts.ShowRemarkIcon))
                {
                    rect = Rectangle.Union(rect, RemarkIconBounds);
                }
                return rect;
            }
        }

        [Browsable(false)]
        public Rectangle FoldingButton { get; set; }

        [Browsable(false)]
        public bool FoldingButtonVisible { get; set; }
    }
}
