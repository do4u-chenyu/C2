using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1
{
    public partial class MoveOpControl : UserControl
    {
        private bool isMouseDown = false;
        private Point mouseOffset;
        public MoveOpControl()
        {
            InitializeComponent();
        }

        private void MoveOpControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                int left = (sender as MoveOpControl).Left + e.X - mouseOffset.X;
                int top = (sender as MoveOpControl).Top + e.Y - mouseOffset.Y;
                (sender as MoveOpControl).Location = new Point(left, top);
            }
        }

        private void MoveOpControl_MouseDown(object sender, MouseEventArgs e)
        {
            Console.Write("Control");
            if (e.Button == MouseButtons.Left)
            {
                mouseOffset.X = e.X;
                mouseOffset.Y = e.Y;
                isMouseDown = true;
            }
        }

        private void MoveOpControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }

        }


        private void OpButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                int left = (sender as Button).Parent.Left + e.X - mouseOffset.X;
                int top = (sender as Button).Parent.Top + e.Y - mouseOffset.Y;
                (sender as Button).Parent.Location = new Point(left, top);
            }

        }

        private void OpPicture_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                int left = (sender as PictureBox).Parent.Left + e.X - mouseOffset.X;
                int top = (sender as PictureBox).Parent.Top + e.Y - mouseOffset.Y;
                (sender as PictureBox).Parent.Location = new Point(left, top);
            }

        }
    }
}

