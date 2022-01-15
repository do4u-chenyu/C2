using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace C2.Business.CastleBravo.Intruder
{
    public partial class IntruderForm : Form
    {
        Boolean flag = true;
        public IntruderForm()
        {
            InitializeComponent();
        }

        //目标地址自动解析
        private void textBoxRequestMessage_TextChanged(object sender, EventArgs e)
        {
            var lines = tBReqMess.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string[] splitLine = tBReqMess.Text.Split(new char[] { '\n' });
            string lastLine = splitLine[splitLine.Length - 1].Trim();
           

            if (tBReqMess.Text.Contains("Host")) 
            {
                var address = Array.Find(lines, line => line.IndexOf("Host") != -1).Replace("Host:", "").Trim();
                if (address.Contains(":"))
                {
                    textBoxUrl.Text = address.Split(':')[0];
                    textBoxPort.Text = address.Substring(address.IndexOf(':') + 1);
                }
                else 
                {
                    textBoxUrl.Text = address;
                    textBoxPort.Text = "8080";
                }   
            }

            if (tBReqMess.Text.Contains("Referer"))
            {
                var referer = Array.Find(lines, line => line.IndexOf("Referer") != -1).Replace("Host:", "").Trim();
                TextBoxReferer.Text = referer.Substring(referer.IndexOf(':') + 1);
            }

            /*
            int ss = tBReqMess.Text.LastIndexOf("=");
            int sst = tBReqMess.Text.LastIndexOf("\n");
            int ttt = tBReqMess.Text.Length;
            */

            
            if (lastLine.Contains("=") && splitLine.Length >= 2 && flag &&
                (splitLine[splitLine.Length - 2] == string.Empty || splitLine[splitLine.Length - 2] == "\r"))
            {
                tBReqMessSetting(tBReqMess.Text.LastIndexOf("\n") + 1, tBReqMess.Text.LastIndexOf("=") - tBReqMess.Text.LastIndexOf("\n") - 1, Color.Blue);
                tBReqMessSetting(tBReqMess.Text.LastIndexOf("=") + 1, tBReqMess.Text.Length - tBReqMess.Text.LastIndexOf("=") - 1, Color.Red);
                tBReqMessSetting(tBReqMess.Text.LastIndexOf("") + 1, tBReqMess.Text.Length - tBReqMess.Text.LastIndexOf("=") - 1, Color.Black);
                //flag = false;
            }
               
        }

        public void tBReqMessSetting(int start, int end, Color color)
        {
            this.tBReqMess.SelectionStart = start;
            this.tBReqMess.SelectionLength = end;
            this.tBReqMess.SelectionColor = color;
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

        //清空报文
        private void packageTBCbutton_Click(object sender, System.EventArgs e)
        {
            TextBoxReferer.Text = string.Empty;
            tBReqMess.Text = string.Empty;
        }

        private void pasteTBCButton_Click(object sender, System.EventArgs e)
        {

        }

        private void sslCB_CheckedChanged(object sender, System.EventArgs e)
        {

        }

    }
}
