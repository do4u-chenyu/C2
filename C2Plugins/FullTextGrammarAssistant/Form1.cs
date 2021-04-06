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
        protected List<ComboBox> comboBoxes;
        protected string[] logicItems = new string[] { };
        protected readonly Dictionary<int, int> filterDict = new Dictionary<int, int>();
        protected string[] nowColumnsName0;         // 当前左表(pin0)数据源表头字段(columnName)
        protected string[] nowColumnsName1;         // 当前右表(pin1)数据源表头字段
        protected int ColumnCount { get => this.tableLayoutPanel1.ColumnCount; }       // 有增减条件的表格步长
        private static global::System.Globalization.CultureInfo resourceCulture;
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
        protected ComboBox NewAndORComboBox()
        {
            ComboBox combox = NewComboBox();
            combox.Anchor = AnchorStyles.None;
            logicItems = new string[] { "AND", "OR", "NOT" };
            combox.Items.AddRange(logicItems);
            combox.SelectionChangeCommitted += new EventHandler(this.GetLogicalSelectedItemIndex);
            combox.TextUpdate += new System.EventHandler(LogicalComboBox_TextUpdate);
            combox.DropDownClosed += new System.EventHandler(LogicalComboBox_ClosedEvent);
            return combox;
        }
        protected void GetLogicalSelectedItemIndex(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            GetSelectedItemIndex(comboBox, logicItems);
        }
        protected void GetSelectedItemIndex(ComboBox comboBox, string[] nowColumns)
        {
            if (nowColumns.Length == 0)
                return;
            List<string> filterItems = new List<string>();
            for (int i = 0; i < comboBox.Items.Count; i++)
                filterItems.Add(comboBox.Items[i].ToString());


            // 下拉列表中选取值
            if (filterItems.SequenceEqual(nowColumns))
            {
                comboBox.Tag = comboBox.SelectedIndex.ToString();
                return;
            }

            // 保存下拉列表选择字段的索引
            if (filterDict.Keys.Contains(comboBox.SelectedIndex))
                comboBox.Tag = filterDict[comboBox.SelectedIndex];

        }
        public void LogicalComboBox_TextUpdate(object sender, EventArgs e)
        { ComboBox_TextUpdate(sender as ComboBox, logicItems); }
        public void ComboBox_TextUpdate(ComboBox comboBox, string[] nowColumns)
        {
            comboBox.SelectedIndex = -1;
            comboBox.Tag = null;
            int count = nowColumns.Length;
            if (comboBox.Text == "" || count == 0)
            {
                comboBox.DroppedDown = false;
                return;
            }

            filterDict.Clear();

            //每次搜索文本改变，就是对字典重新赋值
            comboBox.Items.Clear();
            List<string> filterItems = new List<string>();

            for (int i = 0; i < count; i++)
            {
                if (nowColumns[i].Contains(comboBox.Text))
                {
                    filterItems.Add(nowColumns[i]);
                    // 模糊搜索得到的下拉列表字段索引对应原始下拉列表字段索引
                    filterDict[filterItems.Count - 1] = i;
                }
            }

            comboBox.Items.AddRange(filterItems.ToArray());
            comboBox.SelectionStart = comboBox.Text.Length;
            comboBox.DroppedDown = true;
            //保持鼠标指针原来状态，有时候鼠标指针会被下拉框覆盖，所以要进行一次设置。
            Cursor = Cursors.Default;
        }
        public void LogicalComboBox_ClosedEvent(object sender, EventArgs e)
        { ComboBox_ClosedEvent(sender as ComboBox, logicItems); }
        public void ComboBox_ClosedEvent(ComboBox comboBox, string[] nowColumns)
        {

            if (nowColumns.Length == 0)
                return;

            // 恢复下拉列表原始字段
            comboBox.Items.Clear();
            comboBox.Items.AddRange(nowColumns);
            if (comboBox.Tag != null && !comboBox.Tag.ToString().Equals("-1") && IsInt(comboBox.Tag.ToString()))
            {
                int index = Convert.ToInt32(comboBox.Tag.ToString());
                comboBox.SelectedIndex = index;
                comboBox.Text = nowColumns[index];
            }

            // 手动将字段全部输入，这时候selectItem.index=-1,我们将设成下拉列表第一个匹配字段的索引
            if (comboBox.SelectedIndex == -1 && !string.IsNullOrEmpty(comboBox.Text))
            {
                for (int i = 0; i < nowColumns.Length; i++)
                {
                    if (nowColumns[i].Equals(comboBox.Text))
                    {
                        comboBox.SelectedIndex = i;
                        comboBox.Tag = i;
                        break;
                    }
                }
            }
        }
        public static bool IsInt(string value)
        {
            try
            {
                int.Parse(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        protected ComboBox NewComboBox()
        {
            ComboBox combox = new ComboBox
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("微软雅黑", 8f, FontStyle.Regular)
            };
            comboBoxes.Add(combox);
            return combox;
        }
        protected ComboBox NewColumnsName0ComboBox()
        {
            ComboBox combox = NewComboBox();
            combox.Items.AddRange(this.nowColumnsName0);
            combox.SelectionChangeCommitted += new EventHandler(this.GetLeftSelectedItemIndex);
            combox.TextUpdate += new System.EventHandler(LeftComboBox_TextUpdate);
            combox.DropDownClosed += new System.EventHandler(LeftComboBox_ClosedEvent);
            return combox;
        }
        protected void GetLeftSelectedItemIndex(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            GetSelectedItemIndex(comboBox, nowColumnsName0);
        }
        public void LeftComboBox_TextUpdate(object sender, EventArgs e)
        { ComboBox_TextUpdate(sender as ComboBox, nowColumnsName0); }
        public void LeftComboBox_ClosedEvent(object sender, EventArgs e)
        { ComboBox_ClosedEvent(sender as ComboBox, nowColumnsName0); }
        protected void CreateLine(int addLine)
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
        protected Button NewDelButton(string name)
        {
            Button delButton = NewButton(name);
            delButton.BackgroundImage = Properties.Resources.减;
            delButton.Click += new EventHandler(this.Del_Click);
            return delButton;
        }
        protected void Del_Click(object sender, EventArgs e)
        {
            this.tableLayoutPanel1.SuspendLayout();
            Button button = (Button)sender;
            int lineNumber = int.Parse(button.Name);


            for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
            {
                int buttonPosition = (i * ColumnCount) + ColumnCount - 1;
                Control bt1 = this.tableLayoutPanel1.Controls[buttonPosition];
                if (bt1.Name == button.Name)
                {
                    for (int j = buttonPosition; j >= (i * ColumnCount); j--)
                    {
                        this.tableLayoutPanel1.Controls.RemoveAt(j);
                    }
                    break;
                }

            }

            MoveTableLayoutPanelControls(lineNumber);

            this.tableLayoutPanel1.RowStyles.RemoveAt(this.tableLayoutPanel1.RowCount - 1);
            this.tableLayoutPanel1.RowCount -= 1;
            this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;

            this.tableLayoutPanel1.ResumeLayout(true);
        }
        protected virtual void MoveTableLayoutPanelControls(int lineNumber)
        {
            for (int k = lineNumber; k < this.tableLayoutPanel1.RowCount - 1; k++)
            {
                Control ctlNext = this.tableLayoutPanel1.GetControlFromPosition(0, k + 1);
                this.tableLayoutPanel1.SetCellPosition(ctlNext, new TableLayoutPanelCellPosition(0, k));
                Control ctlNext1 = this.tableLayoutPanel1.GetControlFromPosition(1, k + 1);
                this.tableLayoutPanel1.SetCellPosition(ctlNext1, new TableLayoutPanelCellPosition(1, k));
                Control ctlNext2 = this.tableLayoutPanel1.GetControlFromPosition(2, k + 1);
                this.tableLayoutPanel1.SetCellPosition(ctlNext2, new TableLayoutPanelCellPosition(2, k));
                Control ctlNext3 = this.tableLayoutPanel1.GetControlFromPosition(3, k + 1);
                ctlNext3.Name = k.ToString();
                this.tableLayoutPanel1.SetCellPosition(ctlNext3, new TableLayoutPanelCellPosition(3, k));
                Control ctlNext4 = this.tableLayoutPanel1.GetControlFromPosition(4, k + 1);
                ctlNext4.Name = k.ToString();
                this.tableLayoutPanel1.SetCellPosition(ctlNext4, new TableLayoutPanelCellPosition(4, k));
            }
        }
        protected Button NewAddButton(string name)
        {
            Button addButton = NewButton(name);
            addButton.BackgroundImage = Properties.Resources.加;
            addButton.Click += new EventHandler(this.pictureBox1_Click);
            return addButton;
        }
        private Button NewButton(string name)
        {
            Button delButton = new Button();
            delButton.FlatAppearance.BorderColor = SystemColors.Control;
            delButton.FlatAppearance.BorderSize = 0;
            delButton.FlatAppearance.MouseDownBackColor = SystemColors.Control;
            delButton.FlatAppearance.MouseOverBackColor = SystemColors.Control;
            delButton.FlatStyle = FlatStyle.Flat;
            delButton.BackColor = SystemColors.Control;
            delButton.UseVisualStyleBackColor = true;
            delButton.BackgroundImageLayout = ImageLayout.Center;
            delButton.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            delButton.Name = name;
            return delButton;
        }
        protected ComboBox NewColumnsName1ComboBox()
        {
            ComboBox combox = NewComboBox();
            combox.Items.AddRange(this.nowColumnsName1);
            combox.SelectionChangeCommitted += new EventHandler(this.GetRightSelectedItemIndex);
            combox.TextUpdate += new System.EventHandler(RightComboBox_TextUpdate);
            combox.DropDownClosed += new System.EventHandler(RightComboBox_ClosedEvent);
            return combox;
        }
        protected void GetRightSelectedItemIndex(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            GetSelectedItemIndex(comboBox, nowColumnsName1);
        }
        public void RightComboBox_TextUpdate(object sender, EventArgs e)
        { ComboBox_TextUpdate(sender as ComboBox, nowColumnsName1); }
        public void RightComboBox_ClosedEvent(object sender, EventArgs e)
        { ComboBox_ClosedEvent(sender as ComboBox, nowColumnsName1); }


        private void Form1_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            string helpfile = Application.StartupPath;
            helpfile += @"\Resources\Help\检索系统帮助.html";
            Process.Start(helpfile);
        }
    }
}
