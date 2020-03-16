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
        
        public NaviViewControl()
        {
            InitializeComponent();
            this.controls = new List<Control>();
            this.pen = new Pen(Color.Black);
            this.rate = 10;
        }



        public void UpdateNaviView(int rate = 10)
        {
            this.rate = rate;
            //System.Console.WriteLine(mainPanelSize.ToString());
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
        
        public Point ScreenToWorld(Point Ps,String op)
        {
            Point Pm = (this.Parent as LocChangeValue).NoteDrage();
            Point Pw = new Point();
            if (op == "add")
            {
                Pw.X = Ps.X + Pm.X;
                Pw.Y = Ps.Y + Pm.Y;
            }
            else if (op == "sub")
            {
                Pw.X = Ps.X - Pm.X;
                Pw.Y = Ps.Y - Pm.Y;
            }
            return Pw;
        }
        private void NaviViewControl_Paint(object sender, PaintEventArgs e)
        {
            Graphics gc = e.Graphics;


            viewBoxPosition = ScreenToWorld(new Point(0,0),"sub");
            Rectangle rect = new Rectangle(viewBoxPosition.X /rate, viewBoxPosition.Y / rate, this.Width / 2, this.Height / 2);
            gc.DrawRectangle(pen, rect);

           
            foreach (Control ct in controls)
            {
                if (ct.Visible == true)
                {
                    ctWorldPosition = ScreenToWorld(ct.Location,"sub") ;
                    rect = new Rectangle(ctWorldPosition.X / rate, ctWorldPosition.Y / rate, ct.Width / rate, ct.Height / rate);
                    gc.DrawRectangle(pen, rect);
                }
            }
        }
    }
}
