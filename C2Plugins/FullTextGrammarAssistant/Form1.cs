using C2.IAOLab.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FullTextGrammarAssistant
{
    public partial class Form1 : Form, IPlugin
    {
        public Form1()
        {
            InitializeComponent();
            foreach (CheckBox ck in panel1.Controls)
            {
                ck.CheckedChanged += checkBox1_CheckedChanged;
            }
            foreach (CheckBox ck in panel2.Controls)
            {
                ck.CheckedChanged += checkBox1_CheckedChanged;
            }
            foreach (CheckBox ck in panel3.Controls)
            {
                ck.CheckedChanged += checkBox48_CheckedChanged;
            }
            foreach (CheckBox ck in panel4.Controls)
            {
                ck.CheckedChanged += checkBox53_CheckedChanged;
            }
            foreach (CheckBox ck in panel5.Controls)
            {
                ck.CheckedChanged += checkBox50_CheckedChanged;
            }
        }

        public string GetPluginDescription()
        {
            return "全文语法助手";
        }

        public Image GetPluginImage()
        {
            return this.Icon.ToBitmap();
        }

        public string GetPluginName()
        {
            return "全文语法助手";
        }

        public string GetPluginVersion()
        {
            return "0.0.1";
        }

        public DialogResult ShowFormDialog()
        {
            return this.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel1.Visible = false;
        }

        private void checkBox1_Click_1(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                foreach (CheckBox ck in panel1.Controls)
                    ck.Checked = true;
                foreach (CheckBox ck in panel2.Controls)
                    ck.Checked = true;
            }
            else
            {
                foreach (CheckBox ck in panel1.Controls)
                    ck.Checked = false;
                foreach (CheckBox ck in panel2.Controls)
                    ck.Checked = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = sender as CheckBox;
            if (c.Checked == true)
            {
                foreach (CheckBox ch in panel1.Controls)
                {
                    if (ch.Checked == false)
                        return;
                }
                foreach (CheckBox ch in panel2.Controls)
                {
                    if (ch.Checked == false)
                        return;
                }
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }
        }

        private void checkBox37_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox48_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = sender as CheckBox;
            if (c.Checked == true)
            {
                foreach (CheckBox ch in panel3.Controls)
                {
                    if (ch.Checked == false)
                        return;
                }
                checkBox48.Checked = true;
            }
            else
            {
                checkBox48.Checked = false;
            }
        }

        private void checkBox48_Click(object sender, EventArgs e)
        {
            if (checkBox48.CheckState == CheckState.Checked)
            {
                foreach (CheckBox ck in panel3.Controls)
                    ck.Checked = true;
            }
            else
            {
                foreach (CheckBox ck in panel3.Controls)
                    ck.Checked = false;
            }
        }

        private void checkBox53_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = sender as CheckBox;
            if (c.Checked == true)
            {
                foreach (CheckBox ch in panel4.Controls)
                {
                    if (ch.Checked == false)
                        return;
                }
                checkBox53.Checked = true;
            }
            else
            {
                checkBox53.Checked = false;
            }
        }

        private void checkBox53_Click(object sender, EventArgs e)
        {
            if (checkBox53.CheckState == CheckState.Checked)
            {
                foreach (CheckBox ck in panel4.Controls)
                    ck.Checked = true;
            }
            else
            {
                foreach (CheckBox ck in panel4.Controls)
                    ck.Checked = false;
            }
        }

        private void checkBox50_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = sender as CheckBox;
            if (c.Checked == true)
            {
                foreach (CheckBox ch in panel5.Controls)
                {
                    if (ch.Checked == false)
                        return;
                }
                checkBox50.Checked = true;
            }
            else
            {
                checkBox50.Checked = false;
            }
        }

        private void checkBox50_Click(object sender, EventArgs e)
        {
            if (checkBox50.CheckState == CheckState.Checked)
            {
                foreach (CheckBox ck in panel5.Controls)
                    ck.Checked = true;
            }
            else
            {
                foreach (CheckBox ck in panel5.Controls)
                    ck.Checked = false;
            }
        }
    }
}
