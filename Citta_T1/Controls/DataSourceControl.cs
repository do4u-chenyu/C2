using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.Controls
{
    public partial class DataSourceControl : UserControl
    {
        public DataSourceControl()
        {
            InitializeComponent();
            
        }

        private void LocalFrame_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ExternalData_Click(object sender, EventArgs e)
        {
            this.ExternalData.Font= new Font("微软雅黑", 12,FontStyle.Bold );
            this.LocalData.Font = new Font("微软雅黑", 12, FontStyle.Regular);
            this.ExternalFrame.Visible = true;
            this.LocalFrame.Visible = false;
        }

         private void LocalData_Click(object sender, EventArgs e)
         {
             this.LocalData.Font = new Font("微软雅黑", 12, FontStyle.Bold);
             this.ExternalData.Font = new Font("微软雅黑", 12, FontStyle.Regular);
             this.LocalFrame.Visible = true;
             this.ExternalFrame.Visible = false;
         }

    }
}
