using System;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.Intruder
{
    public partial class IntruderForm : Form
    {
        public IntruderForm()
        {
            InitializeComponent();
        }

        //目标地址自动解析  Host && Ip
        private void textBoxRequestMessage_TextChanged(object sender, EventArgs e)
        {
            if (textBoxRequestMessage.Text.Contains("Host")) 
            {
                var lines = textBoxRequestMessage.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                var addressIpLine = Array.Find(lines, line => line.IndexOf("Host") != -1).Replace("Host:","").Trim();
                if (addressIpLine.Contains(":"))
                {
                    textBoxUrl.Text = addressIpLine.Split(':')[0];
                    textBoxPort.Text = addressIpLine.Substring(addressIpLine.IndexOf(':') + 1);
                }
                else 
                {
                    textBoxUrl.Text = addressIpLine;
                    textBoxPort.Text = "8080";
                }   
            }
        }

        private void startButton_Click(object sender, System.EventArgs e)
        {

        }

        private void suspendButton_Click(object sender, System.EventArgs e)
        {

        }

        private void stopButton_Click(object sender, System.EventArgs e)
        {

        }

        private void proxyTestButton_Click(object sender, System.EventArgs e)
        {

        }

        private void enableProxyCB_CheckedChanged(object sender, System.EventArgs e)
        {

        }

        private void markSbutton_Click(object sender, System.EventArgs e)
        {

        }

        private void markCbutton_Click(object sender, System.EventArgs e)
        {

        }

        private void packageTBCbutton_Click(object sender, System.EventArgs e)
        {

        }

        private void pasteTBCButton_Click(object sender, System.EventArgs e)
        {

        }

        private void sslCB_CheckedChanged(object sender, System.EventArgs e)
        {

        }

       
    }
}
