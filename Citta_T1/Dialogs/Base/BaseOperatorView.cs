using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using C2.Utils;

namespace C2.Dialogs.Base
{

    public partial class BaseOperatorView : Form
    {
        protected string dataSourceFFP0;            // 左表数据源路径
        protected string dataSourceFFP1;            // 右表数据源路径
        protected string[] nowColumnsName0;         // 当前左表(pin0)数据源表头字段(columnName)
        protected string[] nowColumnsName1;         // 当前右表(pin1)数据源表头字段
        protected string[] logicItems = new string[] { };
        protected readonly Dictionary<int, int> filterDict = new Dictionary<int, int>();
        protected List<ComboBox> comboBoxes;

        public BaseOperatorView()
        {
            InitializeComponent();
        }

        private void DataSourceTB1_MouseHover(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(dataSourceTB1, this.dataSourceFFP1);
        }

        private void DataSourceTB0_MouseHover(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(dataSourceTB0, this.dataSourceFFP0);
        }

        //C2 filter算子需要的方法
        protected string[] comparedItems = new string[] { };
        protected void GetLeftSelectedItemIndex(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            GetSelectedItemIndex(comboBox, nowColumnsName0);
        }
        protected void GetRightSelectedItemIndex(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            GetSelectedItemIndex(comboBox, nowColumnsName1);
        }
        protected void GetComparedSelectedItemIndex(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            GetSelectedItemIndex(comboBox, comparedItems);
        }
        protected void GetLogicalSelectedItemIndex(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            GetSelectedItemIndex(comboBox, logicItems);
        }
        protected void GetSelectedItemIndex(ComboBox comboBox, string[] nowColumns)
        {
            if (nowColumns.Length == 0)
                return;
            List<string> filterItems = new List<string>();
            for (int i = 0; i < comboBox.Items.Count; i++)
                filterItems.Add(comboBox.Items[i].ToString());


            // 下拉列表中选取值
            if (filterItems.SequenceEqual(nowColumns))
            {
                comboBox.Tag = comboBox.SelectedIndex.ToString();
                return;
            }

            // 保存下拉列表选择字段的索引
            if (filterDict.Keys.Contains(comboBox.SelectedIndex))
                comboBox.Tag = filterDict[comboBox.SelectedIndex];

        }
        protected void GetSelectedItemIndex(object sender, EventArgs e)
        {
            (sender as ComboBox).Tag = (sender as ComboBox).SelectedIndex.ToString();
        }

        #region 下拉列表关闭后 下拉列表内容重置和选中的索引校验
        public void LeftComboBox_ClosedEvent(object sender, EventArgs e)
        { ComboBox_ClosedEvent(sender as ComboBox, nowColumnsName0); }
        public void RightComboBox_ClosedEvent(object sender, EventArgs e)
        { ComboBox_ClosedEvent(sender as ComboBox, nowColumnsName1); }
        public void ComparedComboBox_ClosedEvent(object sender, EventArgs e)
        { ComboBox_ClosedEvent(sender as ComboBox, comparedItems); }
        public void LogicalComboBox_ClosedEvent(object sender, EventArgs e)
        { ComboBox_ClosedEvent(sender as ComboBox, logicItems); }
        public void ComboBox_ClosedEvent(ComboBox comboBox, string[] nowColumns)
        {

            if (nowColumns.Length == 0)
                return;

            // 恢复下拉列表原始字段
            comboBox.Items.Clear();
            comboBox.Items.AddRange(nowColumns);
            if (comboBox.Tag != null && !comboBox.Tag.ToString().Equals("-1") && ConvertUtil.IsInt(comboBox.Tag.ToString()))
            {
                int index = Convert.ToInt32(comboBox.Tag.ToString());
                comboBox.SelectedIndex = index;
                comboBox.Text = nowColumns[index];
            }

            // 手动将字段全部输入，这时候selectItem.index=-1,我们将设成下拉列表第一个匹配字段的索引
            if (comboBox.SelectedIndex == -1 && !string.IsNullOrEmpty(comboBox.Text))
            {
                for (int i = 0; i < nowColumns.Length; i++)
                {
                    if (nowColumns[i].Equals(comboBox.Text))
                    {
                        comboBox.SelectedIndex = i;
                        comboBox.Tag = i;
                        break;
                    }
                }
            }
        }
        #endregion

        public void LeftComboBox_TextUpdate(object sender, EventArgs e)
        { ComboBox_TextUpdate(sender as ComboBox, nowColumnsName0); }
        public void RightComboBox_TextUpdate(object sender, EventArgs e)
        { ComboBox_TextUpdate(sender as ComboBox, nowColumnsName1); }
        public void ComparedComboBox_TextUpdate(object sender, EventArgs e)
        { ComboBox_TextUpdate(sender as ComboBox, comparedItems); }
        public void LogicalComboBox_TextUpdate(object sender, EventArgs e)
        { ComboBox_TextUpdate(sender as ComboBox, logicItems); }
        public void ComboBox_TextUpdate(ComboBox comboBox, string[] nowColumns)
        {
            comboBox.SelectedIndex = -1;
            comboBox.Tag = null;
            int count = nowColumns.Length;
            if (comboBox.Text == "" || count == 0)
            {
                comboBox.DroppedDown = false;
                return;
            }

            filterDict.Clear();

            //每次搜索文本改变，就是对字典重新赋值
            comboBox.Items.Clear();
            List<string> filterItems = new List<string>();

            for (int i = 0; i < count; i++)
            {
                if (nowColumns[i].Contains(comboBox.Text))
                {
                    filterItems.Add(nowColumns[i]);
                    // 模糊搜索得到的下拉列表字段索引对应原始下拉列表字段索引
                    filterDict[filterItems.Count - 1] = i;
                }
            }

            comboBox.Items.AddRange(filterItems.ToArray());
            comboBox.SelectionStart = comboBox.Text.Length;
            comboBox.DroppedDown = true;
            //保持鼠标指针原来状态，有时候鼠标指针会被下拉框覆盖，所以要进行一次设置。
            Cursor = Cursors.Default;
        }


        protected ComboBox NewAndORComboBox()
        {
            ComboBox combox = NewComboBox();
            combox.Anchor = AnchorStyles.None;
            logicItems = new string[] { "AND", "OR" };
            combox.Items.AddRange(logicItems);
            combox.SelectionChangeCommitted += new EventHandler(this.GetLogicalSelectedItemIndex);
            combox.TextUpdate += new System.EventHandler(LogicalComboBox_TextUpdate);
            combox.DropDownClosed += new System.EventHandler(LogicalComboBox_ClosedEvent);
            return combox;
        }
        protected ComboBox NewComboBox()
        {
            ComboBox combox = new ComboBox
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("微软雅黑", 8f, FontStyle.Regular)
            };
            comboBoxes.Add(combox);
            return combox;
        }
    }
}
