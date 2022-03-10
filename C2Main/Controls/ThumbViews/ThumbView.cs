using C2.Core;
using C2.Forms;
using C2.Forms.Splash;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
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
            _Dimension = new Size(4, 2);
            _CellSize = new Size(210, 180);
            _MinimumCellSize = new Size(120, 100);
            _CellSpace = new Size(16, 30);
            ShowEmptyCells = true;
            SetPaintStyles();

            Items = new XList<ThumbItem>();
            Items.ItemAdded += Items_ItemAdded;
            Items.ItemRemoved += Items_ItemRemoved;
            InitItems();
            this.BackColor = SystemColors.ControlLightLight;
            CellBackColor = SystemColors.ControlLightLight;
            CellForeColor = SystemColors.ControlText;
            ActiveCellBackColor = SystemColors.ControlLightLight;
            ActiveCellForeColor = SystemColors.HighlightText;

            toolTip1 = new ToolTip();
        }
        void InitItems()
        {
            Items.Add(new ThumbItem("分析笔记", "承载分析师的分析思路、过程和结果",          global::C2.Properties.Resources.首页_分析笔记, ThumbItem.ModelTypes.AnalysisNotes));
            Items.Add(new ThumbItem("战术手册","流程化的战术模型和操作指导",          global::C2.Properties.Resources.首页_战术手册, ThumbItem.ModelTypes.TacticalManual));
            Items.Add(new ThumbItem("喝彩城堡", "模型闭环配套的安全工具",      global::C2.Properties.Resources.首页_喝彩城堡, ThumbItem.ModelTypes.CastleBravo));
            Items.Add(new ThumbItem("实验楼",  "常用分析小工具集合",       global::C2.Properties.Resources.首页_实验楼, ThumbItem.ModelTypes.Laboratory));
            Items.Add(new ThumbItem("网站侦察兵", "对网站进行爬取、分类并截图",    global::C2.Properties.Resources.首页_网站侦察兵, ThumbItem.ModelTypes.WebsiteScout));
            Items.Add(new ThumbItem("APK大眼睛","APK解析与信息提取",          global::C2.Properties.Resources.首页_APK检测站, ThumbItem.ModelTypes.APKMonitor));
            Items.Add(new ThumbItem("知识库", "各业务方向关键词库、线索库",      global::C2.Properties.Resources.首页_知识库, ThumbItem.ModelTypes.Knowledge));
            Items.Add(new ThumbItem("HIBU",   "提供23种AI能力",      global::C2.Properties.Resources.首页_HIBU, ThumbItem.ModelTypes.HIBU));
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
                e.Graphics.DrawRectangle(new Pen(Color.Red), item.Bounds);
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

        public string GetChromePath()
        {
            RegistryKey regKey = Registry.ClassesRoot;
            string path = string.Empty;
            List<string> chromeKeyList = new List<string>();
            foreach (var chrome in regKey.GetSubKeyNames())
            {
                if (chrome.ToUpper().Contains("CHROMEHTML"))
                {
                    chromeKeyList.Add(chrome);
                }
            }
            foreach (string chromeKey in chromeKeyList)
            {
                path = Registry.GetValue(@"HKEY_CLASSES_ROOT\" + chromeKey + @"\shell\open\command", null, null) as string;
                if (path != null)
                {
                    var split = path.Split('\"');
                    path = split.Length >= 2 ? split[1] : null;
                    if (File.Exists(path))
                        return path;
                }
            }
            return string.Empty;
        }

        protected virtual void OnItemClick(ThumbItem item)
        {
            String chromePath = GetChromePath();
            switch (item.Types)
            {
                //分析笔记
                case ThumbItem.ModelTypes.AnalysisNotes:
                    Global.GetMainForm().NewDocumentForm_Click(item.Text);
                    break;
                //战术手册
                case ThumbItem.ModelTypes.TacticalManual:
                    if (!Global.GetMainForm().manualControl.Visible || Global.GetMainForm().isLeftViewPanelMinimum)
                        Global.GetMainForm().SelectLeftPanel(Global.GetMainForm().manualButton, Global.GetMainForm().manualControl);
                    new ManualSplashForm().ShowDialog();
                    break;
                //喝彩城堡
                case ThumbItem.ModelTypes.CastleBravo:
                    if (!Global.GetMainForm().castleBravoControl.Visible || Global.GetMainForm().isLeftViewPanelMinimum)
                        Global.GetMainForm().ShowLeftPanel(Global.GetMainForm().castleBravoButton, Global.GetMainForm().castleBravoControl);
                    Global.GetCastleBravoControl().AddLabelClick();
                    break;
                //实验楼
                case ThumbItem.ModelTypes.Laboratory:
                    if (!Global.GetMainForm().iaoLabControl.Visible || Global.GetMainForm().isLeftViewPanelMinimum)  // 避免反复点击时的闪烁
                        Global.GetMainForm().SelectLeftPanel(Global.GetMainForm().iaoLabButton, Global.GetMainForm().iaoLabControl);
                    new IAOLabelSplashForm().ShowDialog();
                    break;
                //侦察兵
                case ThumbItem.ModelTypes.WebsiteScout:
                    if (!Global.GetMainForm().websiteFeatureDetectionControl.Visible || Global.GetMainForm().isLeftViewPanelMinimum)
                        Global.GetMainForm().ShowLeftPanel(Global.GetMainForm().detectionButton, Global.GetMainForm().websiteFeatureDetectionControl);
                    Global.GetWebsiteFeatureDetectionControl().AddLabelClick();
                    break;
                //APK监测站
                case ThumbItem.ModelTypes.APKMonitor:
                    if (!Global.GetMainForm().iaoLabControl.Visible || Global.GetMainForm().isLeftViewPanelMinimum)  // 避免反复点击时的闪烁
                        Global.GetMainForm().ShowLeftPanel(Global.GetMainForm().iaoLabButton, Global.GetMainForm().iaoLabControl);
                    if (!string.IsNullOrEmpty(chromePath))
                    {
                        System.Diagnostics.Process.Start(chromePath, "http://113.31.110.244:6663/ns/APPtest/home");
                    }
                    else
                        MessageBox.Show("未能找到chrome启动路径");
                    break;
                //知识库
                case ThumbItem.ModelTypes.Knowledge:
                    if (!Global.GetMainForm().iaoLabControl.Visible || Global.GetMainForm().isLeftViewPanelMinimum)  // 避免反复点击时的闪烁
                        Global.GetMainForm().ShowLeftPanel(Global.GetMainForm().iaoLabButton, Global.GetMainForm().iaoLabControl);
                    if (!string.IsNullOrEmpty(chromePath))
                    {
                        System.Diagnostics.Process.Start(chromePath, "15.73.3.241:19001/KnowledgeBase/");
                    }
                    else
                        MessageBox.Show("未能找到chrome启动路径");
                    break;
                //HIBU
                case ThumbItem.ModelTypes.HIBU:
                    if (!Global.GetMainForm().HIBUControl.Visible || Global.GetMainForm().isLeftViewPanelMinimum)  // 避免反复点击时的闪烁
                        Global.GetMainForm().SelectLeftPanel(Global.GetMainForm().HIBUButton, Global.GetMainForm().HIBUControl);
                    new HIBUSplashForm().ShowDialog();
                    break;
                case ThumbItem.ModelTypes.Model:
                    CanvasForm cf = Global.GetMainForm().SearchCanvasForm(Path.Combine(Global.MarketViewPath, item.Text));
                    if (cf != null)
                        Global.GetMainForm().SelectForm(cf);
                    else
                        Global.GetMainForm().LoadCanvasFormByXml(Global.MarketViewPath, item.Text);
                    break;
                default:
                    break;
            }
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

            if (e.Button == MouseButtons.Left)
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
