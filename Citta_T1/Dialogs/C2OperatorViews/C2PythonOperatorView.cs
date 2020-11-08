using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C2.Dialogs.Base;
using C2.Globalization;
using C2.IAOLab.PythonOP;
using C2.Model;
using C2.Model.Widgets;
using C2.Utils;

namespace C2.Dialogs.C2OperatorViews
{
    public partial class C2PythonOperatorView : C2BaseOperatorView
    {
        private readonly string oldPath;
        private string fullOutputFilePath;
        private readonly List<string> previewTextList = new List<string>(new string[] { "", "", "" });
        public C2PythonOperatorView(OperatorWidget operatorWidget) : base(operatorWidget)
        {
            InitializeComponent();

            InitializeDataSource();//初始化配置内容
            PythonInterpreterInfoLoad();//加载虚拟机
            LoadOption();//加载配置内容

            //旧状态记录
            this.oldPath = this.fullOutputFilePath;
            InitPreViewText();
            //注册py算子的ComboBox
            this.comboBoxes.Add(this.pythonChosenComboBox);
        }

        #region 初始化配置
        protected override void InitializeDataSource()
        {
            // 初始化左右表数据源配置信息
            this.InitializeDataSource();
            //初始化输入输出路径
            //ModelElement resultElement = Global.GetCurrentDocument().SearchResultElementByOpID(this.opControl.ID);
            //if (resultElement != ModelElement.Empty)
            //{
            //    this.fullOutputFilePath = resultElement.FullFilePath;
            //}
            //else
            //{
            //    string tmpOutFileName = String.Format("L{0}_{1}.bcp", Global.GetCurrentDocument().ElementCount, DateTime.Now.ToString("yyyyMMdd_hhmmss"));
            //    this.fullOutputFilePath = Path.Combine(Global.GetCurrentDocument().SavePath, tmpOutFileName);
            //    this.opControl.Option.SetOption("browseChosen", this.fullOutputFilePath);
            //    this.browseChosenTextBox.Text = fullOutputFilePath;
            //}

            //this.paramInputFileFullPath.Text = dataInfo["dataPath0"];
            //this.rsFullFileNameTextBox.Text = noChangedOutputFilePath;
        }

        private void InitPreViewText()
        {
            //初始化预览文件 //[虚拟机全路径，脚本全路径，其他参数，输入文件相关，输出文件相关]
            this.previewTextList[0] = GetVirtualMachinFullPath(this.pythonChosenComboBox.Text);
            this.previewTextList[1] = this.pyFullFilePathTextBox.Text;
            this.previewTextList[2] = this.pyParamTextBox.Text;
            //string previewInput = GetControlRadioName(this.inputFileSettingTab) == "paramInputFileRadio" ? this.paramInputFileTextBox.Text + " " + this.paramInputFileFullPath.Text : "";
            //this.previewTextList[3] = previewInput;
            //string previewOutput = GetControlRadioName(this.outputFileSettingTab) == "paramRadioButton" ? this.paramPrefixTagTextBox.Text + " " + this.rsFullFileNameTextBox.Text : GetControlRadioName(this.outputFileSettingTab) == "stdoutRadioButton" ? " > " + this.fullOutputFilePath : "";
            //this.previewTextList[4] = previewOutput;
            this.previewCmdText.Text = string.Join(" ", this.previewTextList);
        }
        #endregion

        #region 配置信息的保存与加载
        protected override void SaveOption()
        {
            this.operatorWidget.Option.SetOption("columnname0", firstDataSourceColumns);

            this.operatorWidget.Option.SetOption("virtualMachine", this.pythonChosenComboBox.Text);
            this.operatorWidget.Option.SetOption("pyFullPath", this.pyFullFilePathTextBox.Text);
            this.operatorWidget.Option.SetOption("pyParam", this.pyParamTextBox.Text);

            this.operatorWidget.Option.SetOption("browseChosen", this.browseChosenTextBox.Text);
            this.fullOutputFilePath = this.browseChosenTextBox.Text;
            //暂去掉
            //this.fullOutputFilePath = this.noChangedOutputFilePath;

            string outputEncode = GetControlRadioName(this.outputFileEncodeSettingGroup).ToLower();
            string outputSeparator = GetControlRadioName(this.outputFileSeparatorSettingGroup).ToLower();
            this.operatorWidget.Option.SetOption("outputEncode", outputEncode);
            this.operatorWidget.Option.SetOption("outputSeparator", outputSeparator);
            this.operatorWidget.Option.SetOption("otherSeparator", (outputSeparator == "otherSeparatorRadio".ToLower()) ? this.otherSeparatorText.Text : "");

            this.operatorWidget.Option.SetOption("cmd", String.Join(" ", this.previewTextList));
        }

