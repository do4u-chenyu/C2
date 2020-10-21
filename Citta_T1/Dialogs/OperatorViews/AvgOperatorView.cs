using C2.Controls.Move.Op;
using C2.Core;
using C2.OperatorViews.Base;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace C2.OperatorViews
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
            bool notReady = true;
            if (this.dataSourceTB0.Text == "") return notReady;
            if (this.comboBox0.Text == "")
            {
                MessageBox.Show("请选择平均值字段!");
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
            this.opControl.Option.SetOption("avgfield", comboBox0.Tag == null ? this.comboBox0.SelectedIndex.ToString() : comboBox0.Tag.ToString());
            this.opControl.Option.SetOption("outfield0", comboBox0.Tag == null ? this.comboBox0.SelectedIndex.ToString() : comboBox0.Tag.ToString());
            this.selectedColumns.Add(this.comboBox0.SelectedItem.ToString());

            //更新子图所有节点状态
            UpdateSubGraphStatus();
        }

        private void LoadOption()
        {
            if (Global.GetOptionDao().IsCleanSingleOperatorOption(this.opControl, this.nowColumnsName0))
                return;
            if (String.IsNullOrEmpty(this.opControl.Option.GetOption("avgfield")))
                return;
            int index = Convert.ToInt32(this.opControl.Option.GetOption("avgfield"));
            if (!OpUtil.IsArrayIndexOutOfBounds(this.comboBox0, index))
            {
                this.comboBox0.Text = this.comboBox0.Items[index].ToString();
                this.comboBox0.Tag = index.ToString();
                this.oldOutName0 = new List<string>() { this.comboBox0.Items[index].ToString() };
            }
 
        }
        #endregion
    }
}
