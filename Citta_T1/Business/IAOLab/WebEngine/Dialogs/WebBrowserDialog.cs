using C2.Controls;
using C2.Core;
using C2.Dialogs;
using C2.Model;
using C2.Model.MindMaps;
using C2.Model.Widgets;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using static C2.IAOLab.WebEngine.WebManager;

namespace C2.IAOLab.WebEngine.Dialogs
{

    partial class WebBrowserDialog : StandardDialog
    {
        private ToolStripButton LoadMapData;
        private ToolStripButton LoadBossData;
        private ToolStripButton SaveHtml;
        private ToolStripButton SavePic;
        private ToolStripButton Clear;
        private ToolStripButton EditCode;
        private readonly MapConfig MapConfig;
        private Point WebBrowserFullLocation;
        private Point WebBrowserHalfLocation;


        public WebType WebType;
        public Topic HitTopic;
        public List<DataItem> DataItems;
        public string Title { set => this.Text = value; get => this.Text; }
        public bool SourceCodeMapActive;

        public string WebUrl;

        public string SourceWebUrl;
        private readonly string picPath;
        public Dictionary<string, int[]> ChartOptions;
        public PictureWidget.PictureDesign CurrentObject;

        public WebBrowserDialog()
        {
            InitializeComponent();
            Title = string.Empty;
            WebUrl = string.Empty;
            ChartOptions = new Dictionary<string, int[]>();
            picPath = Path.Combine(Global.TempDirectory, "boss.png");
            SourceWebUrl = string.Empty;
            WebBrowserFullLocation = this.webBrowser1.Location;
            WebBrowserHalfLocation = new Point(this.Width / 2, this.WebBrowserFullLocation.Y);
        }
        public WebBrowserDialog(Topic hitTopic, WebType webType) : this()
        {
            HitTopic = hitTopic;
            DataItems = hitTopic.GetDataItems();
            WebType = webType;
            MapConfig = InitMapConfig();
            SourceCodeMapActive = false;
        }
        /// <summary>
        /// 初始化一个地图配置项
        /// 1. 当用户选择保存时，将配置项保存至MapWidget中
        /// 2. 当用户选择取消时，不保存配置项
        /// </summary>
        /// <returns></returns>
        private MapConfig InitMapConfig()
        {
            MapConfig tmp = new MapConfig();
            if (HitTopic == null)
                return tmp;
            MapWidget mapWidget = HitTopic.FindWidget<MapWidget>();
            if (mapWidget != null)
            {
                tmp = mapWidget.MapConfig.Clone();
            }
            return tmp;
        }
        #region 窗体事件
        private void WebBrowserDialog_Load(object sender, EventArgs e)
        {
            WebBrowserConfig.SetWebBrowserFeatures(11);//TODO 暂定11，后面需要检测
            webBrowser1.Navigate(WebUrl);
            if (WebType == WebType.Boss)//数据大屏初次打开是自动弹出配置窗口
                OpenSelectBossDialog();
        }

