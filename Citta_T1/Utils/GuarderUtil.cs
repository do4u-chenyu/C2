using System;
using System.Windows.Forms;

namespace C2.Utils
{
    class GuarderUtil
    {
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
    }
}
