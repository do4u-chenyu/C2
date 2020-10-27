using C2.Model;
using C2.Model.MindMaps;
using C2.Model.Widgets;
using C2.OperatorViews;
using System.Collections.Generic;
using System.Windows.Forms;

namespace C2.Controls.Common
{
    public partial class DesignerControl : UserControl
    {
        Topic _SelectedTopic;

        public Topic SelectedTopic
        {
            get { return _SelectedTopic; }
            set
            {
                if (_SelectedTopic != value)
                {
                    _SelectedTopic = value;
                    OnSelectedTopicChanged();
                }
            }
        }

        public DataItem SelectedDataSource { get; set; }
        public string SelectedOperator { get; set; }
        public List<DataItem> ComboDataSource { get; set; }

        private void OnSelectedTopicChanged()
        {
            if(SelectedTopic == null)
            {
                this.topicName.Text = "未选中主题";
                this.dataSourceCombo.Text = "";
                this.dataSourceCombo.Items.Clear();
                this.operatorName.Text = "";
            }
            else
            {
                SetSelectedTopic();//设置选中主题
                SetSelectedDataSource();//设置选中数据源
                SetComboDataSource();
                SetSelectedOperator();
            }

        }

        private void SetSelectedTopic()
        {
            this.topicName.Text = SelectedTopic.Text;
        }

        private void SetSelectedDataSource()
        {
            OperatorWidget opw = SelectedTopic.FindWidget<OperatorWidget>();
            if (opw != null)
            {
                //TODO
                //dtw.选中数据源;
                DataItem d1 = opw.DataSourceItem;
                if (d1 != null)
                {
                    SelectedDataSource = d1;
                    this.dataSourceCombo.Text = d1.FileName;
                }
                else
                {
                    SelectedDataSource = null;
                    this.dataSourceCombo.Text = "请选择数据源";
                }
            }
        }

        private void SetComboDataSource()
        {
            this.dataSourceCombo.Items.Clear();

            //TODO
            //数据大纲，父类所有数据源,暂用固定列表模拟
            DataSourceWidget dtw = SelectedTopic.FindWidget<DataSourceWidget>();
            if (dtw != null)
            {
                List<DataItem> di = dtw.DataItems;
                foreach (DataItem dataItem in di)
                {
                    this.dataSourceCombo.Items.Add(dataItem.FileName);
                }
                ComboDataSource = di;
            }

        }

        private void SetSelectedOperator()
        {
            OperatorWidget opw = SelectedTopic.FindWidget<OperatorWidget>();

            this.operatorName.Text = opw != null ? opw.OpType : "";
            SelectedOperator = opw != null ? opw.OpType : "";
        }

        public DesignerControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if(this.topicName.Text == "未选中主题")
            {
                MessageBox.Show("未选中主题，请选中主题后再配置");
                return;
            }

            if (SelectedDataSource == null)
            {
                MessageBox.Show("未选中数据源,请添加后再配置");
                return;
            }

            if (string.IsNullOrEmpty(SelectedOperator))
            {
                MessageBox.Show("未添加算子,请添加后再配置");
                return;
            }

            switch (SelectedOperator)
            {
                case "最大值":
                    //new MaxOperatorView(SelectedTopic.FindWidget<OperatorWidget>()).ShowDialog();
                    break;
                case "排序":
                    //new SortOperatorView(SelectedTopic.FindWidget<OperatorWidget>()).ShowDialog();
                    break;
                default:
                    break;
            }

        }

        private void dataSourceCombo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            OperatorWidget opw = SelectedTopic.FindWidget<OperatorWidget>();

            if (opw != null)
            {
                opw.DataSourceItem = ComboDataSource[this.dataSourceCombo.SelectedIndex];
            }
        }
    }
}
