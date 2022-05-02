using System;
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
            protected readonly Control[] cts;
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
            public virtual void Dispose()
            {
                foreach (Control ct in this.cts)
                    ct.Enabled = true;
            }
        }

        public class ToolStripItemEnableGuarder : IDisposable
        {
            private ToolStripItem[] items;
            public ToolStripItemEnableGuarder(ToolStripItem[] items, bool value = false)
            {
                this.items = items;
                foreach (ToolStripItem item in this.items)
                    item.Enabled = value;
            }

            public ToolStripItemEnableGuarder(ToolStripItem item, bool value = false) 
                : this(new ToolStripItem[] { item }, value) { }
            public void Dispose()
            {
                foreach (ToolStripItem item in this.items)
                    item.Enabled = !item.Enabled;
            }
        }

        public class ToolStripItemTextGuarder : IDisposable
        {
            private readonly ToolStripItem item;
            private readonly string end;
            public ToolStripItemTextGuarder(ToolStripItem item, string begin, string end)
            {
                this.item = item;
                this.end = end;
                this.item.Text = begin;
            }

            public void Dispose()
            {
                this.item.Text = end;
            }

        }

        public class ControlTextGuarder : IDisposable
        {
            private readonly Control ct;
            private readonly string end;
            public ControlTextGuarder(Control ct, string begin, string end)
            {
                this.ct = ct;
                this.end = end;
                this.ct.Text = begin;
            }

            public void Dispose()
            {
                this.ct.Text = end;
            }

        }
    }
}
