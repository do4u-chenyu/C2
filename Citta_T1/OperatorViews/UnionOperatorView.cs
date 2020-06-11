using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.OperatorViews.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{
    public partial class UnionOperatorView : BaseOperatorView
    {

        public UnionOperatorView(MoveOpControl opControl) : base(opControl)
        {
            InitializeComponent();
            InitByDataSource();
            LoadOption();

            this.textBox0.Leave += new EventHandler(this.IsIllegalCharacter);
            this.textBox0.KeyUp += new KeyEventHandler(this.IsIllegalCharacter);
        }
        #region 初始化配置
        private void InitByDataSource()
        {
            // 初始化左右表数据源配置信息
            this.InitDataSource();
            // 窗体自定义的初始化逻辑
            this.oldOutName0 = this.opControl.Option.GetOptionSplit("outname").ToList();
            this.comboBox0.Items.AddRange(nowColumnsName0);
            this.comboBox1.Items.AddRange(nowColumnsName1);
        }
        #endregion
        #region 配置信息的保存与加载
        protected override void SaveOption()
        {
            //删除部分取并条件不清空时，加载根据配置字典内容，旧条件仍会加载
            this.opControl.Option.OptionDict.Clear();
            this.opControl.Option.SetOption("columnname0", opControl.FirstDataSourceColumns);
            this.opControl.Option.SetOption("columnname1", opControl.SecondDataSourceColumns);
            string index01 = this.comboBox0.Tag == null ? this.comboBox0.SelectedIndex.ToString() : this.comboBox0.Tag.ToString();
            string index02 = this.comboBox1.Tag == null ? this.comboBox1.SelectedIndex.ToString() : this.comboBox1.Tag.ToString();
            string factor1 = index01 + "\t" + index02 + "\t" + this.textBox0.Text;
            this.opControl.Option.SetOption("factor1", factor1);
            this.selectedColumns.Add(OutColumnName(this.comboBox0.Text, this.textBox0.Text));
            if (this.tableLayoutPanel1.RowCount > 0)
            {
                for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
                {
                    ComboBox control1 = this.tableLayoutPanel1.GetControlFromPosition(0, i) as ComboBox;
                    ComboBox control2 = this.tableLayoutPanel1.GetControlFromPosition(1, i) as ComboBox;
                    Control control3 = (Control)this.tableLayoutPanel1.Controls[i * 5 + 2];
                    string index1 = control1.Tag == null ? control1.SelectedIndex.ToString() : control1.Tag.ToString();
                    string index2 = control2.Tag == null ? control2.SelectedIndex.ToString() : control2.Tag.ToString();

                    string factor = index1 + "\t" + index2 + "\t" + control3.Text;
                    this.opControl.Option.SetOption("factor" + (i + 2).ToString(), factor);
                    this.selectedColumns.Add(OutColumnName((control1 as ComboBox).Text, control3.Text));
                }
            }
            this.opControl.Option.SetOption("outname", this.selectedColumns);
            this.opControl.Option.SetOption("noRepetition", this.noRepetition.Checked);
            this.opControl.Option.SetOption("repetition", this.repetition.Checked);

            //更新子图所有节点状态
            UpdateSubGraphStatus();
        }
        private string OutColumnName(string name, string alias)
        {
            return alias == "别名" ? name : alias;
        }
        private void LoadOption()
        {
            if (Global.GetOptionDao().IsCleanBinaryOperatorOption(this.opControl, this.nowColumnsName0, this.nowColumnsName1))
                return;

            int count = this.opControl.Option.KeysCount("factor");
            string factor1 = this.opControl.Option.GetOption("factor1");

            this.noRepetition.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("noRepetition"));
            this.repetition.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("repetition"));
            string[] factorList0 = factor1.Split('\t');
            int[] itemsList0 = Array.ConvertAll(factorList0.Take(factorList0.Length - 1).ToArray(), int.Parse);
            this.comboBox0.Text = this.comboBox0.Items[itemsList0[0]].ToString();
            this.comboBox1.Text = this.comboBox1.Items[itemsList0[1]].ToString();
            this.textBox0.Text = factorList0[2];
            this.comboBox0.Tag = itemsList0[0].ToString();
            this.comboBox1.Tag = itemsList0[1].ToString();

            if (count <= 1)
                return;
            InitNewFactorControl(count - 1);


            for (int i = 2; i < (count + 1); i++)
            {
                string name = "factor" + i.ToString();
                string factor = this.opControl.Option.GetOption(name);
                if (factor == "") continue;

                string[] factorList1 = factor.Split('\t');
                int[] itemsList1 = Array.ConvertAll(factorList1.Take(factorList1.Length - 1).ToArray(), int.Parse);


                Control control1 = this.tableLayoutPanel1.Controls[(i - 2) * 5 + 0];
                Control control2 = this.tableLayoutPanel1.Controls[(i - 2) * 5 + 1];
                Control control3 = this.tableLayoutPanel1.Controls[(i - 2) * 5 + 2];
                control1.Text = (control1 as ComboBox).Items[itemsList1[0]].ToString();
                control2.Text = (control2 as ComboBox).Items[itemsList1[1]].ToString();
                control3.Text = factorList1[2];
                control1.Tag = itemsList1[0].ToString();
                control2.Tag = itemsList1[1].ToString();
            }
         
        }


        #endregion
        #region 判断是否配置完毕
        protected override bool IsDuplicateSelect()
        {
            bool repetition = false;
            string index01 = this.comboBox0.Tag == null ? this.comboBox0.SelectedIndex.ToString() : this.comboBox0.Tag.ToString();
            string index02 = this.comboBox1.Tag == null ? this.comboBox1.SelectedIndex.ToString() : this.comboBox1.Tag.ToString();
            string factor1 = index01 + "," + index02 + "," + this.textBox0.Text;
            Dictionary<string, string> factors = new Dictionary<string, string>();
            factors["factor1"] = factor1;
            if (this.tableLayoutPanel1.RowCount > 0)
            {
                for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
                {
                    ComboBox control1 = (ComboBox)this.tableLayoutPanel1.Controls[i * 5 + 0];
                    ComboBox control2 = (ComboBox)this.tableLayoutPanel1.Controls[i * 5 + 1];
                    Control control3 = (Control)this.tableLayoutPanel1.Controls[i * 5 + 2];
                    string index1 = control1.Tag == null ? control1.SelectedIndex.ToString() : control1.Tag.ToString();
                    string index2 = control2.Tag == null ? control2.SelectedIndex.ToString() : control2.Tag.ToString();
                    string factor = index1 + "," + index2 + "," + control3.Text;
                    factors["factor" + (i + 2).ToString()] = factor;

                }
            }
            var duplicateValues = factors.Where(x => x.Key.Contains("factor")).GroupBy(x => x.Value).Where(x => x.Count() > 1);
            foreach (var item in duplicateValues)
            {
                MessageBox.Show("取并集条件存在完全重复选项,请重新选择并集条件");
                repetition = true;
            }
            return repetition;
        }
        protected override bool IsOptionNotReady()
        {
            bool notReady = true;
            List<string> types = new List<string>();
            types.Add(this.comboBox0.GetType().Name);
            types.Add(this.textBox0.GetType().Name);
            foreach (Control ctl in this.tableLayoutPanel2.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == String.Empty)
                {
                    MessageBox.Show("请填写过滤条件");
                    return notReady;
                }
            }
            foreach (Control ctl in this.tableLayoutPanel1.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == String.Empty)
                {
                    MessageBox.Show("请填写过滤条件");
                    return notReady;
                }
            }
            return !notReady;
        }
        #endregion
        protected override void CreateLine(int addLine)
        {
            // 添加控件
            ComboBox dataBox = new ComboBox();
            dataBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            dataBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            dataBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dataBox.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            dataBox.Items.AddRange(this.nowColumnsName0);
            dataBox.Leave += new EventHandler(this.Control_Leave);
            dataBox.KeyUp += new KeyEventHandler(this.Control_KeyUp);
            dataBox.SelectionChangeCommitted += new EventHandler(this.GetSelectedItemIndex);
            this.tableLayoutPanel1.Controls.Add(dataBox, 0, addLine);

            ComboBox filterBox = NewFilterComboBox();
            this.tableLayoutPanel1.Controls.Add(filterBox, 1, addLine);

            TextBox textBox = NewAliasTextBox();
            this.tableLayoutPanel1.Controls.Add(textBox, 2, addLine);

            Button addButton = NewAddButton(addLine.ToString());
            addButton.Click += new EventHandler(this.Add_Click);
            this.tableLayoutPanel1.Controls.Add(addButton, 3, addLine);

            Button delButton = NewDelButton(addLine.ToString());
            delButton.Click += new EventHandler(this.Del_Click);
            this.tableLayoutPanel1.Controls.Add(delButton, 4, addLine);
        }

        private void Add_Click(object sender, EventArgs e)
        {
            Button tmp = (Button)sender;
            int addLine;
            if (this.tableLayoutPanel1.RowCount == 0)
            {
                this.tableLayoutPanel1.RowCount++;
                this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;
                this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
                addLine = 0;
                CreateLine(addLine);
            }
            else
            {
                if (tmp.Name == "button1")
                    addLine = 0;
                else
                    addLine = int.Parse(tmp.Name) + 1;

                this.tableLayoutPanel1.RowCount++;
                this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;
                this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
                for (int k = this.tableLayoutPanel1.RowCount - 2; k >= addLine; k--)
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
                CreateLine(addLine);
            }

        }

        private void Del_Click(object sender, EventArgs e)
        {
            Button tmp = (Button)sender;
            int delLine = int.Parse(tmp.Name);

            for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
            {
                Control bt1 = this.tableLayoutPanel1.Controls[(i * 5) + 4];
                if (bt1.Name == tmp.Name)
                {
                    for (int j = (i * 5) + 4; j >= (i * 5); j--)
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
                ctlNext3.Name = k.ToString();
                this.tableLayoutPanel1.SetCellPosition(ctlNext3, new TableLayoutPanelCellPosition(3, k));
                Control ctlNext4 = this.tableLayoutPanel1.GetControlFromPosition(4, k + 1);
                ctlNext4.Name = k.ToString();
                this.tableLayoutPanel1.SetCellPosition(ctlNext4, new TableLayoutPanelCellPosition(4, k));
            }
            this.tableLayoutPanel1.RowStyles.RemoveAt(this.tableLayoutPanel1.RowCount - 1);
            this.tableLayoutPanel1.RowCount -= 1;

            this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;
        }
    }
}
