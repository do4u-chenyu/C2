using System.Windows.Forms;
using Citta_T1.Controls.Move;

namespace Citta_T1.OperatorViews
{
    public partial class CustomOperatorView : Form
    {
        public CustomOperatorView(MoveOpControl moc)
        {
            InitializeComponent();
            
            if (moc.OperatorDimension() == 2)
            {
                this.dataSource1.Visible = true;
                this.outList1.Visible = true;
            }
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
