using C2.Controls.Move.Op;
using C2.Core;
using C2.Dialogs.Base;
using C2.Utils;
using System;
using System.Linq;
using System.Windows.Forms;

namespace C2.OperatorViews
{
    public partial class MaxOperatorView : C1BaseOperatorView
    {
        public MaxOperatorView(MoveOpControl opControl) : base(opControl)
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
                MessageBox.Show("请选择最大值字段");
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
            this.opControl.Option.Clear();
            this.opControl.Option.SetOption("columnname0", opControl.FirstDataSourceColumns);
            this.opControl.Option.SetOption("outfield0", outListCCBL0.GetItemCheckIndex());
            this.selectedColumns = this.outListCCBL0.GetItemCheckText();
            if (this.comboBox0.Text == String.Empty)
                this.opControl.Option.SetOption("maxfield", String.Empty);
            else
                this.opControl.Option.SetOption("maxfield", this.comboBox0.Tag == null ? this.comboBox0.SelectedIndex.ToString() : this.comboBox0.Tag.ToString());

            //更新子图所有节点状态
            UpdateSubGraphStatus();
        }

        private void LoadOption()
        {
            
            if (Global.GetOptionDao().IsCleanSingleOperatorOption(this.opControl, this.nowColumnsName0))
                return;

            if (!String.IsNullOrEmpty(this.opControl.Option.GetOption("maxfield")))
            {
                int maxIndex = Convert.ToInt32(this.opControl.Option.GetOption("maxfield"));
                if (!OpUtil.IsArrayIndexOutOfBounds(this.comboBox0, maxIndex))
                {
                    this.comboBox0.Text = this.comboBox0.Items[maxIndex].ToString();
                    this.comboBox0.Tag = maxIndex.ToString();
                }

            }
            if (!String.IsNullOrEmpty(this.opControl.Option.GetOption("outfield0")))
            {
                string[] checkIndexs = this.opControl.Option.GetOptionSplit("outfield0");
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
