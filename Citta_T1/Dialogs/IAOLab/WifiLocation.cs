using C2.Controls;
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
            StringBuilder tmpResult = new StringBuilder();
            string[] inputArray = this.inputAndResult.Text.Split('\n');
            this.Cursor = Cursors.WaitCursor;
            switch (FormType)
            {
               
                case "BaseStation":
                    
                    progressBar1.Value = 0;
                    progressBar1.Maximum = GetRelLengthOfArry(inputArray);
                    progressBar1.Minimum = 0;
                    foreach (string baseStation in inputArray)
                    {
                        ShowResult(baseStation, "baseStation",tmpResult);
                    }
                    break;
                case "Wifi":
                    
                    progressBar1.Value = 0;
                    progressBar1.Maximum = GetRelLengthOfArry(inputArray);
                    progressBar1.Minimum = 0;
                    foreach (string mac in inputArray)
                    {
                        ShowResult(mac,"mac",tmpResult);
                    }
                    break;
                case "Card":
                    
                    progressBar1.Value = 0;
                    progressBar1.Maximum = GetRelLengthOfArry(inputArray);
                    progressBar1.Minimum = 0;
                    foreach (string bankCard in inputArray)
                    {
                        ShowResult(bankCard,"bankCard",tmpResult);
                    }
                    
                    break;             
                default:
                    break;
            }
            this.Cursor = Cursors.Arrow;
        }
        private void ShowResult(string input, string type, StringBuilder tmpResult)
        {

            if (!string.IsNullOrEmpty(input) && progressBar1.Value < 1001)
            {

                if (!string.IsNullOrEmpty(input.Split('\t')[0].Replace(" ", "")))
                {
                    if (progressBar1.Value % 50 == 0)
                    {
                        Thread.Sleep(500);

                    }
                    switch (type)
                    {
                        case "baseStation":
                            tmpResult.Append(BaseStation.GetInstance().BaseStationLocate(input.Split('\t')[0]));
                            break;
                        case "mac":
                            tmpResult.Append(WifiMac.GetInstance().MacLocate(input.Split('\t')[0]));
                            break;
                        case "bankCard":
                            tmpResult.Append(BankTool.GetInstance().BankToolSearch(input.Split('\t')[0]));
                            break;
                    }

                    inputAndResult.Text = tmpResult.ToString();
                    progressBar1.Value += 1;
                }


            }
        }
            
        
        private int GetRelLengthOfArry(string[] arry)
        {
            int relLength = 0;
            foreach(string i in arry)
            {
                
                if (!string.IsNullOrEmpty(i.Split('\t')[0].Replace(" ","")))
                    relLength++;
               
            }
            return relLength;
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
