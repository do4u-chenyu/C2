﻿using System;
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
        // 边界处理
        private void VieBox_BoundProcess()
        {

        }
        private void NaviViewControl_Paint(object sender, PaintEventArgs e)
        {
            Console.WriteLine("浮动窗, X = " + this.Location.X.ToString() + ", Y = " + this.Location.Y.ToString());
            Graphics gc = e.Graphics;
            int width = this.Location.X + this.Width;
            int height = this.Location.Y + this.Height;

            viewBoxPosition = ScreenToWorld(new Point(1, 1), "sub");
            Rectangle rect = new Rectangle(viewBoxPosition.X / rate, viewBoxPosition.Y / rate, width / rate, height / rate);
            gc.DrawRectangle(p1, rect);
            SolidBrush trnsRedBrush = new SolidBrush(Color.DarkGray);
            gc.FillRectangle(trnsRedBrush, rect);

            foreach (Control ct in controls)
            {
                if (ct.Visible == true)
                {
                    ctWorldPosition = ScreenToWorld(ct.Location,"sub") ;
                    //rect = new Rectangle(ctWorldPosition.X / rate, ctWorldPosition.Y / rate, ct.Width / rate, ct.Height / rate);
                    //gc.DrawRectangle(pen, rect);
                    Rectangle rect1 = new Rectangle(ctWorldPosition.X / rate, ctWorldPosition.Y / rate, ct.Width / rate, ct.Height / rate);
                    gc.DrawRectangle(pen, rect1);
                }
            }
        }
    }
}
