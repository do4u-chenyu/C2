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

            public void Dispose()
            {
                Cursor.Current = this.cursor;
            }
        }

        public class LayoutGuarder : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}
