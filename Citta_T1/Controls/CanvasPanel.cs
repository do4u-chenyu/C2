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
using System.Reflection;

namespace Citta_T1.Controls
{
    public partial class CanvasPanel : Panel
    {
        public int sizeLevel = 0;
        private bool isLeftMouseDown;
        private float deltaX;
        private float deltaY;

        bool MouseIsDown = false;
        Point basepoint;
        Bitmap i;
        Graphics g;
        Pen p;

        public CanvasPanel()
        {
            InitializeComponent();
        }
        #region 右上角功能实现部分
        //画布右上角的放大与缩小功能实现
        public void ChangSize(bool isLarger, float factor=1.3F)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲DoubleBuffer

            SetTag(this);
            if (isLarger && sizeLevel <= 2)
            {
                SetControlsBySize(factor, factor, this);
                sizeLevel += 1;
                Control[] mocs = Controls.Find("MoveOpControl", true);
                foreach(MoveOpControl moc in mocs)
                {
                    moc.sizeLevel += 1;
                }

            }
            else if (!isLarger && sizeLevel > 0)
            {
                sizeLevel -= 1;
                SetControlsBySize(1 / factor, 1 / factor, this);
                Control[] mocs = Controls.Find("MoveOpControl", true);
                foreach (MoveOpControl moc in mocs)
                {
                    moc.sizeLevel -= 1;
                }
            }
        }
        
        // 画布右上角的拖动功能实现
        public void ChangLoc(float dx, float dy)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲DoubleBuffer

