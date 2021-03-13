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
using C2.Dialogs;

namespace C2.IAOLab.WebEngine.Dialogs
{
    partial class SelectMapDialog : StandardDialog
    {
        public string WebUrl;
        public string map;
        public string tude;
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

        public string drawlatude;

        public string drawlontude;

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
            clearComBox();
            this.hitItem = DataItems[datasourceComboBox.SelectedIndex];
            SetDropDown();
        }
        private void SetDropDown()
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
            map = this.mapTypeComboBox.Text;

            //生成html
            WebUrl = GenGisMapHtml.GetInstance().TransDataToHtml();
            if (OptionNotReady())
                return false;
            int upperLimit = 100;
            // 获得x,y轴数据的列索引
            int latIndex = latComboBox.Tag == null ? latComboBox.SelectedIndex : ConvertUtil.TryParseInt(latComboBox.Tag.ToString());
            List<int> indexlat = new List<int>() { latIndex };
            int lonIndex = lonComboBox.Tag == null ? lonComboBox.SelectedIndex : ConvertUtil.TryParseInt(lonComboBox.Tag.ToString());
            List<int> indexlon = new List<int>() { lonIndex };
            int countIndex = countComboBox.Tag == null ? countComboBox.SelectedIndex : ConvertUtil.TryParseInt(countComboBox.Tag.ToString());
            List<int> indexcount = new List<int>() { countIndex };

            // 获取选中输入、输出各列数据
            string fileContent;
            if (hitItem.FileType == OpUtil.ExtType.Excel)
                fileContent = BCPBuffer.GetInstance().GetCachePreviewExcelContent(FilePath);
            else
                fileContent = BCPBuffer.GetInstance().GetCachePreviewBcpContent(FilePath, FileEncoding);
            List<string> rows = new List<string>(fileContent.Split('\n'));
            upperLimit = Math.Min(rows.Count, upperLimit);
            List<List<string>> lonValues = Utils.FileUtil.GetColumns(indexlon, hitItem, rows, upperLimit);
            List<List<string>> latValues = Utils.FileUtil.GetColumns(indexlat, hitItem, rows, upperLimit);
            List<List<string>> countValues = Utils.FileUtil.GetColumns(indexcount, hitItem, rows, upperLimit);

            Random rm = new Random();
            drawlontude = lonValues[0][rm.Next(lonValues.Count)];
            drawlatude = latValues[0][rm.Next(latValues.Count)];
            if (!TudeLimit())
            {
                return false;
            }
            if (latValues.Count == 0)
            {
                HelpUtil.ShowMessageBox("文件内容为空");
                Close();
                return false;
            }
            if (latValues[0].Count != lonValues[0].Count || latValues[0].Count != lonValues[0].Count)
            {
                HelpUtil.ShowMessageBox("输入维度不一致");
                return false;
            }
            List<string> tmpList = new List<string>();
            //准备数据
            if (this.mapTypeComboBox.Text == "热力图")
            {
                string JSON_OBJ_Format_heat = "\"lng\": \" {0} \", \"lat\": \" {1} \", \"count\": \" {2} \"";
                for (int i = 0; i < latValues[0].Count; i++)
                {
                    tmpList.Add('{' + String.Format(JSON_OBJ_Format_heat, latValues[0][i], lonValues[0][i], countValues[0][i]) + '}'); 
                }

            }
            else
            {
                string JSON_OBJ_Format = "\"lng\": \" {0} \", \"lat\": \" {1} \"";
                for (int i = 0; i < latValues[0].Count; i++)
                {
                    tmpList.Add('{' + String.Format(JSON_OBJ_Format , latValues[0][i], lonValues[0][i]) + '}');
                }
            }

            tude = '[' + String.Join(",", tmpList.ToArray()) + ']';
            this.DialogResult = DialogResult.OK;
            Close();

            return base.OnOKButtonClick();
        }

        private bool TudeLimit()
        {
            if (double.Parse(drawlatude)>180|| double.Parse(drawlatude)<-180)
            {
                HelpUtil.ShowMessageBox("请重新输入正确的经度！");
                return false;
            }
            else if (double.Parse(drawlontude) > 90 || double.Parse(drawlontude) < -90)
            {
                HelpUtil.ShowMessageBox("请重新输入正确的纬度！");
                return false;
            }
            else
                return true;
        }

        private bool OptionNotReady()
        {
            bool notReady = true;
            int status0 = String.IsNullOrEmpty(this.mapTypeComboBox.Text) ? 1 : 0;
            int status1 = String.IsNullOrEmpty(this.latComboBox.Text) ? 2 : 0;
            int status2 = String.IsNullOrEmpty(this.lonComboBox.Text) ? 4 : 0;
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
                    HelpUtil.ShowMessageBox("请设置经度");
                    break;
                case 4:
                    HelpUtil.ShowMessageBox("请设置纬度");
                    break;
                default:
                    break;
            }
            return notReady;
        }

        private void mapTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.mapTypeComboBox.Text == "热力图")
                countComboBox.Enabled = true;
            else
                countComboBox.Enabled = false;
        }
    }
}