        private void LoadOption()
        {
            //this.pythonChosenComboBox.Text = this.opControl.Option.GetOption("virtualMachine");
            this.pyFullFilePathTextBox.Text = this.operatorWidget.Option.GetOption("pyFullPath");
            this.pyParamTextBox.Text = this.operatorWidget.Option.GetOption("pyParam");

            SetControlRadioCheck(this.outputFileEncodeSettingGroup, this.operatorWidget.Option.GetOption("outputEncode"), this.utfRadio);
            SetControlRadioCheck(this.outputFileSeparatorSettingGroup, this.operatorWidget.Option.GetOption("outputSeparator"), this.tabRadio);
            this.browseChosenTextBox.Text = this.operatorWidget.Option.GetOption("browseChosen");
            this.otherSeparatorText.Text = this.operatorWidget.Option.GetOption("otherSeparator");
            this.previewCmdText.Text = this.operatorWidget.Option.GetOption("cmd");
        }
        #endregion

        #region 添加取消
        protected override void ConfirmButton_Click(object sender, System.EventArgs e)
        {
            if (IsOptionNotReady()) return;
            if (IsIllegalFieldName()) return;
            this.DialogResult = DialogResult.OK;
            SaveOption();

            //结果文件变更问题
            operatorWidget.OpName = operatorWidget.DataSourceItem.FileName + "-" + Lang._(operatorWidget.OpType.ToString());
            string path = this.fullOutputFilePath;
            string name = Path.GetFileNameWithoutExtension(path);
            char separator = '\t';
            string separatorRadio = GetControlRadioName(this.outputFileSeparatorSettingGroup).ToLower();
            if (separatorRadio == "commaradio")
                separator = ',';
            else if (separatorRadio == "otherseparatorradio")
                separator = String.IsNullOrEmpty(this.otherSeparatorText.Text) ? OpUtil.DefaultSeparator : this.otherSeparatorText.Text[0];
            OpUtil.Encoding encoding = GetControlRadioName(this.outputFileEncodeSettingGroup).ToLower() == "utfradio" ? OpUtil.Encoding.UTF8 : OpUtil.Encoding.GBK;

            operatorWidget.ResultItem = new DataItem(path, name, separator, encoding, JudgeFileExtType(path));
        }
        protected override bool IsOptionNotReady()
        {
            bool notReady = true;
            //输入源没连上
            if (String.IsNullOrEmpty(this.dataSourceTB0.Text)) return notReady;

            //虚拟机是否勾选
            if (string.IsNullOrEmpty(this.pythonChosenComboBox.Text))
            {
                MessageBox.Show("请选择python虚拟机，若无选项请前往‘首选项-python引擎’中配置。");
                return notReady;
            }
            //脚本是否导入
            if (String.IsNullOrEmpty(pyFullFilePathTextBox.Text))
            {
                MessageBox.Show("没有配置需要运行的Python脚本，请点击浏览按钮导入脚本。");
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

        #region 虚拟机配置
        private bool PythonInterpreterInfoLoad()
        {
            // 先从模型文档中加载配置项, 如果模型文档中没有相关信息
            // 则从App.Config中加载


            return LoadFromModelDocumentXml() || LoadFromAppConfig();
        }

        private bool LoadFromModelDocumentXml()
        {
            //先清空,再加载
            this.pythonChosenComboBox.Items.Clear();
            //判断xml里是否有值，有值，判断是否在config里有？没有return false，有return true
            string xmlVirtualMachineName = this.operatorWidget.Option.GetOption("virtualMachine");
            if (String.IsNullOrEmpty(xmlVirtualMachineName)) return false;
            if (String.IsNullOrEmpty(GetVirtualMachinFullPath(xmlVirtualMachineName)))
            {
                this.operatorWidget.Option["virtualMachine"] = String.Empty;
                return false;
            }

            //加载到items
            string pythonConfigString = ConfigUtil.TryGetAppSettingsByKey("python");
            PythonOPConfig config = new PythonOPConfig(pythonConfigString);
            foreach (PythonInterpreterInfo pii in config.AllPII)
            {
                this.pythonChosenComboBox.Items.Add(pii.PythonAlias);
            }
            this.pythonChosenComboBox.Text = xmlVirtualMachineName;
            return true;
        }
        private bool LoadFromAppConfig()
        {
            string pythonConfigString = ConfigUtil.TryGetAppSettingsByKey("python");
            PythonOPConfig config = new PythonOPConfig(pythonConfigString);
            if (config.Empty())
            {
                this.pythonChosenComboBox.Items.Clear();
                return false;
            }

            this.pythonChosenComboBox.Text = "选择Python虚拟机";

            foreach (PythonInterpreterInfo pii in config.AllPII)
            {
                this.pythonChosenComboBox.Items.Add(pii.PythonAlias);
                if (pii.ChosenDefault)
                {
                    this.pythonChosenComboBox.Text = pii.PythonAlias;
                    //this.pythonChosenComboBox.SelectedItem = pii.PythonAlias;
                }
            }
            return true;
        }

        private string GetVirtualMachinFullPath(string pythonAlias)
        {
            string pythonConfigString = ConfigUtil.TryGetAppSettingsByKey("python");
            PythonOPConfig config = new PythonOPConfig(pythonConfigString);
            foreach (PythonInterpreterInfo pii in config.AllPII)
            {
                if (pii.PythonAlias == pythonAlias)
                {
                    return pii.PythonFFP;
                }
            }
            return "";
        }
        #endregion

        #region 预览框
        private void PythonChosenComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //动态改变preview内容
            this.previewTextList[0] = GetVirtualMachinFullPath(this.pythonChosenComboBox.Text);
            UpdatePreviewText();
        }

        private void PyBrowseButton_Click(object sender, System.EventArgs e)
        {
            DialogResult rs = this.openFileDialog1.ShowDialog();
            if (rs != DialogResult.OK)
                return;
            this.pyFullFilePathTextBox.Text = this.openFileDialog1.FileName;
            this.toolTip1.SetToolTip(this.pyFullFilePathTextBox, this.pyFullFilePathTextBox.Text);

            this.previewTextList[1] = this.pyFullFilePathTextBox.Text;
            UpdatePreviewText();
        }

        private void PyParamTextBox_TextChanged(object sender, EventArgs e)
        {
            this.previewTextList[2] = this.pyParamTextBox.Text;
            UpdatePreviewText();
        }

        private void RsChosenButton_Click(object sender, System.EventArgs e)
        {   // 由用户自己指定Py脚本生成的文件路径名,因此在配置的时候,py脚本还没运行
            // 此时结果文件还不存在,故使用saveFileDialog对话框
            this.saveFileDialog1.OverwritePrompt = false;
            DialogResult rs = this.saveFileDialog1.ShowDialog();
            if (rs != DialogResult.OK)
                return;
            this.browseChosenTextBox.Text = this.saveFileDialog1.FileName;
            this.toolTip1.SetToolTip(this.browseChosenTextBox, this.browseChosenTextBox.Text);
        }

        private void UpdatePreviewText()
        {
            this.previewCmdText.Text = String.Join(" ", this.previewTextList);
        }
        #endregion

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
            return String.Empty;
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

        //public void CreateNewBlankBCPFile(string fullFilePath)
        //{
        //    if (!Directory.Exists(Global.GetCurrentDocument().SavePath))
        //    {
        //        Directory.CreateDirectory(Global.GetCurrentDocument().SavePath);
        //        FileUtil.AddPathPower(Global.GetCurrentDocument().SavePath, "FullControl");
        //    }

        //    if (!File.Exists(fullFilePath))
        //    {
        //        using (StreamWriter sw = new StreamWriter(fullFilePath, false, Encoding.UTF8))
        //        {
        //            sw.Write(String.Empty); //TODO 这一行应该不需要
        //        }
        //    }
        //}

        private void OtherSeparatorText_TextChanged(object sender, EventArgs e)
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


        private void CancelButton_Click(object sender, EventArgs e)
        {
            bool isReady = this.operatorWidget.Status == OpStatus.Done || this.operatorWidget.Status == OpStatus.Ready;
            bool notHasVirtualMachine = this.pythonChosenComboBox.Text == "未配置Python虚拟机";
            if (isReady && notHasVirtualMachine)
                this.operatorWidget.Status = OpStatus.Null;
        }


        private void PythonOperatorView_Load(object sender, EventArgs e)
        {

        }

        private void OutputFileEncodeSettingGroup_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }

        private void OutputFileSeparatorSettingGroup_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }

        private void label10_Click(object sender, EventArgs e)
        {
            //打开一个窗口
            new PythonOperatorHelper().ShowDialog();
        }
    }
}

