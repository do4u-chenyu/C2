using Citta_T1.Business.Model;
using Citta_T1.Business.Option;
using Citta_T1.Controls.Move.Op;
using Citta_T1.Core;
using Citta_T1.OperatorViews.Base;
using Citta_T1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Citta_T1.OperatorViews
{

    public partial class KeywordOperatorView : BaseOperatorView
    {
        private const int colIndexDefault = 0;
        private const bool readyStatus = true;
        private string keywordEncoding, keywordExtType, keywordSep;

        public KeywordOperatorView(MoveOpControl opControl) : base(opControl)
        {
            InitializeComponent();
            InitByDataSource();
            LoadOption();
        }

        protected override void ConfirmButton_Click(object sender, EventArgs e)
        {
            if (!OptionStatusCheck().Equals(readyStatus))
                return;
            SaveOption();
            this.DialogResult = DialogResult.OK;
            if (this.oldOptionDictStr != this.opControl.Option.ToString())
            {
                Global.GetMainForm().SetDocumentDirty();
            }
            //生成结果控件,创建relation,bcp结果文件
            this.selectedColumns = this.outListCCBL0.GetItemCheckText();
            ModelElement resultElement = Global.GetCurrentDocument().SearchResultElementByOpID(this.opControl.ID);
            if (resultElement == ModelElement.Empty)
            {
                MoveRsControlFactory.GetInstance().CreateNewMoveRsControl(this.opControl, this.selectedColumns);
                return;
            }

            // 对应的结果文件置脏
            BCPBuffer.GetInstance().SetDirty(resultElement.FullFilePath);
            //输出变化，重写BCP文件
            List<string> outName = (from string index in this.opControl.Option.GetOptionSplit("outfield")
                                    select this.nowColumnsName0[Convert.ToInt32(index)]).ToList();
            if (!this.oldOutList0.SequenceEqual(this.outListCCBL0.GetItemCheckIndex()))
            {
                Global.GetOptionDao().DoOutputCompare(this.oldColumnsName0, outName, this.opControl.ID);
            }
        }

        private void KeywordComBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreviewText();
        }

        #region 加载连接数据
        private void InitByDataSource()
        {
            // 初始化左右表数据源配置信息
            this.InitDataSource();
            // 每个控件自定义的数据源配置逻辑
            dataInfo.TryGetValue("encoding1", out keywordEncoding);
            dataInfo.TryGetValue("extType1", out keywordExtType);
            dataInfo.TryGetValue("separator1", out keywordSep);

            comboBox0.Items.AddRange(nowColumnsName0);
            comboBox0.SelectedIndex = colIndexDefault;

            comboBox1.Items.AddRange(nowColumnsName1);
            comboBox1.SelectedIndex = colIndexDefault;

            outListCCBL0.Items.AddRange(nowColumnsName0);
            conditionSelectBox.SelectedIndex = colIndexDefault;
            UpdatePreviewText();
        }
        private void LoadOption()
        {
            Global.GetOptionDao().IsCleanOption(opControl, nowColumnsName0, "outfield");
            Global.GetOptionDao().IsCleanOption(opControl, nowColumnsName0, "dataSelectIndex");
            Global.GetOptionDao().IsCleanOption(opControl, nowColumnsName1, "keySelectIndex");
            string[] checkIndexs = opControl.Option.GetOptionSplit("outfield");
            int[] indexs = Array.ConvertAll(checkIndexs, int.Parse);
            oldOutList0 = indexs.ToList();
            outListCCBL0.LoadItemCheckIndex(indexs);
            oldColumnsName0.AddRange(from int index in indexs
                                     select outListCCBL0.Items[index].ToString());
            comboBox0.SelectedIndex = Convert.ToInt32(opControl.Option.GetOption("dataSelectIndex", null));
            comboBox1.SelectedIndex = Convert.ToInt32(opControl.Option.GetOption("keySelectIndex", null));
            conditionSelectBox.SelectedIndex = Convert.ToInt32(opControl.Option.GetOption("conditionSlect", null));
        }
        private void SaveOption()
        {
            opControl.Option.OptionDict.Clear();
            List<int> checkIndexs = this.outListCCBL0.GetItemCheckIndex();
            List<int> outIndexs = new List<int>(this.oldOutList0);
            Global.GetOptionDao().UpdateOutputCheckIndexs(checkIndexs, outIndexs);
            string outField = string.Join("\t", outIndexs);

            opControl.Option.SetOption("outfield", outField);
            opControl.Option.SetOption("columnname0", string.Join("\t", opControl.FirstDataSourceColumns));
            opControl.Option.SetOption("columnname1", string.Join("\t", opControl.SecondDataSourceColumns));
            opControl.Option.SetOption("dataSelectIndex", comboBox0.SelectedIndex.ToString());
            opControl.Option.SetOption("keySelectIndex", comboBox1.SelectedIndex.ToString());
            opControl.Option.SetOption("conditionSlect", conditionSelectBox.SelectedIndex.ToString());
            opControl.Option.SetOption("keyWordText", keywordPreviewBox.Text);


            ElementStatus oldStatus = this.opControl.Status;
            if (this.oldOptionDictStr != this.opControl.Option.ToString())
                this.opControl.Status = ElementStatus.Ready;

            if (oldStatus == ElementStatus.Done && this.opControl.Status == ElementStatus.Ready)
                Global.GetCurrentDocument().DegradeChildrenStatus(this.opControl.ID);
        }

        #endregion
        #region 检查
        private bool OptionStatusCheck()
        {
            if (this.outListCCBL0.GetItemCheckIndex().Count == colIndexDefault)
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
            this.keywordPreviewBox.Text = new KeywordCombine().KeywordPreView(dataSourceFFP1,
                                                                              keywordSep.ToCharArray(),
                                                                              comboBox1.SelectedIndex,
                                                                              keywordExtType,
                                                                              keywordEncoding);
        }
        #endregion
    }
    public class KeywordCombine
    {
        private const string defaultInfo = "发生未知的原因，关键词组合失败，您需要联系开发团队或者重命名关键词文件并导入";
        private const string blankSpaceSepInfo = "空格分隔符与当前的关键词组合逻辑冲突，组合效果会有误差，建议您更换文件格式";
        private List<string> datas = new List<string>();

        public string KeywordPreView(string keywordFile,
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
            KeywordRead(keywordFile,
                        separator,
                        colIndex,
                        OpUtil.ExtTypeEnum(extType),
                        OpUtil.EncodingEnum(encoding));
            result = string.Join(" OR ", datas);
            if (result.Equals(string.Empty))
                result = defaultInfo;
            return result;
        }
        public void KeywordRead(string fullFilePath,
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
                separator = "\t".ToCharArray();
                rows = new List<string>(BCPBuffer.GetInstance().GetCachePreViewExcelContent(fullFilePath,
                                                                                            isForceRead).Split('\n'));
            }
            else if (extType == OpUtil.ExtType.Text)
            {
                rows = new List<string>(BCPBuffer.GetInstance().GetCachePreViewBcpContent(fullFilePath,
                                                                                          encoding,
                                                                                          isForceRead).Split('\n'));
            }
            else
            {
                rows = new List<string>();
            }

            for (int i = 0; i < Math.Min(rows.Count - 1, maxNumOfFile); i++)
            {
                List<string> lines = new List<string>(rows[i + 1].TrimEnd('\r').Split(separator));
                if (colIndex >= lines.Count)
                {
                    continue;
                }
                line = lines[colIndex];
                if (line.Equals(string.Empty))
                {
                    continue;
                }
                datas.Add(line);
            }
        }
    }
}
