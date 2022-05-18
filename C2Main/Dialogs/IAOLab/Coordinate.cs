﻿using C2.Controls;
using C2.IAOLab.Transform;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Dialogs.IAOLab
{
    public partial class CoordinateConversion : BaseDialog
    {
        private string formType;
        public string FormType { get { return this.formType; } set { this.formType = value; } }
        public string Tab0Tip {  set { this.tip0.Text = value; } }
        public string Tib1Tip { set { this.tip1.Text = value; } }
        public string IPTip { set { this.label1.Text = value; } }
        public string TimeTip { set { this.label2.Text = value; } }
        public CoordinateConversion()
        {
            InitializeComponent();
            this.BackColor = Color.White;
          
        }


        public void ReLayoutForm()
        {
            // ip time转换窗体
            this.Text = "IP和时间转换";
            this.tabPage1.Text = "IP转换";
            this.tabPage2.Text = "时间转换";
            this.tabPage1.Controls.Remove(this.sixTransform);
            this.tip1.Location = this.tip0.Location;

            AddRadioButton("IP转整形", "整形转IP", tabPage1);
            AddRadioButton("真实时间转绝对秒", "绝对秒转真实时间", tabPage2);
            
            this.inputAndResult.Location = new Point(
                 this.inputAndResult.Location.X,
                 this.inputAndResult.Location.Y - 30
                );
            this.inputAndResult.Height += 35;
            this.inputAndResult1.Location = this.inputAndResult.Location;
            this.inputAndResult1.Size = this.inputAndResult.Size;


        }


        private void AddRadioButton(string radio0Name, string radio1Name,TabPage page)
        {
            RadioButton realToAbs = new RadioButton()
            {
                Location = new Point(7, 60),
                Text = radio0Name,
                Width = 170,
            };
            realToAbs.Checked = true;
            RadioButton absToReal = new RadioButton()
            {
                Location = new Point(17 + realToAbs.Width, 60),
                Text = radio1Name,
                Width = 170
            };
            page.Controls.Add(realToAbs);
            page.Controls.Add(absToReal);
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl.SelectedIndex == 0)
                this.inputAndResult.Focus();
            else
                this.inputAndResult1.Focus();
        }

        private void ComputeXYTransform(string[] inputArray, StringBuilder tmpResult)
        {
            foreach (RadioButton button in sixTransform.Controls)
            {
                if (!button.Checked)
                    continue;
                foreach (string locationorigin in inputArray)
                {
                    string location = locationorigin.Replace(" 输入格式有误", "").Split('\t')[0];
                    if (string.IsNullOrEmpty(location))
                        continue;
                    tmpResult.Append(GPSTransform.GetInstance(location).CoordinateConversion(button.Name));
                    inputAndResult.Text = tmpResult.ToString();
                }
                return;
            }
        }
        private void ComputeDistance(string[] inputArray, StringBuilder tmpResult)
        {
            foreach (string inputorigin in inputArray)
            {
                string input = inputorigin.Replace(" 输入格式有误", "").Split('\t')[0];
                if (!String.IsNullOrEmpty(input))
                {
                    tmpResult.Append(GPSTransform.GetInstance(input).ComputeDistance());
                    inputAndResult1.Text = tmpResult.ToString();
                }
               

            }
        }
        private void IPTransform(string[] inputArray, StringBuilder tmpResult)
        {
            foreach(Control button in tabPage3.Controls)
            {
                if (!(button is RadioButton && (button as RadioButton).Checked))
                    continue;
                foreach (string inputorigin in inputArray)
                {
                    string input = inputorigin.Replace(" 输入有误","").Split('\t')[0];
                    if (string.IsNullOrEmpty(input))
                        continue;
                    tmpResult.Append(TimeAndIPTransform.GetInstance(input).TimeIPTransform(button.Text));
                    richTextBox1.Text = tmpResult.ToString();
                }
                return;

            }
        }
        private void TimeTransform(string[] inputArray, StringBuilder tmpResult)
        {

            foreach (Control button in tabPage4.Controls)
            {
                if (!(button is RadioButton && (button as RadioButton).Checked))
                    continue;
                foreach (string inputorigin in inputArray)
                {
                    string input = inputorigin.Replace(" 输入有误", "").Split('\t')[0];
                    if (string.IsNullOrEmpty(input))
                        continue;
                    tmpResult.Append(TimeAndIPTransform.GetInstance(input).TimeIPTransform(button.Text));
                    richTextBox2.Text = tmpResult.ToString();
                }
                return;
            }
        }


        private void Search_Click(object sender, EventArgs e)
        {
            StringBuilder tmpResult = new StringBuilder();
            this.Cursor = Cursors.WaitCursor;
            if (tabControl.SelectedIndex == 0)
                ComputeXYTransform(this.inputAndResult.Text.Split('\n'), tmpResult);
            if (tabControl.SelectedIndex == 1)
                ComputeDistance(this.inputAndResult1.Text.Split('\n'), tmpResult);
            if (tabControl.SelectedIndex == 2)
                IPTransform(this.richTextBox1.Text.Split('\n'), tmpResult);
            if (tabControl.SelectedIndex == 3)
                TimeTransform(this.richTextBox2.Text.Split('\n'), tmpResult);
            this.Cursor = Cursors.Arrow;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {         
            Close();
        }

        private void Form_Shown(object sender, EventArgs e)
        {
            this.inputAndResult.Focus();
        }

        private void CoordinateFormClosed(object sender, FormClosedEventArgs e)
        {
            inputAndResult.Clear();
            inputAndResult1.Clear();
            richTextBox1.Clear();
            richTextBox2.Clear();
        }
    }
}
