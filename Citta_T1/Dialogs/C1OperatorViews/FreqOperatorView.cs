using C2.Controls.Move.Op;
using C2.Core;
using C2.Dialogs.Base;
using C2.Utils;
using System;
using System.Linq;
using System.Windows.Forms;

namespace C2.OperatorViews
{
    public partial class FreqOperatorView : C1BaseOperatorView
    {
        public FreqOperatorView(MoveOpControl opControl) : base(opControl)
        {
            InitializeComponent();
            InitializeComponentManual();
            InitializeDataSource();
            LoadOption();
        }
        // 为了兼顾设计器，一些控件需要手工初始化。
        private void InitializeComponentManual()
        {
            // 利用Paint方式groupBox附近的虚线留白
            //this.groupBox1.Paint += new PaintEventHandler(this.GroupBox_Paint);
            //this.groupBox2.Paint += new PaintEventHandler(this.GroupBox_Paint);
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
            this.opControl.Option.Clear();
            this.opControl.Option.SetOption("columnname0", this.nowColumnsName0);
            this.opControl.Option.SetOption("outfield0", outListCCBL0.GetItemCheckIndex());
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

            if (!String.IsNullOrEmpty(opControl.Option.GetOption("outfield0")))
            {
                string[] checkIndexs = opControl.Option.GetOptionSplit("outfield0");
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
