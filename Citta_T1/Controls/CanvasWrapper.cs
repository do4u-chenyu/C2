using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Controls
{
    public class CanvasWrapper
	{
		CanvasPanel canvas;
		Graphics graphics;
		RectangleF rect;
		SolidBrush backgroundBrush = new SolidBrush(Color.White);
		public CanvasWrapper(CanvasPanel canvas)
		{
			this.canvas = canvas;
			this.graphics = null;
			this.rect = new Rectangle();
		}
		public CanvasWrapper(CanvasPanel canvas, Graphics graphics, Rectangle clientrect)
		{
			this.canvas = canvas;
			this.graphics = graphics;
			this.rect = clientrect;
		}

		public void DrawBackgroud(Rectangle rect)
		{
			this.graphics.FillRectangle(this.backgroundBrush, rect);
		}
		public Graphics Graphics
		{
			get { return this.graphics; }
		}
		public void Dispose()
		{
			this.graphics = null;
		}
	}
}
