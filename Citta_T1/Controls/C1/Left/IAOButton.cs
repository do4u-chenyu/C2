
using C2.Business.Model;
using C2.Core;
using C2.Model;
using C2.Utils;
using System;
using System.Windows.Forms;

namespace C2.Controls.Left
{
    public partial class IAOButton : UserControl
    {
        //public string DataSourceName { get; set; }
 
        public IAOButton(string ffp)
        {
            InitializeComponent();
            txtButton.Name = ffp;
            //DataSourceName = dataSourceName;
        }
        public string ModelTitle => this.txtButton.Text;
        private void IAOButton_Load(object sender, EventArgs e)
        {
        }

    }
}
