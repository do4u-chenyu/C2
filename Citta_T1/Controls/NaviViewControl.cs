using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Controls
{
    public partial class NaviViewControl : UserControl
    {
        private List<Control> controls;
        private Pen pen;
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

        private void NaviViewControl_Paint(object sender, PaintEventArgs e)
        {
            Graphics gc = e.Graphics;
         
            foreach (Control ct in controls)
            {
                Rectangle rect = new Rectangle(ct.Location.X / rate, ct.Location.Y / rate, ct.Width / rate, ct.Height / rate);
                gc.DrawRectangle(pen, rect);
            }
        }
    }
}
