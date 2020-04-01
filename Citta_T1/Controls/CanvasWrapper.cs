using Citta_T1.Utils;
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
		private LogUtil log = LogUtil.GetInstance("MoveOpControl");

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

		public void RepaintStatic(Rectangle r, List<Line> exceptLines = null)
		{
			// 给staticImage上色
			this.DrawBackgroud(r);
			// 将`需要重绘`IDrawable对象重绘在静态图上
			this.Draw(r, exceptLines);
		}

		private void Draw(RectangleF rect, List<Line> exceptLines = null)
		{
			int cnt = 0;
			IEnumerable<Line> drawLines = exceptLines == null ? this.canvas.lines : this.canvas.lines.Except(exceptLines);
			foreach (Line line in drawLines)
			{
				if (line == null)
				{
					log.Info("line == null!");
					continue;
				}
				// 不在该区域内就别重绘了
				//bool isInRect = (line as IDrawObject).ObjectInRectangle(rect);
				//if (isInRect == false)
				//    log.Info("line 不在区域" + rect.ToString() + "内");
				//    continue;
				line.Draw(this, rect);
				log.Info("重绘线，起点坐标：" + line.StartP.ToString() + "终点坐标：" + line.EndP.ToString());
				cnt += 1;
				log.Info("已重绘" + cnt + "条曲线");
			}

		}

		public void CoverPanelByRect(Rectangle r)
		{
			if (this.canvas.staticImage == null)
				return;
			Graphics g = this.canvas.CreateGraphics();
			if (r.X < 0) r.X = 0;
			if (r.X > this.canvas.staticImage.Width) r.X = 0;
			if (r.Y < 0) r.Y = 0;
			if (r.Y > this.canvas.staticImage.Height) r.Y = 0;

			if (r.Width > this.canvas.staticImage.Width || r.Width < 0)
				r.Width = this.canvas.staticImage.Width;
			if (r.Height > this.canvas.staticImage.Height || r.Height < 0)
				r.Height = this.canvas.staticImage.Height;
			// 用保存好的图来局部覆盖当前背景图
			this.canvas.staticImage.Save("Citta_repaintStatic.png");
			Pen pen = new Pen(Color.Red);
			//g.DrawRectangle(pen, r);
			pen.Dispose();
			r.Inflate(1, 1);
			g.DrawImage(this.canvas.staticImage, r, r, GraphicsUnit.Pixel);
			g.Dispose();

		}

		public void RepaintObject(Line line)
		{
			if (line == null)
				return;
			Graphics g = this.canvas.CreateGraphics();
			line.DrawLine(g);
			g.Dispose();
		}
	}
}
