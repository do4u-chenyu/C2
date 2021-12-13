using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace C2.Controls
{
    class JSTabBarRenderer : NormalTabBarRenderer
    {
        Rectangle rectangle;
        public JSTabBarRenderer(TabBar bar)
            : base(bar)
        {
        }

        protected override void DrawItemBackground(TabItemPaintEventArgs e)
        {
            rectangle = e.Bounds;
            Color borderColor = Color.Empty;
            if (e.Selected)
            {
                // 把BaseLineSize加回去
                switch (e.Bar.Alignment)
                {
                    case TabAlignment.Left:
                        rectangle.Width += e.Bar.BaseLineSize;
                        break;
                    case TabAlignment.Top:
                        rectangle.Height += e.Bar.BaseLineSize;
                        break;
                    case TabAlignment.Right:
                        rectangle.X -= e.Bar.BaseLineSize;
                        rectangle.Width += e.Bar.BaseLineSize;
                        break;
                    case TabAlignment.Bottom:
                        rectangle.Y -= e.Bar.BaseLineSize;
                        rectangle.Height += e.Bar.BaseLineSize;
                        break;
                }

                Color backColor = e.Item.BackColor ?? e.Bar.SelectedItemBackColor;
                if (!backColor.IsEmpty)
                {
                    //LinearGradientBrush backBrush = new LinearGradientBrush(e.Bounds, PaintHelper.GetLightColor(backColor), backColor, 90.0f);
                    var backBrush = new SolidBrush(backColor);
                    e.Graphics.FillRectangle(backBrush, rectangle);
                }

                borderColor = e.Bar.BaseLineColor;
            }
            else
            {
                Color backColor = e.Item.BackColor ?? e.Bar.ItemBackColor;
                switch (e.Status)
                {
                    case UIControlStatus.Hover:
                        backColor = e.Bar.HoverItemBackColor;
                        break;
                }

                if (!backColor.IsEmpty)
                {
                    var backBrush = new SolidBrush(backColor);
                    e.Graphics.FillRectangle(backBrush, e.Item.Bounds);
                    //TabBar各矩形空间之间划线
                    //borderColor = PaintHelper.AdjustColorS(PaintHelper.GetDarkColor(backColor, 0.15), 10, 20);
                }
            }

            if (!borderColor.IsEmpty)
            {
                //TabItem之间矩形边界根据颜色划线
                DrawItemBorder(e, rectangle, borderColor);
            }
        }

        protected override void DrawItemBorder(TabItemPaintEventArgs e, Rectangle rect, Color color)
        {
            rect.Width--;
            rect.Height--;
            if (rect.Width <= 0 || rect.Height <= 0)
                return;
            Pen newPen = new Pen(Color.DodgerBlue, 6);
            e.Graphics.DrawLine(newPen, new Point(rectangle.Left, rectangle.Bottom), new Point(rectangle.Right, rectangle.Bottom));
        }
    }
}
