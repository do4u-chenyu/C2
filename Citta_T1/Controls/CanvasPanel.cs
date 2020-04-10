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
        //public int sizeLevel = 0;
        public event NewElementEventHandler NewElementEvent;
        public Bitmap staticImage;
        public Bitmap staticImage2;


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
        public List<Bezier> lines = new List<Bezier>() { };
        public enum ECommandType
        {
            Hold,
            PinDraw,
            Null,
        }
        public ECommandType cmd = ECommandType.Null;
        public PointF startP;
        public PointF endP;
        private Control startC;
        private Control endC;
        Rectangle invalidateRectWhenMoving;
        Bezier lineWhenMoving;

        public void SetStartP(PointF p)
        {
            startP = p;
        }


        public Control StartC { get => startC; set => startC = value; }
        public Control EndC { get => endC;  set => endC = value; }
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
            int sizeLevel = Global.GetCurrentDocument().SizeL;
            if (isLarger && sizeLevel <= 2)
            {
                log.Info("放大");
                sizeLevel += 1;
                Global.GetCurrentDocument().ScreenFactor *= factor;
                log.Info(Global.GetCurrentDocument().ScreenFactor .ToString()+ "放大倍数");

                foreach (Control con in Controls)
                {
                    if (con is IScalable && con.Visible)
                    {
                        (con as IScalable).ChangeSize(sizeLevel);
                    }
                }
                foreach (ModelRelation mr in Global.GetCurrentDocument().ModelRelations)
                {
                    mr.ZoomIn();
                }
            }
            else if (!isLarger && sizeLevel > 0)
            {
                log.Info("缩小");
                sizeLevel -= 1;
                Global.GetCurrentDocument().ScreenFactor /=  factor;
                log.Info(Global.GetCurrentDocument().ScreenFactor.ToString() + "放大倍数");
                foreach (Control con in Controls)
                {
                    if (con is IScalable && con.Visible)
                    {
                        (con as IScalable).ChangeSize(sizeLevel);
                    }
                }
                foreach (ModelRelation mr in Global.GetCurrentDocument().ModelRelations)
                {
                    mr.ZoomOut();
                }
            }
            Global.GetCurrentDocument().SizeL = sizeLevel;
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
            type = (ElementType)e.Data.GetData("Type");
            text = e.Data.GetData("Text").ToString();
            int sizeLevel = Global.GetCurrentDocument().SizeL;
            if (type == ElementType.DataSource)
            {
                path = e.Data.GetData("Path").ToString();                
                isutf8 = (DSUtil.Encoding)e.Data.GetData("Encoding");
                AddNewDataSource(path, sizeLevel, text, location, isutf8);
            }
            else if (type == ElementType.Operator)
                AddNewOperator(sizeLevel, text, location);
        }

        public void CanvasPanel_MouseDown(object sender, MouseEventArgs e)
        {
            // 卡的版本
            // 强制编辑控件失去焦点,触发算子控件的Leave事件 
            ((MainForm)(this.Parent)).blankButton.Focus();
            if (sender is MoveDtControl || sender is MoveOpControl || sender is MoveRsControl)
            {
                this.cmd = ECommandType.PinDraw;
                // 不能乱写
                this.StartC = sender as Control;
                this.SetStartP(new PointF(e.X, e.Y));
                // 初始化静态图
                if (this.staticImage == null)
                {
                    this.staticImage = new Bitmap(this.Width, this.Height);

                    Graphics g = Graphics.FromImage(this.staticImage);
                    g.Clear(this.BackColor);
                    g.Dispose();
                }
                CanvasWrapper dcStatic = new CanvasWrapper(this, Graphics.FromImage(this.staticImage), this.ClientRectangle);
                this.RepaintStatic(dcStatic, new Rectangle(this.Location, new Size(this.Width, this.Height)));
            }

            if (e.Button != MouseButtons.Left) return;
            if (((MainForm)(this.Parent)).flowControl.SelectFrame)
            {
                MouseIsDown = true;

                basepoint = e.Location;

                staticImage = new Bitmap(this.Width, this.Height);
                this.DrawToBitmap(staticImage, new Rectangle(0, 0, this.Width, this.Height));
            }
            else if ((this.Parent as MainForm).flowControl.SelectDrag)
            {
                
                dragWrapper.DragDown(this.Size, Global.GetCurrentDocument().ScreenFactor, e);
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
                dragWrapper.DragMove(this.Size, Global.GetCurrentDocument().ScreenFactor, e);
            }
            // 绘制
            else if (cmd == ECommandType.PinDraw)
            {
                // 吸附效果实现
                /*
                 * 1. 遍历当前Document上所有LeftPin，检查该点是否在LeftPin的附近
                 * 2. 如果在，对该点就行修正
                 * 3. 
                 */
                log.Info("开始划线");
                PointF nowP = e.Location;
                if (lineWhenMoving != null)
                    invalidateRectWhenMoving = LineUtil.ConvertRect(lineWhenMoving.GetBoundingRect());
                else
                    invalidateRectWhenMoving = new Rectangle();
                // 遍历所有OpControl的leftPin
                foreach (ModelElement modelEle in Global.GetCurrentDocument().ModelElements)
                {
                    Control con = modelEle.GetControl;
                    if (modelEle.Type == ElementType.Operator && con != startC)
                    {
                        // 修正坐标
                        nowP = (con as IMoveControl).RevisePointLoc(nowP);
                    }
                }
                endP = nowP;
                lineWhenMoving = new Bezier(startP, nowP);
                // TODO 这不不应该挡住其他的线
                CoverPanelByRect(invalidateRectWhenMoving);
                lineWhenMoving.OnMouseMove(nowP);
                
                // 重绘曲线
                RepaintObject(lineWhenMoving);

            }
        }
        /*
         * 根据lines来重绘保存好的静态图
         */
        public void RepaintStatic(CanvasWrapper canvasWrp, Rectangle r)
        {
            // 给staticImage上色
            //canvasWrp.DrawBackgroud(r);
            // 将`需要重绘`IDrawable对象重绘在静态图上
            Draw(canvasWrp, r);
        }
        public void RepaintObject(Bezier line, Graphics g)
        {
            if (line == null)
                return;
            line.DrawBezier(g);
        }

        public void RepaintObject(Bezier line)
        {
            Graphics g = this.CreateGraphics();
            if (line == null)
                return;
            line.DrawBezier(g);
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
            Pen pen = new Pen(Color.Red);
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
                
                dragWrapper.DragUp(this.Size, Global.GetCurrentDocument().ScreenFactor, e);
            }

            else if (cmd == ECommandType.PinDraw)
            {
                bool isDuplicatedRelation = false;
                ModelDocument cd = Global.GetCurrentDocument();
                /* 不是所有位置Up都能形成曲线的
                 * 如果没有endC，那就不形成线，结束绘线动作
                 */
                if (this.endC == null)
                {
                    cmd = ECommandType.Null;
                    lineWhenMoving = null;
                    return;
                }
                /* 
                 * 在Canvas_MouseMove的时候，对鼠标的终点进行
                 * 只保存线索引
                 *         __________
                 * endP1  | MControl | startP
                 * endP2  |          |
                 * 
                 *         ----------
                 */
                Bezier line = new Bezier(startP, new PointF(e.X, e.Y));
                
                ModelRelation mr = new ModelRelation(
                    (startC as IMoveControl).GetID(),
                    (endC as IMoveControl).GetID(),
                    startP,
                    new PointF(e.X, e.Y),
                    (endC as MoveOpControl).revisedPinIndex
                    );
                isDuplicatedRelation = cd.IsDuplicatedRelation(mr);
                if (!isDuplicatedRelation)
                {
                    //endC右键菜单设置Enable
                    Global.GetOptionDao().EnableControlOption(mr);


                    cd.AddModelRelation(mr);
                    cd.BindLineToControl(line, this.startC, this.endC);
                }


                cmd = ECommandType.Null;
                lineWhenMoving = null;
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
            // 拖动时的OnPaint处理

            if (Global.GetCurrentDocument() == null)
                return;

            if (dragWrapper.DragPaint(this.Size, Global.GetCurrentDocument().ScreenFactor, e))
                return;


            //TODO
            //普通状态下算子的OnPaint处理
            //遍历当前文档所有line,然后画出来
            ModelDocument doc = Global.GetCurrentDocument();
            if (doc == null)
                return;
            // 将当前文档所有的线全部画出来
            foreach (ModelRelation mr in doc.ModelRelations)
            {
                e.Graphics.DrawBezier(Pens.Green, mr.StartP, mr.A, mr.B, mr.EndP);
            }

        }


        private void Draw(CanvasWrapper dcStatic, RectangleF rect)
        {
            Dictionary<int, Bezier> mld = Global.GetCurrentDocument().ModelLineDict;
            foreach (int lineIndex in mld.Keys)
            {
                Bezier line = mld[lineIndex];
                line.DrawBezier(dcStatic.Graphics, rect);
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
            btn.Encoding = DSUtil.Encoding.UTF8;//不清楚后面怎么编码
            AddNewElement(btn);
            return btn;
        }

        private void AddNewElement(Control btn)
        {
            this.Controls.Add(btn);
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