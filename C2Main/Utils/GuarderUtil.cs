using System;
using System.Threading;
using System.Windows.Forms;

namespace C2.Utils
{
    class GuarderUtil
    {
        public static CursorGuarder WaitCursor { get => new CursorGuarder(); }
        public class CursorGuarder : IDisposable
        {
            private Cursor cursor;

            public CursorGuarder(Cursor cursor)
            {
                this.cursor = Cursor.Current;
                Cursor.Current = cursor;
            }

            public CursorGuarder()
            {
                this.cursor = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
            }

     

            public void Dispose()
            {
                Cursor.Current = this.cursor;
            }
        }

        public class LayoutGuarder : IDisposable
        {
            private Control control;
            public LayoutGuarder(Control ct)
            {
                control = ct;
                control.SuspendLayout();
            }
            public void Dispose()
            {
                control.ResumeLayout(true);
            }
        }

        public class ControlEnableGuarder : IDisposable
        {
            private readonly Control[] cts;
            public ControlEnableGuarder(Control ct)
            {
                this.cts = new Control[] { ct };
                ct.Enabled = false;
            }
            public ControlEnableGuarder(Control[] cts)
            {
                this.cts = cts;
                foreach (Control ct in this.cts)
                    ct.Enabled = false;
            }
            public void Dispose()
            {
                foreach (Control ct in this.cts)
                    ct.Enabled = true;
            }
        }

        public class ToolStripItemEnableGuarder : IDisposable
        {
            private ToolStripItem[] items;
            public ToolStripItemEnableGuarder(ToolStripItem[] items)
            {
                this.items = items;
                foreach (ToolStripItem item in this.items)
                    item.Enabled = false;
            }
            public void Dispose()
            {
                foreach (ToolStripItem item in this.items)
                    item.Enabled = true;
            }
        }
    }
}