        private object[] OpenMapFile(string path, char seperator, int latIndex, int lonIndex)
        {
            if (!File.Exists(path))
            {
                HelpUtil.ShowMessageBox(HelpUtil.FileNotFoundHelpInfo + ", 文件路径：" + path);
                return new object[] { String.Empty };
            }
            List<string> latValues = new List<string>();
            List<string> lonValues = new List<string>();
            String line;
            int lineCounter = 0;
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if (lineCounter++ == 0)
                        continue;
                    string[] tempstr = line.Split(seperator);
                    for (int i = 0; i < tempstr.Length; i++)
                    {
                        latValues.Add(tempstr[latIndex]);
                        lonValues.Add(tempstr[lonIndex]);
                    }
                    lineCounter += 1;
                }
            }
            string JSON_OBJ_Format = "\"lng\": \" {0} \", \"lat\": \" {1} \"";
            List<string> tmpList = new List<string>();
            for (int i = 0; i < latValues.Count; i++)
            {
                tmpList.Add('{' + String.Format(JSON_OBJ_Format, lonValues[i], latValues[i]) + '}');
            }
            string res = '[' + string.Join(",", tmpList.ToArray()) + ']';
            return new object[] { res };
        }
        private object[] OpenHeatMapFile(string path, char seperator, int latIndex, int lonIndex, int weightIndex)
        {
            List<string> latValues = new List<string>();
            List<string> lonValues = new List<string>();
            List<string> weightValues = new List<string>();
            String line;
            int lineCounter = 0;
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if (lineCounter++ == 0)
                        continue;
                    string[] tempstr = line.Split(seperator);
                    for (int i = 0; i < tempstr.Length; i++)
                    {
                        latValues.Add(tempstr[latIndex]);
                        lonValues.Add(tempstr[lonIndex]);
                        weightValues.Add(tempstr[weightIndex]);
                    }
                }
            }
            string JSON_OBJ_Format_heat = "\"lng\": \" {0} \", \"lat\": \" {1} \", \"count\": \" {2} \"";
            List<string> tmpList = new List<string>();
            for (int i = 0; i < latValues.Count; i++)
            {
                tmpList.Add('{' + String.Format(JSON_OBJ_Format_heat, lonValues[i], latValues[i], weightValues[i]) + '}');
            }
            string res = '[' + string.Join(",", tmpList.ToArray()) + ']';
            return new object[] { res };
        }
        private void WebBrowserDialog_Activated(object sender, EventArgs e)
        {
            webBrowser1.Focus();
        }
        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            /*
             * 打开地图 进行初始化
             * 1. 检查有没有存好的DataItem
             * 2. 加载地图
             */
            if (WebType == WebType.Map)
            {
                if (MapConfig.MapType == MapType.StartMap)
                    InitStartMapByConfig();
                else
                {
                    if (!SourceCodeMapActive)
                        ShowSourceCodeMap();
                    InitSourceCodeMapByConfig();
                }
            }
        }

        private void InitStartMapByConfig()
        {
            string configstr = String.Format("{0},{1},{2}", MapConfig.InitLng, MapConfig.InitLat, MapConfig.Zoom);
            webBrowser1.Document.InvokeScript("initialMap", new object[] { configstr });
            foreach (OverlapConfig oc in MapConfig.OverlapConfigList)
            {
                DataItem di = oc.DataItem;
                object[] datas = oc.OverlapType == OverlapType.Heatmap ?
                    OpenHeatMapFile(di.FilePath, di.FileSep, oc.LatIndex, oc.LngIndex, oc.WeightIndex) :
                    OpenMapFile(di.FilePath, di.FileSep, oc.LatIndex, oc.LngIndex);
                if (datas.Length == 0)
                    continue;
                switch (oc.OverlapType)
                {
                    case OverlapType.Marker:
                        webBrowser1.Document.InvokeScript("markerPoints", datas);
                        break;
                    case OverlapType.Polygon:
                        webBrowser1.Document.InvokeScript("drawPolygon", datas);
                        break;
                    case OverlapType.Orit:
                        webBrowser1.Document.InvokeScript("drawOrit", datas);
                        break;
                    case OverlapType.Heatmap:
                        webBrowser1.Document.InvokeScript("drawHeatmap", datas);
                        break;
                    default:
                        break;
                }
            }
        }
        private void InitSourceCodeMapByConfig()
        {
            this.htmlEditorControlEx1.Text = this.MapConfig.SourceCode;
        }
        #endregion


        public void InitializeMapToolStrip()
        {
            LoadMapData = new ToolStripButton();
            SaveHtml = new ToolStripButton();
            SavePic = new ToolStripButton();
            Clear = new ToolStripButton();
            EditCode = new ToolStripButton();

            // LoadMapData
            LoadMapData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            //LoadMapData.Image = global::C2.Properties.Resources.designer;
            LoadMapData.Image = global::C2.Properties.Resources.map_setting;
            LoadMapData.Text = "参数配置";
            LoadMapData.Click += new System.EventHandler(this.LoadMapData_Click);

            // SavePic
            SavePic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            //SavePic.Image = global::C2.Properties.Resources.image;
            SavePic.Image = global::C2.Properties.Resources.map_save;
            SavePic.Text = "保存成图片";
            SavePic.Click += new System.EventHandler(this.SavePic_Click);

            // Clear
            Clear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            //Clear.Image = global::C2.Properties.Resources.delete;
            Clear.Image = global::C2.Properties.Resources.map_clear;
            Clear.Text = "清空";
            Clear.Click += new System.EventHandler(this.Clear_Click);

            // EditCode
            EditCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            //EditCode.Image = global::C2.Properties.Resources.edit_code;
            EditCode.Image = global::C2.Properties.Resources.map_sourceCode;
            EditCode.Text = "自定义源码";
            EditCode.Click += new System.EventHandler(this.EditCode_Click);

            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                LoadMapData,
                EditCode,
                SavePic,
                Clear});
        }
        public void InitializeBossToolStrip()
        {
            LoadBossData = new ToolStripButton();
            SaveHtml = new ToolStripButton();
            SavePic = new ToolStripButton();

            // LoadBossData
            LoadBossData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            LoadBossData.Image = global::C2.Properties.Resources.designer;
            LoadBossData.Text = "参数配置";
            LoadBossData.Click += new System.EventHandler(this.LoadBossData_Click);

            // SaveHtml
            SaveHtml.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            SaveHtml.Image = global::C2.Properties.Resources.save;
            SaveHtml.Text = "保存成html";

            // SavePic
            SavePic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            SavePic.Image = global::C2.Properties.Resources.image;
            SavePic.Text = "保存成图片";
            SavePic.Click += new System.EventHandler(this.SavePic_Click);

            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                LoadBossData,
                SaveHtml,
                SavePic
            });
        }

        #region 地图菜单事件
        void LoadMapData_Click(object sender, EventArgs e)
        {
            /* 将数据上图
             * 加载地图的时候应该把MapWidget的DataItems写入
             * 1. new DataItem
             * 2. HitTopic.FindWidget<MapWidget>().DataItems.Add()
             */
            SelectMapDialog dialog = new SelectMapDialog(DataItems);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string[] args = new string[1] { dialog.tude };
                switch (dialog.map)
                {
                    case "标注图":
                        webBrowser1.Document.InvokeScript("markerPoints", args);
                        AddDataItem(OverlapType.Marker, dialog.LatIndex, dialog.LngIndex, dialog.WeightIndex, dialog.HitItem);
                        break;
                    case "轨迹图":
                        webBrowser1.Document.InvokeScript("drawOrit", args);
                        AddDataItem(OverlapType.Orit, dialog.LatIndex, dialog.LngIndex, dialog.WeightIndex, dialog.HitItem);
                        break;
                    case "多边形图":
                        webBrowser1.Document.InvokeScript("drawPolygon", args);
                        AddDataItem(OverlapType.Polygon, dialog.LatIndex, dialog.LngIndex, dialog.WeightIndex, dialog.HitItem);
                        break;
                    case "热力图":
                        webBrowser1.Document.InvokeScript("drawHeatmap", args);
                        AddDataItem(OverlapType.Heatmap, dialog.LatIndex, dialog.LngIndex, dialog.WeightIndex, dialog.HitItem);
                        break;
                }
                var configMap = new ConfigForm();
                string newCenterAndZoom = dialog.drawlontude + ',' + dialog.drawlatude + ',' + configMap.scale;
                webBrowser1.Document.InvokeScript("centerAndZoom", new object[] { newCenterAndZoom });
            }
            else
                return;
        }
        /// <summary>
        /// 将数据添加到临时数组里
        /// </summary>
        /// <param name="overlapType"></param>
        /// <param name="jsonData"></param>
        private void AddDataItem(OverlapType overlapType, int latIndex, int lngIndex, int weightIndex, DataItem dataItem)
        {
            OverlapConfig mdi = new OverlapConfig(latIndex, lngIndex, weightIndex, dataItem, overlapType);
            MapConfig.OverlapConfigList.Add(mdi);
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            ClearOverlap();
        }

        private void ClearOverlap()
        {
            webBrowser1.Document.InvokeScript("clearAll");
            MapConfig.OverlapConfigList.Clear();
        }

        private void EditCode_Click(object sender, EventArgs e)
        {
            if (!SourceCodeMapActive)
            {
                SaveCenterAndZoom();
                ShowSourceCodeMap();
                MapConfig.MapType = MapType.SourceCodeMap;
                InitSourceCodeMapByConfig();
            }
            else
            {
                SaveSourceCode();
                HideSourceCodeMap();
                MapConfig.MapType = MapType.StartMap;

                WebUrl = Path.Combine(Application.StartupPath, "Business\\IAOLab\\WebEngine\\Html", "StartMap.html");
                webBrowser1.Navigate(WebUrl);

                InitStartMapByConfig();
            }
        }

        private void HideSourceCodeMap()
        {
            this.editorPanel.Visible = false;
            this.editorPanel.Enabled = false;
            this.webBrowser1.Location = WebBrowserFullLocation;
            this.LoadMapData.Enabled = true;
            this.SavePic.Enabled = true;
            this.SourceCodeMapActive = !SourceCodeMapActive;
        }

        private void ShowSourceCodeMap()
        {
            this.editorPanel.Visible = true;
            this.editorPanel.Enabled = true;
            this.editorPanel.Width = this.Width / 2;
            this.webBrowser1.Location = WebBrowserHalfLocation;
            this.LoadMapData.Enabled = false;
            this.SavePic.Enabled = false;
            this.SourceCodeMapActive = !SourceCodeMapActive;
        }

        private void SaveSourceCode()
        {
            this.MapConfig.SourceCode = this.htmlEditorControlEx1.Text;
        }
        private void SaveCenterAndZoom()
        {
            if (MapConfig.MapType != MapType.StartMap)
                return;
            dynamic data = webBrowser1.Document.InvokeScript("eval", new[] {
                "(function() { return {lat: map.getCenter()[\"lat\"], lng: map.getCenter()[\"lng\"], zoom: map.getZoom()}; })()"
            });
            if (data.lat != null)
                MapConfig.InitLat = (float)data.lat;
            if (data.lng != null)
                MapConfig.InitLng = (float)data.lng;
            if (data.zoom != null)
                MapConfig.Zoom = (int)data.zoom;
        }
        /// <summary>
        /// 得有文件，要不然不能使用WebBrowser访问
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunButton_Click(object sender, EventArgs e)
        {
            ClearOverlap();
            this.MapConfig.SourceCode = this.htmlEditorControlEx1.Text;
            string tempDir = FileUtil.TryGetSysTempDir();
            Global.TempDirectory = Path.Combine(tempDir, "FiberHomeIAOTemp");
            string tmpHtmlFilePath = Path.Combine(Global.TempDirectory, "editorMap.html");
            if (File.Exists(tmpHtmlFilePath))
                File.Delete(tmpHtmlFilePath);
            using (StreamWriter sw = new StreamWriter(tmpHtmlFilePath, false, System.Text.Encoding.UTF8))
                sw.Write(this.htmlEditorControlEx1.Text);
            webBrowser1.Navigate(tmpHtmlFilePath);
        }
        private void ResetButton_Click(object sender, EventArgs e)
        {
            this.htmlEditorControlEx1.Text = Properties.Resources.SourceCodeMap;
        }
        #endregion

        #region 数据大屏菜单事件
        void LoadBossData_Click(object sender, EventArgs e)
        {
            OpenSelectBossDialog();
        }

        public void OpenSelectBossDialog()
        {
            var dialog = new SelectBossDialog(DataItems, ChartOptions, webBrowser1);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                webBrowser1.Navigate(dialog.WebUrl);
                ChartOptions = dialog.ChartOptions;
            }
        }

        void SavePic_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(webBrowser1.DrawToImage(), new Size(webBrowser1.Width, webBrowser1.Height));
            bitmap.Save(picPath, ImageFormat.Png);

            SaveFileDialog fd = new SaveFileDialog
            {
                Filter = "图片文件(*.png)|*.png",
                AddExtension = true
            };
            if (fd.ShowDialog() != DialogResult.OK)
                return;
            File.Copy(picPath, fd.FileName, true);
        }
        #endregion

        protected override bool OnOKButtonClick()
        {
            if (WebType == WebType.Boss && ChartOptions.ContainsKey("Datasource"))
            {
                string path = Path.Combine(Global.UserWorkspacePath, "业务视图", Global.GetCurrentDocument().Name, String.Format("数据大屏{0}.png", HitTopic.ID));
                Bitmap bitmap = new Bitmap(webBrowser1.Width, webBrowser1.Height);
                Rectangle rectangle = new Rectangle(0, 0, webBrowser1.Width, webBrowser1.Height);  // 绘图区域
                webBrowser1.DrawToBitmap(bitmap, rectangle);
                bitmap.Save(path);

                //当前webbrowser截图，作为图片挂件加入当前节点
                PictureWidget template = new PictureWidget();
                CurrentObject = new PictureWidget.PictureDesign
                {
                    SourceType = PictureSource.File,
                    Url = path,
                    AddToLibrary = false,
                    LimitImageSize = true,
                    Name = Path.GetFileNameWithoutExtension(path),
                    EmbedIn = false
                };
                template.Image = CurrentObject;
                template.SizeType = PictureSizeType.Thumb;
                HitTopic.Add(template);
            }
            else if (WebType == WebType.Map)
            {
                SaveMapConfig();
            }
            return base.OnOKButtonClick();
        }
        protected override bool OnCancelButtonClick()
        {
            return base.OnCancelButtonClick();
        }
        /// <summary>
        /// 保存地图配置信息到MapWidget中
        /// 这里不做配置项的持久化
        /// </summary>
        private void SaveMapConfig()
        {
            // TODO 获得当前的经纬度和zoom等级
            SaveSourceCode();
            SaveCenterAndZoom();
            MapWidget mw = HitTopic.FindWidget<MapWidget>();
            mw.MapConfig = MapConfig;
        }
    }
    #region 内部类
    enum OverlapType
    {
        Marker,
        Polygon,
        Orit,
        Heatmap
    }
    enum MapType
    {
        StartMap,
        SourceCodeMap
    }
    /// <summary>
    /// 每一个地图数据都要有的东西地图配置信息
    /// </summary>
    class OverlapConfig
    {
        public int LatIndex;
        public int LngIndex;
        public int WeightIndex;
        public DataItem DataItem;
        public OverlapType OverlapType;
        public OverlapConfig(int latIdx, int lngIdx, int weightIdx, DataItem dataItem, OverlapType overlapType)
        {
            this.LatIndex = latIdx;
            this.LngIndex = lngIdx;
            this.WeightIndex = weightIdx;
            this.DataItem = dataItem;
            this.OverlapType = overlapType;
        }
    }
    class MapConfig
    {
        const float defaultLat = (float)36.269395;
        const float defaultLng = (float)108.876433;
        const int defaultZoom = 5;
        const MapType defaultMapType = MapType.StartMap;
        public float InitLat;
        public float InitLng;
        public int Zoom;
        public MapType MapType;
        public List<OverlapConfig> OverlapConfigList;
        public string SourceCode;
        /// <summary>
        /// 优先解析配置窗口里的经纬度缩放比
        /// </summary>
        public MapConfig()
        {
            var configForm = new ConfigForm();
            if (!float.TryParse(configForm.latude, out this.InitLat))
                this.InitLat = defaultLat;
            if (!float.TryParse(configForm.lontude, out this.InitLng))
                this.InitLng = defaultLng;
            if (!int.TryParse(configForm.scale, out this.Zoom))
                this.Zoom = defaultZoom;
            this.MapType = defaultMapType;
            this.OverlapConfigList = new List<OverlapConfig>();
            this.SourceCode = Properties.Resources.SourceCodeMap;
        }
        public MapConfig(float initLat, float initLng, int zoom, MapType mapType, List<OverlapConfig> mapDataConfigList, string sourceCode)
        {
            this.InitLat = initLat;
            this.InitLng = initLng;
            this.Zoom = zoom;
            this.MapType = mapType;
            this.OverlapConfigList = new List<OverlapConfig>();
            foreach (OverlapConfig mapDataConfig in mapDataConfigList)
                this.OverlapConfigList.Add(mapDataConfig);
            this.SourceCode = sourceCode;
        }

        public MapConfig Clone()
        {
            return new MapConfig(this.InitLat, this.InitLng, this.Zoom, this.MapType, this.OverlapConfigList, this.SourceCode);
        }
    }
    #endregion
}
