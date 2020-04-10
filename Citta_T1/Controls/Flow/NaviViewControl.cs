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
using Citta_T1.Business.Model;
using Citta_T1.Controls.Interface;

namespace Citta_T1.Controls.Flow
{
    public partial class NaviViewControl : UserControl
    {
        private LogUtil log = LogUtil.GetInstance("NaviViewControl");
        private Pen p1;
        private int rate;
        private Pen p2;
        private int startX;
        private int startY;

        private Bitmap staticImage;
        private Dictionary<ModelElement, Point> elementWorldLocDict;

        public NaviViewControl()
        {
            InitializeComponent();
            this.p1 = new Pen(Color.DimGray, 0.0001f);
            this.p2 = new Pen(Color.LightGray, 0.0001f);
            this.rate = 10;
            elementWorldLocDict = new Dictionary<ModelElement, Point>(256);
        }



        public void UpdateNaviView(int rate = 10)
        {
            this.rate = rate;
            this.Invalidate(true);
        }

        private void NaviViewControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startX = e.X;
                startY = e.Y;
                List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;
                float factor = Global.GetCurrentDocument().ScreenFactor;
                Point mapOrigin = Global.GetCurrentDocument().MapOrigin;
                if (this.elementWorldLocDict.Count > 1024)
                    this.elementWorldLocDict.Clear();

                foreach (ModelElement me in modelElements)
                {
                    PointF ctOrgPosition = new PointF(me.Location.X / factor, me.Location.Y / factor);
                    PointF ctWorldPosition = Global.GetCurrentDocument().ScreenToWorldF(ctOrgPosition, mapOrigin);
                    Point loc = new Point(Convert.ToInt32(ctWorldPosition.X / rate), Convert.ToInt32(ctWorldPosition.Y / rate));
                    if (!elementWorldLocDict.ContainsKey(me))
                       elementWorldLocDict[me] = loc;
                }
            }
        }

        private void NaviViewControl_MouseUp(object sender, MouseEventArgs e)
        {

            float factor = Global.GetCurrentDocument().ScreenFactor;
            Point mapOrigin = Global.GetCurrentDocument().MapOrigin;

            int dx = Convert.ToInt32((startX - e.X ) * rate / factor);
            int dy = Convert.ToInt32((startY - e.Y ) * rate / factor);
            mapOrigin = new Point(mapOrigin.X + dx, mapOrigin.Y + dy);

            Point moveOffset = OpUtil.WorldBoundControl(mapOrigin, factor, Parent.Width, Parent.Height);
            OpUtil.ChangLoc((startX - e.X) * rate - moveOffset.X * factor, (startY - e.Y) * rate - moveOffset.Y * factor);
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

            Point viewBoxPosition;


            float factor = (this.Parent as CanvasPanel).ScreenFactor;//
            try
            {
                factor = Global.GetCurrentDocument().ScreenFactor;//
                mapOrigin = Global.GetCurrentDocument().MapOrigin;
                Point moveOffset = OpUtil.WorldBoundControl(mapOrigin, factor, Parent.Width, Parent.Height);                
                OpUtil.ChangLoc(-moveOffset.X, -moveOffset.Y);
                Global.GetCurrentDocument().MapOrigin = new Point(mapOrigin.X - moveOffset.X, mapOrigin.Y - moveOffset.Y);
                mapOrigin = Global.GetCurrentDocument().MapOrigin;               
                viewBoxPosition = Global.GetCurrentDocument().ScreenToWorld(new Point(50, 30), mapOrigin);
            }
            catch
            {
                mapOrigin = new Point(-600, -300);
                viewBoxPosition = new Point(650, 330);
            }


            if ((this.Parent as CanvasPanel).StartMove)
            {
                //UpdateImage(this.Width, this.Height, factor, mapOrigin);
                (this.Parent as CanvasPanel).StartMove = false;
            }

            Rectangle rect = new Rectangle(viewBoxPosition.X / rate, viewBoxPosition.Y / rate, Convert.ToInt32(width / factor) / rate, Convert.ToInt32(height / factor) / rate);
            gc.DrawRectangle(p2, rect);
            SolidBrush trnsRedBrush = new SolidBrush(Color.DarkGray);
            gc.FillRectangle(trnsRedBrush, rect);
            //if (this.staticImage == null)
            //{
            UpdateImage(this.Width, this.Height, factor, mapOrigin);
            //}
            gc.DrawImageUnscaled(this.staticImage, 0, 0);

        }

        private void UpdateImage(int width,int height,float factor,Point mapOrigin)
        {
            if (this.staticImage != null)
            {   // bitmap是重型资源,需要强制释放
                this.staticImage.Dispose();
                this.staticImage = null;
            }
            this.staticImage = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(staticImage);
            List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;
            log.Info("=================================================");
            foreach (ModelElement me in modelElements)
            { 
                Point ctOrgPosition = new Point(Convert.ToInt32(me.Location.X / factor), Convert.ToInt32(me.Location.Y / factor));
                Point ctWorldPosition = Global.GetCurrentDocument().ScreenToWorld(ctOrgPosition, mapOrigin);
                Point ctScreenPos = new Point(Convert.ToInt32(ctWorldPosition.X / rate), Convert.ToInt32(ctWorldPosition.Y / rate));

                if (elementWorldLocDict.ContainsKey(me))
                {
                    Point loc = elementWorldLocDict[me];
                    log.Info(String.Format("dic:{0}, scr:{1}", loc, ctScreenPos));
                    
                    if (Math.Abs(loc.X - ctScreenPos.X) + Math.Abs(loc.Y - ctScreenPos.Y) <= 2)
                    {
                        ctScreenPos = loc;
                    }
                        
                    //else
                        //elementWorldLocDict[me] = ctScreenPos;
                }
                else {
                    elementWorldLocDict[me] = ctScreenPos;
                }


                Rectangle rect = new Rectangle(ctScreenPos.X, ctScreenPos.Y, 142 / rate, 25 / rate);
                g.DrawRectangle(p1, rect);
            }

            g.Dispose();
            
        }

    }
}