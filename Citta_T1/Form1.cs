using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebKit;

namespace C2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            webKitBrowser1.Navigate("http://ie.icoa.cn/");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //WebKit.WebKitBrowser browser = new WebKit.WebKitBrowser();
            //browser.Dock = DockStyle.Fill;
            //this.webBrowser1 = new WebKit.WebKitBrowser();
            //this.Controls.Add(browser);
            ////browser.Navigate("https://www.baidu.com/");
            //webBrowser1.Navigate("http://ie.icoa.cn/");
           
        }

    }
}
