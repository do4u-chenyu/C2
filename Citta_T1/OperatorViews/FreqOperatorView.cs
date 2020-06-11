using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.OperatorViews.Base;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{
    public partial class FreqOperatorView : BaseOperatorView
    {
        public FreqOperatorView(MoveOpControl opControl) : base(opControl)
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
            this.outListCCBL0.Items.AddRange(nowColumnsName0);
           
        }
        #endregion
        #region 添加取消
        protected override bool IsOptionNotReady()
        {
            bool notReady = true;
            if (this.outListCCBL0.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请选择输出字段!");
                return notReady;
            }
            if (!this.noRepetition.Checked && !this.repetition.Checked)
            {
                MessageBox.Show("请选择数据是否进行去重");
                return notReady;
            }
            if (!this.ascendingOrder.Checked && !this.descendingOrder.Checked)
            {
                MessageBox.Show("请选择数据排序");
                return notReady;
            }
            return !notReady;
        }
        #endregion
        #region 配置信息的保存与加载
        protected override void SaveOption()
        {
            this.opControl.Option.OptionDict.Clear();
            this.opControl.Option.SetOption("columnname0", this.nowColumnsName0);
            this.opControl.Option.SetOption("outfield", outListCCBL0.GetItemCheckIndex());
            this.opControl.Option.SetOption("repetition", this.repetition.Checked);
            this.opControl.Option.SetOption("noRepetition", this.noRepetition.Checked);
            this.opControl.Option.SetOption("ascendingOrder", this.ascendingOrder.Checked);
            this.opControl.Option.SetOption("descendingOrder", this.descendingOrder.Checked);
            this.selectedColumns = this.outListCCBL0.GetItemCheckText();
            this.selectedColumns.Add("频率统计结果");

            //更新子图所有节点状态
            UpdateSubGraphStatus();
        }

        private void LoadOption()
        {
            if (Global.GetOptionDao().IsCleanSingleOperatorOption(opControl, nowColumnsName0))
                return;

            repetition.Checked      = Convert.ToBoolean(opControl.Option.GetOption("repetition", "True"));
            noRepetition.Checked    = Convert.ToBoolean(opControl.Option.GetOption("noRepetition", "False"));
            ascendingOrder.Checked  = Convert.ToBoolean(opControl.Option.GetOption("ascendingOrder", "False"));
            descendingOrder.Checked = Convert.ToBoolean(opControl.Option.GetOption("descendingOrder", "True"));

            string[] checkIndexs = opControl.Option.GetOptionSplit("outfield");
            int[] indexs = Array.ConvertAll(checkIndexs, int.Parse);
            oldOutList0 = indexs.ToList();
            outListCCBL0.LoadItemCheckIndex(indexs);
            foreach (int index in indexs)
                oldOutName0.Add(outListCCBL0.Items[index].ToString());
            oldOutName0.Add("频率统计结果");

        }
        #endregion
    }
}
