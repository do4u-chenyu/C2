using C2.Controls.Move.Op;
using C2.Dialogs.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C2.OperatorViews
{
    public partial class AnalysisOperatorView :  C1BaseOperatorView
    {
        public AnalysisOperatorView(MoveOpControl opControl) : base(opControl)
        {
            InitializeComponent();

            if (opControl.OperatorDimension() == 2)
            {
                this.dataSourceTB1.Visible = true;
                this.outListCCBL1.Visible = true;
                this.Text = "数据分析2";
                this.Icon = Properties.Resources.custom_icon;
                this.comboBox2.Items.AddRange(new object[] { "关键词分析" });
            }
            else
            {
                this.Text = "数据分析1";
                this.label2.Location = new Point(this.label2.Location.X, this.label2.Location.Y - 30);
                this.comboBox2.Location = new Point(this.comboBox2.Location.X, this.comboBox2.Location.Y - 30);
                this.comboBox2.Items.AddRange(new object[] { "同群分析", "银行卡提取" });
            }

            InitializeDataSource();//初始化配置内容
            LoadOption();
        }


        #region 配置信息的保存与加载
        protected override void SaveOption()
        {
            this.opControl.Option.Clear();
            this.opControl.Option.SetOption("analysisType", this.comboBox2.Text);

            this.opControl.Option.SetOption("columnname0", opControl.FirstDataSourceColumns);
            if (opControl.OperatorDimension() == 2)
            {
                this.opControl.Option.SetOption("columnname1", opControl.SecondDataSourceColumns);
            }
            //更新子图所有节点状态
            UpdateSubGraphStatus();
        }

        private void LoadOption()
        {
            if (string.IsNullOrEmpty(this.opControl.Option.GetOption("analysisType")))
                return;
            this.comboBox2.Text = this.opControl.Option.GetOption("analysisType");
        }
        #endregion

        protected override void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }
    }
}
