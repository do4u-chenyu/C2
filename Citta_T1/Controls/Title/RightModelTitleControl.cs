using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Controls.Title
{
    class RightModelTitleControl:UserControl
    {
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // RightModelTitleControl
            // 
            this.BackgroundImage = global::Citta_T1.Properties.Resources.u700;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.DoubleBuffered = true;
            this.Name = "RightModelTitleControl";
            this.Size = new System.Drawing.Size(26, 26);
            this.ResumeLayout(false);

        }
    }
}
