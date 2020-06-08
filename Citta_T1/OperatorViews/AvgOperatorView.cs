using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.OperatorViews.Base;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{
    public partial class AvgOperatorView : BaseOperatorView
    {
        private string oldAvg;
        private string selectedIndex;

        public AvgOperatorView(MoveOpControl opControl) : base(opControl)
        {
            InitializeComponent();
            InitByDataSource();
            LoadOption();

            this.oldAvg = this.comboBox0.Text;
        }
        #region 初始化配置
        private void InitByDataSource()
        {
            // 初始化左右表数据源配置信息
            this.InitDataSource();
            // 窗体自定义的初始化逻辑
            this.comboBox0.Items.AddRange(nowColumnsName0);
            this.opControl.Option.SetOption("columnname0", String.Join("\t", this.nowColumnsName0));
        }


        #endregion

        protected override void ConfirmButton_Click(object sender, EventArgs e)
        {
            //未设置字段警告
            if (this.dataSourceTB0.Text == "") return;
            if (this.comboBox0.Text == "")
            {
                MessageBox.Show("请选择平均值字段!");
                return;
            }
            this.DialogResult = DialogResult.OK;
            SaveOption();

            //情况1：内容修改
            //       引起文档dirty
            //情况2：内容不修改
            //        返回
            if (this.oldOptionDictStr != this.opControl.Option.ToString())
                Global.GetMainForm().SetDocumentDirty();
            else
                return;
            //生成结果控件,创建relation,bcp结果文件
            this.selectedColumns.Add(this.comboBox0.SelectedItem.ToString());
            ModelElement resultElement = Global.GetCurrentDocument().SearchResultElementByOpID(this.opControl.ID);
            if (resultElement == ModelElement.Empty)
            {
                MoveRsControlFactory.GetInstance().CreateNewMoveRsControl(this.opControl, this.selectedColumns);
                return;
            }
            // 对应的结果文件置脏
            BCPBuffer.GetInstance().SetDirty(resultElement.FullFilePath);
            //输出变化，重写BCP文件
            Global.GetOptionDao().DoOutputCompare(new List<string>() { this.oldAvg }, this.selectedColumns, this.opControl.ID);

        }

        #region 配置信息的保存与加载
        private void SaveOption()
        {

            this.opControl.Option.SetOption("avgfield", this.selectedIndex == null ? this.comboBox0.SelectedIndex.ToString() : this.selectedIndex);
            this.opControl.Option.SetOption("outfield", this.comboBox0.SelectedIndex.ToString());

            ElementStatus oldStatus = this.opControl.Status;
            if (this.oldOptionDictStr != this.opControl.Option.ToString())
                this.opControl.Status = ElementStatus.Ready;

            if (oldStatus == ElementStatus.Done && this.opControl.Status == ElementStatus.Ready)
                Global.GetCurrentDocument().DegradeChildrenStatus(this.opControl.ID);
        }

        private void LoadOption()
        {
            if (!Global.GetOptionDao().IsCleanOption(this.opControl, this.nowColumnsName0, "avgfield"))
            {
                int index = Convert.ToInt32(this.opControl.Option.GetOption("avgfield"));
                this.comboBox0.Text = this.comboBox0.Items[index].ToString();
                this.selectedIndex = index.ToString();
            }

        }
        #endregion

        private void AvgComBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.selectedIndex = this.comboBox0.SelectedIndex.ToString();
        }
    }
}
