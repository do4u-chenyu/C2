using Citta_T1.Business.Model;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.OperatorViews.Base;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{
    public partial class MaxOperatorView : BaseOperatorView
    {
        public MaxOperatorView(MoveOpControl opControl) : base(opControl)
        {
            InitializeComponent();
            InitByDataSource();
            LoadOption();
        }
        #region 判断是否配置完毕
        protected override bool IsOptionNotReady()
        {
            bool empty = true;
            if (this.dataSourceTB0.Text == String.Empty)
                return empty;
            if (this.comboBox0.Text == String.Empty)
            {
                MessageBox.Show("请选择最大值字段");
                return empty;
            }
            if (this.outListCCBL0.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请选择输出字段!");
                return empty;
            }
            return !empty;
        }
    
        #endregion
        #region 配置信息的保存与加载
        protected override void SaveOption()
        {
            this.opControl.Option.SetOption("columnname0", opControl.FirstDataSourceColumns);
            this.opControl.Option.SetOption("outfield", outListCCBL0.GetItemCheckIndex());
            this.selectedColumns = this.outListCCBL0.GetItemCheckText();
            if (this.comboBox0.Text == String.Empty)
                this.opControl.Option.SetOption("maxfield", String.Empty);
            else
                this.opControl.Option.SetOption("maxfield", this.comboBox0.Tag == null ? this.comboBox0.SelectedIndex.ToString() : this.comboBox0.Tag.ToString());

            ElementStatus oldStatus = this.opControl.Status;
            if (this.oldOptionDictStr != this.opControl.Option.ToString())
                this.opControl.Status = ElementStatus.Ready;

            if (oldStatus == ElementStatus.Done && this.opControl.Status == ElementStatus.Ready)
                Global.GetCurrentDocument().DegradeChildrenStatus(this.opControl.ID);

        }

        private void LoadOption()
        {
            
            if (Global.GetOptionDao().IsCleanSingleOperatorOption(this.opControl, this.nowColumnsName0))
                return;
            int maxIndex = -1;
            maxIndex = Convert.ToInt32(this.opControl.Option.GetOption("maxfield"));
            this.comboBox0.Text = this.comboBox0.Items[maxIndex].ToString();
            this.comboBox0.Tag = maxIndex.ToString();

            string[] checkIndexs = this.opControl.Option.GetOptionSplit("outfield");
            int[] outIndexs = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
            this.oldOutList0 = outIndexs.ToList();
            this.outListCCBL0.LoadItemCheckIndex(outIndexs);
            foreach (int i in outIndexs)
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
