using System.Drawing;
using Citta_T1.Controls;
using System.Xml;
using Citta_T1.Canvas;
using Citta_T1.Model.Styles;

namespace Citta_T1.ChartControls.MindMap.Lines
{
    interface ILine
    {
        void DrawLine(IGraphics graphics, IPen pen,
            TopicShape shapeFrom, TopicShape shapeTo, 
            Rectangle rectFrom, Rectangle rectTo, Vector4 vectorFrom, Vector4 vectorTo, 
            LineAnchor startAnchor, LineAnchor endAnchor);
    }
}
