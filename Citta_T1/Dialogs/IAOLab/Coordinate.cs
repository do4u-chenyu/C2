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
            this.tabPage1.Text = "IP转换";
            this.tabPage2.Text = "时间转换";
            this.tabPage1.Controls.Remove(this.sixTransform);

            AddRadioButton("IP转整形", "整形转IP", tabPage1);
            AddRadioButton("真实时间转绝对秒", "绝对秒转真实时间", tabPage2);
            

            this.inputAndResult.Location = new Point(
                 this.inputAndResult.Location.X,
                 this.inputAndResult.Location.Y - 30
                );
            this.inputAndResult1.Location = this.inputAndResult.Location;
            this.tip1.Location = this.tip0.Location;
            this.inputAndResult.Height += 35;
            this.inputAndResult1.Height = this.inputAndResult.Height;
            this.inputAndResult1.Width = this.inputAndResult.Width;

        }


        private void AddRadioButton(string radio0Name, string radio1Name,TabPage page)
        {
            RadioButton realToAbs = new RadioButton()
            {
                Location = new Point(7, 60),
                Text = radio0Name,
                Width = 170,
            };
            realToAbs.Checked = true;
            RadioButton absToReal = new RadioButton()
            {
                Location = new Point(17 + realToAbs.Width, 60),
                Text = radio1Name,
                Width = 170
            };
            page.Controls.Add(realToAbs);
            page.Controls.Add(absToReal);
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
                inputAndResult1.Text = tmpResult.ToString();

            }
        }
        private void IPTransform(string[] inputArray, StringBuilder tmpResult)
        {

        }
        private void timeTransform(string[] inputArray, StringBuilder tmpResult)
        {

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
                    if (this.tabControl.SelectedIndex == 0)
                        IPTransform(this.inputAndResult.Text.Split('\n'), tmpResult);
                    else
                        timeTransform(this.inputAndResult.Text.Split('\n'), tmpResult);
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
