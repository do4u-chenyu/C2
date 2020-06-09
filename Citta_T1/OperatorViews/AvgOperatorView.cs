using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.OperatorViews.Base;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{
    public partial class AvgOperatorView : BaseOperatorView
    {

        public AvgOperatorView(MoveOpControl opControl) : base(opControl)
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

        }


        #endregion

        #region 是否配置完毕
        protected override bool IsOptionNotReady()
        {
            bool empty = true;
            if (this.dataSourceTB0.Text == "") return empty;
            if (this.comboBox0.Text == "")
            {
                MessageBox.Show("请选择平均值字段!");
                return empty;
            }
            return !empty;
        }
        #endregion
        #region 配置信息的保存与加载
        protected override void SaveOption()
        {
            this.opControl.Option.SetOption("columnname0", String.Join("\t", this.nowColumnsName0));
            this.opControl.Option.SetOption("avgfield", comboBox0.Tag == null ? this.comboBox0.SelectedIndex.ToString() : comboBox0.Tag.ToString());
            this.opControl.Option.SetOption("outfield", this.comboBox0.SelectedIndex.ToString());
            this.selectedColumns.Add(this.comboBox0.SelectedItem.ToString());


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

            int index = Convert.ToInt32(this.opControl.Option.GetOption("avgfield"));
            this.comboBox0.Text = this.comboBox0.Items[index].ToString();
            this.comboBox0.Tag = index.ToString();
            this.oldOutName0 = new List<string>() { this.comboBox0.Items[index].ToString() };

        }
        #endregion
    }
}
