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

    public partial class FilterOperatorView : C1BaseOperatorView
    {
        public FilterOperatorView(MoveOpControl opControl) : base(opControl)
        {
            InitializeComponent();
            InitializeComponentManual();
            InitializeDataSource();          
            this.comparedItems = new string[] {
            "大于 >",
            "小于 <",
            "等于 =",
            "大于等于 ≥",
            "小于等于 ≦",
            "不等于 ≠" };
            LoadOption();
        }
        // 为了兼顾设计器，一些控件需要手工初始化。
        private void InitializeComponentManual()
        {
            this.comboBox1.SelectionChangeCommitted -= new EventHandler(this.GetRightSelectedItemIndex);
            this.comboBox1.TextUpdate -= new System.EventHandler(RightComboBox_TextUpdate);
            this.comboBox1.DropDownClosed -= new System.EventHandler(RightComboBox_ClosedEvent);
            this.comboBox1.SelectionChangeCommitted += new EventHandler(this.GetComparedSelectedItemIndex);
            this.comboBox1.TextUpdate += new System.EventHandler(ComparedComboBox_TextUpdate);
            this.comboBox1.DropDownClosed += new System.EventHandler(ComparedComboBox_ClosedEvent);

            this.button1.Click += new EventHandler(this.Add_Click);

            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 28F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Size = new Size(506, 84);
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
                if (types.Contains(ctl.GetType().Name) && String.IsNullOrEmpty(ctl.Text))
                {
                    HelpUtil.ShowMessageBox("请填写过滤条件");
                    return notReady;
                }
                if (ctl is TextBox && IsIllegalCharacter(ctl))
                {
                    HelpUtil.ShowMessageBox("字段名中包含不合法字符TAB，请重新输入");
                    return notReady;
                }
            }
            foreach (Control ctl in this.tableLayoutPanel1.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && String.IsNullOrEmpty(ctl.Text))
                {
                    HelpUtil.ShowMessageBox("请填写过滤条件");
                    return notReady;
                }
                if (ctl is TextBox && IsIllegalCharacter(ctl))
                {
                    HelpUtil.ShowMessageBox("字段名中包含不合法字符TAB，请重新输入");
                    return notReady;
                }
            }
            if (this.outListCCBL0.GetItemCheckIndex().Count == 0)
            {
                HelpUtil.ShowMessageBox("请填写输出字段");
                return notReady;
            }
            return !notReady;
        }
        #region 初始化配置
        protected override void InitializeDataSource()
        {
            // 初始化左右表数据源配置信息
            base.InitializeDataSource();
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

            this.opControl.Option.SetOption("outfield0", outListCCBL0.GetItemCheckIndex());

            string index00 = comboBox0.Tag == null ? comboBox0.SelectedIndex.ToString() : comboBox0.Tag.ToString();
            string index11 = comboBox1.Tag == null ? comboBox1.SelectedIndex.ToString() : comboBox1.Tag.ToString();

            string factor1 = index00 + "\t" + index11 + "\t" + this.textBoxEx1.Text;
            this.opControl.Option.SetOption("factor0", factor1);
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

                this.opControl.Option.SetOption("factor" + (i + 1).ToString(), factor);
            }

            //更新子图所有节点状态
            UpdateSubGraphStatus();
        }

        private void LoadOption()
        {
            if (Global.GetOptionDao().IsCleanSingleOperatorOption(this.opControl, this.nowColumnsName0))
                return;
            if (!String.IsNullOrEmpty(this.opControl.Option.GetOption("outfield0")))
            {
                string[] checkIndexs = this.opControl.Option.GetOptionSplit("outfield0");
                int[] indexs = Array.ConvertAll(checkIndexs, int.Parse);
                this.oldOutList0 = indexs.ToList();
                this.outListCCBL0.LoadItemCheckIndex(indexs);
                foreach (int i in indexs)
                {
                    if (OpUtil.IsArrayIndexOutOfBounds(this.outListCCBL0, i))
                        continue;
                    this.oldOutName0.Add(this.outListCCBL0.Items[i].ToString());
                }
                    
            }

            string[] factorList0 = this.opControl.Option.GetOptionSplit("factor0");
            if (factorList0.Length > 2)
            {
                int[] itemsList0 = Array.ConvertAll(factorList0.Take(factorList0.Length - 1).ToArray(), int.Parse);
                this.textBoxEx1.Text = factorList0[2];
                if (!OpUtil.IsArrayIndexOutOfBounds(this.comboBox0, itemsList0[0]))
                {
                    this.comboBox0.Text = this.comboBox0.Items[itemsList0[0]].ToString();
                    this.comboBox0.Tag = itemsList0[0].ToString();
                }
                if (!OpUtil.IsArrayIndexOutOfBounds(this.comboBox1, itemsList0[1]))
                {
                    this.comboBox1.Text = this.comboBox1.Items[itemsList0[1]].ToString();
                    this.comboBox1.Tag = itemsList0[1].ToString();

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
                if (factorList1.Length < 4) continue;

                int[] itemsList1 = Array.ConvertAll(factorList1.Take(factorList1.Length - 1).ToArray(), int.Parse);               
                Control control1 = this.tableLayoutPanel1.Controls[i * 6 + 0];
                Control control2 = this.tableLayoutPanel1.Controls[i * 6 + 1];
                Control control3 = this.tableLayoutPanel1.Controls[i * 6 + 2];
                Control control4 = this.tableLayoutPanel1.Controls[i * 6 + 3];
                if (!OpUtil.IsArrayIndexOutOfBounds(control1, itemsList1[0]))
                {
                    control1.Text = (control1 as ComboBox).Items[itemsList1[0]].ToString();
                    control1.Tag = itemsList1[0].ToString();
                }
                if (!OpUtil.IsArrayIndexOutOfBounds(control2, itemsList1[1]))
                {
                    control2.Text = (control2 as ComboBox).Items[itemsList1[1]].ToString();
                    control2.Tag = itemsList1[1].ToString();
                }
                if (!OpUtil.IsArrayIndexOutOfBounds(control3, itemsList1[2]))
                {
                    control3.Text = (control3 as ComboBox).Items[itemsList1[2]].ToString();
                    control3.Tag = itemsList1[2].ToString();
                }

                 control4.Text = factorList1[3];
            }
            
        }
        #endregion

        protected override void CreateLine(int addLine)
        {
            this.tableLayoutPanel2.Location = new System.Drawing.Point(60, 1);
            // And OR 选择框
            ComboBox regBox = NewAndORComboBox();
            regBox.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tableLayoutPanel1.Controls.Add(regBox, 0, addLine);
            // 左表列下拉框
            ComboBox data0ComboBox = NewColumnsName0ComboBox();
            data0ComboBox.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tableLayoutPanel1.Controls.Add(data0ComboBox, 1, addLine);
            ComboBox filterBox = NewComboBox();
            filterBox.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            filterBox.Items.AddRange(this.comparedItems);
            filterBox.SelectionChangeCommitted += new EventHandler(this.GetComparedSelectedItemIndex);
            filterBox.TextUpdate += new System.EventHandler(ComparedComboBox_TextUpdate);
            filterBox.DropDownClosed += new System.EventHandler(ComparedComboBox_ClosedEvent);

            this.tableLayoutPanel1.Controls.Add(filterBox, 2, addLine);

            TextBox textBox = new TextBox
            {
                Font = new Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134))),
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                //AutoSize = false,
                Size = new System.Drawing.Size(86, 26)
        };
            this.tableLayoutPanel1.Controls.Add(textBox, 3, addLine);
            // 添加行按钮
            Button addButton = NewAddButton(addLine.ToString());
            addButton.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel1.Controls.Add(addButton, 4, addLine);
            // 删除行按钮
            Button delButton = NewDelButton(addLine.ToString());
            delButton.BackColor = System.Drawing.SystemColors.Window;
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
            if (this.tableLayoutPanel1.RowCount == 1)
                this.tableLayoutPanel2.Location = new System.Drawing.Point(60, 41);
        }
    }
}