            SetTag(this);
            SetControlByDelta(dx, dy, this);
        }
        #endregion

        #region 控件大小随窗体大小等比例缩放
        private int deep = 0;
        private MoveOpControl moc;

        //设置双缓冲区、解决闪屏问题
        public static void SetDouble(Control cc)
        {

            cc.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance |
                         System.Reflection.BindingFlags.NonPublic).SetValue(cc, true, null);

        }
        
        // 保存大小信息
        private void SetTag(Control cons)
        {
            deep += 1;
            foreach (Control con in cons.Controls)
            {
                if (con is Scalable)
                {
                    // 通过反射调用方法
                    object obj = Activator.CreateInstance(con.GetType());//创建一个obj对象
                    MethodInfo mi = con.GetType().GetMethod("SetTag");
                    mi.Invoke(obj, new Object[] {con});//调用方法
                }
            }
            deep -= 1;
        }

        // 缩放画布中的托块
        private void SetControlsBySize(float newx, float newy, Control cons)
        {
            deep += 1;
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {
                ////获取控件的Tag属性值，并分割后存储字符串数组
                SetDouble(this);
                SetDouble(con);
                //if (con.Tag != null && ((deep == 1 && con is MoveOpControl) || deep != 1))
                if (con.Tag != null && (con is Scalable))
                {
                    if (con.Name == "MoveOpControl")
                    {
                        moc = (MoveOpControl)con;
                    }
                    // 通过反射调用方法
                    object obj = Activator.CreateInstance(con.GetType());//创建一个obj对象
                    MethodInfo mi = con.GetType().GetMethod("SetControlsBySize");
                    mi.Invoke(obj, new Object[] { newx, newy, con});//调用方法
                }
            }
            deep -= 1;
        }
        #endregion

        #region 画布中鼠标拖动的事件
        public int startX;
        public int startY;
        public int nowX;
        public int nowY;

        private void SetControlByDelta(float dx, float dy, Control cons)
        {
            deep += 1;
            // 遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {
                ////获取控件的Tag属性值，并分割后存储字符串数组
                SetDouble(this);
                SetDouble(con);
                if (con.Tag != null && ((deep == 1 && con is MoveOpControl)))
                {
                    if (con.Name == "MoveOpControl")
                    {
                        moc = (MoveOpControl)con;
                    }
                    Console.WriteLine(con.GetType().ToString());

                    string[] mytag = con.Tag.ToString().Split(new char[] { ';' });
                    //根据窗体缩放的比例确定控件的值
                    con.Left = Convert.ToInt32(System.Convert.ToSingle(mytag[2]) + dx);//左边距
                    con.Top = Convert.ToInt32(System.Convert.ToSingle(mytag[3]) + dy);//顶边距
                    if (con.Controls.Count > 0)
                    {
                        SetControlByDelta(dx, dy, con);
                    }
                }
            }
            deep -= 1;
            }
        #endregion

        #region 各种事件
        public void CanvasPanel_DragDrop(object sender, DragEventArgs e)
        {
            bool isData = false;
            string index = null;
            try
            {
                isData = (bool)e.Data.GetData("isData");
                index = e.Data.GetData("index").ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            // 首先根据数据`e`判断传入的是什么类型的button，分别创建不同的Control
            if (isData)
            {
                Console.WriteLine("创建一个`MoveDtControl`对象");
                MoveDtControl btn = new MoveDtControl(
                    index,
                    sizeLevel,
                    e.Data.GetData("Text").ToString(),
                    this.PointToClient(new Point(e.X - 300, e.Y - 100))
                );
                Controls.Add(btn);
                ((MainForm)(this.Parent)).naviViewControl.AddControl(btn);
                ((MainForm)(this.Parent)).naviViewControl.UpdateNaviView();
            }
            else
            {
                MoveOpControl btn = new MoveOpControl(
                    sizeLevel,
                    e.Data.GetData("Text").ToString(),
                    this.PointToClient(new Point(e.X - 300, e.Y - 100))
                );
                Controls.Add(btn);
                ((MainForm)(this.Parent)).naviViewControl.AddControl(btn);
                ((MainForm)(this.Parent)).naviViewControl.UpdateNaviView();
            }

        }

        public void CanvasPanel_MouseDown(object sender, MouseEventArgs e)
        {
            MouseIsDown = true;
            basepoint = e.Location;
            ((MainForm)(this.Parent)).blankButton.Focus();
            if (e.Button == MouseButtons.Left)
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
                //实例化一个和窗口一样大的位图
                i = new Bitmap(this.Width, this.Height);
                //创建位图的gdi对象
                g = Graphics.FromImage(i);
                //创建画笔
                p = new Pen(Color.Gray, 0.0001f);
                //指定线条的样式为划线段
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                //根据当前位置画图，使用math的abs()方法求绝对值
                if (e.X < basepoint.X && e.Y < basepoint.Y)
                    g.DrawRectangle(p, e.X, e.Y, System.Math.Abs(e.X - basepoint.X), System.Math.Abs(e.Y - basepoint.Y));
                else if (e.X > basepoint.X && e.Y < basepoint.Y)
                    g.DrawRectangle(p, basepoint.X, e.Y, System.Math.Abs(e.X - basepoint.X), System.Math.Abs(e.Y - basepoint.Y));
                else if (e.X < basepoint.X && e.Y > basepoint.Y)
                    g.DrawRectangle(p, e.X, basepoint.Y, System.Math.Abs(e.X - basepoint.X), System.Math.Abs(e.Y - basepoint.Y));
                else
                    g.DrawRectangle(p, basepoint.X, basepoint.Y, System.Math.Abs(e.X - basepoint.X), System.Math.Abs(e.Y - basepoint.Y));

                //将位图贴到窗口上
                BackgroundImage = i;
                //释放gid和pen资源
                g.Dispose();
                p.Dispose();
            }
            else if (e.Button == MouseButtons.Left && ((MainForm)(this.Parent)).flowControl.isClick)
            {
                ((CanvasPanel)(this.Parent)).nowX = e.X;
                ((CanvasPanel)(this.Parent)).nowY = e.Y;
                ((CanvasPanel)(this.Parent)).ChangLoc(nowX - startX, nowY - startY);
                ((CanvasPanel)(this.Parent)).startX = e.X;
                ((CanvasPanel)(this.Parent)).startY = e.Y;
                Console.WriteLine("After, X = " + startX.ToString() + ", Y = " + startY.ToString());
            }
        }

        public void CanvasPanel_MouseUp(object sender, MouseEventArgs e)
        {
            i = new Bitmap(this.Width, this.Height);
            g = Graphics.FromImage(i);
            g.Clear(Color.Transparent);
            BackgroundImage = i;
            g.Dispose();

            //标志位置低
            MouseIsDown = false;
        }

        public void CanvasPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        #endregion
    }
}
