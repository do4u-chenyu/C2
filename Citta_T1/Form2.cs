using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            WebKit.WebKitBrowser webkit = new WebKit.WebKitBrowser();
            webkit.Dock = DockStyle.Fill;
            webkit.Navigate("http://ie.icoa.cn/");
            this.Controls.Add(webkit);
        }
    }
}
