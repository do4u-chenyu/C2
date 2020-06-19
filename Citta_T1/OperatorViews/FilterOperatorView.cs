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

    public partial class FilterOperatorView : BaseOperatorView
    {
        public FilterOperatorView(MoveOpControl opControl) : base(opControl)
        {
            InitializeComponent();
            InitializeComponentManual();
            InitByDataSource();
            LoadOption();
            checkOptions.Add("factor0", DataType.Int, new int[]{ -1,5})
                        .Add("factorI", DataType.Int, new int[]{ 1,-1,5 })
                        .Add("outfield", DataType.Int, -1);
        }
        // 为了兼顾设计器，一些控件需要手工初始化。
        private void InitializeComponentManual()
        {
            this.textBoxEx1.Leave += new EventHandler(this.IsIllegalCharacter);
            this.textBoxEx1.KeyUp += new KeyEventHandler(this.IsIllegalCharacter);
            this.button1.Click += new EventHandler(this.Add_Click);

            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 38F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 38F));
            this.tableLayoutPanel1.Size = new Size(435, 84);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
        }

        protected override bool IsOptionNotReady()
        {
            bool notReady = true;
            List<string> types = new List<string>
            {
                this.comboBox0.GetType().Name,
                this.outListCCBL0.GetType().Name,
                this.textBoxEx1.GetType().Name
            };
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
            if (this.outListCCBL0.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请填写输出字段");
                return notReady;
            }
            return !notReady;
        }
        #region 初始化配置
        private void InitByDataSource()
        {
            // 初始化左右表数据源配置信息
            this.InitDataSource();
            // 窗体自定义的初始化逻辑
            this.outListCCBL0.Items.AddRange(nowColumnsName0);
            this.comboBox0.Items.AddRange(nowColumnsName0);
        }
        #endregion
        #region 配置信息的保存与加载
        protected override void SaveOption()
        {
            this.opControl.Option.Clear();
            this.opControl.Option.SetOption("columnname0", opControl.FirstDataSourceColumns);
            this.selectedColumns = this.outListCCBL0.GetItemCheckText();

            this.opControl.Option.SetOption("outfield", outListCCBL0.GetItemCheckIndex());

            string index00 = comboBox0.Tag == null ? comboBox0.SelectedIndex.ToString() : comboBox0.Tag.ToString();
            string index11 = comboBox1.Tag == null ? comboBox1.SelectedIndex.ToString() : comboBox1.Tag.ToString();

            string factor1 = index00 + "\t" + index11 + "\t" + this.textBoxEx1.Text;
            this.opControl.Option.SetOption("factor1", factor1);
            if (this.tableLayoutPanel1.RowCount > 0)
            {
                for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
                {

                    Control control1 = this.tableLayoutPanel1.GetControlFromPosition(0, i);
                    Control control2 = this.tableLayoutPanel1.GetControlFromPosition(1, i);
                    Control control3 = this.tableLayoutPanel1.GetControlFromPosition(2, i);
                    Control control4 = this.tableLayoutPanel1.GetControlFromPosition(3, i);

                    string index1 = (control1 as ComboBox).Tag == null ? (control1 as ComboBox).SelectedIndex.ToString() : (control1 as ComboBox).Tag.ToString();
                    string index2 = (control2 as ComboBox).Tag == null ? (control2 as ComboBox).SelectedIndex.ToString() : (control2 as ComboBox).Tag.ToString();
                    string index3 = (control3 as ComboBox).Tag == null ? (control3 as ComboBox).SelectedIndex.ToString() : (control3 as ComboBox).Tag.ToString();
                    string factor = index1 + "\t" + index2 + "\t" + index3 + "\t" + control4.Text;

                    this.opControl.Option.SetOption("factor" + (i + 2).ToString(), factor);
                }
            }

            //更新子图所有节点状态
            UpdateSubGraphStatus();
        }

        private void LoadOption()
        {
            if (Global.GetOptionDao().IsCleanSingleOperatorOption(this.opControl, this.nowColumnsName0))
                return;
            if (!String.IsNullOrEmpty(this.opControl.Option.GetOption("outfield")))
            {
                string[] checkIndexs = this.opControl.Option.GetOptionSplit("outfield");
                int[] indexs = Array.ConvertAll(checkIndexs, int.Parse);
                this.oldOutList0 = indexs.ToList();
                this.outListCCBL0.LoadItemCheckIndex(indexs);
                foreach (int i in indexs)
                    this.oldOutName0.Add(this.outListCCBL0.Items[i].ToString());
            }
          
           
            string factor1 = this.opControl.Option.GetOption("factor1");
            if (!String.IsNullOrEmpty(factor1))
            {
                string[] factorList0 = factor1.Split('\t');
                int[] itemsList0 = Array.ConvertAll(factorList0.Take(factorList0.Length - 1).ToArray(), int.Parse);
                int index = itemsList0[0];
                this.comboBox0.Text = this.comboBox0.Items[itemsList0[0]].ToString();
                this.comboBox1.Text = this.comboBox1.Items[itemsList0[1]].ToString();
                this.textBoxEx1.Text = factorList0[2];
                this.comboBox0.Tag = itemsList0[0].ToString();
                this.comboBox1.Tag = itemsList0[1].ToString();
            }
            

            int count = this.opControl.Option.KeysCount("factor") - 1;
            if (count < 1)
                return;
            InitNewFactorControl(count);

            for (int i = 0; i < count; i++)
            {
                string name = "factor" + (i + 2).ToString();
                string factor = this.opControl.Option.GetOption(name);
                if (String.IsNullOrEmpty(factor)) continue;
                string[] factorList1 = factor.Split('\t');
                int[] itemsList1 = Array.ConvertAll(factorList1.Take(factorList1.Length - 1).ToArray(), int.Parse);             

                Control control1 = this.tableLayoutPanel1.Controls[i * 6 + 0];
                Control control2 = this.tableLayoutPanel1.Controls[i * 6 + 1];
                Control control3 = this.tableLayoutPanel1.Controls[i * 6 + 2];
                Control control4 = this.tableLayoutPanel1.Controls[i * 6 + 3];
                control1.Text = (control1 as ComboBox).Items[itemsList1[0]].ToString();
                control2.Text = (control2 as ComboBox).Items[itemsList1[1]].ToString();
                control3.Text = (control3 as ComboBox).Items[itemsList1[2]].ToString();
                control4.Text = factorList1[3];
                control1.Tag = itemsList1[0].ToString();
                control2.Tag = itemsList1[1].ToString();
                control3.Tag = itemsList1[2].ToString();
            }
            
        }
        #endregion
        protected override void CreateLine(int addLine)
        {
            // And OR 选择框
            ComboBox regBox = NewAndORComboBox();
            this.tableLayoutPanel1.Controls.Add(regBox, 0, addLine);
            // 左表列下拉框
            ComboBox data0ComboBox = NewColumnsName0ComboBox();
            this.tableLayoutPanel1.Controls.Add(data0ComboBox, 1, addLine);

            ComboBox filterBox = NewComboBox();
            filterBox.Items.AddRange(new object[] {
            "大于 >",
            "小于 <",
            "等于 =",
            "大于等于 ≥",
            "小于等于 ≦",
            "不等于 ≠"});
            this.tableLayoutPanel1.Controls.Add(filterBox, 2, addLine);

            TextBox textBox = new TextBox
            {
                Font = new Font("微软雅黑", 8f, FontStyle.Regular),
                Anchor = AnchorStyles.Left | AnchorStyles.Right
            };
            textBox.Leave += new EventHandler(this.IsIllegalCharacter);
            textBox.KeyUp += new KeyEventHandler(this.IsIllegalCharacter);
            this.tableLayoutPanel1.Controls.Add(textBox, 3, addLine);
            // 添加行按钮
            Button addButton = NewAddButton(addLine.ToString());
            this.tableLayoutPanel1.Controls.Add(addButton, 4, addLine);
            // 删除行按钮
            Button delButton = NewDelButton(addLine.ToString());
            this.tableLayoutPanel1.Controls.Add(delButton, 5, addLine);
        }

        protected override void AddTableLayoutPanelControls(int lineNumber)
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
                this.tableLayoutPanel1.SetCellPosition(ctlNext3, new TableLayoutPanelCellPosition(3, k + 1));
                Control ctlNext4 = this.tableLayoutPanel1.GetControlFromPosition(4, k);
                ctlNext4.Name = (k + 1).ToString();
                this.tableLayoutPanel1.SetCellPosition(ctlNext4, new TableLayoutPanelCellPosition(4, k + 1));
                Control ctlNext5 = this.tableLayoutPanel1.GetControlFromPosition(5, k);
                ctlNext5.Name = (k + 1).ToString();
                this.tableLayoutPanel1.SetCellPosition(ctlNext5, new TableLayoutPanelCellPosition(5, k + 1));
            }
        }

        protected override void MoveTableLayoutPanelControls(int delLine)
        {
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
        }
    }
}
