using C2.Business.Model;
using C2.Controls.Move.Op;
using C2.Core;
using C2.Dialogs.Base;
using System;

namespace C2.OperatorViews
{
    public partial class PreprocessingOperatorView : C1BaseOperatorView
    {
        public PreprocessingOperatorView(MoveOpControl opControl) : base(opControl)
        {
            InitializeComponent();
            InitializeDataSource();//初始化配置内容
            LoadOption();
        }


        #region 配置信息的保存与加载
        protected override void SaveOption()
        {
            this.opControl.Option.Clear();
            //输入输出字段待定
            //this.opControl.Option.SetOption("columnname0", this.nowColumnsName0);
            //this.opControl.Option.SetOption("outfield0", comboBox0.Tag == null ? this.comboBox0.SelectedIndex.ToString() : comboBox0.Tag.ToString());
            
            this.opControl.Option.SetOption("pretype", System.Boolean.TrueString);

            //更新子图所有节点状态
            UpdateSubGraphStatus();
        }

        private void LoadOption()
        {
            if (string.IsNullOrEmpty(this.opControl.Option.GetOption("pretype")))
                return;
            bool index = Convert.ToBoolean(this.opControl.Option.GetOption("pretype"));
            this.checkBox1.Checked = index;
            this.checkBox2.Checked = index;
            this.checkBox3.Checked = index;

        }
        #endregion

        protected override void CancelButton_Click(object sender, EventArgs e)
        {
            //bool isReady = this.opControl.Status == ElementStatus.Done || this.opControl.Status == ElementStatus.Ready;
            //if (isReady && !this.checkBox1.Checked && !this.checkBox2.Checked && !this.checkBox3.Checked)
            //{
            //    this.opControl.Status = ElementStatus.Null;
            //}
            //opControl.Option.OptionValidating();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();

        }
    }
}
