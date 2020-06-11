using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.OperatorViews.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{
    public partial class DataFormatOperatorView : BaseOperatorView
    {
        public DataFormatOperatorView(MoveOpControl opControl) : base(opControl)
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
            this.comboBox0.Items.AddRange(nowColumnsName0);
            this.oldOutName0 = this.opControl.Option.GetOptionSplit("outname").ToList();
        }
        #endregion

        #region 配置信息的保存与加载

        private void LoadOption()
        {
            if (Global.GetOptionDao().IsCleanSingleOperatorOption(this.opControl, this.nowColumnsName0))
                return;

            int count = this.opControl.Option.KeysCount("factor");
            string factor1 = this.opControl.Option.GetOption("factor1");
            string[] factorList0 = factor1.Split('\t');
            int[] indexs0 = Array.ConvertAll(factorList0.Take(factorList0.Length - 1).ToArray(), int.Parse);

            this.comboBox0.Text = this.comboBox0.Items[indexs0[0]].ToString();
            this.comboBox0.Tag = indexs0[0].ToString();
            this.textBox0.Text = factorList0[1];

            if (count <= 1)
                return;
            InitNewFactorControl(count - 1);
 
            for (int i = 2; i < (count + 1); i++)
            {
                string name = "factor" + i.ToString();
                string factor = this.opControl.Option.GetOption(name);
                if (factor == "") continue;

                string[] factorList1 = factor.Split('\t');
                int[] indexs1 = Array.ConvertAll(factorList1.Take(factorList1.Length - 1).ToArray(), int.Parse);

                Control control1 = this.tableLayoutPanel1.GetControlFromPosition(1, i - 2);
                Control control2 = this.tableLayoutPanel1.GetControlFromPosition(2, i - 2);
                control1.Text = (control1 as ComboBox).Items[indexs1[0]].ToString();
                control1.Tag = indexs1[0].ToString();
                control2.Text = factorList1[1];
            }   
        }
        protected override void SaveOption()
        {
            this.opControl.Option.OptionDict.Clear();
            this.opControl.Option.SetOption("columnname0", opControl.FirstDataSourceColumns);
            string index1 = comboBox0.Tag == null ? comboBox0.SelectedIndex.ToString() : comboBox0.Tag.ToString();
            string factor1 = index1 + "\t" + this.textBox0.Text;
            this.opControl.Option.SetOption("factor1", factor1);
            this.selectedColumns.Add(OutColumnName(this.comboBox0.Text, this.textBox0.Text));

            if (this.tableLayoutPanel1.RowCount > 0)
            {
                for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
                {
                    ComboBox control1 = this.tableLayoutPanel1.GetControlFromPosition(1, i) as ComboBox;
                    Control control2 = this.tableLayoutPanel1.GetControlFromPosition(2, i);
                    //Control control1 = (Control)this.tableLayoutPanel1.Controls[i * 5 + 1];
                    //Control control2 = (Control)this.tableLayoutPanel1.Controls[i * 5 + 2];
                    string tmpIndex = control1.Tag == null ? control1.SelectedIndex.ToString() : control1.Tag.ToString();
                    string factor = tmpIndex + "\t" + control2.Text;
                    this.opControl.Option.SetOption("factor" + (i + 2).ToString(), factor);
                    this.selectedColumns.Add(OutColumnName(control1.Text, control2.Text));
                }
            }
            this.opControl.Option.SetOption("outname",this.selectedColumns);

            //更新子图所有节点状态
            UpdateSubGraphStatus(); ;
        }

        private string OutColumnName(string name, string alias)
        {
            return alias == "别名" ? name : alias;
        }
        #endregion

        #region 添加取消        

        protected override bool IsOptionNotReady()
        {
            bool notReady = true;
            List<string> types = new List<string>
            {
                this.comboBox0.GetType().Name
            };
            foreach (Control ctl in this.tableLayoutPanel2.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == "")
                {
                    MessageBox.Show("请选择字段");
                    return notReady;
                }
            }
            foreach (Control ctl in this.tableLayoutPanel1.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == "")
                {
                    MessageBox.Show("请选择字段");
                    return notReady;
                }
            }
            return !notReady;
        }
        #endregion

        #region 分组字段重复选择判断
        protected override bool IsDuplicateSelect()
        {
            bool repetition = false;
            string index01 = this.comboBox0.Tag == null ? this.comboBox0.SelectedIndex.ToString() : this.comboBox0.Tag.ToString();
            string factor1 = index01 + "," + this.textBox0.Text;
            Dictionary<string, string> factors = new Dictionary<string, string>
            {
                ["factor1"] = factor1
            };
            if (this.tableLayoutPanel1.RowCount > 0)
            {
                for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
                {
                    ComboBox control1 = (ComboBox)this.tableLayoutPanel1.Controls[i * 5 + 1];
                    Control control2 = (Control)this.tableLayoutPanel1.Controls[i * 5 + 2];
                    string index1 = control1.Tag == null ? control1.SelectedIndex.ToString() : control1.Tag.ToString();
                    string factor = index1 + "," + control2.Text;
                    factors["factor" + (i + 2).ToString()] = factor;

                }
            }
            var duplicateValues = factors.Where(x => x.Key.Contains("factor")).GroupBy(x => x.Value).Where(x => x.Count() > 1);
            foreach (var item in duplicateValues)
            {
                MessageBox.Show("数据标准化存在完全重复选项,请重新选择并集条件");
                repetition = true;
            }
            return repetition;
        }
        #endregion

        protected override void CreateLine(int addLine)
        {
            // 添加控件
            Label label = new Label
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                AutoSize = true,
                Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = (addLine + 2).ToString()
            };
            this.tableLayoutPanel1.Controls.Add(label, 0, addLine);


            ComboBox dataBox = new ComboBox
            {
                AutoCompleteMode = AutoCompleteMode.SuggestAppend,
                AutoCompleteSource = AutoCompleteSource.ListItems,
                Font = new Font("微软雅黑", 8f, FontStyle.Regular),
                Anchor = AnchorStyles.Left | AnchorStyles.Right
            };
            dataBox.Items.AddRange(this.nowColumnsName0);
            dataBox.Leave += new EventHandler(this.Control_Leave);
            dataBox.KeyUp += new KeyEventHandler(this.Control_KeyUp);
            dataBox.SelectionChangeCommitted += new EventHandler(this.GetSelectedItemIndex);
            this.tableLayoutPanel1.Controls.Add(dataBox, 1, addLine);

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
                    ctlNext.Text = (k + 3).ToString();
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
                ctlNext.Text = (k + 2).ToString();
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
            this.tableLayoutPanel1.RowCount = this.tableLayoutPanel1.RowCount - 1;

            this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;

        }
    }
}
