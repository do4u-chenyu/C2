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
    public partial class SortOperatorView : BaseOperatorView
    {
        private List<int> outList;

        public SortOperatorView(MoveOpControl opControl) : base(opControl)
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
            this.outList = Enumerable.Range(0, this.nowColumnsName0.Length).ToList();
           
        }
        #endregion
        #region 添加取消
        protected override bool IsOptionNotReady()
        {
            bool notReady = true;
            if (String.IsNullOrWhiteSpace(this.comboBox0.Text))
            {
                MessageBox.Show("请选择排序字段!");
                return notReady;
            }
            if (String.IsNullOrWhiteSpace(this.firstRow.Text))
            {
                MessageBox.Show("请输出行数!");
                return notReady;
            }

            if (!IsCorrectOutOrder(this.firstRow.Text, this.endRow.Text))
                return notReady;
            return !notReady;
        }      
        #endregion

        #region 配置信息的保存与加载
        protected override void SaveOption()
        {
            this.opControl.Option.SetOption("columnname0", this.nowColumnsName0);
            this.opControl.Option.SetOption("outfield", this.outList);
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

            int index = Convert.ToInt32(this.opControl.Option.GetOption("sortfield"));
            this.comboBox0.Text = this.comboBox0.Items[index].ToString();
            this.comboBox0.Tag = index.ToString();
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

        #region 输入非数字，警告
        private void FirstRow_Leave(object sender, EventArgs e)
        {
            if (!ConvertUtil.IsInt(this.firstRow.Text))
            {
                MessageBox.Show("请输入数字");
                this.firstRow.Text = String.Empty;
            }

            else
                this.firstRow.Text = int.Parse(this.firstRow.Text).ToString();
        }

        private void EndRow_Leave(object sender, EventArgs e)
        {
            if (!ConvertUtil.IsInt(this.endRow.Text))
            {
                MessageBox.Show("请输入数字");
                this.endRow.Text = String.Empty;
            }

            else
                this.endRow.Text = int.Parse(this.endRow.Text).ToString();

        }

        #endregion

        private bool IsCorrectOutOrder(string firstRow, string endRow)
        {
            if (endRow == "") return true;
            int first = Convert.ToInt32(firstRow);
            int end = Convert.ToInt32(endRow);
            if (first > end)
            {
                MessageBox.Show("输出行数选择中，起始行数大于结束行数");
                return false;
            }
            return true;
        }
    }
}
