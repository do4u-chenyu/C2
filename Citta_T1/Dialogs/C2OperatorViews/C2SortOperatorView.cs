using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C2.Dialogs.Base;
using C2.Model.Widgets;
using C2.Utils;

namespace C2.Dialogs.C2OperatorViews
{
    public partial class C2SortOperatorView : C2BaseOperatorView
    {
        private List<int> outList;
        public C2SortOperatorView(OperatorWidget operatorWidget) : base(operatorWidget)
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
                || !String.IsNullOrWhiteSpace(endText) && ConvertUtil.ControlTextTryParseInt(endRow))
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
            this.operatorWidget.Option.Clear();
            this.operatorWidget.Option.SetOption("columnname0", this.nowColumnsName0);
            this.operatorWidget.Option.SetOption("outfield0", this.outList);
            this.operatorWidget.Option.SetOption("sortfield", this.comboBox0.Tag == null ? this.comboBox0.SelectedIndex.ToString() : this.comboBox0.Tag.ToString());
            this.operatorWidget.Option.SetOption("repetition", this.repetition.Checked);
            this.operatorWidget.Option.SetOption("noRepetition", this.noRepetition.Checked);
            this.operatorWidget.Option.SetOption("ascendingOrder", this.ascendingOrder.Checked);
            this.operatorWidget.Option.SetOption("descendingOrder", this.descendingOrder.Checked);
            this.operatorWidget.Option.SetOption("sortByNum", this.sortByNum.Checked);
            this.operatorWidget.Option.SetOption("sortByString", this.sortByString.Checked);
            this.operatorWidget.Option.SetOption("firstRow", this.firstRow.Text);
            this.operatorWidget.Option.SetOption("endRow", this.endRow.Text);
            this.selectedColumns = this.nowColumnsName0.ToList();
        }

        private void LoadOption()
        {
            if (!String.IsNullOrEmpty(this.operatorWidget.Option.GetOption("sortfield")))
            {
                int index = Convert.ToInt32(this.operatorWidget.Option.GetOption("sortfield"));
                if (!OpUtil.IsArrayIndexOutOfBounds(this.comboBox0, index))
                {
                    this.comboBox0.Text = this.comboBox0.Items[index].ToString();
                    this.comboBox0.Tag = index.ToString();
                }

            }

            this.repetition.Checked = Convert.ToBoolean(this.operatorWidget.Option.GetOption("repetition", "False"));
            this.noRepetition.Checked = Convert.ToBoolean(this.operatorWidget.Option.GetOption("noRepetition", "True"));
            this.ascendingOrder.Checked = Convert.ToBoolean(this.operatorWidget.Option.GetOption("ascendingOrder", "True"));
            this.descendingOrder.Checked = Convert.ToBoolean(this.operatorWidget.Option.GetOption("descendingOrder", "False"));
            this.sortByNum.Checked = Convert.ToBoolean(this.operatorWidget.Option.GetOption("sortByNum", "True"));
            this.sortByString.Checked = Convert.ToBoolean(this.operatorWidget.Option.GetOption("sortByString", "False"));
            this.firstRow.Text = this.operatorWidget.Option.GetOption("firstRow", "1");
            this.endRow.Text = this.operatorWidget.Option.GetOption("endRow");
            this.oldOutName0 = this.operatorWidget.Option.GetOptionSplit("columnname0").ToList();
        }
        #endregion
    }
}