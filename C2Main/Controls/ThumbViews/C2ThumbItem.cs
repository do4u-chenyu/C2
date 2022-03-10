using C2.Controls.OS;
using System;
using System.Drawing;

namespace C2.Controls
{
    class C2ThumbItem : ThumbItem
    {
        string _TextLine1;

        public string TextLine1
        {
            get { return _TextLine1; }
            set { _TextLine1 = value; }
        }

        public C2ThumbItem(string text, string line1, Image image, ModelTypes types) 
            : base(text, image, types)
        {
            TextLine1 = line1;
        }

        // 获取宽度缩放百分比 （**当获取的DPI的值一直是96的时候，可以通过用此方法获取的值转化为DPI，ScaleX * 96**）
        private int DpiScaleX()
        {
            IntPtr hdc = User32.GetDC(IntPtr.Zero);
            int horzres = Gdi32.GetDeviceCaps(hdc, DeviceCapsIndex.HORZRES);
            int desktopHorzres = Gdi32.GetDeviceCaps(hdc, DeviceCapsIndex.DESKTOPHORZRES);
            User32.ReleaseDC(IntPtr.Zero, hdc);           
            return 100 * desktopHorzres / horzres ;
        }

        private Tuple<float, float> ScaleFontSize()
        {
            int scaleX = DpiScaleX();

            float f1 = 10f;
            float f2 = 8f;

            if (Width > 200 && Width < 300 && scaleX == 150)
            {
                f1 = 12f;
                f2 = 9f;
            }
            else if (Width > 350 && Width < 500 && scaleX != 150)
            {
                f1 = 15f;
                f2 = 10f;
            }

            return new Tuple<float, float>(f1, f2);
        }

        protected override bool OnPaint(ThumbViewPaintEventArgs e)
        {
            base.OnPaint(e);
            // 根据窗口大小计算文字位置
            Tuple<float, float> scale = ScaleFontSize();
            
            Font fontTitle = new Font("微软雅黑", scale.Item1, FontStyle.Bold);
            Font fontContent = new Font("微软雅黑", scale.Item2);
            int x = Left + 15;
            int y = ((Top + Bottom) / 2 + Bottom) / 2;

            var height = fontTitle.Height - 6;
            var rect = new Rectangle(x, y - height * 2, Width - 15, Height - 40);
            e.Graphics.DrawString(Text, fontTitle, Brushes.Black, rect);

            height = fontContent.Height - 6;
            rect = new Rectangle(x, y - height, Width - 15, Height - 100);
            e.Graphics.DrawString(TextLine1, fontContent, Brushes.Black, rect);

            return true;
        }
    }
}
