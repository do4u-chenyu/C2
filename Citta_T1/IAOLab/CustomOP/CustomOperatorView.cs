using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.OperatorViews.Base;
using Citta_T1.Utils;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{
    public partial class CustomOperatorView : BaseOperatorView
    {
        private string oldPath;
        private OpUtil.Encoding encoding;
        private char separator;
        private string oldResultColumns;

        public CustomOperatorView(MoveOpControl opControl) : base(opControl)
        {

            InitializeComponent();

            if (opControl.OperatorDimension() == 2)
            {
                this.dataSourceTB1.Visible = true;
                this.outListCCBL1.Visible = true;
                this.Text = "多源算子设置";
            }
            else
                this.Text = "AI实践算子设置";


            //旧状态记录
            this.oldPath = this.rsFullFilePathTextBox.Text;
            this.oldOutList0 = this.outListCCBL0.GetItemCheckIndex();
            this.oldOutList1 = this.outListCCBL1.GetItemCheckIndex();

            //初始化配置内容
            InitByDataSource();
            //加载配置内容
            LoadOption();
            oldResultColumns = opControl.Option.GetOption("resultColumns");
        }

        #region 初始化配置
        private void InitByDataSource()
        {
            // 初始化左右表数据源配置信息
            this.InitDataSource();
            // 窗体自定义的初始化逻辑

            this.outListCCBL0.Items.AddRange(nowColumnsName0);
            this.outListCCBL1.Items.AddRange(nowColumnsName1);

        }
        #endregion

        #region 配置信息的保存与加载
        protected override void SaveOption()
        {
            this.opControl.Option.OptionDict.Clear();
            this.opControl.Option.SetOption("columnname0", opControl.FirstDataSourceColumns);
            this.opControl.Option.SetOption("fix", this.fixRadioButton.Checked);
            this.opControl.Option.SetOption("random", this.randomRadioButton.Checked);
            this.opControl.Option.SetOption("fixSecond", this.fixSecondTextBox.Text);
            this.opControl.Option.SetOption("randomBegin", this.randomBeginTextBox.Text);
            this.opControl.Option.SetOption("randomEnd", this.randomEndTextBox.Text);
            this.opControl.Option.SetOption("path", this.rsFullFilePathTextBox.Text);
            this.opControl.Option.SetOption("outfield0", outListCCBL0.GetItemCheckIndex());

        

            if (opControl.OperatorDimension() == 2)
            {
                this.opControl.Option.SetOption("columnname1", opControl.SecondDataSourceColumns);
                this.opControl.Option.SetOption("outfield1", outListCCBL1.GetItemCheckIndex());
            }

            string outputEncode = GetControlRadioName(this.outputFileEncodeSettingGroup).ToLower();
            string outputSeparator = GetControlRadioName(this.outputFileSeparatorSettingGroup).ToLower();
            this.opControl.Option.SetOption("outputEncode", outputEncode);
            this.opControl.Option.SetOption("outputSeparator", outputSeparator);
            this.opControl.Option.SetOption("otherSeparator", (outputSeparator == "otherSeparatorRadio".ToLower()) ? this.otherSeparatorText.Text : "");


            // 获取编码、分隔符类型，以获得结果文件表头
            encoding = GetControlRadioName(this.outputFileEncodeSettingGroup).ToLower() == "utfradio" ? OpUtil.Encoding.UTF8 : OpUtil.Encoding.GBK;
            separator = OpUtil.DefaultSeparator;
            string radioName = GetControlRadioName(this.outputFileSeparatorSettingGroup).ToLower();
            if (radioName == "commaradio")
            {
                separator = ',';
            }
            else if (radioName == "otherseparatorradio")
            {
                separator = String.IsNullOrEmpty(this.otherSeparatorText.Text) ? OpUtil.DefaultSeparator : this.otherSeparatorText.Text[0];
            }
            // 路径相同，改变分编码格式、分隔符内容将会变化，需要强制刷新缓存
            string oldResultLine = BCPBuffer.GetInstance().GetCacheColumnLine(this.rsFullFilePathTextBox.Text, encoding, true);
            this.opControl.Option.SetOption("resultColumns", oldResultLine);

            //更新子图所有节点状态
            UpdateSubGraphStatus();
        }

        private void LoadOption()
        {


            if (this.Text == "AI实践算子设置" && Global.GetOptionDao().IsCleanSingleOperatorOption(this.opControl, this.nowColumnsName0))
                return;
            if (this.Text == "多源算子设置" && Global.GetOptionDao().IsCleanBinaryOperatorOption(this.opControl, this.nowColumnsName0, this.nowColumnsName1))
                return;

            this.fixRadioButton.Checked = Convert.ToBoolean(opControl.Option.GetOption("fix", "True"));
            this.randomRadioButton.Checked = Convert.ToBoolean(opControl.Option.GetOption("random", "False"));
            this.fixSecondTextBox.Text = this.opControl.Option.GetOption("fixSecond");
            this.randomBeginTextBox.Text = this.opControl.Option.GetOption("randomBegin");
            this.randomEndTextBox.Text = this.opControl.Option.GetOption("randomEnd");
            this.rsFullFilePathTextBox.Text = this.opControl.Option.GetOption("path");


            int[] outIndexs = new int[] { };
            if (!String.IsNullOrEmpty(this.opControl.Option.GetOption("outfield0")))
            {
                string[] checkIndexs = this.opControl.Option.GetOptionSplit("outfield0");
                outIndexs = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                this.oldOutList0 = outIndexs.ToList();
                this.outListCCBL0.LoadItemCheckIndex(outIndexs);
                foreach (int i in outIndexs)
                    this.oldOutName0.Add(this.outListCCBL0.Items[i].ToString());
            }

            int[] outIndexs1 = new int[] { };
            if (!String.IsNullOrEmpty(this.opControl.Option.GetOption("outfield1")))
            {
                string[] checkIndexs = this.opControl.Option.GetOptionSplit("outfield1");
                outIndexs1 = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                this.oldOutList1 = outIndexs1.ToList();
                this.outListCCBL1.LoadItemCheckIndex(outIndexs1);
                foreach (int i in outIndexs1)
                    this.oldOutName1.Add(this.outListCCBL1.Items[i].ToString());
            }


            SetControlRadioCheck(this.outputFileEncodeSettingGroup, this.opControl.Option.GetOption("outputEncode"), this.utfRadio);
            SetControlRadioCheck(this.outputFileSeparatorSettingGroup, this.opControl.Option.GetOption("outputSeparator"), this.tabRadio);
            this.otherSeparatorText.Text = this.opControl.Option.GetOption("otherSeparator");

        }
        #endregion

        #region 添加取消
        protected override void ConfirmButton_Click(object sender, System.EventArgs e)
        {
            if (IsOptionNotReady()) return;

            this.DialogResult = DialogResult.OK;
            SaveOption();

            //内容修改，引起文档dirty 
            if (this.oldOptionDictStr != this.opControl.Option.ToString())
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
            //hasResultNew.InnerControl.Description = Path.GetFileNameWithoutExtension(this.rsFullFilePathTextBox.Text);
            //hasResultNew.InnerControl.FinishTextChange();//TODO 此处可能有BUG

            OpUtil.Encoding oldEncoding = hasResultNew.Encoding;
            char oldSeparator = hasResultNew.Separator;

            hasResultNew.InnerControl.Encoding = encoding;
            hasResultNew.InnerControl.Separator = separator;
         

            /*
             * 结果文件表头不一致、分隔符、编码改变，子图状态降级
             */

            if(oldResultColumns!=opControl.Option.GetOption("resultColumns")
                || oldEncoding!= encoding
                || oldSeparator!= separator )

                Global.GetCurrentDocument().SetChildrenStatusNull(opControl.ID);

        }

        protected override bool IsOptionNotReady()
        {
            bool notReady = true;
            if (this.dataSourceTB0.Text == "") return true;
            if (opControl.OperatorDimension() == 2 && this.dataSourceTB1.Text == "") return true;

            if (this.outListCCBL0.GetItemCheckIndex().Count == 0)
            {
                if (opControl.OperatorDimension() == 2)
                    MessageBox.Show("请选择左侧文件输出字段");
                else
                    MessageBox.Show("请选择文件输出字段");
                return notReady;
            }

            if (opControl.OperatorDimension() == 2 && this.outListCCBL1.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请选择右侧文件输出字段");
                return notReady;
            }

            if (this.rsFullFilePathTextBox.Text == "")
            {
                MessageBox.Show("请选择结果文件路径");
                return notReady;
            }

            //有任一框中非数字
            if (!IsValidNum(this.fixSecondTextBox.Text) || !IsValidNum(this.randomBeginTextBox.Text) || !IsValidNum(this.randomEndTextBox.Text))
            {
                MessageBox.Show("输入时间非纯数字，请重新输入");
                return notReady;
            }
            //分隔符-其他，是否有值
            if (GetControlRadioName(this.outputFileSeparatorSettingGroup) == "otherSeparatorRadio" && String.IsNullOrEmpty(this.otherSeparatorText.Text))
            {
                MessageBox.Show("未输入其他类型分隔符内容");
                return notReady;
            }

            return !notReady;
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

        private void OtherSeparatorText_TextChanged(object sender, EventArgs e)
        {
            this.otherSeparatorRadio.Checked = true;
            if (String.IsNullOrEmpty(this.otherSeparatorText.Text))
                return;
            try
            {
                char separator = Regex.Unescape(this.otherSeparatorText.Text).ToCharArray()[0];
            }
            catch (Exception)
            {
                MessageBox.Show("指定的分隔符有误！目前分隔符为：" + this.otherSeparatorText.Text);
            }
        }
    }
}
