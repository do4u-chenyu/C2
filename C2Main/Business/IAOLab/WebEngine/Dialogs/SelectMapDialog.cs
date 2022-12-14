using C2.Business.Option;
using C2.Controls;
using C2.Core;
using C2.IAOLab.WebEngine.GisMap;
using C2.Model;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace C2.IAOLab.WebEngine.Dialogs
{
    partial class SelectMapDialog : StandardDialog
    {
        public string WebUrl;
        public string map;
        public string tude;
        public List<MapPoint> mapPoints;
        private BcpInfo bcpInfo;
        private DataItem hitItem;
        private string FilePath { get => hitItem.FilePath; }

        private OpUtil.Encoding FileEncoding { get => hitItem.FileEncoding; }
        private char FileSep { get => hitItem.FileSep; }
        private string FileName { get => hitItem.FileName; }
        public int LatIndex;
        public int LngIndex;
        public int WeightIndex;
        public SelectMapDialog()
        {
            InitializeComponent();
        }
        public DataItem HitItem { get { return this.hitItem; } }
        public List<DataItem> DataItems;

        public string drawlatitude;

        public string drawlongitude;

        public SelectMapDialog(List<DataItem> dataItems)
        {
            InitializeComponent();
            mapPoints = new List<MapPoint>();
            WebUrl = Path.Combine(Global.WebEnginePath, "Html", "StartMap.html");
            DataItems = dataItems;
            foreach (DataItem dataItem in DataItems)
            {
                if (dataItem.FileType == OpUtil.ExtType.Database)
                    BCPBuffer.GetInstance().GetCachePreviewTable(dataItem.DBItem);
                this.datasourceComboBox.Items.Add(dataItem.FileName);
            }
            if (DataItems.Count == 1)
                this.datasourceComboBox.SelectedIndex = 0;
        }

        private void DatasourceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearComBox();
            if (datasourceComboBox.SelectedIndex > -1 && DataItems.Count > 0)
                this.hitItem = DataItems[datasourceComboBox.SelectedIndex];
            SetDropDown();
        }
        private void SetDropDown()
        {
            this.bcpInfo = new BcpInfo(FilePath, FileEncoding, new char[] { FileSep });
            this.lonComboBox.Items.AddRange(bcpInfo.ColumnArray);
            this.latComboBox.Items.AddRange(bcpInfo.ColumnArray);
            this.countComboBox.Items.AddRange(bcpInfo.ColumnArray);

        }
        private void ClearComBox() 
        {
            lonComboBox.Items.Clear();
            lonComboBox.Text = string.Empty;
            latComboBox.Items.Clear();
            latComboBox.Text = string.Empty;
            countComboBox.Items.Clear();
            countComboBox.Text = string.Empty;

        }

        protected override bool OnOKButtonClick()
        {
            new Log.Log().LogManualButton("图上作战", "运行");
            map = mapTypeComboBox.Text;

            //生成html
            WebUrl = GenGisMapHtml.GetInstance().TransDataToHtml();
            if (OptionNotReady())
                return false;
            int upperLimit = 100;
            // 获得x,y轴数据的列索引
            LngIndex = lonComboBox.Tag == null ? lonComboBox.SelectedIndex : ConvertUtil.TryParseInt(lonComboBox.Tag.ToString());
            _ = new List<int>() { LngIndex };
            LatIndex = latComboBox.Tag == null ? latComboBox.SelectedIndex : ConvertUtil.TryParseInt(latComboBox.Tag.ToString());
            _ = new List<int>() { LatIndex };
            WeightIndex = countComboBox.Tag == null ? countComboBox.SelectedIndex : ConvertUtil.TryParseInt(countComboBox.Tag.ToString());
            _ = new List<int>() { WeightIndex };

            // 获取选中输入、输出各列数据
            string fileContent;
            if (hitItem.FileType == OpUtil.ExtType.Excel)
                fileContent = BCPBuffer.GetInstance().GetCachePreviewExcelContent(FilePath);
            else if (hitItem.FileType == OpUtil.ExtType.Text)
                fileContent = BCPBuffer.GetInstance().GetCachePreviewBcpContent(FilePath, FileEncoding);
            else if (hitItem.FileType == OpUtil.ExtType.Database)
                fileContent = BCPBuffer.GetInstance().GetCachePreviewTable(hitItem.DBItem);
            else
            {
                HelpUtil.ShowMessageBox(HelpUtil.InvalidDataType);
                return false;
            }
            List<string> rows = new List<string>(fileContent.Split('\n'));
            upperLimit = Math.Min(rows.Count, upperLimit);
            List<List<string>> lonLatCountList = GetLonLatCountColumns(new List<int>() { LngIndex, LatIndex, WeightIndex }, hitItem, rows, upperLimit);
            List<List<string>> lonValues = new List<List<string>>() { lonLatCountList[0] };
            List<List<string>> latValues = new List<List<string>>() { lonLatCountList[1] };
            List<List<string>> countValues = new List<List<string>>() { lonLatCountList[2] };

            //List<List<string>> lonValues = Utils.FileUtil.GetColumns(indexlon, hitItem, rows, upperLimit);
            //List<List<string>> latValues = Utils.FileUtil.GetColumns(indexlat, hitItem, rows, upperLimit);
            //List<List<string>> countValues = Utils.FileUtil.GetColumns(indexcount, hitItem, rows, upperLimit);

            if (latValues[0].Count == 0 || lonValues[0].Count == 0)
            {
                HelpUtil.ShowMessageBox(HelpUtil.InvalidInputMapInfo);
                return false;
            }
            Random rm = new Random();
            drawlongitude = lonValues[0][rm.Next(lonValues[0].Count)];
            drawlatitude = latValues[0][rm.Next(latValues[0].Count)];
            if (!InvalidLonValues(lonValues))
            {
                HelpUtil.ShowMessageBox(HelpUtil.InvalidLongitude);
                return false;
            }
            if (!InvalidLatValues(latValues))
            {
                HelpUtil.ShowMessageBox(HelpUtil.InvalidLatitude);
                return false;
            }
            if (latValues[0].Count != lonValues[0].Count || latValues[0].Count != lonValues[0].Count)
            {
                HelpUtil.ShowMessageBox(HelpUtil.InvalidDimension);
                return false;
            }
            List<string> tmpList = new List<string>();
            //准备数据
            if (this.mapTypeComboBox.Text == "热力图")
            {
                if (countValues[0].Count == 0 || latValues[0].Count != countValues[0].Count || lonValues[0].Count != countValues[0].Count)
                {
                    HelpUtil.ShowMessageBox(HelpUtil.InvalidCount);
                    return false;
                }
                string JSON_OBJ_Format_heat = "\"lng\": \" {0} \", \"lat\": \" {1} \", \"count\": \" {2} \"";
                for (int i = 0; i < latValues[0].Count; i++)
                {
                    tmpList.Add('{' + String.Format(JSON_OBJ_Format_heat, lonValues[0][i], latValues[0][i], countValues[0][i]) + '}');
                    mapPoints.Add(new MapPoint(lonValues[0][i], latValues[0][i]));
                }

            }
            else
            {
                string JSON_OBJ_Format = "\"lng\": \" {0} \", \"lat\": \" {1} \"";
                for (int i = 0; i < latValues[0].Count; i++)
                {
                    tmpList.Add('{' + String.Format(JSON_OBJ_Format , lonValues[0][i], latValues[0][i]) + '}');
                    mapPoints.Add(new MapPoint(lonValues[0][i], latValues[0][i]));
                }
            }

            tude = '[' + String.Join(",", tmpList.ToArray()) + ']';
            this.DialogResult = DialogResult.OK;
            Close();

            return base.OnOKButtonClick();
        }

        private bool InvalidCountValues(List<List<string>> countValues)
        {
            if (countValues.Count == 0)
                return true;
            foreach (string count in countValues[0])
            {
                if (!float.TryParse(count, out float result) || result < 0)
                    return false;
            }
            return true;
        }

        private bool InvalidLatValues(List<List<string>> latValues)
        {
            if (latValues.Count == 0)
                return false;
            foreach (string latStr in latValues[0])
            {
                if (!float.TryParse(latStr, out float result) || result < -90 && result > 90)
                    return false;
            }
            return true;
        }

        private bool InvalidLonValues(List<List<string>> lonValues)
        {
            if (lonValues.Count == 0)
                return false;
            foreach(string lonStr in lonValues[0])
            {
                if (!float.TryParse(lonStr, out float result) || result < -180 && result > 180)
                    return false;
            }
            return true;
        }
        private bool InvalidValue(string value)
        {
            //是浮点类型，且是-180到180范围内
            return (float.TryParse(value, out float result) && result > -180 && result < 180);
        }

        private bool InvalidCountValue(string value)
        {
            //是浮点类型，且大于0
            return (float.TryParse(value, out float result) && result > 0);
        }

        private bool OptionNotReady()
        {
            bool notReady = true;
            int status0 = String.IsNullOrEmpty(this.mapTypeComboBox.Text) ? 1 : 0;
            int status1 = String.IsNullOrEmpty(this.lonComboBox.Text) ? 2 : 0;
            int status2 = String.IsNullOrEmpty(this.latComboBox.Text) ? 4 : 0;
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
            if (this.countComboBox.Enabled && String.IsNullOrEmpty(this.countComboBox.Text))
            {
                HelpUtil.ShowMessageBox("请设置权重");
                notReady = true;
            }   
            return notReady;
        }

        private void MapTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.mapTypeComboBox.Text == "热力图")
                countComboBox.Enabled = true;
            else
                countComboBox.Enabled = false;
        }

        public List<List<string>> GetLonLatCountColumns(List<int> indexs, DataItem dataItem, List<string> rows, int upperLimit)
        {
            int lonIdx = indexs[0];
            int latIdx = indexs[1];
            int countIdx = indexs[2];

            List<List<string>> columnValues = new List<List<string>>() { new List<string>(), new List<string>(), new List<string>()};
            for (int i = 0; i < upperLimit; i++)
            {
                if (i == 0)
                    continue;
                string row = rows[i].TrimEnd('\r');

                if (String.IsNullOrEmpty(row))
                    continue;
                string[] rowElement = row.Split(dataItem.FileSep);
                if (rowElement.Length <= indexs.Max())
                    continue;
                //有非法格式的，跳过该行
                if (!InvalidValue(rowElement[lonIdx]) || !InvalidValue(rowElement[latIdx]) || (countIdx != -1 && !InvalidCountValue(rowElement[countIdx])))
                    continue;

                columnValues[0].Add(rowElement[lonIdx]);
                columnValues[1].Add(rowElement[latIdx]);
                columnValues[2].Add(countIdx == -1 ? string.Empty : rowElement[lonIdx]);

            }
            return columnValues;
        }
    }
}
