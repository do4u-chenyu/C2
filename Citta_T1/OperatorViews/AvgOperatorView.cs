using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move.Dt;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
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
    public partial class AvgOperatorView : Form
    {
        private MoveOpControl opControl;
        private string oldAvg;
        private string dataPath = "";
        private string[] columnName;
        private List<string> selectName;
        private string oldOptionDict;
        private static LogUtil log = LogUtil.GetInstance("AvgOperatorView");
        private string selectedIndex;
        private OptionInfoCheck optionInfoCheck;

        public AvgOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
            this.optionInfoCheck = new OptionInfoCheck();
            selectName = new List<string>();
            this.opControl = opControl;
            InitOptionInfor();
            LoadOption();
           
            this.oldAvg = this.AvgComBox.Text;
            this.oldOptionDict = string.Join(",", this.opControl.Option.OptionDict.ToList());

            SetTextBoxName(this.DataInfo);
        }
        #region 初始化配置
        private void InitOptionInfor()
        {
            int startID = -1;
            string encoding = "";
            char separator = '\t';
            List<ModelRelation> modelRelations = Global.GetCurrentDocument().ModelRelations;
            List<ModelElement> modelElements = Global.GetCurrentDocument().ModelElements;
            foreach (ModelRelation mr in modelRelations)
            {
                if (mr.EndID== this.opControl.ID)
                {
                    startID = mr.StartID;
                    break;
                }
            }
            foreach (ModelElement me in modelElements)
            {
                if (me.ID == startID)
                {
                    this.dataPath = me.GetFullFilePath();
                    if (me.GetControl is MoveDtControl)
                        separator = (me.GetControl as MoveDtControl).Separator;
                    //设置数据信息选项
                    this.DataInfo.Text = Path.GetFileNameWithoutExtension(this.dataPath);
                    this.toolTip1.SetToolTip(this.DataInfo, this.DataInfo.Text);
                    encoding = me.Encoding.ToString();
                    break;
                }
            }
            if (this.dataPath != "")
                SetOption(this.dataPath, this.DataInfo.Text, encoding, separator);

        }
        private void SetOption(string path, string dataName, string encoding,char separator)
        {
            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Null, EnType(encoding));
            string column = bcpInfo.columnLine;
            this.columnName = column.Split(separator);
            foreach (string name in this.columnName)
                this.AvgComBox.Items.Add(name);


            //新旧数据源比较，是否清空窗口配置
            List<string> keys = new List<string>(this.opControl.Option.OptionDict.Keys);
            foreach (string field in keys)
            {
                if (!field.Contains("columnname"))
                    Global.GetOptionDao().IsSingleDataSourceChange(this.opControl, this.columnName, field);
            }

            this.opControl.SingleDataSourceColumns =  this.columnName.ToList();
            this.opControl.Option.SetOption("columnname", String.Join("\t", this.columnName));
        }
      

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

        private void confirmButton_Click(object sender, EventArgs e)
        {
            //未设置字段警告
            if (this.DataInfo.Text == "") return;
            if (this.AvgComBox.Text == "")
            {
                MessageBox.Show("请选择平均值字段!");
                return;
            }
            this.DialogResult = DialogResult.OK;
           
            SaveOption();
            //内容修改，引起文档dirty
            if (this.oldOptionDict != string.Join(",", this.opControl.Option.OptionDict.ToList()))
                Global.GetMainForm().SetDocumentDirty();
            //生成结果控件,创建relation,bcp结果文件
            this.selectName.Add(this.AvgComBox.SelectedItem.ToString());
            ModelElement hasResutl = Global.GetCurrentDocument().SearchResultOperator(this.opControl.ID);
            if (hasResutl == null)
            { 
                Global.GetCreateMoveRsControl().CreateResultControl(this.opControl, this.selectName);
                return;
            }
            //输出变化，重写BCP文件
            List<string> oldColumn = new List<string>();
            oldColumn.Add(this.oldAvg);
            if (hasResutl != null &&  this.oldAvg != this.AvgComBox.Text)
                Global.GetOptionDao().IsModifyOut(oldColumn, this.selectName, this.opControl.ID);

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        #region 配置信息的保存与加载
        private void SaveOption()
        {

            this.opControl.Option.SetOption("avgfield", this.selectedIndex == null ? this.AvgComBox.SelectedIndex.ToString() : this.selectedIndex);
            this.opControl.Option.SetOption("outfield", this.AvgComBox.SelectedIndex.ToString());
            if (this.oldOptionDict == string.Join(",", this.opControl.Option.OptionDict.ToList()) && this.opControl.Status != ElementStatus.Null)
                return;
            else
                this.opControl.Status = ElementStatus.Ready;

        }

        private void LoadOption()
        {
            if (this.opControl.Option.GetOption("avgfield") != "")
            {
                int index = Convert.ToInt32(this.opControl.Option.GetOption("avgfield"));
                this.AvgComBox.Text = this.AvgComBox.Items[index].ToString();
                this.selectedIndex = index.ToString();
            }
            
        }
        #endregion
        private DSUtil.Encoding EnType(string type)
        { return (DSUtil.Encoding)Enum.Parse(typeof(DSUtil.Encoding), type); }
        private void DataInfo_MouseClick(object sender, MouseEventArgs e)
        {
            this.DataInfo.Text = Path.GetFileNameWithoutExtension(this.dataPath);
        }

        private void DataInfo_LostFocus(object sender, EventArgs e)
        {
            SetTextBoxName(this.DataInfo);
        }

        private void AvgComBox_Leave(object sender, EventArgs e)
        {
            optionInfoCheck.IsIllegalInputName(this.AvgComBox, this.columnName, this.AvgComBox.Text);
        }

        private void AvgComBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                optionInfoCheck.IsIllegalInputName(this.AvgComBox, this.columnName, this.AvgComBox.Text);
        }

        private void AvgComBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.selectedIndex = this.AvgComBox.SelectedIndex.ToString();
        }
    }
}
