using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2Shell.Dialogs
{
    public partial class UpdateProgressBar : Form
    {
        private int counts;
        public UpdateProgressBar()
        {
            InitializeComponent();
        }
       
        public string Status { get => this.status.Text; set => this.status.Text = value; }
        public int SpeedValue { 
            get => this.counts;
            set
            { 
                this.counts = value;
                OnSpeedValueChange();
            }
        }
        private void OnSpeedValueChange()
        {
            this.speedValue.Text = this.counts + "%";
        }
    }
}
