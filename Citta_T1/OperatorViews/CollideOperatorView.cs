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
    public partial class CollideOperatorView : BaseOperatorView
    {

        public CollideOperatorView(MoveOpControl opControl) : base(opControl)
        {
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
            this.outListCCBL0.Items.AddRange(nowColumnsName0);
            this.comboBox1.Items.AddRange(nowColumnsName1);
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
            this.selectedColumns = this.outListCCBL0.GetItemCheckText();
            ModelElement resultElement = Global.GetCurrentDocument().SearchResultElementByOpID(this.opControl.ID);
            if (resultElement == ModelElement.Empty)
            {
                MoveRsControlFactory.GetInstance().CreateNewMoveRsControl(this.opControl, this.selectedColumns);
                return;
            }

            // 对应的结果文件置脏
            BCPBuffer.GetInstance().SetDirty(resultElement.FullFilePath);

            //输出变化，重写BCP文件
             Global.GetOptionDao().DoOutputCompare(this.oldOutName0, this.selectedColumns, this.opControl.ID);
        }

        private bool IsOptionReay()
        {
            bool empty = false;
            List<string> types = new List<string>() { this.comboBox0.GetType().Name, this.outListCCBL0.GetType().Name };
            foreach (Control ctl in this.tableLayoutPanel2.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == "")
                {
                    MessageBox.Show("请填写碰撞条件!");
                    empty = true;
                    return empty;
                }
            }
            foreach (Control ctl in this.tableLayoutPanel1.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == "")
                {
                    MessageBox.Show("请填写碰撞条件!");
                    empty = true;
                    return empty;
                }
            }
            if (this.outListCCBL0.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请填写输出字段!");
                empty = true;
                return empty;
            }
            return empty;
        }
        #endregion
        #region 保存加载
        private void InitNewFactorControl(int count)
        {
            for (int line = 0; line < count; line++)
            {
                this.tableLayoutPanel1.RowCount++;
                this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40));
                CreateLine(line);
            }
        }

        private void LoadOption()
        {
            if (!Global.GetOptionDao().IsCleanOption(this.opControl, this.nowColumnsName0, "outfield"))
            {
                string[] checkIndexs = this.opControl.Option.GetOptionSplit("outfield");
                int[] indexs = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                this.oldOutList0 = indexs.ToList();
                this.outListCCBL0.LoadItemCheckIndex(indexs);
                foreach (int index in indexs)
                    this.oldOutName0.Add(this.outListCCBL0.Items[index].ToString());

            }
            int count = this.opControl.Option.KeysCount("factor");
            string factor1 = this.opControl.Option.GetOption("factor1");
            if (factor1 != String.Empty)
            {
                int[] Nums = Array.ConvertAll<string, int>(factor1.Split('\t'), int.Parse);
                bool case0 = Global.GetOptionDao().IsCleanOption(this.opControl, this.nowColumnsName0, "factor1", Nums[0]);
                bool case1 = Global.GetOptionDao().IsCleanOption(this.opControl, this.nowColumnsName1, "factor1", Nums[1]);
                if (!case0 && !case1)
                {
                    this.comboBox0.Text = this.comboBox0.Items[Nums[0]].ToString();
                    this.comboBox1.Text = this.comboBox1.Items[Nums[1]].ToString();
                    this.comboBox0.Tag = Nums[0].ToString();
                    this.comboBox1.Tag = Nums[1].ToString();
                }

            }
            if (count - 1 > 0)
                InitNewFactorControl(count - 1);
            else
            {
                this.opControl.Option.SetOption("columnname0", String.Join("\t", this.opControl.FirstDataSourceColumns));
                this.opControl.Option.SetOption("columnname1", String.Join("\t", this.opControl.SecondDataSourceColumns));
                return;
            }

            for (int i = 2; i < (count + 1); i++)
            {
                string name = "factor" + i.ToString();
                string factor = this.opControl.Option.GetOption(name);
                if (factor == "") continue;

                int[] Nums = Array.ConvertAll<string, int>(factor.Split('\t'), int.Parse);
                bool case0 = Global.GetOptionDao().IsCleanOption(this.opControl, this.nowColumnsName0, name, Nums[1]);
                bool case1 = Global.GetOptionDao().IsCleanOption(this.opControl, this.nowColumnsName1, name, Nums[2]);
                if (case0 || case1) continue;

                Control control1 = (Control)this.tableLayoutPanel1.Controls[(i - 2) * 5 + 0];
                control1.Text = (control1 as ComboBox).Items[Nums[0]].ToString();
                Control control2 = (Control)this.tableLayoutPanel1.Controls[(i - 2) * 5 + 1];
                control2.Text = (control2 as ComboBox).Items[Nums[1]].ToString();
                Control control3 = (Control)this.tableLayoutPanel1.Controls[(i - 2) * 5 + 2];
                control3.Text = (control3 as ComboBox).Items[Nums[2]].ToString();
                control1.Tag = Nums[0].ToString();
                control2.Tag = Nums[1].ToString();
                control3.Tag = Nums[2].ToString();
            }
            this.opControl.Option.SetOption("columnname0", String.Join("\t", this.opControl.FirstDataSourceColumns));
            this.opControl.Option.SetOption("columnname1", String.Join("\t", this.opControl.SecondDataSourceColumns));
        }

        protected override void SaveOption()
        {
            this.opControl.Option.OptionDict.Clear();
            this.opControl.Option.SetOption("columnname0", String.Join("\t", this.opControl.FirstDataSourceColumns));
            this.opControl.Option.SetOption("columnname1", String.Join("\t", this.opControl.SecondDataSourceColumns));

            string outField = string.Join("\t", this.outListCCBL0.GetItemCheckIndex());
            this.opControl.Option.SetOption("outfield", outField);

            string index00 = comboBox0.Tag == null ? comboBox0.SelectedIndex.ToString() : comboBox0.Tag.ToString();
            string index11 = comboBox1.Tag == null ? comboBox1.SelectedIndex.ToString() : comboBox1.Tag.ToString();
            string factor1 = index00 + "\t" + index11;
            this.opControl.Option.SetOption("factor1", factor1);
            if (this.tableLayoutPanel1.RowCount > 0)
            {
                for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
                {
                    Control control1 = (Control)this.tableLayoutPanel1.Controls[i * 5 + 0];
                    Control control2 = (Control)this.tableLayoutPanel1.Controls[i * 5 + 1];
                    Control control3 = (Control)this.tableLayoutPanel1.Controls[i * 5 + 2];
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


        private void CreateLine(int addLine)
        {
            // 添加控件
            ComboBox regBox = new ComboBox();
            regBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            regBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            regBox.Anchor = AnchorStyles.None;
            regBox.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            regBox.Items.AddRange(new object[] {
            "AND",
            "OR"});
            regBox.Leave += new System.EventHandler(optionInfoCheck.Control_Leave);
            regBox.KeyUp += new System.Windows.Forms.KeyEventHandler(optionInfoCheck.Control_KeyUp);
            regBox.SelectionChangeCommitted += new System.EventHandler(Global.GetOptionDao().GetSelectedItemIndex);
            this.tableLayoutPanel1.Controls.Add(regBox, 0, addLine);


            ComboBox dataBox = new ComboBox();
            dataBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            dataBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            dataBox.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            dataBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dataBox.Items.AddRange(this.nowColumnsName0);
            dataBox.Leave += new System.EventHandler(optionInfoCheck.Control_Leave);
            dataBox.KeyUp += new System.Windows.Forms.KeyEventHandler(optionInfoCheck.Control_KeyUp);
            dataBox.SelectionChangeCommitted += new System.EventHandler(Global.GetOptionDao().GetSelectedItemIndex);
            this.tableLayoutPanel1.Controls.Add(dataBox, 1, addLine);

            ComboBox filterBox = new ComboBox();
            filterBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            filterBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            filterBox.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            filterBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            filterBox.Items.AddRange(this.nowColumnsName1);
            filterBox.Leave += new System.EventHandler(optionInfoCheck.Control_Leave);
            filterBox.KeyUp += new System.Windows.Forms.KeyEventHandler(optionInfoCheck.Control_KeyUp);
            filterBox.SelectionChangeCommitted += new System.EventHandler(Global.GetOptionDao().GetSelectedItemIndex);
            this.tableLayoutPanel1.Controls.Add(filterBox, 2, addLine);

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
            this.tableLayoutPanel1.Controls.Add(addButton1, 3, addLine);

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
            delButton1.Click += new System.EventHandler(this.Delete_Click);
            delButton1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            delButton1.Name = addLine.ToString();
            this.tableLayoutPanel1.Controls.Add(delButton1, 4, addLine);
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
                    ctlNext3.Name = (k + 1).ToString();
                    this.tableLayoutPanel1.SetCellPosition(ctlNext3, new TableLayoutPanelCellPosition(3, k + 1));
                    Control ctlNext4 = this.tableLayoutPanel1.GetControlFromPosition(4, k);
                    ctlNext4.Name = (k + 1).ToString();
                    this.tableLayoutPanel1.SetCellPosition(ctlNext4, new TableLayoutPanelCellPosition(4, k + 1));
                }
                CreateLine(addLine);
            }

        }

        private void Delete_Click(object sender, EventArgs e)
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
            this.tableLayoutPanel1.RowCount = this.tableLayoutPanel1.RowCount - 1;

            this.tableLayoutPanel1.Height = this.tableLayoutPanel1.RowCount * 40;

        }
    }
}
