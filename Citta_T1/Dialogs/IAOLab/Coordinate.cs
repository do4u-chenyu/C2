using C2.Controls;
using C2.IAOLab.Transform;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Dialogs.IAOLab
{
    public partial class coordinateConversion : BaseDialog
    {
        private string formType;
        public string FormType { get { return this.formType; } set { this.formType = value; } }
        public string Tab0Tip {  set { this.tip0.Text = value; } }
        public string Tib1Tip { set { this.tip1.Text = value; } }
        public coordinateConversion()
        {
            InitializeComponent();
            this.BackColor = Color.White;
          
        }


        public void ReLayoutForm()
        {
            // ip time转换窗体

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
  
        }
      
        private void ComputeXYTransform(string[] inputArray, StringBuilder tmpResult)
        {
            foreach (RadioButton button in sixTransform.Controls)
            {
                if (button.Checked)
                {
                    foreach (string location in inputArray)
                    {
                        tmpResult.Append(GPSTransform.GetInstance(location).CoordinateConversion(button.Name));
                        inputAndResult.Text = tmpResult.ToString();
                    }
                    return;
                }
            }
        }
        private void ComputeDistance(string[] inputArray, StringBuilder tmpResult)
        {
            foreach (string input in inputArray)
            {
                tmpResult.Append(GPSTransform.GetInstance(input).ComputeDistance());
                inputAndResult.Text = tmpResult.ToString();

            }
        }

        private void Search_Click(object sender, EventArgs e)
        {
            StringBuilder tmpResult = new StringBuilder();
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            switch (FormType)
            {

                case "Tude":
                    if (this.tabControl.SelectedIndex == 0)
                        ComputeXYTransform(this.inputAndResult.Text.Split('\n'), tmpResult);
                    else
                        ComputeDistance(this.inputAndResult1.Text.Split('\n'), tmpResult);

                    break;
                case "Ip":

                    break;
                default:
                    break;
            }
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
        }

        private void Cancle_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
