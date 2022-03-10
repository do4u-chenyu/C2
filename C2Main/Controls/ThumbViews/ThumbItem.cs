using System;
using System.Drawing;
using System.Management;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace C2.Controls
{
    class ThumbItem
    {
        Point _Location;
        Size _Size;
        Color _BackColor;
        Color _ForeColor;
        string _Text;
        string _TextContent;
        Image _Image;
        

        public event EventHandler Click;

        public ThumbItem()
        {
        }

        public ThumbItem(string text)
        {
            Text = text;
        }
        public enum ModelTypes
        {
            Null,
            AnalysisNotes,
            TacticalManual,
            CastleBravo,
            Laboratory,
            WebsiteScout,
            Business, 
            APKMonitor,
            Knowledge,
            HIBU,
            Model
        }
        public ModelTypes Types { get; set; }

        public ThumbItem(string text, string textContent, Image image, ModelTypes types)
        {
            Text = text;
            TextContent = textContent;
            Image = image;
            Types = types;
        }

        public ThumbView View { get; internal set; }

        public Point Location
        {
            get { return _Location; }
            set 
            {
                if (_Location != value)
                {
                    _Location = value;
                    OnLocationChanged();
                }
            }
        }

        public Size Size
        {
            get { return _Size; }
            set 
            {
                if (_Size != value)
                {
                    _Size = value;
                    OnSizeChanged();
                }
            }
        }

        public Color BackColor
        {
            get { return _BackColor; }
            set
            {
                if (_BackColor != value)
                {
                    _BackColor = value;
                    OnBackColorChanged();
                }
            }
        }

        public Color ForeColor
        {
            get { return _ForeColor; }
            set
            {
                if (_ForeColor != value)
                {
                    _ForeColor = value;
                    OnForeColorChanged();
                }
            }
        }

        public string Text
        {
            get { return _Text; }
            set 
            {
                if (_Text != value)
                {
                    _Text = value;
                    OnTextChanged();
                }
            }
        }

        public string TextContent
        {
            get { return _TextContent; }
            set
            {
                if (_TextContent != value)
                {
                    _TextContent = value;
                    OnTextChanged();
                }
            }
        }

        public Image Image
        {
            get { return _Image; }
            set 
            {
                if (_Image != value)
                {
                    _Image = value;
                    OnImageChanged();
                }
            }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle(Location, Size); }
            set
            {
                Location = value.Location;
                Size = value.Size;
            }
        }

        public bool CanClose { get; set; }

        public int Left { get { return Location.X; } }

        public int Top { get { return Location.Y; } }

        public int Right { get { return Location.X + Size.Width; } }

        public int Bottom { get { return Location.Y + Size.Height; } }

        public int Width { get { return Size.Width; } }

        public int Height { get { return Size.Height; } }

        public int DisplayIndex { get; set; }

        public bool Selected { get; internal set; }

        public bool Pressed { get; internal set; }

        public object Tag { get; set; }

        protected virtual void OnImageChanged()
        {
        }

        protected virtual void OnTextChanged()
        {
        }

        protected virtual void OnBackColorChanged()
        {
        }

        protected virtual void OnForeColorChanged()
        {
        }

        protected virtual void OnLocationChanged()
        {
        }

        protected virtual void OnSizeChanged()
        {
        }

        internal bool NotifyPaint(ThumbViewPaintEventArgs e)
        {
            return OnPaint(e);
        }

        /*
        [DllImport("gdi32.dll", EntryPoint = "GetDeviceCaps", SetLastError = true)]
        public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        
        enum DeviceCap
        {
            VERTRES = 10,
            PHYSICALWIDTH = 110,
            SCALINGFACTORX = 114,
            DESKTOPVERTRES = 117,


            // http://pinvoke.net/default.aspx/gdi32/GetDeviceCaps.html
        }
        
        private static double GetScreenScalingFactor()
        {
            var g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            var physicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES);

            var screenScalingFactor =
                (double)physicalScreenHeight / Screen.PrimaryScreen.Bounds.Height;
            //SystemParameters.PrimaryScreenHeight;

            return screenScalingFactor;
        }
        */
        
        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr ptr);
        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(
        IntPtr hdc, // handle to DC
        int nIndex // index of capability
        );
        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);
        const int HORZRES = 8;
        const int DESKTOPHORZRES = 118;

        /// <summary>
        /// 获取宽度缩放百分比 （**当获取的DPI的值一直是96的时候，可以通过用此方法获取的值转化为DPI，ScaleX * 96**）
        /// </summary>
        public float DpiScale()
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            float ScaleX = (float)GetDeviceCaps(hdc, DESKTOPHORZRES) / (float)GetDeviceCaps(hdc, HORZRES);
            ReleaseDC(IntPtr.Zero, hdc);
            return ScaleX; 
        }

        public Tuple<float, float> autoFont()
        {
            //string scrWidth = Screen.PrimaryScreen.Bounds.Width.ToString();

            /*
            using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
            {
                float dpiX = graphics.DpiX;
                float dpiY = graphics.DpiY;
                double dpixRatio = dpiX / dpiY;
            }
            */



            //double ss = GetScreenScalingFactor();
            float scale = DpiScale();

            float fontSizeTitle;
            float fontSizeContent;
            if (Width > 350 && Width < 500 && scale != 1.5 )
            {
                fontSizeTitle = 15f;
                fontSizeContent = 10f;
            }
            else if (Width > 200 && Width < 300 && scale == 1.5)
            {
                fontSizeTitle = 12f;
                fontSizeContent = 9f;
            }
            else
            {
                fontSizeTitle = 10f;
                fontSizeContent = 8f;
            }
            float[] T = { fontSizeTitle, fontSizeContent };
            Tuple<float, float> tup = new Tuple<float, float>(T[0], T[1]);
            return tup;
        }

        protected virtual bool OnPaint(ThumbViewPaintEventArgs e)
        {
            e.PaintBackground();

            // image
            if (Image != null)
            {
                var rectImg = new Rectangle(Left, Top, Width, Height  - 4);
                // modified by DK: 小白的缩略图需要放大。
                // 图标大小修改，需要改一下PaintHelp方法
                //PaintHelper.DrawImageInRange(e.Graphics, Image, rectImg, true);
                e.Graphics.DrawImage(Image,rectImg);
            }


            // text
            Font fontTitle = new Font("微软雅黑", autoFont().Item1, FontStyle.Bold);
            Font fontContent = new Font("微软雅黑", autoFont().Item2);

            //int ss = Width;
            //int ii = Height;

            var textHeight = fontTitle.Height-6;
            var rectText = new Rectangle(Left+15, ((Top+Bottom)/2+Bottom)/2-textHeight*2, Width-15, Height-40);
            Brush whiteBrush = new SolidBrush(Color.Black);
            e.Graphics.DrawString(Text,fontTitle, whiteBrush,rectText);


            var textHeightContent = fontContent.Height-6;
            var rectTextContent = new Rectangle(Left+15, ((Top + Bottom) / 2 + Bottom) / 2 - textHeightContent, Width-15, Height-100);
            e.Graphics.DrawString(TextContent,fontContent, whiteBrush,rectTextContent);


            /*
            if (!string.IsNullOrEmpty(Text) && !string.IsNullOrEmpty(TextContent))
            {
                var sf = PaintHelper.SFCenter;
                sf.Trimming = StringTrimming.EllipsisCharacter;
                sf.FormatFlags |= StringFormatFlags.NoWrap;

                e.Graphics.DrawString(Text,
                    fontTitle,
                    Selected ? new SolidBrush(e.View.ActiveCellForeColor) : new SolidBrush(e.View.CellForeColor),
                    rectText,
                    sf);
                
                e.Graphics.DrawString(TextContent,
                    fontContent,
                    Selected ? new SolidBrush(e.View.ActiveCellForeColor) : new SolidBrush(e.View.CellForeColor),
                    rectTextContent,
                    sf);
                
            }
            */
            return true;
        }

        internal void NotifyClick()
        {
            OnClick();
        }

        protected virtual void OnClick()
        {
            if (Click != null)
                Click(this, EventArgs.Empty);
        }

        public override string ToString()
        {
            return Text;
        }

        public virtual string GetToolTipAt(int x, int y)
        {
            return null;
        }
    }
}
