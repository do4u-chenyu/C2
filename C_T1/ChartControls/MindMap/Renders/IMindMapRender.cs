using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using C2.Core;
using C2.Model;
using C2.Model.MindMaps;

namespace C2.Controls.MapViews
{
    public interface IMindMapRender
    {
        //Size Layout(MindMap map, RenderArgs e);

        void Paint(MindMap map, RenderArgs e);

        void PaintNavigationMap(MindMap map, float zoom, PaintEventArgs e);

        void PaintTopic(Topic topic, RenderArgs e);

        void PaintTopics(IEnumerable<Topic> topics, RenderArgs e);
    }
}
