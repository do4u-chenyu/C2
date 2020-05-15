using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls;
using Citta_T1.Controls.Move;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{
    public partial class MaxOperatorView : Form
    {
        private MoveOpControl opControl;
        private string dataPath;
        private string oldMaxfield;
        private List<int> oldOutList;
        private ElementStatus oldstatus;
        private string[] columnName;
        private string oldOptionDict;
        private List<string> oldColumnName;
        private bool hasNewDataSource;
        private LogUtil log = LogUtil.GetInstance("MaxOperatorView");


        public MaxOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
            dataPath = "";
            this.hasNewDataSource = false;
            this.columnName = new string[] { };
            this.oldColumnName = new List<string>();
            this.oldOutList = new List<int>();
            this.opControl = opControl;
            InitOptionInfo();
            LoadOption();
                       
            this.oldMaxfield = this.maxValueBox.Text;           
            this.oldstatus = opControl.Status;
            this.oldOptionDict = string.Join(",", this.opControl.Option.OptionDict.ToList());

            SetTextBoxName(this.dataInfoBox);
            this.maxValueBox.Leave += new System.EventHandler(Global.GetOptionDao().Control_Leave);
            this.maxValueBox.KeyUp += new System.Windows.Forms.KeyEventHandler(Global.GetOptionDao().Control_KeyUp);
        }
        #region 添加取消
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            //未设置字段警告
            if (this.dataInfoBox.Text == "") return;
            if (this.maxValueBox.Text == "")
            {
                MessageBox.Show("请选择最大值字段!");
                return;
            }
            if (this.OutList.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请选择输出字段!");
                return;
            }
            this.DialogResult = DialogResult.OK;
            
            SaveOption();
            //内容修改，引起文档dirty
            if (this.oldMaxfield != this.maxValueBox.Text || !this.oldOutList.SequenceEqual(this.OutList.GetItemCheckIndex()))
                Global.GetMainForm().SetDocumentDirty();

            //生成结果控件,创建relation,bcp结果文件
            ModelElement hasResutl = Global.GetCurrentDocument().SearchResultOperator(this.opControl.ID);
            if (hasResutl == null)
            {
                Global.GetOptionDao().CreateResultControl(this.opControl, this.OutList.GetItemCheckText());
                return;
            }
              
          
            //输出变化，重写BCP文件
            if (hasResutl != null && String.Join(",", this.oldOutList) != this.opControl.Option.GetOption("outfield"))
                Global.GetOptionDao().IsModifyOut(this.oldColumnName, this.OutList.GetItemCheckText(), this.opControl.ID);
           
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion
        #region 配置信息的保存与加载
        private void SaveOption()
        {
            List<int> checkIndexs = this.OutList.GetItemCheckIndex();
            List<int> outIndexs =new List<int>(this.oldOutList);
            Global.GetOptionDao().UpdateOutputCheckIndexs(checkIndexs, outIndexs);
            string outField = string.Join(",", outIndexs);
            this.opControl.Option.SetOption("outfield", outField);
            if (this.maxValueBox.Text == "")
                this.opControl.Option.SetOption("maxfield", "");
            else
                this.opControl.Option.SetOption("maxfield", this.maxValueBox.SelectedIndex.ToString());
            
            if (this.oldOptionDict == string.Join(",", this.opControl.Option.OptionDict.ToList()) && this.opControl.Status != ElementStatus.Null)
                return;
            else
                this.opControl.Status = ElementStatus.Ready;

        }

        private void LoadOption()
        {
            int maxIndex = -1;
            if (this.opControl.Option.GetOption("maxfield") != "")
            {
                maxIndex = Convert.ToInt32(this.opControl.Option.GetOption("maxfield"));
                this.maxValueBox.Text = this.maxValueBox.Items[maxIndex].ToString();
            }
            if (this.opControl.Option.GetOption("outfield") != "")
            {
                
                string[] checkIndexs = this.opControl.Option.GetOption("outfield").Split(',');
                int[] outIndexs = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                this.oldOutList = outIndexs.ToList();
                this.OutList.LoadItemCheckIndex(outIndexs);
                foreach(int i in outIndexs)
                    this.oldColumnName.Add(this.OutList.Items[i].ToString());
            }
           
            this.opControl.Option.SetOption("columnname", this.opControl.SingleDataSourceColumns);
        }
        #endregion
        #region 初始化配置
        private void InitOptionInfo()
        {
            Dictionary<string, string> dataInfo = Global.GetOptionDao().GetDataSourceInfo(this.opControl.ID);
            if (dataInfo.ContainsKey("dataPath0") && dataInfo.ContainsKey("encoding0"))
            {
                this.dataPath = dataInfo["dataPath0"];
                this.dataInfoBox.Text = Path.GetFileNameWithoutExtension(this.dataPath);
                this.toolTip1.SetToolTip(this.dataInfoBox, this.dataInfoBox.Text);
                SetOption(this.dataPath, this.dataInfoBox.Text, dataInfo["encoding0"], dataInfo["separator0"].ToCharArray());
            }
        }

        private void SetOption(string path, string dataName, string encoding, char[] separator)
        {
 
            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Null, EnType(encoding));
            string column = bcpInfo.columnLine;
            this.columnName = column.Split(separator);
            foreach (string name in this.columnName)
            {
                this.OutList.AddItems(name);
                this.maxValueBox.Items.Add(name);
            }
            CompareDataSource();
            this.opControl.SingleDataSourceColumns = String.Join("\t", this.columnName);
        }
        private void CompareDataSource()
        {
            //新数据源与旧数据源表头不匹配，对应配置内容是否情况进行判断
            if (this.opControl.Option.GetOption("columnname") == "") return;
            string[] oldColumnList = this.opControl.Option.GetOption("columnname").Split('\t');
            try
            {
                if (this.opControl.Option.GetOption("maxfield") != "")
                {
                    int index = Convert.ToInt32(this.opControl.Option.GetOption("maxfield"));
                    if (index > this.columnName.Length - 1 || oldColumnList[index] != this.columnName[index])
                        this.opControl.Option.OptionDict.Remove("maxfield");
                }
                if (this.opControl.Option.GetOption("outfield") != "")
                {

                    string[] checkIndexs = this.opControl.Option.GetOption("outfield").Split(',');
                    int[] outIndex = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                    if (Global.GetOptionDao().IsDataSourceEqual(oldColumnList, this.columnName, outIndex))
                    {
                        this.opControl.Option.OptionDict.Remove("outfield");
                        this.hasNewDataSource = true;
                    }
                }
            }
            catch (Exception ex) { log.Error(ex.Message); };
        }


        public void SetTextBoxName(TextBox textBox)
        {
            string dataName = textBox.Text;
            int maxLength = 18;
            MatchCollection chs = Regex.Matches(dataName, "[\u4E00-\u9FA5]");
            int sumcount = chs.Count * 2;
            int sumcountDigit = Regex.Matches(dataName, "[a-zA-Z0-9]").Count;

            //防止截取字符串时中文乱码
            foreach(Match mc in chs)
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
        private DSUtil.Encoding EnType(string type)
        { return (DSUtil.Encoding)Enum.Parse(typeof(DSUtil.Encoding), type); }

        private void DataInfoBox_MouseClick(object sender, MouseEventArgs e)
        {
            this.dataInfoBox.Text = Path.GetFileNameWithoutExtension(this.dataPath);
        }

        private void DataInfoBox_LostFocus(object sender, EventArgs e)
        {
            SetTextBoxName(this.dataInfoBox);
        }
        
    }

}
