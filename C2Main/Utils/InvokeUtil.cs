using System;
using System.Windows.Forms;

namespace C2.Utils
{
    public class InvokeUtil
    {
        public delegate void AsynCallback();
        public delegate void AsynCallback_S1(string val);
        public delegate void AsynCallback_B1(bool val);

        public static string Invoke(Control ctl, Delegate method)
        {
            string message = string.Empty;
            if (!ctl.IsHandleCreated)
                return message;

            if (ctl.IsDisposed)
                return message;

            if (ctl.InvokeRequired)
            {
                try
                {
                    ctl.Invoke(method);
                }
                catch (InvalidOperationException)
                {
                    return message;
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }

            return message;
        }

        public static string Invoke(Control ctl, Delegate method, string arg)
        {
            string message = string.Empty;
            if (!ctl.IsHandleCreated)
                return message;

            if (ctl.IsDisposed)
                return message;

            if (ctl.InvokeRequired)
            {
                try
                {
                    ctl.Invoke(method, arg);
                }
                catch (InvalidOperationException)
                {
                    return message;
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }

            return message;
        }
    }
}
