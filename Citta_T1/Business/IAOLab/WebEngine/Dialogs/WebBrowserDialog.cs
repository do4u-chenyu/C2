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
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton Clear;
        private ToolStripButton EditCode;

        public WebType WebType;
        public Topic HitTopic;
        public List<DataItem> DataItems;
        public string Title { set => this.Text = value; get => this.Text; }
        public string WebUrl;

        public string SourceWebUrl;
        bool isActive = true;
        bool clearClick = false;
        private string picPath;
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
        }

        public WebBrowserDialog(Topic hitTopic, WebType webType) : this()
        {
            HitTopic = hitTopic;
            DataItems = hitTopic.GetDataItems();
            WebType = webType;
        }

        #region 窗体事件
        private void WebBrowserDialog_Load(object sender, EventArgs e)
        {
            WebBrowserConfig.SetWebBrowserFeatures(11);//TODO 暂定11，后面需要检测
            webBrowser1.Navigate(WebUrl);
            if (WebType == WebType.Boss)//数据大屏初次打开是自动弹出配置窗口
                OpenSelectBossDialog();
        }

        private object[] OpenMapFile(string path)
        {
            List<string> latValues = new List<string>();
            List<string> lonValues = new List<string>();
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] tempstr = line.Split(',');

                for (int i = 0; i < tempstr.Length; i++)
                {
                    if (i % 2 == 0)
                        latValues.Add(tempstr[i]);
                    else
                        lonValues.Add(tempstr[i]);
                }
            }
            // TODO DK 热力图没考虑
            string JSON_OBJ_Format = "\"lng\": \" {0} \", \"lat\": \" {1} \"";
            List<string> tmpList = new List<string>();
            for (int i = 0; i < latValues.Count; i++)
            {
                tmpList.Add('{' + String.Format(JSON_OBJ_Format, latValues[i], lonValues[i]) + '}');
            }
            string res = '[' + string.Join(",", tmpList.ToArray()) + ']';
            sr.Close();
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
                var configMap = new ConfigForm();
                string configstr = configMap.latude + ',' + configMap.lontude + ',' + configMap.scale;
                webBrowser1.Document.InvokeScript("initialMap", new object[] { configstr });
                MapWidget maw = HitTopic.FindWidget<MapWidget>();
                foreach (DataItem di in maw.DataItems)
                {
                    if (di.FileName.Contains("标注图") && File.Exists(di.FilePath))
                        webBrowser1.Document.InvokeScript("markerPoints", OpenMapFile(di.FilePath));
                    if (di.FileName.Contains("多边形图") && File.Exists(di.FilePath))
                        webBrowser1.Document.InvokeScript("drawPolygon", OpenMapFile(di.FilePath));
                    if (di.FileName.Contains("轨迹图") && File.Exists(di.FilePath))
                        webBrowser1.Document.InvokeScript("drawOrit", OpenMapFile(di.FilePath));
                    if (di.FileName.Contains("热力图") && File.Exists(di.FilePath))
                        webBrowser1.Document.InvokeScript("drawHeatmap", OpenMapFile(di.FilePath));
                }
            }
        }
        #endregion


        public void InitializeMapToolStrip()
        {
            LoadMapData = new ToolStripButton();
            SaveHtml = new ToolStripButton();
            SavePic = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            Clear = new ToolStripButton();
            EditCode = new ToolStripButton();

            // LoadMapData
            LoadMapData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            LoadMapData.Image = global::C2.Properties.Resources.designer;
            LoadMapData.Text = "参数配置";
            LoadMapData.Click += new System.EventHandler(this.LoadMapData_Click);

            // SaveHtml
            SaveHtml.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            SaveHtml.Image = global::C2.Properties.Resources.save;
            SaveHtml.Text = "保存成html";

            // SavePic
            SavePic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            SavePic.Image = global::C2.Properties.Resources.image;
            SavePic.Text = "保存成图片";
            SavePic.Click += new System.EventHandler(this.SavePic_Click);

            // Clear
            Clear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            Clear.Image = global::C2.Properties.Resources.delete;
            Clear.Text = "清空";
            Clear.Click += new System.EventHandler(this.Clear_Click);

            // EditCode
            EditCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            EditCode.Image = global::C2.Properties.Resources.edit_code;
            EditCode.Text = "自定义源码";
            EditCode.Click += new System.EventHandler(this.EditCode_Click);

            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                LoadMapData,
                SaveHtml,
                SavePic,
                toolStripSeparator1,
                Clear,
                EditCode});
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
            var dialog = new SelectMapDialog(DataItems);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string[] args = new string[1];
                args[0] = dialog.tude;
                switch (dialog.map)
                {
                    case "标注图":
                        webBrowser1.Document.InvokeScript("markerPoints", args);
                        SavePointsToDataItem("标注图", args[0]);
                        break;
                    case "轨迹图":
                        webBrowser1.Document.InvokeScript("drawOrit", args);
                        SavePointsToDataItem("轨迹图", args[0]);
                        break;
                    case "多边形图":
                        webBrowser1.Document.InvokeScript("drawPolygon", args);
                        SavePointsToDataItem("多边形图", args[0]);
                        break;
                    case "热力图":
                        webBrowser1.Document.InvokeScript("drawHeatmap", args);
                        SavePointsToDataItem("热力图", args[0]);
                        break;
                }
                var configMap = new ConfigForm();
                string newCenterAndZoom = dialog.drawlatude + ',' + dialog.drawlontude + ',' + configMap.scale;
                webBrowser1.Document.InvokeScript("centerAndZoom", new object[] { newCenterAndZoom });
            }
            else
                return;
        }

        private void SavePointsToDataItem(string mapTypeName, string jsonData)
        {
            DataItem dataItem = new DataItem();
            //using(StreamWriter sw = new StreamWriter)
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("clearAll");
            clearClick = true;
        }


        private void EditCode_Click(object sender, EventArgs e)
        {
            if (isActive)
            {
                this.editorPanel.Visible = true;
                this.editorPanel.Enabled = true;
                this.webBrowser1.Location = new System.Drawing.Point(600, 28);
                this.LoadMapData.Enabled = false;
                this.SavePic.Enabled = false;
                isActive = false;
                
                WebUrl = Path.Combine(Application.StartupPath, "Business\\IAOLab\\WebEngine\\Html", "SourceCodeMap.html");
                webBrowser1.Navigate(WebUrl);
                SavePointsToDisk();
            }
            else
            {
                this.editorPanel.Visible = false;
                this.editorPanel.Enabled = false;
                this.webBrowser1.Location = new System.Drawing.Point(12, 23);
                this.LoadMapData.Enabled = true;
                this.SavePic.Enabled = true;
                isActive = true;
                WebUrl = Path.Combine(Application.StartupPath, "Business\\IAOLab\\WebEngine\\Html", "StartMap.html");
                webBrowser1.Navigate(WebUrl);
                var configMap = new ConfigForm();
                var dialog = new SelectMapDialog(DataItems);
                string configstr = dialog.drawlatude + ',' + dialog.drawlontude + ',' + configMap.scale;
                webBrowser1.Document.InvokeScript("initialMap", new object[] { configstr });
                MapWidget maw = HitTopic.FindWidget<MapWidget>();
                // TODO 没有考虑热力图的持久化
                foreach (DataItem di in maw.DataItems)
                {
                    if (di.FileName.Contains("标注图") && File.Exists(di.FilePath))
                        webBrowser1.Document.InvokeScript("markerPoints", OpenMapFile(di.FilePath));
                    if (di.FileName.Contains("多边形图") && File.Exists(di.FilePath))
                        webBrowser1.Document.InvokeScript("drawPolygon", OpenMapFile(di.FilePath));
                    if (di.FileName.Contains("轨迹图") && File.Exists(di.FilePath))
                        webBrowser1.Document.InvokeScript("drawOrit", OpenMapFile(di.FilePath));
                    if (di.FileName.Contains("热力图") && File.Exists(di.FilePath))
                        webBrowser1.Document.InvokeScript("drawHeatmap", OpenMapFile(di.FilePath));
                }

            }
            LoadHtml();

        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            string tempDir = FileUtil.TryGetSysTempDir();
            Global.TempDirectory = Path.Combine(tempDir, "FiberHomeIAOTemp");
            if (!File.Exists(Path.Combine(Global.TempDirectory, "editorMap.html")))
            {
                StreamWriter strmsave = new StreamWriter(Path.Combine(Global.TempDirectory, "editorMap.html"), false, System.Text.Encoding.UTF8);
                strmsave.Write(this.htmlEditorControlEx1.Text);
                strmsave.Close();
            }
            else
            {
                StreamWriter strmsave = new StreamWriter(Path.Combine(Global.TempDirectory, "editorMap.html"), false, System.Text.Encoding.UTF8);
                strmsave.Write(this.htmlEditorControlEx1.Text);
                strmsave.Close();
            }
            SourceWebUrl = Path.Combine(Global.TempDirectory, "editorMap.html");
            webBrowser1.Navigate(SourceWebUrl);
        }
        private void ResetButton_Click(object sender, EventArgs e)
        {
            LoadHtml();
        }
        public void LoadHtml()
        {
            Stream myStream = new FileStream(Path.Combine(Application.StartupPath, "Business\\IAOLab\\WebEngine\\Html", "SourceCodeMap.html"), FileMode.Open);
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");//若是格式为utf-8的需要将gb2312替换
            StreamReader myStreamReader = new StreamReader(myStream, encode);
            string strhtml = myStreamReader.ReadToEnd();
            myStream.Close();
            this.htmlEditorControlEx1.Text = strhtml;
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
                SavePointsToDisk();

            }
            return base.OnOKButtonClick();
        }

        private void SavePointsToDisk()
        {
            string markerpath = string.Empty;
            string polygonpath = string.Empty;
            string polylinepath = string.Empty;
            string heatmapPath = string.Empty;
            List<DataItem> dataItems = new List<DataItem>();
            MapWidget maw = HitTopic.FindWidget<MapWidget>();
            if (maw != null)
                dataItems = maw.DataItems;
            foreach (DataItem di in dataItems)
            {
                if (di.FileName.Contains("标注图"))
                    markerpath = di.FilePath;
                if (di.FileName.Contains("多边形图"))
                    polygonpath = di.FilePath;
                if (di.FileName.Contains("轨迹图"))
                    polylinepath = di.FilePath;
                if (di.FileName.Contains("热力图"))
                    heatmapPath = di.FilePath;
            }

            if (string.IsNullOrEmpty(markerpath))
            {
                markerpath = Path.Combine(Global.UserWorkspacePath, "业务视图", Global.GetCurrentDocument().Name, String.Format("{0}_标注图{1}.txt", HitTopic.Text, DateTime.Now.ToString("yyyyMMdd_hhmmss")));
                DataItem marker = new DataItem(markerpath, Path.GetFileNameWithoutExtension(markerpath), ',', OpUtil.Encoding.UTF8, OpUtil.ExtType.Text);
                maw.DataItems.Add(marker);
            }
            if (string.IsNullOrEmpty(polygonpath))
            {
                polygonpath = Path.Combine(Global.UserWorkspacePath, "业务视图", Global.GetCurrentDocument().Name, String.Format("{0}_多边形图{1}.txt", HitTopic.Text, DateTime.Now.ToString("yyyyMMdd_hhmmss")));
                DataItem polygon = new DataItem(polygonpath, Path.GetFileNameWithoutExtension(polygonpath), ',', OpUtil.Encoding.UTF8, OpUtil.ExtType.Text);
                maw.DataItems.Add(polygon);
            }
            if (string.IsNullOrEmpty(polylinepath))
            {
                polylinepath = Path.Combine(Global.UserWorkspacePath, "业务视图", Global.GetCurrentDocument().Name, String.Format("{0}_轨迹图{1}.txt", HitTopic.Text, DateTime.Now.ToString("yyyyMMdd_hhmmss")));
                DataItem polyline = new DataItem(polylinepath, Path.GetFileNameWithoutExtension(polylinepath), ',', OpUtil.Encoding.UTF8, OpUtil.ExtType.Text);
                maw.DataItems.Add(polyline);
            }
            if (string.IsNullOrEmpty(heatmapPath))
            {
                heatmapPath = Path.Combine(Global.UserWorkspacePath, "业务视图", Global.GetCurrentDocument().Name, String.Format("{0}_热力图{1}.txt", HitTopic.Text, DateTime.Now.ToString("yyyyMMdd_hhmmss")));
                DataItem heatmap = new DataItem(polylinepath, Path.GetFileNameWithoutExtension(polylinepath), ',', OpUtil.Encoding.UTF8, OpUtil.ExtType.Text);
                maw.DataItems.Add(heatmap);
            }

            if (clearClick)
            {
                File.Delete(markerpath);
                File.Delete(polygonpath);
                File.Delete(polylinepath);
                File.Delete(heatmapPath);
            }
            else
            {
                string temp = markerpath + ',' + polygonpath + ',' + polylinepath + ',' + heatmapPath;
                webBrowser1.Document.InvokeScript("getPath", new object[] { temp });
                webBrowser1.Document.InvokeScript("savePoints");
            }
        }
    }
}
