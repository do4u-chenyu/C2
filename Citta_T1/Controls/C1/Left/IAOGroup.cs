
using C2.Business.Model;
using C2.Core;
using C2.Model;
using C2.Utils;
using System;
using System.Windows.Forms;

namespace C2.Controls.Left
{
    public partial class IAOGroup : UserControl
    {
        public string DataSourceName { get; set; }


        public IAOGroup(string ffp, string dataSourceName)
        {
            InitializeComponent();
            txtButton.Name = ffp;
            DataSourceName = dataSourceName;
        }

        private void DataButton_Load(object sender, EventArgs e)
        {
        }

        private void TxtButton_MouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
