using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Citta_T1.Controls.Move;
using Citta_T1.Utils;
using Citta_T1.Business;

namespace Citta_T1.Controls
{
    public delegate void NewElementEventHandler(Control ct);

    public partial class CanvasPanel : Panel
    {
        public int sizeLevel = 0;
        public event NewElementEventHandler NewElementEvent;
        private Bitmap staticImage;

        //记录拖动引起的坐标变化量
        public float screenChange = 1;

        bool MouseIsDown = false;
        Point basepoint;


        Graphics g;

        private Pen p1 = new Pen(Color.Gray, 0.0001f);

        // 绘图
        public List<Line> lines = new List<Line>() { };
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
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲DoubleBuffer

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

            //SetControlByDelta(dx, dy, this);
            foreach (Control con in Controls)
            {   // 仅当前文档中的元素需要拖动
                if (con.Visible && con is IDragable)
                {
                    (con as IDragable).ChangeLoc(dx, dy);
                }
            }
        }
        #endregion


        #region 画布中鼠标拖动的事件
        public int startX;
        public int startY;
        public int nowX;
        public int nowY;

        #endregion

        #region 各种事件
        public void CanvasPanel_DragDrop(object sender, DragEventArgs e)
        {
            ElementType type = ElementType.Null;
            string path = "";
            string text = "";
            Point location = this.Parent.PointToClient(new Point(e.X - 300, e.Y - 100));
            try
            {
                type = (ElementType)e.Data.GetData("Type");
                path = e.Data.GetData("Path").ToString();
                text = e.Data.GetData("Text").ToString();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            // 首先根据数据`e`判断传入的是什么类型的button，分别创建不同的Control
            if (type == ElementType.DataSource)
                AddNewDataSource(path, sizeLevel, text, location);
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

                staticImage = new Bitmap(this.Width,this.Height);
                Graphics g = Graphics.FromImage(staticImage);
                g.Clear(this.BackColor);
                g.Dispose();
            }
            else if ((this.Parent as MainForm).flowControl.selectDrag && e.Button == MouseButtons.Left)
            {
                startX = e.X;
                startY = e.Y;
                Console.WriteLine("Before, X = " + startX.ToString() + ", Y = " + startY.ToString());
            }
        }

        
        public void CanvasPanel_MouseMove(object sender, MouseEventArgs e)
        {
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
            else if (e.Button == MouseButtons.Left && ((MainForm)(this.Parent)).flowControl.selectDrag)
            {
                
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
            }
        }

        public void CanvasPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (((MainForm)(this.Parent)).flowControl.selectFrame)
            {
                Bitmap i = new Bitmap(this.staticImage);
                Graphics n = this.CreateGraphics();
                n.DrawImageUnscaled(i, 0, 0);
                n.Dispose();
                //标志位置低
                MouseIsDown = false;
            }
            Console.WriteLine("拖拽结束");
            Global.GetNaviViewControl().UpdateNaviView();

        }

        public void CanvasPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // `Invalidate`方法会强制重绘panel，因为调用该方法
            base.OnPaint(e);
            Graphics g = e.Graphics;
            if (lines.Count() > 0)
            {
                foreach(Line line in lines)
                {
                    line.DrawLine(g);
                }
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

        public void AddNewDataSource(string path, int sizeL, string text, Point location)
        {
            MoveDtControl btn = new MoveDtControl(
                path,
                sizeL,
                text,
                location);
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
