using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
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
        private void InitNewFactorControl(int count)
        {
            for (int line = 0; line < count; line++)
            {
                this.tableLayoutPanel1.RowCount++;
                this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;
                this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
                CreateLine(line);
            }
        }

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

            this.oldOutList0 = Array.ConvertAll<string, int>(this.opControl.Option.GetOptionSplit("outfield"), int.Parse).ToList();
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
                Control control1 = (Control)this.tableLayoutPanel1.Controls[(i - 2) * 3 + 0];
                control1.Text = (control1 as ComboBox).Items[num].ToString();
                control1.Tag = num.ToString();
            }

        }
        protected override void SaveOption()
        {
            this.opControl.Option.OptionDict.Clear();
            this.opControl.Option.SetOption("columnname0", String.Join("\t", this.opControl.FirstDataSourceColumns));
            string factor1 = comboBox0.Tag == null ? comboBox0.SelectedIndex.ToString() : comboBox0.Tag.ToString();
            this.opControl.Option.SetOption("factor1", factor1);
            this.groupColumn.Add(this.comboBox0.SelectedIndex);

            if (this.tableLayoutPanel1.RowCount > 0)
            {
                for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
                {
                    Control control1 = (Control)this.tableLayoutPanel1.Controls[i * 3 + 0];
                    string factor = (control1 as ComboBox).Tag == null ? (control1 as ComboBox).SelectedIndex.ToString() : (control1 as ComboBox).Tag.ToString();
                    this.groupColumn.Add(Convert.ToInt32(factor));
                    this.opControl.Option.SetOption("factor" + (i + 2).ToString(), factor);
                }
            }
            this.opControl.Option.SetOption("noRepetition", this.noRepetition.Checked.ToString());
            this.opControl.Option.SetOption("repetition", this.repetition.Checked.ToString());
            this.opControl.Option.SetOption("ascendingOrder", this.ascendingOrder.Checked.ToString());
            this.opControl.Option.SetOption("descendingOrder", this.descendingOrder.Checked.ToString());
            this.opControl.Option.SetOption("sortByString", this.sortByString.Checked.ToString());
            this.opControl.Option.SetOption("sortByNum", this.sortByNum.Checked.ToString());
            this.outList = new List<int>(this.groupColumn);
            int[] columnIndex = Enumerable.Range(0, this.nowColumnsName0.Length).ToArray();
            foreach (int index in columnIndex)
            {
                if (!this.groupColumn.Contains(index))
                    this.outList.Add(index);
            }
            this.opControl.Option.SetOption("outfield", string.Join("\t", this.outList));

            ElementStatus oldStatus = this.opControl.Status;

            if (this.oldOptionDictStr != this.opControl.Option.ToString())
                this.opControl.Status = ElementStatus.Ready;

            if (oldStatus == ElementStatus.Done && this.opControl.Status == ElementStatus.Ready)
                Global.GetCurrentDocument().DegradeChildrenStatus(this.opControl.ID);

        }
        #endregion
        #region 添加取消
        protected override void ConfirmButton_Click(object sender, EventArgs e)
        {

            bool empty = IsOptionReay();
            if (empty) return;
            //判断分组字段是否重复选择
            if (IsDuplicateSelect()) return;
            SaveOption();
            this.DialogResult = DialogResult.OK;
            //内容修改，引起文档dirty
            if (this.oldOptionDictStr != this.opControl.Option.ToString())
                Global.GetMainForm().SetDocumentDirty();
            //生成结果控件,创建relation,bcp结果文件
            foreach (int index in this.outList)
                this.selectedColumns.Add(this.nowColumnsName0[index]);
            ModelElement resultElement = Global.GetCurrentDocument().SearchResultElementByOpID(this.opControl.ID);
            if (resultElement == ModelElement.Empty)
            {
                MoveRsControlFactory.GetInstance().CreateNewMoveRsControl(this.opControl, this.selectedColumns);
                return;
            }

            // 对应的结果文件置脏
            BCPBuffer.GetInstance().SetDirty(resultElement.FullFilePath);
            //输出变化，重写BCP文件
            if (!this.oldOutName0.SequenceEqual(this.selectedColumns))
                Global.GetOptionDao().DoOutputCompare(this.oldOutName0, this.selectedColumns, this.opControl.ID);
        }

        private bool IsOptionReay()
        {
            bool empty = false;
            List<string> types = new List<string>();
            types.Add(this.comboBox0.GetType().Name);
            foreach (Control ctl in this.tableLayoutPanel2.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == "")
                {
                    MessageBox.Show("请填写过滤条件!");
                    empty = true;
                    return empty;
                }
            }
            foreach (Control ctl in this.tableLayoutPanel1.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == "")
                {
                    MessageBox.Show("请填写过滤条件!");
                    empty = true;
                    return empty;
                }
            }
            return empty;
        }
        #endregion

        private void CreateLine(int addLine)
        {
            // 添加控件
            ComboBox dataBox = new ComboBox();
            dataBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            dataBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            dataBox.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            dataBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dataBox.Items.AddRange(this.nowColumnsName0);
            dataBox.Leave += new System.EventHandler(this.Control_Leave);
            dataBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Control_KeyUp);
            dataBox.SelectionChangeCommitted += new System.EventHandler(this.GetSelectedItemIndex);
            this.tableLayoutPanel1.Controls.Add(dataBox, 0, addLine);


            Button addButton1 = new Button();
            addButton1.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            addButton1.FlatAppearance.BorderSize = 0;
            addButton1.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            addButton1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            addButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            addButton1.BackColor = System.Drawing.SystemColors.Control;
            addButton1.BackgroundImage = global::Citta_T1.Properties.Resources.add;
            addButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            addButton1.Click += new System.EventHandler(this.Add_Click);
            addButton1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            addButton1.Name = addLine.ToString();
            addButton1.UseVisualStyleBackColor = true;
            this.tableLayoutPanel1.Controls.Add(addButton1, 1, addLine);

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
            delButton1.Click += new System.EventHandler(this.Del_Click);
            delButton1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            delButton1.Name = addLine.ToString();
            this.tableLayoutPanel1.Controls.Add(delButton1, 2, addLine);
        }

        private void Add_Click(object sender, EventArgs e)
        {
            Button tmp = (Button)sender;
            int addLine;
            if (this.tableLayoutPanel1.RowCount == 0)
            {
                this.tableLayoutPanel1.RowCount++;
                this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40));
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

                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40));
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
            this.tableLayoutPanel1.RowCount = this.tableLayoutPanel1.RowCount - 1;

            this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;

        }
        private void GroupBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }

        private void GroupBox2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
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
                    Control control1 = (Control)this.tableLayoutPanel1.Controls[i * 3 + 0];
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

        private void GroupBox3_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }
    }
}
