using Citta_T1.Controls.Move;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{
    public partial class RelateOperatorView : Form
    {
        private MoveOpControl opControl;
        private string dataPath;
        private List<int> oldOutList;
        string[] columnName;
        private string oldOptionDict;
        private List<string> selectColumn;

        public RelateOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
            dataPath = "";
            //this.opControl = opControl;
            //this.oldOptionDict = string.Join(",", this.opControl.Option.OptionDict.ToList());
            //InitOptionInfo();
            //LoadOption();
            //this.oldOutList = this.OutList.GetItemCheckIndex();
            SetTextBoxName(this.DataInfoBox);
            SetTextBoxName(this.DataInfoBox2);
        }
        #region 配置初始化
        #endregion
        #region 保存加载
        #endregion
        #region 添加取消
        private void confirmButton_Click(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion
        public void SetTextBoxName(TextBox textBox)
        {
            string dataName = textBox.Text;
            int maxLength = 18;
            MatchCollection chs = Regex.Matches(dataName, "[\u4E00-\u9FA5]");
            int sumcount = chs.Count * 2;
            int sumcountDigit = Regex.Matches(dataName, "[a-zA-Z0-9]").Count;

            //防止截取字符串时中文乱码
            foreach (Match mc in chs)
            {
                if (dataName.IndexOf(mc.ToString()) == maxLength)
                {
                    maxLength -= 1;
                    break;
                }
            }

            if (sumcount + sumcountDigit > maxLength)
            {
                textBox.Text = System.Text.Encoding.GetEncoding("GB2312").GetString(System.Text.Encoding.GetEncoding("GB2312").GetBytes(dataName), 0, maxLength) + "...";
            }
        }

        private void createLine(int addLine)
        {
            // 添加控件
            ComboBox regBox = new ComboBox();
            regBox.Anchor = AnchorStyles.None;
            regBox.Items.AddRange(new object[] {
            "AND",
            "OR"});
            this.tableLayoutPanel1.Controls.Add(regBox, 0, addLine);

            ComboBox dataBox = new ComboBox();
            dataBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            //dataBox.Items.AddRange(this.columnName);
            this.tableLayoutPanel1.Controls.Add(dataBox, 1, addLine);

            Label label = new Label();
            label.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label.Text = "等于=";
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tableLayoutPanel1.Controls.Add(label, 2, addLine);

            ComboBox data2box = new ComboBox();
            data2box.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            data2box.Items.AddRange(new object[] {
            "age",
            "name"});
            data2box.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.tableLayoutPanel1.Controls.Add(data2box, 3, addLine);

            Button addButton1 = new Button();
            addButton1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            addButton1.FlatAppearance.BorderSize = 0;
            addButton1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            addButton1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            addButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            addButton1.BackColor = System.Drawing.SystemColors.Control;
            addButton1.BackgroundImage = global::Citta_T1.Properties.Resources.add;
            addButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            addButton1.Click += new System.EventHandler(this.add_Click);
            addButton1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            addButton1.Name = addLine.ToString();
            addButton1.UseVisualStyleBackColor = true;
            this.tableLayoutPanel1.Controls.Add(addButton1, 4, addLine);

            Button delButton1 = new Button();
            delButton1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            delButton1.FlatAppearance.BorderSize = 0;
            delButton1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            delButton1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            delButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            delButton1.BackColor = System.Drawing.SystemColors.Control;
            delButton1.UseVisualStyleBackColor = true;
            delButton1.BackgroundImage = global::Citta_T1.Properties.Resources.div;
            delButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            delButton1.Click += new System.EventHandler(this.del_Click);
            delButton1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            delButton1.Name = addLine.ToString();
            this.tableLayoutPanel1.Controls.Add(delButton1, 5, addLine);
        }

        private void add_Click(object sender, EventArgs e)
        {
            Button tmp = (Button)sender;
            int addLine;
            if (this.tableLayoutPanel1.RowCount == 0)
            {
                this.tableLayoutPanel1.RowCount++;
                this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40));
                addLine = 0;
                createLine(addLine);
            }
            else
            {
                if (tmp.Name == "button1")
                    addLine = 0;
                else
                    addLine = int.Parse(tmp.Name) + 1;

                this.tableLayoutPanel1.RowCount++;
                this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;

                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40));
                for (int k = this.tableLayoutPanel1.RowCount - 2; k >= addLine; k--)
                {
                    Control ctlNext = this.tableLayoutPanel1.GetControlFromPosition(0, k);
                    this.tableLayoutPanel1.SetCellPosition(ctlNext, new TableLayoutPanelCellPosition(0, k + 1));
                    Control ctlNext1 = this.tableLayoutPanel1.GetControlFromPosition(1, k);
                    this.tableLayoutPanel1.SetCellPosition(ctlNext1, new TableLayoutPanelCellPosition(1, k + 1));
                    Control ctlNext2 = this.tableLayoutPanel1.GetControlFromPosition(2, k);
                    this.tableLayoutPanel1.SetCellPosition(ctlNext2, new TableLayoutPanelCellPosition(2, k + 1));
                    Control ctlNext3 = this.tableLayoutPanel1.GetControlFromPosition(3, k);
                    this.tableLayoutPanel1.SetCellPosition(ctlNext3, new TableLayoutPanelCellPosition(3, k + 1));
                    Control ctlNext4 = this.tableLayoutPanel1.GetControlFromPosition(4, k);
                    ctlNext4.Name = (k + 1).ToString();
                    this.tableLayoutPanel1.SetCellPosition(ctlNext4, new TableLayoutPanelCellPosition(4, k + 1));
                    Control ctlNext5 = this.tableLayoutPanel1.GetControlFromPosition(5, k);
                    ctlNext5.Name = (k + 1).ToString();
                    this.tableLayoutPanel1.SetCellPosition(ctlNext5, new TableLayoutPanelCellPosition(5, k + 1));
                }
                createLine(addLine);
            }

        }

        private void del_Click(object sender, EventArgs e)
        {
            Button tmp = (Button)sender;
            int delLine = int.Parse(tmp.Name);

            for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
            {
                Control bt1 = this.tableLayoutPanel1.Controls[(i * 6) + 5];
                if (bt1.Name == tmp.Name)
                {
                    for (int j = (i * 6) + 5; j >= (i * 6); j--)
                    {
                        this.tableLayoutPanel1.Controls.RemoveAt(j);
                    }
                    break;
                }

            }

            for (int k = delLine; k < this.tableLayoutPanel1.RowCount - 1; k++)
            {
                Control ctlNext = this.tableLayoutPanel1.GetControlFromPosition(0, k + 1);
                this.tableLayoutPanel1.SetCellPosition(ctlNext, new TableLayoutPanelCellPosition(0, k));
                Control ctlNext1 = this.tableLayoutPanel1.GetControlFromPosition(1, k + 1);
                this.tableLayoutPanel1.SetCellPosition(ctlNext1, new TableLayoutPanelCellPosition(1, k));
                Control ctlNext2 = this.tableLayoutPanel1.GetControlFromPosition(2, k + 1);
                this.tableLayoutPanel1.SetCellPosition(ctlNext2, new TableLayoutPanelCellPosition(2, k));
                Control ctlNext3 = this.tableLayoutPanel1.GetControlFromPosition(3, k + 1);
                this.tableLayoutPanel1.SetCellPosition(ctlNext3, new TableLayoutPanelCellPosition(3, k));
                Control ctlNext4 = this.tableLayoutPanel1.GetControlFromPosition(4, k + 1);
                ctlNext4.Name = k.ToString();
                this.tableLayoutPanel1.SetCellPosition(ctlNext4, new TableLayoutPanelCellPosition(4, k));
                Control ctlNext5 = this.tableLayoutPanel1.GetControlFromPosition(5, k + 1);
                ctlNext5.Name = k.ToString();
                this.tableLayoutPanel1.SetCellPosition(ctlNext5, new TableLayoutPanelCellPosition(5, k));
            }
            this.tableLayoutPanel1.RowStyles.RemoveAt(this.tableLayoutPanel1.RowCount - 1);
            this.tableLayoutPanel1.RowCount = this.tableLayoutPanel1.RowCount - 1;

            this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;

        }

        private void DataInfoBox2_MouseClick(object sender, MouseEventArgs e)
        {
            //this.DataInfoBox2.Text = Path.GetFileNameWithoutExtension(this.dataPath1);
        }

        private void DataInfoBox2_LostFocus(object sender, EventArgs e)
        {
            SetTextBoxName(this.DataInfoBox2);
        }

        private void DataInfoBox_MouseClick(object sender, MouseEventArgs e)
        {
            //this.DataInfoBox.Text = Path.GetFileNameWithoutExtension(this.dataPath0);
        }

        private void DataInfoBox_LostFocus(object sender, EventArgs e)
        {
            SetTextBoxName(this.DataInfoBox);
        }
    }
}
