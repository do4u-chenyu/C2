using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using C2.Model;
using C2.Utils;

namespace C2.Controls
{
    class ImageLibraryListBox : CellListBox<Picture>
    {
        public ImageLibraryListBox()
        {
            CellSize = new Size(55, 62);
            RefreshItems();
        }

        protected override void DrawCell(int index, Rectangle rect, PaintEventArgs e)
        {
            Picture picture = Items[index];
            if (picture == null || picture.Data == null)
                return;

            //PaintHelper.DrawImageInRange(e.Graphics, picture.Data, rect);

            Rectangle rect1 = new Rectangle(rect.Location, new Size(55, 46));
            Rectangle rect2 = new Rectangle(rect.Location, new Size(55, 16));
            rect2.Offset(0, 46);
            PaintHelper.DrawImageInRange(e.Graphics, picture.Data, rect1);
            PaintHelper.DrawStringDefault(e.Graphics, picture.SName, rect2);
        }

        public void RefreshItems()
        {
            SuspendLayout();
            int old = SelectedIndex;

            Clear();
            //按前缀数字逆序排序
            Items.AddRange(MyIconLibrary.Share.Values.OrderBy(s => ConvertUtil.TryParseInt(s.ID)));

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
