using Citta_T1.Business.Option;
using System;
using System.Windows.Forms;


namespace Citta_T1.OperatorViews
{
    public partial class CollideOperatorView : Form
    {
    
        private OperatorOption operatorOption;

        public CollideOperatorView(OperatorOption option)
        {
            InitializeComponent();
            this.operatorOption = option;
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            // 设置完成
            this.OptionReady();
            // if ()
            this.operatorOption.SetOption("status", "OK");
            // if ()
            // this.operatorOption.SetOption("status", "False");
            // 设置失败
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
            // if ()
            this.operatorOption.SetOption("status", "OK");
            // if ()
            // this.operatorOption.SetOption("status", "False");
            // 设置失败
        }
        private void OptionReady()
        {
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
