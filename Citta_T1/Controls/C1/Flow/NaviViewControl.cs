using C2.Business.Model;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace C2.Controls.Flow
{
    public partial class NaviViewControl : UserControl
    {
        private Pen p1;  // 小元素画笔
        private Pen p2;  // 视野画笔
        private SolidBrush viewFrameBrush; // 视野框画刷
        private int rate;

        private int startX;
        private int startY;

        private Bitmap staticImage;
        private Dictionary<ModelElement, PointF> elementWorldLocDict; //缓存所有元素的世界坐标系

        public NaviViewControl()
        {
            this.p1 = new Pen(Color.DimGray, 0.0001f);
            this.p2 = new Pen(Color.LightGray, 0.0001f);
            this.viewFrameBrush = new SolidBrush(Color.DarkGray);
            this.rate = 10;
            elementWorldLocDict = new Dictionary<ModelElement, PointF>(256);
            InitializeComponent();
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
                // 界面删除元素时缓存并不及时删除, 采用积累到一定程度后, 定期清空,
                //int bufCount = this.elementWorldLocDict.Count;
                //int usrCount = Global.GetModelDocumentDao().CountAllModelElements();
                // 删除的元素太多,重置缓存
                //if (Math.Abs(bufCount - usrCount) > Math.Max(128, this.elementWorldLocDict.Count / 2))
                //{
                //    this.elementWorldLocDict.Clear();
                //    foreach (ModelDocument md in Global.GetModelDocumentDao().ModelDocuments)
                //        PushModelDocument(md);
                //}
                //else
                //    PushModelDocument(Global.GetCurrentDocument());
                PushModelDocument(Global.GetCurrentModelDocument());
            }
        }

        private void PushModelDocument(ModelDocument md)
        {
            List<ModelElement> modelElements = md.ModelElements;
            float factor = md.WorldMap.ScreenFactor;

            // 鼠标点下时,缓存所有元素世界坐标系
            foreach (ModelElement me in modelElements)
            {
                if (elementWorldLocDict.ContainsKey(me))
                    continue;
                PointF ctWorldPosition = md.WorldMap.ScreenToWorldF(me.Location, true);
                PointF loc = new PointF(ctWorldPosition.X / rate, ctWorldPosition.Y / rate);
                elementWorldLocDict[me] = loc;
            }
        }

        private void NaviViewControl_MouseUp(object sender, MouseEventArgs e)
        {

            float factor = Global.GetCurrentModelDocument().WorldMap.ScreenFactor;
            Point mapOrigin = Global.GetCurrentModelDocument().WorldMap.MapOrigin;

            int dx = Convert.ToInt32((startX - e.X) * rate / factor);
            int dy = Convert.ToInt32((startY - e.Y) * rate / factor);
            Global.GetCurrentModelDocument().WorldMap.MapOrigin = new Point(mapOrigin.X + dx, mapOrigin.Y + dy);
            // 更新canvas所有元素的位置
            Point moveOffset = Global.GetCurrentModelDocument().WorldMap
                                     .WorldBoundControl(factor, Parent.Width, Parent.Height);
            Global.GetCurrentModelDocument().WorldMap.MapOrigin = mapOrigin;
            // 修改线的位置，线的位置修改了空间位置修改不一样，需要重绘一下才能生效
            LineUtil.ChangeLoc((startX - e.X) * rate - moveOffset.X * factor, (startY - e.Y) * rate - moveOffset.Y * factor);
            Global.GetCanvasPanel().Invalidate();
            OpUtil.CanvasDragLocation((startX - e.X) * rate - moveOffset.X * factor, (startY - e.Y) * rate - moveOffset.Y * factor);

            Global.GetCurrentModelDocument().WorldMap.MapOrigin = new Point(mapOrigin.X + dx - moveOffset.X, mapOrigin.Y + dy - moveOffset.Y);
            startX = e.X;
            startY = e.Y;
            Global.GetNaviViewControl().UpdateNaviView();
            Global.GetCanvasPanel().FrameWrapper.InitFrame();
            Global.GetTopToolBarControl().ResetStatus();
        }

        private void NaviViewControl_Paint(object sender, PaintEventArgs e)
        {
            ModelDocument currentDocument = Global.GetCurrentModelDocument();
            if (currentDocument == null)
                return;

            Graphics gc = e.Graphics;
            Point mapOrigin;
            int width = this.Location.X + this.Width;
            int height = this.Location.Y + this.Height;

            Point viewBoxPosition;


            float factor = Global.GetCurrentModelDocument().WorldMap.ScreenFactor;
            try
            {

                mapOrigin = currentDocument.WorldMap.MapOrigin;

                Point moveOffset = Global.GetCurrentModelDocument().WorldMap
                                         .WorldBoundControl(factor, Parent.Width, Parent.Height);
                OpUtil.CanvasDragLocation(-Convert.ToInt32(moveOffset.X * factor), 
                                          -Convert.ToInt32(moveOffset.Y * factor));
                currentDocument.WorldMap.MapOrigin = new Point(mapOrigin.X - moveOffset.X, mapOrigin.Y - moveOffset.Y);
                mapOrigin = currentDocument.WorldMap.MapOrigin;
                viewBoxPosition = currentDocument.WorldMap.ScreenToWorld(new Point(50, 30), true);
            }
            catch
            {
                mapOrigin = new Point(-600, -300);
                viewBoxPosition = new Point(650, 330);
            }

            Rectangle rect = new Rectangle(viewBoxPosition.X / rate, viewBoxPosition.Y / rate, Convert.ToInt32(width / factor) / rate, Convert.ToInt32(height / factor) / rate);
            gc.DrawRectangle(p2, rect);
            gc.FillRectangle(viewFrameBrush, rect);
            UpdateImage(this.Width, this.Height, factor, mapOrigin);
            gc.DrawImageUnscaled(this.staticImage, 0, 0);


        }

        private void UpdateImage(int width, int height, float factor, Point mapOrigin)
        {
            ModelDocument currentDocument = Global.GetCurrentModelDocument();
            if (currentDocument == null)
                return;

            if (this.staticImage != null)
            {   // bitmap是重型资源,需要强制释放
                this.staticImage.Dispose();
                this.staticImage = null;
            }
            this.staticImage = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(staticImage);
            g.SmoothingMode = SmoothingMode.HighQuality;//去掉锯齿
            g.CompositingQuality = CompositingQuality.HighQuality;//合成图像的质量
            List<ModelElement> modelElements = currentDocument.ModelElements;
            List<ModelRelation> modelRelations = currentDocument.ModelRelations;

            foreach (ModelElement me in modelElements)
            {

                PointF ctWorldPosition = currentDocument.WorldMap.ScreenToWorldF(me.Location, true);
                PointF ctScreenPos = new PointF(ctWorldPosition.X / rate, ctWorldPosition.Y / rate);

                // 为了解决导航框拖动时,元素漂移的问题
                if (elementWorldLocDict.ContainsKey(me))
                {
                    PointF loc = elementWorldLocDict[me];
                    // 如果本次该元素的世界坐标和缓存的世界左边很接近,就直接用缓存的世界左边
                    if (Math.Abs(loc.X - ctScreenPos.X) + Math.Abs(loc.Y - ctScreenPos.Y) <= 1)
                        ctScreenPos = loc;
                    else
                        // 如果偏移过大,更新缓存的坐标
                        elementWorldLocDict[me] = ctScreenPos;
                }
                else
                { // 新增元素,加入缓存
                    elementWorldLocDict[me] = ctScreenPos;
                }

                Rectangle rect = new Rectangle(Convert.ToInt32(ctScreenPos.X), Convert.ToInt32(ctScreenPos.Y), 142 / rate, 25 / rate);
                g.DrawRectangle(p1, rect);
            }

            foreach (ModelRelation mr in modelRelations)
            {
                ModelElement startMe = currentDocument.SearchElementByID(mr.StartID);
                ModelElement endMe = currentDocument.SearchElementByID(mr.EndID);
                if (endMe == ModelElement.Empty || startMe == ModelElement.Empty)
                    continue;
                if (!elementWorldLocDict.ContainsKey(startMe) || !elementWorldLocDict.ContainsKey(endMe))
                    continue;
                PointF s = new PointF();
                PointF e = new PointF();
                s.X = elementWorldLocDict[startMe].X + Convert.ToInt32(142 / rate);
                s.Y = elementWorldLocDict[startMe].Y + Convert.ToInt32(25 / (rate * 2));
                e.X = elementWorldLocDict[endMe].X;
                e.Y = elementWorldLocDict[endMe].Y + Convert.ToInt32(25 / (rate * 2));

                Bezier line = new Bezier(s, e);
                line.DrawNaviewBezier(g);
            }

            g.Dispose();

        }

    }
}