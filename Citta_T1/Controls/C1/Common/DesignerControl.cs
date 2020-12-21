using C2.Controls.MapViews;
using C2.Core;
using C2.Dialogs.Base;
using C2.Dialogs.C2OperatorViews;
using C2.Globalization;
using C2.Model;
using C2.Model.Documents;
using C2.Model.MindMaps;
using C2.Model.Widgets;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace C2.Controls.Common
{
    public partial class DesignerControl: UserControl
    {
        private Topic _SelectedTopic;
        ToolTip DcToolTip;
        public MindMapView MindmapView { get; set; }
        public OperatorWidget OpWidget { get; set; }
        //选中节点
        public Topic SelectedTopic
        {
            get { return _SelectedTopic; }
            set
            {
                if (_SelectedTopic != value)
                {
                    Topic old = _SelectedTopic;
                    _SelectedTopic = value;
                    OnTopicChanged(old);
                }
            }
        }
        public string SelectedOperator { get; set; }//选中算子
        public DataItem SelectedDataSource { get; set; }//选中数据
        public List<DataItem> ComboDataSource { get; set; }//下拉数据
        public List<OpType> ComboOperator { get; set; }//下拉算子

        public DesignerControl()
        {
            InitializeComponent();
            Font = UITheme.Default.DefaultFont;
            ComboDataSource = new List<DataItem>();
            DcToolTip = new ToolTip();
            InitComboOperator();
        }

        private void InitComboOperator()
        {
            ComboOperator = new List<OpType>();
            foreach (OpType opType in Enum.GetValues(typeof(OpType)))
            {
                if(opType==OpType.Null)
                    continue;
                string tmpOpType = Lang._(opType.ToString());
                ComboOperator.Add(opType);
                this.operatorCombo.Items.Add(tmpOpType);
            }
        }

        public void SetSelectedTopicDesign(Topic topic,MindMapView mindmapview)
        {
            SelectedTopic = topic;
            MindmapView = mindmapview;
            if(SelectedTopic == null)
            {
                this.topicName.Text = "未选中节点";
                DcToolTip.SetToolTip(this.topicName, "");
                this.dataSourceCombo.Text = String.Empty;
                this.dataSourceCombo.Items.Clear();
                this.operatorCombo.Text = String.Empty;
                this.operatorCombo.Items.Clear();
            }
            else
            {
                OpWidget = SelectedTopic.FindWidget<OperatorWidget>();
                SetSelectedTopic();//设置选中主题
                SetComboDataSource();//设置数据源下拉选项
                SetComboOperator();//设置算子下拉选项
                SetSelectedOperator();//设置选中算子
                SetSelectedDataSource();//设置选中数据源
            }

        }

        private void SetSelectedTopic()
        {
            this.topicName.Text = SelectedTopic.Text;
            DcToolTip.SetToolTip(this.topicName, SelectedTopic.Text);
        }

        private void SetSelectedDataSource()
        {
            if(OpWidget == null)
            {
                SelectedDataSource = null;
                this.dataSourceCombo.Text = string.Empty;
                return;
            }

            DataItem d1 = OpWidget.DataSourceItem;
            if(ComboDataSource != null && d1 != null && ComboDataSource.Find(d => d.FilePath == d1.FilePath) != null)
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

        private void SetComboOperator()
        {
            this.operatorCombo.Items.Clear();
            ComboOperator.ForEach(o => this.operatorCombo.Items.Add(Lang._(o.ToString())));
        }

        private void SetComboDataSource()
        {
            this.dataSourceCombo.Items.Clear();

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
            if (OpWidget != null && OpWidget.OpType != OpType.Null)
            {
                this.operatorCombo.Text = Lang._(OpWidget.OpType.ToString());
                SelectedOperator = Lang._(OpWidget.OpType.ToString());
            }
            else
            {
                this.operatorCombo.Text = string.Empty;
                SelectedOperator = string.Empty;
            }
        }


        private void Button1_Click(object sender, System.EventArgs e)
        {
            if(this.topicName.Text == "未选中节点")
            {
                HelpUtil.ShowMessageBox("未选中节点，请选中节点后再配置","未选中节点");
                return;
            }

            //模型算子选中时，可以不用选中数据源
            if ( SelectedDataSource == null || SelectedDataSource.IsEmpty() || this.dataSourceCombo.SelectedIndex<0)
            {
                HelpUtil.ShowMessageBox("未选中数据源,请添加后再配置", "未选中数据源");
                return;
            }

            if (string.IsNullOrEmpty(SelectedOperator))
            {
                HelpUtil.ShowMessageBox("未选择算子,请添加后再配置", "未选择算子");
                return;
            }

            string message = FileUtil.FileExistOrUse(SelectedDataSource.FilePath);
            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (OpWidget == null)
            {
                SelectedTopic.Widgets.Add(new OperatorWidget());
                OpWidget = SelectedTopic.FindWidget<OperatorWidget>();
            }

            OpType tmpOpType = OpWidget.OpType;
            DataItem tmpDataItem = OpWidget.DataSourceItem;

            OpWidget.OpType =ComboOperator[this.operatorCombo.SelectedIndex];
            OpWidget.DataSourceItem = ComboDataSource[this.dataSourceCombo.SelectedIndex];
            Cursor tempCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            C2BaseOperatorView dialog = GenerateOperatorView();
            if (dialog == null)
                return;
            DialogResult dr = dialog.ShowDialog(this);
            if (dr == DialogResult.OK)
                OpWidget.Status = OpStatus.Ready;
            else if(dr == DialogResult.Cancel)
            {
                OpWidget.OpType = tmpOpType;
                OpWidget.DataSourceItem = tmpDataItem;
                SetSelectedTopicDesign(SelectedTopic, MindmapView);
            }
            this.Cursor = tempCursor;
        }

        private C2BaseOperatorView GenerateOperatorView()
        {
            switch (OpWidget.OpType)
            {
                case OpType.MaxOperator:return new C2MaxOperatorView(OpWidget);
                case OpType.CustomOperator:return new C2CustomOperatorView(OpWidget);
                case OpType.MinOperator:return new C2MinOperatorView(OpWidget);
                case OpType.AvgOperator:return new C2AvgOperatorView(OpWidget);
                case OpType.DataFormatOperator:return new C2DataFormatOperatorView(OpWidget);
                case OpType.RandomOperator:return new C2RandomOperatorView(OpWidget);
                case OpType.FreqOperator:return new C2FreqOperatorView(OpWidget);
                case OpType.SortOperator:return new C2SortOperatorView(OpWidget);
                case OpType.FilterOperator:return new C2FilterOperatorView(OpWidget);
                case OpType.GroupOperator:return new C2GroupOperatorView(OpWidget);
                case OpType.PythonOperator:return new C2PythonOperatorView(OpWidget);
                case OpType.SqlOperator:return new C2SqlOperatorView(OpWidget);
                default:return null;
            }
        }

        private void DataSourceCombo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (ComboDataSource == null || this.dataSourceCombo.SelectedIndex < 0 || ComboDataSource.Count <= this.dataSourceCombo.SelectedIndex )
                return;
            SelectedDataSource = ComboDataSource[this.dataSourceCombo.SelectedIndex];
        }

        private void OperatorCombo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (ComboOperator == null || ComboOperator.Count <= this.operatorCombo.SelectedIndex || this.operatorCombo.SelectedIndex < 0)
                return;
            SelectedOperator = Lang._(ComboOperator[this.operatorCombo.SelectedIndex].ToString());
        }
        private void OnTopicChanged(Topic old)
        {
            if (old != null)
            {
                old.TextChanged -= new EventHandler(Topic_TextChanged);
            }

            if (SelectedTopic != null)
            {
                SelectedTopic.TextChanged += new EventHandler(Topic_TextChanged);
            }
        }
        private void Topic_TextChanged(object sender, EventArgs e)
        {
            if (SelectedTopic != null)
            {
                this.topicName.Text = SelectedTopic.Text;
                DcToolTip.SetToolTip(this.topicName, SelectedTopic.Text);
            }
        }
    }
}
