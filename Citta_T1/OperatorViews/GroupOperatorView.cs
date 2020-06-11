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
    public partial class GroupOperatorView : BaseOperatorView
    {
        private List<int> groupColumn;
        private List<int> outList;
        public GroupOperatorView(MoveOpControl opControl) : base(opControl)
        {
            this.groupColumn = new List<int>();

            InitializeComponent();
            InitByDataSource();
            LoadOption();
        }
        #region 初始化配置
        private void InitByDataSource()
        {
            // 初始化左右表数据源配置信息
            this.InitDataSource();
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

            this.oldOutList0 = Array.ConvertAll(this.opControl.Option.GetOptionSplit("outfield"), int.Parse).ToList();
            foreach (int i in this.oldOutList0)
                this.oldOutName0.Add(this.nowColumnsName0[i]);


            string factor1 = this.opControl.Option.GetOption("factor1");
            int index = Convert.ToInt32(factor1);
            this.comboBox0.Text = this.comboBox0.Items[index].ToString();
            this.comboBox0.Tag = index.ToString();


            int count = this.opControl.Option.KeysCount("factor");
            if (count <= 1)
                return;
            InitNewFactorControl(count - 1);
            for (int i = 2; i < (count + 1); i++)
            {
                string name = "factor" + i.ToString();
                int num = Convert.ToInt32(this.opControl.Option.GetOption(name));
                Control control1 = this.tableLayoutPanel1.Controls[(i - 2) * 3 + 0];
                control1.Text = (control1 as ComboBox).Items[num].ToString();
                control1.Tag = num.ToString();
            }

        }
        protected override void SaveOption()
        {
            this.opControl.Option.OptionDict.Clear();
            this.opControl.Option.SetOption("columnname0", opControl.FirstDataSourceColumns);
            string factor1 = comboBox0.Tag == null ? comboBox0.SelectedIndex.ToString() : comboBox0.Tag.ToString();
            this.opControl.Option.SetOption("factor1", factor1);
            this.groupColumn.Add(this.comboBox0.SelectedIndex);


            if (this.tableLayoutPanel1.RowCount > 0)
            {
                for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
                {
                    ComboBox control1 = this.tableLayoutPanel1.Controls[i * 3 + 0] as ComboBox;
                    string factor = control1.Tag == null ? control1.SelectedIndex.ToString() : control1.Tag.ToString();
                    this.groupColumn.Add(Convert.ToInt32(factor));
                    this.opControl.Option.SetOption("factor" + (i + 2).ToString(), factor);
                }
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
            this.opControl.Option.SetOption("outfield", this.outList);
            foreach (int index in this.outList)
                this.selectedColumns.Add(this.nowColumnsName0[index]);

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
                    MessageBox.Show("请填写过滤条件!");
                    return notReady;
                }
            }
            foreach (Control ctl in this.tableLayoutPanel1.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == "")
                {
                    MessageBox.Show("请填写过滤条件!");
                    return notReady;
                }
            }
            return !notReady;
        }
        #endregion

        protected override void CreateLine(int addLine)
        {
            // 添加控件
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
            this.tableLayoutPanel1.Controls.Add(dataBox, 0, addLine);


            Button addButton = NewAddButton(addLine.ToString());
            addButton.Click += new EventHandler(this.Add_Click);
            this.tableLayoutPanel1.Controls.Add(addButton, 1, addLine);

            Button delButton = NewDelButton(addLine.ToString());
            delButton.Click += new EventHandler(this.Del_Click);
            this.tableLayoutPanel1.Controls.Add(delButton, 2, addLine);
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
                    ctlNext1.Name = (k + 1).ToString();
                    this.tableLayoutPanel1.SetCellPosition(ctlNext1, new TableLayoutPanelCellPosition(1, k + 1));
                    Control ctlNext2 = this.tableLayoutPanel1.GetControlFromPosition(2, k);
                    ctlNext2.Name = (k + 1).ToString();
                    this.tableLayoutPanel1.SetCellPosition(ctlNext2, new TableLayoutPanelCellPosition(2, k + 1));
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
                Control bt1 = this.tableLayoutPanel1.Controls[(i * 3) + 2];
                if (bt1.Name == tmp.Name)
                {
                    for (int j = (i * 3) + 2; j >= (i * 3); j--)
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
                ctlNext1.Name = k.ToString();
                this.tableLayoutPanel1.SetCellPosition(ctlNext1, new TableLayoutPanelCellPosition(1, k));
                Control ctlNext2 = this.tableLayoutPanel1.GetControlFromPosition(2, k + 1);
                ctlNext2.Name = k.ToString();
                this.tableLayoutPanel1.SetCellPosition(ctlNext2, new TableLayoutPanelCellPosition(2, k));
            }
            this.tableLayoutPanel1.RowStyles.RemoveAt(this.tableLayoutPanel1.RowCount - 1);
            this.tableLayoutPanel1.RowCount -= 1;

            this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;

        }

        #region 分组字段重复选择判断
        protected override bool IsDuplicateSelect()
        {
            bool ret = false;
            Dictionary<int, string> selectedIndex = new Dictionary<int, string>();

            string index0 = this.comboBox0.Tag == null ? this.comboBox0.SelectedIndex.ToString() : this.comboBox0.Tag.ToString();
            selectedIndex[0] = index0;

            if (this.tableLayoutPanel1.RowCount > 0)
            {
                for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
                {
                    Control control1 = this.tableLayoutPanel1.Controls[i * 3 + 0];
                    string index1 = (control1 as ComboBox).Tag == null ? (control1 as ComboBox).SelectedIndex.ToString() : (control1 as ComboBox).Tag.ToString();
                    selectedIndex[i + 1] = index1;
                }
            }
            var duplicateValues = selectedIndex.GroupBy(x => x.Value).Where(x => x.Count() > 1);
            List<int> indexs = new List<int>();
            foreach (var item in duplicateValues)
                indexs.Add(Convert.ToInt32(item.Key));
            if (indexs != null && indexs.Count() > 0)
            {
                string name = "";
                foreach (int num in indexs)
                    name += this.nowColumnsName0[num];
                MessageBox.Show("分组字段" + name + "重复选择，请保持每个字段只被选择一次");
                ret = true;
            }
            return ret;
        }
        #endregion
    }
}
