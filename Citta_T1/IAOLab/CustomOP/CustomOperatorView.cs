using System.Windows.Forms;
using Citta_T1.Controls.Move;

namespace Citta_T1.OperatorViews
{
    public partial class CustomOperatorView : Form
    {
        public CustomOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
        }

        private void ConfirmButton_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void CancelButton_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
