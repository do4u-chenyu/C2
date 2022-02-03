﻿using C2.Controls.Move.Op;
using C2.Core;
using C2.Dialogs.Base;
using C2.Utils;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace C2.OperatorViews
{

    public partial class KeywordOperatorView : C1BaseOperatorView
    {
        private const int colIndexDefault = -1;
        private const int colCountDefault = 0;
        private string keywordEncoding, keywordExtType, keywordSep;
        private string keywordXml;
        private const string outInfo = "请选择输出列";
        private const string dataHelpInfo = "请选择需要处理的数据项";
        private const string keywordInfo = "请选择使用的关键词列";
        private const string conditionInfo = "请选择:过滤噪音还是命中提取";
        
        public KeywordOperatorView(MoveOpControl opControl) : base(opControl)
        {
            InitializeComponent();
            this.comparedItems = new string[] {
            "命中提取",
            "过滤去噪"};
            InitializeDataSource();
            LoadOption();
        }
       

        private void KeywordComBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreviewText();
        }

        #region 加载连接数据
        protected override void InitializeDataSource()
        {
            // 初始化左右表数据源配置信息
            base.InitializeDataSource();
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
                if (indexs.Max() < outListCCBL0.Items.Count && indexs.Min()>0)
                {
                    oldOutName0.AddRange(from int index in indexs
                                         select outListCCBL0.Items[index].ToString());
                }

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
                comboBox0.Text = string.Empty;
                MessageBox.Show(dataHelpInfo);
                return notReady;
            }
            if (conditionSelectBox.SelectedIndex.Equals(colIndexDefault))
            {
                conditionSelectBox.Text = string.Empty;
                MessageBox.Show(conditionInfo);
                return notReady;
            }
            if (comboBox1.SelectedIndex.Equals(colIndexDefault))
            {
                comboBox1.Text = string.Empty;
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
                                                             keywordSep,
                                                             comboBox1.SelectedIndex, 
                                                             keywordExtType, 
                                                             keywordEncoding);
            this.keywordPreviewBox.Text = keywordXml.Replace("\t", " OR ");
        }
        #endregion
    }
    public class KeywordCombine
    {
        private const string defaultInfo = "发生未知的原因，关键词组合失败，检查您的关键词输入是否有问题";
        private const string blankSpaceSepInfo = "空格分隔符与当前的关键词组合逻辑冲突，组合效果会有误差，建议您更换文件格式";
        private const string blankKeyColInfo = "当前尚未指定关键词列";
        private readonly List<string> datas = new List<string>();

        public string KeywordPreView(string keywordFile,
                                     string separator,
                                     int colIndex,
                                     string extType,
                                     string encoding)
        {
            string result = String.Empty;
            if (separator.Equals(OpUtil.StringBlank))
            {
                
                return blankSpaceSepInfo;
            }
            if (colIndex.Equals(-1))
            {
                return blankKeyColInfo;
            }
            KeywordRead(keywordFile,
                        separator.ToCharArray(),
                        colIndex,
                        OpUtil.ExtTypeEnum(extType),
                        OpUtil.EncodingEnum(encoding));
            if (datas.Count > 0)
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
                rows = new List<string>(BCPBuffer.GetInstance().GetCachePreviewExcelContent(fullFilePath).Split('\n'));
            }
            else if (extType == OpUtil.ExtType.Text)
            {
                rows = new List<string>(BCPBuffer.GetInstance().GetCachePreviewBcpContent(fullFilePath,
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
