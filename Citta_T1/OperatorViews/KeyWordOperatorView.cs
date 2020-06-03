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

    public partial class KeyWordOperatorView : Form
    {
        private const int colIndexDefault = 0;
        private const bool readyStatus = true;
        private static LogUtil log = LogUtil.GetInstance("CanvasPanel");
        private string dataSourcePath, dataSourceEncoding,dataSourceSep;
        private string keyWordPath, keyWordEncoding, keyWordExtType,keyWordSep;
        private MoveOpControl opControl;
        private string[] dataSrcColName, keyWordColName;
        private List<int> oldOutList;
        private List<string> oldColumnName;
        private List<string> selectOutColumn;
        private string oldOptionDictStr;

        public KeyWordOperatorView(MoveOpControl opControl)
        {
            this.opControl = opControl;
            this.oldOptionDictStr = string.Join(",", this.opControl.Option.OptionDict.ToList());
            oldOutList = new List<int>();
            oldColumnName = new List<string>();
            InitializeComponent();
            InitOptionInfo();
            LoadOption();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            if (!OptionStatusCheck().Equals(readyStatus))
                return;
            SaveOption();
            this.DialogResult = DialogResult.OK;
            if (this.oldOptionDictStr != string.Join(",", this.opControl.Option.OptionDict.ToList()))
                Global.GetMainForm().SetDocumentDirty();
            //生成结果控件,创建relation,bcp结果文件
            this.selectOutColumn = this.outList.GetItemCheckText();
            ModelElement resultElement = Global.GetCurrentDocument().SearchResultElementByOpID(this.opControl.ID);
            if (resultElement == ModelElement.Empty)
            {
                MoveRsControlFactory.GetInstance().CreateNewMoveRsControl(this.opControl, this.selectOutColumn);
                return;
            }

            // 对应的结果文件置脏
            BCPBuffer.GetInstance().SetDirty(resultElement.FullFilePath);
            //输出变化，重写BCP文件

            List<string> outName = new List<string>();
            foreach (string index in this.opControl.Option.GetOption("outfield").Split(','))
            { outName.Add(this.dataSrcColName[Convert.ToInt32(index)]); }
            if (!this.oldOutList.SequenceEqual(this.outList.GetItemCheckIndex()))
                Global.GetOptionDao().DoOutputCompare(this.oldColumnName, outName, this.opControl.ID);
        }

        private void keyWordColBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreviewText();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
        #region 加载连接数据
        private void InitOptionInfo()
        {
            GetDataInfo(); 
            dataSourceBox.Text = SetTextBoxName(dataSourcePath);
            dataSourceTip.SetToolTip(dataSourceBox, dataSourceBox.Text);

            keyWordBox.Text = SetTextBoxName(keyWordPath);
            keyWordTip.SetToolTip(keyWordBox, keyWordBox.Text);
            dataSrcColName = SetOption(this.dataSourcePath,
                                       this.dataSourceBox.Text,
                                       dataSourceEncoding,
                                       dataSourceSep.ToCharArray());
            keyWordColName = SetOption(this.keyWordPath,
                                       this.keyWordBox.Text,
                                       keyWordEncoding,
                                       keyWordSep.ToCharArray());
            this.opControl.FirstDataSourceColumns = this.dataSrcColName.ToList();
            this.opControl.SecondDataSourceColumns = this.keyWordColName.ToList();
            this.dataColumnBox.Items.AddRange(dataSrcColName);
            this.keyWordColBox.Items.AddRange(keyWordColName);
            this.outList.Items.AddRange(dataSrcColName);
            this.dataColumnBox.SelectedIndex = colIndexDefault;
            this.keyWordColBox.SelectedIndex = colIndexDefault;
            this.conditionSelectBox.SelectedIndex = colIndexDefault;
            UpdatePreviewText();
        }
        private void LoadOption()
        {
            if (Global.GetOptionDao().IsCleanOption(this.opControl, this.dataSrcColName, "outfield"))
            {
                return;
            }
            string[] checkIndexs = this.opControl.Option.GetOption("outfield").Split(',');
            int[] indexs = Array.ConvertAll<string, int>(checkIndexs, int.Parse);
            this.oldOutList = indexs.ToList();
            this.outList.LoadItemCheckIndex(indexs);
            foreach (int index in indexs)
                this.oldColumnName.Add(this.outList.Items[index].ToString());
            this.conditionSelectBox.SelectedIndex = Convert.ToInt32(this.opControl.Option.GetOption("conditionSlect"));
            this.keyWordColBox.SelectedIndex = Convert.ToInt32(this.opControl.Option.GetOption("keySelectIndex"));
            this.dataColumnBox.SelectedIndex = Convert.ToInt32(this.opControl.Option.GetOption("dataSelectIndex"));
        }
        private void SaveOption()
        {
            this.opControl.Option.OptionDict.Clear();
            List<int> checkIndexs = this.outList.GetItemCheckIndex();
            List<int> outIndexs = new List<int>(this.oldOutList);
            Global.GetOptionDao().UpdateOutputCheckIndexs(checkIndexs, outIndexs);
            string outField = string.Join(",", outIndexs);
            this.opControl.Option.SetOption("outfield", outField);

            this.opControl.Option.SetOption("columnname0", String.Join("\t", this.opControl.FirstDataSourceColumns));
            this.opControl.Option.SetOption("columnname1", String.Join("\t", this.opControl.SecondDataSourceColumns));
            this.opControl.Option.SetOption("dataSelectIndex", this.dataColumnBox.SelectedIndex.ToString());
            this.opControl.Option.SetOption("keySelectIndex", this.keyWordColBox.SelectedIndex.ToString());
            this.opControl.Option.SetOption("conditionSlect", this.conditionSelectBox.SelectedIndex.ToString());
            this.opControl.Option.SetOption("keyWordText", this.keyWordPreviewBox.Text);
            if (this.oldOptionDictStr == string.Join(",", this.opControl.Option.OptionDict.ToList())
                && this.opControl.Status != ElementStatus.Null
                && this.opControl.Status != ElementStatus.Warn)
            {
                return;
            }
            this.opControl.Status = ElementStatus.Ready;
        }
        private void GetDataInfo()
        {
            Dictionary<string, string> dataInfoDic = Global.GetOptionDao().GetDataSourceInfo(opControl.ID);
            _ = dataInfoDic.TryGetValue("dataPath0", out dataSourcePath);
            _ = dataInfoDic.TryGetValue("dataPath1", out keyWordPath);
            _ = dataInfoDic.TryGetValue("encoding0", out dataSourceEncoding);
            _ = dataInfoDic.TryGetValue("encoding1", out keyWordEncoding);
            _ = dataInfoDic.TryGetValue("extType1", out keyWordExtType);
            _ = dataInfoDic.TryGetValue("separator0", out dataSourceSep);
            _ = dataInfoDic.TryGetValue("separator1", out keyWordSep);
        }

        private string[] SetOption(string path, string dataName, string encoding, char[] separator)
        {
            BcpInfo bcpInfo = new BcpInfo(path, dataName, ElementType.Empty, OpUtil.EncodingEnum(encoding), separator);
            this.opControl.FirstDataSourceColumns = bcpInfo.ColumnArray.ToList();
            return bcpInfo.ColumnArray;
        }
        private string SetTextBoxName(string filePath)
        {
            String fileName = Path.GetFileNameWithoutExtension(filePath);
            int maxLength = 18;
            MatchCollection chs = Regex.Matches(fileName, "[\u4E00-\u9FA5]");
            int sumcount = chs.Count * 2;
            int sumcountDigit = Regex.Matches(fileName, "[a-zA-Z0-9]").Count;

            //防止截取字符串时中文乱码
            foreach (Match mc in chs)
            {
                if (fileName.IndexOf(mc.ToString()) == maxLength)
                {
                    maxLength -= 1;
                    break;
                }
            }
            if (sumcount + sumcountDigit <= maxLength)
            {
                return fileName;
            }
            return ConvertUtil.SubstringByte(fileName, colIndexDefault, maxLength);
        }
        #endregion
        #region 检查
        private bool OptionStatusCheck()
        {
            if (this.outList.GetItemCheckIndex().Count == colIndexDefault)
            {
                MessageBox.Show("您需要选择输出字段");
                return !readyStatus;
            }
            return readyStatus;
        }
        #endregion
        #region 配置信息的保存与更新
        private void UpdatePreviewText()
        {
            this.keyWordPreviewBox.Text = new KeyWordCombine().KeyWordPreView(keyWordPath,
                                                                              keyWordSep.ToCharArray(),
                                                                              keyWordColBox.SelectedIndex,
                                                                              keyWordExtType,
                                                                              keyWordEncoding);
        }
        #endregion
    }
    public class KeyWordCombine
    {
        private const string defaultInfo = "发生未知的原因，关键词组合失败，您需要联系开发团队或者重命名关键词文件并导入";
        private const string blankSpaceSepInfo = "空格分隔符与当前的关键词组合逻辑冲突，组合效果会有误差，建议您更换文件格式";
        private List<string> datas = new List<string>();

        public string KeyWordPreView(string kerWordFile,
                                     char[] separator,
                                     int colIndex,
                                     string extType,
                                     string encoding)
        {
            string result;
            if (separator.Equals(' '))
            {
                return blankSpaceSepInfo;
            }
            KeyWordRead(kerWordFile,
                        separator,
                        colIndex,
                        OpUtil.ExtTypeEnum(extType),
                        OpUtil.EncodingEnum(encoding));
            result = "(" + string.Join(" OR ", datas) + ")";
            if (result.Equals(string.Empty))
                result = defaultInfo;
            return result;
        }
        public void KeyWordRead(string fullFilePath,
                                        char[] separator,
                                        int colIndex,
                                        OpUtil.ExtType extType = OpUtil.ExtType.Text,
                                        OpUtil.Encoding encoding = OpUtil.Encoding.UTF8,
                                        bool isForceRead = false,
                                        int maxNumOfFile = 100)
        {
            List<string> rows;
            string line;
            if (extType == OpUtil.ExtType.Excel)
            {
                rows = new List<string>(BCPBuffer.GetInstance().GetCachePreViewExcelContent(fullFilePath,
                                                                                            isForceRead).Split('\n'));
            }
            else if (extType == OpUtil.ExtType.Text)
                rows = new List<string>(BCPBuffer.GetInstance().GetCachePreViewBcpContent(fullFilePath,
                                                                                          encoding,
                                                                                          isForceRead).Split('\n'));
            else
                rows = new List<string>();
            for (int i = 0; i < Math.Min(rows.Count - 1, maxNumOfFile); i++)
            {
                List<string> lines = new List<string>(rows[i + 1].TrimEnd('\r').Split(separator));
                if (colIndex >= lines.Count)
                    continue;
                line = lines[colIndex];
                if (line.Equals(string.Empty))
                {
                    continue;
                }
                line.Replace(" ", "OR");
                datas.Add(line);                                                 
            }
        }
    }
}
