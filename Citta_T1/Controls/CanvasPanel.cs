using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Citta_T1.Controls.Move;
using Citta_T1.Utils;
using Citta_T1.Controls.Interface;
using Citta_T1.Business.Model;

namespace Citta_T1.Controls
{
    public delegate void NewElementEventHandler(Control ct);

    public partial class CanvasPanel : UserControl
    {
        private LogUtil log = LogUtil.GetInstance("CanvasPanel");
        public int sizeLevel = 0;
        public event NewElementEventHandler NewElementEvent;
        public Bitmap staticImage;
        
        //屏幕拖动涉及的变量
        private float screenFactor = 1;
        private bool startMove = false;
        private DragWrapper dragWrapper;




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


        public Control SetStartC { set => startC = value; }
        public Control SetEndC { set => endC = value; }
        public float ScreenFactor { get => screenFactor; set => screenFactor = value; }
        public bool StartMove { get => startMove; set => startMove = value; }

        public CanvasPanel()
        {
            InitializeComponent();
            p1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲DoubleBuffer
            SetStyle(ControlStyles.ResizeRedraw, true);
            dragWrapper = new DragWrapper();
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
                log.Info("放大");
                sizeLevel += 1;
                this.screenFactor = this.screenFactor * factor;
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
                log.Info("缩小");
                sizeLevel -= 1;
                this.screenFactor = this.screenFactor / factor;
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

        #endregion

        #region 各种事件
        public void CanvasPanel_DragDrop(object sender, DragEventArgs e)
        {
            ElementType type = ElementType.Null;
            string path = "";
            string text = "";
            DSUtil.Encoding isutf8 = DSUtil.Encoding.UTF8;
            Point location = this.Parent.PointToClient(new Point(e.X - 300, e.Y - 100));
            try
            {
                type = (ElementType)e.Data.GetData("Type");
                path = e.Data.GetData("Path").ToString();
                text = e.Data.GetData("Text").ToString();
                isutf8 = (DSUtil.Encoding)e.Data.GetData("Encoding");
            }
            catch (Exception ex)
            {
                log.Warn(ex.Message);
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
            if (e.Button != MouseButtons.Left) return;
            if (((MainForm)(this.Parent)).flowControl.SelectFrame)
            {
                MouseIsDown = true;

                basepoint = e.Location;

                staticImage = new Bitmap(this.Width, this.Height);
                this.DrawToBitmap(staticImage, new Rectangle(0, 0, this.Width, this.Height));
                Graphics g = Graphics.FromImage(staticImage);

                g.Dispose();
            }
            else if ((this.Parent as MainForm).flowControl.SelectDrag)
            {
                
                dragWrapper.DragDown(this.Size, this.screenFactor,e);
            }

        }
        public void CanvasPanel_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (e.Button != MouseButtons.Left) return;
            // 画框
            if (MouseIsDown && ((MainForm)(this.Parent)).flowControl.SelectFrame)
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
            else if ( ((MainForm)(this.Parent)).flowControl.SelectDrag)
            {
                dragWrapper.DragMove(this.Size, this.screenFactor, e);
            }
            //绘制
            else if (cmd == eCommandType.draw)
            {

                PointF nowP = e.Location;
                if (lineWhenMoving != null)
                    invalidateRectWhenMoving = LineUtil.ConvertRect(lineWhenMoving.GetBoundingRect());
                else
                    invalidateRectWhenMoving = new Rectangle();

                log.Info("line'count = " + lines.Count().ToString());
                lineWhenMoving = new Line(startP, nowP);
                log.Info("line'count = " + lines.Count().ToString());
                // TODO 这里可能受到分辨率的影响
                CoverPanelByRect(invalidateRectWhenMoving);
                lineWhenMoving.OnMouseMove(nowP);
                // 重绘曲线
                RepaintObject(lineWhenMoving);

            }
        }
        /*
         * 根据lines来重绘保存好的静态图
         */
        public void RepaintStatic(CanvasWrapper canvasWrp, Rectangle r, List<Line> exceptLines = null)
        {
            // 给staticImage上色
            canvasWrp.DrawBackgroud(r);
            // 将`需要重绘`IDrawable对象重绘在静态图上
            Draw(canvasWrp, r, exceptLines);
        }
        public void RepaintObject(Line line)
        {
            if (line == null)
                return;
            g = this.CreateGraphics();
            line.DrawLine(g);
            g.Dispose();
        }

        /*
         * 使用静态图的指定位置的指定大小来覆盖当前屏幕的指定位置的指定大小
         */
        public void CoverPanelByRect(Rectangle r)
        {
            if (this.staticImage == null)
                return;
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
            r.Inflate(1, 1);
            g.DrawImage(this.staticImage, r, r, GraphicsUnit.Pixel);
            g.Dispose();

        }
        public void CanvasPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return; 

            if (((MainForm)(this.Parent)).flowControl.SelectFrame)
            {
                Bitmap i = new Bitmap(this.staticImage);
                Graphics n = this.CreateGraphics();
                n.DrawImageUnscaled(i, 0, 0);
                n.Dispose();
                // 标志位置低
                MouseIsDown = false;
            }

            else if (((MainForm)(this.Parent)).flowControl.SelectDrag)
            {
                
                dragWrapper.DragUp(this.Size, this.screenFactor, e);
            }

            else if (cmd == eCommandType.draw)
            {
                Line line = new Line(startP, new PointF(e.X, e.Y));
                lines.Add(line);
                log.Info("添加曲线，当前索引：" + (lines.Count() - 1).ToString() + "坐标：" + line.StartP.ToString());
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


        public void CanvasPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        private void CanvasPanel_Paint(object sender, PaintEventArgs e)
        {

            if (dragWrapper.DragPaint(this.Size, this.screenFactor, e))
            {
                return;
            }

            Rectangle clipRectangle = e.ClipRectangle;

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


        private void Draw(CanvasWrapper dcStatic, RectangleF rect, List<Line> exceptLines = null)
        {
            // TODO
            int cnt = 0;
            log.Info("line'number = " + lines.Count() + ", ");
            IEnumerable<Line> drawLines = exceptLines == null ? this.lines : this.lines.Except(exceptLines);
            foreach (Line line in drawLines)
            {
                if (line == null)
                {
                    log.Info("line == null!");
                    continue;
                }
                // 不在该区域内就别重绘了
                //bool isInRect = (line as IDrawObject).ObjectInRectangle(rect);
                //if (isInRect == false)
                //    Console.WriteLine("line 不在区域" + rect.ToString() + "内");
                //    continue;
                line.Draw(dcStatic, rect);
                log.Info("重绘线，起点坐标：" + line.StartP.ToString() + "终点坐标：" + line.EndP.ToString());
                cnt += 1;
                log.Info("已重绘" + cnt + "条曲线");
            }

        }
        #endregion

        public void DeleteElement(Control ctl)
        {
            this.Controls.Remove(ctl);
        }
        public void AddNewOperator(int sizeL, string text, Point location)
        {
            startMove = true;
            MoveOpControl btn = new MoveOpControl(
                                sizeL,
                                text,
                                location);
            AddNewElement(btn);
        }

        public void AddNewDataSource(string path, int sizeL, string text, Point location, DSUtil.Encoding encoding)
        {
            startMove = true;
            MoveDtControl btn = new MoveDtControl(
                path,
                sizeL,
                text,
                location);
            btn.Encoding = encoding;
            AddNewElement(btn);
        }
        public MoveRsControl AddNewResult(int sizeL, string text, Point location) 
        {
            startMove = true;
            MoveRsControl btn = new MoveRsControl(
                                sizeL,
                                text,
                                location);
            AddNewElement(btn);
            return btn;
        }

        private void AddNewElement(Control btn)
        {
            this.Controls.Add(btn);
            Global.GetNaviViewControl().AddControl(btn);
            Global.GetNaviViewControl().UpdateNaviView();
            NewElementEvent?.Invoke(btn);
        }

        private bool SelectDrag()
        {
            return Global.GetFlowControl().SelectDrag;
        }

        private bool SelectFrame()
        {
            return Global.GetFlowControl().SelectFrame;
        }
    }
}