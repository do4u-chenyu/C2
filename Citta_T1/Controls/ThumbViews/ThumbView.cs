using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C2.Controls.DataCharts;
using C2.Core;

namespace C2.Controls
{
    class ThumbView : BaseControl
    {
        Color _CellBackColor;
        Color _CellForeColor;
        Color _ActiveCellBackColor;
        Color _ActiveCellForeColor;
        Size _Dimension;
        Size _ActualDimension;
        Size _CellSpace;
        Size _CellSize;
        Size _MinimumCellSize;
        Size _ActualCellSize;
        HitResult _HoverObject;
        HitResult _PressedObject;
        ToolTip toolTip1;
        string _ToolTipText;

        public event ThumbViewItemCancelEventHandler ItemClosing;
        public event ThumbViewItemEventHandler ItemClosed;

        public ThumbView()
        {
            _Dimension = new System.Drawing.Size(4, 2);
            _CellSize = new System.Drawing.Size(210, 180);
            _MinimumCellSize = new System.Drawing.Size(120, 100);
            _CellSpace = new Size(16, 30);
            ShowEmptyCells = true;
            SetPaintStyles();

            Items = new XList<ThumbItem>();
            Items.ItemAdded += Items_ItemAdded;
            Items.ItemRemoved += Items_ItemRemoved;
            initItems();
            this.BackColor = SystemColors.ControlLightLight;
            CellBackColor = SystemColors.ControlLightLight;
            CellForeColor = SystemColors.ControlText;
            ActiveCellBackColor = SystemColors.ControlLightLight;
            ActiveCellForeColor = SystemColors.HighlightText;

            toolTip1 = new ToolTip();
        }
        void initItems()
        {
            Items.Add(new ThumbItem("逻辑图", global::C2.Properties.Resources.logicMap));
            Items.Add(new ThumbItem("树状图", global::C2.Properties.Resources.tree));
            Items.Add(new ThumbItem("组织架构图", global::C2.Properties.Resources.organization));
            Items.Add(new ThumbItem("思维导图", global::C2.Properties.Resources.mindMap));
            Items.Add(new ThumbItem("", global::C2.Properties.Resources.dbnet));
            Items.Add(new ThumbItem("", global::C2.Properties.Resources.gunLuntan));
            Items.Add(new ThumbItem("", global::C2.Properties.Resources.yellowGroup));
            Items.Add(new ThumbItem("", global::C2.Properties.Resources.bank));
            Items.Add(new ThumbItem("", global::C2.Properties.Resources.modelTopLabel));
            Items.Add(new ThumbItem("", global::C2.Properties.Resources.BusinessViewLabel));
        }
        [DefaultValue(typeof(Size), "4, 2")]
        public Size Dimension
        {
            get { return _Dimension; }
            set 
            {
                if (_Dimension != value)
                {
                    _Dimension = value;
                    OnDimensionChanged();
                }
            }
        }

        Size ActualDimension
        {
            get { return _ActualDimension; }
            set { _ActualDimension = value; }
        }

        [DefaultValue(typeof(Size), "16, 50")]
        public Size CellSpace
        {
            get { return _CellSpace; }
            set 
            {
                if (_CellSpace != value)
                {
                    _CellSpace = value;
                    OnCellSpaceChanged();
                }
            }
        }

        [DefaultValue(typeof(Size), "210, 180")]
        public Size CellSize
        {
            get { return _CellSize; }
            set 
            {
                if (_CellSize != value)
                {
                    _CellSize = value;
                    OnCellSizeChanged();
                }
            }
        }

        [DefaultValue(typeof(Size), "120, 100")]
        public Size MinimumCellSize
        {
            get { return _MinimumCellSize; }
            set 
            {
                if (_MinimumCellSize != value)
                {
                    _MinimumCellSize = value;
                    OnMinimumCellSizeChanged();
                }
            }
        }

