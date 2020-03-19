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
        private Point viewBoxPosition,ctWorldPosition;
        private int rate;
        private Pen p1 = new Pen(Color.LightGray, 0.0001f);
        public int startX;
        public int startY;
        public int nowX;
        public int nowY;
        public bool startNaviView = false;
        public NaviViewControl()
        {
            InitializeComponent();
            this.controls = new List<Control>();
            this.pen = new Pen(Color.DimGray,0.0001f);
            this.rate = 10;
            
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
                startNaviView = true;
            }
        }



        private void NaviViewControl_MouseUp(object sender, MouseEventArgs e)
        {

            Global.GetNaviViewControl().UpdateNaviView();
            startNaviView = false;
        }

        private void NaviViewControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

            }
            
        }

        private void NaviViewControl_Paint(object sender, PaintEventArgs e)
        {
            
            Graphics gc = e.Graphics;
            int width = this.Location.X + this.Width;
            int height = this.Location.Y + this.Height;
            
            float factor = 1/(this.Parent as CanvasPanel).screenChange;
            Point dragMove1 = Global.GetCurrentDocument().MapOrigin;
            viewBoxPosition = Global.GetCurrentDocument().ScreenToWorld(new Point(50, 30), dragMove1);
            Rectangle rect = new Rectangle(viewBoxPosition.X / rate, viewBoxPosition.Y / rate, Convert.ToInt32(width * factor) / rate , Convert.ToInt32(height * factor) / rate);
            gc.DrawRectangle(p1, rect);
            SolidBrush trnsRedBrush = new SolidBrush(Color.DarkGray);
            gc.FillRectangle(trnsRedBrush, rect);

            foreach (Control ct in controls)
            {
                if (ct.Visible == true)
                {
                    ctWorldPosition = Global.GetCurrentDocument().ScreenToWorld(ct.Location, dragMove1);
                    rect = new Rectangle(Convert.ToInt32(ctWorldPosition.X * factor) / rate, Convert.ToInt32(ctWorldPosition.Y * factor) / rate, 142 / rate, 25 / rate);
                    gc.DrawRectangle(pen, rect);
                }
            }
        }
    }
}
