using C2.Model;
using System.Drawing;

namespace C2.Controls.C1.Left
{
    class TableListItem: ListBoxControlItem<DatabaseItem>
    {
        public DatabaseItem DatabaseItem;
        public Image Image;

        public TableListItem(DatabaseItem dbi):base()
        {
            this.DatabaseItem = dbi;
        }
    }
}
