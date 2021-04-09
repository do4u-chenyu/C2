using C2.IAOLab.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FullTextGrammarAssistant
{
    public partial class Form1 : Form, IPlugin
    {
        private readonly List<string> previewTextList = new List<string>(new string[] { "","", "", "" });
        public Form1()
        {
            InitializeComponent();
            InitializeComponentManual();
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

        private void InitializeComponentManual()
        {
            this.button3.Click += new EventHandler(this.button3_Click);
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.70732F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.29269F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 144F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 271F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 839F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Location = new System.Drawing.Point(172, 470);
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
            this.textBox1.Text = "hello AND world OR 你好";
            this.textBox3.Text = DateTime.Now.AddYears(-1).ToString();
            this.textBox4.Text = DateTime.Now.ToString();
            this.previewTextList[1] = "--start " + DateTime.Now.AddYears(-1).ToString("yyyyMMddHHmmss");
            this.previewTextList[2] = "--end " + DateTime.Now.ToString("yyyyMMddHHmmss");
            this.previewTextList[3] = "--querystring \"" + this.textBox1.Text + "\"";
            UpdatePreviewText();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_Click_1(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                foreach (CheckBox ck in panel2.Controls)
                    ck.Checked = true;
            }
            else
            {
                foreach (CheckBox ck in panel2.Controls)
                    ck.Checked = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = sender as CheckBox;
            if (c.Checked == true)
            {
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            string helpfile = Application.StartupPath;
            helpfile += @"\Resources\Help\检索系统帮助.html";
            Process.Start(helpfile);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.tableLayoutPanel2.SuspendLayout();
            Button button = sender as Button;
            int lineNumber = 0;

            this.tableLayoutPanel2.RowCount++;
            this.tableLayoutPanel2.Height = this.tableLayoutPanel2.RowCount * 40;
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

            if (this.tableLayoutPanel2.RowCount > 1)
            {
                lineNumber = button.Name == "button3" ? 0 : int.Parse(button.Name) + 1;
                AddTableLayoutPanelControls(lineNumber);
            }
            CreateLine(lineNumber);
            this.tableLayoutPanel2.ResumeLayout(true);
        }
        protected void AddTableLayoutPanelControls(int lineNumber)
        {
            for (int k = this.tableLayoutPanel2.RowCount - 2; k >= lineNumber; k--)
            {
                Control ctlNext = this.tableLayoutPanel2.GetControlFromPosition(0, k);
                this.tableLayoutPanel2.SetCellPosition(ctlNext, new TableLayoutPanelCellPosition(0, k + 1));
                Control ctlNext1 = this.tableLayoutPanel2.GetControlFromPosition(1, k);
                this.tableLayoutPanel2.SetCellPosition(ctlNext1, new TableLayoutPanelCellPosition(1, k + 1));
                Control ctlNext2 = this.tableLayoutPanel2.GetControlFromPosition(2, k);
                this.tableLayoutPanel2.SetCellPosition(ctlNext2, new TableLayoutPanelCellPosition(2, k + 1));
                Control ctlNext3 = this.tableLayoutPanel2.GetControlFromPosition(3, k);
                this.tableLayoutPanel2.SetCellPosition(ctlNext3, new TableLayoutPanelCellPosition(3, k + 1));
                Control ctlNext4 = this.tableLayoutPanel2.GetControlFromPosition(4, k);
                ctlNext4.Name = (k + 1).ToString();
                this.tableLayoutPanel2.SetCellPosition(ctlNext4, new TableLayoutPanelCellPosition(4, k + 1));
                //Control ctlNext5 = this.tableLayoutPanel1.GetControlFromPosition(5, k);
                //ctlNext5.Name = (k + 1).ToString();
                //this.tableLayoutPanel1.SetCellPosition(ctlNext5, new TableLayoutPanelCellPosition(5, k + 1));
            }
        }
        protected void CreateLine(int addLine)
        {
            // And OR 选择框
            ComboBox regBox = NewAndORComboBox();
            this.tableLayoutPanel1.Controls.Add(regBox, 0, addLine);
        }

        private ComboBox NewAndORComboBox()
        {
            throw new NotImplementedException();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            this.previewTextList[1] = "--start " + Convert.ToDateTime(this.textBox3.Text).ToString("yyyyMMddHHmmss");
            UpdatePreviewText();
        }
        private void UpdatePreviewText()
        {
            this.previewTextList[0] = "/home/search/sbin/queryclient --server 127.0.0.1 --port 9871";
            this.previewCmdText.Text = String.Join(" ", this.previewTextList);
        }

        private void previewCmdText_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            this.previewTextList[2] = "--end " + Convert.ToDateTime(this.textBox4.Text).ToString("yyyyMMddHHmmss");
            UpdatePreviewText();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.previewTextList[3] = "--querystring \"" + this.textBox1.Text + "\"";
            UpdatePreviewText();
        }
    }
}
