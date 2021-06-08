using C2.Core;
using C2.Dialogs;
using C2.Forms;
using C2.Utils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace C2.Controls
{
    public class TabItem
    {
        bool _CanClose = false;
        string _Text = string.Empty;
        bool _Enabled = true;
        bool _Visible = true;
        object _Tag = null;
        Rectangle _Bounds = Rectangle.Empty;
        Image _Icon = null;
        int _DisplayIndex = 0;
        Color? _BackColor;
        Color? _ForeColor;

        public ContextMenuStrip MenuStrip;

        public event System.EventHandler IconChanged;
        public event System.EventHandler TextChanged;
        public event System.EventHandler VisibleChanged;
        public event System.EventHandler CanCloseChanged;
        public event EventHandler Click;

        public TabItem()
        {
            Selectable = true;
        }

        public TabItem(string text)
        {
            this.Text = text;
            Selectable = true;
        }

        [DefaultValue("")]
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

        [DefaultValue(true)]
        public bool CanClose
        {
            get { return _CanClose; }
            set 
            {
                if (_CanClose != value)
                {
                    _CanClose = value;
                    OnCanCloseChanged();
                }
            }
        }

        [DefaultValue(true)]
        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }

        [DefaultValue(true)]
        public bool Visible
        {
            get { return _Visible; }
            set 
            {
                if (_Visible != value)
                {
                    _Visible = value;
                    OnVisibleChanged();
                }
            }
        }

        [DefaultValue(null)]
        public object Tag
        {
            get { return _Tag; }
            set { _Tag = value; }
        }

        [DefaultValue(null)]
        public Image Icon
        {
            get { return _Icon; }
            set 
            {
                if (_Icon != value)
                {
                    _Icon = value;
                    OnIconChanged();
                }
            }
        }

        public Padding Padding { get; set; }

        public bool Selectable { get; protected set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle Bounds
        {
            get { return _Bounds; }
            internal set { _Bounds = value; }
        }

        public Point Location
        {
            get { return _Bounds.Location; }
            set
            {
                if (_Bounds.Location != value)
                {
                    _Bounds = new Rectangle(value, _Bounds.Size);
                }
            }
        }

        public Size Size
        {
            get { return _Bounds.Size; }
            set
            {
                if (_Bounds.Size != value)
                {
                    _Bounds = new Rectangle(_Bounds.Location, value);
                }
            }
        }

        public int DisplayIndex
        {
            get { return _DisplayIndex; }
            internal set { _DisplayIndex = value; }
        }

        public TabBar Bar { get; internal set; }

        [DefaultValue(null)]
        public string ToolTipText { get; set; }

        public Color? BackColor
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

        public Color? ForeColor
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

        public bool IsHorizontal
        {
            get
            {
                if (Bar != null)
                    return Bar.IsHorizontal;
                else
                    return true;
            }
        }

        private void OnIconChanged()
        {
            if (IconChanged != null)
                IconChanged(this, EventArgs.Empty);
        }

        private void OnTextChanged()
        {
            if (TextChanged != null)
                TextChanged(this, EventArgs.Empty);
        }

        private void OnVisibleChanged()
        {
            if (VisibleChanged != null)
                VisibleChanged(this, EventArgs.Empty);
        }

        private void OnCanCloseChanged()
        {
            if (CanCloseChanged != null)
                CanCloseChanged(this, EventArgs.Empty);
        }

        protected virtual void OnBackColorChanged()
        {
            Invalidate();
        }

        protected virtual void OnForeColorChanged()
        {
            Invalidate();
        }

        public void Invalidate()
        {
            if (Bar != null)
                Bar.Invalidate(Bounds);
        }

        public override string ToString()
        {
            return this.Text;
        }

        public Rectangle GetCloseButtonRect()
        {
            Rectangle itemRect = Bounds;
            Size closeButtonSize = Bar.CloseButtonSize;
            Padding itemPadding = Bar.ItemPadding;
            if (IsHorizontal)
            {
                return new Rectangle(itemRect.Right - closeButtonSize.Width - itemPadding.Right,
                    itemRect.Top + (itemRect.Height - closeButtonSize.Height) / 2,
                    closeButtonSize.Width, closeButtonSize.Height);
            }
            else
            {
                return new Rectangle(itemRect.Left + (itemRect.Width - closeButtonSize.Width) / 2,
                    itemRect.Bottom - closeButtonSize.Height - itemPadding.Right,
                    closeButtonSize.Width, closeButtonSize.Height);
            }
        }

        internal void OnClick()
        {
            if (Click != null)
                Click(this, EventArgs.Empty);
        }


        public void ShowTabItemMenu(Point hitPoint)
        {
            if (this.Tag != null && (this.Tag is DocumentForm) && !string.IsNullOrEmpty(this.Text))
            {
                if (MenuStrip == null)
                    InitMenuStrip();

                MenuStrip.Show(Global.GetTaskBar(), hitPoint);
            }
        }

        private void InitMenuStrip()
        {
            MenuStrip = new ContextMenuStrip();

            ToolStripMenuItem ExportModel = new ToolStripMenuItem();
            ToolStripMenuItem Explorer = new ToolStripMenuItem();
            ToolStripMenuItem CopyFilePathToClipboard = new ToolStripMenuItem();

            ExportModel.Name = "ExportModel";
            ExportModel.Text = "导出";
            ExportModel.Click += ExportModel_Click;

            Explorer.Name = "Explorer";
            Explorer.Text = "打开所在文件夹";
            Explorer.Click += Explorer_Click;

            CopyFilePathToClipboard.Name = "CopyFilePathToClipboard";
            CopyFilePathToClipboard.Text = "复制文件路径到剪切板";
            CopyFilePathToClipboard.Click += CopyFilePathToClipboard_Click;

            MenuStrip.Items.AddRange(new ToolStripItem[] {
                    ExportModel,
                    new ToolStripSeparator(),
                    Explorer,
                    CopyFilePathToClipboard});

        }

        void ExportModel_Click(object sender, EventArgs e)
        {
            string fullFilePath = (this.Tag as BaseDocumentForm).Filename;
            if (!File.Exists(fullFilePath))
            {
                HelpUtil.ShowMessageBox("模型文档不存在，可能已被删除");
                return;
            }

            ZipDialog zipDialog = new ExportZipDialog(this.Text);
            if (zipDialog.ShowDialog() == DialogResult.OK)
            {
                string exportFullPath = zipDialog.ModelPath;
                string password = zipDialog.Password;
                if (C2.Business.Model.ExportModel.GetInstance().ExportC2Model(fullFilePath, exportFullPath, password))
                    HelpUtil.ShowMessageBox("模型导出成功,存储路径：" + exportFullPath);
                FileUtil.DeleteDirectory(Path.Combine(Global.TempDirectory));
            }
        }

        void Explorer_Click(object sender, EventArgs e)
        {
            FileUtil.ExploreDirectory((this.Tag as BaseDocumentForm).Filename);
        }

        void CopyFilePathToClipboard_Click(object sender, EventArgs e)
        {
            FileUtil.TryClipboardSetText((this.Tag as BaseDocumentForm).Filename);
        }
    }
}
