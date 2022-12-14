using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using C2.Controls.OS;
using C2.Controls;

namespace C2.WorkSpace
{
    public class MdiWorkSpace : BaseWorkSpace
    {
        public event System.EventHandler MdiFormActived;
        public event System.EventHandler MdiFormClosed;

        public override Form ActivedMdiForm
        {
            get { return _ActivedMdiForm; }
            protected set 
            {
                if (_ActivedMdiForm != value)
                {
                    _ActivedMdiForm = value;
                    OnActivedMdiFormChanged();
                }
            }
        }

        private Rectangle MdiRectangle
        {
            get
            {
                Rectangle rect = ClientRectangle;
                rect.X += Margin.Left;
                rect.Y += Margin.Top;
                rect.Width -= Margin.Horizontal;
                rect.Height -= Margin.Vertical;
                return rect;

                //return ClientRectangle;
            }
        }

        protected override Padding DefaultMargin
        {
            get
            {
                return Padding.Empty;
            }
        }

        public override void ShowMdiForm(Form form)
        {
            AddMidForm(form);
            ActiveMdiForm(form);
        }

        public void AddMidForm(Form form)
        {
            if (form == null)
                throw new ArgumentNullException();

            form.Bounds = MdiRectangle;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.WindowState = FormWindowState.Normal;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.ControlBox = false;
            form.Dock = DockStyle.Fill;
            form.Activated += new EventHandler(MdiForm_Activated);
            form.FormClosed += new FormClosedEventHandler(MdiForm_FormClosed);
            Controls.Add(form);
        }

        public override void ActiveMdiForm(Form form)
        {
            if (form == null || !Contains(form))
                return;

            ActivedMdiForm = form;
        }

        public override void CloseMdiForm(Form form)
        {
            if (form == null || !Contains(form))
                return;

            if(!form.IsDisposed)
                form.Close();
        }

        public override bool CloseAll()
        {
            for (int i = Controls.Count - 1; i >= 0; i--)
            {
                if (Controls[i] is Form)
                {
                    Form form = (Form)Controls[i];
                    form.Close();
                }
            }

            return Controls.Count == 0;
        }

        void ActiveNextForm()
        {
            for (int i = ActiveForms.Count - 1; i >= 0; i--)
            {
                Form form = ActiveForms[i];
                if (form == null || form.IsDisposed)
                {
                    ActiveForms.RemoveAt(i);
                    continue;
                }
                
                ActiveMdiForm(form);
                return;
            }

            ActivedMdiForm = null;
        }

        bool Contains(Form form)
        {
            foreach (Control ctl in Controls)
            {
                if (ctl == form)
                    return true;
            }

            return false;
        }

        void OnActivedMdiFormChanged()
        {
            Form form = ActivedMdiForm;

            foreach (Control ctl in this.Controls)
            {
                if (ctl != form)
                {
                    if (User32.IsWindowVisible(ctl.Handle))
                        User32.ShowWindow(ctl.Handle, ShowWindowFlags.SW_HIDE);
                }
                else
                {
                    if (!User32.IsWindowVisible(form.Handle))
                        User32.ShowWindow(form.Handle, ShowWindowFlags.SW_SHOWNA);
                }
            }

            if (form != null)
            {
                form.BringToFront();
                ResetActivedMdiFormBounds();

                if (ActiveForms.Contains(form))
                    ActiveForms.Remove(form);

                ActiveForms.Add(form);
                OnMdiFormActived(form);

                if (form.CanFocus)
                    form.Focus();
                else
                    ((Form)TopLevelControl).ActiveControl = form;
            }
        }

        void OnMdiFormActived(Form form)
        {
            MdiFormActived?.Invoke(form, EventArgs.Empty);
        }

        void OnMdiFormClosed(Form form)
        {
            if (ActiveForms.Contains(form))
                ActiveForms.Remove(form);
            if(Controls.Contains(form))
                Controls.Remove(form);

            MdiFormClosed?.Invoke(form, EventArgs.Empty);
        }

        //protected override void OnResize(EventArgs e)
        //{
        //    base.OnResize(e);

        //    ResetActivedMdiFormBounds();
        //}

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            ResetActivedMdiFormBounds();
        }

        void MdiForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is Form)
            {
                Form form = (Form)sender;
                OnMdiFormClosed(form);
                ActiveNextForm();
            }
        }

        void MdiForm_Activated(object sender, EventArgs e)
        {
            ActiveMdiForm(sender as Form);
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (e.Control == ActivedMdiForm)
            {
                if (e.Control.CanFocus)
                    e.Control.Focus();
                else
                    ((Form)TopLevelControl).ActiveControl = e.Control;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            e.Graphics.Clear(BackColor);

            if (DesignMode)
            {
                ControlPaint.DrawBorder(e.Graphics, ClientRectangle, SystemColors.ControlDark, ButtonBorderStyle.Dashed);
            }
        }

        protected override void OnMarginChanged(EventArgs e)
        {
            base.OnMarginChanged(e);
            
            OnActivedMdiFormChanged();
            Invalidate();
        }

        void ResetActivedMdiFormBounds()
        {
            Form form = ActivedMdiForm;
            if (form == null || form.IsDisposed)
                return;

            if (User32.IsWindow(form.Handle))
            {
                if (form.Bounds != MdiRectangle)
                {
                    form.Bounds = MdiRectangle;
                }
            }
        }
    }
}
