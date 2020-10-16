using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.IAOLab.PythonOP;
using Citta_T1.OperatorViews.Base;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{
    public partial class PythonOperatorView : BaseOperatorView
    {
        private readonly string oldPath;
        private string fullOutputFilePath;
        private readonly List<string> previewTextList = new List<string>(new string[] { "", "", "" });

        public PythonOperatorView(MoveOpControl opControl) : base(opControl)
        {
            InitializeComponent();

            InitByDataSource();//初始化配置内容
            PythonInterpreterInfoLoad();//加载虚拟机
            LoadOption();//加载配置内容

            //旧状态记录
            this.oldPath = this.fullOutputFilePath;
            InitPreViewText();
            //注册python算子的ComboBox
            this.comboBoxes.Add(this.pythonChosenComboBox);
        }

        #region 初始化配置
        private void InitByDataSource()
        {
            // 初始化左右表数据源配置信息
            this.InitDataSource();
            //初始化输入输出路径
            ModelElement resultElement = Global.GetCurrentDocument().SearchResultElementByOpID(this.opControl.ID);
            if (resultElement != ModelElement.Empty)
            {
                this.fullOutputFilePath = resultElement.FullFilePath;
            }
            else
            {
                string tmpOutFileName = String.Format("L{0}_{1}.bcp", Global.GetCurrentDocument().ElementCount, DateTime.Now.ToString("yyyyMMdd_hhmmss"));
                this.fullOutputFilePath = Path.Combine(Global.GetCurrentDocument().SavePath, tmpOutFileName);
                this.opControl.Option.SetOption("browseChosen", this.fullOutputFilePath);
                this.browseChosenTextBox.Text = fullOutputFilePath;
            }
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
            this.opControl.Option.SetOption("columnname0", opControl.FirstDataSourceColumns);

            this.opControl.Option.SetOption("virtualMachine", this.pythonChosenComboBox.Text);
            this.opControl.Option.SetOption("pyFullPath", this.pyFullFilePathTextBox.Text);
            this.opControl.Option.SetOption("pyParam", this.pyParamTextBox.Text);

            this.opControl.Option.SetOption("browseChosen", this.browseChosenTextBox.Text);
            this.fullOutputFilePath = this.browseChosenTextBox.Text;
            //暂去掉
            //this.fullOutputFilePath = this.noChangedOutputFilePath;

            string outputEncode = GetControlRadioName(this.outputFileEncodeSettingGroup).ToLower();
            string outputSeparator = GetControlRadioName(this.outputFileSeparatorSettingGroup).ToLower();
            this.opControl.Option.SetOption("outputEncode", outputEncode);
            this.opControl.Option.SetOption("outputSeparator", outputSeparator);
            this.opControl.Option.SetOption("otherSeparator", (outputSeparator == "otherSeparatorRadio".ToLower()) ? this.otherSeparatorText.Text : "");

            this.opControl.Option.SetOption("cmd", String.Join(" ", this.previewTextList));
            //更新子图所有节点状态
            UpdateSubGraphStatus();
        }

        private void LoadOption()
        {
            //this.pythonChosenComboBox.Text = this.opControl.Option.GetOption("virtualMachine");
            this.pyFullFilePathTextBox.Text = this.opControl.Option.GetOption("pyFullPath");
            this.pyParamTextBox.Text = this.opControl.Option.GetOption("pyParam");

            SetControlRadioCheck(this.outputFileEncodeSettingGroup, this.opControl.Option.GetOption("outputEncode"), this.utfRadio);
            SetControlRadioCheck(this.outputFileSeparatorSettingGroup, this.opControl.Option.GetOption("outputSeparator"), this.tabRadio);
            this.browseChosenTextBox.Text = this.opControl.Option.GetOption("browseChosen");
            this.otherSeparatorText.Text = this.opControl.Option.GetOption("otherSeparator");
            this.previewCmdText.Text = this.opControl.Option.GetOption("cmd");
        }
        #endregion

        #region 添加取消
        protected override void ConfirmButton_Click(object sender, System.EventArgs e)
        {
            if (IsOptionNotReady()) return;
            if (IsIllegalFieldName()) return;
            this.DialogResult = DialogResult.OK;
            // 旧的输出结果文件选项
            string oldOptionOut = this.opControl.Option.GetOption("outputOption");
            string oldBrowseChosen = this.opControl.Option.GetOption("browseChosen");
            SaveOption();

            // 内容修改，引起文档dirty 
            if (this.oldOptionDictStr != this.opControl.Option.ToString())
                Global.GetMainForm().SetDocumentDirty();

            // 生成结果控件,创建relation,bcp结果文件
            ModelElement resultElement = Global.GetCurrentDocument().SearchResultElementByOpID(this.opControl.ID);
            if (resultElement == ModelElement.Empty)
            {
                MoveRsControlFactory.GetInstance().CreateNewMoveRsControl(this.opControl, this.fullOutputFilePath);
                CreateNewBlankBCPFile(this.fullOutputFilePath);
            }


            // 输出变化，修改结果算子路径
            if (resultElement != ModelElement.Empty && !this.oldPath.SequenceEqual(this.fullOutputFilePath))
            {
                resultElement.FullFilePath = this.fullOutputFilePath;
                CreateNewBlankBCPFile(this.fullOutputFilePath);
            }

            ModelElement hasResultNew = Global.GetCurrentDocument().SearchResultElementByOpID(this.opControl.ID);
            // 修改结果算子内容
            //hasResultNew.InnerControl.Description = Path.GetFileNameWithoutExtension(this.fullOutputFilePath);
            //hasResultNew.InnerControl.FinishTextChange();//TODO 此处可能有BUG
            // 旧的编码和分隔符
            OpUtil.Encoding oldEncoding = hasResultNew.Encoding;
            char oldSeparator = hasResultNew.Separator;


            hasResultNew.InnerControl.Encoding = GetControlRadioName(this.outputFileEncodeSettingGroup).ToLower() == "utfradio" ? OpUtil.Encoding.UTF8 : OpUtil.Encoding.GBK;
            hasResultNew.InnerControl.Separator = OpUtil.DefaultSeparator;
            string separator = GetControlRadioName(this.outputFileSeparatorSettingGroup).ToLower();



            if (separator == "commaradio")
            {
                hasResultNew.Separator = ',';
            }
            else if (separator == "otherseparatorradio")
            {
                hasResultNew.Separator = String.IsNullOrEmpty(this.otherSeparatorText.Text) ? OpUtil.DefaultSeparator : this.otherSeparatorText.Text[0];
            }
            BCPBuffer.GetInstance().SetDirty(this.fullOutputFilePath);

            /*
            *  结果文件、分隔符、编码改变，子图状态降级
            */

            if (oldOptionOut != this.opControl.Option.GetOption("outputOption")
                || oldBrowseChosen != this.opControl.Option.GetOption("browseChosen")
                || oldEncoding != hasResultNew.Encoding
                || oldSeparator != hasResultNew.Separator)

                Global.GetCurrentDocument().SetChildrenStatusNull(opControl.ID);
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
            string xmlVirtualMachineName = this.opControl.Option.GetOption("virtualMachine");
            if (String.IsNullOrEmpty(xmlVirtualMachineName)) return false;
            if (String.IsNullOrEmpty(GetVirtualMachinFullPath(xmlVirtualMachineName)))
            {
                this.opControl.Option["virtualMachine"] = String.Empty;
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

        public void CreateNewBlankBCPFile(string fullFilePath)
        {
            if (!Directory.Exists(Global.GetCurrentDocument().SavePath))
            {
                Directory.CreateDirectory(Global.GetCurrentDocument().SavePath);
                FileUtil.AddPathPower(Global.GetCurrentDocument().SavePath, "FullControl");
            }

            if (!File.Exists(fullFilePath))
            {
                using (StreamWriter sw = new StreamWriter(fullFilePath, false, Encoding.UTF8))
                {
                    sw.Write(String.Empty); //TODO 这一行应该不需要
                }
            }
        }

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
            bool isReady = this.opControl.Status == ElementStatus.Done || this.opControl.Status == ElementStatus.Ready;
            bool notHasVirtualMachine = this.pythonChosenComboBox.Text == "未配置Python虚拟机";
            if (isReady && notHasVirtualMachine)
                this.opControl.Status = ElementStatus.Null;
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
