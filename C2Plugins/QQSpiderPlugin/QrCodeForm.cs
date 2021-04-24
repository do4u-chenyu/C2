using System.Drawing;
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
