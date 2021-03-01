using C2.Controls;
using C2.Dialogs;
using C2.Model;
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

        public string Title { set => this.Text = value; }
        public string WebUrl;
        public List<DataItem> DataItems;
        bool isActive = true;

        public Dictionary<string, int[]> ChartOptions;

        public WebBrowserDialog()
        {
            InitializeComponent();
            Title = string.Empty;
            WebUrl = string.Empty;
            DataItems = new List<DataItem>();
            ChartOptions = new Dictionary<string, int[]>();
        }

        private void WebBrowserDialog_Load(object sender, EventArgs e)
        {
            WebBrowserConfig.SetWebBrowserFeatures(11);//TODO 暂定11，后面需要检测
            webBrowser1.Navigate(WebUrl);
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
            LoadMapData.Image = global::C2.Properties.Resources.importDataSource;
            LoadMapData.Text = "导入数据";
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
            LoadBossData.Image = global::C2.Properties.Resources.importDataSource;
            LoadBossData.Text = "导入数据";
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
            var dialog = new SelectBossDialog(DataItems, ChartOptions);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                webBrowser1.Navigate(dialog.WebUrl);
                ChartOptions = dialog.ChartOptions;
            }  
        }

        void SavePic_Click(object sender, EventArgs e)
        {
            SaveFileDialog fd = new SaveFileDialog
            {
                Filter = "图片文件(*.png)|*.png",
                AddExtension = true
            };
            if (fd.ShowDialog() != DialogResult.OK)
                return;

            Bitmap bitmap = new Bitmap(webBrowser1.Width, webBrowser1.Height);
            Rectangle rectangle = new Rectangle(0, 0, webBrowser1.Width, webBrowser1.Height);  // 绘图区域
            webBrowser1.DrawToBitmap(bitmap, rectangle);
            bitmap.Save(fd.FileName);
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
                this.panel1.Visible = true;
                this.panel1.Enabled = true;
                this.webBrowser1.Location = new System.Drawing.Point(600, 28);
                this.webBrowser1.Width = 750;
                this.SaveHtml.Enabled = false;
                this.SavePic.Enabled = false;
                this.SaveHtml.Enabled = false;
                this.LoadMapData.Enabled = false;
                this.Clear.Enabled = false;
                isActive = false;
            }
            else
            {
                this.panel1.Visible = false;
                this.panel1.Enabled = false;
                this.webBrowser1.Location = new System.Drawing.Point(12, 23);
                this.webBrowser1.Width = 1340;
                this.SaveHtml.Enabled = true;
                this.SavePic.Enabled = true;
                this.SaveHtml.Enabled = true;
                this.LoadMapData.Enabled = true;
                this.Clear.Enabled = true;
                isActive = true;
            }
            LoadHtml();
            SaveEditorHtml();
        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var configMap = new ConfigForm();
            string configstr = configMap.latude + ',' + configMap.lontude + ',' + configMap.scale;
            webBrowser1.Document.InvokeScript("initialMap", new object[] { configstr });

        }

        private void runButton_Click(object sender, EventArgs e)
        {
            SaveEditorHtml();        
            //WebUrl = Path.Combine(Application.StartupPath, "IAOLab\\WebEngine\\Html", "StartMap.html");
            //webBrowser1.Navigate(WebUrl);

        }

        public void LoadHtml()
        {
            Stream myStream = new FileStream(@"D:\work\C2\Citta_T1\IAOLab\WebEngine\Html\StartMap.html", FileMode.Open);
            Encoding encode = System.Text.Encoding.GetEncoding("gb2312");//若是格式为utf-8的需要将gb2312替换
            StreamReader myStreamReader = new StreamReader(myStream, encode);
            string strhtml = myStreamReader.ReadToEnd();
            myStream.Close();
            this.htmlEditorControlEx1.Text = strhtml;
        }
        public void SaveEditorHtml()
        {
            //这个函数需要确定一个存放临时文件的位置，如果没有临时文件，是不是换种调用语法就可以实现直接调用？？？？
        }
        private void resetButton_Click(object sender, EventArgs e)
        {
            LoadHtml();
        }

        private void WebBrowserDialog_Activated(object sender, EventArgs e)
        {
            webBrowser1.Focus();
        }
    }
}
