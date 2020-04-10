using System;
using System.Drawing;
using System.Windows.Forms;
using Citta_T1.Business.Model;

namespace Citta_T1.Controls.Left
{
    public partial class OperatorControl : UserControl
    {
        private Point mouseOffset; //记录鼠标指针的坐标
        public OperatorControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void LeftPaneOp_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataObject dragDropData = new DataObject();
                dragDropData.SetData("Type", ElementType.Operator);
                dragDropData.SetData("Path", "");
                dragDropData.SetData("Text", (sender as Button).Text);
                leftPanelOpIntersect.DoDragDrop(dragDropData, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOffset.X = e.X;
                mouseOffset.Y = e.Y;
            }
        }

        protected override void OnGiveFeedback(GiveFeedbackEventArgs e) 
        {
            base.OnGiveFeedback(e);
            e.UseDefaultCursors = false;
            Bitmap map = new Bitmap(this.pictureBox1.Image);
            

            Bitmap myNewCursor = new Bitmap(map.Width * 2 , map.Height * 2 );
            Graphics g = Graphics.FromImage(myNewCursor);
            g.Clear(Color.FromArgb(0, 0, 0, 0));
            g.DrawImage(map, map.Width, map.Height, map.Width,map.Height);
            
            Cursor.Current = new Cursor(myNewCursor.GetHicon());

            g.Dispose();
            myNewCursor.Dispose();
        }

    }
}
