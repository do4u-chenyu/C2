using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Controls.Move.Rs;
using Citta_T1.Core;
using Citta_T1.IAOLab.PythonOP;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{
    public partial class PythonOperatorView : Form
    {

        private MoveOpControl opControl;
        private string dataPath0;
        private string[] columnName0;
        private string oldPath;
        private string fullOutputFilePath;
        private string noChangedOutputFilePath;
        private string oldOptionDict;
        private List<string> previewTextList = new List<string>(new string[]{"", "", "", "", ""});

        public PythonOperatorView(MoveOpControl opControl)
        {
            InitializeComponent();
            this.opControl = opControl;
            
            InitOptionInfo();//初始化配置内容
            PythonInterpreterInfoLoad();//加载虚拟机
            LoadOption();//加载配置内容

            //旧状态记录
            this.oldPath = this.fullOutputFilePath;
            this.oldOptionDict = string.Join(",", this.opControl.Option.OptionDict.ToList());

            InitPreViewText();
        }

        #region 初始化配置
        private void InitOptionInfo()
        {
            //初始化数据源
            Dictionary<string, string> dataInfo = Global.GetOptionDao().GetDataSourceInfo(this.opControl.ID);
            if (dataInfo.ContainsKey("dataPath0") && dataInfo.ContainsKey("encoding0"))
            {
                this.dataPath0 = dataInfo["dataPath0"];
                this.dataSource0.Text = Path.GetFileNameWithoutExtension(this.dataPath0);
                this.toolTip1.SetToolTip(this.dataSource0, this.dataSource0.Text);
                columnName0 = SetOption(this.dataPath0, this.dataSource0.Text, dataInfo["encoding0"], dataInfo["separator0"].ToCharArray());
                this.opControl.SingleDataSourceColumns = this.columnName0.ToList();
                this.opControl.Option.SetOption("columnname", String.Join("\t", this.opControl.SingleDataSourceColumns));
            }
            //初始化输入输出路径
            ModelElement resultElement = Global.GetCurrentDocument().SearchResultElementByOpID(this.opControl.ID);
            if (resultElement != ModelElement.Empty)
            {
                this.fullOutputFilePath = resultElement.GetFullFilePath();
            }
            else
            {
                string tmpOutFileName = String.Format("L{0}_{1}.bcp", Global.GetCurrentDocument().ElementCount, DateTime.Now.ToString("yyyyMMdd_hhmmss"));
                this.noChangedOutputFilePath = Path.Combine(Global.GetCurrentDocument().SavePath, tmpOutFileName);
                this.opControl.Option.SetOption("outputParamPath", this.noChangedOutputFilePath);
            }
            this.paramInputFileFullPath.Text = dataInfo["dataPath0"];

            this.rsFullFileNameTextBox.Text = noChangedOutputFilePath;
        }

        private void InitPreViewText()
        {
            //初始化预览文件 //[虚拟机全路径，脚本全路径，其他参数，输入文件相关，输出文件相关]
            this.previewTextList[0] = GetVirtualMachinFullPath(this.pythonChosenComboBox.Text);
            this.previewTextList[1] = this.pyFullFilePathTextBox.Text;
            this.previewTextList[2] = this.pyParamTextBox.Text;
            string previewInput = GetControlRadioName(this.inputFileSettingTab) == "paramInputFileRadio" ? this.paramInputFileTextBox.Text + " " + this.paramInputFileFullPath.Text : "";
            this.previewTextList[3] = previewInput;
            string previewOutput = GetControlRadioName(this.outputFileSettingTab) == "paramRadioButton" ? this.paramPrefixTagTextBox.Text + " " + this.rsFullFileNameTextBox.Text : GetControlRadioName(this.outputFileSettingTab) == "stdoutRadioButton" ? " > " + this.fullOutputFilePath : "";
            this.previewTextList[4] = previewOutput;
            this.previewCmdText.Text = string.Join(" ", this.previewTextList);
        }
        private string[] SetOption(string path, string dataName, string encoding, char[] separator)
        {

            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Empty, EnType(encoding));
            string column = bcpInfo.columnLine;
            string[] columnName = column.Split(separator);
            this.opControl.SingleDataSourceColumns = columnName.ToList();
            return columnName;
        }

        private DSUtil.Encoding EnType(string type)
        { return (DSUtil.Encoding)Enum.Parse(typeof(DSUtil.Encoding), type); }
        #endregion

        #region 配置信息的保存与加载
        private void SaveOption()
        {
            string inputOption = GetControlRadioName(this.inputFileSettingTab).ToLower();
            string outputOption = GetControlRadioName(this.outputFileSettingTab).ToLower();
            string outputEncode = GetControlRadioName(this.outputFileEncodeSettingGroup).ToLower();
            string outputSeparator = GetControlRadioName(this.outputFileSeparatorSettingGroup).ToLower();

            this.opControl.Option.SetOption("virtualMachine", this.pythonChosenComboBox.Text);
            this.opControl.Option.SetOption("pyFullPath", this.pyFullFilePathTextBox.Text);
            this.opControl.Option.SetOption("pyParam", this.pyParamTextBox.Text);
            this.opControl.Option.SetOption("inputOption", inputOption);
            //this.opControl.Option.SetOption("paramInput", (inputOption == "paramInputFileRadio".ToLower()) ?this.paramInputFileTextBox.Text:"-f1");
            this.opControl.Option.SetOption("paramInput", this.paramInputFileTextBox.Text);
            this.opControl.Option.SetOption("outputOption", outputOption);
            //this.opControl.Option.SetOption("paramPrefixTag", (outputOption == "paramRadioButton".ToLower()) ? this.paramPrefixTagTextBox.Text : "-f2");
            this.opControl.Option.SetOption("paramPrefixTag", this.paramPrefixTagTextBox.Text);
            this.opControl.Option.SetOption("browseChosen", this.browseChosenTextBox.Text);
            if (outputOption == "browseChosenRadioButton".ToLower())
            {
                this.fullOutputFilePath = this.browseChosenTextBox.Text;
            }
            else
            {
                this.fullOutputFilePath = this.noChangedOutputFilePath;
            }
            //this.opControl.Option.SetOption("browseChosen", (outputOption == "browseChosenRadioButton".ToLower()) ? this.browseChosenTextBox.Text : "");
            this.opControl.Option.SetOption("outputEncode", outputEncode);
            this.opControl.Option.SetOption("outputSeparator", outputSeparator);
            this.opControl.Option.SetOption("otherSeparator", (outputSeparator == "otherSeparatorRadio".ToLower()) ? this.otherSeparatorText.Text : "");

            this.opControl.Option.SetOption("cmd", String.Join(" ", this.previewTextList));
            
            if (this.oldOptionDict == string.Join(",", this.opControl.Option.OptionDict.ToList()) && this.opControl.Status != ElementStatus.Null)
                return;
            else
            {
                this.opControl.Status = ElementStatus.Ready;
            }
                
        }

        private void LoadOption()
        {
            //this.pythonChosenComboBox.Text = this.opControl.Option.GetOption("virtualMachine");
            this.pyFullFilePathTextBox.Text = this.opControl.Option.GetOption("pyFullPath");
            this.pyParamTextBox.Text = this.opControl.Option.GetOption("pyParam");

            SetControlRadioCheck(this.inputFileSettingTab, this.opControl.Option.GetOption("inputOption"),this.noInputFileRadio);
            SetControlRadioCheck(this.outputFileSettingTab, this.opControl.Option.GetOption("outputOption"),this.browseChosenRadioButton);
            SetControlRadioCheck(this.outputFileEncodeSettingGroup, this.opControl.Option.GetOption("outputEncode"),this.utfRadio);
            SetControlRadioCheck(this.outputFileSeparatorSettingGroup, this.opControl.Option.GetOption("outputSeparator"),this.tabRadio);
            this.paramInputFileTextBox.Text = String.IsNullOrEmpty(this.opControl.Option.GetOption("paramInput")) ? "-f1" : this.opControl.Option.GetOption("paramInput");
            this.paramPrefixTagTextBox.Text = String.IsNullOrEmpty(this.opControl.Option.GetOption("paramPrefixTag")) ? "-f2" : this.opControl.Option.GetOption("paramPrefixTag");
            this.rsFullFileNameTextBox.Text = this.opControl.Option.GetOption("outputParamPath");
            this.noChangedOutputFilePath = this.opControl.Option.GetOption("outputParamPath");
            this.browseChosenTextBox.Text = this.opControl.Option.GetOption("browseChosen");
            this.otherSeparatorText.Text = this.opControl.Option.GetOption("otherSeparator");
            this.previewCmdText.Text = this.opControl.Option.GetOption("cmd");
        }
        #endregion

        #region 添加取消
        private void ConfirmButton_Click(object sender, System.EventArgs e)
        {
            if (!IsOptionReady()) return;

            this.DialogResult = DialogResult.OK;
            SaveOption();

            //内容修改，引起文档dirty 
            if (this.oldOptionDict != string.Join(",", this.opControl.Option.OptionDict.ToList()))
                Global.GetMainForm().SetDocumentDirty();

            //生成结果控件,创建relation,bcp结果文件
            ModelElement resultElement = Global.GetCurrentDocument().SearchResultElementByOpID(this.opControl.ID);
            if (resultElement == ModelElement.Empty)
            {
                Global.GetCreateMoveRsControl().CreateResultControlCustom(this.opControl, this.fullOutputFilePath);
                CreateNewBlankBCPFile(this.fullOutputFilePath);
            }

            
            //输出变化，修改结果算子路径
            if (!this.oldPath.SequenceEqual(this.fullOutputFilePath))
            {
                (resultElement.GetControl as MoveRsControl).FullFilePath = this.fullOutputFilePath;
                CreateNewBlankBCPFile(this.fullOutputFilePath);
            }

            ModelElement hasResultNew = Global.GetCurrentDocument().SearchResultElementByOpID(this.opControl.ID);
            //修改结果算子内容
            (hasResultNew.GetControl as MoveRsControl).textBox.Text = System.IO.Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(this.fullOutputFilePath));
            (hasResultNew.GetControl as MoveRsControl).FinishTextChange();
            (hasResultNew.GetControl as MoveRsControl).Encoding = GetControlRadioName(this.outputFileEncodeSettingGroup).ToLower() == "utfradio" ? DSUtil.Encoding.UTF8 : DSUtil.Encoding.GBK;
            (hasResultNew.GetControl as MoveRsControl).Separator = '\t';
            string separator = GetControlRadioName(this.outputFileSeparatorSettingGroup).ToLower();
            if(separator == "commaradio")
            {
                (hasResultNew.GetControl as MoveRsControl).Separator = ',';
            }
            else if(separator == "otherseparatorradio")
            {
                (hasResultNew.GetControl as MoveRsControl).Separator = String.IsNullOrEmpty(this.otherSeparatorText.Text) ? '\t' : this.otherSeparatorText.Text[0] ;
            }
            BCPBuffer.GetInstance().SetDirty(this.fullOutputFilePath);

        }

        private void CancelButton_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private bool IsOptionReady()
        {
            //输入源没连上
            if (String.IsNullOrEmpty(this.dataSource0.Text)) return false;

            //虚拟机是否勾选
            if (this.pythonChosenComboBox.Text == "未配置Python虚拟机")
            {
                MessageBox.Show("请选择python虚拟机，若无选项请前往‘首选项-python引擎’中配置。");
                return false;
            }
            //脚本是否导入
            if (this.pyFullFilePathTextBox.Text == "")
            {
                MessageBox.Show("没有配置需要运行的Python脚本，请点击浏览按钮导入脚本。");
                return false;
            }
            //输入文件设置，选2时是否写了参数
            if(GetControlRadioName(this.inputFileSettingTab) == "paramInputFileRadio" && String.IsNullOrEmpty(this.paramInputFileTextBox.Text))
            {
                MessageBox.Show("未配置输入文件设置中的指定输入文件参数。");
                return false;
            }
            //结果文件设置，选3时是否写了参数
            if (GetControlRadioName(this.outputFileSettingTab) == "paramRadioButton" && String.IsNullOrEmpty(this.paramPrefixTagTextBox.Text))
            {
                MessageBox.Show("未配置结果文件设置中的指定结果文件参数。");
                return false;
            }
            //结果文件设置，选4时是否选择约定路径
            if (GetControlRadioName(this.outputFileSettingTab) == "browseChosenRadioButton" && String.IsNullOrEmpty(this.browseChosenTextBox.Text))
            {
                MessageBox.Show("结果文件设置中的浏览指定结果文件未导入，请点击约定按钮添加结果文件路径。");
                return false;
            }
            //分隔符-其他，是否有值
            if(GetControlRadioName(this.outputFileSeparatorSettingGroup)== "otherSeparatorRadio" && String.IsNullOrEmpty(this.otherSeparatorText.Text))
            {
                MessageBox.Show("未输入其他类型分隔符内容");
                return false;
            }

            return true;
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
            this.pythonChosenComboBox.Text = "选择Python虚拟机";
            this.pythonChosenComboBox.Items.Clear();
            //判断xml里是否有值，有值，判断是否在config里有？没有return false，有return true
            string xmlVirtualMachineName = this.opControl.Option.GetOption("virtualMachine");
            if (String.IsNullOrEmpty(xmlVirtualMachineName)) return false;
            if (String.IsNullOrEmpty(GetVirtualMachinFullPath(xmlVirtualMachineName))) return false;

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
                this.pythonChosenComboBox.Text = "未配置Python虚拟机";
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
                if(pii.PythonAlias == pythonAlias)
                {
                    return pii.PythonFFP;
                }
            }
            return "";
        }
        #endregion

        #region 预览框
        private void pythonChosenComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void pyParamTextBox_TextChanged(object sender, EventArgs e)
        {
            this.previewTextList[2] = this.pyParamTextBox.Text;
            UpdatePreviewText();
        }

        private void noInputFileRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (this.noInputFileRadio.Checked)
            {
                this.previewTextList[3] = "";
                UpdatePreviewText();
            }
        }

        private void paramInputFileRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (this.paramInputFileRadio.Checked)
            {
                this.previewTextList[3] = this.paramInputFileTextBox.Text + " " + this.paramInputFileFullPath.Text;
                UpdatePreviewText();
            }
        }

        private void paramInputFileTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.paramInputFileRadio.Checked)
            {
                this.previewTextList[3] = this.paramInputFileTextBox.Text + " " + this.paramInputFileFullPath.Text;
                UpdatePreviewText();
            }
        }

        private void noOutputFileRadio_CheckedChanged(object sender, EventArgs e)
        {
            // 此时不需要 rsChosenButton
            this.rsChosenButton.Enabled = false;
            if (this.noInputFileRadio.Checked)
            {
                this.previewTextList[4] = "";
                UpdatePreviewText();
            }
        }

        private void StdoutRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            // 此时不需要 rsChosenButton
            this.rsChosenButton.Enabled = false;
            if (this.stdoutRadioButton.Checked)
            {
                this.previewTextList[4] = " > " + this.noChangedOutputFilePath;
                UpdatePreviewText();
            }
        }
        private void ParamRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            // 此时不需要 rsChosenButton
            this.rsChosenButton.Enabled = false;
            if (this.paramRadioButton.Checked)
            {
                this.previewTextList[4] = this.paramPrefixTagTextBox.Text + " " + this.rsFullFileNameTextBox.Text;
                UpdatePreviewText();
            }
        }
        private void paramPrefixTagTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.paramRadioButton.Checked)
            {
                this.fullOutputFilePath = this.rsFullFileNameTextBox.Text;
                this.previewTextList[4] = this.paramPrefixTagTextBox.Text + " " + this.rsFullFileNameTextBox.Text;
                UpdatePreviewText();
            }
        }

        private void RsChosenButton_Click(object sender, System.EventArgs e)
        {   // 由用户自己指定Py脚本生成的文件路径名,因此在配置的时候,py脚本还没运行
            // 此时结果文件还不存在,故使用saveFileDialog对话框
            DialogResult rs = this.saveFileDialog1.ShowDialog();
            if (rs != DialogResult.OK)
                return;
            this.browseChosenTextBox.Text = this.saveFileDialog1.FileName;
            this.toolTip1.SetToolTip(this.browseChosenTextBox, this.browseChosenTextBox.Text);

            if (this.browseChosenRadioButton.Checked)
            {
                this.previewTextList[4] = "";
                UpdatePreviewText();
            }
        }

        private void BrowseChosenRadioButton_CheckedChanged(object sender, System.EventArgs e)
        {
            // 此时需要 rsChosenButton
            this.rsChosenButton.Enabled = true;
            if (this.browseChosenRadioButton.Checked)
            {
                this.fullOutputFilePath = this.browseChosenTextBox.Text;
                this.previewTextList[4] = "";
                UpdatePreviewText();
            }
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
                    sw.Write("");
                }
            }
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
