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

namespace C2.Dialogs.IAOLab
{
    public partial class WifiLocation : BaseDialog
    {
        private string formType;
        public WifiLocation()
        {
            InitializeComponent();
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
                        string result = BaseStation.GetInstance().BaseStationLocate(baseStation);

                        string m_baseStationLocation = result;
                        baseStationLocation.Append(m_baseStationLocation);
                        inputAndResult.Text = baseStationLocation.ToString();
                    }
                    break;
                case "Wifi":
                    string[] macArry = this.inputAndResult.Text.Split('\n');
                    this.inputAndResult.Text = null;
                    StringBuilder macLocation = new StringBuilder();
                    foreach (string mac in macArry)
                    {
                        string result = WifiMac.GetInstance().MacLocate(mac);
                        
                        string m_macLocation = result;
                        macLocation.Append(m_macLocation);
                        inputAndResult.Text = macLocation.ToString();
                    }
                    break;
                case "Card":
                    
                    break;
                case "Tude":
                    
                    break;
                case "Ip":
                   
                    break;
            }
        }

        private void Cancle_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tipLable_Click(object sender, EventArgs e)
        {

        }
    }
}
