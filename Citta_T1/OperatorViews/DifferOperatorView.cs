using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.OperatorViews.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{
    public partial class DifferOperatorView : BaseOperatorView
    {

        public DifferOperatorView(MoveOpControl opControl) : base(opControl)
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

            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 38F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 38F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(345, 84);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
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

        #region 配置信息的保存与加载

        private void LoadOption()
        {
            if (Global.GetOptionDao().IsCleanBinaryOperatorOption(this.opControl, this.nowColumnsName0, this.nowColumnsName1))
                return;
            if (!String.IsNullOrEmpty(this.opControl.Option.GetOption("outfield0")))
            {
                string[] checkIndexs = this.opControl.Option.GetOptionSplit("outfield0");
                int[] indexs = Array.ConvertAll(checkIndexs, int.Parse);
                this.oldOutList0 = indexs.ToList();
                this.outListCCBL0.LoadItemCheckIndex(indexs);
                foreach (int index in indexs)
                    this.oldOutName0.Add(this.outListCCBL0.Items[index].ToString());
            }



            string factor1 = this.opControl.Option.GetOption("factor0");
            if (!String.IsNullOrEmpty(factor1))
            {
                int[] factorList0 = Array.ConvertAll(factor1.Split('\t'), int.Parse);
                this.comboBox0.Text = this.comboBox0.Items[factorList0[0]].ToString();
                this.comboBox1.Text = this.comboBox1.Items[factorList0[1]].ToString();
                this.comboBox0.Tag = factorList0[0].ToString();
                this.comboBox1.Tag = factorList0[1].ToString();
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

                int[] factorList1 = Array.ConvertAll<string, int>(factor.Split('\t'), int.Parse);               
                Control control1 = this.tableLayoutPanel1.Controls[i * 5 + 0];
                Control control2 = this.tableLayoutPanel1.Controls[i * 5 + 1];
                Control control3 = this.tableLayoutPanel1.Controls[i * 5 + 2];
                control1.Text = (control1 as ComboBox).Items[factorList1[0]].ToString();
                control2.Text = (control2 as ComboBox).Items[factorList1[1]].ToString();
                control3.Text = (control3 as ComboBox).Items[factorList1[2]].ToString();
                control1.Tag = factorList1[0].ToString();
                control2.Tag = factorList1[1].ToString();
                control3.Tag = factorList1[2].ToString();
            }
        }
        protected override void SaveOption()
        {
            this.opControl.Option.Clear();

            this.opControl.Option.SetOption("columnname0", opControl.FirstDataSourceColumns);
            this.opControl.Option.SetOption("columnname1", opControl.SecondDataSourceColumns);
            this.opControl.Option.SetOption("outfield0", outListCCBL0.GetItemCheckIndex());
            this.selectedColumns = this.outListCCBL0.GetItemCheckText();

            string index00 = comboBox0.Tag == null ? comboBox0.SelectedIndex.ToString() : comboBox0.Tag.ToString();
            string index11 = comboBox1.Tag == null ? comboBox1.SelectedIndex.ToString() : comboBox1.Tag.ToString();
            string factor1 = index00 + "\t" + index11;
            this.opControl.Option.SetOption("factor0", factor1);
            for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
            {
                ComboBox control1 = this.tableLayoutPanel1.GetControlFromPosition(0, i) as ComboBox;
                ComboBox control2 = this.tableLayoutPanel1.GetControlFromPosition(1, i) as ComboBox;
                ComboBox control3 = this.tableLayoutPanel1.GetControlFromPosition(2, i) as ComboBox;
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
            List<string> types = new List<string>() { this.comboBox0.GetType().Name, this.outListCCBL0.GetType().Name };
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
            if (this.outListCCBL0.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请填写输出字段!");
                return notReady;
            }
            return !notReady;
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
            // 右表列下拉框
            ComboBox data2Box = NewColumnsName1ComboBox();
            this.tableLayoutPanel1.Controls.Add(data2Box, 2, addLine);
            // 添加行按钮
            Button addButton = NewAddButton(addLine.ToString());
            this.tableLayoutPanel1.Controls.Add(addButton, 3, addLine);
            // 删除行按钮
            Button delButton = NewDelButton(addLine.ToString());
            this.tableLayoutPanel1.Controls.Add(delButton, 4, addLine);
        }
    }
}
