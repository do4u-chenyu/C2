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
    public partial class C2AvgOperatorView : C2BaseOperatorView
    {
        public C2AvgOperatorView(OperatorWidget operatorWidget) : base(operatorWidget)
        {
            InitializeComponent();
            InitializeDataSource();
            LoadOption();

        }
        #region 初始化配置
        protected override void InitializeDataSource()
        {
            // 初始化左右表数据源配置信息
            base.InitializeDataSource();
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
            this.operatorWidget.Option.Clear();
            this.operatorWidget.Option.SetOption("columnname0", this.nowColumnsName0);
            this.operatorWidget.Option.SetOption("avgfield", comboBox0.Tag == null ? this.comboBox0.SelectedIndex.ToString() : comboBox0.Tag.ToString());
            this.operatorWidget.Option.SetOption("outfield0", comboBox0.Tag == null ? this.comboBox0.SelectedIndex.ToString() : comboBox0.Tag.ToString());
            this.selectedColumns.Add(this.comboBox0.SelectedItem.ToString());
        }

        private void LoadOption()
        {
            if (String.IsNullOrEmpty(this.operatorWidget.Option.GetOption("avgfield")))
                return;
            int index = Convert.ToInt32(this.operatorWidget.Option.GetOption("avgfield"));
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
