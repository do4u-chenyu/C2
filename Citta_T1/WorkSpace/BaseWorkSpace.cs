using C2.Controls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace C2.WorkSpace
{
    public class BaseWorkSpace : BaseControl
    {
        protected Form _ActivedMdiForm = null;
        protected List<Form> ActiveForms;

        public BaseWorkSpace()
        {
            ActiveForms = new List<Form>();
            ResizeRedraw = true;
        }

        public virtual Form this[int index]
        {
            get
            {
                if (index < 0 || index >= Controls.Count)
                    throw new ArgumentOutOfRangeException();
                return Controls[index] as Form;
            }
        }

        public virtual Form ActivedMdiForm
        {
            get { return _ActivedMdiForm; }
            protected set
            {
                if (_ActivedMdiForm != value)
                {
                    _ActivedMdiForm = value;
                }
            }
        }

        public virtual void ShowMdiForm(Form form)
        {
        }

        public virtual void ActiveMdiForm(Form form)
        {
            if (form == null || !Contains(form))
                return;

            ActivedMdiForm = form;
        }

        public virtual void CloseMdiForm(Form form)
        {
            if (form == null || !Contains(form))
                return;

            if(!form.IsDisposed)
                form.Close();
        }

        public virtual bool CloseAll()
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
    }
}
