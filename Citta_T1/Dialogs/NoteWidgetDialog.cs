using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using C2.Configuration;
using C2.Controls;
using C2.Core;
using C2.Globalization;
using C2.Model;
using C2.Model.Widgets;

namespace C2.Dialogs
{
    class NoteWidgetDialog : StandardDialog, IWidgetEditDialog
    {
        public const string NoteWidgetDialogSize = "NoteWidgetDialog";
        HtmlEditor htmlEditor;
        //bool _IsEditMode;
        Widget _Widget;

        public NoteWidgetDialog()
        {
            InitializeComponent();

            AfterInitialize();
        }

        void InitializeComponent()
        {
            htmlEditor = new HtmlEditor();
            htmlEditor.ReadOnly = ReadOnly;

            SuspendLayout();
            Controls.Add(htmlEditor);
            MinimumSize = new Size(550, 300);
            if (Options.Current.Contains(NoteWidgetDialogSize))
                Size = Options.Current.GetValue(NoteWidgetDialogSize, Size);
            ResumeLayout(false);
        }

        public string Remark
        {
            get { return htmlEditor.Text; }
            set { htmlEditor.Text = value; }
        }

        [DefaultValue(null)]
        public Widget Widget
        {
            get { return _Widget; }
            set 
            {
                if (_Widget != value)
                {
                    _Widget = value;
                    OnWidgetChanged();
                }
            }
        }

        protected override bool ShowButtonArea
        {
            get
            {
                return true;
            }
        }

        void OnIsEditModeChanged()
        {
            RefreshViewStatus();
        }

        protected override void OnCurrentLanguageChanged()
        {
            base.OnCurrentLanguageChanged();

            Text = Lang._("Remark");
        }

        void RefreshViewStatus()
        {
            var readOnly = ReadOnly;

            htmlEditor.ReadOnly = readOnly;
        }

        protected override void OnReadOnlyChanged()
        {
            base.OnReadOnlyChanged();

            RefreshViewStatus();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                RefreshViewStatus();
            }
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);

            if (htmlEditor != null)
            {
                htmlEditor.Bounds = this.ControlsRectangle;
            }
        }

        protected override bool OnOKButtonClick()
        {
            if (Widget is NoteWidget)
            {
                var nw = (NoteWidget)Widget;
                nw.Text = Remark ?? string.Empty;
            }
            else if (string.IsNullOrEmpty(Remark))
            {
                this.ShowMessage("Please enter something here", MessageBoxIcon.Error);
                return false;
            }

            return base.OnOKButtonClick();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            Options.Current.SetValue(NoteWidgetDialogSize, Size);
        }

        void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        void OnWidgetChanged()
        {
            if (Widget != null)
                Remark = Widget.Remark;
            else
                Remark = null;
        }
    }
}
