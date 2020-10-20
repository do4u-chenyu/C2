using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using C2.Controls.OS;

namespace C2.Controls
{
    public class BaseWorkSpace : BaseControl
    {
        Form _ActivedMdiForm = null;
        List<Form> ActiveForms;

        public event System.EventHandler MdiFormActived;
        public event System.EventHandler MdiFormClosed;

        public BaseWorkSpace()
        {
            ActiveForms = new List<Form>();

            ResizeRedraw = true;
        }

        public Form this[int index]
        {
            get
            {
                if (index < 0 || index >= Controls.Count)
                    throw new ArgumentOutOfRangeException();
                return Controls[index] as Form;
            }
        }

        public Form ActivedMdiForm
        {
            get { return _ActivedMdiForm; }
            private set 
            {
            }
        }

        public void ShowMdiForm(Form form)
        {
        }

        public void ActiveMdiForm(Form form)
        {
            if (form == null || !Contains(form))
                return;

            ActivedMdiForm = form;
        }

        public void CloseMdiForm(Form form)
        {
            if (form == null || !Contains(form))
                return;

            if(!form.IsDisposed)
                form.Close();
        }

        public bool CloseAll()
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