        Size ActualCellSize
        {
            get { return _ActualCellSize; }
            set { _ActualCellSize = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public XList<ThumbItem> Items { get; private set; }

        [DefaultValue(typeof(Color), "White")]
        public Color CellBackColor
        {
            get { return _CellBackColor; }
            set 
            {
                if (_CellBackColor != value)
                {
                    _CellBackColor = value;
                    OnCellBackColorChanged();
                }
            }
        }

        [DefaultValue(typeof(Color), "ControlText")]
        public Color CellForeColor
        {
            get { return _CellForeColor; }
            set
            {
                if (_CellForeColor != value)
                {
                    _CellForeColor = value;
                    OnCellForeColorChanged();
                }
            }
        }

        [DefaultValue(typeof(Color), "Highlight")]
        public Color ActiveCellBackColor
        {
            get { return _ActiveCellBackColor; }
            set 
            {
                if (_ActiveCellBackColor != value)
                {
                    _ActiveCellBackColor = value;
                    OnActiveCellBackColorChanged();
                }
            }
        }

        [DefaultValue(typeof(Color), "HighlightText")]
        public Color ActiveCellForeColor
        {
            get { return _ActiveCellForeColor; }
            set
            {
                if (_ActiveCellForeColor != value)
                {
                    _ActiveCellForeColor = value;
                    OnActiveCellForeColorChanged();
                }
            }
        }

        [DefaultValue(true)]
        public bool ShowEmptyCells { get; private set; }

        protected IEnumerable<ThumbItem> DisplayItems
        {
            get
            {
                var items = from item in Items
                            select item;
                var count = ActualDimension.Width * ActualDimension.Height;
                return items.Take(count);
            }
        }

        Point StartLocation { get; set; }

        HitResult HoverObject
        {
            get { return _HoverObject; }
            set 
            {
                if (_HoverObject != value)
                {
                    var old = _HoverObject;
                    _HoverObject = value;
                    OnHoverObjectChanged(old);
                }
            }
        }

        HitResult PressedObject
        {
            get { return _PressedObject; }
            set
            {
                if (_PressedObject != value)
                {
                    var old = _PressedObject;
                    _PressedObject = value;
                    OnPressedObjectChanged(old);
                }
            }
        }

        string ToolTipText
        {
            get { return _ToolTipText; }
            set
            {
                if (_ToolTipText != value)
                {
                    _ToolTipText = value;
                    toolTip1.SetToolTip(this, value);
                }
            }
        }

        protected virtual void OnCellBackColorChanged()
        {
            Invalidate();
        }

        protected virtual void OnCellForeColorChanged()
        {
            Invalidate();
        }

        protected virtual void OnActiveCellBackColorChanged()
        {
            Invalidate();
        }

        protected virtual void OnActiveCellForeColorChanged()
        {
            Invalidate();
        }

        protected virtual void OnCellSpaceChanged()
        {
            PerformLayout();
        }

        protected virtual void OnCellSizeChanged()
        {
            PerformLayout();
        }

        protected virtual void OnDimensionChanged()
        {
            //PerformLayout();
        }

        protected virtual void OnMinimumCellSizeChanged()
        {
            PerformLayout();
        }

        void OnHoverObjectChanged(HitResult old)
        {
            if (old.Item != null)
            {
                old.Item.Selected = false;
                Invalidate(old.Item);
            }

            if (HoverObject.Item != null)
            {
                HoverObject.Item.Selected = true;
                Invalidate(HoverObject.Item);
            }

            if (HoverObject.Item != null && !HoverObject.IsCloseButton)
                this.Cursor = Cursors.Hand;
            else
                this.Cursor = Cursors.Default;
        }

        void OnPressedObjectChanged(HitResult old)
        {
            if (old.Item != null)
            {
                old.Item.Pressed = false;
                Invalidate(old.Item);
            }

            if (PressedObject.Item != null)
            {
                PressedObject.Item.Pressed = true;
                Invalidate(PressedObject.Item);
            }
        }

        void Invalidate(ThumbItem item)
        {
            if (item != null)
            {
                Invalidate(item.Bounds);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //
            if (BackgroundImage != null)
            {
                e.Graphics.Clear(BackColor);
                var image = BackgroundImage;
                var client = ClientRectangle;
                var imageSize = image.Size;
                if (client.Width < image.Width || client.Height < image.Height)
                {
                    float zoom = Math.Min(client.Width / (float)image.Width , client.Height / (float)image.Height);
                    imageSize.Width = (int)Math.Floor(imageSize.Width * zoom);
                    imageSize.Height = (int)Math.Floor(imageSize.Height * zoom);                        
                }

                e.Graphics.DrawImage(image,
                    new Rectangle(client.Right - imageSize.Width, client.Bottom - imageSize.Height, imageSize.Width, imageSize.Height),
                    0, 0, image.Width, image.Height,
                    GraphicsUnit.Pixel);
            }
            else
            {
                InvokePaintBackground(this, e);
            }
            foreach (var item in Items)
            {
                PaintItem(e, item);
                //e.Graphics.DrawRectangle(new Pen(Color.Red), item.Bounds);
            }
            
            if (DesignMode)
            {
                DrawDesignBackground(e);
            }
        }

        internal void PaintCellBackground(ThumbViewPaintEventArgs e, ThumbItem item)
        {
            Color backColor;

            if (item == HoverObject.Item)
            {
                backColor = ActiveCellBackColor;
            }
            else
            {
                backColor = CellBackColor;
            }

            if (!backColor.IsEmpty)
            {
                e.Graphics.FillRectangle(new SolidBrush(backColor), item.Bounds);
            }
        }

        void PaintItem(PaintEventArgs e, ThumbItem item)
        {
            ThumbViewPaintEventArgs args = new ThumbViewPaintEventArgs(this, e.Graphics, item, Font);
            if (!item.NotifyPaint(args))
            {
                PaintCellBackground(args, item);

                Color foreColor;
                if (item == HoverObject.Item)
                {
                    foreColor = ActiveCellForeColor;
                }
                else
                {
                    foreColor = CellForeColor;
                }
                
                if (!string.IsNullOrEmpty(item.Text))
                {
                    Font font = new Font(UITheme.Default.DefaultFont.FontFamily, 16);
                    e.Graphics.DrawString(item.Text, font, new SolidBrush(foreColor), item.Bounds, PaintHelper.SFCenter);
                }
            }

            // close button
            if (item.CanClose)
            {
                PaintItemCloseButton(e, item);
            }
        }

        void PaintItemCloseButton(PaintEventArgs e, ThumbItem item)
        {
            if (item != HoverObject.Item)
                return;

            Image image = Properties.Resources.taskbar_close_button;
            UIStatusImage img = UIStatusImage.FromVertical(image,
                new UIControlStatus[] {
                    UIControlStatus.Normal,
                    UIControlStatus.Hover,
                    UIControlStatus.Selected,
                    UIControlStatus.InactivedHover,
                    UIControlStatus.Inactived,
                    UIControlStatus.Disabled});

            var rect = GetItemCloseButtonRectangle(item);
            var bs = UIControlStatus.Normal;
            if (PressedObject.Item == item && PressedObject.IsCloseButton)
                bs = UIControlStatus.Selected;
            else if (HoverObject.Item == item && HoverObject.IsCloseButton)
                bs = UIControlStatus.Hover;

            img.Draw(e.Graphics, bs, rect);
        }

        Rectangle GetItemCloseButtonRectangle(ThumbItem item)
        {
            if (item == null)
                throw new ArgumentNullException();

            return new Rectangle(item.Right - 16, item.Top, 16, 16);
        }

        void DrawDesignBackground(PaintEventArgs e)
        {
            Pen pen = new Pen(SystemColors.ControlDark);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);

            for (int c = 0; c < ActualDimension.Width; c++)
            {
                for (int r = 0; r < ActualDimension.Height; r++)
                {
                    var cellBounds = new Rectangle(
                        StartLocation.X + c * (ActualCellSize.Width + CellSpace.Width),
                        StartLocation.Y + r * (ActualCellSize.Height + CellSpace.Height),
                        ActualCellSize.Width,
                        ActualCellSize.Height);
                    e.Graphics.DrawRectangle(pen, cellBounds.X, cellBounds.Y, cellBounds.Width - 1, cellBounds.Height - 1);
                }
            }
        }

        void Items_ItemAdded(object sender, XListEventArgs<ThumbItem> e)
        {
            if (e.Item != null && e.Item.View != this)
                e.Item.View = this;
        }

        void Items_ItemRemoved(object sender, XListEventArgs<ThumbItem> e)
        {
            if (e.Item != null && e.Item.View == this)
                e.Item.View = null;

            PerformLayout();
            Invalidate();
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);

            var rect = ClientRectangle;

            // calculate dimension
            var dimension = Dimension;
            if (Dimension.Width > Dimension.Height)
            {
                var minSize = (MinimumCellSize.Width + CellSpace.Width) * Dimension.Width + CellSize.Width;
                if (rect.Height > rect.Width && Dimension.Width < minSize)
                    dimension = new Size(Dimension.Height, Dimension.Width);
            }
            else if (Dimension.Width < Dimension.Height)
            {
                var minSize = (MinimumCellSize.Height + CellSpace.Height) * Dimension.Height + CellSize.Height;
                if (rect.Width > rect.Height && Dimension.Height < minSize)
                    dimension = new Size(Dimension.Height, Dimension.Width);
            }
            ActualDimension = dimension;

            // calculate cell size
            var cellSize = new Size(
                (rect.Width - dimension.Width * CellSpace.Width) / dimension.Width,
                (rect.Height - dimension.Height * CellSpace.Height) / dimension.Height);
            ActualCellSize = cellSize;

            // calculate start location
            
            var startX = rect.X + CellSpace.Width/2;
            var startY = rect.Y + CellSpace.Height;
            StartLocation = new Point(startX, rect.Y);

            // calculate cell bounds
            int index = 0;
            foreach (var item in DisplayItems)
            {
                int x = index % dimension.Width;
                int y = index / dimension.Width;
                var cellBounds = new Rectangle(
                    startX + x * (cellSize.Width + CellSpace.Width),
                    startY + y * (cellSize.Height + CellSpace.Height),
                    cellSize.Width,
                    cellSize.Height);
                item.Bounds = cellBounds;
                index++;
                if (x == 0)
                    Items[Items.Count - y -1].Bounds = new Rectangle(cellBounds.X, 
                                                                     cellBounds.Y - CellSpace.Height, 
                                                                     cellBounds.Width/2, 
                                                                     CellSpace.Height );
                if (index == 8) 
                    break;
            }
        }

        protected virtual void OnItemClick(ThumbItem item)
        {
            if (item.Text.Equals(""))
                return;
            Global.GetMainForm().NewDocumentForm_Click(item.Text);
        }

        void OnItemClose(ThumbItem item)
        {
            if (item == null)
                throw new ArgumentNullException();

            if (Items.Contains(item))
            {
                var e = new ThumbViewItemCancelEventArgs(item);
                OnItemClosing(e);
                if (e.Cancel)
                    return;

                Items.Remove(item);

                var e2 = new ThumbViewItemEventArgs(item);
                OnItemClosed(e2);
            }
        }

        protected virtual void OnItemClosed(ThumbViewItemEventArgs e)
        {
            if (ItemClosed != null)
                ItemClosed(this, e);
        }

        protected virtual void OnItemClosing(ThumbViewItemCancelEventArgs e)
        {
            if (ItemClosing != null)
            {
                ItemClosing(this, e);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            HoverObject = HitTest(e.X, e.Y);
            if (HoverObject.Item != null)
                ToolTipText = HoverObject.Item.GetToolTipAt(e.X, e.Y);
            else
                ToolTipText = null;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            HoverObject = HitResult.Empty;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                PressedObject = HitTest(e.X, e.Y);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            var p = PressedObject;
            PressedObject = HitResult.Empty;
            if (!p.IsEmpty && p == HitTest(e.X, e.Y))
            {
                if (p.IsCloseButton)
                    OnItemClose(p.Item);
                else
                    OnItemClick(p.Item);
            }
        }

        HitResult HitTest(int x, int y)
        {
            foreach (var item in DisplayItems)
            {
                if (item.Bounds.Contains(x, y))
                {
                    var hr = new HitResult(x, y, item);
                    hr.IsCloseButton = GetItemCloseButtonRectangle(item).Contains(x, y);
                    return hr;
                }
            }

            return new HitResult();
        }

        struct HitResult
        {
            public ThumbItem Item;
            public int X;
            public int Y;
            public bool IsCloseButton;

            public HitResult(int x, int y, ThumbItem item)
            {
                X = x;
                Y = y;
                Item = item;
                IsCloseButton = false;
            }

            public static HitResult Empty
            {
                get { return new HitResult(); }
            }

            public bool IsEmpty
            {
                get
                {
                    return Item == null;
                }
            }

            public override bool Equals(object obj)
            {
                if (obj is HitResult)
                {
                    var hr = (HitResult)obj;
                    return Item == hr.Item
                        && IsCloseButton == hr.IsCloseButton;
                }

                return base.Equals(obj);
            }

            public override int GetHashCode()
            {
                return (Item != null ? Item.GetHashCode() : 0)
                    ^ (IsCloseButton ? 1 : 0);
            }

            public static bool operator==(HitResult hr1, HitResult hr2)
            {
                return hr1.Equals(hr2);
            }

            public static bool operator !=(HitResult hr1, HitResult hr2)
            {
                return !hr1.Equals(hr2);
            }
        }
    }
}
