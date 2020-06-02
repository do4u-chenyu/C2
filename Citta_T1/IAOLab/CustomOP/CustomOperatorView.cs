using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Controls.Move.Rs;
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
    public partial class CustomOperatorView : Form
    {
        private MoveOpControl opControl;
        private string dataPath0;
        private string dataPath1;
        private string[] columnName0;
        private string[] columnName1;

        private string oldOptionDict;

        private List<int> oldOutList0;
        private List<int> oldOutList1;
        private List<string> oldColumnName;

        private string oldPath;

        public CustomOperatorView(MoveOpControl opControl)
        {

            InitializeComponent();

            if (opControl.OperatorDimension() == 2)
            {
                this.dataSource1.Visible = true;
                this.outList1.Visible = true;
                this.Text = "多源算子设置";
            }
            else
                this.Text = "AI实践算子设置";

            this.opControl = opControl;

            //旧状态记录
            this.oldPath = this.rsFullFilePathTextBox.Text;
            this.oldOutList0 = this.outList0.GetItemCheckIndex();
            this.oldOutList1 = this.outList1.GetItemCheckIndex();

            this.oldOptionDict = string.Join(",", this.opControl.Option.OptionDict.ToList());
            this.oldColumnName = new List<string>();

            //初始化配置内容
            InitOptionInfo();
            //加载配置内容
            LoadOption();
            
            SetTextBoxName(this.dataSource0);
            SetTextBoxName(this.dataSource1);
        }

        #region 初始化配置
        private void InitOptionInfo()
        {
            //获取两个数据源表头字段
            Dictionary<string, string> dataInfo = Global.GetOptionDao().GetDataSourceInfo(this.opControl.ID);


            if (dataInfo.ContainsKey("dataPath0") && dataInfo.ContainsKey("encoding0"))
            {
                this.dataPath0 = dataInfo["dataPath0"];
                this.dataSource0.Text = Path.GetFileNameWithoutExtension(this.dataPath0);
                this.toolTip1.SetToolTip(this.dataSource0, this.dataSource0.Text);
                columnName0 = SetOption(this.dataPath0, this.dataSource0.Text, dataInfo["encoding0"], dataInfo["separator0"].ToCharArray());
                this.opControl.FirstDataSourceColumns = this.columnName0.ToList();
                this.opControl.FirstDataSourceColumns =this.columnName0.ToList();//单输入的也要赋值
                this.opControl.Option.SetOption("columnname0", String.Join("\t", this.opControl.FirstDataSourceColumns));
                foreach (string name in this.columnName0)
                    this.outList0.AddItems(name);

            }
            if (this.Text != "AI实践算子设置" && dataInfo.ContainsKey("dataPath1") && dataInfo.ContainsKey("encoding1"))
            {
                this.dataPath1 = dataInfo["dataPath1"];
                this.dataSource1.Text = Path.GetFileNameWithoutExtension(dataInfo["dataPath1"]);
                this.toolTip1.SetToolTip(this.dataSource1, this.dataSource1.Text);
                columnName1 = SetOption(this.dataPath1, this.dataSource1.Text, dataInfo["encoding1"], dataInfo["separator1"].ToCharArray());
                this.opControl.SecondDataSourceColumns= this.columnName1.ToList();
                this.opControl.Option.SetOption("columnname1", String.Join("\t", this.opControl.SecondDataSourceColumns));
                foreach (string name in this.columnName1)
                    this.outList1.AddItems(name);
            }
        }

        private string[] SetOption(string path, string dataName, string encoding, char[] separator)
        {

            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Empty, OpUtil.EncodingEnum(encoding), separator);
            this.opControl.FirstDataSourceColumns = bcpInfo.ColumnArray.ToList();
            return bcpInfo.ColumnArray;
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
                textBox.Text = ConvertUtil.GB2312.GetString(ConvertUtil.GB2312.GetBytes(dataName), 0, maxLength) + "...";
            }
        }
        #endregion

        #region 配置信息的保存与加载
        private void SaveOption()
        {

       
            this.opControl.Option.SetOption("fix", this.fixRadioButton.Checked.ToString());
            this.opControl.Option.SetOption("random", this.randomRadioButton.Checked.ToString());
            this.opControl.Option.SetOption("fixSecond", this.fixSecondTextBox.Text);
            this.opControl.Option.SetOption("randomBegin", this.randomBeginTextBox.Text);
            this.opControl.Option.SetOption("randomEnd", this.randomEndTextBox.Text);
            this.opControl.Option.SetOption("path", this.rsFullFilePathTextBox.Text);

            List<int> checkIndexs = this.outList0.GetItemCheckIndex();
            List<int> outIndexs = new List<int>(this.oldOutList0);
            foreach (int index in checkIndexs)
            {
                if (!outIndexs.Contains(index))
                    outIndexs.Add(index);
            }
            foreach (int index in outIndexs)
            {
                if (!checkIndexs.Contains(index))
                {
                    outIndexs = new List<int>(checkIndexs);
                    break;
                }
            }
            string outField = string.Join(",", outIndexs);
            this.opControl.Option.SetOption("outfield0", outField);


            if (this.Text != "AI实践算子设置")
            {
                List<int> checkIndexs1 = this.outList1.GetItemCheckIndex();
                List<int> outIndexs1 = new List<int>(this.oldOutList1);
                foreach (int index in checkIndexs1)
                {
                    if (!outIndexs1.Contains(index))
                        outIndexs1.Add(index);
                }
                foreach (int index in outIndexs1)
                {
                    if (!checkIndexs1.Contains(index))
                    {
                        outIndexs1 = new List<int>(checkIndexs1);
                        break;
                    }
                }
                string outField1 = string.Join(",", outIndexs1);
                this.opControl.Option.SetOption("outfield1", outField1);
            }

            string outputEncode = GetControlRadioName(this.outputFileEncodeSettingGroup).ToLower();
            string outputSeparator = GetControlRadioName(this.outputFileSeparatorSettingGroup).ToLower();
            this.opControl.Option.SetOption("outputEncode", outputEncode);
            this.opControl.Option.SetOption("outputSeparator", outputSeparator);
            this.opControl.Option.SetOption("otherSeparator", (outputSeparator == "otherSeparatorRadio".ToLower()) ? this.otherSeparatorText.Text : "");


            if (this.oldOptionDict == string.Join(",", this.opControl.Option.OptionDict.ToList()) && this.opControl.Status != ElementStatus.Null)
                return;
            else
                this.opControl.Status = ElementStatus.Ready;

        }

        private void LoadOption()
        {
            if (this.opControl.Option.GetOption("fix") != "")
                this.fixRadioButton.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("fix"));
            if (this.opControl.Option.GetOption("random") != "")
                this.randomRadioButton.Checked = Convert.ToBoolean(this.opControl.Option.GetOption("random"));
            if (this.opControl.Option.GetOption("fixSecond") != "")
                this.fixSecondTextBox.Text = this.opControl.Option.GetOption("fixSecond");
            if (this.opControl.Option.GetOption("randomBegin") != "")
                this.randomBeginTextBox.Text = this.opControl.Option.GetOption("randomBegin");
            if (this.opControl.Option.GetOption("randomEnd") != "")
                this.randomEndTextBox.Text = this.opControl.Option.GetOption("randomEnd");
            if (this.opControl.Option.GetOption("path") != "")
                this.rsFullFilePathTextBox.Text = this.opControl.Option.GetOption("path");

            int[] outIndexs = new int[] { };
            if (this.opControl.Option.GetOption("outfield0") != "")
            {
                string[] checkIndexs = this.opControl.Option.GetOption("outfield0").Split(',');
                outIndexs = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                this.oldOutList0 = outIndexs.ToList();
                this.outList0.LoadItemCheckIndex(outIndexs);
                foreach (int i in outIndexs)
                    this.oldColumnName.Add(this.outList0.Items[i].ToString());
            }

            int[] outIndexs1 = new int[] { };
            if (this.opControl.Option.GetOption("outfield1") != "")
            {
                string[] checkIndexs = this.opControl.Option.GetOption("outfield1").Split(',');
                outIndexs1 = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                this.oldOutList1 = outIndexs1.ToList();
                this.outList1.LoadItemCheckIndex(outIndexs1);
                foreach (int i in outIndexs1)
                    this.oldColumnName.Add(this.outList1.Items[i].ToString());
            }


            SetControlRadioCheck(this.outputFileEncodeSettingGroup, this.opControl.Option.GetOption("outputEncode"), this.utfRadio);
            SetControlRadioCheck(this.outputFileSeparatorSettingGroup, this.opControl.Option.GetOption("outputSeparator"), this.tabRadio);
            this.otherSeparatorText.Text = this.opControl.Option.GetOption("otherSeparator");

        }
        #endregion

        #region 添加取消
        private void ConfirmButton_Click(object sender, System.EventArgs e)
        {
            bool empty = IsOptionReady();
            if (empty) return;

            this.DialogResult = DialogResult.OK;

            SaveOption();

            //内容修改，引起文档dirty 
            if (this.oldOptionDict != string.Join(",", this.opControl.Option.OptionDict.ToList()))
                Global.GetMainForm().SetDocumentDirty();

            //生成结果控件,创建relation,bcp 结果文件
            ModelElement resultElement = Global.GetCurrentDocument().SearchResultElementByOpID(this.opControl.ID);
            if (resultElement == ModelElement.Empty)
            {
                MoveRsControlFactory.GetInstance().CreateNewMoveRsControl(this.opControl, this.rsFullFilePathTextBox.Text);
            }

            //输出变化，修改结果算子路径
            if (resultElement != ModelElement.Empty && !this.oldPath.SequenceEqual(this.rsFullFilePathTextBox.Text))
                resultElement.FullFilePath = this.rsFullFilePathTextBox.Text;


            ModelElement hasResultNew = Global.GetCurrentDocument().SearchResultElementByOpID(this.opControl.ID);
            //修改结果算子内容
            (hasResultNew.InnerControl as MoveRsControl).Description = System.IO.Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(this.rsFullFilePathTextBox.Text));
            (hasResultNew.InnerControl as MoveRsControl).FinishTextChange();
            (hasResultNew.InnerControl as MoveRsControl).Encoding = GetControlRadioName(this.outputFileEncodeSettingGroup).ToLower() == "utfradio" ? OpUtil.Encoding.UTF8 : OpUtil.Encoding.GBK;
            (hasResultNew.InnerControl as MoveRsControl).Separator = OpUtil.DefaultSeparator;
            string separator = GetControlRadioName(this.outputFileSeparatorSettingGroup).ToLower();
            if (separator == "commaradio")
            {
                hasResultNew.Separator = ',';
            }
            else if (separator == "otherseparatorradio")
            {
                hasResultNew.Separator = String.IsNullOrEmpty(this.otherSeparatorText.Text) ? OpUtil.DefaultSeparator : this.otherSeparatorText.Text[0];
            }
            BCPBuffer.GetInstance().SetDirty(this.rsFullFilePathTextBox.Text);
        }

        private void CancelButton_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }


        private bool IsOptionReady()
        {
            bool empty = false;
            List<string> types = new List<string>();
            if (this.dataSource0.Text == "") return true;
            if (opControl.OperatorDimension() == 2 && this.dataSource1.Text == "") return true;

            if (this.outList0.GetItemCheckIndex().Count == 0)
            {
                if(opControl.OperatorDimension() == 2)
                    MessageBox.Show("请选择左侧文件输出字段");
                else
                    MessageBox.Show("请选择文件输出字段");
                empty = true;
                return empty;
            }

            if (opControl.OperatorDimension() == 2 && this.outList1.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请选择右侧文件输出字段");
                empty = true;
                return empty;
            }

            if (this.rsFullFilePathTextBox.Text == "")
            {
                MessageBox.Show("请选择结果文件路径");
                empty = true;
                return empty;
            }

            //有任一框中非数字
            if( !IsValidNum(this.fixSecondTextBox.Text) || !IsValidNum(this.randomBeginTextBox.Text) || !IsValidNum(this.randomEndTextBox.Text))
            {
                MessageBox.Show("输入时间非纯数字，请重新输入");
                empty = true;
                return empty;
            }
            //分隔符-其他，是否有值
            if (GetControlRadioName(this.outputFileSeparatorSettingGroup) == "otherSeparatorRadio" && String.IsNullOrEmpty(this.otherSeparatorText.Text))
            {
                MessageBox.Show("未输入其他类型分隔符内容");
                empty = true;
                return empty;
            }

            return empty;
        }
        #endregion

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "files|*.txt;*.bcp;*.xls;*.xlsx";

            if (fd.ShowDialog() == DialogResult.OK)
            {
                this.rsFullFilePathTextBox.Text = fd.FileName;
            }
        }

        private bool IsValidNum(string content)
        {
            Regex rg = new Regex("^[0-9]*$");
            if (!rg.IsMatch(content))
            {
                return false;
            }
            return true;
        }

        private string GetControlRadioName(Control group)
        {
            foreach (Control ct in group.Controls)
            {
                if (!(ct is RadioButton))
                    continue;
                RadioButton rb = ct as RadioButton;
                if (rb.Checked)
                {
                    return rb.Name;
                }
            }
            //TODO默认返回一个
            return "";
        }

        private void SetControlRadioCheck(Control group, string radioName, RadioButton defaulRb)
        {
            if (!IsControlRadioCheck(group, radioName))
                defaulRb.Checked = true;
        }

        private bool IsControlRadioCheck(Control group, string radioName)
        {
            foreach (Control ct in group.Controls)
            {
                if (!(ct is RadioButton))
                    continue;
                RadioButton rb = ct as RadioButton;
                if (rb.Name.ToLower() == radioName)
                {
                    rb.Checked = true;
                    return true;
                }
                else
                {
                    rb.Checked = false;
                }
            }
            return false;
        }

        private void otherSeparatorText_TextChanged(object sender, EventArgs e)
        {
            this.otherSeparatorRadio.Checked = true;
            if (String.IsNullOrEmpty(this.otherSeparatorText.Text))
                return;
            try
            {
                char separator = System.Text.RegularExpressions.Regex.Unescape(this.otherSeparatorText.Text).ToCharArray()[0];
            }
            catch (Exception)
            {
                MessageBox.Show("指定的分隔符有误！目前分隔符为：" + this.otherSeparatorText.Text);
            }
        }
    }
}
