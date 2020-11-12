using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C2.Core;
using C2.Dialogs.Base;
using C2.Model.Widgets;
using C2.Utils;

namespace C2.Dialogs.C2OperatorViews
{
    public partial class C2MinOperatorView : C2BaseOperatorView
    {
        public C2MinOperatorView(OperatorWidget operatorWidget) : base(operatorWidget)
        {
            InitializeComponent();
            InitializeDataSource();
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
            this.operatorWidget.Option.Clear();
            this.operatorWidget.Option.SetOption("columnname0", this.nowColumnsName0);
            this.operatorWidget.Option.SetOption("outfield0", outListCCBL0.GetItemCheckIndex());
            this.operatorWidget.Option.SetOption("minfield", this.comboBox0.Tag == null ? this.comboBox0.SelectedIndex.ToString() : this.comboBox0.Tag.ToString());
            this.selectedColumns = this.outListCCBL0.GetItemCheckText();
        }

        private void LoadOption()
        {
            if (!String.IsNullOrEmpty(this.operatorWidget.Option.GetOption("minfield")))
            {
                int index = Convert.ToInt32(this.operatorWidget.Option.GetOption("minfield"));
                if (!OpUtil.IsArrayIndexOutOfBounds(this.comboBox0, index))
                {
                    this.comboBox0.Text = this.comboBox0.Items[index].ToString();
                    this.comboBox0.Tag = index.ToString();
                }
            }

            if (!String.IsNullOrEmpty(this.operatorWidget.Option.GetOption("outfield0")))
            {
                string[] checkIndexs = this.operatorWidget.Option.GetOptionSplit("outfield0");
                int[] indexs = Array.ConvertAll(checkIndexs, int.Parse);
                this.oldOutList0 = indexs.ToList();
                this.outListCCBL0.LoadItemCheckIndex(indexs);
                foreach (int i in indexs)
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
