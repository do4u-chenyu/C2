using C2.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Dialogs.WebsiteFeatureDetection
{
    partial class AddWFDTask : StandardDialog
    {
        public string TaskName { get => this.taskNameTextBox.Text; set => this.taskNameTextBox.Text = value; }
        public string FilePath { get => this.filePathTextBox.Text; set => this.filePathTextBox.Text = value; }
        public AddWFDTask()
        {
            InitializeComponent();
        }
    }
}
