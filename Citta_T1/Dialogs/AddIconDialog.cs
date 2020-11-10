using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using C2.Controls;
using C2.Core;
using C2.Globalization;
using C2.Model.Widgets;

namespace C2.Dialogs
{
    class AddIconDialog : StandardDialog
    {
        PictureEditUI Editor;
        CheckBox CkbMore;
        bool _NeedMoreOptions;

        public AddIconDialog()
        {
            Editor = new PictureEditUI();
            SetMainControl(Editor);
            Editor.TabIndex = 0;
            Editor.Initialize(this);
            ActiveControl = Editor;

            CkbMore = new CheckBox();
            CkbMore.Text = "More Options...";
            CkbMore.AutoSize = true;
            CkbMore.CheckedChanged +=new EventHandler(CkbMore_CheckedChanged);
            Controls.Add(CkbMore);

            MinimumSize = Size;
            Size = new Size(300, 600);
            AfterInitialize();
        }

        [DefaultValue(false)]
        public bool NeedMoreOptions
        {
            get { return _NeedMoreOptions; }
            set { _NeedMoreOptions = value; }
        }

        [Browsable(false)]
        public PictureWidget.PictureDesign CurrentObject
        {
            get
            {
                if (Editor != null)
                    return Editor.CurrentObject;
                else
                    return null;
            }
        }

        protected override void OnCurrentLanguageChanged()
        {
            base.OnCurrentLanguageChanged();

            Text = Lang._("Select Icon");
            CkbMore.Text = Lang.GetTextWithEllipsis("More Options");
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);

            if (CkbMore != null)
                CkbMore.Location = new Point(12, BaseLinePosition - CkbMore.Height);
        }

        private void CkbMore_CheckedChanged(object sender, EventArgs e)
        {
            NeedMoreOptions = CkbMore.Checked;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AddIconDialog
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "AddIconDialog";
            this.ResumeLayout(false);

        }
    }
}
