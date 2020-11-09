using C2.Controls.MapViews;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;

namespace C2.Model.Widgets
{
    public abstract class C2BaseWidget : Widget
    {
        [Browsable(false)]
        public List<DataItem> DataItems { get; set; }
        public override bool ResponseMouse
        {
            get => true;
        }

        public override Size CalculateSize(MindMapLayoutArgs e)
        {
            return new Size(20, 20);
        }


    }
}
