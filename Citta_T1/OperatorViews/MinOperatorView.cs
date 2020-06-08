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
    public partial class MinOperatorView : BaseOperatorView
    {
        public MinOperatorView(MoveOpControl opControl) : base(opControl)
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
            if (String.IsNullOrWhiteSpace(this.comboBox0.Text))
            {
                MessageBox.Show("请选择最小值字段!");
                return;
            }
            if (this.outListCCBL0.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请选择输出字段!");
                return;
            }
            this.DialogResult = DialogResult.OK;
            if (String.IsNullOrWhiteSpace(dataSourceTB0.Text)) return;
            SaveOption();
            //内容修改，引起文档dirty
            if (this.oldOptionDictStr != opControl.Option.ToString())
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
            this.opControl.Option.SetOption("minfield", this.comboBox0.Tag == null ? this.comboBox0.SelectedIndex.ToString() : this.comboBox0.Tag.ToString());

            ElementStatus oldStatus = this.opControl.Status;
            if (this.oldOptionDictStr != this.opControl.Option.ToString())
                this.opControl.Status = ElementStatus.Ready;

            if (oldStatus == ElementStatus.Done && this.opControl.Status == ElementStatus.Ready)
                Global.GetCurrentDocument().DegradeChildrenStatus(this.opControl.ID);
        }

        private void LoadOption()
        {
            if (!Global.GetOptionDao().IsCleanOption(this.opControl, this.nowColumnsName0, "minfield"))
            {
                int index = Convert.ToInt32(this.opControl.Option.GetOption("minfield"));
                this.comboBox0.Text = this.comboBox0.Items[index].ToString();
                this.comboBox0.Tag = index.ToString();
            }
            if (!Global.GetOptionDao().IsCleanOption(this.opControl, this.nowColumnsName0, "outfield"))
            {

                string[] checkIndexs = this.opControl.Option.GetOptionSplit("outfield");
                int[] indexs = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                this.oldOutList0 = indexs.ToList();
                this.outListCCBL0.LoadItemCheckIndex(indexs);
                foreach (int index in indexs)
                    this.oldColumnsName0.Add(this.outListCCBL0.Items[index].ToString());
            }

        }
        #endregion
        #region 初始化配置
        private void InitByDataSource()
        {
            this.InitDataSource();
            this.outListCCBL0.Items.AddRange(nowColumnsName0);
            this.comboBox0.Items.AddRange(nowColumnsName0);
            this.opControl.Option.SetOption("columnname0", String.Join("\t", this.nowColumnsName0));
        }
        #endregion
    }
}
