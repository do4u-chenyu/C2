using C2.Controls.Move.Op;
using C2.Core;
using C2.Dialogs.Base;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace C2.OperatorViews
{
    public partial class DataFormatOperatorView : C1BaseOperatorView
    {
        public DataFormatOperatorView(MoveOpControl opControl) : base(opControl)
        {
            InitializeComponent();
            InitializeComponentManual(); // 设计器不支持复用基类中的tablelayoutpanel,需要手工初始化。
            InitByDataSource();
            LoadOption();

        }
        // 为了兼顾设计器，一些控件需要手工初始化。
        private void InitializeComponentManual()
        {
            this.button1.Click += new EventHandler(this.Add_Click);
            this.textBox0.Enter += new EventHandler(this.AliasTextBox_Enter);
            this.textBox0.Leave += new EventHandler(this.AliasTextBox_Leave);
  
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 38F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 38F));
            this.tableLayoutPanel1.Size = new Size(358, 85);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
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
            
            string[] factorList0 = this.opControl.Option.GetOptionSplit("factor0");
            if (factorList0.Length > 1)
            {
                int[] indexs0 = Array.ConvertAll(factorList0.Take(factorList0.Length - 1).ToArray(), int.Parse);
                this.textBox0.Text = factorList0[1];
                if (!OpUtil.IsArrayIndexOutOfBounds(this.comboBox0, indexs0[0]))
                {
                    this.comboBox0.Text = this.comboBox0.Items[indexs0[0]].ToString();
                    this.comboBox0.Tag = indexs0[0].ToString();
                }
            }


            int count = this.opControl.Option.KeysCount("factor") - 1;
            if (count < 1)
                return;
            InitNewFactorControl(count);
 
            for (int i = 0; i < count; i++)
            {
                string name = "factor" + (i + 1).ToString();
                string factor = this.opControl.Option.GetOption(name);
                if (String.IsNullOrEmpty(factor)) continue;

                string[] factorList1 = factor.Split('\t');
                if (factorList1.Length < 2) continue;

                int[] indexs1 = Array.ConvertAll(factorList1.Take(factorList1.Length - 1).ToArray(), int.Parse);                
                Control control1 = this.tableLayoutPanel1.GetControlFromPosition(1, i);
                Control control2 = this.tableLayoutPanel1.GetControlFromPosition(2, i);
                if (!OpUtil.IsArrayIndexOutOfBounds(control1, indexs1[0]))
                {
                    control1.Text = (control1 as ComboBox).Items[indexs1[0]].ToString();
                    control1.Tag = indexs1[0].ToString();
                }
               
                 control2.Text = factorList1[1];
            }   
        }
        protected override void SaveOption()
        {
            this.opControl.Option.Clear();
            this.opControl.Option.SetOption("columnname0", opControl.FirstDataSourceColumns);
            string index1 = comboBox0.Tag == null ? comboBox0.SelectedIndex.ToString() : comboBox0.Tag.ToString();
            string factor1 = index1 + "\t" + this.textBox0.Text;
            this.opControl.Option.SetOption("factor0", factor1);
            this.selectedColumns.Add(OutColumnName(this.comboBox0.Text, this.textBox0.Text));

            for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
            {
                ComboBox control1 = this.tableLayoutPanel1.GetControlFromPosition(1, i) as ComboBox;
                Control control2 = this.tableLayoutPanel1.GetControlFromPosition(2, i);
                string tmpIndex = control1.Tag == null ? control1.SelectedIndex.ToString() : control1.Tag.ToString();
                string factor = tmpIndex + "\t" + control2.Text;
                this.opControl.Option.SetOption("factor" + (i + 1).ToString(), factor);
                this.selectedColumns.Add(OutColumnName(control1.Text, control2.Text));
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
            foreach (Control ctl in this.tableLayoutPanel2.Controls)
            {
                if (ctl is ComboBox && String.IsNullOrEmpty(ctl.Text))
                {
                    MessageBox.Show("请选择字段");
                    return notReady;
                }
                if (ctl is TextBox && IsIllegalCharacter(ctl))
                {
                    MessageBox.Show("字段名中包含不合法字符TAB，请重新输入");
                    return notReady;
                }
            }
            foreach (Control ctl in this.tableLayoutPanel1.Controls)
            {
                if (ctl is ComboBox && string.IsNullOrEmpty(ctl.Text))
                {
                    MessageBox.Show("请选择字段");
                    return notReady;
                }
                if (ctl is TextBox && IsIllegalCharacter(ctl))
                {
                    MessageBox.Show("字段名中包含不合法字符TAB，请重新输入");
                    return notReady;
                }
            }
            return !notReady;
        }
        #endregion

        #region 分组字段重复选择判断
        protected override bool IsDuplicateSelect()
        {
            string index01 = this.comboBox0.Tag == null ? this.comboBox0.SelectedIndex.ToString() : this.comboBox0.Tag.ToString();
            string factor1 = index01 + "," + this.textBox0.Text;
            Dictionary<string, string> factors = new Dictionary<string, string>
            {
                ["factor0"] = factor1
            };
            for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
            {
                ComboBox control1 = (ComboBox)this.tableLayoutPanel1.Controls[i * 5 + 1];
                Control control2 = (Control)this.tableLayoutPanel1.Controls[i * 5 + 2];
                string index1 = control1.Tag == null ? control1.SelectedIndex.ToString() : control1.Tag.ToString();
                string factor = index1 + "," + control2.Text;
                factors["factor" + (i + 1).ToString()] = factor;

            }
            //找到所有的“添加条件”，判断是否有完全重复的“添加条件”
            var duplicateValues = factors.Where(x => x.Key.Contains("factor")).GroupBy(x => x.Value).Where(x => x.Count() > 1);
            foreach (var item in duplicateValues)
            {
                MessageBox.Show("数据标准化存在完全重复选项,请重新选择添加条件");
                return true;
            }
            return false;
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
            // 左表列下拉框
            ComboBox data0ComboBox = NewColumnsName0ComboBox();
            data0ComboBox.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tableLayoutPanel1.Controls.Add(data0ComboBox, 1, addLine);
            // 别名文本框
            TextBox textBox = NewAliasTextBox();
            textBox.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tableLayoutPanel1.Controls.Add(textBox, 2, addLine);
            // 添加行按钮
            Button addButton = NewAddButton(addLine.ToString());
            this.tableLayoutPanel1.Controls.Add(addButton, 3, addLine);
            addButton.BackColor = System.Drawing.SystemColors.Window;
            // 删除行按钮
            Button delButton = NewDelButton(addLine.ToString());
            this.tableLayoutPanel1.Controls.Add(delButton, 4, addLine);
            delButton.BackColor = System.Drawing.SystemColors.Window;
        }

        protected override void AddTableLayoutPanelControls(int lineNumber)
        {
            for (int k = this.tableLayoutPanel1.RowCount - 2; k >= lineNumber; k--)
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
        }

        protected override void MoveTableLayoutPanelControls(int lineNumber)
        {
            for (int k = lineNumber; k < this.tableLayoutPanel1.RowCount - 1; k++)
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
        }
    }
}
