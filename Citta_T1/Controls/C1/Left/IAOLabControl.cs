using System.Drawing;
using System.Windows.Forms;

namespace C2.Controls.Left
{
    public partial class IAOLabControl : UserControl
    {
        public IAOLabControl()
        {
            InitializeComponent();
            startPoint = new Point(ButtonLeftX, -ButtonBottomOffsetY);
            startPoint.Y -= 12;
        }
      
        private static readonly int ButtonLeftX = 18;
        private static readonly int ButtonBottomOffsetY = 23;
        private Point startPoint;

        public void GenIAOButton(string modelName)
        {
            IAOButton ib = new IAOButton(modelName);
            LayoutModelButtonLocation(ib); // 递增       

            this.IAOLabPanel.Controls.Add(ib);
        }

        private void LayoutModelButtonLocation(Control ct)
        {
            if (this.IAOLabPanel.Controls.Count > 0)
            {
                this.startPoint = this.IAOLabPanel.Controls[this.IAOLabPanel.Controls.Count - 1].Location;
            }
            this.startPoint.Y += ct.Height + ButtonBottomOffsetY;
            ct.Location = this.startPoint;
        }

        private void IAOLabControl_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.FromArgb(195, 195, 195), 1)
            {
                DashStyle = System.Drawing.Drawing2D.DashStyle.Custom,
                DashPattern = new float[] { 4, 4 }　 //画笔 1宽度.
            }; 
            g.DrawLine(p, 0, 30, 200, 30);//x1,y1,x2,y2
        }     
    }
}
