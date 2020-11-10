using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using C2.Model;

namespace C2.Controls
{
    class ImageLibraryListBox : CellListBox<Picture>
    {
        public ImageLibraryListBox()
        {
            CellSize = new Size(32, 62);
            RefreshItems();
        }

        protected override void DrawCell(int index, Rectangle rect, PaintEventArgs e)
        {
            Picture picture = Items[index];
            if (picture == null || picture.Data == null)
                return;

            //PaintHelper.DrawImageInRange(e.Graphics, picture.Data, rect);

            Rectangle rect1 = new Rectangle(rect.Location, new Size(32, 46));
            Rectangle rect2 = new Rectangle(rect.Location, new Size(32, 16));
            rect2.Offset(0, 46);
            PaintHelper.DrawImageInRange(e.Graphics, picture.Data, rect1);
            PaintHelper.DrawStringDefault(e.Graphics, picture.SName, rect2);
        }

        public void RefreshItems()
        {
            SuspendLayout();

            int old = SelectedIndex;
            Clear();
            foreach (KeyValuePair<string, Picture> obj in MyIconLibrary.Share)
            {
                if (obj.Value.Data != null)
                    Items.Add(obj.Value);
            }

            if (old > -1 && old < Items.Count)
                SelectedIndex = old;

            ResumeLayout();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }
}
