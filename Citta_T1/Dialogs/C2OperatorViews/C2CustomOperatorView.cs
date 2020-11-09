using C2.Core;
using C2.Dialogs.Base;
using C2.Globalization;
using C2.Model;
using C2.Model.Widgets;
using C2.Utils;
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

namespace C2.Dialogs.C2OperatorViews
{
    public partial class C2CustomOperatorView : C2BaseOperatorView
    {
        private readonly string oldPath;
        private OpUtil.Encoding encoding;
        private char separator;

        public C2CustomOperatorView(OperatorWidget operatorWidget) : base(operatorWidget)
        {
            InitializeComponent();

            this.Text = "AI实践算子设置";
            //旧状态记录
            this.oldPath = this.rsFullFilePathTextBox.Text;
            this.oldOutList0 = this.outListCCBL0.GetItemCheckIndex();

            //初始化配置内容
            InitializeDataSource();
            //加载配置内容
            LoadOption();
        }

        #region 初始化配置
        protected override void InitializeDataSource()
        {
            // 初始化左右表数据源配置信息
            base.InitializeDataSource();
            // 窗体自定义的初始化逻辑

            this.outListCCBL0.Items.AddRange(nowColumnsName0);
        }
        #endregion

        #region 配置信息的保存与加载
        protected override void SaveOption()
        {
            this.operatorWidget.Option.Clear();
            this.operatorWidget.Option.SetOption("columnname0", firstDataSourceColumns);
            this.operatorWidget.Option.SetOption("fix", this.fixRadioButton.Checked);
            this.operatorWidget.Option.SetOption("random", this.randomRadioButton.Checked);
            this.operatorWidget.Option.SetOption("fixSecond", this.fixSecondTextBox.Text);
            this.operatorWidget.Option.SetOption("randomBegin", this.randomBeginTextBox.Text);
            this.operatorWidget.Option.SetOption("randomEnd", this.randomEndTextBox.Text);
            this.operatorWidget.Option.SetOption("path", this.rsFullFilePathTextBox.Text);
            this.operatorWidget.Option.SetOption("outfield0", outListCCBL0.GetItemCheckIndex());


            string outputEncode = GetControlRadioName(this.outputFileEncodeSettingGroup).ToLower();
            string outputSeparator = GetControlRadioName(this.outputFileSeparatorSettingGroup).ToLower();
            this.operatorWidget.Option.SetOption("outputEncode", outputEncode);
            this.operatorWidget.Option.SetOption("outputSeparator", outputSeparator);
            this.operatorWidget.Option.SetOption("otherSeparator", (outputSeparator == "otherSeparatorRadio".ToLower()) ? this.otherSeparatorText.Text : "");


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
            this.operatorWidget.Option.SetOption("resultColumns", oldResultLine);
        }
        private void LoadOption()
        {
            this.fixRadioButton.Checked = Convert.ToBoolean(operatorWidget.Option.GetOption("fix", "True"));
            this.randomRadioButton.Checked = Convert.ToBoolean(operatorWidget.Option.GetOption("random", "False"));
            this.fixSecondTextBox.Text = this.operatorWidget.Option.GetOption("fixSecond");
            this.randomBeginTextBox.Text = this.operatorWidget.Option.GetOption("randomBegin");
            this.randomEndTextBox.Text = this.operatorWidget.Option.GetOption("randomEnd");
            this.rsFullFilePathTextBox.Text = this.operatorWidget.Option.GetOption("path");


            int[] outIndexs = new int[] { };
            if (!String.IsNullOrEmpty(this.operatorWidget.Option.GetOption("outfield0")))
            {
                string[] checkIndexs = this.operatorWidget.Option.GetOptionSplit("outfield0");
                outIndexs = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
                this.oldOutList0 = outIndexs.ToList();
                this.outListCCBL0.LoadItemCheckIndex(outIndexs);
                foreach (int i in outIndexs)
                {
                    if (OpUtil.IsArrayIndexOutOfBounds(this.outListCCBL0, i))
                        continue;
                    this.oldOutName0.Add(this.outListCCBL0.Items[i].ToString());
                }

            }

            SetControlRadioCheck(this.outputFileEncodeSettingGroup, this.operatorWidget.Option.GetOption("outputEncode"), this.utfRadio);
            SetControlRadioCheck(this.outputFileSeparatorSettingGroup, this.operatorWidget.Option.GetOption("outputSeparator"), this.tabRadio);
            this.otherSeparatorText.Text = this.operatorWidget.Option.GetOption("otherSeparator");

        }
        #endregion

        #region 添加取消
        protected override void ConfirmButton_Click(object sender, System.EventArgs e)
        {
            if (IsOptionNotReady()) return;
            if (IsIllegalFieldName()) return;
            this.DialogResult = DialogResult.OK;
            SaveOption();
            operatorWidget.OpName = operatorWidget.DataSourceItem.FileName + "-" + Lang._(operatorWidget.OpType.ToString());
            string path = this.rsFullFilePathTextBox.Text;
            string name = Path.GetFileNameWithoutExtension(path);
            operatorWidget.ResultItem = new DataItem(path, name, separator, encoding, JudgeFileExtType(path));
        }

        protected override bool IsOptionNotReady()
        {
            bool notReady = true;
            if (this.dataSourceTB0.Text == "") return true;

            if (this.outListCCBL0.GetItemCheckIndex().Count == 0)
            {
                MessageBox.Show("请选择文件输出字段");
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
            //设定时间空值检测
            if (this.fixRadioButton.Checked && String.IsNullOrEmpty(this.fixSecondTextBox.Text))
            {
                MessageBox.Show("请设置固定运行时间");
                return notReady;
            }
            if (this.randomRadioButton.Checked && (String.IsNullOrEmpty(this.randomBeginTextBox.Text) || String.IsNullOrEmpty(this.randomEndTextBox.Text)))
            {
                MessageBox.Show("请设置随机运行时间");
                return notReady;
            }
            //分隔符-其他，是否有值
            if (GetControlRadioName(this.outputFileSeparatorSettingGroup) == "otherSeparatorRadio" && String.IsNullOrEmpty(this.otherSeparatorText.Text))
            {
                MessageBox.Show("未输入其他类型分隔符内容");
                return notReady;
            }
            if (Convert.ToInt32(randomBeginTextBox.Text) > Convert.ToInt32(randomEndTextBox.Text))
            {
                MessageBox.Show("随机运行时间设置中，起始时间大于结束时间");
                return notReady;
            }
            return !notReady;
        }
        #endregion

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog
            {
                Filter = "files|*.txt;*.bcp;*.xls;*.xlsx"
            };

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
