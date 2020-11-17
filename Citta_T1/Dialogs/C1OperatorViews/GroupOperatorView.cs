using C2.Controls.Move.Op;
using C2.Core;
using C2.Dialogs.Base;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace C2.OperatorViews
{
    public partial class GroupOperatorView : C1BaseOperatorView
    {
        private readonly List<int> groupColumn;
        private List<int> outList;
        public GroupOperatorView(MoveOpControl opControl) : base(opControl)
        {
            this.groupColumn = new List<int>();
            InitializeComponent();
            InitializeComponentManual();
            InitializeDataSource();
            LoadOption();
        }

        // 为了兼顾设计器，一些控件需要手工初始化。
        private void InitializeComponentManual()
        {
            this.button1.Click += new EventHandler(this.Add_Click);
            // 利用Paint方式groupBox附近的虚线留白
            //this.groupBox1.Paint += new PaintEventHandler(this.GroupBox_Paint);
            //this.groupBox2.Paint += new PaintEventHandler(this.GroupBox_Paint);
            //this.groupBox3.Paint += new PaintEventHandler(this.GroupBox_Paint);

            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 28F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(206, 85);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
        }

        #region 初始化配置
        protected override void InitializeDataSource()
        {
            // 初始化左右表数据源配置信息
            base.InitializeDataSource();
            // 窗体自定义的初始化逻辑
            this.comboBox0.Items.AddRange(nowColumnsName0);
        }

        #endregion
        #region 配置信息的保存与加载
        private void LoadOption()
        {

            if (Global.GetOptionDao().IsCleanSingleOperatorOption(this.opControl, this.nowColumnsName0))
                return;

            noRepetition.Checked = Convert.ToBoolean(opControl.Option.GetOption("noRepetition", "False"));
            repetition.Checked = Convert.ToBoolean(opControl.Option.GetOption("repetition", "True"));
            ascendingOrder.Checked  = Convert.ToBoolean(opControl.Option.GetOption("ascendingOrder", "True"));
            descendingOrder.Checked = Convert.ToBoolean(opControl.Option.GetOption("descendingOrder", "False"));
            sortByNum.Checked    = Convert.ToBoolean(opControl.Option.GetOption("sortByNum", "True"));
            sortByString.Checked = Convert.ToBoolean(opControl.Option.GetOption("sortByString", "False"));

            if (!String.IsNullOrEmpty(this.opControl.Option.GetOption("outfield0")))
            {
                this.oldOutList0 = Array.ConvertAll(this.opControl.Option.GetOptionSplit("outfield0"), int.Parse).ToList();
                foreach (int i in this.oldOutList0)
                {
                    if (i >= this.nowColumnsName0.Length || i < 0)
                        continue;
                    this.oldOutName0.Add(this.nowColumnsName0[i]);
                }
                    
            }
            
            string factor1 = this.opControl.Option.GetOption("factor0");
            if (ConvertUtil.IsInt(factor1))
            {
                int index = Convert.ToInt32(factor1);
                if (!OpUtil.IsArrayIndexOutOfBounds(this.comboBox0, index))
                {
                    this.comboBox0.Text = this.comboBox0.Items[index].ToString();
                    this.comboBox0.Tag = index.ToString();
                }                
            }
           
            int count = this.opControl.Option.KeysCount("factor") - 1;
            if (count < 1)
                return;
            InitNewFactorControl(count);
            for (int i = 0; i < count; i++)
            {
                string name = "factor" + (i + 1).ToString();
                if (!ConvertUtil.IsInt(this.opControl.Option.GetOption(name))) continue;
                int num = Convert.ToInt32(this.opControl.Option.GetOption(name));
                Control control1 = this.tableLayoutPanel1.Controls[i * 3 + 0];
                if (!OpUtil.IsArrayIndexOutOfBounds(control1, num))
                {
                    control1.Text = (control1 as ComboBox).Items[num].ToString();
                    control1.Tag = num.ToString();
                }             
            }
        }
        protected override void SaveOption()
        {
            this.opControl.Option.Clear();
            this.opControl.Option.SetOption("columnname0", opControl.FirstDataSourceColumns);
            string factor1 = comboBox0.Tag == null ? comboBox0.SelectedIndex.ToString() : comboBox0.Tag.ToString();
            this.opControl.Option.SetOption("factor0", factor1);
            this.groupColumn.Add(this.comboBox0.SelectedIndex);


            for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
            {
                ComboBox control1 = this.tableLayoutPanel1.GetControlFromPosition(0, i) as ComboBox;
                string factor = control1.Tag == null ? control1.SelectedIndex.ToString() : control1.Tag.ToString();

                this.groupColumn.Add(Convert.ToInt32(factor));
                this.opControl.Option.SetOption("factor" + (i + 1).ToString(), factor);
            }
            this.opControl.Option.SetOption("noRepetition", this.noRepetition.Checked);
            this.opControl.Option.SetOption("repetition", this.repetition.Checked);
            this.opControl.Option.SetOption("ascendingOrder", this.ascendingOrder.Checked);
            this.opControl.Option.SetOption("descendingOrder", this.descendingOrder.Checked);
            this.opControl.Option.SetOption("sortByString", this.sortByString.Checked);
            this.opControl.Option.SetOption("sortByNum", this.sortByNum.Checked);
            this.outList = new List<int>(this.groupColumn);
            int[] columnIndex = Enumerable.Range(0, this.nowColumnsName0.Length).ToArray();
            foreach (int index in columnIndex)
            {
                if (!this.groupColumn.Contains(index))
                    this.outList.Add(index);
            }
            this.opControl.Option.SetOption("outfield0", this.outList);
            foreach (int index in this.outList)
            {
                if (index >= this.nowColumnsName0.Length || index < 0)
                    continue;
                this.selectedColumns.Add(this.nowColumnsName0[index]);
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
                this.comboBox0.GetType().Name
            };
            foreach (Control ctl in this.tableLayoutPanel2.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == "")
                {
                    HelpUtil.ShowMessageBox("请填写分组字段");
                    return notReady;
                }
            }
            foreach (Control ctl in this.tableLayoutPanel1.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == "")
                {
                    HelpUtil.ShowMessageBox("请填写分组字段");
                    return notReady;
                }
            }
            return !notReady;
        }
        #endregion

        protected override void CreateLine(int addLine)
        {
            if (this.tableLayoutPanel1.RowCount == 1)
            {
                this.tableLayoutPanel2.Location = new System.Drawing.Point(1, 2);
            }
   
            // 左表列下拉框
            ComboBox data0ComoboBox = NewColumnsName0ComboBox();
            data0ComoboBox.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tableLayoutPanel1.Controls.Add(data0ComoboBox, 0, addLine);
            // 添加行按钮
            Button addButton = NewAddButton(addLine.ToString());
            this.tableLayoutPanel1.Controls.Add(addButton, 1, addLine);
            addButton.BackColor = System.Drawing.SystemColors.Window;
            // 删除行按钮
            Button delButton = NewDelButton(addLine.ToString());
            this.tableLayoutPanel1.Controls.Add(delButton, 2, addLine);
            delButton.BackColor = System.Drawing.SystemColors.Window;
        }

        protected override void AddTableLayoutPanelControls(int lineNumber)
        {
            for (int k = this.tableLayoutPanel1.RowCount - 2; k >= lineNumber; k--)
            {
                Control ctlNext = this.tableLayoutPanel1.GetControlFromPosition(0, k);
                this.tableLayoutPanel1.SetCellPosition(ctlNext, new TableLayoutPanelCellPosition(0, k + 1));
                Control ctlNext1 = this.tableLayoutPanel1.GetControlFromPosition(1, k);
                ctlNext1.Name = (k + 1).ToString();
                this.tableLayoutPanel1.SetCellPosition(ctlNext1, new TableLayoutPanelCellPosition(1, k + 1));
                Control ctlNext2 = this.tableLayoutPanel1.GetControlFromPosition(2, k);
                ctlNext2.Name = (k + 1).ToString();
                this.tableLayoutPanel1.SetCellPosition(ctlNext2, new TableLayoutPanelCellPosition(2, k + 1));
            }
        }

        protected override void MoveTableLayoutPanelControls(int delLine)
        {
            for (int k = delLine; k < this.tableLayoutPanel1.RowCount - 1; k++)
            {
                Control ctlNext = this.tableLayoutPanel1.GetControlFromPosition(0, k + 1);
                this.tableLayoutPanel1.SetCellPosition(ctlNext, new TableLayoutPanelCellPosition(0, k));
                Control ctlNext1 = this.tableLayoutPanel1.GetControlFromPosition(1, k + 1);
                ctlNext1.Name = k.ToString();
                this.tableLayoutPanel1.SetCellPosition(ctlNext1, new TableLayoutPanelCellPosition(1, k));
                Control ctlNext2 = this.tableLayoutPanel1.GetControlFromPosition(2, k + 1);
                ctlNext2.Name = k.ToString();
                this.tableLayoutPanel1.SetCellPosition(ctlNext2, new TableLayoutPanelCellPosition(2, k));
            }
            if (this.tableLayoutPanel1.RowCount == 1)
                this.tableLayoutPanel2.Location = new System.Drawing.Point(1, 38);
        }

        #region 分组字段重复选择判断
        protected override bool IsDuplicateSelect()
        {
            Dictionary<int, string> selectedIndex = new Dictionary<int, string>();

            string index0 = this.comboBox0.Tag == null ? this.comboBox0.SelectedIndex.ToString() : this.comboBox0.Tag.ToString();
            selectedIndex[0] = index0;

            for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
            {
                Control control1 = this.tableLayoutPanel1.Controls[i * 3 + 0];
                string index1 = (control1 as ComboBox).Tag == null ? (control1 as ComboBox).SelectedIndex.ToString() : (control1 as ComboBox).Tag.ToString();
                selectedIndex[i + 1] = index1;
            }

            //找到所有的“分组字段”，判断是否有完全重复的“分组字段”
            var duplicateValues = selectedIndex.GroupBy(x => x.Value).Where(x => x.Count() > 1);
            List<int> indexs = new List<int>();
            foreach (var item in duplicateValues)
                indexs.Add(Convert.ToInt32(item.Key));


            if (indexs.Count < 1)
                return false;
            string name = String.Empty;
            foreach (int num in indexs)
            {
                if (num >= this.nowColumnsName0.Length || num < 0)
                    continue;
                name += "\"" + this.nowColumnsName0[num] + "\"" + "、";
            }
               
            MessageBox.Show("分组字段" + name.Trim('、') + "重复选择，请保持每个字段只被选择一次");
            return true;


        }
        #endregion

        private void ascendingOrder_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
