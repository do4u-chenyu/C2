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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.tableLayoutPanel1.SuspendLayout();
            PictureBox pictureBox = sender as PictureBox;
            int lineNumber = 0;

            this.tableLayoutPanel1.RowCount++;
            this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

            if (this.tableLayoutPanel1.RowCount > 1)
            {
                lineNumber = pictureBox.Name == "pictureBox1" ? 0 : int.Parse(pictureBox.Name) + 1;
                AddTableLayoutPanelControls(lineNumber);
            }
            CreateLine(lineNumber);
            this.tableLayoutPanel1.ResumeLayout(true);
        }
        protected virtual void AddTableLayoutPanelControls(int lineNumber)
        {
            for (int k = this.tableLayoutPanel1.RowCount - 2; k >= lineNumber; k--)
            {
                Control ctlNext = this.tableLayoutPanel1.GetControlFromPosition(0, k);
                this.tableLayoutPanel1.SetCellPosition(ctlNext, new TableLayoutPanelCellPosition(0, k + 1));
                Control ctlNext1 = this.tableLayoutPanel1.GetControlFromPosition(1, k);
                this.tableLayoutPanel1.SetCellPosition(ctlNext1, new TableLayoutPanelCellPosition(1, k + 1));
                Control ctlNext2 = this.tableLayoutPanel1.GetControlFromPosition(2, k);
                this.tableLayoutPanel1.SetCellPosition(ctlNext2, new TableLayoutPanelCellPosition(2, k + 1));
                Control ctlNext3 = this.tableLayoutPanel1.GetControlFromPosition(3, k);
                ctlNext3.Name = (k + 1).ToString();
                this.tableLayoutPanel1.SetCellPosition(ctlNext3, new TableLayoutPanelCellPosition(3, k + 1));
                Control ctlNext4 = this.tableLayoutPanel1.GetControlFromPosition(4, k);
                ctlNext4.Name = (k + 1).ToString();
                this.tableLayoutPanel1.SetCellPosition(ctlNext4, new TableLayoutPanelCellPosition(4, k + 1));
            }
        }
        protected override void CreateLine(int addLine)
        {
            if (this.tableLayoutPanel1.RowCount == 1)
            {
                this.tableLayoutPanel2.Location = new System.Drawing.Point(58, 4);
            }

            // And OR 选择框
            ComboBox regBox = NewAndORComboBox();
            regBox.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tableLayoutPanel1.Controls.Add(regBox, 0, addLine);
            // 左表列下拉框
            ComboBox data0ComboBox = NewColumnsName0ComboBox();
            data0ComboBox.Size = new System.Drawing.Size(88, 26);
            data0ComboBox.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tableLayoutPanel1.Controls.Add(data0ComboBox, 1, addLine);
            // 右表列下拉框
            ComboBox data1ComboBox = NewColumnsName1ComboBox();
            data1ComboBox.Size = new System.Drawing.Size(88, 26);
            data1ComboBox.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tableLayoutPanel1.Controls.Add(data1ComboBox, 2, addLine);
            // 添加行按钮
            Button addButton = NewAddButton(addLine.ToString());
            this.tableLayoutPanel1.Controls.Add(addButton, 3, addLine);
            addButton.BackColor = System.Drawing.SystemColors.Window;
            // 删除行按钮
            Button delButton = NewDelButton(addLine.ToString());
            this.tableLayoutPanel1.Controls.Add(delButton, 4, addLine);
            delButton.BackColor = System.Drawing.SystemColors.Window;
        }
    }
}
