﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace C2.Controls
{
    class ThumbItem
    {
        Point _Location;
        Size _Size;
        Color _BackColor;
        Color _ForeColor;
        string _Text;
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
            Business,
            Model
        }
        public ModelTypes Types { get; set; }

        public ThumbItem(string text, Image image, ModelTypes types)
        {
            Text = text;
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

        protected virtual bool OnPaint(ThumbViewPaintEventArgs e)
        {
            e.PaintBackground();

            // text
            //var textHeight = e.Font.Height + 6;
            //var rectText = new Rectangle(Left, Bottom - textHeight, Width, textHeight);
            //if (!string.IsNullOrEmpty(Text))
            //{
            //    var sf = PaintHelper.SFCenter;
            //    sf.Trimming = StringTrimming.EllipsisCharacter;
            //    sf.FormatFlags |= StringFormatFlags.NoWrap;

            //    e.Graphics.DrawString(Text,
            //        e.Font,
            //        Selected ? new SolidBrush(e.View.ActiveCellForeColor) : new SolidBrush(e.View.CellForeColor),
            //        rectText,
            //        sf);
            //}

            // image
            if (Image != null)
            {
                var rectImg = new Rectangle(Left, Top, Width, Height  - 4);
                // modified by DK: 小白的缩略图需要放大。
                // 图标大小修改，需要改一下PaintHelp方法
                PaintHelper.DrawImageInRange(e.Graphics, Image, rectImg, true);
            }

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
