using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.OperatorViews.Base;
using System;
using System.Collections.Generic;
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

            this.comboBox0.SelectionChangeCommitted += new EventHandler(Global.GetOptionDao().GetSelectedItemIndex);
        }
        #region 添加取消
        protected override void ConfirmButton_Click(object sender, EventArgs e)
        {
            //未设置字段警告
            if (this.dataSourceTB0.Text == String.Empty) return;
            if (this.comboBox0.Text == String.Empty)
            {
                MessageBox.Show("请选择最大值字段");
                return;
            }
            if (this.outListCCBL0.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请选择输出字段!");
                return;
            }
            this.DialogResult = DialogResult.OK;

            SaveOption();
            //内容修改，引起文档dirty
            if (this.oldOptionDictStr != this.opControl.Option.ToString())
                Global.GetMainForm().SetDocumentDirty();

            //生成结果控件,创建relation,bcp结果文件
            ModelElement resultElement = Global.GetCurrentDocument().SearchResultElementByOpID(this.opControl.ID);
            if (resultElement == ModelElement.Empty)
            {
                MoveRsControlFactory.GetInstance().CreateNewMoveRsControl(this.opControl, this.outListCCBL0.GetItemCheckText());
                return;
            }

            // 对应的结果文件置脏
            BCPBuffer.GetInstance().SetDirty(resultElement.FullFilePath);

            //输出变化，重写BCP文件

             Global.GetOptionDao().DoOutputCompare(this.oldColumnsName0, this.outListCCBL0.GetItemCheckText(), this.opControl.ID);



        }
        #endregion
        #region 配置信息的保存与加载
        private void SaveOption()
        {
            string outField = string.Join("\t", this.outListCCBL0.GetItemCheckIndex());
            this.opControl.Option.SetOption("outfield", outField);
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
            int maxIndex = -1;
            if (!Global.GetOptionDao().IsCleanOption(this.opControl, this.nowColumnsName0, "maxfield"))
            {
                maxIndex = Convert.ToInt32(this.opControl.Option.GetOption("maxfield"));
                this.comboBox0.Text = this.comboBox0.Items[maxIndex].ToString();
                this.comboBox0.Tag = maxIndex.ToString();
            }
            if (!Global.GetOptionDao().IsCleanOption(this.opControl, this.nowColumnsName0, "outfield"))
            {

                string[] checkIndexs = this.opControl.Option.GetOptionSplit("outfield");
                int[] outIndexs = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                this.oldOutList0 = outIndexs.ToList();
                this.outListCCBL0.LoadItemCheckIndex(outIndexs);
                foreach (int i in outIndexs)
                    this.oldColumnsName0.Add(this.outListCCBL0.Items[i].ToString());
            }

            this.opControl.Option.SetOption("columnname0", string.Join("\t", this.opControl.FirstDataSourceColumns));
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
