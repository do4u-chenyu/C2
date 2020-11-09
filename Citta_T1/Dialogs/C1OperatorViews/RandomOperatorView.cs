using C2.Controls.Move.Op;
using C2.Core;
using C2.Dialogs.Base;
using C2.Utils;
using System;
using System.Linq;
using System.Windows.Forms;

namespace C2.OperatorViews
{

    public partial class RandomOperatorView : C1BaseOperatorView
    {
        public RandomOperatorView(MoveOpControl opControl) : base(opControl)
        {
            InitializeComponent();
            InitializeDataSource();
            LoadOption();
        }
        #region 初始化配置
        protected override void InitializeDataSource()
        {   // 初始化左右表数据源配置信息
            base.InitializeDataSource();
            // 窗体自定义的初始化逻辑
            this.outListCCBL0.Items.AddRange(nowColumnsName0);
           
        }
        #endregion
        #region 配置信息的保存与加载
        protected override void SaveOption()
        {
            this.opControl.Option.Clear();
            this.opControl.Option.SetOption("columnname0", this.nowColumnsName0);
            this.opControl.Option.SetOption("randomnum", this.randomNumBox.Text);
            this.opControl.Option.SetOption("outfield0", outListCCBL0.GetItemCheckIndex());
            this.selectedColumns = this.outListCCBL0.GetItemCheckText();

            //更新子图所有节点状态
            UpdateSubGraphStatus();
        }

        private void LoadOption()
        {
            if (Global.GetOptionDao().IsCleanSingleOperatorOption(this.opControl, this.nowColumnsName0))
                return;

            this.randomNumBox.Text = this.opControl.Option.GetOption("randomnum");
            if (!String.IsNullOrEmpty(this.opControl.Option.GetOption("outfield0")))
            {
                string[] checkIndexs = this.opControl.Option.GetOptionSplit("outfield0");
                int[] indexs = Array.ConvertAll(checkIndexs, int.Parse);
                this.oldOutList0 = indexs.ToList();
                this.outListCCBL0.LoadItemCheckIndex(indexs);
                foreach (int index in indexs)
                {
                    if (OpUtil.IsArrayIndexOutOfBounds(this.outListCCBL0, index))
                        continue;
                    this.oldOutName0.Add(this.outListCCBL0.Items[index].ToString()); 
                }
                   
            }
           

        }
        #endregion
        #region 判断是否配置完毕
        protected override bool IsOptionNotReady()
        {
            bool notReady = true;

            if (String.IsNullOrEmpty(this.dataSourceTB0.Text))
                return notReady;

            if (String.IsNullOrEmpty(this.randomNumBox.Text))
            {
                MessageBox.Show("随机条数字段不能为空,请输入一个整数");
                return notReady;
            }
            if (ConvertUtil.ControlTextTryParseInt(randomNumBox))
            {
                MessageBox.Show("请输入小于" + int.MaxValue + "的正整数.");
                return notReady;
            }
            if (this.outListCCBL0.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请选择算子要输出的字段");
                return notReady;
            }
            return !notReady;
        }       
        #endregion

    }
}
