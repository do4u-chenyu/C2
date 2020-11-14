using C2.Dialogs.Base;
using C2.Model.Widgets;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.Dialogs.C2OperatorViews
{
    public partial class C2MaxOperatorView : C2BaseOperatorView
    {
        public C2MaxOperatorView(OperatorWidget operatorWidget) : base(operatorWidget)
        {           
            InitializeComponent();
            InitializeDataSource();
            LoadOption();
        }
        #region 判断是否配置完毕
        protected override bool IsOptionNotReady()
        {
            bool notReady = true;
            if (this.dataSourceTB0.Text == String.Empty)
                return notReady;
            if (this.comboBox0.Text == String.Empty)
            {
                HelpUtil.ShowMessageBox("请选择最大值字段");
                return notReady;
            }
            if (this.outListCCBL0.GetItemCheckIndex().Count == 0)
            {
                HelpUtil.ShowMessageBox("请选择输出字段");
                return notReady;
            }
            return !notReady;
        }

        #endregion

        #region 配置信息的保存与加载
        protected override void SaveOption()
        {
            this.operatorWidget.Option.Clear();
            this.operatorWidget.Option.SetOption("columnname0", firstDataSourceColumns);
            this.operatorWidget.Option.SetOption("outfield0", outListCCBL0.GetItemCheckIndex());
            this.selectedColumns = this.outListCCBL0.GetItemCheckText();
            if (this.comboBox0.Text == String.Empty)
                this.operatorWidget.Option.SetOption("maxfield", String.Empty);
            else
                this.operatorWidget.Option.SetOption("maxfield", this.comboBox0.Tag == null ? this.comboBox0.SelectedIndex.ToString() : this.comboBox0.Tag.ToString());

        }

        private void LoadOption()
        {
            if (!String.IsNullOrEmpty(this.operatorWidget.Option.GetOption("maxfield")))
            {
                int maxIndex = Convert.ToInt32(this.operatorWidget.Option.GetOption("maxfield"));
                if (!OpUtil.IsArrayIndexOutOfBounds(this.comboBox0, maxIndex))
                {
                    this.comboBox0.Text = this.comboBox0.Items[maxIndex].ToString();
                    this.comboBox0.Tag = maxIndex.ToString();
                }

            }
            if (!String.IsNullOrEmpty(this.operatorWidget.Option.GetOption("outfield0")))
            {
                string[] checkIndexs = this.operatorWidget.Option.GetOptionSplit("outfield0");
                int[] outIndexs = Array.ConvertAll(checkIndexs, int.Parse);
                this.oldOutList0 = outIndexs.ToList();
                this.outListCCBL0.LoadItemCheckIndex(outIndexs);
                foreach (int i in outIndexs)
                {
                    if (OpUtil.IsArrayIndexOutOfBounds(this.outListCCBL0, i))
                        continue;
                    this.oldOutName0.Add(this.outListCCBL0.Items[i].ToString());
                }
            }
        }
        #endregion

        #region 初始化配置
        protected override void InitializeDataSource()
        {
            base.InitializeDataSource();
            this.outListCCBL0.Items.AddRange(nowColumnsName0);
            this.comboBox0.Items.AddRange(nowColumnsName0);
        }
        #endregion
    }
}
