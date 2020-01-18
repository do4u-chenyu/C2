using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Controls.Small
{
    public partial class DragLineControl : UserControl
    {
        private bool mouseDown;
        private Control bottomViewPanel;
        public DragLineControl()
        {
            InitializeComponent();
            this.mouseDown = false;
            this.bottomViewPanel = null;
        }

        private void DragLineControl_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            this.bottomViewPanel = Utils.ControlUtil.FindControlByName(Utils.ControlUtil.FindRootConrtol(this), "bottomViewPanel");

        }

        private void DragLineControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown && this.bottomViewPanel != null)
            {
                //System.Console.WriteLine(this.bottomViewPanel.Height.ToString());
                //System.Console.WriteLine(e.Y);
                this.bottomViewPanel.Height = this.bottomViewPanel.Height - e.Y;
            }
        }

        private void DragLineControl_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
    }
}
