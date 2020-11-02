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
    public partial class RelateOperatorView_v5 : C1BaseOperatorView
    {

        public RelateOperatorView_v5(MoveOpControl opControl) : base(opControl)
        {
            InitializeComponent();
            InitializeComponentManual();
            InitByDataSource();
            LoadOption();
        }

        // 为了兼顾设计器，一些控件需要手工初始化。
        private void InitializeComponentManual()
        {
            this.button1.Click += new EventHandler(this.Add_Click);

            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 28F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Size = new Size(471, 84);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
        }

        #region 配置初始化
        private void InitByDataSource()
        {
            // 初始化左右表数据源配置信息
            this.InitDataSource();
            // 窗体自定义的初始化逻辑
            this.comboBox0.Items.AddRange(nowColumnsName0);
            this.outListCCBL0.Items.AddRange(nowColumnsName0);

            this.comboBox1.Items.AddRange(nowColumnsName1);
            this.outListCCBL1.Items.AddRange(nowColumnsName1);
        }

        #endregion
        #region 保存加载
        private void LoadOption()
        {
            if (Global.GetOptionDao().IsCleanBinaryOperatorOption(this.opControl, this.nowColumnsName0, this.nowColumnsName1))
                return;

            if (!String.IsNullOrEmpty(this.opControl.Option.GetOption("outfield0")))
            {
                string[] checkIndexs0 = this.opControl.Option.GetOptionSplit("outfield0");
                int[] indexs0 = Array.ConvertAll(checkIndexs0, int.Parse);
                this.oldOutList0 = indexs0.ToList();
                this.outListCCBL0.LoadItemCheckIndex(indexs0);
                foreach (int index in indexs0)
                {
                    if (OpUtil.IsArrayIndexOutOfBounds(this.outListCCBL0, index))
                        continue;
                    this.oldOutName0.Add(this.outListCCBL0.Items[index].ToString());
                }

            }
            if (!String.IsNullOrEmpty(this.opControl.Option.GetOption("outfield1")))
            {
                string[] checkIndexs1 = this.opControl.Option.GetOptionSplit("outfield1");
                int[] indexs1 = Array.ConvertAll(checkIndexs1, int.Parse);
                this.oldOutList1 = indexs1.ToList();
                this.outListCCBL1.LoadItemCheckIndex(indexs1);
                foreach (int index in indexs1)
                {
                    if (OpUtil.IsArrayIndexOutOfBounds(this.outListCCBL1, index))
                        continue;
                    this.oldOutName1.Add(this.outListCCBL1.Items[index].ToString());
                }

            }


            int[] itemsList0 = Array.ConvertAll(this.opControl.Option.GetOptionSplit("factor0"), int.Parse);
            if (itemsList0.Length > 1)
            {

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
                int[] itemsList1 = Array.ConvertAll(this.opControl.Option.GetOptionSplit(name), int.Parse);
                if (itemsList1.Length < 3) continue;

                Control control1 = this.tableLayoutPanel1.Controls[i * 6 + 0];
                Control control2 = this.tableLayoutPanel1.Controls[i * 6 + 1];
                Control control3 = this.tableLayoutPanel1.Controls[i * 6 + 3];
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
            }
        }
        protected override void SaveOption()
        {
            this.opControl.Option.Clear();
            this.opControl.Option.SetOption("columnname0", opControl.FirstDataSourceColumns);
            this.opControl.Option.SetOption("columnname1", opControl.SecondDataSourceColumns);
            this.opControl.Option.SetOption("outfield0", outListCCBL0.GetItemCheckIndex());
            this.opControl.Option.SetOption("outfield1", outListCCBL1.GetItemCheckIndex());
            this.selectedColumns = this.outListCCBL0.GetItemCheckText().Concat(this.outListCCBL1.GetItemCheckText()).ToList();

            string index01 = this.comboBox0.Tag == null ? this.comboBox0.SelectedIndex.ToString() : this.comboBox0.Tag.ToString();
            string index02 = this.comboBox1.Tag == null ? this.comboBox1.SelectedIndex.ToString() : this.comboBox1.Tag.ToString();
            string factor1 = index01 + "\t" + index02;
            this.opControl.Option.SetOption("factor0", factor1);
            for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
            {
                ComboBox control1 = this.tableLayoutPanel1.GetControlFromPosition(0, i) as ComboBox;
                ComboBox control2 = this.tableLayoutPanel1.GetControlFromPosition(1, i) as ComboBox;
                ComboBox control3 = this.tableLayoutPanel1.GetControlFromPosition(3, i) as ComboBox;
                string index1 = control1.Tag == null ? control1.SelectedIndex.ToString() : control1.Tag.ToString();
                string index2 = control2.Tag == null ? control2.SelectedIndex.ToString() : control2.Tag.ToString();
                string index3 = control3.Tag == null ? control3.SelectedIndex.ToString() : control3.Tag.ToString();
                string factor = index1 + "\t" + index2 + "\t" + index3;

                this.opControl.Option.SetOption("factor" + (i + 1).ToString(), factor);
            }

            //更新子图所有节点状态
            UpdateSubGraphStatus();
        }
        #endregion
        #region 判断是否配置完毕       
        protected override bool IsOptionNotReady()
        {
            bool notReady = true;
            List<string> types = new List<string>
            {
                this.comboBox0.GetType().Name,
                this.outListCCBL0.GetType().Name
            };
            foreach (Control ctl in this.tableLayoutPanel2.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == String.Empty)
                {
                    MessageBox.Show("请填写连接条件");
                    return notReady;
                }
            }
            foreach (Control ctl in this.tableLayoutPanel1.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == String.Empty)
                {
                    MessageBox.Show("请填写连接条件");
                    return notReady;
                }
            }
            if (this.outListCCBL0.GetItemCheckIndex().Count == 0 || this.outListCCBL1.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请填写输出字段");
                return notReady;
            }
            return !notReady;
        }
        #endregion


        protected override void CreateLine(int addLine)

        {
            this.tableLayoutPanel2.Location = new System.Drawing.Point(62, 0);
            // And OR 选择框
            ComboBox regBox = NewAndORComboBox();
            //regBox.Size = new System.Drawing.Size(85, 26);
            regBox.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tableLayoutPanel1.Controls.Add(regBox, 0, addLine);


            // 左表列下拉框
            ComboBox data0ComboBox = NewColumnsName0ComboBox();
            data0ComboBox.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tableLayoutPanel1.Controls.Add(data0ComboBox, 1, addLine);

            Label label = new Label
            {
                Font = new Font("宋体", 10f, FontStyle.Regular),
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                Text = "等于",
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.tableLayoutPanel1.Controls.Add(label, 2, addLine);
            // 右表列下拉框
            ComboBox data1ComboBox = NewColumnsName1ComboBox();
            data1ComboBox.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tableLayoutPanel1.Controls.Add(data1ComboBox, 3, addLine);
            // 添加行按钮
            Button addButton = NewAddButton(addLine.ToString());
            this.tableLayoutPanel1.Controls.Add(addButton, 4, addLine);
            addButton.BackColor = System.Drawing.SystemColors.Window;
            // 删除行按钮
            Button delButton = NewDelButton(addLine.ToString());
            this.tableLayoutPanel1.Controls.Add(delButton, 5, addLine);
            delButton.BackColor = System.Drawing.SystemColors.Window;
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
            if(this.tableLayoutPanel1.RowCount==1)
                this.tableLayoutPanel2.Location = new System.Drawing.Point(62, 39);

        }

        private void valuePanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
