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
            this.inputLabel.Location = new Point(30, 14);
            RadioButton xy = new RadioButton() { Location = new Point(17, 14), Size = new Size(15, 15) };
            RadioButton distance = new RadioButton() { Location = new Point(210, 14), Size = new Size(15, 15) };
            RadioButton GcjBd = new RadioButton() { Location = new Point(17, 44), Size = new Size(15, 15) };
            RadioButton BdGcj = new RadioButton() { Location = new Point(145, 44), Size = new Size(15, 15) };
            RadioButton GcjWgs = new RadioButton() { Location = new Point(274, 44), Size = new Size(15, 15) };
            RadioButton WgsGcj = new RadioButton() { Location = new Point(17, 74), Size = new Size(15, 15) };
            RadioButton WgsBd = new RadioButton() { Location = new Point(145, 74), Size = new Size(15, 15) };
            RadioButton BdWgs = new RadioButton() { Location = new Point(274, 74), Size = new Size(15, 15) };
            Label distanceLabel = new Label { Location = new Point(223, 14),Size = new Size(200, 15) };
            Label GcjBdLabel = new Label { Location = new Point(30, 44) };
            Label BdGcjLabel = new Label { Location = new Point(158, 44) };
            Label GcjWgsLabel = new Label { Location = new Point(287, 44) };
            Label WgsGcjLabel = new Label { Location = new Point(30, 74) };
            Label WgsBdLabel = new Label { Location = new Point(158, 74) };
            Label BdWgsLabel = new Label { Location = new Point(287, 74) };
            distanceLabel.Text = "计算2个坐标间距离";
            GcjBdLabel.Text = "国标转百度";
            BdGcjLabel.Text = "百度转国标";
            GcjWgsLabel.Text = "国标转国际标";
            WgsGcjLabel.Text = "国际标转国标";
            WgsBdLabel.Text = "国际标转百度";
            BdWgsLabel.Text = "百度转国际标";

            this.tipLable.Location = new Point(this.tipLable.Location.X, this.tipLable.Location.Y + 60);
            this.inputAndResult.Location = new Point(this.inputAndResult.Location.X, this.inputAndResult.Location.Y + 60);
            this.Height += 60;
            //this.Width += 40;


            this.Controls.Add(xy);
            this.Controls.Add(distance);
            this.Controls.Add(GcjBd);
            this.Controls.Add(BdGcj);
            this.Controls.Add(GcjWgs);
            this.Controls.Add(WgsGcj);
            this.Controls.Add(WgsBd);
            this.Controls.Add(BdWgs);
            this.Controls.Add(GcjBdLabel);
            this.Controls.Add(BdGcjLabel);
            this.Controls.Add(GcjWgsLabel);
            this.Controls.Add(WgsGcjLabel);
            this.Controls.Add(WgsBdLabel);
            this.Controls.Add(BdWgsLabel);
            this.Controls.Add(distanceLabel);

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

                        string baseStationLocationSring = result;
                        baseStationLocation.Append(baseStationLocationSring);
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
                        
                        string macLocationString = result;
                        macLocation.Append(macLocationString);
                        inputAndResult.Text = macLocation.ToString();
                    }
                    break;
                case "Card":
                    string[] bankCardArry = this.inputAndResult.Text.Split('\n');
                    this.inputAndResult.Text = null;
                    StringBuilder bankTool = new StringBuilder();
                    foreach (string bankCard in bankCardArry)
                    {
                        string result = BankTool.GetInstance().BankToolSearch(bankCard);

                        string bankToolString = result;
                        bankTool.Append(bankToolString);
                        inputAndResult.Text = bankTool.ToString();
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
    }
}
