using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Citta_T1.Core;
using Citta_T1.Model;
using Citta_T1.Model.MindMaps;

namespace Citta_T1.Controls.MapViews
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
