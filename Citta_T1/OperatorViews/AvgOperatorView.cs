using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.OperatorViews.Base;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.IO;
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
            InitOptionInfo();
            LoadOption();
           
            this.oldAvg = this.AvgComBox.Text;
            SetTextBoxName(this.dataSourceTB0);
        }
        #region 初始化配置
        private void InitOptionInfo()
        {
            
            Dictionary<string, string> dataInfo = Global.GetOptionDao().GetDataSourceInfo(this.opControl.ID);
            if (dataInfo.ContainsKey("dataPath0") && dataInfo.ContainsKey("encoding0"))
            {
                this.dataSourceFFP0 = dataInfo["dataPath0"];
                this.dataSourceTB0.Text = Path.GetFileNameWithoutExtension(this.dataSourceFFP0);
                SetOption(this.dataSourceFFP0, this.dataSourceTB0.Text, dataInfo["encoding0"], dataInfo["separator0"].ToCharArray());
            }

        }
        private void SetOption(string path, string dataName, string encoding, char[] separator)
        {
            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Empty, OpUtil.EncodingEnum(encoding), separator);
            this.nowColumnsName0 = bcpInfo.ColumnArray;
            foreach (string name in this.nowColumnsName0)
                this.AvgComBox.Items.Add(name);
            this.opControl.FirstDataSourceColumns = this.nowColumnsName0;
            this.opControl.Option.SetOption("columnname0", String.Join("\t", this.nowColumnsName0));
        }

        #endregion

        protected override void ConfirmButton_Click(object sender, EventArgs e)
        {
            //未设置字段警告
            if (this.dataSourceTB0.Text == "") return;
            if (this.AvgComBox.Text == "")
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
            this.selectedColumns.Add(this.AvgComBox.SelectedItem.ToString());
            ModelElement resultElement = Global.GetCurrentDocument().SearchResultElementByOpID(this.opControl.ID);
            if (resultElement == ModelElement.Empty)
            {
                MoveRsControlFactory.GetInstance().CreateNewMoveRsControl(this.opControl, this.selectedColumns);
                return;
            }
            // 对应的结果文件置脏
            BCPBuffer.GetInstance().SetDirty(resultElement.FullFilePath);

            //输出变化，重写BCP文件
            List<string> oldColumn = new List<string>();
            oldColumn.Add(this.oldAvg);
            if (this.oldAvg != this.AvgComBox.Text)
                Global.GetOptionDao().DoOutputCompare(oldColumn, this.selectedColumns, this.opControl.ID);

        }

        #region 配置信息的保存与加载
        private void SaveOption()
        {

            this.opControl.Option.SetOption("avgfield", this.selectedIndex == null ? this.AvgComBox.SelectedIndex.ToString() : this.selectedIndex);
            this.opControl.Option.SetOption("outfield", this.AvgComBox.SelectedIndex.ToString());

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
                this.AvgComBox.Text = this.AvgComBox.Items[index].ToString();
                this.selectedIndex = index.ToString();
            }
            
        }
        #endregion

        private void AvgComBox_Leave(object sender, EventArgs e)
        {
            this.optionInfoCheck.IsIllegalInputName(this.AvgComBox, this.nowColumnsName0, this.AvgComBox.Text);
        }

        private void AvgComBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.optionInfoCheck.IsIllegalInputName(this.AvgComBox, this.nowColumnsName0, this.AvgComBox.Text);
        }

        private void AvgComBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.selectedIndex = this.AvgComBox.SelectedIndex.ToString();
        }
    }
}
