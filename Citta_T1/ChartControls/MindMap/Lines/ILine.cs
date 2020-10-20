using System.Drawing;
using C2.Controls;
using System.Xml;
using C2.Canvas;
using C2.Model.Styles;

namespace C2.ChartControls.MindMap.Lines
{
    interface ILine
    {
        void DrawLine(IGraphics graphics, IPen pen,
            TopicShape shapeFrom, TopicShape shapeTo, 
            Rectangle rectFrom, Rectangle rectTo, Vector4 vectorFrom, Vector4 vectorTo, 
            LineAnchor startAnchor, LineAnchor endAnchor);
    }
}
