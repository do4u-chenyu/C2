using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public void changeSize(bool isLarger, float factor=1.3F)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲DoubleBuffer

            setTag(this);
            if (isLarger && sizeLevel <= 2)
            {
                setControlsBySize(factor, factor, this);
                sizeLevel += 1;
            }
            else if (!isLarger && sizeLevel > 0)
            {
                sizeLevel -= 1;
                setControlsBySize(1 / factor, 1 / factor, this);
            }
        }

        public void changLoc(float dx, float dy)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲DoubleBuffer

            setTag(this);
            setControlsByDelta(dx, dy, this);
        }

        #region 控件大小随窗体大小等比例缩放
        private int deep = 0;
        private MoveOpControl moc;
        private void setTag(Control cons)
        {
            deep += 1;
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ";" + con.Height + ";" + con.Left + ";" + con.Top + ";" + con.Font.Size;
                if (con.Controls.Count > 0 && ((deep == 1 && con is MoveOpControl) || deep != 1))
                {
                    Console.WriteLine("setTag:" + con.GetType().ToString());
                    setTag(con);
                }
            }
            deep -= 1;
        }
        //设置双缓冲区、解决闪屏问题
        public static void SetDouble(Control cc)
        {

            cc.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance |
                         System.Reflection.BindingFlags.NonPublic).SetValue(cc, true, null);

        }
        private void setControlsBySize(float newx, float newy, Control cons)
        {
            deep += 1;
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {
                ////获取控件的Tag属性值，并分割后存储字符串数组
                SetDouble(this);
                SetDouble(con);
                if (con.Tag != null && ((deep == 1 && con is MoveOpControl) || deep != 1))
                {
                    if (con.Name == "MoveOpControl")
                    {
                        moc = (MoveOpControl)con;
                    }
                    string[] mytag = con.Tag.ToString().Split(new char[] { ';' });
                    //根据窗体缩放的比例确定控件的值
                    con.Width = Convert.ToInt32(System.Convert.ToSingle(mytag[0]) * newx);//宽度
                    con.Height = Convert.ToInt32(System.Convert.ToSingle(mytag[1]) * newy);//高度
                    con.Left = Convert.ToInt32(System.Convert.ToSingle(mytag[2]) * newx);//左边距
                    con.Top = Convert.ToInt32(System.Convert.ToSingle(mytag[3]) * newy);//顶边距
                    Single currentSize = System.Convert.ToSingle(mytag[4]) * newy;//字体大小
                    // Note 字体变化会导致MoveOpControl的Width和Height也变化
                    con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    //Console.WriteLine(con.Name + "'Width变化之前: " + mytag[0] + ", 变化之后： " + con.Width.ToString());
                    //Console.WriteLine(con.Name + "'Height变化之前: " + mytag[1] + ", 变化之后： " + con.Height.ToString());
                    //Console.WriteLine(con.Name + "'Left变化之前: " + mytag[2] + ", 变化之后： " + con.Left.ToString());
                    //Console.WriteLine(con.Name + "'Top变化之前: " + mytag[3] + ", 变化之后： " + con.Top.ToString());
                    //Console.WriteLine(con.Name + "'Font变化之前: " + mytag[4] + ", 变化之后： " + currentSize.ToString());
                    if (con.Controls.Count > 0)
                    {
                        setControlsBySize(newx, newy, con);
                    }
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

        private void setControlsByDelta(float dx, float dy, Control cons)
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
                        setControlsByDelta(dx, dy, con);
                    }
                }
            }
            deep -= 1;
            }
        #endregion
    }
}
