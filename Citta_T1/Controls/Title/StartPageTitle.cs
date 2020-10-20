using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C2.Properties;
using C2.Utils;
using C2.Core;

namespace C2.Controls.Title
{
    public partial class StartPageTitle : UserControl
    {
        
        public StartPageTitle()
        {
            InitializeComponent();
            this.Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            var rectImg = new Rectangle(12, 5, 24, 15);
            Image img = SetImgcolor(global::C2.Properties.Resources.startPageTitle);
            PaintHelper.DrawImageInRange(e.Graphics, img, rectImg);
        }

        private Bitmap SetImgcolor(Bitmap img)
        {
            Color initColor = img.GetPixel(0, 0);
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    Color c = img.GetPixel(j, i);
                    if (c.Equals(initColor))
                        img.SetPixel(j, i, Color.FromArgb(0, 216, 216, 216));
                }
            }
            return img;
        }



        private void StartPageTitle_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
    }

}
