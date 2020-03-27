using Citta_T1.Business.Option;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{
    public partial class DifferOperatorView : Form
    {
        private OperatorOption operatorOption;
        public DifferOperatorView(OperatorOption option)
        {
            InitializeComponent();
            this.operatorOption = option;
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        public void OptionReady(OperatorOption operatorOption)
        {
            this.operatorOption = operatorOption;
            //this.operatorOption.SetOption("dataInfor", this.DataInforBox.Text);
            //this.operatorOption.SetOption("max", this.MaxValueBox.Text);
            //this.operatorOption.SetOption("outField", "");
        }
        public void SetOption(OperatorOption operatorOption)
        {
            //this.DataInforBox.Text = operatorOption.GetOption("dataInfor");
            //this.MaxValueBox.Text = operatorOption.GetOption("max");
        }
    }
}
