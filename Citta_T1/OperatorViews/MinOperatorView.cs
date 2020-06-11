using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.OperatorViews.Base;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{
    public partial class MinOperatorView : BaseOperatorView
    {
        public MinOperatorView(MoveOpControl opControl) : base(opControl)
        {
            InitializeComponent();
            InitByDataSource();
            LoadOption();
        }
        #region 判断是否配置完毕
        protected override bool IsOptionNotReady()
        {
            bool notReady = true;
            if (String.IsNullOrWhiteSpace(dataSourceTB0.Text))
                return notReady;
            if (String.IsNullOrWhiteSpace(this.comboBox0.Text))
            {
                MessageBox.Show("请选择最小值字段!");
                return notReady;
            }
            if (this.outListCCBL0.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请选择输出字段!");
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
            this.opControl.Option.SetOption("minfield", this.comboBox0.Tag == null ? this.comboBox0.SelectedIndex.ToString() : this.comboBox0.Tag.ToString());
            this.selectedColumns = this.outListCCBL0.GetItemCheckText();

            //更新子图所有节点状态
            UpdateSubGraphStatus();
        }

        private void LoadOption()
        {
            if (Global.GetOptionDao().IsCleanSingleOperatorOption(this.opControl, this.nowColumnsName0))
                return;

            int index = Convert.ToInt32(this.opControl.Option.GetOption("minfield"));
            this.comboBox0.Text = this.comboBox0.Items[index].ToString();
            this.comboBox0.Tag = index.ToString();

            string[] checkIndexs = this.opControl.Option.GetOptionSplit("outfield");
            int[] indexs = Array.ConvertAll(checkIndexs, int.Parse);
            this.oldOutList0 = indexs.ToList();
            this.outListCCBL0.LoadItemCheckIndex(indexs);
            foreach (int i in indexs)
                this.oldOutName0.Add(this.outListCCBL0.Items[i].ToString());

        }
        #endregion
        #region 初始化配置
        private void InitByDataSource()
        {
            this.InitDataSource();
            this.outListCCBL0.Items.AddRange(nowColumnsName0);
            this.comboBox0.Items.AddRange(nowColumnsName0);          
        }
        #endregion
    }
}
