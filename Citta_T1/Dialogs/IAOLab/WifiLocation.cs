using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C2.Controls;
using C2.IAOLab.BaseStation;
using C2.IAOLab.WifiMac;
using log4net.Util;
using C2.IAOLab.BankTool;

namespace C2.Dialogs.IAOLab
{
    public partial class WifiLocation : BaseDialog
    {
        private string formType;
        public WifiLocation()
        {
            InitializeComponent();
        }
        public void ReLayoutForm()
        {
            this.methodPanel.Visible = true;
            this.inputLabel.Visible = false;
            this.sixTransform.Visible = true;
            //tipLable.Location = new Point(30, 58);
            inputAndResult.Location = new Point(10, 104);
            inputAndResult.Height -= 20;
            inputAndResult.Width -= 10;
            //bd_wgs.Checked = false;
        }
        public string Tip { set { this.tipLable.Text = value; } }
        public string InputLable { set { this.inputLabel.Text = value; } }
        public Point InputLableLaction { set { this.inputLabel.Location = value; }get { return this.inputLabel.Location;  } }

        public string FormType { get { return this.formType; } set { this.formType = value; } }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void Search_Click(object sender, EventArgs e)
        {
            string[] inputArray = this.inputAndResult.Text.Split('\n');
            StringBuilder tmpResult = new StringBuilder();
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            switch (FormType)
            {
                case "APK":
                  
                    break;
                case "BaseStation":

                    foreach (string baseStation in inputArray)
                    {
                        if (!string.IsNullOrEmpty(baseStation))
                        {
                            tmpResult.Append(BaseStation.GetInstance().BaseStationLocate(baseStation));
                            inputAndResult.Text = tmpResult.ToString();
                        }
                    }
                    break;
                case "Wifi":
                    foreach (string mac in inputArray)
                    {
                        if (!string.IsNullOrEmpty(mac))
                        {
                            tmpResult.Append(WifiMac.GetInstance().MacLocate(mac));
                            inputAndResult.Text = tmpResult.ToString();
                        }
                    }
                    break;
                case "Card":
                    foreach (string bankCard in inputArray)
                    {
                        if (!string.IsNullOrEmpty(bankCard))
                        {                        
                            tmpResult.Append(BankTool.GetInstance().BankToolSearch(bankCard));
                            inputAndResult.Text = tmpResult.ToString();
                        }

                    }
                    break;
                case "Tude":
                    if (this.xyTtransform.Checked == true)
                    {
                        foreach (RadioButton choices in sixTransform.Controls)
                        {
                            if (choices.Checked == true)
                            {
                                string[] locationArry = this.inputAndResult.Text.Split('\n');
                                this.inputAndResult.Text = null;
                                StringBuilder transform = new StringBuilder();
                                string type = choices.Name;
                                foreach (string location in locationArry)
                                {
                                    string result = C2.IAOLab.Transform.GPSTransform.GetInstance().CoordinateConversion(location, type);
                                    string transformString = result;
                                    transform.Append(transformString);
                                    inputAndResult.Text = transform.ToString();
                                }
                            }
                        }
                    }
                    if (this.computeDistance.Checked == true)
                    {
                        string[] locationArry = this.inputAndResult.Text.Split('\n');
                        this.inputAndResult.Text = null;
                        StringBuilder transform = new StringBuilder();
                        string type = "distance";
                        foreach (string location in locationArry)
                        {
                            string result = C2.IAOLab.Transform.GPSTransform.GetInstance().CoordinateConversion(location, type);
                            string transformString = result;
                            transform.Append(transformString);
                            inputAndResult.Text = transform.ToString();

                        }
                    }
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
            this.inputAndResult.Text = null;
            Close();
        }

        private void tipLable_Click(object sender, EventArgs e)
        {

        }

        private void methodPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ComputeDistance_MouseClick(object sender, MouseEventArgs e)
        {
            EnableChange(false);
        }

        private void XYTtransform_MouseClick(object sender, MouseEventArgs e)
        {
            EnableChange(true);
        }
        private void EnableChange(bool enable)
        {
            foreach (Control choices in sixTransform.Controls)
            {
                if (choices is RadioButton)
                {
                    choices.Enabled = enable;
                }
            }
        }

        private void WifiLocation_Load(object sender, EventArgs e)
        {

        }
    }
}
