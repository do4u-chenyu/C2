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
            //inputAndResult.Location = new Point(30, 104);
            //inputAndResult.Height += 46;
            //bd_wgs.Checked = false;
        }
        public string Tip { set { this.tipLable.Text = value; } }
        public string InputLable { set { this.inputLabel.Text = value; } }
        public Point InputLableLaction { set { this.inputLabel.Location = value; }get { return this.inputLabel.Location;  } }

        public string FormType { get { return this.formType; } set { this.formType = value; } }
        private void WifiLocation_Load(object sender, EventArgs e)
        {
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void Confirm_Click(object sender, EventArgs e)
        {
            switch (FormType)
            {
                case "APK":
                  
                    break;
                case "BaseStation":
                    string[] baseStationArry = this.inputAndResult.Text.Split('\n');
                    this.inputAndResult.Text = null;
                    StringBuilder baseStationLocation = new StringBuilder();
                    foreach (string baseStation in baseStationArry)
                    {
                        if (baseStation != "")
                        {
                            string result = BaseStation.GetInstance().BaseStationLocate(baseStation);
                            string baseStationLocationSring = result;
                            baseStationLocation.Append(baseStationLocationSring);
                            inputAndResult.Text = baseStationLocation.ToString();
                        }
                    }
                    break;
                case "Wifi":
                    string[] macArry = this.inputAndResult.Text.Split('\n');
                    this.inputAndResult.Text = null;
                    StringBuilder macLocation = new StringBuilder();
                    foreach (string mac in macArry)
                    {
                        if (mac != "")
                        {
                            string result = WifiMac.GetInstance().MacLocate(mac);
                            string macLocationString = result;
                            macLocation.Append(macLocationString);
                            inputAndResult.Text = macLocation.ToString();
                        }
                    }
                    break;
                case "Card":
                    string[] bankCardArry = this.inputAndResult.Text.Split('\n');
                    this.inputAndResult.Text = null;
                    StringBuilder bankTool = new StringBuilder();
                    foreach (string bankCard in bankCardArry)
                    {
                        if(bankCard != "") 
                        {
                        string result = BankTool.GetInstance().BankToolSearch(bankCard);
                        string bankToolString = result;
                        bankTool.Append(bankToolString);
                        inputAndResult.Text = bankTool.ToString();
                        }
                        
                    }
                    break;
                case "Tude":
                    
                    break;
                case "Ip":
                   
                    break;
            }
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
        { }
    }
}
