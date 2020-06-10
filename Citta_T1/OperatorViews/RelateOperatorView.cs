using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
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
    public partial class RelateOperatorView : BaseOperatorView
    {

        public RelateOperatorView(MoveOpControl opControl) : base(opControl)
        {
            InitializeComponent();
            InitByDataSource();
            LoadOption();
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
            if (Global.GetOptionDao().IsCleanBinaryOperatorOption(this.opControl, this.nowColumnsName0, this.nowColumnsName1))
                return;

            string[] checkIndexs0 = this.opControl.Option.GetOptionSplit("outfield0");
            int[] indexs0 = Array.ConvertAll<string, int>(checkIndexs0, int.Parse);
            this.oldOutList0 = indexs0.ToList();
            this.outListCCBL0.LoadItemCheckIndex(indexs0);
            foreach (int index in indexs0)
                this.oldOutName0.Add(this.outListCCBL0.Items[index].ToString());


            string[] checkIndexs1 = this.opControl.Option.GetOptionSplit("outfield1");
            int[] indexs1 = Array.ConvertAll<string, int>(checkIndexs1, int.Parse);
            this.oldOutList1 = indexs1.ToList();
            this.outListCCBL1.LoadItemCheckIndex(indexs1);
            foreach (int index in indexs1)
                this.oldOutName1.Add(this.outListCCBL1.Items[index].ToString());

            int count = this.opControl.Option.KeysCount("factor");
            string factor1 = this.opControl.Option.GetOption("factor1");

            int[] itemsList0 = Array.ConvertAll<string, int>(factor1.Split('\t'), int.Parse);
            this.comboBox0.Text = this.comboBox0.Items[itemsList0[0]].ToString();
            this.comboBox1.Text = this.comboBox1.Items[itemsList0[1]].ToString();
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

                int[] itemsList1 = Array.ConvertAll<string, int>(factor.Split('\t'), int.Parse);              

                Control control1 = (Control)this.tableLayoutPanel1.Controls[(i - 2) * 6 + 0];
                control1.Text = (control1 as ComboBox).Items[itemsList1[0]].ToString();
                Control control2 = (Control)this.tableLayoutPanel1.Controls[(i - 2) * 6 + 1];
                control2.Text = (control2 as ComboBox).Items[itemsList1[1]].ToString();
                Control control3 = (Control)this.tableLayoutPanel1.Controls[(i - 2) * 6 + 3];
                control3.Text = (control3 as ComboBox).Items[itemsList1[2]].ToString();
                control1.Tag = itemsList1[0].ToString();
                control2.Tag = itemsList1[1].ToString();
                control3.Tag = itemsList1[2].ToString();
            }          
        }
        protected override void SaveOption()
        {
            this.opControl.Option.OptionDict.Clear();
            this.opControl.Option.SetOption("columnname0", String.Join("\t", this.opControl.FirstDataSourceColumns));
            this.opControl.Option.SetOption("columnname1", String.Join("\t", this.opControl.SecondDataSourceColumns));

            string outField0 = string.Join("\t", this.outListCCBL0.GetItemCheckIndex());
            this.opControl.Option.SetOption("outfield0", outField0);

            string outField1 = string.Join("\t", this.outListCCBL1.GetItemCheckIndex());
            this.opControl.Option.SetOption("outfield1", outField1);

            string index01 = this.comboBox0.Tag == null ? this.comboBox0.SelectedIndex.ToString() : this.comboBox0.Tag.ToString();
            string index02 = this.comboBox1.Tag == null ? this.comboBox1.SelectedIndex.ToString() : this.comboBox1.Tag.ToString();
            string factor1 = index01 + "\t" + index02;
            this.opControl.Option.SetOption("factor1", factor1);
            if (this.tableLayoutPanel1.RowCount > 0)
            {
                for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
                {
                    Control control1 = (Control)this.tableLayoutPanel1.Controls[i * 6 + 0];
                    Control control2 = (Control)this.tableLayoutPanel1.Controls[i * 6 + 1];
                    Control control3 = (Control)this.tableLayoutPanel1.Controls[i * 6 + 3];
                    string index1 = (control1 as ComboBox).Tag == null ? (control1 as ComboBox).SelectedIndex.ToString() : (control1 as ComboBox).Tag.ToString();
                    string index2 = (control2 as ComboBox).Tag == null ? (control2 as ComboBox).SelectedIndex.ToString() : (control2 as ComboBox).Tag.ToString();
                    string index3 = (control3 as ComboBox).Tag == null ? (control3 as ComboBox).SelectedIndex.ToString() : (control3 as ComboBox).Tag.ToString();
                    string factor = index1 + "\t" + index2 + "\t" + index3;

                    this.opControl.Option.SetOption("factor" + (i + 2).ToString(), factor);
                }
            }

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
            SaveOption();
            this.DialogResult = DialogResult.OK;
            //内容修改，引起文档dirty
            if (this.oldOptionDictStr != this.opControl.Option.ToString())
                Global.GetMainForm().SetDocumentDirty();
            //生成结果控件,创建relation,bcp结果文件

            ModelElement resultElement = Global.GetCurrentDocument().SearchResultElementByOpID(this.opControl.ID);
            this.selectedColumns = this.outListCCBL0.GetItemCheckText().Concat(this.outListCCBL1.GetItemCheckText()).ToList();
            if (resultElement == ModelElement.Empty)
            {
                MoveRsControlFactory.GetInstance().CreateNewMoveRsControl(this.opControl, this.selectedColumns);
                return;
            }

            // 对应的结果文件置脏
            BCPBuffer.GetInstance().SetDirty(resultElement.FullFilePath);

            //输出变化，重写BCP文件
            List<string> oldName = this.oldOutName0.Concat(this.oldOutName1).ToList();
            Global.GetOptionDao().DoOutputCompare(oldName, this.selectedColumns, this.opControl.ID);


        }
        private bool IsOptionReay()
        {
            bool empty = false;
            List<string> types = new List<string>();
            types.Add(this.comboBox0.GetType().Name);
            types.Add(this.outListCCBL0.GetType().Name);
            foreach (Control ctl in this.tableLayoutPanel2.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == String.Empty)
                {
                    MessageBox.Show("请填写连接条件");
                    empty = true;
                    return empty;
                }
            }
            foreach (Control ctl in this.tableLayoutPanel1.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == String.Empty)
                {
                    MessageBox.Show("请填写连接条件");
                    empty = true;
                    return empty;
                }
            }
            if (this.outListCCBL0.GetItemCheckIndex().Count == 0 || this.outListCCBL1.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请填写输出字段");
                empty = true;
                return empty;
            }
            return empty;
        }
        #endregion


        private void CreateLine(int addLine)
        {
            // 添加控件
            ComboBox regBox = new ComboBox();
            regBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            regBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            regBox.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            regBox.Anchor = AnchorStyles.None;
            regBox.Items.AddRange(new object[] {
            "AND",
            "OR"});
            regBox.Leave += new EventHandler(this.Control_Leave);
            regBox.KeyUp += new KeyEventHandler(this.Control_KeyUp);
            regBox.SelectionChangeCommitted += new EventHandler(this.GetSelectedItemIndex);
            this.tableLayoutPanel1.Controls.Add(regBox, 0, addLine);

            ComboBox dataBox = new ComboBox();
            dataBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            dataBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            dataBox.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            dataBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dataBox.Items.AddRange(this.nowColumnsName0);
            dataBox.Leave += new EventHandler(this.Control_Leave);
            dataBox.KeyUp += new KeyEventHandler(this.Control_KeyUp);
            dataBox.SelectionChangeCommitted += new EventHandler(this.GetSelectedItemIndex);
            this.tableLayoutPanel1.Controls.Add(dataBox, 1, addLine);

            Label label = new Label
            {
                Font = new Font("微软雅黑", 8f, FontStyle.Regular),
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                Text = "等于=",
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            this.tableLayoutPanel1.Controls.Add(label, 2, addLine);

            ComboBox data2box = new ComboBox();
            data2box.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            data2box.AutoCompleteSource = AutoCompleteSource.ListItems;
            data2box.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            data2box.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            data2box.Items.AddRange(this.nowColumnsName1);
            data2box.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            data2box.Leave += new System.EventHandler(this.Control_Leave);
            data2box.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Control_KeyUp);
            data2box.SelectionChangeCommitted += new System.EventHandler(this.GetSelectedItemIndex);
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
            addButton1.Click += new System.EventHandler(this.Add_Click);
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
            delButton1.Click += new System.EventHandler(this.Del_Click);
            delButton1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            delButton1.Name = addLine.ToString();
            this.tableLayoutPanel1.Controls.Add(delButton1, 5, addLine);
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
                CreateLine(addLine);
            }

        }

        private void Del_Click(object sender, EventArgs e)
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
            this.tableLayoutPanel1.RowCount -= 1;

            this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;

        }
    }
}
