using C2.Utils;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.Binary
{
    public partial class BinaryMainForm : Form
    {
        public BinaryMainForm()
        {
            InitializeComponent();
        }

        private void FileButton_Click(object sender, System.EventArgs e)
        {
            DialogResult ret = this.openFileDialog1.ShowDialog();
            if (ret != DialogResult.OK)
                return;

            this.ResultTB.Text = string.Empty;
            this.FileTB.Text = this.openFileDialog1.FileName;

            using (GuarderUtil.WaitCursor)
            {
                this.ResultTB.Text = new BinStrings().Strings(this.FileTB.Text);
            }
        }
    }
}
