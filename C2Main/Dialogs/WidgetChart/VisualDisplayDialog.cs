﻿using C2.Business.Option;
using C2.Core;
using C2.Dialogs.Base;
using C2.Model;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace C2.Dialogs
{
    public partial class VisualDisplayDialog : C1BaseOperatorView
    {
        private BcpInfo bcpInfo;
        private DataItem hitItem;
        private string FilePath { get => hitItem.FilePath; }

        private OpUtil.Encoding FileEncoding { get => hitItem.FileEncoding; }
        private char FileSep { get => hitItem.FileSep; }
        private string FileName { get => hitItem.FileName; }

        public VisualDisplayDialog(DataItem hitItem)
        {
            this.hitItem = hitItem;
            InitializeComponent();
            InitializeDropDown();
        }
        private void InitializeDropDown()
        {
            this.bcpInfo = new BcpInfo(FilePath, FileEncoding, new char[] { FileSep });
            this.comboBox0.Items.AddRange(bcpInfo.ColumnArray);
            this.outListCCBL0.Items.AddRange(bcpInfo.ColumnArray);
        }

        protected override void  ConfirmButton_Click(object sender, EventArgs e)
        {
            if (OptionNotReady())
                return;
            int upperLimit = 100;

            // 获得x,y轴数据的列索引
            int xIndex = comboBox0.Tag == null ? comboBox0.SelectedIndex : ConvertUtil.TryParseInt(comboBox0.Tag.ToString());
            List<int> indexs = new List<int>() { xIndex };
            indexs.AddRange(outListCCBL0.GetItemCheckIndex());

            // 获取选中输入、输出各列数据
            string fileContent;
            if (hitItem.FileType == OpUtil.ExtType.Excel)
                fileContent = BCPBuffer.GetInstance().GetCachePreviewExcelContent(FilePath);
            else
                fileContent = BCPBuffer.GetInstance().GetCachePreviewBcpContent(FilePath, FileEncoding);
            List<string> rows = new List<string>(fileContent.Split('\n'));
            upperLimit = Math.Min(rows.Count, upperLimit);

            List<List<string>> columnValues= Utils.FileUtil.GetColumns(indexs, hitItem, rows, upperLimit);
            if (columnValues.Count == 0)
            {
                HelpUtil.ShowMessageBox("文件内容为空或格式有问题");
                Close(); 
                return;
            }
            // xy轴标题名
            string xName = comboBox0.SelectedItem.ToString();
            List<string> yNames = outListCCBL0.GetItemCheckText();
            yNames.Insert(0, this.FileName);
            yNames.Insert(1, xName);

            Utils.ControlUtil.PaintChart(columnValues, yNames, this.chartTypesList.Text);
            // 存储图表挂件需要的数据
            hitItem.ChartType = this.chartTypesList.Text;
            hitItem.SelectedIndexs = indexs;
            hitItem.SelectedItems = yNames;
            this.DialogResult = DialogResult.OK;
            Close();
        }      
        private bool OptionNotReady()
        {
            bool notReady = true;
            int status0 = String.IsNullOrEmpty(this.chartTypesList.Text) ? 1 : 0;
            int status1 = String.IsNullOrEmpty(this.comboBox0.Text) ? 2 : 0;
            int status2 = this.outListCCBL0.GetItemCheckIndex().Count == 0 ? 4 : 0;
            switch (status0 | status1 | status2)
            {
                case 0:
                    notReady = false;
                    break;
                case 7:
                case 5:
                case 3:
                case 1:         
                    HelpUtil.ShowMessageBox("请设置图表类型");
                    break;
                case 6:
                case 2:
                    HelpUtil.ShowMessageBox("请设置输入维度");
                    break;
                case 4:
                    HelpUtil.ShowMessageBox("请设置输出维度");
                    break;
                default:
                    break;
            }        
            return notReady;
        }
 
        protected override void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
