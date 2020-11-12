using C2.Controls.Move.Op;
using C2.Core;
using C2.Dialogs.Base;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace C2.OperatorViews
{
    public partial class SortOperatorView : C1BaseOperatorView
    {
        private List<int> outList;

        public SortOperatorView(MoveOpControl opControl) : base(opControl)
        {
            InitializeComponent();
            InitializeComponentManual();
            InitializeDataSource();
            LoadOption();
        }
        // 为了兼顾设计器，一些控件需要手工初始化。
        private void InitializeComponentManual()
        {
            // 利用Paint方式groupBox附近的虚线留白
           // this.groupBox1.Paint += new PaintEventHandler(this.GroupBox_Paint);
           // this.groupBox2.Paint += new PaintEventHandler(this.GroupBox_Paint);
            //this.groupBox3.Paint += new PaintEventHandler(this.GroupBox_Paint);
        }

        #region 配置初始化
        protected override void InitializeDataSource()
        {
            // 初始化左右表数据源配置信息
            base.InitializeDataSource();
            // 窗体自定义的初始化逻辑
            this.comboBox0.Items.AddRange(nowColumnsName0);
            this.outList = Enumerable.Range(0, this.nowColumnsName0.Length).ToList();
           
        }
        #endregion
        #region 添加取消
        protected override bool IsOptionNotReady()
        {
            bool notReady = true;
            string firstText = this.firstRow.Text;
            string endText = this.endRow.Text;
           
            if (String.IsNullOrWhiteSpace(this.comboBox0.Text))
            {
                MessageBox.Show("请选择排序字段!");
                return notReady;
            }
            if (String.IsNullOrWhiteSpace(firstText))
            {
                MessageBox.Show("请选择输出行数!");
                return notReady;
            }
            if (ConvertUtil.ControlTextTryParseInt(firstRow) 
                || !String.IsNullOrWhiteSpace(endText)&& ConvertUtil.ControlTextTryParseInt(endRow))
            {
                MessageBox.Show("请输入小于" + int.MaxValue + "的正整数.");
                return notReady;
            }
            if (!String.IsNullOrEmpty(endText) && Convert.ToInt32(firstText) > Convert.ToInt32(endText))
            {
                MessageBox.Show("输出行数选择中，起始行数大于结束行数");
                return notReady;
            }
            return !notReady;
        }      
        #endregion

        #region 配置信息的保存与加载
        protected override void SaveOption()
        {
            this.opControl.Option.Clear();
            this.opControl.Option.SetOption("columnname0", this.nowColumnsName0);
            this.opControl.Option.SetOption("outfield0", this.outList);
            this.opControl.Option.SetOption("sortfield", this.comboBox0.Tag == null ? this.comboBox0.SelectedIndex.ToString() : this.comboBox0.Tag.ToString());
            this.opControl.Option.SetOption("repetition", this.repetition.Checked);
            this.opControl.Option.SetOption("noRepetition", this.noRepetition.Checked);
            this.opControl.Option.SetOption("ascendingOrder", this.ascendingOrder.Checked);
            this.opControl.Option.SetOption("descendingOrder", this.descendingOrder.Checked);
            this.opControl.Option.SetOption("sortByNum", this.sortByNum.Checked);
            this.opControl.Option.SetOption("sortByString", this.sortByString.Checked);
            this.opControl.Option.SetOption("firstRow", this.firstRow.Text);
            this.opControl.Option.SetOption("endRow", this.endRow.Text);
            this.selectedColumns = this.nowColumnsName0.ToList();

            //更新子图所有节点状态
            UpdateSubGraphStatus();
        }

        private void LoadOption()
        {
            if (Global.GetOptionDao().IsCleanSingleOperatorOption(this.opControl, this.nowColumnsName0))
                return;

            if (!String.IsNullOrEmpty(this.opControl.Option.GetOption("sortfield")))
            {
                int index = Convert.ToInt32(this.opControl.Option.GetOption("sortfield"));
                if (!OpUtil.IsArrayIndexOutOfBounds(this.comboBox0, index))
                {
                    this.comboBox0.Text = this.comboBox0.Items[index].ToString();
                    this.comboBox0.Tag = index.ToString();
                }

            }

            this.repetition.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("repetition", "False"));
            this.noRepetition.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("noRepetition", "True"));
            this.ascendingOrder.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("ascendingOrder", "True"));
            this.descendingOrder.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("descendingOrder", "False"));
            this.sortByNum.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("sortByNum", "True"));
            this.sortByString.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("sortByString", "False"));
            this.firstRow.Text = this.opControl.Option.GetOption("firstRow", "1");
            this.endRow.Text = this.opControl.Option.GetOption("endRow");
            this.oldOutName0 = this.opControl.Option.GetOptionSplit("columnname0").ToList();
        }
        #endregion

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
