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
    public partial class C2FreqOperatorView : C2BaseOperatorView
    {
        public C2FreqOperatorView(OperatorWidget operatorWidget) : base(operatorWidget)
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
            this.outListCCBL0.Items.AddRange(nowColumnsName0);

        }
        #endregion
        #region 添加取消
        protected override bool IsOptionNotReady()
        {
            bool notReady = true;
            if (this.outListCCBL0.GetItemCheckIndex().Count == 0)
            {
                HelpUtil.ShowMessageBox("请选择输出字段");
                return notReady;
            }
            if (!this.noRepetition.Checked && !this.repetition.Checked)
            {
                HelpUtil.ShowMessageBox("请选择数据是否进行去重");
                return notReady;
            }
            if (!this.ascendingOrder.Checked && !this.descendingOrder.Checked)
            {
                HelpUtil.ShowMessageBox("请选择数据排序");
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
            this.operatorWidget.Option.SetOption("repetition", this.repetition.Checked);
            this.operatorWidget.Option.SetOption("noRepetition", this.noRepetition.Checked);
            this.operatorWidget.Option.SetOption("ascendingOrder", this.ascendingOrder.Checked);
            this.operatorWidget.Option.SetOption("descendingOrder", this.descendingOrder.Checked);
            this.selectedColumns = this.outListCCBL0.GetItemCheckText();
            this.selectedColumns.Add("频率统计结果");
        }

        private void LoadOption()
        {
            repetition.Checked = Convert.ToBoolean(operatorWidget.Option.GetOption("repetition", "True"));
            noRepetition.Checked = Convert.ToBoolean(operatorWidget.Option.GetOption("noRepetition", "False"));
            ascendingOrder.Checked = Convert.ToBoolean(operatorWidget.Option.GetOption("ascendingOrder", "False"));
            descendingOrder.Checked = Convert.ToBoolean(operatorWidget.Option.GetOption("descendingOrder", "True"));

            if (!String.IsNullOrEmpty(operatorWidget.Option.GetOption("outfield0")))
            {
                string[] checkIndexs = operatorWidget.Option.GetOptionSplit("outfield0");
                int[] indexs = Array.ConvertAll(checkIndexs, int.Parse);
                oldOutList0 = indexs.ToList();
                outListCCBL0.LoadItemCheckIndex(indexs);
                foreach (int index in indexs)
                {
                    if (OpUtil.IsArrayIndexOutOfBounds(outListCCBL0, index))
                        continue;
                    oldOutName0.Add(outListCCBL0.Items[index].ToString());
                }

                oldOutName0.Add("频率统计结果");
            }
        }
        #endregion
    }
}
