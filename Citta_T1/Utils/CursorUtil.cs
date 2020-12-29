using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Utils
{
    class CursorUtil
    {
        public class UsingCursor : IDisposable
        {
            private Cursor cursor;

            public UsingCursor(Cursor cursor)
            {
                this.cursor = Cursor.Current;
                Cursor.Current = cursor;
            }

            public void Dispose()
            {
                Cursor.Current = this.cursor;
            }
        }
    }
}
