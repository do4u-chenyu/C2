using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QQSpiderPlugin
{
    public partial class QrCodeForm : Form
    {
        public QrCodeForm(Image img)
        {
            InitializeComponent();
            SetPictureBox(img);
        }
        private void SetPictureBox(Image img)
        {
            this.qrPictureBox.Image = img;
        }
    }
}
