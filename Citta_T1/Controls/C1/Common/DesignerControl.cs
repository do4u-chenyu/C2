using C2.Controls.MapViews;
using C2.Dialogs.C2OperatorViews;
using C2.Model;
using C2.Model.MindMaps;
using C2.Model.Widgets;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace C2.Controls.Common
{
    public partial class DesignerControl : BorderPanel
    {
        private string[] ComboOperator;
        public Topic SelectedTopic { get; set; }
        public MindMapView MindmapView { get; set; }
        public OperatorWidget OpWidget { get; set; }
        public DataItem SelectedDataSource { get; set; }
        public string SelectedOperator { get; set; }
        public List<DataItem> ComboDataSource { get; set; }

        public DesignerControl()
        {
            InitializeComponent();
            Font = UITheme.Default.DefaultFont;
            ComboDataSource = new List<DataItem>();
            ComboOperator = new string[] { "最大值", "AI实践" };
        }


        public void SetSelectedTopicDesign(Topic topic,MindMapView mindmapview)
        {
            SelectedTopic = topic;
            MindmapView = mindmapview;
            if(SelectedTopic == null)
            {
                this.topicName.Text = "未选中主题";
                this.dataSourceCombo.Text = String.Empty;
                this.dataSourceCombo.Items.Clear();
                this.operatorCombo.Text = String.Empty;
            }
            else
            {
                OpWidget = SelectedTopic.FindWidget<OperatorWidget>();
                SetSelectedTopic();//设置选中主题√
                SetComboDataSource();//设置数据源下拉选项√
                SetSelectedOperator();//设置选中算子
                SetSelectedDataSource();//设置选中数据源
            }

        }

        private void SetSelectedTopic()
        {
            this.topicName.Text = SelectedTopic.Text;
        }

        private void SetSelectedDataSource()
        {
            /*
             * 1、当算子挂件不存在时，置空
             * 2、算子挂件存在时
             *      2.1  opw.DataSourceItem 为空，置空
             *      2.2                                  不为空，赋值
             *      
             *  算子挂件中存的数据源，和下拉数据源对比问题：
             *  (1)有数据源，有下拉数据源，比较是否包含
             *       包含：正常显示
             *       不包含：置空
             *  (2)至少有一个为空，直接置空
             */
            if(OpWidget == null)
            {
                SelectedDataSource = null;
                this.dataSourceCombo.Text = string.Empty;
                return;
            }

            DataItem d1 = OpWidget.DataSourceItem;
            if(ComboDataSource == null || d1 == null)
            {
                SelectedDataSource = null;
                this.dataSourceCombo.Text = string.Empty;
            }
            else if (ComboDataSource.Contains(d1))
            {
                SelectedDataSource = d1;
                this.dataSourceCombo.Text = d1.FileName;
            }
            else
            {
                SelectedDataSource = null;
                this.dataSourceCombo.Text = string.Empty;
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
            if(OpWidget != null && OpWidget.OpType != null)
            {
                this.operatorCombo.Text = OpWidget.OpType;
                SelectedOperator = OpWidget.OpType;
            }
            else
            {
                this.operatorCombo.Text = string.Empty;
                SelectedOperator = string.Empty;
            }
        }


        private void Button1_Click(object sender, System.EventArgs e)
        {
            if(this.topicName.Text == "未选中主题")
            {
                MessageBox.Show("未选中主题，请选中主题后再配置");
                return;
            }

            if (SelectedDataSource ==null || SelectedDataSource.IsEmpty())
            {
                MessageBox.Show("未选中数据源,请添加后再配置");
                return;
            }

            if (string.IsNullOrEmpty(SelectedOperator))
            {
                MessageBox.Show("未添加算子,请添加后再配置");
                return;
            }

            if (OpWidget == null)
            {
                MindmapView.AddOperator(new Topic[] { SelectedTopic });
                OpWidget = SelectedTopic.FindWidget<OperatorWidget>();
            }

            OpWidget.OpType = ComboOperator[this.operatorCombo.SelectedIndex];
            OpWidget.DataSourceItem = ComboDataSource[this.dataSourceCombo.SelectedIndex];

            switch (SelectedOperator)
            {
                case "最大值":
                    var dialog = new C2MaxOperatorView(OpWidget);
                    if(dialog.ShowDialog(this) == DialogResult.OK)
                        OpWidget.Status = OpStatus.Ready;
                    break;
                case "AI实践":
                    var dialog2 = new C2CustomOperatorView(OpWidget);
                    if (dialog2.ShowDialog(this) == DialogResult.OK)
                    {
                        OpWidget.OpName = OpWidget.DataSourceItem.FileName + "-" + OpWidget.OpType;
                        DataItem resultItem = OpWidget.ResultItem;
                        SelectedTopic.Widgets.Add(new ResultWidget());
                        OpWidget.Status = OpStatus.Ready;
                    }
                    break;
                default:
                    break;
            }

        }

        private void DataSourceCombo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            SelectedDataSource = ComboDataSource[this.dataSourceCombo.SelectedIndex];
        }

        private void OperatorCombo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            SelectedOperator = ComboOperator[this.operatorCombo.SelectedIndex];
        }
    }
}
