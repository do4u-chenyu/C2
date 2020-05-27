using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{
    public partial class SortOperatorView : Form
    {
        private MoveOpControl opControl;
        private string dataPath;
        private string[] columnName;
        private string oldOptionDict;
        private string oldSort;
        private List<bool> oldCheckedItems = new List<bool>();
        private string oldFirstRow;
        private string oldEndRow;
        private List<int> outList;
        private List<string> oldColumnName;
        private static LogUtil log = LogUtil.GetInstance("SortOperatorView");
        private OptionInfoCheck optionInfoCheck;
        public SortOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
            this.optionInfoCheck = new OptionInfoCheck();
            this.opControl = opControl;
            dataPath = "";
           
            this.oldColumnName = this.opControl.Option.GetOption("columnname").Split('\t').ToList();
            InitOptionInfo();
            LoadOption();
            this.oldFirstRow = this.firstRow.Text;
            this.oldEndRow = this.endRow.Text;
            this.oldSort = this.sortField.Text;
            this.oldCheckedItems.Add(this.repetition.Checked);
            this.oldCheckedItems.Add(this.noRepetition.Checked);
            this.oldCheckedItems.Add(this.ascendingOrder.Checked);
            this.oldCheckedItems.Add(this.descendingOrder.Checked);
            this.oldCheckedItems.Add(this.sortByString.Checked);
            this.oldCheckedItems.Add(this.sortByNum.Checked);
            this.oldOptionDict = string.Join(",", this.opControl.Option.OptionDict.ToList());
            this.sortField.Leave += new System.EventHandler(optionInfoCheck.Control_Leave);
            this.sortField.KeyUp += new System.Windows.Forms.KeyEventHandler(optionInfoCheck.Control_KeyUp);
            SetTextBoxName(this.dataInfo);
            //selectindex会在某些不确定情况触发，这种情况是不期望的
            this.sortField.SelectionChangeCommitted += new System.EventHandler(Global.GetOptionDao().GetSelectedItemIndex);
        }
      
        #region 配置初始化
        private void InitOptionInfo()
        {
            Dictionary<string, string> dataInfo = Global.GetOptionDao().GetDataSourceInfo(this.opControl.ID);
            if (dataInfo.ContainsKey("dataPath0") && dataInfo.ContainsKey("encoding0"))
            {
                this.dataPath = dataInfo["dataPath0"];
                this.dataInfo.Text = Path.GetFileNameWithoutExtension(this.dataPath);
                this.toolTip1.SetToolTip(this.dataInfo, this.dataInfo.Text);
                SetOption(this.dataPath, this.dataInfo.Text, dataInfo["encoding0"], dataInfo["separator0"].ToCharArray());
            }
        }
        private void SetOption(string path, string dataName, string encoding, char[] separator)
        {
            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Empty, EnType(encoding));
            string column = bcpInfo.columnLine;
            this.columnName = column.Split(separator);
            this.outList = Enumerable.Range(0,this.columnName.Length).ToList();
            foreach (string name in columnName)
                this.sortField.Items.Add(name);

            //新旧数据源比较，是否清空窗口配置
            List<string> keys = new List<string>(this.opControl.Option.OptionDict.Keys);
            Global.GetOptionDao().IsSingleDataSourceChange(this.opControl, this.columnName, "sortfield");

            this.opControl.SingleDataSourceColumns = String.Join("\t", this.columnName);
            this.opControl.Option.SetOption("columnname", this.opControl.SingleDataSourceColumns);
        }
       
        private DSUtil.Encoding EnType(string type)
        { return (DSUtil.Encoding)Enum.Parse(typeof(DSUtil.Encoding), type); }

        public void SetTextBoxName(TextBox textBox)
        {
            string dataName = textBox.Text;
            int maxLength = 18;
            MatchCollection chs = Regex.Matches(dataName, "[\u4E00-\u9FA5]");
            int sumcount = chs.Count * 2;
            int sumcountDigit = Regex.Matches(dataName, "[a-zA-Z0-9]").Count;

            //防止截取字符串时中文乱码
            foreach (Match mc in chs)
            {
                if (dataName.IndexOf(mc.ToString()) == maxLength)
                {
                    maxLength -= 1;
                    break;
                }
            }

            if (sumcount + sumcountDigit > maxLength)
            {
                textBox.Text = System.Text.Encoding.GetEncoding("GB2312").GetString(System.Text.Encoding.GetEncoding("GB2312").GetBytes(dataName), 0, maxLength) + "...";
            }
        }
        #endregion
        #region 添加取消
        private void confirmButton_Click(object sender, EventArgs e)
        {
            if (this.dataInfo.Text == "") return;
            if (this.sortField.Text == "")
            {
                MessageBox.Show("请选择排序字段!");
                return;
            }
            this.DialogResult = DialogResult.OK;
           
            SaveOption();

            //内容修改，引起文档dirty 
            if (this.oldOptionDict != string.Join(",", this.opControl.Option.OptionDict.ToList()))
                Global.GetMainForm().SetDocumentDirty();

            //生成结果控件,创建relation,bcp结果文件
            ModelElement resultElement = Global.GetCurrentDocument().SearchResultElement(this.opControl.ID);
            if (resultElement == ModelElement.Empty)
            {
                Global.GetCreateMoveRsControl().CreateResultControl(this.opControl, this.columnName.ToList());
                return;
            }

            // 对应的结果文件置脏
            BCPBuffer.GetInstance().SetDirty(resultElement.GetFullFilePath());

            //输出变化，重写BCP文件
            if (!this.oldColumnName.SequenceEqual(this.columnName))
                Global.GetOptionDao().IsNewOut(this.columnName.ToList(), this.opControl.ID);

        }
       
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion

        #region 配置信息的保存与加载
        private void SaveOption()
        {

            this.opControl.Option.SetOption("outfield", String.Join(",",this.outList));
            this.opControl.Option.SetOption("sortfield", this.sortField.Tag == null ? this.sortField.SelectedIndex.ToString() : this.sortField.Tag.ToString());
            this.opControl.Option.SetOption("repetition", this.repetition.Checked.ToString());
            this.opControl.Option.SetOption("noRepetition", this.noRepetition.Checked.ToString());
            this.opControl.Option.SetOption("ascendingOrder", this.ascendingOrder.Checked.ToString());
            this.opControl.Option.SetOption("descendingOrder", this.descendingOrder.Checked.ToString());
            this.opControl.Option.SetOption("sortByNum", this.sortByNum.Checked.ToString());
            this.opControl.Option.SetOption("sortByString", this.sortByString.Checked.ToString());
            this.opControl.Option.SetOption("firstRow", this.firstRow.Text);         
            this.opControl.Option.SetOption("endRow", this.endRow.Text);


            if (this.oldOptionDict == string.Join(",", this.opControl.Option.OptionDict.ToList()) && this.opControl.Status != ElementStatus.Null)
                return;
            else
                this.opControl.Status = ElementStatus.Ready;

        }

        private void LoadOption()
        {
           
            if (this.opControl.Option.GetOption("sortfield") != "")
            {
                int index = Convert.ToInt32(this.opControl.Option.GetOption("sortfield"));
                this.sortField.Text = this.sortField.Items[index].ToString();
                this.sortField.Tag = index.ToString();
            }   
            if (this.opControl.Option.GetOption("repetition") != "")
                this.repetition.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("repetition"));
            if (this.opControl.Option.GetOption("noRepetition") != "")
                this.noRepetition.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("noRepetition"));
            if (this.opControl.Option.GetOption("ascendingOrder") != "")
                this.ascendingOrder.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("ascendingOrder"));
            if (this.opControl.Option.GetOption("descendingOrder") != "")
                this.descendingOrder.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("descendingOrder"));
            if (this.opControl.Option.GetOption("sortByNum") != "")
                this.sortByNum.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("sortByNum"));
            if (this.opControl.Option.GetOption("sortByString") != "")
                this.sortByString.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("sortByString"));
            if (this.opControl.Option.GetOption("firstRow") != "")
                this.firstRow.Text = this.opControl.Option.GetOption("firstRow");
            if (this.opControl.Option.GetOption("endRow") != "")
                this.endRow.Text = this.opControl.Option.GetOption("endRow");

        }
        #endregion
        
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }
        private void dataInfo_MouseClick(object sender, MouseEventArgs e)
        {
            this.dataInfo.Text = Path.GetFileNameWithoutExtension(this.dataPath);
        }

        private void dataInfo_LostFocus(object sender, EventArgs e)
        {
            SetTextBoxName(this.dataInfo);
        }
        #region 输入非数字，警告
        private void firstRow_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                optionInfoCheck.NonNumeric_ControlText(this.firstRow);
        }

        private void firstRow_Leave(object sender, EventArgs e)
        {
            optionInfoCheck.NonNumeric_ControlText(this.firstRow);
        }

        private void endRow_Leave(object sender, EventArgs e)
        {
            optionInfoCheck.NonNumeric_ControlText(this.endRow);
        }

        private void endRow_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                optionInfoCheck.NonNumeric_ControlText(this.endRow);
        }
        #endregion

        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }
    }
}
