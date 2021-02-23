using C2.Business.Option;
using C2.Controls;
using C2.Core;
using C2.IAOLab.WebEngine.GisMap;
using C2.Model;
using C2.Utils;
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

namespace C2.IAOLab.WebEngine.Dialogs
{
    partial class SelectMapDialog : StandardDialog
    {
        public string WebUrl;
        private BcpInfo bcpInfo;
        private DataItem hitItem;
        private string FilePath { get => hitItem.FilePath; }

        private OpUtil.Encoding FileEncoding { get => hitItem.FileEncoding; }
        private char FileSep { get => hitItem.FileSep; }
        private string FileName { get => hitItem.FileName; }
        public SelectMapDialog()
        {
            InitializeComponent();
        }
        public List<DataItem> DataItems;


        public SelectMapDialog(List<DataItem> dataItems)
        {
            InitializeComponent();
            WebUrl = Path.Combine(Application.StartupPath, "IAOLab\\WebEngine\\Html", "StartMap.html");
            DataItems = dataItems;
            foreach (DataItem dataItem in DataItems)
            {
                this.datasourceComboBox.Items.Add(dataItem.FileName);
            }
          
        }

        private void datasourceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DataItem dataItem in DataItems)
                if (this.datasourceComboBox.SelectedItem == dataItem.FileName)
                {
                    clearComBox();
                    this.hitItem = dataItem;
                }
                    
            InitializeDropDown();
        }
        private void InitializeDropDown()
        {
            
            this.bcpInfo = new BcpInfo(FilePath, FileEncoding, new char[] { FileSep });
            this.latComboBox.Items.AddRange(bcpInfo.ColumnArray);
            this.lonComboBox.Items.AddRange(bcpInfo.ColumnArray);
            this.countComboBox.Items.AddRange(bcpInfo.ColumnArray);

        }
        private void clearComBox() 
        {
            latComboBox.Items.Clear();
            latComboBox.Text = "";
            lonComboBox.Items.Clear();
            lonComboBox.Text = "";
            countComboBox.Items.Clear();
            countComboBox.Text = "";

        }

        protected override bool OnOKButtonClick()
        {
            //生成html
            WebUrl = GenGisMapHtml.GetInstance().TransDataToHtml();

            //if (OptionNotReady())
            //    return;
            int upperLimit = 100;

            // 获得x,y轴数据的列索引
            int xIndex = latComboBox.Tag == null ? latComboBox.SelectedIndex : ConvertUtil.TryParseInt(latComboBox.Tag.ToString());
            List<int> indexs = new List<int>() { xIndex };
            //indexs.AddRange(outListCCBL0.GetItemCheckIndex());

            // 获取选中输入、输出各列数据
            string fileContent;
            if (hitItem.FileType == OpUtil.ExtType.Excel)
                fileContent = BCPBuffer.GetInstance().GetCachePreviewExcelContent(FilePath);
            else
                fileContent = BCPBuffer.GetInstance().GetCachePreviewBcpContent(FilePath, FileEncoding);
            List<string> rows = new List<string>(fileContent.Split('\n'));
            upperLimit = Math.Min(rows.Count, upperLimit);

            List<List<string>> columnValues = Utils.FileUtil.GetColumns(indexs, hitItem, rows, upperLimit);
            if (columnValues.Count == 0)
            {
                HelpUtil.ShowMessageBox("文件内容为空");
                Close();
                //return;
            }
            // xy轴标题名
            string xName = latComboBox.SelectedItem.ToString();
           // List<string> yNames = outListCCBL0.GetItemCheckText();
            //yNames.Insert(0, this.FileName);
            //yNames.Insert(1, xName);

            //Utils.ControlUtil.PaintChart(columnValues, yNames, this.chartTypesList.Text);
            //// 存储图表挂件需要的数据
            //hitItem.ChartType = this.chartTypesList.Text;
            //hitItem.SelectedIndexs = indexs;
            //hitItem.SelectedItems = yNames;
            //this.DialogResult = DialogResult.OK;
            Close();

            return base.OnOKButtonClick();
        }

        private bool OptionNotReady()
        {
            bool notReady = true;
            //int status0 = String.IsNullOrEmpty(this.chartTypesList.Text) ? 1 : 0;
            //int status1 = String.IsNullOrEmpty(this.comboBox0.Text) ? 2 : 0;
            //int status2 = this.outListCCBL0.GetItemCheckIndex().Count == 0 ? 4 : 0;
            //switch (status0 | status1 | status2)
            //{
            //    case 0:
            //        notReady = false;
            //        break;
            //    case 7:
            //    case 5:
            //    case 3:
            //    case 1:
            //        HelpUtil.ShowMessageBox("请设置图表类型");
            //        break;
            //    case 6:
            //    case 2:
            //        HelpUtil.ShowMessageBox("请设置输入维度");
            //        break;
            //    case 4:
            //        HelpUtil.ShowMessageBox("请设置输出维度");
            //        break;
            //    default:
            //        break;
            //}
            return notReady;
        }

       
    }
}
