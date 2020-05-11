using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
        private LogUtil log = LogUtil.GetInstance("SortOperatorView");
        public SortOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
          
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
            this.oldOptionDict = string.Join(",", this.opControl.Option.OptionDict.ToList());

            SetTextBoxName(this.dataInfo);
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
            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Null, EnType(encoding));
            string column = bcpInfo.columnLine;
            this.columnName = column.Split(separator);
            this.outList = Enumerable.Range(0,this.columnName.Length).ToList();
            foreach (string name in columnName)
                this.sortField.Items.Add(name);
            CompareDataSource();
            this.opControl.SingleDataSourceColumns = column;
            this.opControl.Option.SetOption("columnname", this.opControl.SingleDataSourceColumns);
        }
        private void CompareDataSource()
        {
            //新数据源与旧数据源表头不匹配，对应配置内容是否情况进行判断
            if (this.opControl.Option.GetOption("columnname") == "") return;
            string[] oldColumnList = this.opControl.Option.GetOption("columnname").Split('\t');
            try
            {
                if (this.opControl.Option.GetOption("sortfield") != "")
                {
                    int index = Convert.ToInt32(this.opControl.Option.GetOption("sortfield"));
                    if (index > this.columnName.Length - 1 || oldColumnList[index] != this.columnName[index])
                        this.opControl.Option.OptionDict.Remove("sortfield");
                }
            }
            catch (Exception ex) { log.Error(ex.Message); };
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
            if (this.oldCheckedItems[0] != this.repetition.Checked)
                Global.GetMainForm().SetDocumentDirty();
            else if (this.oldCheckedItems[1] != this.noRepetition.Checked)
                Global.GetMainForm().SetDocumentDirty();
            else if (this.oldCheckedItems[2] != this.ascendingOrder.Checked)
                Global.GetMainForm().SetDocumentDirty();
            else if (this.oldCheckedItems[3] != this.descendingOrder.Checked)
                Global.GetMainForm().SetDocumentDirty();
            else if (!this.oldSort.SequenceEqual(this.sortField.Text))
                Global.GetMainForm().SetDocumentDirty();
            else if(this.oldFirstRow!=this.firstRow.Text)
                Global.GetMainForm().SetDocumentDirty();
            else if(this.oldEndRow!=this.endRow.Text)
                Global.GetMainForm().SetDocumentDirty();

            //生成结果控件,创建relation,bcp结果文件
            ModelElement hasResutl = Global.GetCurrentDocument().SearchResultOperator(this.opControl.ID);
            if (hasResutl == null)
            {
                Global.GetOptionDao().CreateResultControl(this.opControl, this.columnName.ToList());
                return;
            }
            //输出变化，重写BCP文件
            if (hasResutl != null && !this.oldColumnName.SequenceEqual(this.columnName))
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
            this.opControl.Option.SetOption("sortfield", this.sortField.SelectedIndex.ToString());
            this.opControl.Option.SetOption("repetition", this.repetition.Checked.ToString());
            this.opControl.Option.SetOption("noRepetition", this.noRepetition.Checked.ToString());
            this.opControl.Option.SetOption("ascendingOrder", this.ascendingOrder.Checked.ToString());
            this.opControl.Option.SetOption("descendingOrder", this.descendingOrder.Checked.ToString());
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
            }   
            if (this.opControl.Option.GetOption("repetition") != "")
                this.repetition.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("repetition"));
            if (this.opControl.Option.GetOption("noRepetition") != "")
                this.noRepetition.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("noRepetition"));
            if (this.opControl.Option.GetOption("ascendingOrder") != "")
                this.ascendingOrder.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("ascendingOrder"));
            if (this.opControl.Option.GetOption("descendingOrder") != "")
                this.descendingOrder.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("descendingOrder"));
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
                Global.GetOptionDao().NonNumeric_ControlText(this.firstRow);
        }

        private void firstRow_Leave(object sender, EventArgs e)
        {
            Global.GetOptionDao().NonNumeric_ControlText(this.firstRow);
        }

        private void endRow_Leave(object sender, EventArgs e)
        {
            Global.GetOptionDao().NonNumeric_ControlText(this.endRow);
        }

        private void endRow_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                Global.GetOptionDao().NonNumeric_ControlText(this.endRow);
        }
        #endregion
    }
}
