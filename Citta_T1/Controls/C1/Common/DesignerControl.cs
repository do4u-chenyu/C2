using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C2.Model;
using C2.Model.MindMaps;
using C2.Model.Widgets;

namespace C2.Controls.Common
{
    public partial class DesignerControl : UserControl
    {
        Topic _SelectedTopic;
        DataItem _SelectedDataSource;

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

        public DataItem SelectedDataSource
        {
            get { return _SelectedDataSource; }
            set { _SelectedDataSource = value; }
        }

        private void OnSelectedTopicChanged()
        {
            if(SelectedTopic == null)
            {
                this.topicName.Text = "未选中主题";
                this.dataSourceCombo.Text = "";
                this.dataSourceCombo.Items.Clear();
                this.operatorName.Text = "无选择算子";
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
            DataSourceWidget dtw = SelectedTopic.FindWidget<DataSourceWidget>();
            if (dtw != null)
            {
                //TODO
                //dtw.选中数据源;
                DataItem d1 = new DataItem("d:\file1.txt", "file1", "\t", "UTF8", "datasource");
                SelectedDataSource = d1;
                this.dataSourceCombo.Text = d1.FileName;
            }
        }

        private void SetComboDataSource()
        {
            //TODO
            //数据大纲，父类所有数据源
            List<DataItem> di = new List<DataItem>();
            DataItem d1 = new DataItem("d:\file1.txt", "file1", "\t", "UTF8", "datasource");
            DataItem d2 = new DataItem("d:\file2.txt", "file2", "\t", "UTF8", "datasource");
            this.dataSourceCombo.Items.Clear();
            foreach(DataItem dataItem in di)
            {
                this.dataSourceCombo.Items.Add(dataItem.FileName);
            }
            
        }

        private void SetSelectedOperator()
        {
            OperatorWidget opw = SelectedTopic.FindWidget<OperatorWidget>();
            this.operatorName.Text = opw != null ? opw.OpType : "未添加算子";
        }

        public DesignerControl()
        {
            InitializeComponent();
        }
    }
}
