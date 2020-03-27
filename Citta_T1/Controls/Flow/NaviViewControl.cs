using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Citta_T1.Utils;

namespace Citta_T1.Controls.Flow
{
    public partial class NaviViewControl : UserControl
    {
        private List<Control> controls;
        private Pen pen;
        private Point viewBoxPosition, ctWorldPosition;
        private int rate;
        private Pen p1 = new Pen(Color.LightGray, 0.0001f);
        private int startX;
        private int startY;
        private int nowX;
        private int nowY;
        private DragWrapper dragWrapper;
        public NaviViewControl()
        {
            InitializeComponent();
            this.controls = new List<Control>();
            this.pen = new Pen(Color.DimGray, 0.0001f);
            this.rate = 10;
            dragWrapper = new DragWrapper();
        }



        public void UpdateNaviView(int rate = 10)
        {
            this.rate = rate;
            this.Invalidate(true);
        }
        public void AddControl(Control ct)
        {

            this.controls.Add(ct);

        }
        public void RemoveControl(Control ct)
        {
            this.controls.Remove(ct);
        }

        private void NaviViewControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startX = e.X;
                startY = e.Y;
            }
        }

        private void NaviViewControl_MouseUp(object sender, MouseEventArgs e)
        {


            float factor = (this.Parent as CanvasPanel).ScreenFactor;
            nowX = e.X;
            nowY = e.Y;
            
            dragWrapper.InitDragWrapper(this.Parent.Size, factor);
            Point mapOrigin = Global.GetCurrentDocument().MapOrigin;
            int dx = Convert.ToInt32((startX - nowX ) * rate / factor);
            int dy = Convert.ToInt32((startY - nowY ) * rate / factor);
            mapOrigin = new Point(mapOrigin.X + dx, mapOrigin.Y + dy);

            Point moveOffset = dragWrapper.WorldBoundControl(mapOrigin);
            dragWrapper.ChangLoc((startX - nowX) * rate - moveOffset.X * factor, (startY - nowY) * rate - moveOffset.Y * factor);
            Global.GetCurrentDocument().MapOrigin = new Point(mapOrigin.X - moveOffset.X, mapOrigin.Y - moveOffset.Y);
            startX = e.X;
            startY = e.Y;
            Global.GetNaviViewControl().UpdateNaviView();
        }

        private void NaviViewControl_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void NaviViewControl_Paint(object sender, PaintEventArgs e)
        {

            Graphics gc = e.Graphics;
            Point mapOrigin;
            int width = this.Location.X + this.Width;
            int height = this.Location.Y + this.Height;



            float factor = (this.Parent as CanvasPanel).ScreenFactor;
            try
            {
                mapOrigin = Global.GetCurrentDocument().MapOrigin;
                
                dragWrapper.InitDragWrapper(this.Parent.Size, factor);
                Point moveOffset = dragWrapper.WorldBoundControl(mapOrigin);

                if (moveOffset != new Point(0, 0))
                {
                    Console.WriteLine("发生越界");
                    dragWrapper.ChangLoc(-moveOffset.X, -moveOffset.Y);
                    Global.GetCurrentDocument().MapOrigin = new Point(mapOrigin.X - moveOffset.X, mapOrigin.Y - moveOffset.Y);
                    mapOrigin = Global.GetCurrentDocument().MapOrigin;
                }
                viewBoxPosition = Global.GetCurrentDocument().ScreenToWorld(new Point(50, 30), mapOrigin);
            }
            catch
            {
                mapOrigin = new Point(-600, -300);
                viewBoxPosition = new Point(650, 330);
            }
            Rectangle rect = new Rectangle(viewBoxPosition.X / rate, viewBoxPosition.Y / rate, Convert.ToInt32(width / factor) / rate, Convert.ToInt32(height / factor) / rate);
            gc.DrawRectangle(p1, rect);
            SolidBrush trnsRedBrush = new SolidBrush(Color.DarkGray);
            gc.FillRectangle(trnsRedBrush, rect);

            foreach (Control ct in controls)
            {
                if (ct.Visible == true)
                {
                    Point ctOrgPosition = new Point(Convert.ToInt32(ct.Location.X / factor), Convert.ToInt32(ct.Location.Y / factor));
                    ctWorldPosition = Global.GetCurrentDocument().ScreenToWorld(ctOrgPosition, mapOrigin);
                    rect = new Rectangle(Convert.ToInt32(ctWorldPosition.X / rate), Convert.ToInt32(ctWorldPosition.Y / rate), 142 / rate, 25 / rate); gc.DrawRectangle(pen, rect);
                }
            }
        }
    }
}