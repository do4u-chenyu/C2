using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PdfSharp.Drawing;

namespace C2.Canvas.Pdf
{
    class PdfGraphicsState : IGraphicsState
    {
        XGraphicsState State;

        public PdfGraphicsState(XGraphicsState state)
        {
            State = state;
        }

        public object Raw
        {
            get { return State; }
        }
    }
}
