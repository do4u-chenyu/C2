﻿using C2.Controls.Move.Op;
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
    public partial class UnionOperatorView : C1BaseOperatorView
    {

        public UnionOperatorView(MoveOpControl opControl) : base(opControl)
        {
            InitializeComponent();
            InitializeComponentManual();
            InitializeDataSource();
            LoadOption();


        }

        // 为了兼顾设计器，一些控件需要手工初始化。
        private void InitializeComponentManual()
        {
            this.textBox0.Enter += new EventHandler(this.AliasTextBox_Enter);
            this.textBox0.Leave += new EventHandler(this.AliasTextBox_Leave);
            this.button1.Click += new EventHandler(this.Add_Click);
            // 利用Paint方式groupBox1附近的虚线留白
            //this.groupBox1.Paint += new PaintEventHandler(this.GroupBox_Paint);

            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 28F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(446, 84);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
        }

        #region 初始化配置
        protected override void InitializeDataSource()
        {
            // 初始化左右表数据源配置信息
            base.InitializeDataSource();
            // 窗体自定义的初始化逻辑
            this.oldOutName0 = this.opControl.Option.GetOptionSplit("outname").ToList();
            this.comboBox0.Items.AddRange(nowColumnsName0);
            this.comboBox1.Items.AddRange(nowColumnsName1);
        }
        #endregion
        #region 配置信息的保存与加载
        protected override void SaveOption()
        {         
            this.opControl.Option.Clear();
            this.opControl.Option.SetOption("columnname0", opControl.FirstDataSourceColumns);
            this.opControl.Option.SetOption("columnname1", opControl.SecondDataSourceColumns);
            string index01 = this.comboBox0.Tag == null ? this.comboBox0.SelectedIndex.ToString() : this.comboBox0.Tag.ToString();
            string index02 = this.comboBox1.Tag == null ? this.comboBox1.SelectedIndex.ToString() : this.comboBox1.Tag.ToString();
            string factor1 = index01 + "\t" + index02 + "\t" + this.textBox0.Text;
            this.opControl.Option.SetOption("factor0", factor1);
            this.selectedColumns.Add(OutColumnName(this.comboBox0.Text, this.textBox0.Text));
            for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
            {
                ComboBox control1 = this.tableLayoutPanel1.GetControlFromPosition(0, i) as ComboBox;
                ComboBox control2 = this.tableLayoutPanel1.GetControlFromPosition(1, i) as ComboBox;
                Control control3 = this.tableLayoutPanel1.GetControlFromPosition(2, i);
                string index1 = control1.Tag == null ? control1.SelectedIndex.ToString() : control1.Tag.ToString();
                string index2 = control2.Tag == null ? control2.SelectedIndex.ToString() : control2.Tag.ToString();

                string factor = index1 + "\t" + index2 + "\t" + control3.Text;
                this.opControl.Option.SetOption("factor" + (i + 1).ToString(), factor);
                this.selectedColumns.Add(OutColumnName((control1 as ComboBox).Text, control3.Text));
            }
            this.opControl.Option.SetOption("outname", this.selectedColumns);
            this.opControl.Option.SetOption("noRepetition", this.noRepetition.Checked);
            this.opControl.Option.SetOption("repetition", this.repetition.Checked);

            //更新子图所有节点状态
            UpdateSubGraphStatus();
        }
        private string OutColumnName(string name, string alias)
        {
            return alias == "别名" ? name : alias;
        }
        private void LoadOption()
        {
            if (Global.GetOptionDao().IsCleanBinaryOperatorOption(this.opControl, this.nowColumnsName0, this.nowColumnsName1))
                return;
      
            this.noRepetition.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("noRepetition", "True"));
            this.repetition.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("repetition","False"));

            string[] factorList0 = this.opControl.Option.GetOptionSplit("factor0");
            if (factorList0.Length > 2)
            {

                int[] itemsList0 = Array.ConvertAll(factorList0.Take(factorList0.Length - 1).ToArray(), int.Parse);
                this.textBox0.Text = factorList0[2];

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
                if (factorList1.Length < 3) continue;

                int[] itemsList1 = Array.ConvertAll(factorList1.Take(factorList1.Length - 1).ToArray(), int.Parse);
                Control control1 = this.tableLayoutPanel1.Controls[i * 5 + 0];
                Control control2 = this.tableLayoutPanel1.Controls[i * 5 + 1];
                Control control3 = this.tableLayoutPanel1.Controls[i * 5 + 2];
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
                 control3.Text = factorList1[2];


            }
         
        }


        #endregion
        #region 判断是否配置完毕
        protected override bool IsDuplicateSelect()
        {
            string index01 = this.comboBox0.Tag == null ? this.comboBox0.SelectedIndex.ToString() : this.comboBox0.Tag.ToString();
            string index02 = this.comboBox1.Tag == null ? this.comboBox1.SelectedIndex.ToString() : this.comboBox1.Tag.ToString();
            string factor1 = index01 + "," + index02 + "," + this.textBox0.Text;
            Dictionary<string, string> factors = new Dictionary<string, string>
            {
                ["factor0"] = factor1
            };
            for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
            {
                ComboBox control1 = (ComboBox)this.tableLayoutPanel1.Controls[i * 5 + 0];
                ComboBox control2 = (ComboBox)this.tableLayoutPanel1.Controls[i * 5 + 1];
                Control control3 = (Control)this.tableLayoutPanel1.Controls[i * 5 + 2];
                string index1 = control1.Tag == null ? control1.SelectedIndex.ToString() : control1.Tag.ToString();
                string index2 = control2.Tag == null ? control2.SelectedIndex.ToString() : control2.Tag.ToString();
                string factor = index1 + "," + index2 + "," + control3.Text;
                factors["factor" + (i + 1).ToString()] = factor;

            }

            //找到所有的“取并条件”，判断是否有完全重复的“取并条件”
            var duplicateValues = factors.Where(x => x.Key.Contains("factor")).GroupBy(x => x.Value).Where(x => x.Count() > 1);
            foreach (var item in duplicateValues)
            {
                HelpUtil.ShowMessageBox("取并集条件存在完全重复选项,请重新选择并集条件");
                return true;
            }
            return false;
        }
        protected override bool IsOptionNotReady()
        {
            bool notReady = true;
            List<string> types = new List<string>
            {
                this.comboBox0.GetType().Name,
                this.textBox0.GetType().Name
            };
            foreach (Control ctl in this.tableLayoutPanel2.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && String.IsNullOrEmpty(ctl.Text))
                {
                    HelpUtil.ShowMessageBox("请填写并集条件");
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
                    HelpUtil.ShowMessageBox("请填写并集条件");
                    return notReady;
                }
                if (ctl is TextBox && IsIllegalCharacter(ctl))
                {
                    HelpUtil.ShowMessageBox("字段名中包含不合法字符TAB，请重新输入");
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
                this.tableLayoutPanel2.Location = new System.Drawing.Point(1, 1);
            }
            
            // 左表列下拉框
            ComboBox data0ComboBox = NewColumnsName0ComboBox();
            data0ComboBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tableLayoutPanel1.Controls.Add(data0ComboBox, 0, addLine);
            // 右表列下拉框
            ComboBox data1ComboBox = NewColumnsName1ComboBox();
            data1ComboBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tableLayoutPanel1.Controls.Add(data1ComboBox, 1, addLine);
            // 别名文本框
            TextBox textBox = NewAliasTextBox();
            textBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
                //Control ctlNext5 = this.tableLayoutPanel1.GetControlFromPosition(5, k + 1);
                //ctlNext5.Name = k.ToString();
                //this.tableLayoutPanel1.SetCellPosition(ctlNext5, new TableLayoutPanelCellPosition(5, k));
            }
            if (this.tableLayoutPanel1.RowCount == 1)
                this.tableLayoutPanel2.Location = new System.Drawing.Point(1, 41);

        }
    }
}
