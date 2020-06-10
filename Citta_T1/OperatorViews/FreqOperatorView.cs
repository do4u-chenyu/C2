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
    public partial class FreqOperatorView : BaseOperatorView
    {
        private List<bool> oldCheckedItems = new List<bool>();

        public FreqOperatorView(MoveOpControl opControl) : base(opControl)
        {
            InitializeComponent();
            InitByDataSource();
            LoadOption();

            this.oldCheckedItems.Add(this.repetition.Checked);
            this.oldCheckedItems.Add(this.noRepetition.Checked);
            this.oldCheckedItems.Add(this.ascendingOrder.Checked);
            this.oldCheckedItems.Add(this.descendingOrder.Checked);
        }
        #region 初始化配置
        private void InitByDataSource()
        {
            // 初始化左右表数据源配置信息
            this.InitDataSource();
            // 窗体自定义的初始化逻辑
            this.outListCCBL0.Items.AddRange(nowColumnsName0);
           
        }


        #endregion
        #region 添加取消
        protected override void ConfirmButton_Click(object sender, EventArgs e)
        {
            //未设置字段警告           
            if (this.outListCCBL0.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请选择输出字段!");
                return;
            }
            if (!this.noRepetition.Checked && !this.repetition.Checked)
            {
                MessageBox.Show("请选择数据是否进行去重");
                return;
            }
            if (!this.ascendingOrder.Checked && !this.descendingOrder.Checked)
            {
                MessageBox.Show("请选择数据排序");
                return;
            }
            this.DialogResult = DialogResult.OK;
            if (this.dataSourceTB0.Text == "") return;
            SaveOption();
            //内容修改，引起文档dirty

            if (this.oldCheckedItems[0] != this.repetition.Checked)
                Global.GetMainForm().SetDocumentDirty();
            else if (this.oldCheckedItems[1] != this.noRepetition.Checked)
                Global.GetMainForm().SetDocumentDirty();
            else if (this.oldCheckedItems[2] != this.ascendingOrder.Checked)
                Global.GetMainForm().SetDocumentDirty();
            else if (this.oldCheckedItems[3] != this.descendingOrder.Checked)
                Global.GetMainForm().SetDocumentDirty();
            else if (String.Join("\t", this.oldOutList0) != this.opControl.Option.GetOption("outfield"))
                Global.GetMainForm().SetDocumentDirty();
            //生成结果控件,创建relation,bcp结果文件

            ModelElement resultElement = Global.GetCurrentDocument().SearchResultElementByOpID(this.opControl.ID);
            if (resultElement == ModelElement.Empty)
            {
                this.selectedColumns = this.outListCCBL0.GetItemCheckText();
                this.selectedColumns.Add("频率统计结果");
                MoveRsControlFactory.GetInstance().CreateNewMoveRsControl(this.opControl, this.selectedColumns);
                return;
            }

            // 对应的结果文件置脏
            BCPBuffer.GetInstance().SetDirty(resultElement.FullFilePath);

            List<string> newData = new List<string>(this.outListCCBL0.GetItemCheckText());
            newData.Add("频率统计结果");
            //输出变化，重写BCP文件,它只要输出列名变化，表头就会改变
            if (String.Join("\t", this.oldOutList0) != this.opControl.Option.GetOption("outfield"))
                Global.GetOptionDao().IsNewOut(newData, this.opControl.ID);


        }


        #endregion
        #region 配置信息的保存与加载
        protected override void SaveOption()
        {
            this.opControl.Option.SetOption("columnname0", String.Join("\t", this.nowColumnsName0));
            List<int> checkIndexs = this.outListCCBL0.GetItemCheckIndex();
            this.opControl.Option.SetOption("outfield", string.Join("\t", checkIndexs));

            this.opControl.Option.SetOption("repetition", this.repetition.Checked.ToString());
            this.opControl.Option.SetOption("noRepetition", this.noRepetition.Checked.ToString());
            this.opControl.Option.SetOption("ascendingOrder", this.ascendingOrder.Checked.ToString());
            this.opControl.Option.SetOption("descendingOrder", this.descendingOrder.Checked.ToString());

            ElementStatus oldStatus = this.opControl.Status;
            if (this.oldOptionDictStr != this.opControl.Option.ToString())
                this.opControl.Status = ElementStatus.Ready;

            if (oldStatus == ElementStatus.Done && this.opControl.Status == ElementStatus.Ready)
                Global.GetCurrentDocument().DegradeChildrenStatus(this.opControl.ID);

        }

        private void LoadOption()
        {

            if (Global.GetOptionDao().IsCleanSingleOperatorOption(this.opControl, this.nowColumnsName0))
                return;

             repetition.Checked      = Convert.ToBoolean(opControl.Option.GetOption("repetition", "True"));
            noRepetition.Checked    = Convert.ToBoolean(opControl.Option.GetOption("noRepetition", "False"));
            ascendingOrder.Checked  = Convert.ToBoolean(opControl.Option.GetOption("ascendingOrder", "False"));
            descendingOrder.Checked = Convert.ToBoolean(opControl.Option.GetOption("descendingOrder", "True"));

            string[] checkIndexs = this.opControl.Option.GetOptionSplit("outfield");
            int[] indexs = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
            this.oldOutList0 = indexs.ToList();
            this.outListCCBL0.LoadItemCheckIndex(indexs);
            foreach (int index in indexs)
                this.oldOutName0.Add(this.outListCCBL0.Items[index].ToString());


        }
        #endregion
        private void GroupBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }

        private void GroupBox2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }
    }
}
