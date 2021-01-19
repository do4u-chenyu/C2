﻿using System;
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
using C2.IAOLab.Transform;
using System.Threading;

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
            this.Cursor = Cursors.WaitCursor;
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
                    int i = 0;
                    foreach (string bankCard in inputArray)
                    {
                        if (!string.IsNullOrEmpty(bankCard))
                        {
                            i++;                    
                            tmpResult.Append(BankTool.GetInstance().BankToolSearch(bankCard));
                            inputAndResult.Text = tmpResult.ToString();
                        }
                        if (i == 50)
                        {
                            Thread.Sleep(500);
                            i = 0;
                        }
                    }
                    break;             
                default:
                    break;
            }
            this.Cursor = Cursors.Arrow;
        }

        private void Cancle_Click(object sender, EventArgs e)
        {
            this.inputAndResult.Text = null;
            Close();
        }

        private void tipLable_Click(object sender, EventArgs e)
        {

        }

        private void WifiLocation_Load(object sender, EventArgs e)
        {

        }

    }
}
