using C2.Business.Option;
using C2.Core;
using C2.Globalization;
using C2.Model;
using C2.Model.Widgets;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace C2.Dialogs.Base
{
    public partial class C2BaseOperatorView : BaseOperatorView
    {
        protected OperatorWidget operatorWidget;
        protected string dataSourceFFP0;            // 左表数据源路径
        protected string[] nowColumnsName0;         // 当前左表(pin0)数据源表头字段(columnName)
        protected List<string> oldOutName0;         // 上一次（左输出）输出表头字段
        protected List<int> oldOutList0;            // 上一次用户选择的左表输出字段的索引
        protected List<string> selectedColumns;     // 本次配置用户选择的输出字段名称
        protected string oldOptionDictStr;          // 旧配置字典的字符串表述
        protected int ColumnCount { get => this.tableLayoutPanel1.ColumnCount; }       // 有增减条件的表格步长
        protected DataItem dataInfo; // 加载左右表数据源基本信息: FFP, Description, EXTType, encoding, sep等
        protected List<ComboBox> comboBoxes;
        protected string[] firstDataSourceColumns { get; set; }  //第一个入度的表头配置
        public C2BaseOperatorView()
        {
            operatorWidget = null;
            oldOptionDictStr = String.Empty;
            dataSourceFFP0 = String.Empty;
            nowColumnsName0 = new string[0];
            oldOutName0 = new List<string>();
            oldOutList0 = new List<int>();
            selectedColumns = new List<string>();
            dataInfo = null;
            firstDataSourceColumns = new string[0];
            InitializeComponent();
        }
        public C2BaseOperatorView(OperatorWidget operatorWidget) : this()
        {
            this.operatorWidget = operatorWidget;
            //oldOptionDictStr = operatorWidget.Option.ToString();
            comboBoxes = new List<ComboBox>() { this.comboBox0 };
        }

        protected void InitDataSource()
        {
            dataInfo = operatorWidget.DataSourceItem;

            this.dataSourceFFP0 = dataInfo.FilePath;
            this.dataSourceTB0.Text = dataInfo.FileName;
            BcpInfo bcpInfo = new BcpInfo(dataSourceFFP0, dataInfo.FileEncoding, dataInfo.FileSep.ToString().ToCharArray());
            firstDataSourceColumns = bcpInfo.ColumnArray;
            if (!(bcpInfo.ColumnArray.Length == 1 && bcpInfo.ColumnArray[0] == ""))//排除表头为空的情况
                this.nowColumnsName0 = bcpInfo.ColumnArray;
            SetTextBoxName(this.dataSourceTB0);

        }

        protected void SetTextBoxName(TextBox textBox)
        {
            string dataName = textBox.Text;
            int maxLength = 18;
            MatchCollection chs = Regex.Matches(dataName, "[\u4E00-\u9FA5]");
            int sumcount = chs.Count * 2;
            int sumcountDigit = Regex.Matches(dataName, "[a-zA-Z0-9]").Count;

            //防止截取字符串时中文乱码
            foreach (System.Text.RegularExpressions.Match mc in chs)
            {
                if (dataName.IndexOf(mc.ToString()) == maxLength)
                {
                    maxLength -= 1;
                    break;
                }
            }

            if (sumcount + sumcountDigit > maxLength)
            {
                textBox.Text = ConvertUtil.GB2312.GetString(ConvertUtil.GB2312.GetBytes(dataName), 0, maxLength) + "...";
            }
        }

        protected virtual void SaveOption()
        {
        }

        protected virtual bool IsOptionNotReady()
        {

            return false;
        }
        void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        protected virtual void ConfirmButton_Click(object sender, EventArgs e)
        {

            if (IsOptionNotReady()) return;
            if (IsIllegalFieldName()) return;
            if (IsDuplicateSelect()) return;//数据标准化窗口
            SaveOption();
            this.DialogResult = DialogResult.OK;

            operatorWidget.OpName = operatorWidget.DataSourceItem.FileName + "-" + Lang._(operatorWidget.OpType.ToString());
            string path = Path.Combine(Global.WorkspaceDirectory, String.Format("{0}_结果{1}.bcp", operatorWidget.OpName, DateTime.Now.ToString("yyyyMMdd_hhmmss")));
            string name = Path.GetFileNameWithoutExtension(path);
            char separator = OpUtil.DefaultSeparator;
            operatorWidget.ResultItem = new DataItem(path, name, separator, OpUtil.Encoding.UTF8, OpUtil.ExtType.Text);
        }

        protected bool IsIllegalFieldName()
        {
            bool isIllegal = true;
            foreach (ComboBox cbb in this.comboBoxes)
            {
                if (String.IsNullOrEmpty(cbb.Text))
                    continue;
                if (!cbb.Items.Contains(cbb.Text))
                {
                    cbb.Text = String.Empty;
                    MessageBox.Show("未输入正确字段名，请从下拉列表中选择正确字段名");
                    return isIllegal;
                }
                if (IsIllegalCharacter(cbb))
                {
                    MessageBox.Show("字段名中包含分隔符TAB，请检查与算子相连数据源的分隔符选择是否正确");
                    return isIllegal;
                }
            }
            return !isIllegal;
        }

        protected bool IsIllegalCharacter(Control control)
        {
            if (control.Text.Contains('\t'))
            {
                control.Text = String.Empty;
                return true;
            }
            return false;
        }

        protected virtual bool IsDuplicateSelect()
        {
            return false;
        }
    }
}
