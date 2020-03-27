using Citta_T1.Business.Model;
using Citta_T1.Controls.Flow;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Controls
{
    class DragWrapper
    {
        //private List<Bitmap> buffer;
        private int width;
        private int height;
        private float screenChange;

        private int worldWidth;
        private int worldHeight;

        public DragWrapper(Size canvasSize, float canvasScreenFactor)
        {
            NaviViewControl naview = Global.GetNaviViewControl();
            width = naview.Location.X + naview.Width;
            height = naview.Location.Y + naview.Height;
            worldWidth = 2000;
            worldHeight = 1000;
            screenChange = canvasScreenFactor;
        }

        //生成当前模型控件快照
        //TODO 放大缩小的情况下 部分保存成图片失败不显示 1倍无Bug 
        public Bitmap CreateWorldImage()
        {
            Bitmap staticImage = new Bitmap( Convert.ToInt32(worldWidth * screenChange), Convert.ToInt32(worldHeight * screenChange));
            Graphics g = Graphics.FromImage(staticImage);
            g.Clear(Color.White);
            List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;

            Point mapOrigin = Global.GetCurrentDocument().MapOrigin;
            //mapOrigin.X = Convert.ToInt32(mapOrigin.X * screenChange);
            //mapOrigin.Y = Convert.ToInt32(mapOrigin.Y * screenChange);
            foreach (ModelElement me in modelElements)
            {
                Control ct = me.GetControl;
                Point Pw = Global.GetCurrentDocument().ScreenToWorld(ct.Location, mapOrigin);
                if (Pw.X < 0 || Pw.Y <0){ continue; }
                ct.DrawToBitmap(staticImage, new Rectangle(Pw.X, Pw.Y, ct.Width, ct.Height));
                me.Hide();
            }
            
            g.Dispose();
            return staticImage;
        }
        // TODO 勉强解决放大缩小BUG；
        public void MoveWorldImage(Graphics n, Bitmap staticImage, Point start, Point now)
        {
            Point mapOrigin = Global.GetCurrentDocument().MapOrigin;

            int dx = Convert.ToInt32((now.X - start.X) / screenChange);
            int dy = Convert.ToInt32((now.Y - start.Y) / screenChange);
            mapOrigin = new Point(mapOrigin.X + dx, mapOrigin.Y + dy);
            Point moveOffset = WorldBoundControl(mapOrigin);

            mapOrigin = Global.GetCurrentDocument().MapOrigin;
            mapOrigin.X = mapOrigin.X  + now.X - start.X;
            mapOrigin.Y = mapOrigin.Y  + now.Y - start.Y;
           
            Bitmap i = new Bitmap(staticImage);
            moveOffset.X = Convert.ToInt32(moveOffset.X * screenChange);
            moveOffset.Y = Convert.ToInt32(moveOffset.Y * screenChange);
            n.DrawImageUnscaled(i, mapOrigin.X - moveOffset.X, mapOrigin.Y - moveOffset.Y);
            i.Dispose();
            i = null;
        }

        public void controlChange(Point start, Point now)
        {

            Point mapOrigin = Global.GetCurrentDocument().MapOrigin;
            int dx = Convert.ToInt32((now.X - start.X) / screenChange);
            int dy = Convert.ToInt32((now.Y - start.Y) / screenChange);
            mapOrigin = new Point(mapOrigin.X + dx, mapOrigin.Y + dy);
            Point moveOffset = WorldBoundControl(mapOrigin);

            ChangLoc(now.X - start.X - moveOffset.X * screenChange, now.Y - start.Y - moveOffset.Y * screenChange);

            Global.GetCurrentDocument().MapOrigin = new Point(mapOrigin.X - moveOffset.X, mapOrigin.Y - moveOffset.Y);
            List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;
            foreach (ModelElement me in modelElements)
            {
                me.Show();
            }

            Global.GetNaviViewControl().UpdateNaviView();
        }

        public void ChangLoc(float dx, float dy)
        {

            List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;
            foreach (ModelElement me in modelElements)
            {
                 Control ct = me.GetControl;
                 if (ct is IDragable)
                    (ct as IDragable).ChangeLoc(dx, dy);
            }
        }

        public Point WorldBoundControl(Point Pm)
        {

            Point dragOffset = new Point(0, 0);
            Point Pw = Global.GetCurrentDocument().ScreenToWorld(new Point(50, 30), Pm);
            if (Pw.X < 50)
            {
                dragOffset.X = 50 - Pw.X;
            }
            if (Pw.Y < 30)
            {
                dragOffset.Y = 30 - Pw.Y;
            }
            if (Pw.X > 2000 - Convert.ToInt32(this.width / screenChange))
            {
                dragOffset.X = 2000 - Convert.ToInt32(this.width / screenChange) - Pw.X;
            }
            if (Pw.Y > 1000 - Convert.ToInt32(this.height / screenChange))
            {
                dragOffset.Y = 1000 - Convert.ToInt32(this.height / screenChange) - Pw.Y;
            }
            return dragOffset;
        }

    }
}
