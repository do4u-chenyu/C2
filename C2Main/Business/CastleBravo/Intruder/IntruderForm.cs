﻿using System;
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
            var lines = textBoxRequestMessage.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var address = Array.Find(lines, line => line.IndexOf("Host") != -1).Replace("Host:", "").Trim();
            var referer = Array.Find(lines, line => line.IndexOf("Referer") != -1).Replace("Host:", "").Trim();
            if (textBoxRequestMessage.Text.Contains("Host")) 
            {
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
            if (textBoxRequestMessage.Text.Contains("Referer"))
                TextBoxReferer.Text = referer;
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
