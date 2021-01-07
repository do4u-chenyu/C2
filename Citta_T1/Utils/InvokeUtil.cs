using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Utils
{
    public static class InvokeUtil
    {
        public static void Invoke(Control ctl, Delegate method)
        {
            if (!ctl.IsHandleCreated)
                return;

            if (ctl.IsDisposed)
                return;

            if (ctl.InvokeRequired)
            {
                try
                {
                    ctl.Invoke(method);
                }
                catch (InvalidOperationException)
                {

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                return;
            }
        }

        public static void Invoke(Control ctl, Delegate method,string arg)
        {
            if (!ctl.IsHandleCreated)
                return;

            if (ctl.IsDisposed)
                return;

            if (ctl.InvokeRequired)
            {
                try
                {
                    ctl.Invoke(method, arg);
                }
                catch (InvalidOperationException)
                {

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                return;
            }
        }
    }
}
