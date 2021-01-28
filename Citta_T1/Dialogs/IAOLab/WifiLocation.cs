﻿using C2.Controls;
using C2.IAOLab.BankTool;
using C2.IAOLab.BaseStation;
using C2.IAOLab.WifiMac;
using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;

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
      

        public string FormType { get { return this.formType; } set { this.formType = value; } }



        private void Search_Click(object sender, EventArgs e)
        {
            string[] inputArray = this.inputAndResult.Text.Split('\n');
            StringBuilder tmpResult = new StringBuilder();
            this.Cursor = Cursors.WaitCursor;
            switch (FormType)
            {
               
                case "BaseStation":
                    
                    progressBar1.Value = 0;
                    progressBar1.Maximum = inputArray.Length;
                    progressBar1.Minimum = 0;
                    foreach (string baseStation in inputArray)
                    {
                        progressBar1.Value += 1 ;
                        if (!string.IsNullOrEmpty(baseStation) && progressBar1.Value < 1001 && string.IsNullOrEmpty(baseStation.Remove('\t').Remove(' ')))
                        {
                            tmpResult.Append(BaseStation.GetInstance().BaseStationLocate(baseStation.Split('\t')[0]));
                            inputAndResult.Text = tmpResult.ToString();
                        }
                    }
                    
                    break;
                case "Wifi":
                    
                    progressBar1.Value = 0;
                    progressBar1.Maximum = inputArray.Length;
                    progressBar1.Minimum = 0;
                    foreach (string mac in inputArray)
                    {
                        progressBar1.Value += 1;
                        if (!string.IsNullOrEmpty(mac) && progressBar1.Value < 1001 && string.IsNullOrEmpty(mac.Remove('\t').Remove(' ')))
                        {
                            tmpResult.Append(WifiMac.GetInstance().MacLocate(mac.Split('\t')[0]));
                            inputAndResult.Text = tmpResult.ToString();
                        }
                        
                    }
                    
                    break;
                case "Card":
                    
                    progressBar1.Value = 0;
                    progressBar1.Maximum = inputArray.Length;
                    progressBar1.Minimum = 0;
                    foreach (string bankCard in inputArray)
                    {
                        progressBar1.Value += 1;
                        if (!string.IsNullOrEmpty(bankCard) && progressBar1.Value < 1001 && string.IsNullOrEmpty(bankCard.Remove('\t').Remove(' ')))
                        {
                           
                            if (progressBar1.Value % 25 == 0 )
                            {
                                Thread.Sleep(800);
                                
                            }
                            tmpResult.Append(BankTool.GetInstance().BankToolSearch(bankCard.Split('\t')[0]));
                            inputAndResult.Text = tmpResult.ToString();
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
            progressBar1.Value = 0;
            Close();
        }

        private void WifiLocation_FormClosed(object sender, FormClosedEventArgs e)
        {
            progressBar1.Value = 0;
            this.inputAndResult.Clear();
        }
    }
}
