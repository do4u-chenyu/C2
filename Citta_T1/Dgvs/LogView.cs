﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HZH_Controls;
using HZH_Controls.Controls;
using HZH_Controls.Forms;

namespace Citta_T1.Dgvs
{
    public partial class LogView : UserControl
    {
        private int maxLineCount = 10000;
        public LogView()
        {
            InitializeComponent();
        }

        public void LogUpdate(string log)
        {
            this.textBox1.AppendText(log + System.Environment.NewLine);


            if (this.textBox1.Lines.Length > maxLineCount)
            {
                string[] newlines = new string[maxLineCount];
                Array.Copy(this.textBox1.Lines, this.textBox1.Lines.Length - maxLineCount, newlines, 0, maxLineCount);
                this.textBox1.Lines = newlines;
            }


            
        }

        private void MenuItemClearAll_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
        }

        private void MenuItemSelectAll_Click(object sender, EventArgs e)
        {
            this.textBox1.SelectAll();
            string copy = this.textBox1.SelectedText;
            Clipboard.SetDataObject(copy);
        }
    }
}
