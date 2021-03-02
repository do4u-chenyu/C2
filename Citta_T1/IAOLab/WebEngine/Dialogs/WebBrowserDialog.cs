using C2.Controls;
using C2.Controls.Left;
using C2.Core;
using C2.Dialogs;
using C2.Model;
using C2.Model.MindMaps;
using C2.Model.Widgets;
using C2.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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
        public string Title { set => this.Text = value; get => this.Text; }
        public string WebUrl;
        public string SourceWebUrl;
        public Topic HitTopic;
        public List<DataItem> DataItems;
        bool isActive = true;

        public Dictionary<string, int[]> ChartOptions;

        private string picPath;
        PictureWidget.PictureDesign _CurrentObject;
        public PictureWidget.PictureDesign CurrentObject
        {
            get { return _CurrentObject; }
            set { _CurrentObject = value; }
        }

        public WebBrowserDialog()
        {
            InitializeComponent();
            Title = string.Empty;
            WebUrl = string.Empty;
            ChartOptions = new Dictionary<string, int[]>();
            WebType = WebType.Null;
            picPath = Path.Combine(Global.TempDirectory, "boss.png");
            SourceWebUrl = string.Empty;
        }

        public WebBrowserDialog(Topic hitTopic, WebType webType) : this()
        {
            HitTopic = hitTopic;
            DataItems = hitTopic.GetDataItems();
            WebType = webType;
        }
        private void WebBrowserDialog_Load(object sender, EventArgs e)
        {
            WebBrowserConfig.SetWebBrowserFeatures(11);//TODO 暂定11，后面需要检测
            webBrowser1.Navigate(WebUrl);
            if (WebType == WebType.Boss)//数据大屏初次打开是自动弹出配置窗口
                OpenSelectBossDialog();
        }

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
            SaveHtml.Click += new System.EventHandler(this.SaveHtml_Click);

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

        void LoadMapData_Click(object sender, EventArgs e)
        {
     
            var dialog = new SelectMapDialog(DataItems);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string[] methodstr = new string[1];
                methodstr[0] = dialog.tude;
                switch (dialog.map)
                {
                    case "标注图":
                        webBrowser1.Document.InvokeScript("markerPoints", methodstr);    
                        break;
                    case "轨迹图":
                        webBrowser1.Document.InvokeScript("drawOrit", methodstr);
                        break;
                    case "多边形图":
                        webBrowser1.Document.InvokeScript("drawPolygon", methodstr);
                        break;
                    case "热力图":
                        webBrowser1.Document.InvokeScript("relitu", methodstr);
                        break;
                }
            }
            else
                return;
           
        }

        void LoadBossData_Click(object sender, EventArgs e)
        {
            OpenSelectBossDialog();
        }

        public void OpenSelectBossDialog()
        {
            var dialog = new SelectBossDialog(DataItems, ChartOptions);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                webBrowser1.Navigate(dialog.WebUrl);
                ChartOptions = dialog.ChartOptions;
            }
        }

        void SavePic_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(webBrowser1.Width, webBrowser1.Height);
            Rectangle rectangle = new Rectangle(0, 0, webBrowser1.Width, webBrowser1.Height);  // 绘图区域
            webBrowser1.DrawToBitmap(bitmap, rectangle);
            bitmap.Save(picPath);

            SaveFileDialog fd = new SaveFileDialog
            {
                Filter = "图片文件(*.png)|*.png",
                AddExtension = true
            };
            if (fd.ShowDialog() != DialogResult.OK)
                return;
            File.Copy(picPath, fd.FileName, true);
        }

        void SaveHtml_Click(object sender, EventArgs e)
        {
           //这里有待探讨
        }

        private void Clear_Click(object sender, EventArgs e)
        {
           
            webBrowser1.Document.InvokeScript("clearAll");
            
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
            }
            else
            {
                this.editorPanel.Visible = false;
                this.editorPanel.Enabled = false;
                this.webBrowser1.Location = new System.Drawing.Point(12, 23);
                this.LoadMapData.Enabled = true;
                this.SavePic.Enabled = true;
                isActive = true;
            }
            LoadHtml();
            WebUrl = Path.Combine(Application.StartupPath, "IAOLab\\WebEngine\\Html", "SourceCodeMap.html");
            webBrowser1.Navigate(WebUrl);
        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if(WebType == WebType.Map)
            {
                var configMap = new ConfigForm();
                string configstr = configMap.latude + ',' + configMap.lontude + ',' + configMap.scale;
                webBrowser1.Document.InvokeScript("initialMap", new object[] { configstr });
            }
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            string tempDir = FileUtil.TryGetSysTempDir();
            Global.TempDirectory = Path.Combine(tempDir, "FiberHomeIAOTemp");
            if (!File.Exists(Path.Combine(Global.TempDirectory, "editorMap.html")))
            {
                StreamWriter strmsave = new StreamWriter(Path.Combine(Global.TempDirectory, "editorMap.html"), false, System.Text.Encoding.Default);
                strmsave.Write(this.htmlEditorControlEx1.Text);
                strmsave.Close();
            }
            else
            {
                StreamWriter strmsave = new StreamWriter(Path.Combine(Global.TempDirectory, "editorMap.html"), false, System.Text.Encoding.Default);
                strmsave.Write(this.htmlEditorControlEx1.Text);
                strmsave.Close();
            }
            SourceWebUrl = Path.Combine(Global.TempDirectory, "editorMap.html");
            webBrowser1.Navigate(SourceWebUrl);
        }

        public void LoadHtml()
        {
            Stream myStream = new FileStream(@"D:\work\C2\Citta_T1\IAOLab\WebEngine\Html\SourceCodeMap.html", FileMode.Open);
            Encoding encode = System.Text.Encoding.GetEncoding("gb2312");//若是格式为utf-8的需要将gb2312替换
            StreamReader myStreamReader = new StreamReader(myStream, encode);
            string strhtml = myStreamReader.ReadToEnd();
            myStream.Close();
            this.htmlEditorControlEx1.Text = strhtml;
        }
        public void SaveEditorHtml()
        {
            //StreamWriter strmsave = new StreamWriter(Path.Combine(Global.TempDirectory, "editorMap.html"), false, System.Text.Encoding.Default);
            

        }
        private void resetButton_Click(object sender, EventArgs e)
        {
            LoadHtml();
        }

        private void WebBrowserDialog_Activated(object sender, EventArgs e)
        {
            webBrowser1.Focus();
        }

        protected override bool OnOKButtonClick()
        {
            if(WebType == WebType.Boss && ChartOptions.ContainsKey("Datasource"))
            {
                Bitmap bitmap = new Bitmap(webBrowser1.Width, webBrowser1.Height);
                Rectangle rectangle = new Rectangle(0, 0, webBrowser1.Width, webBrowser1.Height);  // 绘图区域
                webBrowser1.DrawToBitmap(bitmap, rectangle);
                bitmap.Save(picPath);//TODO phx 考虑是否放入对应业务视图，并且考虑名字

                //当前webbrowser截图，作为图片挂件加入当前节点
                var template = new PictureWidget();
                CurrentObject = new PictureWidget.PictureDesign();
                CurrentObject.SourceType = PictureSource.File;
                CurrentObject.Url = picPath;
                CurrentObject.AddToLibrary = false;
                CurrentObject.LimitImageSize = true;
                CurrentObject.Name = Path.GetFileNameWithoutExtension(picPath);
                CurrentObject.EmbedIn = false;
                template.Image = CurrentObject;
                HitTopic.Add(template);
            }

            string Markerpath = Path.Combine(Global.UserWorkspacePath, "业务视图", Global.GetCurrentDocument().Name, HitTopic.Text, String.Format("{0}_标注图{1}.txt", HitTopic.Text, DateTime.Now.ToString("yyyyMMdd_hhmmss")));
            string Polygonpath = Path.Combine(Global.UserWorkspacePath, "业务视图", Global.GetCurrentDocument().Name, HitTopic.Text, String.Format("{0}_多边形图{1}.txt", HitTopic.Text, DateTime.Now.ToString("yyyyMMdd_hhmmss")));
            string Polylinepath = Path.Combine(Global.UserWorkspacePath, "业务视图", Global.GetCurrentDocument().Name, HitTopic.Text, String.Format("{0}_折线图{1}.txt", HitTopic.Text, DateTime.Now.ToString("yyyyMMdd_hhmmss")));
            //if (!File.Exists(Markerpath))
            //{
            //    FileStream fs = new FileStream(Markerpath, FileMode.Create, FileAccess.Write);
            //    fs.Close();
            //}
            string path = Markerpath + ',' + Polygonpath + ',' + Polylinepath;
            webBrowser1.Document.InvokeScript("getPath", new object[] { path });
            

            return base.OnOKButtonClick();
        }
    }
}
