﻿using C2.Business.Model;
using C2.Core;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace C2.Controls
{
    class DragWrapper
    {
        private CanvasPanel canvasPanel;
        public int Width { get; set; }
        public int Height { get; set; }
        public float Factor { get; set; }
        public bool StartDrag { get; set; }
        public int WorldWidth { get; set; }
        public int WorldHeight { get; set; }
        public Point Start { get; set; }
        public Point Now { get; set; }
        public Bitmap StaticImage { get; set; }

        public DragWrapper(CanvasPanel canvas)
        {
            canvasPanel = canvas;
            WorldWidth = 2000;
            WorldHeight = 1000;
            StartDrag = false;
        }
        public void InitDragWrapper(Size canvasSize, float canvasFactor)
        {
            Width = canvasSize.Width;
            Height = canvasSize.Height;
            Factor = canvasFactor;
        }
        public void DragDown(Size canvasSize, float canvasFactor, MouseEventArgs e)
        {
            this.StartDrag = true;
            this.Start = e.Location;
            if (this.StaticImage != null)
            {   // bitmap是重型资源,需要强制释放
                this.StaticImage.Dispose();
                this.StaticImage = null;
            }
            this.InitDragWrapper(canvasSize, canvasFactor);
            this.StaticImage = this.CreateWorldImage();
        }
        public void DragMove(Size canvasSize, float canvasFactor, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;
            this.Now = e.Location;
            this.InitDragWrapper(canvasSize, canvasFactor);
            Graphics n = Global.GetCanvasPanel().CreateGraphics();

            this.MoveWorldImage(n);
            n.Dispose();
        }
        public virtual void DragUp(Size canvasSize, float canvasFactor, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;
            Graphics n = Global.GetCanvasPanel().CreateGraphics();
            this.Now = e.Location;
            this.InitDragWrapper(canvasSize, canvasFactor);
            this.MoveWorldImage(n);
            this.ControlChange();
            n.Dispose();
            this.StartDrag = false;
            this.Start = e.Location;

        }
        public bool DragPaint(Size canvasSize, float canvasFactor, PaintEventArgs e)
        {
            if (this.StartDrag && this.StaticImage != null)
            {
                this.InitDragWrapper(canvasSize, canvasFactor);
                this.MoveWorldImage(e.Graphics);
                return true;
            }
            return false;
        }
        //生成当前模型控件快照
        public virtual Bitmap CreateWorldImage()
        {
            Bitmap staticImage = new Bitmap(Convert.ToInt32(WorldWidth * Factor), Convert.ToInt32(WorldHeight * Factor));
            Graphics g = Graphics.FromImage(staticImage);
            
            g.SmoothingMode = SmoothingMode.HighQuality;//去掉锯齿
            g.CompositingQuality = CompositingQuality.HighQuality;//合成图像的质量
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;//去掉文字的锯齿

            g.Clear(Color.White);
            List<ModelElement> modelElements = Global.GetCurrentModelDocument().ModelElements;
            List<ModelRelation> modelRelations = Global.GetCurrentModelDocument().ModelRelations;


            // 先画线，避免线盖住控件
            foreach (ModelRelation mr in modelRelations)
            {
                Point Pw = Global.GetCurrentModelDocument().WorldMap.ScreenToWorld(mr.GetBoundingRect().Location, false);
                if (Pw.X < 0 || Pw.Y < 0)
                    continue;

                PointF s = Global.GetCurrentModelDocument().WorldMap.ScreenToWorldF(mr.StartP, false);
                PointF a = Global.GetCurrentModelDocument().WorldMap.ScreenToWorldF(mr.A, false);
                PointF b = Global.GetCurrentModelDocument().WorldMap.ScreenToWorldF(mr.B, false);
                PointF e = Global.GetCurrentModelDocument().WorldMap.ScreenToWorldF(mr.EndP, false);
                LineUtil.DrawBezier(g, s, a, b, e, mr.Selected);
            }
            // 反向遍历,解决Move时旧控件压在新控件上
            for (int i = 0; i < modelElements.Count; i++)
            {
                ModelElement me = modelElements[modelElements.Count - i - 1];
                Control ct = me.InnerControl;
                Point Pw = Global.GetCurrentModelDocument().WorldMap.ScreenToWorld(ct.Location, false);
                if (Pw.X < 0 || Pw.Y < 0)
                    continue;
                ct.DrawToBitmap(staticImage, new Rectangle(Pw.X, Pw.Y, ct.Width, ct.Height));
                me.Hide();
            }
            g.Dispose();
            return staticImage;
        }

        public virtual void MoveWorldImage(Graphics n)
        {
            DragEdgeCheck(out Point mapOrigin, out Point moveOffset);
            mapOrigin.X = Convert.ToInt32(mapOrigin.X * Factor) + Now.X - Start.X;
            mapOrigin.Y = Convert.ToInt32(mapOrigin.Y * Factor) + Now.Y - Start.Y;
            moveOffset.X = Convert.ToInt32(moveOffset.X * Factor);
            moveOffset.Y = Convert.ToInt32(moveOffset.Y * Factor);
            n.DrawImageUnscaled(this.StaticImage, mapOrigin.X - moveOffset.X, mapOrigin.Y - moveOffset.Y);
        }

        public void ControlChange()
        {
            DragEdgeCheck(out Point mapOrigin, out Point moveOffset);
            int dx = Convert.ToInt32((Now.X - Start.X) / this.Factor);
            int dy = Convert.ToInt32((Now.Y - Start.Y) / this.Factor);

            // 移动当前文档中的所有控件
            LineUtil.ChangeLoc(Now.X - Start.X - moveOffset.X * Factor, Now.Y - Start.Y - moveOffset.Y * Factor);
            OpUtil.CanvasDragLocation(Now.X - Start.X - moveOffset.X * Factor, Now.Y - Start.Y - moveOffset.Y * Factor);
            // 获得移动获得世界坐标原点

            Global.GetCurrentModelDocument().WorldMap.MapOrigin = new Point(mapOrigin.X + dx - moveOffset.X, mapOrigin.Y + dy - moveOffset.Y);
            // 将所有控件都显示出来
            Global.GetCurrentModelDocument().ModelElements.ForEach(me => me.Show());

            Global.GetCurrentModelDocument().UpdateAllLines();
            Global.GetCanvasPanel().Invalidate(false);
            Global.GetNaviViewControl().UpdateNaviView();
        }
        private void DragEdgeCheck(out Point mapOrigin, out Point moveOffset)
        {
            mapOrigin = Global.GetCurrentModelDocument().WorldMap.MapOrigin;
            int dx = Convert.ToInt32((Now.X - Start.X) / Factor);
            int dy = Convert.ToInt32((Now.Y - Start.Y) / Factor);
            Global.GetCurrentModelDocument().WorldMap.MapOrigin = new Point(mapOrigin.X + dx, mapOrigin.Y + dy);
            moveOffset = Global.GetCurrentModelDocument().WorldMap.WorldBoundControl(Factor, Width, Height);
            Global.GetCurrentModelDocument().WorldMap.MapOrigin = mapOrigin;
        }
    }
}