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
        private const int colIndexDefault = -1;
        private const int colCountDefault = 0;
        private string keywordEncoding, keywordExtType, keywordSep;
        private string keywordXml;
        private const string outInfo = "您需要选择输出列";
        private const string dataHelpInfo = "您需要选择需要处理的数据项";
        private const string keywordInfo = "您需要选择使用的关键词列";
        private const string conditionInfo = "您需要选择操作为过滤噪音还是命中提取";

        public KeywordOperatorView(MoveOpControl opControl) : base(opControl)
        {
            InitializeComponent();
            this.comparedItems = new string[] {
            "命中提取",
            "过滤去噪"};
            InitByDataSource();
            LoadOption();
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
            comboBox1.Items.AddRange(nowColumnsName1);
            outListCCBL0.Items.AddRange(nowColumnsName0);

            // conditionSelectBox模糊查找功能
            this.conditionSelectBox.Items.AddRange(this.comparedItems);
            this.conditionSelectBox.SelectionChangeCommitted += new EventHandler(this.GetComparedSelectedItemIndex);
            this.conditionSelectBox.TextUpdate += new System.EventHandler(ComparedComboBox_TextUpdate);
            this.conditionSelectBox.DropDownClosed += new System.EventHandler(ComparedComboBox_ClosedEvent);
            UpdatePreviewText();
        }
        private void LoadOption()
        {
            if (Global.GetOptionDao().IsCleanBinaryOperatorOption(this.opControl, 
                                                                  this.nowColumnsName0, 
                                                                  this.nowColumnsName1))
                return;

            if (!String.IsNullOrEmpty(opControl.Option.GetOption("outfield0")))
            {
                string[] checkIndexs = opControl.Option.GetOptionSplit("outfield0");
                int[] indexs = Array.ConvertAll(checkIndexs, int.Parse);
                oldOutList0 = indexs.ToList();
                outListCCBL0.LoadItemCheckIndex(indexs);
                oldOutName0.AddRange(from int index in indexs
                                     select outListCCBL0.Items[index].ToString());
            }

            comboBox0.SelectedIndex = Convert.ToInt32(opControl.Option.GetOption("dataSelectIndex", "0"));
            comboBox1.SelectedIndex = Convert.ToInt32(opControl.Option.GetOption("keySelectIndex", "0"));
            conditionSelectBox.SelectedIndex = Convert.ToInt32(opControl.Option.GetOption("conditionSlect", "0"));
        }
        protected override void SaveOption()
        {
            opControl.Option.Clear();
            opControl.Option.SetOption("outfield0", outListCCBL0.GetItemCheckIndex());
            opControl.Option.SetOption("columnname0", opControl.FirstDataSourceColumns);
            opControl.Option.SetOption("columnname1", opControl.SecondDataSourceColumns);
            opControl.Option.SetOption("dataSelectIndex", comboBox0.SelectedIndex);
            opControl.Option.SetOption("keySelectIndex", comboBox1.SelectedIndex);
            opControl.Option.SetOption("conditionSlect", conditionSelectBox.SelectedIndex);
            opControl.Option.SetOption("keyWordText", keywordXml);
            this.selectedColumns = this.outListCCBL0.GetItemCheckText();

            //更新子图所有节点状态
            UpdateSubGraphStatus();
        }

        #endregion
        #region 检查
        protected override bool IsOptionNotReady()
        {
            bool notReady = true;
            if (comboBox0.SelectedIndex.Equals(colIndexDefault))
            {
                MessageBox.Show(dataHelpInfo);
                return notReady;
            }
            if (conditionSelectBox.SelectedIndex.Equals(colIndexDefault))
            {
                MessageBox.Show(conditionInfo);
                return notReady;
            }
            if (comboBox1.SelectedIndex.Equals(colIndexDefault))
            {
                MessageBox.Show(keywordInfo);
                return notReady;
            }
            if (this.outListCCBL0.GetItemCheckIndex().Count.Equals(colCountDefault))
            {
                MessageBox.Show(outInfo);
                return notReady;
            }
            return !notReady;
        }
        #endregion
        #region 配置信息的保存与更新
        private void UpdatePreviewText()
        {

            keywordXml = new KeywordCombine().KeywordPreView(dataSourceFFP1, 
                                                             keywordSep.ToCharArray(),
                                                             comboBox1.SelectedIndex, 
                                                             keywordExtType, 
                                                             keywordEncoding);
            this.keywordPreviewBox.Text = keywordXml.Replace("\t", " OR ");
        }
        #endregion
    }
    public class KeywordCombine
    {
        private const string defaultInfo = "发生未知的原因，关键词组合失败，您需要联系开发团队或者重命名关键词文件并导入";
        private const string blankSpaceSepInfo = "空格分隔符与当前的关键词组合逻辑冲突，组合效果会有误差，建议您更换文件格式";
        private const string blankKeyColInfo = "当前尚未指定关键词列";

        private readonly List<string> datas = new List<string>();

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
            if (colIndex.Equals(-1))
            {
                return blankKeyColInfo;
            }
            KeywordRead(keywordFile,
                        separator,
                        colIndex,
                        OpUtil.ExtTypeEnum(extType),
                        OpUtil.EncodingEnum(encoding));
            result = string.Join("\t", datas);  //TODO， 如果输入关键词本身是"OR",会是什么情况
            if (String.IsNullOrWhiteSpace(result))
                result = defaultInfo;
            return result;
        }

        private void KeywordRead(string fullFilePath,
                                        char[] separator,
                                        int colIndex,
                                        OpUtil.ExtType extType = OpUtil.ExtType.Text,
                                        OpUtil.Encoding encoding = OpUtil.Encoding.UTF8)
        {
            List<string> rows = new List<string>();      
            if (extType == OpUtil.ExtType.Excel)
            {
                separator = "\t".ToCharArray();
                rows = new List<string>(BCPBuffer.GetInstance().GetCachePreViewExcelContent(fullFilePath).Split('\n'));
            }
            else if (extType == OpUtil.ExtType.Text)
            {
                rows = new List<string>(BCPBuffer.GetInstance().GetCachePreViewBcpContent(fullFilePath,
                                                                                          encoding).Split('\n'));
            }
            // 第一行是表头，跳过表头,i从1开始
            for (int i = 1; i < rows.Count; i++)
            {
                List<string> lines = new List<string>(rows[i].TrimEnd('\r').Split(separator));
                if (colIndex >= lines.Count || String.IsNullOrWhiteSpace(lines[colIndex]))
                    continue;
                datas.Add(lines[colIndex]);
            }
        }
    }
}
