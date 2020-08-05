using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.OperatorViews.Base;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{
    public partial class CollideOperatorView : BaseOperatorView
    {

        public CollideOperatorView(MoveOpControl opControl) : base(opControl)
        {
            InitializeComponent();
            InitializeComponentManual(); // 设计器不支持复用基类中的tablelayoutpanel,需要手工初始化。
            InitByDataSource();
            LoadOption();
        }

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
        #region 判断是否配置完毕
        protected override bool IsOptionNotReady()
        {
            bool notReady = true;
            List<string> types = new List<string>() { this.comboBox0.GetType().Name, this.outListCCBL0.GetType().Name };
            foreach (Control ctl in this.tableLayoutPanel2.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == "")
                {
                    MessageBox.Show("请填写碰撞条件!");
                    return notReady;
                }
            }
            foreach (Control ctl in this.tableLayoutPanel1.Controls)
            {
                if (types.Contains(ctl.GetType().Name) && ctl.Text == "")
                {
                    MessageBox.Show("请填写碰撞条件!");
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
        #region 保存加载

        private void LoadOption()
        {
            if(Global.GetOptionDao().IsCleanBinaryOperatorOption(this.opControl, this.nowColumnsName0, this.nowColumnsName1))
                return;
            if (!String.IsNullOrEmpty(this.opControl.Option.GetOption("outfield0")))
            {
                string[] checkIndexs = this.opControl.Option.GetOptionSplit("outfield0");
                int[] indexs = Array.ConvertAll(checkIndexs, int.Parse);
                this.oldOutList0 = indexs.ToList();
                this.outListCCBL0.LoadItemCheckIndex(indexs);
                foreach (int index in indexs)
                {
                    if (OpUtil.IsArrayIndexOutOfBounds(this.outListCCBL0, index))
                        continue;
                    this.oldOutName0.Add(this.outListCCBL0.Items[index].ToString());
                }
                    
            }
            if (!String.IsNullOrEmpty(this.opControl.Option.GetOption("factor0")))
            {
                string factor1 = this.opControl.Option.GetOption("factor0");
                int[] optionItems0 = Array.ConvertAll(factor1.Split('\t'), int.Parse);
                if (!OpUtil.IsArrayIndexOutOfBounds(this.comboBox0, optionItems0[0]))
                {
                    this.comboBox0.Text = this.comboBox0.Items[optionItems0[0]].ToString();
                    this.comboBox0.Tag = optionItems0[0].ToString();
                }
                if (!OpUtil.IsArrayIndexOutOfBounds(this.comboBox1, optionItems0[1]))
                {
                    this.comboBox1.Text = this.comboBox1.Items[optionItems0[1]].ToString();
                    this.comboBox1.Tag = optionItems0[1].ToString();
                }
                
            }
           

            int count = this.opControl.Option.KeysCount("factor") - 1;
            if (count < 1)  // 只有factor1的情况
                return;
            InitNewFactorControl(count);

            for (int i = 0; i < count; i++)
            {
                string name = "factor" + (i + 1).ToString();
                string factor = this.opControl.Option.GetOption(name);
                if (String.IsNullOrEmpty(factor)) continue;
                int[] optionItems1 = Array.ConvertAll(factor.Split('\t'), int.Parse);
                Control control1 = this.tableLayoutPanel1.Controls[i * 5 + 0];
                Control control2 = this.tableLayoutPanel1.Controls[i * 5 + 1];
                Control control3 = this.tableLayoutPanel1.Controls[i * 5 + 2];
                if (!OpUtil.IsArrayIndexOutOfBounds(control1, optionItems1[0]))
                {
                    control1.Text = (control1 as ComboBox).Items[optionItems1[0]].ToString();
                    control1.Tag = optionItems1[0].ToString();
                }

                if (!OpUtil.IsArrayIndexOutOfBounds(control2, optionItems1[1]))
                {
                    control2.Text = (control2 as ComboBox).Items[optionItems1[1]].ToString();
                    control2.Tag = optionItems1[1].ToString();
                }

                if (!OpUtil.IsArrayIndexOutOfBounds(control3, optionItems1[2]))
                {
                    control3.Text = (control3 as ComboBox).Items[optionItems1[2]].ToString();
                    control3.Tag = optionItems1[2].ToString();
                }

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


        protected override void CreateLine(int addLine)
        {
            // And OR 选择框
            ComboBox regBox = NewAndORComboBox();
            this.tableLayoutPanel1.Controls.Add(regBox, 0, addLine);
            // 左表列下拉框
            ComboBox data0ComboBox = NewColumnsName0ComboBox();
            this.tableLayoutPanel1.Controls.Add(data0ComboBox, 1, addLine);
            // 右表列下拉框
            ComboBox data1ComboBox = NewColumnsName1ComboBox();
            this.tableLayoutPanel1.Controls.Add(data1ComboBox, 2, addLine);
            // 添加行按钮
            Button addButton = NewAddButton(addLine.ToString());
            this.tableLayoutPanel1.Controls.Add(addButton, 3, addLine);
            // 删除行按钮
            Button delButton = NewDelButton(addLine.ToString());
            this.tableLayoutPanel1.Controls.Add(delButton, 4, addLine);
        }

    }
}
