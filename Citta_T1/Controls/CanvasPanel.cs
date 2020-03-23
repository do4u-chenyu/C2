using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Citta_T1.Controls.Move;
using Citta_T1.Utils;
using Citta_T1.Business;
using Citta_T1.Controls.Interface;
using Citta_T1.Business.Model;

namespace Citta_T1.Controls
{
    public delegate void NewElementEventHandler(Control ct);

    public partial class CanvasPanel : Panel
    {
        public int sizeLevel = 0;
        public event NewElementEventHandler NewElementEvent;
        private Bitmap staticImage;
        private bool startDrag = false;
        //记录拖动引起的坐标变化量
        public float screenChange = 1;

        bool MouseIsDown = false;
        Point basepoint;


        Graphics g;

        private Pen p1 = new Pen(Color.Gray, 0.0001f);

        // 绘图
        // 绘图
        public List<Line> lines = new List<Line>() { };
        public enum eCommandType
        {
            draw,
            select
        }
        public eCommandType cmd = eCommandType.select;
        public PointF startP;
        private Control startC;
        private Control endC;
        Rectangle invalidateRectWhenMoving;
        Line lineWhenMoving;

        public void SetStartP(PointF p)
        {
            startP = p;
        }
        public CanvasPanel()
        {
            InitializeComponent();
            p1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;


        }
        #region 右上角功能实现部分
        //画布右上角的放大与缩小功能实现
        public void ChangSize(bool isLarger, float factor = 1.3F)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);//禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);//双缓冲
            this.UpdateStyles();
            if (isLarger && sizeLevel <= 2)
            {
                Console.WriteLine("放大");
                sizeLevel += 1;
                this.screenChange = this.screenChange * factor;
                foreach (Control con in Controls)
                {
                    if (con is IScalable)
                    {
                        (con as IScalable).ChangeSize(sizeLevel);
                    }
                }

            }
            else if (!isLarger && sizeLevel > 0)
            {
                Console.WriteLine("缩小");
                sizeLevel -= 1;
                this.screenChange = this.screenChange / factor;
                foreach (Control con in Controls)
                {
                    if (con is IScalable)
                    {
                        (con as IScalable).ChangeSize(sizeLevel);
                    }
                }
            }
            Global.GetNaviViewControl().UpdateNaviView();
        }

        // 画布右上角的拖动功能实现
        public void ChangLoc(float dx, float dy)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲DoubleBuffer
            this.UpdateStyles();
            //SetControlByDelta(dx, dy, this);
            //foreach (Control con in Controls)
            //{   // 仅当前文档中的元素需要拖动

            //    if (con.Visible && con is IDragable)
            //    {

            //        (con as IDragable).ChangeLoc(dx, dy);
            //    }

            //}
            List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;
            foreach (ModelElement me in modelElements)
            {
                if (me.Type == ElementType.DataSource || me.Type == ElementType.Operator || me.Type == ElementType.Result)
                {
                    Control ct = me.GetControl;
                    (ct as IDragable).ChangeLoc(dx, dy); ;
                }
            }
        }
        #endregion


        #region 画布中鼠标拖动的事件
        public int startX;
        public int startY;
        public int nowX;
        public int nowY;


        public Control SetStartC { set => startC = value; }
        public Control SetEndC { set => endC = value; }


        #endregion

        #region 各种事件
        public void CanvasPanel_DragDrop(object sender, DragEventArgs e)
        {
            ElementType type = ElementType.Null;
            string path = "";
            string text = "";
            bool isutf8 = false;
            Point location = this.Parent.PointToClient(new Point(e.X - 300, e.Y - 100));
            try
            {
                type = (ElementType)e.Data.GetData("Type");
                path = e.Data.GetData("Path").ToString();
                text = e.Data.GetData("Text").ToString();
                isutf8 = (bool)e.Data.GetData("isUTF8");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            // 首先根据数据`e`判断传入的是什么类型的button，分别创建不同的Control
            if (type == ElementType.DataSource)
                AddNewDataSource(path, sizeLevel, text, location, isutf8);
            else if (type == ElementType.Operator)
                AddNewOperator(sizeLevel, text, location);
        }

        public void CanvasPanel_MouseDown(object sender, MouseEventArgs e)
        {
            // 强制编辑控件失去焦点,触发算子控件的Leave事件 
            ((MainForm)(this.Parent)).blankButton.Focus();

            if (((MainForm)(this.Parent)).flowControl.selectFrame)
            {
                MouseIsDown = true;
                
                basepoint = e.Location;

                staticImage = new Bitmap(this.Width, this.Height);
                this.DrawToBitmap(staticImage, new Rectangle(0, 0, this.Width, this.Height));
                Graphics g = Graphics.FromImage(staticImage);

                g.Dispose();
            }
            else if ((this.Parent as MainForm).flowControl.selectDrag && e.Button == MouseButtons.Left)
            {
                startX = e.X;
                startY = e.Y;
                staticImage = new Bitmap(2000, 1000);
                List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;

                Point mapOrigin = Global.GetCurrentDocument().MapOrigin;

                foreach (ModelElement me in modelElements)
                {
                    if (me.Type == ElementType.DataSource || me.Type == ElementType.Operator || me.Type == ElementType.Result)
                    {
                        Control ct = me.GetControl;
                        Point Pw = Global.GetCurrentDocument().ScreenToWorld(ct.Location, mapOrigin);
                        ct.DrawToBitmap(staticImage, new Rectangle(Pw.X, Pw.Y, 142, 25));
                    }
                    me.Hide();
                }
            }

        }

        public void CanvasPanel_MouseMove(object sender, MouseEventArgs e)
        {

            // 画框
            if (MouseIsDown && ((MainForm)(this.Parent)).flowControl.selectFrame)
            {

                Bitmap i = new Bitmap(staticImage);
                
                g = Graphics.FromImage(i);

                if (e.X < basepoint.X && e.Y < basepoint.Y)
                    g.DrawRectangle(p1, e.X, e.Y, System.Math.Abs(e.X - basepoint.X), System.Math.Abs(e.Y - basepoint.Y));
                else if (e.X > basepoint.X && e.Y < basepoint.Y)
                    g.DrawRectangle(p1, basepoint.X, e.Y, System.Math.Abs(e.X - basepoint.X), System.Math.Abs(e.Y - basepoint.Y));
                else if (e.X < basepoint.X && e.Y > basepoint.Y)
                    g.DrawRectangle(p1, e.X, basepoint.Y, System.Math.Abs(e.X - basepoint.X), System.Math.Abs(e.Y - basepoint.Y));
                else
                    g.DrawRectangle(p1, basepoint.X, basepoint.Y, System.Math.Abs(e.X - basepoint.X), System.Math.Abs(e.Y - basepoint.Y));

                Graphics n = this.CreateGraphics();
                n.DrawImageUnscaled(i, 0, 0);

                n.Dispose();
                g.Dispose();

            }

            // 控件移动
            else if (e.Button == MouseButtons.Left && ((MainForm)(this.Parent)).flowControl.selectDrag)
            {
                SetStyle(ControlStyles.UserPaint, true);
                SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
                SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲DoubleBuffer
                this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                this.SetStyle(ControlStyles.DoubleBuffer, true);
                this.SetStyle(ControlStyles.UserPaint, true);
                this.SetStyle(ControlStyles.ResizeRedraw, true);


                this.UpdateStyles();
                Console.WriteLine("正在拖拽");

                Point mapOrigin = Global.GetCurrentDocument().MapOrigin;
                mapOrigin.X = mapOrigin.X + e.X - startX;
                mapOrigin.Y = mapOrigin.Y + e.Y - startY;
                Point moveOffset = WorldBoundControl(mapOrigin);


                Bitmap i = new Bitmap(this.staticImage);
                
                Graphics n = this.CreateGraphics();
                n.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                n.Clear(this.BackColor);
                n.DrawImageUnscaled(i, mapOrigin.X - moveOffset.X, mapOrigin.Y - moveOffset.Y);



                n.Dispose();
            }
            //绘制
            else if (cmd == eCommandType.draw)
            {
                nowX = e.X;
                nowY = e.Y;
                PointF nowP = new PointF(nowX, nowY);
                if (lineWhenMoving != null)
                    invalidateRectWhenMoving = LineUtil.ConvertRect(lineWhenMoving.GetBoundingRect());
                else
                    invalidateRectWhenMoving = new Rectangle();

                Console.WriteLine("line'count = " + lines.Count().ToString());
                lineWhenMoving = new Line(startP, nowP);
                Console.WriteLine("line'count = " + lines.Count().ToString());
                // TODO 这里可能受到分辨率的影响
                RepaintStatic(invalidateRectWhenMoving);
                lineWhenMoving.OnMouseMove(nowP);
                // 重绘曲线
                RepaintObject(lineWhenMoving);

            }
        }

        private void RepaintObject(Line line)
        {
            //RelationLine line = new RelationLine();
            //Global.GetModelDocumentDao().CurrentDocument.Relations.Remove(line);
            if (line == null)
                return;
            //Bitmap bgc = (Bitmap)staticImage.Clone();
            //CanvasWrapper dc = new CanvasWrapper(this, Graphics.FromImage(bgc), ClientRectangle);
            //CanvasWrapper dc = new CanvasWrapper(this, this.Parent.CreateGraphics(), ClientRectangle);
            //RectangleF invalidaterect = obj.GetBoundingRect();
            //obj.Draw(dc, invalidaterect);
            //dc.Graphics.Dispose();
            //dc.Dispose();
            //BackgroundImage = bgc;
            //bgc.Save("Citta_repaint.png");
            g = this.CreateGraphics();
            line.DrawLine(g);
            g.Dispose();
        }

        private void RepaintStatic(Rectangle r)
        {
            if (this.staticImage == null)
                return;
            //g = Graphics.FromImage(BackgroundImage);
            g = this.CreateGraphics();
            if (r.X < 0) r.X = 0;
            if (r.X > this.staticImage.Width) r.X = 0;
            if (r.Y < 0) r.Y = 0;
            if (r.Y > this.staticImage.Height) r.Y = 0;

            if (r.Width > this.staticImage.Width || r.Width < 0)
                r.Width = this.staticImage.Width;
            if (r.Height > this.staticImage.Height || r.Height < 0)
                r.Height = this.staticImage.Height;
            // 用保存好的图来局部覆盖当前背景图
            this.staticImage.Save("Citta_repaintStatic.png");
            Pen pen = new Pen(Color.Red);
            //g.DrawRectangle(pen, r);
            pen.Dispose();
            this.staticImage.Save("Citta_repaintStatic.png");
            r.Inflate(1, 1);
            g.DrawImage(this.staticImage, r, r, GraphicsUnit.Pixel);
            g.Dispose();

        }

        public void CanvasPanel_MouseUp(object sender, MouseEventArgs e)
        {

            if (((MainForm)(this.Parent)).flowControl.selectFrame)
            {
                Bitmap i = new Bitmap(this.staticImage);
                Graphics n = this.CreateGraphics();
                n.DrawImageUnscaled(i, 0, 0);
                n.Dispose();
                // 标志位置低
                MouseIsDown = false;
            }

            else if (((MainForm)(this.Parent)).flowControl.selectDrag)
            {
                Console.WriteLine("拖拽结束");
                List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;


                    
                nowX = e.X;
                nowY = e.Y;
                Point mapOrigin = Global.GetCurrentDocument().MapOrigin;
                int dx = Convert.ToInt32((nowX - startX) / this.screenChange);
                int dy = Convert.ToInt32((nowY - startY) / this.screenChange);
                mapOrigin = new Point(mapOrigin.X + dx, mapOrigin.Y + dy);
                Point moveOffset = WorldBoundControl(mapOrigin);
                ChangLoc(dx - WorldBoundControl(mapOrigin).X, dy - moveOffset.Y);
                   
                Global.GetCurrentDocument().MapOrigin = new Point(mapOrigin.X - moveOffset.X, mapOrigin.Y - moveOffset.Y);
                startX = e.X;
                startY = e.Y;

                foreach (ModelElement me in modelElements)
                {
                    me.Show();
                }

                Global.GetNaviViewControl().UpdateNaviView();
            }

            else if (cmd == eCommandType.draw)
            {
                Line line = new Line(startP, new PointF(e.X, e.Y));
                lines.Add(line);
                Console.WriteLine("添加曲线，当前索引：" + (lines.Count() - 1).ToString() + "坐标：" + line.StartP.ToString());
                /* 
                 * TODO 控件保存连接的曲线的点，对于endP，需要保存endP是哪一个针脚
                 * 只保存线索引
                 *         __________
                 * endP1  | MControl | startP
                 * endP2  |          |
                 *         ----------
                 */
                int line_index = lines.IndexOf(line);
                (this.startC as IMoveControl).SaveStartLines(line_index);
                (this.endC as IMoveControl).SaveEndLines(line_index);
                cmd = eCommandType.select;
            }
            
        }

        //public void CanvasPanel_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (MouseIsDown && ((MainForm)(this.Parent)).flowControl.selectFrame)
        //    {

        //        Bitmap i = new Bitmap(staticImage);

        //        g = Graphics.FromImage(i);

        //        if (e.X < basepoint.X && e.Y < basepoint.Y)
        //            g.DrawRectangle(p1, e.X, e.Y, System.Math.Abs(e.X - basepoint.X), System.Math.Abs(e.Y - basepoint.Y));
        //        else if (e.X > basepoint.X && e.Y < basepoint.Y)
        //            g.DrawRectangle(p1, basepoint.X, e.Y, System.Math.Abs(e.X - basepoint.X), System.Math.Abs(e.Y - basepoint.Y));
        //        else if (e.X < basepoint.X && e.Y > basepoint.Y)
        //            g.DrawRectangle(p1, e.X, basepoint.Y, System.Math.Abs(e.X - basepoint.X), System.Math.Abs(e.Y - basepoint.Y));
        //        else
        //            g.DrawRectangle(p1, basepoint.X, basepoint.Y, System.Math.Abs(e.X - basepoint.X), System.Math.Abs(e.Y - basepoint.Y));

        //        Graphics n = this.CreateGraphics();
        //        n.DrawImageUnscaled(i, 0, 0);

        //        n.Dispose();
        //        g.Dispose();

        //    }
        //    else if (e.Button == MouseButtons.Left && ((MainForm)(this.Parent)).flowControl.selectDrag)
        //    {
        //        Console.WriteLine("拖拽:" + startDrag);
        //        startDrag = true;
        //        nowX = e.X;
        //        nowY = e.Y;
        //        Point mapOrigin = Global.GetCurrentDocument().MapOrigin;
        //        int dx = Convert.ToInt32((nowX - startX) / this.screenChange);
        //        int dy = Convert.ToInt32((nowY - startY) / this.screenChange);


        //        mapOrigin = new Point(mapOrigin.X + dx, mapOrigin.Y + dy);
        //        Point moveOffset = WorldBoundControl(mapOrigin);
        //        ChangLoc(dx - WorldBoundControl(mapOrigin).X, dy - moveOffset.Y);
        //        Global.GetCurrentDocument().MapOrigin = new Point(mapOrigin.X - moveOffset.X, mapOrigin.Y - moveOffset.Y);
        //        startX = e.X;
        //        startY = e.Y;
        //    }
        //}

        //public void CanvasPanel_MouseUp(object sender, MouseEventArgs e)
        //{
        //    selectFrame = false;
        //    if (((MainForm)(this.Parent)).flowControl.selectFrame)
        //    {
        //        Bitmap i = new Bitmap(this.staticImage);
        //        Graphics n = this.CreateGraphics();
        //        n.DrawImageUnscaled(i, 0, 0);
        //        n.Dispose();
        //        //标志位置低
        //        MouseIsDown = false;
        //    }
        //    Console.WriteLine("拖拽结束");
        //    Global.GetNaviViewControl().UpdateNaviView();
        //    startDrag = false;

        //}

        public void CanvasPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Console.WriteLine("开始重绘！" + "重绘范围： " + e.ClipRectangle.ToString());
            base.OnPaint(e);
            Rectangle clipRectangle = e.ClipRectangle;
            //注释掉后看划线区域
            //MainForm mainF = Global.GetMainForm();
            //Rectangle clipRectangle = new Rectangle(mainF.panel3.Location, new Size(mainF.panel3.Width, mainF.panel3.Height));
            //g = this.CreateGraphics();
            //Pen p = new Pen(Color.Green);
            //g.DrawRectangle(p, clipRectangle);
            //p.Dispose();
            //g.Dispose();
            // 保存重绘前的图
            if (this.staticImage == null)
            {
                clipRectangle = ClientRectangle;
                this.staticImage = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
                //BackgroundImage = (Bitmap)this.staticImage.Clone();
                this.staticImage.Save("static_image_save.bmp");
            }
            CanvasWrapper dcStatic = new CanvasWrapper(this, Graphics.FromImage(this.staticImage), ClientRectangle);
            // 给staticImage上色
            dcStatic.DrawBackgroud(clipRectangle);
            // 将`需要重绘`IDrawable对象重绘在静态图上
            Draw(dcStatic, clipRectangle);
            // 将静态图绘制在CanvasPanle里
            //g = Graphics.FromImage(BackgroundImage);
            g = this.CreateGraphics();
            g.DrawImage(this.staticImage, clipRectangle, clipRectangle, GraphicsUnit.Pixel);
            g.Dispose();
            dcStatic.Dispose();

        }

        private void Draw(CanvasWrapper dcStatic, RectangleF rect)
        {
            // TODO
            int cnt = 0;
            Console.WriteLine("line'number = " + lines.Count() + ", ");
            foreach (Line line in lines)
            {
                if (line == null)
                {
                    Console.WriteLine("line == null!");
                    continue;
                }
                // 不在该区域内就别重绘了
                //bool isInRect = (line as IDrawObject).ObjectInRectangle(rect);
                //if (isInRect == false)
                //    Console.WriteLine("line 不在区域" + rect.ToString() + "内");
                //    continue;
                line.Draw(dcStatic, rect);
                Console.WriteLine("重绘线，起点坐标：" + line.StartP.ToString() + "终点坐标：" + line.EndP.ToString());
                cnt += 1;
                Console.WriteLine("已重绘" + cnt + "条曲线");
            }

        }
        #endregion

        public void DeleteElement(Control ctl)
        {
            this.Controls.Remove(ctl);
        }
        public void AddNewOperator(int sizeL, string text, Point location)
        {
            MoveOpControl btn = new MoveOpControl(
                                sizeL,
                                text,
                                location);
            AddNewElement(btn);
        }

        public void AddNewDataSource(string path, int sizeL, string text, Point location, bool encoding)
        {
            MoveDtControl btn = new MoveDtControl(
                path,
                sizeL,
                text,
                location);
            btn.Encoding = encoding;
            AddNewElement(btn);
        }

        private void AddNewElement(Control btn)
        {
            this.Controls.Add(btn);
            Global.GetNaviViewControl().AddControl(btn);
            Global.GetNaviViewControl().UpdateNaviView();
            NewElementEvent?.Invoke(btn);
        }


        #region 屏幕拖拽界限控制部分

        public Point WorldBoundControl(Point Pm)
        {
            
            Point dragOffset = new Point(0,0);
            Point Pw = Global.GetCurrentDocument().ScreenToWorld(new Point(50, 30), Pm);
            if (Pw.X < 50 )
            {
                dragOffset.X = 50 - Pw.X;
            }
            if (Pw.Y < 30 )
            {
                dragOffset.Y =  30 - Pw.Y;
            }
            if (Pw.X > 2000 - Convert.ToInt32(this.Width / this.screenChange))
            {
                dragOffset.X = 2000 - Convert.ToInt32(this.Width / this.screenChange) - Pw.X;
            }
            if (Pw.Y > 1000 - Convert.ToInt32(this.Height / this.screenChange))
            {
                dragOffset.Y = 1000 - Convert.ToInt32(this.Height / this.screenChange) - Pw.Y;
            }
            return dragOffset;
        }
        #endregion

    }
}
