﻿using System;
using C2.Controls.Paint;
using C2.Core;
using C2.Model.MindMaps;

namespace C2.Controls.MapViews
{
    class StraighteningCommand : Command
    {
        Link Link;
        BezierControlPoint CP1;
        BezierControlPoint CP2;

        public StraighteningCommand(Link link)
        {
            if (link == null)
                throw new ArgumentNullException();

            CP1 = link.LayoutData.CP1;
            CP2 = link.LayoutData.CP2;

            Link = link;
        }

        public override string Name
        {
            get { return "Straightening"; }
        }

        public override bool Rollback()
        {
            var layout = Link.LayoutData;
            layout.CP1 = new BezierControlPoint(CP1.Angle, layout.CP1.Length);
            layout.CP2 = new BezierControlPoint(CP2.Angle, layout.CP2.Length);
            Link.RefreshLayout();
            Link.SetChanged();

            return true;
        }
        public override bool Redo()
        {
            return Execute();
        }
        public override bool Execute()
        {
            Link.Reset();

            return true;
        }
    }
}
